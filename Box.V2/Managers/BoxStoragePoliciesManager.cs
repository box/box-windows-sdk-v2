using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using Newtonsoft.Json.Linq;

namespace Box.V2.Managers
{
    public class BoxStoragePoliciesManager : BoxResourceManager, IBoxStoragePoliciesManager
    {
        /// <summary>
        /// Create a new BoxStoragePolicies object.
        /// </summary>
        public BoxStoragePoliciesManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Get details of a single Box Storage Policy.
        /// </summary>
        /// <param name="policyId">Id of the Box Storage Policy to retrieve.</param>
        /// <returns>If the Id is valid, information about the Box Storage Policy is returned. </returns>
        public async Task<BoxStoragePolicy> GetStoragePolicyAsync(string policyId)
        {
            policyId.ThrowIfNullOrWhiteSpace("policyId");

            BoxRequest request = new BoxRequest(_config.StoragePoliciesUri, policyId)
                .Method(RequestMethod.Get);

            IBoxResponse<BoxStoragePolicy> response = await ToResponseAsync<BoxStoragePolicy>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Get a list of Storage Policies that belong to your Enterprise.
        /// </summary>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="marker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="limit">Limit result size to this number. Defults to 100, maximum is 1,000.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns the list of Storage Policies in your Enterprise that match the filer parameters (if passedin).</returns>
        public async Task<BoxCollectionMarkerBased<BoxStoragePolicy>> GetListStoragePoliciesAsync(string fields = null, string marker = null, int limit = 100, bool autoPaginate = false)
        {
            BoxRequest request = new BoxRequest(_config.StoragePoliciesUri)
                .Method(RequestMethod.Get)
                .Param("fields", fields)
                .Param("limit", limit.ToString())
                .Param("marker", marker);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxStoragePolicy>(request, limit).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBased<BoxStoragePolicy>> response = await ToResponseAsync<BoxCollectionMarkerBased<BoxStoragePolicy>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Get details of a single assignment.
        /// </summary>
        /// <param name="assignmentId">Id of the assignment.</param>
        /// <returns>If the assignmentId is valid, information about the assignment is returned.</returns>
        public async Task<BoxStoragePolicyAssignment> GetAssignmentAsync(string assignmentId)
        {
            assignmentId.ThrowIfNullOrWhiteSpace("assignmentId");

            BoxRequest request = new BoxRequest(_config.StoragePolicyAssignmentsUri, assignmentId)
                .Method(RequestMethod.Get);

            IBoxResponse<BoxStoragePolicyAssignment> response = await ToResponseAsync<BoxStoragePolicyAssignment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Get details of a Storage Policy Assignment for target entity.
        /// </summary>
        /// <param name="userId">User Id of the assignment.</param>
        /// <param name="entityType">Entity type of the storage policy assignment.</param>
        /// <returns></returns>
        public async Task<BoxStoragePolicyAssignment> GetAssignmentForTargetAsync(string entityId, string entityType = "user")
        {
            entityId.ThrowIfNullOrWhiteSpace("entityId");

            BoxRequest request = new BoxRequest(_config.StoragePolicyAssignmentsForTargetUri)
                .Method(RequestMethod.Get)
                .Param("resolved_for_type", entityType)
                .Param("resolved_for_id", entityId);

            IBoxResponse<BoxCollectionMarkerBased<BoxStoragePolicyAssignment>> response = await ToResponseAsync<BoxCollectionMarkerBased<BoxStoragePolicyAssignment>>(request).ConfigureAwait(false);
            return response.ResponseObject.Entries[0];
        }

        /// <summary>
        /// Update the storage policy information for storage policy assignment.
        /// </summary>
        /// <param name="assignmentId">Storage Policy assignment Id to update.</param>
        /// <param name="policyId">"The Id of the Storage Policy to update to."</param>
        /// <returns></returns> The updated Storage Policy object with new assignment.
        public async Task<BoxStoragePolicyAssignment> UpdateStoragePolicyAssignment(string assignmentId, string policyId)
        {
            policyId.ThrowIfNullOrWhiteSpace("policyId");

            dynamic req = new JObject();
            dynamic storagePolicyObject = new JObject();

            storagePolicyObject.type = "storage_policy";
            storagePolicyObject.id = policyId;
            req.storage_policy = storagePolicyObject;

            string jsonStr = req.ToString();

            BoxRequest request = new BoxRequest(_config.StoragePolicyAssignmentsUri, assignmentId)
                .Method(RequestMethod.Put)
                .Payload(jsonStr);

            IBoxResponse<BoxStoragePolicyAssignment> response = await ToResponseAsync<BoxStoragePolicyAssignment>(request).ConfigureAwait(false);
            return response.ResponseObject;
        }

        /// <summary>
        /// Create a storage policy assignment to a Box User.
        /// </summary>
        /// <param name="userId">The user Id to create assignment for.</param>
        /// <param name="policyId">The Id of the storage policy to assign the user to.</param>
        /// <returns>The assignment object for the storage policy assignment to user.</returns>
        public async Task<BoxStoragePolicyAssignment> CreateAssignmentAsync(string userId, string policyId)
        {
            userId.ThrowIfNullOrWhiteSpace("userId");
            policyId.ThrowIfNullOrWhiteSpace("policyId");

            dynamic req = new JObject();
            dynamic storagePolicyObject = new JObject();
            dynamic userObject = new JObject();

            storagePolicyObject.type = "storage_policy";
            storagePolicyObject.id = policyId;

            userObject.type = "user";
            userObject.id = userId;

            req.storage_policy = storagePolicyObject;
            req.assigned_to = userObject;

            string jsonStr = req.ToString();

            BoxRequest request = new BoxRequest(_config.StoragePolicyAssignmentsForTargetUri)
                .Method(RequestMethod.Post)
                .Payload(jsonStr);

            IBoxResponse<BoxStoragePolicyAssignment> response = await ToResponseAsync<BoxStoragePolicyAssignment>(request).ConfigureAwait(false);
            return response.ResponseObject;
        }

        /// <summary>
        /// Sends request to delete an existing assignment.
        /// </summary>
        /// <param name="assignmentId">Id of the storage policy assignment.</param>
        /// <returns>A successful request returns 204 No Content.</returns>
        public async Task<bool> DeleteAssignmentAsync(string assignmentId)
        {
            BoxRequest request = new BoxRequest(_config.StoragePolicyAssignmentsUri, assignmentId)
                .Method(RequestMethod.Delete);

            var response = await ToResponseAsync<BoxStoragePolicyAssignment>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Checks if a storage policy assignment exists. If it does not then create an assignment. 
        /// </summary>
        /// <param name="userId">The id of the user to assign storage policy to.</param>
        /// <param name="storagePolicyId">The storage policy id to assign to user.</param>
        /// <returns></returns>
        public async Task<BoxStoragePolicyAssignment> AssignAsync(string userId, string storagePolicyId)
        {
            userId.ThrowIfNullOrWhiteSpace("userId");
            storagePolicyId.ThrowIfNullOrWhiteSpace("storagePolicyId");

            var result = await GetAssignmentForTargetAsync(userId).ConfigureAwait(false);

            if (result.BoxStoragePolicy.Id.Equals(storagePolicyId))
            {
                return result;
            }

            return result.AssignedTo.Type.Equals("enterprise")
                ? await CreateAssignmentAsync(userId, storagePolicyId).ConfigureAwait(false)
                : await UpdateStoragePolicyAssignment(result.Id, storagePolicyId).ConfigureAwait(false);
        }
    }
}
