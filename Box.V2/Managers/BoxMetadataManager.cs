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
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    public class BoxMetadataManager : BoxResourceManager
    {
        public BoxMetadataManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null)
            : base(config, service, converter, auth, asUser) { }

        public async Task<Dictionary<string, object>> GetFileMetadataAsync(string fileId, string scope, string template)
        {
            return await GetMetadata(_config.FilesEndpointUri, fileId, scope, template);
        }

        public async Task<Dictionary<string, object>> GetFolderMetadataAsync(string folderId, string scope, string template)
        {
            return await GetMetadata(_config.FoldersEndpointUri, folderId, scope, template);
        }

        public async Task<Dictionary<string, object>> CreateFileMetadataAsync(string fileId, Dictionary<string, object> metadata, string scope, string template)
        {
            return await CreateMetadata(_config.FilesEndpointUri, fileId, metadata, scope, template);
        }

        public async Task<Dictionary<string,object>> CreateFolderMetadataAsync(string folderId, Dictionary<string,object> metadata, string scope, string template)
        {
            return await CreateMetadata(_config.FoldersEndpointUri, folderId, metadata, scope, template);
        }

        public async Task<bool> DeleteFileMetadataAsync(string fileId, string scope, string template)
        {
            return await DeleteMetadata(_config.FilesEndpointUri, fileId, scope, template);
        }

        public async Task<bool> DeleteFolderMetadataAsync(string folderId, string scope, string template)
        {
            return await DeleteMetadata(_config.FoldersEndpointUri, folderId, scope, template);
        }



        private async Task<Dictionary<string, object>> CreateMetadata(Uri hostUri, string id, Dictionary<string, object> metadata, string scope, string template)
        {
            BoxRequest request = new BoxRequest(hostUri, string.Format(Constants.MetadataPathString, id, scope, template))
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(metadata));

            request.ContentType = Constants.RequestParameters.ContentTypeJson;
            IBoxResponse<Dictionary<string, object>> response = await ToResponseAsync<Dictionary<string, object>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        private async Task<Dictionary<string, object>> GetMetadata(Uri hostUri, string id, string scope, string template)
        {
            BoxRequest request = new BoxRequest(hostUri, string.Format(Constants.MetadataPathString, id, scope, template));
            IBoxResponse<Dictionary<string, object>> response = await ToResponseAsync<Dictionary<string, object>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        private async Task<bool> DeleteMetadata(Uri hostUri, string id, string scope, string template)
        {
            BoxRequest request = new BoxRequest(hostUri, string.Format(Constants.MetadataPathString, id, scope, template))
                .Method(RequestMethod.Delete);

            IBoxResponse<Dictionary<string, object>> response = await ToResponseAsync<Dictionary<string, object>>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }
    }
}
