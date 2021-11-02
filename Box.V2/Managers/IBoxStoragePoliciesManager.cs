using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    public interface IBoxStoragePoliciesManager
    {
        /// <summary>
        /// Get details of a single Box Storage Policy.
        /// </summary>
        /// <param name="policyId">Id of the Box Storage Policy to retrieve.</param>
        /// <returns>If the Id is valid, information about the Box Storage Policy is returned. </returns>
        Task<BoxStoragePolicy> GetStoragePolicyAsync(string policyId);

        /// <summary>
        /// Get a list of Storage Policies that belong to your Enterprise.
        /// </summary>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="marker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="limit">Limit result size to this number. Defults to 100, maximum is 1,000.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns the list of Storage Policies in your Enterprise that match the filer parameters (if passedin).</returns>
        Task<BoxCollectionMarkerBased<BoxStoragePolicy>> GetListStoragePoliciesAsync(string fields = null, string marker = null, int limit = 100, bool autoPaginate = false);

        /// <summary>
        /// Get details of a single assignment.
        /// </summary>
        /// <param name="assignmentId">Id of the assignment.</param>
        /// <returns>If the assignmentId is valid, information about the assignment is returned.</returns>
        Task<BoxStoragePolicyAssignment> GetAssignmentAsync(string assignmentId);

        /// <summary>
        /// Get details of a Storage Policy Assignment for target entity.
        /// </summary>
        /// <param name="userId">User Id of the assignment.</param>
        /// <param name="entityType">Entity type of the storage policy assignment.</param>
        /// <returns></returns>
        Task<BoxStoragePolicyAssignment> GetAssignmentForTargetAsync(string entityId, string entityType = "user");

        /// <summary>
        /// Update the storage policy information for storage policy assignment.
        /// </summary>
        /// <param name="assignmentId">Storage Policy assignment Id to update.</param>
        /// <param name="policyId">"The Id of the Storage Policy to update to."</param>
        /// <returns></returns> The updated Storage Policy object with new assignment.
        Task<BoxStoragePolicyAssignment> UpdateStoragePolicyAssignment(string assignmentId, string policyId);

        /// <summary>
        /// Create a storage policy assignment to a Box User.
        /// </summary>
        /// <param name="userId">The user Id to create assignment for.</param>
        /// <param name="policyId">The Id of the storage policy to assign the user to.</param>
        /// <returns>The assignment object for the storage policy assignment to user.</returns>
        Task<BoxStoragePolicyAssignment> CreateAssignmentAsync(string userId, string policyId);

        /// <summary>
        /// Sends request to delete an existing assignment.
        /// </summary>
        /// <param name="assignmentId">Id of the storage policy assignment.</param>
        /// <returns>A successful request returns 204 No Content.</returns>
        Task<bool> DeleteAssignmentAsync(string assignmentId);

        /// <summary>
        /// Checks if a storage policy assignment exists. If it does not then create an assignment. 
        /// </summary>
        /// <param name="userId">The id of the user to assign storage policy to.</param>
        /// <param name="storagePolicyId">The storage policy id to assign to user.</param>
        /// <returns></returns>
        Task<BoxStoragePolicyAssignment> AssignAsync(string userId, string storagePolicyId);
    }
}
