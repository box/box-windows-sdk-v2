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
    /// <summary>
    /// The class managing the Box API's Retention Policies endpoint.
    /// Retention Management is a feature of the Box Governance package, which can be added on to any Business Plus or Enterprise account.
    /// To use this feature, you must have the manage retention policies scope enabled for your API key via your application management console.
    /// </summary>
    public class BoxRetentionPoliciesManager : BoxResourceManager
    {
        public BoxRetentionPoliciesManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Used to create a new retention policy.
        /// </summary>
        /// <param name="retentionPolicyRequest">BoxRetentionPolicyRequest object.</param>
        /// <returns>A new retention policy object will be returned upon success.</returns>
        public async Task<BoxRetentionPolicy> CreateRetentionPolicyAsync(BoxRetentionPolicyRequest retentionPolicyRequest)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPoliciesEndpointUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(retentionPolicyRequest));

            IBoxResponse<BoxRetentionPolicy> response = await ToResponseAsync<BoxRetentionPolicy>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to retrieve information about a retention policy.
        /// </summary>
        /// <param name="id">ID of the retention policy.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The specified retention policy will be returned upon success.</returns>
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
        /// <param name="id">ID of the retention policy.</param>
        /// <param name="retentionPolicyRequest">BoxRetentionPolicyRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>An updated retention policy object will be returned upon success.</returns>
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
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Returns the list of all retention policies for the enterprise.</returns>
        public async Task<BoxCollection<BoxRetentionPolicy>> GetRetentionPoliciesAsync(string policyName = null, string policyType = null, string createdByUserId = null, List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPoliciesEndpointUri)
                .Param("policy_name", policyName)
                .Param("policy_type", policyType)
                .Param("created_by_user_id", createdByUserId)
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxRetentionPolicy>> response = await ToResponseAsync<BoxCollection<BoxRetentionPolicy>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Returns a list of all retention policy assignments associated with a specified retention policy.
        /// </summary>
        /// <param name="retentionPolicyId">ID of the retention policy.</param>
        /// <param name="type">The type of the retention policy assignment to retrieve. Can either be folder or enterprise.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Returns a list of the retention policy assignments associated with the specified retention policy.</returns>
        public async Task<BoxCollection<BoxRetentionPolicyAssignment>> GetRetentionPolicyAssignmentsAsync(string retentionPolicyId, string type = null, List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPoliciesEndpointUri, string.Format(Constants.RetentionPolicyAssignmentsEndpointString, retentionPolicyId))
                .Param("type", type)
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxRetentionPolicyAssignment>> response = await ToResponseAsync<BoxCollection<BoxRetentionPolicyAssignment>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Creates a retention policy assignment that associates a retention policy with either a folder or an enterprise
        /// </summary>
        /// <param name="policyAssignmentRequest"></param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A new retention policy assignment will be returned upon success.</returns>
        public async Task<BoxRetentionPolicyAssignment> CreateRetentionPolicyAssignmentAsync(BoxRetentionPolicyAssignmentRequest policyAssignmentRequest, List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPolicyAssignmentsUri)
                .Method(RequestMethod.Post)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize<BoxRetentionPolicyAssignmentRequest>(policyAssignmentRequest));

            IBoxResponse<BoxRetentionPolicyAssignment> response = await ToResponseAsync<BoxRetentionPolicyAssignment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to retrieve information about a retention policy assignment.
        /// </summary>
        /// <param name="retentionPolicyAssignmentId">ID of the retention policy assignment.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The specified retention policy assignment will be returned upon success.</returns>
        public async Task<BoxRetentionPolicyAssignment> GetRetentionPolicyAssignmentAsync(string retentionPolicyAssignmentId, List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPolicyAssignmentsUri, retentionPolicyAssignmentId)
                .Param(ParamFields, fields);

            IBoxResponse<BoxRetentionPolicyAssignment> response = await ToResponseAsync<BoxRetentionPolicyAssignment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves all file version retentions for the given enterprise.
        /// </summary>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The specified file version retention will be returned upon success.</returns>
        public async Task<BoxCollection<BoxFileVersionRetention>> GetFileVersionRetentionsAsync(List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.FileVersionRetentionsUri)
                .Param(ParamFields, fields);

            var response = await ToResponseAsync<BoxCollection<BoxFileVersionRetention>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to retrieve information about a file version retention.
        /// </summary>
        /// <param name="fileVersionRetentionId">ID of the file version retention policy.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Returns the list of all file version retentions for the enterprise.</returns>
        public async Task<BoxFileVersionRetention> GetFileVersionRetentionAsync(string fileVersionRetentionId, List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.FileVersionRetentionsUri, fileVersionRetentionId)
                .Param(ParamFields, fields);

            var response = await ToResponseAsync<BoxFileVersionRetention>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }
    }
}
