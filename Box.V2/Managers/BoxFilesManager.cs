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

        public async Task<File> UploadNewVersionAsync(BoxFileRequest fileReq, Stream stream)
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
        public async Task<File> ViewVersionsAsync() { return new File(); }
        public async Task<File> UpdateInformationAsync() { return new File(); }
        public async Task<File> DeleteAsync() { return new File(); }
        public async Task<File> CopyAsync() { return new File(); }
        public async Task<File> CreateSharedLinkAsync() { return new File(); }
        public async Task<File> GetCommentsAsync() { return new File(); }
        public async Task<File> GetThumbnailAsync() { return new File(); }
        public async Task<File> GetTrashedAsync() { return new File(); }
        public async Task<File> RestoreTrashedAsync() { return new File(); }
        public async Task<File> PurgeTrashedAsync() { return new File(); }
    }
}
