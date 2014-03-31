using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    /// <summary>
    /// File objects represent that metadata about individual files in Box, with attributes describing who created the file, 
    /// when it was last modified, and other information. 
    /// </summary>
    public class BoxFilesManager : BoxResourceManager
    {
        public BoxFilesManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth) { }

        /// <summary>
        /// Gets a file object representation of the provided file Id
        /// </summary>
        /// <param name="id">Id of file information to retrieve</param>
        /// <param name="limit">The number of items to return (default=100, max=1000)</param>
        /// <param name="offset">The item at which to begin the response (default=0)</param>
        /// <returns></returns>
        public async Task<BoxFile> GetInformationAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Param(ParamFields, fields);


            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Returns the stream of the requested file
        /// </summary>
        /// <param name="id">Id of the file to download</param>
        /// <returns>MemoryStream of the requested file</returns>
        public async Task<Stream> DownloadStreamAsync(string id, string versionId = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.ContentPathString, id))
                .Param("version", versionId);

            IBoxResponse<Stream> response = await ToResponseAsync<Stream>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Uploads a provided file to the target parent folder
        /// If the file already exists, an error will be thrown
        /// </summary>
        /// <param name="fileRequest"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async Task<BoxFile> UploadAsync(BoxFileRequest fileRequest, Stream stream, List<string> fields = null)
        {
            stream.ThrowIfNull("stream");
            fileRequest.ThrowIfNull("fileRequest")
                .Name.ThrowIfNullOrWhiteSpace("filedRequest.Name");
            fileRequest.Parent.ThrowIfNull("fileRequest.Parent")
                .Id.ThrowIfNullOrWhiteSpace("fileRequest.Parent.Id");

            BoxMultiPartRequest request = new BoxMultiPartRequest(_config.FilesUploadEndpointUri)
                .Param(ParamFields, fields)
                .FormPart(new BoxStringFormPart()
                {
                    Name = "metadata",
                    Value = _converter.Serialize(fileRequest)
                })
                .FormPart(new BoxFileFormPart()
                {
                    Name = "file",
                    Value = stream,
                    FileName = fileRequest.Name
                });

            IBoxResponse<BoxCollection<BoxFile>> response = await ToResponseAsync<BoxCollection<BoxFile>>(request).ConfigureAwait(false);

            // We can only upload one file at a time, so return the first entry
            return response.ResponseObject.Entries.FirstOrDefault();
        }

        /// <summary>
        /// This method is used to upload a new version of an existing file in a user’s account. Similar to regular file uploads, 
        /// these are performed as multipart form uploads An optional If-Match header can be included to ensure that client only 
        /// overwrites the file if it knows about the latest version. The filename on Box will remain the same as the previous version.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="stream"></param>
        /// <param name="etag"></param>
        /// <returns></returns>
        public async Task<BoxFile> UploadNewVersionAsync(string fileName, string fileId, Stream stream, string etag = null, List<string> fields = null)
        {
            stream.ThrowIfNull("stream");
            fileName.ThrowIfNullOrWhiteSpace("fileName");

            BoxMultiPartRequest request = new BoxMultiPartRequest(new Uri(string.Format(Constants.FilesNewVersionEndpointString, fileId)))
                .Header("If-Match", etag)
                .Param(ParamFields, fields)
                .FormPart(new BoxFileFormPart()
                {
                    Name = "filename",
                    Value = stream,
                    FileName = fileName
                });

            IBoxResponse<BoxCollection<BoxFile>> response = await ToResponseAsync<BoxCollection<BoxFile>>(request).ConfigureAwait(false);

            // We can only upload one file at a time, so return the first entry
            return response.ResponseObject.Entries.FirstOrDefault();
        }

        /// <summary>
        /// If there are previous versions of this file, this method can be used to retrieve metadata about the older versions.
        /// <remarks>Versions are only tracked for Box users with premium accounts.</remarks>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BoxCollection<BoxFileVersion>> ViewVersionsAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.VersionsPathString, id))
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxFileVersion>> response = await ToResponseAsync<BoxCollection<BoxFileVersion>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to update individual or multiple fields in the file object, including renaming the file, changing it’s description, 
        /// and creating a shared link for the file. To move a file, change the ID of its parent folder. An optional etag
        /// can be included to ensure that client only updates the file if it knows about the latest version.
        /// </summary>
        /// <param name="fileRequest"></param>
        /// <returns></returns>
        public async Task<BoxFile> UpdateInformationAsync(BoxFileRequest fileRequest, string etag = null, List<string> fields = null)
        {
            fileRequest.ThrowIfNull("fileRequest")
                .Id.ThrowIfNullOrWhiteSpace("fileRequest.Id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, fileRequest.Id)
                .Method(RequestMethod.Put)
                .Header("If-Match", etag)
                .Param(ParamFields, fields);

            request.Payload = _converter.Serialize(fileRequest);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Discards a file to the trash. The etag of the file can be included as an ‘If-Match’ header to prevent race conditions.
        /// <remarks>Depending on the enterprise settings for this user, the item will either be actually deleted from Box or moved to the trash.</remarks>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="etag"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string id, string etag=null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Method(RequestMethod.Delete)
                .Header("If-Match", etag);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Used to create a copy of a file in another folder. The original version of the file will not be altered.
        /// </summary>
        /// <param name="fileRequest"></param>
        /// <returns></returns>
        public async Task<BoxFile> CopyAsync(BoxFileRequest fileRequest, List<string> fields = null)
        {
            fileRequest.ThrowIfNull("fileRequest")
                .Name.ThrowIfNullOrWhiteSpace("fileRequest.Name");
            fileRequest.Parent.ThrowIfNull("fileRequest.Parent")
                .Id.ThrowIfNullOrWhiteSpace("fileRequest.Parent.Id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.CopyPathString, fileRequest.Id))
                .Method(RequestMethod.Post)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(fileRequest));

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to create a shared link for this particular file. Please see here for more information on the permissions available for shared links. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sharedLinkRequest"></param>
        /// <returns></returns>
        public async Task<BoxFile> CreateSharedLinkAsync(string id, BoxSharedLinkRequest sharedLinkRequest, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");
            if (!sharedLinkRequest.ThrowIfNull("sharedLinkRequest").Access.HasValue)
                throw new ArgumentNullException("sharedLink.Access");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Method(RequestMethod.Put)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(new BoxItemRequest() { SharedLink = sharedLinkRequest }));

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves the comments on a particular file, if any exist.
        /// </summary>
        /// <param name="id">The Id of the item the comments should be retrieved for</param>
        /// <returns>A Collection of comment objects are returned. If there are no comments on the file, an empty comments array is returned</returns>
        public async Task<BoxCollection<BoxComment>> GetCommentsAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.CommentsPathString, id))
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxComment>> response = await ToResponseAsync<BoxCollection<BoxComment>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves a thumbnail, or smaller image representation, of this file. Sizes of 32x32, 64x64, 128x128, and 256x256 can be returned. 
        /// Currently thumbnails are only available in .png format and will only be generated for
        /// <see cref="http://en.wikipedia.org/wiki/Image_file_formats"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="minHeight"></param>
        /// <param name="minWidth"></param>
        /// <param name="maxHeight"></param>
        /// <param name="maxWidth"></param>
        /// <param name="throttle">Whether the requests will be throttled. Recommended to be left true to prevent spamming the server</param>
        /// <returns></returns>
        public async Task<Stream> GetThumbnailAsync(string id, int? minHeight = null, int? minWidth = null, int? maxHeight = null, int? maxWidth = null, bool throttle = true)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.ThumbnailPathString, id))
                .Param("min_height", minHeight.ToString())
                .Param("min_width", minWidth.ToString())
                .Param("max_height", maxHeight.ToString())
                .Param("max_width", maxWidth.ToString());

            IBoxResponse<Stream> response = await ToResponseAsync<Stream>(request, throttle).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Gets the stream of a preview page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns>A PNG of the preview</returns>
        public async Task<Stream> GetPreviewAsync(string id, int page)
        {
            return (await GetPreviewResponseAsync(id, page)).ResponseObject;
        }

        /// <summary>
        /// Get the preview and return a BoxFilePreview response. 
        /// </summary>
        /// <param name="id">id of the file to return</param>
        /// <param name="page">page number of the file</param>
        /// <returns>BoxFilePreview that contains the stream, current page number and total number of pages in the file.</returns>
        public async Task<BoxFilePreview> GetFilePreviewAsync(string id, int page, int? maxWidth = null, int? minWidth = null, int? maxHeight = null, int? minHeight = null)
        {  
            IBoxResponse<Stream> response = await GetPreviewResponseAsync(id, page, maxWidth, minWidth, maxHeight, minHeight);

            BoxFilePreview filePreview = new BoxFilePreview();
            filePreview.CurrentPage = page;
            filePreview.ReturnedStatusCode = response.StatusCode;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                filePreview.PreviewStream = response.ResponseObject ;
                filePreview.TotalPages = response.BuildPagesCount();
            }

            return filePreview;
        }

        private async Task<IBoxResponse<Stream>> GetPreviewResponseAsync(string id, int page, int? maxWidth = null, int? minWidth = null, int? maxHeight = null, int? minHeight = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.PreviewPathString, id))
                .Param("page", page.ToString())
                .Param("max_width", maxWidth.ToString())
				.Param("max_height", maxHeight.ToString())
				.Param("min_width", minWidth.ToString())
				.Param("min_height", minHeight.ToString());

            return await ToResponseAsync<Stream>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves an item that has been moved to the trash.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The full item will be returned, including information about when the it was moved to the trash.</returns>
        public async Task<BoxFile> GetTrashedAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.TrashPathString, id))
                .Param(ParamFields, fields);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Restores an item that has been moved to the trash. Default behavior is to restore the item to the folder it was in before 
        /// it was moved to the trash. If that parent folder no longer exists or if there is now an item with the same name in that 
        /// parent folder, the new parent folder and/or new name will need to be included in the request.
        /// </summary>
        /// <returns>The full item will be returned with a 201 Created status. By default it is restored to the parent folder it was in before it was trashed.</returns>
        public async Task<BoxFile> RestoreTrashedAsync(BoxFileRequest fileRequest, List<string> fields = null)
        {
            fileRequest.ThrowIfNull("fileRequest")
                .Id.ThrowIfNullOrWhiteSpace("fileRequest.Id");
            fileRequest.Name.ThrowIfNullOrWhiteSpace("fileRequest.Name");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, fileRequest.Id)
                .Method(RequestMethod.Post)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(fileRequest));

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Permanently deletes an item that is in the trash. The item will no longer exist in Box. This action cannot be undone.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An empty 204 No Content response will be returned upon successful deletion</returns>
        public async Task<bool> PurgeTrashedAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.TrashPathString, id))
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }
    }
}
