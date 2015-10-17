using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Extensions;
using Box.V2.Services;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    public class BoxRetentionPoliciesManager : BoxResourceManager
    {
        public BoxRetentionPoliciesManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth) { }

        /// <summary>
        /// Used to create a new retention policy.
        /// </summary>
        /// <param name="retentionPolicyRequest"></param>
        /// <returns></returns>
        public async Task<BoxRetentionPolicy> CreateRetentionPolicyAsync(BoxRetentionPolicyRequest retentionPolicyRequest)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPoliciesEndpointUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(retentionPolicyRequest));

            IBoxResponse<BoxRetentionPolicy> response = await ToResponseAsync<BoxRetentionPolicy>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

    }
}
