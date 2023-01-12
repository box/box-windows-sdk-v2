using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Services;
using Box.V2.Utility;

namespace Box.V2.Managers
{
    /// <summary>
    /// The class managing the Box API's Retention Policies endpoint.
    /// Retention Management is a feature of the Box Governance package, which can be added on to any Business Plus or Enterprise account.
    /// To use this feature, you must have the manage retention policies scope enabled for your API key via your application management console.
    /// </summary>
    public class BoxRetentionPoliciesManager : BoxResourceManager, IBoxRetentionPoliciesManager
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
        public async Task<BoxRetentionPolicy> GetRetentionPolicyAsync(string id, IEnumerable<string> fields = null)
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
        public async Task<BoxRetentionPolicy> UpdateRetentionPolicyAsync(string id, BoxRetentionPolicyRequest retentionPolicyRequest, IEnumerable<string> fields = null)
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
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="marker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns the list of all retention policies for the enterprise.</returns>
        public async Task<BoxCollectionMarkerBased<BoxRetentionPolicy>> GetRetentionPoliciesAsync(string policyName = null, string policyType = null, string createdByUserId = null, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPoliciesEndpointUri)
                .Param("policy_name", policyName)
                .Param("policy_type", policyType)
                .Param("created_by_user_id", createdByUserId)
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("marker", marker);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxRetentionPolicy>(request, limit).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBased<BoxRetentionPolicy>> response = await ToResponseAsync<BoxCollectionMarkerBased<BoxRetentionPolicy>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Returns a list of all retention policy assignments associated with a specified retention policy.
        /// </summary>
        /// <param name="retentionPolicyId">ID of the retention policy.</param>
        /// <param name="type">The type of the retention policy assignment to retrieve. Can either be folder or enterprise.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="marker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns a list of the retention policy assignments associated with the specified retention policy.</returns>
        public async Task<BoxCollectionMarkerBased<BoxRetentionPolicyAssignment>> GetRetentionPolicyAssignmentsAsync(string retentionPolicyId, string type = null, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPoliciesEndpointUri, string.Format(Constants.RetentionPolicyAssignmentsEndpointString, retentionPolicyId))
                .Param("type", type)
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("marker", marker);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxRetentionPolicyAssignment>(request, limit).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBased<BoxRetentionPolicyAssignment>> response = await ToResponseAsync<BoxCollectionMarkerBased<BoxRetentionPolicyAssignment>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Creates a retention policy assignment that associates a retention policy with either a folder or an enterprise
        /// </summary>
        /// <param name="policyAssignmentRequest"></param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A new retention policy assignment will be returned upon success.</returns>
        public async Task<BoxRetentionPolicyAssignment> CreateRetentionPolicyAssignmentAsync(BoxRetentionPolicyAssignmentRequest policyAssignmentRequest, IEnumerable<string> fields = null)
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
        public async Task<BoxRetentionPolicyAssignment> GetRetentionPolicyAssignmentAsync(string retentionPolicyAssignmentId, IEnumerable<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPolicyAssignmentsUri, retentionPolicyAssignmentId)
                .Param(ParamFields, fields);

            IBoxResponse<BoxRetentionPolicyAssignment> response = await ToResponseAsync<BoxRetentionPolicyAssignment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to delete a retention policy assignment.
        /// </summary>
        /// <param name="retentionPolicyAssignmentId">ID of the retention policy assignment.</param>
        /// <returns>True if the retention policy assignment was successfully deleted.</returns>
        public async Task<bool> DeleteRetentionPolicyAssignmentAsync(string retentionPolicyAssignmentId)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPolicyAssignmentsUri, retentionPolicyAssignmentId)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxRetentionPolicyAssignment> response = await ToResponseAsync<BoxRetentionPolicyAssignment>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Retrieves all file version retentions for the given enterprise.
        /// </summary>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="marker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <param name="fileId">Filters results by files with this ID.</param>
        /// <param name="fileVersionId">Filters results by file versions with this ID.</param>
        /// <param name="policyId">Filters results by the retention policy with this ID.</param>
        /// <param name="dispositionBefore">Filters results by files that will have their disposition come into effect before this date.</param>
        /// <param name="dispositionAfter">Filters results by files that will have their disposition come into effect after this date.</param>
        /// <param name="dispositionAction">Filters results by the retention policy with this disposition action.</param>
        /// <returns>The specified file version retention will be returned upon success.</returns>
        [Obsolete("This method will be deprecated in the future. Please use GetFilesUnderRetentionForAssignmentAsync() and GetFileVersionsUnderRetentionForAssignmentAsync() instead.")]
        public async Task<BoxCollectionMarkerBased<BoxFileVersionRetention>> GetFileVersionRetentionsAsync(IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false, string fileId = null, string fileVersionId = null, string policyId = null, DateTimeOffset? dispositionBefore = null, DateTimeOffset? dispositionAfter = null, DispositionAction? dispositionAction = null)
        {
            BoxRequest request = new BoxRequest(_config.FileVersionRetentionsUri)
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("marker", marker)
                .Param("file_id", fileId)
                .Param("file_version_id", fileVersionId)
                .Param("policy_id", policyId)
                .Param("disposition_before", Helper.ConvertToRFCString(dispositionBefore))
                .Param("disposition_after", Helper.ConvertToRFCString(dispositionAfter))
                .Param("disposition_action", dispositionAction.ToString());

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxFileVersionRetention>(request, limit).ConfigureAwait(false);
            }
            else
            {
                var response = await ToResponseAsync<BoxCollectionMarkerBased<BoxFileVersionRetention>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Used to retrieve information about a file version retention.
        /// </summary>
        /// <param name="fileVersionRetentionId">ID of the file version retention policy.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Returns the list of all file version retentions for the enterprise.</returns>
        public async Task<BoxFileVersionRetention> GetFileVersionRetentionAsync(string fileVersionRetentionId, IEnumerable<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.FileVersionRetentionsUri, fileVersionRetentionId)
                .Param(ParamFields, fields);

            var response = await ToResponseAsync<BoxFileVersionRetention>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to retrieve files under retention by each assignment
        /// To use this feature, you must have the manage retention policies scope enabled
        /// for your API key via your application management console.
        /// </summary>
        /// <param name="retentionPolicyAssignmentId">The Box ID of the policy assignment object to fetch
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="marker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns the list of all files under retentions for the assignment.</returns>
        public async Task<BoxCollectionMarkerBased<BoxFile>> GetFilesUnderRetentionForAssignmentAsync(string retentionPolicyAssignmentId, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPolicyAssignmentsUri, string.Format(Constants.FilesUnderRetentionEndpointString, retentionPolicyAssignmentId))
                .Param("retention_policy_assignment_id", retentionPolicyAssignmentId)
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("marker", marker);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxFile>(request, limit).ConfigureAwait(false);
            }
            else
            {
                var response = await ToResponseAsync<BoxCollectionMarkerBased<BoxFile>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Used to retrieve file versions under retention by each assignment
        /// To use this feature, you must have the manage retention policies scope enabled
        /// for your API key via your application management console.
        /// </summary>
        /// <param name="retentionPolicyAssignmentId">The Box ID of the policy assignment object to fetch
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="marker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns the list of all file versions under retentions for the assignment.</returns>
        public async Task<BoxCollectionMarkerBased<BoxFile>> GetFileVersionsUnderRetentionForAssignmentAsync(string retentionPolicyAssignmentId, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false)
        {
            BoxRequest request = new BoxRequest(_config.RetentionPolicyAssignmentsUri, string.Format(Constants.FileVersionsUnderRetentionEndpointString, retentionPolicyAssignmentId))
                .Param("retention_policy_assignment_id", retentionPolicyAssignmentId)
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("marker", marker);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxFile>(request, limit).ConfigureAwait(false);
            }
            else
            {
                var response = await ToResponseAsync<BoxCollectionMarkerBased<BoxFile>>(request, false,
                    new BoxFileVersionsUnderRetentionJsonConverter()).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }
    }
}
