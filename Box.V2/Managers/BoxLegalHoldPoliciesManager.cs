using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;

namespace Box.V2.Managers
{
    /// <summary>
    /// Allow create, update, get, delete legal hold and legal hold assignment.
    /// </summary>
    public class BoxLegalHoldPoliciesManager : BoxResourceManager, IBoxLegalHoldPoliciesManager
    {
        private const string ParamPolicyId = "policy_id";

        /// <summary>
        /// Create a new BoxLegalHoldPoliciesManager object.
        /// </summary>
        public BoxLegalHoldPoliciesManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Get details of a single Legal Hold Policy.
        /// </summary>
        /// <param name="legalHoldId">Id of the legal hold policy.</param>
        /// <returns>If the Id is valid, information about the Legal Hold Policy is returned.</returns>
        public async Task<BoxLegalHoldPolicy> GetLegalHoldPolicyAsync(string legalHoldId)
        {
            legalHoldId.ThrowIfNullOrWhiteSpace("legalHoldId");

            BoxRequest request = new BoxRequest(_config.LegalHoldPoliciesEndpointUri, legalHoldId)
                .Method(RequestMethod.Get);

            IBoxResponse<BoxLegalHoldPolicy> response = await ToResponseAsync<BoxLegalHoldPolicy>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Get a list of Legal Hold Policies that belong to your Enterprise.
        /// </summary>
        /// <param name="policyName">Case insensitive prefix-match filter on Policy name.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="marker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns the list of Legal Hold Policies in your Enterprise that match the filter parameters (if passed in). By default, will only return only 'type', 'id', and 'policy_name', but you can specify more by using the 'fields' parameter.</returns>
        public async Task<BoxCollectionMarkerBased<BoxLegalHoldPolicy>> GetListLegalHoldPoliciesAsync(string policyName = null, string fields = null, int limit = 100, string marker = null, bool autoPaginate = false)
        {
            BoxRequest request = new BoxRequest(_config.LegalHoldPoliciesEndpointUri)
                .Method(RequestMethod.Get)
                .Param("policy_name", policyName)
                .Param("fields", fields)
                .Param("limit", limit.ToString())
                .Param("marker", marker);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxLegalHoldPolicy>(request, limit).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBased<BoxLegalHoldPolicy>> response = await ToResponseAsync<BoxCollectionMarkerBased<BoxLegalHoldPolicy>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Create a new Legal Hold Policy. Optional date filter may be passed. 
        /// If Policy has a date filter, any Custodian assignments will apply only to file versions created or uploaded inside of the date range. 
        /// (Other assignment types, such as folders and files, will ignore the date filter).
        /// </summary>
        /// <param name="createRequest">BoxLegalHoldPolicyRequest object.</param>
        /// <returns>For a successful request, returns information about the Legal Hold Policy created. 
        /// If the Policy Name is in use for your enterprise, will return null.
        /// </returns>
        public async Task<BoxLegalHoldPolicy> CreateLegalHoldPolicyAsync(BoxLegalHoldPolicyRequest createRequest)
        {
            createRequest.ThrowIfNull("createRequest")
                .PolicyName.ThrowIfNullOrWhiteSpace("createRequest.PolicyName");

            BoxRequest request = new BoxRequest(_config.LegalHoldPoliciesEndpointUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(createRequest));

            IBoxResponse<BoxLegalHoldPolicy> response = await ToResponseAsync<BoxLegalHoldPolicy>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Update existing Legal Hold Policy. Only name and description can be modified.
        /// </summary>
        /// <param name="legalHoldPolicyId">Id of the legal hold policy.</param>
        /// <param name="updateRequest">BoxLegalHoldPolicyRequest object.</param>
        /// <returns>Returns information about the Legal Hold Policy updated.</returns>
        public async Task<BoxLegalHoldPolicy> UpdateLegalHoldPolicyAsync(string legalHoldPolicyId, BoxLegalHoldPolicyRequest updateRequest)
        {
            legalHoldPolicyId.ThrowIfNull("legalHoldPolicyId");
            updateRequest.ThrowIfNull("updateRequest");

            BoxRequest request = new BoxRequest(_config.LegalHoldPoliciesEndpointUri, legalHoldPolicyId)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize(updateRequest));

            IBoxResponse<BoxLegalHoldPolicy> response = await ToResponseAsync<BoxLegalHoldPolicy>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Sends request to delete an existing Legal Hold Policy. Note that this is an asynchronous process - the Policy will not be fully deleted yet when the response comes back.
        /// </summary>
        /// <param name="legalHoldPolicyId">Id of the legal hold policy.</param>
        /// <returns>Returns True if the request to delete the Policy was accepted.</returns>
        public async Task<bool> DeleteLegalHoldPolicyAsync(string legalHoldPolicyId)
        {
            legalHoldPolicyId.ThrowIfNull("legalHoldPolicyId");

            BoxRequest request = new BoxRequest(_config.LegalHoldPoliciesEndpointUri, legalHoldPolicyId)
               .Method(RequestMethod.Delete);

            IBoxResponse<BoxLegalHoldPolicy> response = await ToResponseAsync<BoxLegalHoldPolicy>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Pending;
        }

        /// <summary>
        /// Get details of a single assignment.
        /// </summary>
        /// <param name="assignmentId">Id of the assignment.</param>
        /// <returns>If the assignmentId is valid, information about the Assignment is returned </returns>
        public async Task<BoxLegalHoldPolicyAssignment> GetAssignmentAsync(string assignmentId)
        {
            assignmentId.ThrowIfNullOrWhiteSpace("assignmentId");

            BoxRequest request = new BoxRequest(_config.LegalHoldPolicyAssignmentsEndpointUri, assignmentId)
                .Method(RequestMethod.Get);

            IBoxResponse<BoxLegalHoldPolicyAssignment> response = await ToResponseAsync<BoxLegalHoldPolicyAssignment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Get assignments for a single policy.
        /// </summary>
        /// <param name="legalHoldPolicyId">ID of Policy to get Assignments for.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="assignToType">Filter assignments of this type only. Can be file_version, file, folder, or user.</param>
        /// <param name="assignToId">Filter assignments to this ID only. Note that this will only show assignments applied directly to this entity.</param>
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="marker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns the list of Assignments for the passed in Policy, as well as any optional filter parameters passed in. By default, will only return only type, and id, but you can specify more by using the fields parameter.</returns>
        public async Task<BoxCollectionMarkerBased<BoxLegalHoldPolicyAssignment>> GetAssignmentsAsync(string legalHoldPolicyId, string fields = null, string assignToType = null, string assignToId = null, int limit = 100, string marker = null, bool autoPaginate = false)
        {
            legalHoldPolicyId.ThrowIfNullOrWhiteSpace("legalHoldPolicyId");

            BoxRequest request = new BoxRequest(_config.LegalHoldPoliciesEndpointUri, string.Format(Constants.LegalHoldPolicyAssignmentsPathString, legalHoldPolicyId))
                .Method(RequestMethod.Get)
                .Param(ParamFields, fields)
                .Param("assign_to_type", assignToType)
                .Param("assign_to_id", assignToId)
                .Param("limit", limit.ToString())
                .Param("marker", marker);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxLegalHoldPolicyAssignment>(request, limit).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBased<BoxLegalHoldPolicyAssignment>> response = await ToResponseAsync<BoxCollectionMarkerBased<BoxLegalHoldPolicyAssignment>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Create a new Assignment, which will apply the Legal Hold Policy to the target of the Assignment.
        /// </summary>
        /// <param name="createRequest">BoxLegalHoldPolicyAssignmentRequest object.</param>
        /// <returns>For a successful request, returns object with information about the Assignment created. 
        /// If the policy or assign-to target cannot be found, null will be returned.
        /// </returns>
        public async Task<BoxLegalHoldPolicyAssignment> CreateAssignmentAsync(BoxLegalHoldPolicyAssignmentRequest createRequest)
        {
            createRequest.ThrowIfNull("createRequest")
                .PolicyId.ThrowIfNullOrWhiteSpace("createRequest.PolicyId");
            createRequest.AssignTo.ThrowIfNull("createRequest.AssignTo")
                .Id.ThrowIfNullOrWhiteSpace("createRequest.AssignTo.Id");
            createRequest.AssignTo.Type.ThrowIfNull("createRequest.AssignTo.Type");

            BoxRequest request = new BoxRequest(_config.LegalHoldPolicyAssignmentsEndpointUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(createRequest));

            IBoxResponse<BoxLegalHoldPolicyAssignment> response = await ToResponseAsync<BoxLegalHoldPolicyAssignment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Sends request to delete an existing Assignment.
        /// Note that this is an asynchronous process - the Assignment will not be fully deleted yet when the response comes back.
        /// </summary>
        /// <param name="assignmentId">ID of the legal holds assignment.</param>
        /// <returns>A successful request returns 204 No Content. If the Assignment is still initializing, will return a 409.</returns>
        public async Task<bool> DeleteAssignmentAsync(string assignmentId)
        {
            BoxRequest request = new BoxRequest(_config.LegalHoldPolicyAssignmentsEndpointUri, assignmentId)
                .Method(RequestMethod.Delete);

            var response = await ToResponseAsync<BoxLegalHoldPolicyAssignment>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Get details of a single File Version Legal Hold.
        /// </summary>
        /// <param name="fileVersionLegalHoldId">ID of the file version legal hold.</param>
        /// <returns>If the ID is valid, information about the Hold is returned with a 200.
        /// If the ID is for a non-existent Hold, a 404 is returned.</returns>
        public async Task<BoxFileVersionLegalHold> GetFileVersionLegalHoldAsync(string fileVersionLegalHoldId)
        {
            var request = new BoxRequest(_config.FileVersionLegalHoldsEndpointUri, fileVersionLegalHoldId);

            var response = await ToResponseAsync<BoxFileVersionLegalHold>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Get list of non-deleted Holds for a single Policy.
        /// </summary>
        /// <param name="policyId">ID of Legal Hold Policy to get File Version Legal Holds for.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="marker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns the list of File Version Legal Holds for the passed in Policy. 
        /// By default, will only return only "type", and "id", but you can specify more by using the "fields" parameter.</returns>
        public async Task<BoxCollectionMarkerBased<BoxFileVersionLegalHold>> GetFileVersionLegalHoldsAsync(string policyId, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false)
        {
            BoxRequest request = new BoxRequest(_config.FileVersionLegalHoldsEndpointUri)
                .Param(ParamPolicyId, policyId)
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("marker", marker);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxFileVersionLegalHold>(request, limit).ConfigureAwait(false);
            }
            else
            {
                var response = await ToResponseAsync<BoxCollectionMarkerBased<BoxFileVersionLegalHold>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }
    }
}
