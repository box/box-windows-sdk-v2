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
        public BoxFilesManager(IBoxConfig config, IBoxService service, IAuthRepository auth)
            : base(config, service, auth)
        {

        }

        /// <summary>
        /// Gets the folder along with its contents
        /// </summary>
        /// <param name="id"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<File> GetInformationAsync(string id)
        {
            BoxRequest request = new BoxRequest(_config.BoxApiUri, string.Format(@"/folders/{0}/items", id));
            AddAuthentication(request);

            IBoxResponse<File> response = await _service.ToResponseAsync<File>(request);

            return response.ResponseObject;
        }


        public async Task<byte[]> DownloadAsync(string id)
        {
            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.ContentPathString, id));
            AddAuthentication(request);

            IBoxResponse<byte[]> response = await _service.ToResponseAsync<byte[]>(request);
            IBoxResponse<MemoryStream> r2 = await _service.ToResponseAsync<MemoryStream>(request);

            return response.ResponseObject;
        }

        public async Task<File> UploadAsync() { return new File(); }
        public async Task<File> UploadNewVersionAsync() { return new File(); }
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
