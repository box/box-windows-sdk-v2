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

        public async Task<Dictionary<string,object>> CreateFolderMetadataAsync(string folderId, Dictionary<string,object> metadata, string scope, string template)
        {
            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, string.Format(Constants.MetadataPathString, folderId, scope, template))
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(metadata));

            request.ContentType = Constants.RequestParameters.ContentTypeJson;

            IBoxResponse<Dictionary<string, object>> response = await ToResponseAsync<Dictionary<string, object>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }
    }
}
