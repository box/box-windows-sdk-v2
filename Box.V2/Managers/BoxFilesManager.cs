using Box.V2.Auth;
using Box.V2.Contracts;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
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
        public async Task<File> GetInformationAsync(string id)
        {
            CheckPrerequisite(id);

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<File> response = await ToResponseAsync<File>(request);

            return response.ResponseObject;
        }

        /// <summary>
        /// Returns the byte array of the requested file
        /// </summary>
        /// <param name="id">Id of the file to download</param>
        /// <returns>byte[] of the requested file</returns>
        public async Task<byte[]> DownloadBytesAsync(string id)
        {
            CheckPrerequisite(id);

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.ContentPathString, id))
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<byte[]> response = await ToResponseAsync<byte[]>(request, true);

            return response.ResponseObject;
        }

        /// <summary>
        /// Returns the stream of the requested file
        /// </summary>
        /// <param name="id">Id of the file to download</param>
        /// <returns>MemoryStream of the requested file</returns>
        public async Task<Stream> DownloadStreamAsync(string id)
        {
            CheckPrerequisite(id);

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.ContentPathString, id))
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<Stream> response = await ToResponseAsync<Stream>(request);

            return response.ResponseObject;
        }

        /// <summary>
        /// Uploads a provided file to the target parent folder
        /// If the file already exists, an error will be thrown
        /// </summary>
        /// <param name="fileRequest"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async Task<File> UploadAsync(BoxFileRequest fileRequest, Stream stream)
        {
            stream.ThrowIfNull("stream");
            CheckPrerequisite(
                fileRequest.ThrowIfNull("fileRequest").Name,
                fileRequest.Parent.ThrowIfNull("fileRequest.Parent").Id);

            BoxMultiPartRequest request = new BoxMultiPartRequest(_config.FilesUploadEndpointUri)
                .Authorize(_auth.Session.AccessToken)
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

            IBoxResponse<Collection<File>> response = await ToResponseAsync<Collection<File>>(request, true);

            // We can only upload one file at a time, so return the first entry
            return response.ResponseObject.Entries.FirstOrDefault();
        }

        public async Task<File> UploadAsync(BoxFileRequest fileRequest, byte[] file)
        {

            file.ThrowIfNull("file");
            CheckPrerequisite(
                fileRequest.ThrowIfNull("fileRequest").Name,
                fileRequest.Parent.ThrowIfNull("fileRequest.Parent").Id);

            BoxMultiPartRequest request = new BoxMultiPartRequest(_config.FilesUploadEndpointUri)
                .Authorize(_auth.Session.AccessToken)
                .FormPart(new BoxStringFormPart()
                {
                    Name = "metadata",
                    Value = _converter.Serialize(fileRequest)
                });

            IBoxResponse<File> response = await ToResponseAsync<File>(request, true);

            return response.ResponseObject;
        }

        public async Task<File> UploadNewVersionAsync(string etag, string fileName, Stream stream)
        {
            stream.ThrowIfNull("stream");
            CheckPrerequisite(etag, fileName);

            BoxMultiPartRequest request = new BoxMultiPartRequest(_config.FilesUploadEndpointUri)
                .Header("If-Match", etag)
                .Authorize(_auth.Session.AccessToken)
                .FormPart(new BoxFileFormPart()
                {
                    Name = "filename",
                    Value = stream,
                    FileName = fileName
                });

            IBoxResponse<Collection<File>> response = await ToResponseAsync<Collection<File>>(request);

            // We can only upload one file at a time, so return the first entry
            return response.ResponseObject.Entries.FirstOrDefault();
        }

        public async Task<File> ViewVersionsAsync(string id)
        {
            CheckPrerequisite(id);

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.VersionsPathString, id))
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<Collection<File>> response = await ToResponseAsync<Collection<File>>(request);

            return response.ResponseObject.Entries.FirstOrDefault();
        }

        public async Task<File> UpdateInformationAsync(BoxFileRequest fileRequest)
        {
            CheckPrerequisite(fileRequest.ThrowIfNull("fileRequest").Id);

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, fileRequest.Id)
                .Method(RequestMethod.PUT)
                .Authorize(_auth.Session.AccessToken);
            request.Payload = _converter.Serialize(fileRequest);

            IBoxResponse<File> response = await _service.ToResponseAsync<File>(request);

            return response.ResponseObject;
        }

        public async Task<bool> DeleteAsync(string id, string etag)
        {
            CheckPrerequisite(id, etag);

            BoxRequest request = new BoxRequest(_config.FilesUploadEndpointUri, id)
                .Method(RequestMethod.DELETE)
                .Authorize(_auth.Session.AccessToken)
                .Header("If-Match", etag);

            IBoxResponse<File> response = await ToResponseAsync<File>(request);

            return response.Status == ResponseStatus.Success;
        }

        public async Task<File> CopyAsync(BoxFileRequest fileRequest)
        {
            CheckPrerequisite(fileRequest.ThrowIfNull("fileRequest").Name,
                fileRequest.Parent.ThrowIfNull("fileRequest.Parent").Id);

            BoxRequest request = new BoxRequest(_config.FilesUploadEndpointUri, string.Format(Constants.CopyPathString, fileRequest.Id))
                .Method(RequestMethod.POST)
                .Authorize(_auth.Session.AccessToken);
            request.Payload = _converter.Serialize(fileRequest);

            IBoxResponse<File> response = await ToResponseAsync<File>(request);

            return response.ResponseObject;
        }

        public async Task<File> CreateSharedLinkAsync(string id, BoxSharedLinkRequest sharedLink)
        {
            CheckPrerequisite(id);
            if (!sharedLink.ThrowIfNull("sharedLink").Access.HasValue)
                throw new ArgumentException("A required field is missing", "sharedLink.Access");

            BoxRequest request = new BoxRequest(_config.FilesUploadEndpointUri, id)
                .Method(RequestMethod.POST)
                .Authorize(_auth.Session.AccessToken);
            request.Payload = _converter.Serialize(sharedLink);

            IBoxResponse<File> response = await ToResponseAsync<File>(request);

            return response.ResponseObject;
        }


        /// <summary>
        /// Retrieves the comments on a particular file, if any exist.
        /// </summary>
        /// <param name="id">The Id of the item the comments should be retrieved for</param>
        /// <returns>A Collection of comment objects are returned. If there are no comments on the file, an empty comments array is returned</returns>
        public async Task<Collection<Comment>> GetCommentsAsync(string id)
        {
            CheckPrerequisite(id);

            BoxRequest request = new BoxRequest(_config.FilesUploadEndpointUri, string.Format(Constants.CommentsPathString, id))
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<Collection<Comment>> response = await ToResponseAsync<Collection<Comment>>(request);

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
        /// <returns></returns>
        public async Task<Stream> GetThumbnailAsync(string id, int? minHeight = null, int? minWidth = null, int? maxHeight = null, int? maxWidth = null)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Invalid parameters for required fields");

            BoxRequest request = new BoxRequest(_config.FilesUploadEndpointUri, string.Format(Constants.ThumbnailPathString, id))
                .Authorize(_auth.Session.AccessToken)
                .Param("min_height", minHeight.ToString())
                .Param("min_width", minWidth.ToString())
                .Param("max_height", maxHeight.ToString())
                .Param("max_width", maxWidth.ToString());

            IBoxResponse<Stream> response = await ToResponseAsync<Stream>(request);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves an item that has been moved to the trash.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The full item will be returned, including information about when the it was moved to the trash.</returns>
        public async Task<File> GetTrashedAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Invalid parameters for required fields");

            BoxRequest request = new BoxRequest(_config.FilesUploadEndpointUri, string.Format(Constants.TrashPathString, id))
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<File> response = await ToResponseAsync<File>(request);

            return response.ResponseObject;
        }

        /// <summary>
        /// Restores an item that has been moved to the trash. Default behavior is to restore the item to the folder it was in before 
        /// it was moved to the trash. If that parent folder no longer exists or if there is now an item with the same name in that 
        /// parent folder, the new parent folder and/or new name will need to be included in the request.
        /// </summary>
        /// <returns>The full item will be returned with a 201 Created status. By default it is restored to the parent folder it was in before it was trashed.</returns>
        public async Task<File> RestoreTrashedAsync(BoxFileRequest fileReq)
        {
            if (string.IsNullOrWhiteSpace(fileReq.Id) || 
                string.IsNullOrWhiteSpace(fileReq.Name))
                throw new ArgumentException("Invalid parameters for required fields");

            BoxRequest request = new BoxRequest(_config.FilesUploadEndpointUri, fileReq.Id)
                .Authorize(_auth.Session.AccessToken)
                .Method(RequestMethod.POST);
            request.Payload = _converter.Serialize(fileReq);

            IBoxResponse<File> response = await ToResponseAsync<File>(request);

            return response.ResponseObject;
        }

        /// <summary>
        /// Permanently deletes an item that is in the trash. The item will no longer exist in Box. This action cannot be undone.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An empty 204 No Content response will be returned upon successful deletion</returns>
        public async Task<bool> PurgeTrashedAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Invalid parameters for required fields");

            BoxRequest request = new BoxRequest(_config.FilesUploadEndpointUri, string.Format(Constants.TrashPathString, id))
                .Method(RequestMethod.DELETE)
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<File> response = await ToResponseAsync<File>(request);

            return response.Status == ResponseStatus.Success;
        }
    }
}
