using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Extensions;
using Box.V2.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Box.V2.Managers
{
    public class BoxRetentionPoliciesManager : BoxResourceManager
    {
        public BoxRetentionPoliciesManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth) { }

        /// <summary>
        /// Used to create a new retention policy
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

        /// <summary>
        /// Used to retrieve information about a retention policy
        /// </summary>
        /// <param name="id">ID of the retention policy</param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public async Task<BoxRetentionPolicy> GetRetentionPolicyAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.RetentionPoliciesEndpointUri, id)
                .Param(ParamFields, fields);

            IBoxResponse<BoxRetentionPolicy> response = await ToResponseAsync<BoxRetentionPolicy>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to update a retention policy.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="retentionPolicyRequest"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public async Task<BoxRetentionPolicy> UpdateRetentionPolicyAsync(string id, BoxRetentionPolicyRequest retentionPolicyRequest, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.RetentionPoliciesEndpointUri, id)
                .Method(RequestMethod.Put)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize<BoxRetentionPolicyRequest>(retentionPolicyRequest));

            IBoxResponse<BoxRetentionPolicy> response = await ToResponseAsync<BoxRetentionPolicy>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves all of the retention policies for the given enterprise.
        /// </summary>
        /// <param name="policyName">A name to filter the retention policies by. A trailing partial match search is performed.</param>
        /// <param name="policyType">A policy type to filter the retention policies by.</param>
        /// <param name="createdByUserId">A user id to filter the retention policies by.</param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public async Task<BoxCollection<BoxRetentionPolicy>> GetRetentionPolicies(string policyName = null, string policyType = null, string createdByUserId = null, List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPoliciesEndpointUri)
                .Param("policy_name", policyName)
                .Param("policy_type", policyType)
                .Param("created_by_user_id", createdByUserId)
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxRetentionPolicy>> response = await ToResponseAsync<BoxCollection<BoxRetentionPolicy>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }


        public async Task<BoxCollection<BoxRetentionPolicyAssignment>> GetRetentionPolicyAssignments(string retentionPolicyId, string type = null, string createdByUserId = null, List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPoliciesEndpointUri, string.Format(Constants.RetentionPolicyAssignmentsString, retentionPolicyId))
                .Param("type", type)
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxRetentionPolicyAssignment>> response = await ToResponseAsync<BoxCollection<BoxRetentionPolicyAssignment>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

    }
}
