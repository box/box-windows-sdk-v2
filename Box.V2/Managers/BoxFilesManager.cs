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
            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id);
            AddAuthentication(request);

            IBoxResponse<File> response = await _service.ToResponseAsync<File>(request);

            return response.ResponseObject;
        }

        /// <summary>
        /// Returns the byte array of the requested file
        /// </summary>
        /// <param name="id">Id of the file to download</param>
        /// <returns>byte[] of the requested file</returns>
        public async Task<byte[]> DownloadBytesAsync(string id)
        {
            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.ContentPathString, id));
            AddAuthentication(request);

            IBoxResponse<byte[]> response = await _service.EnqueueAsync<byte[]>(request);

            return response.ResponseObject;
        }

        /// <summary>
        /// Returns the stream of the requested file
        /// </summary>
        /// <param name="id">Id of the file to download</param>
        /// <returns>MemoryStream of the requested file</returns>
        public async Task<MemoryStream> DownloadStreamAsync(string id)
        {
            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.ContentPathString, id));
            AddAuthentication(request);

            IBoxResponse<MemoryStream> response = await _service.ToResponseAsync<MemoryStream>(request);

            return response.ResponseObject;
        }

        /// <summary>
        /// Uploads a provided file to the target parent folder
        /// If the file already exists, an error will be thrown
        /// </summary>
        /// <param name="fileReq"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async Task<File> UploadAsync(BoxFileRequest fileReq, Stream stream)
        {
            if (string.IsNullOrWhiteSpace(fileReq.Name) ||
                string.IsNullOrWhiteSpace(fileReq.Parent.Id) ||
                stream == null)
                throw new ArgumentException("Invalid parameters for required fields");

            BoxMultiPartRequest request = new BoxMultiPartRequest(_config.FilesUploadEndpointUri)
                .FormPart(new BoxStringFormPart()
                {
                    Name = "metadata",
                    Value = _converter.Serialize(fileReq)
                })
                .FormPart(new BoxFileFormPart()
                {
                    Name = "file",
                    Value = stream,
                    FileName = fileReq.Name
                });
            AddAuthentication(request);

            IBoxResponse<Collection<File>> response = await _service.ToResponseAsync<Collection<File>>(request);

            // We can only upload one file at a time, so return the first entry
            return response.ResponseObject.Entries.FirstOrDefault();
        }

        public async Task<File> UploadAsync(BoxFileRequest fileReq, byte[] file)
        {
            if (string.IsNullOrWhiteSpace(fileReq.Name) ||
                string.IsNullOrWhiteSpace(fileReq.Parent.Id))
                throw new ArgumentException("Invalid parameters for required fields");

            BoxMultiPartRequest request = new BoxMultiPartRequest(_config.FilesUploadEndpointUri)
                .FormPart(new BoxStringFormPart()
                {
                    Name = "metadata",
                    Value = _converter.Serialize(fileReq)
                });

            AddAuthentication(request);

            IBoxResponse<File> response = await _service.ToResponseAsync<File>(request);

            return response.ResponseObject;
        }

        public async Task<File> UploadNewVersionAsync(string etag, string fileName, Stream stream)
        {
            if (string.IsNullOrWhiteSpace(etag) ||
                    string.IsNullOrWhiteSpace(fileName) ||
                    stream == null)
                throw new ArgumentException("Invalid parameters for required fields");

            BoxMultiPartRequest request = new BoxMultiPartRequest(_config.FilesUploadEndpointUri)
                .Header("If-Match", etag)
                .FormPart(new BoxFileFormPart()
                {
                    Name = "filename",
                    Value = stream,
                    FileName = fileName
                });
            AddAuthentication(request);

            IBoxResponse<Collection<File>> response = await _service.ToResponseAsync<Collection<File>>(request);

            // We can only upload one file at a time, so return the first entry
            return response.ResponseObject.Entries.FirstOrDefault();
        }

        public async Task<File> ViewVersionsAsync(string id)
        {
            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.VersionsPathString, id));
            AddAuthentication(request);

            IBoxResponse<Collection<File>> response = await _service.ToResponseAsync<Collection<File>>(request);

            return response.ResponseObject.Entries.FirstOrDefault();
        }

        public async Task<File> UpdateInformationAsync(BoxFileRequest fileRequest)
        {
            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, fileRequest.Id)
                .Method(RequestMethod.PUT);
            request.Payload = _converter.Serialize(fileRequest);
            AddAuthentication(request);

            IBoxResponse<File> response = await _service.ToResponseAsync<File>(request);

            return response.ResponseObject;
        }

        public async Task<bool> DeleteAsync(string id, string etag)
        {
            BoxRequest request = new BoxRequest(_config.FilesUploadEndpointUri, id)
                .Method(RequestMethod.DELETE)
                .Header("If-Match", etag);

            AddAuthentication(request);

            IBoxResponse<File> response = await _service.ToResponseAsync<File>(request);

            return response.Status == ResponseStatus.Success;
        }

        public async Task<File> CopyAsync(BoxFileRequest fileReq)
        {
            if (string.IsNullOrWhiteSpace(fileReq.Name) ||
                string.IsNullOrWhiteSpace(fileReq.Parent.Id))
                throw new ArgumentException("Invalid parameters for required fields");

            BoxRequest request = new BoxRequest(_config.FilesUploadEndpointUri, string.Format(Constants.CopyPathString, fileReq.Id))
                .Method(RequestMethod.POST);
            request.Payload = _converter.Serialize(fileReq);
            AddAuthentication(request);

            IBoxResponse<File> response = await _service.ToResponseAsync<File>(request);

            return response.ResponseObject;
        }

        public async Task<File> CreateSharedLinkAsync(string id, BoxSharedLinkRequest sharedLink)
        {
            if (string.IsNullOrWhiteSpace(id) ||
                sharedLink.Access == null)
                throw new ArgumentException("Invalid parameters for required fields");

            BoxRequest request = new BoxRequest(_config.FilesUploadEndpointUri, id)
                .Method(RequestMethod.POST);
            request.Payload = _converter.Serialize(sharedLink);
            AddAuthentication(request);

            IBoxResponse<File> response = await _service.ToResponseAsync<File>(request);

            return response.ResponseObject;
        }


        /// <summary>
        /// Retrieves the comments on a particular file, if any exist.
        /// </summary>
        /// <param name="id">The Id of the item the comments should be retrieved for</param>
        /// <returns>A Collection of comment objects are returned. If there are no comments on the file, an empty comments array is returned</returns>
        public async Task<Collection<Comment>> GetCommentsAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Invalid parameters for required fields");

            BoxRequest request = new BoxRequest(_config.FilesUploadEndpointUri, string.Format(Constants.CommentsPathString, id));
            AddAuthentication(request);

            IBoxResponse<Collection<Comment>> response = await _service.ToResponseAsync<Collection<Comment>>(request);

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
                .Param("min_height", minHeight.ToString())
                .Param("min_width", minWidth.ToString())
                .Param("max_height", maxHeight.ToString())
                .Param("max_width", maxWidth.ToString());
            AddAuthentication(request);

            IBoxResponse<Stream> response = await _service.ToResponseAsync<Stream>(request);

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

            BoxRequest request = new BoxRequest(_config.FilesUploadEndpointUri, string.Format(Constants.TrashPathString, id));
            AddAuthentication(request);

            IBoxResponse<File> response = await _service.ToResponseAsync<File>(request);

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
                .Method(RequestMethod.POST);
            request.Payload = _converter.Serialize(fileReq);
            AddAuthentication(request);

            IBoxResponse<File> response = await _service.ToResponseAsync<File>(request);

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
                .Method(RequestMethod.DELETE);
            AddAuthentication(request);

            IBoxResponse<File> response = await _service.ToResponseAsync<File>(request);

            return response.Status == ResponseStatus.Success;
        }
    }
}
