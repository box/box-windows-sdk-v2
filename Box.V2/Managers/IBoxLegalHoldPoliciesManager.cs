using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    /// <summary>
    /// Allow create, update, get, delete legal hold and legal hold assignment.
    /// </summary>
    public interface IBoxLegalHoldPoliciesManager
    {
        /// <summary>
        /// Get details of a single Legal Hold Policy.
        /// </summary>
        /// <param name="legalHoldId">Id of the legal hold policy.</param>
        /// <returns>If the Id is valid, information about the Legal Hold Policy is returned.</returns>
        Task<BoxLegalHoldPolicy> GetLegalHoldPolicyAsync(string legalHoldId);

        /// <summary>
        /// Get a list of Legal Hold Policies that belong to your Enterprise.
        /// </summary>
        /// <param name="policyName">Case insensitive prefix-match filter on Policy name.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="limit">Limit result size to this number. Defaults to 100, maximum is 1,000.</param>
        /// <param name="marker">Take from "next_marker" column of a prior call to get the next page.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns the list of Legal Hold Policies in your Enterprise that match the filter parameters (if passed in). By default, will only return only 'type', 'id', and 'policy_name', but you can specify more by using the 'fields' parameter.</returns>
        Task<BoxCollectionMarkerBased<BoxLegalHoldPolicy>> GetListLegalHoldPoliciesAsync(string policyName = null, string fields = null, int limit = 100, string marker = null, bool autoPaginate = false);

        /// <summary>
        /// Create a new Legal Hold Policy. Optional date filter may be passed. 
        /// If Policy has a date filter, any Custodian assignments will apply only to file versions created or uploaded inside of the date range. 
        /// (Other assignment types, such as folders and files, will ignore the date filter).
        /// </summary>
        /// <param name="createRequest">BoxLegalHoldPolicyRequest object.</param>
        /// <returns>For a successful request, returns information about the Legal Hold Policy created. 
        /// If the Policy Name is in use for your enterprise, will return null.
        /// </returns>
        Task<BoxLegalHoldPolicy> CreateLegalHoldPolicyAsync(BoxLegalHoldPolicyRequest createRequest);

        /// <summary>
        /// Update existing Legal Hold Policy. Only name and description can be modified.
        /// </summary>
        /// <param name="legalHoldPolicyId">Id of the legal hold policy.</param>
        /// <param name="updateRequest">BoxLegalHoldPolicyRequest object.</param>
        /// <returns>Returns information about the Legal Hold Policy updated.</returns>
        Task<BoxLegalHoldPolicy> UpdateLegalHoldPolicyAsync(string legalHoldPolicyId, BoxLegalHoldPolicyRequest updateRequest);

        /// <summary>
        /// Sends request to delete an existing Legal Hold Policy. Note that this is an asynchronous process - the Policy will not be fully deleted yet when the response comes back.
        /// </summary>
        /// <param name="legalHoldPolicyId">Id of the legal hold policy.</param>
        /// <returns>Returns True if the request to delete the Policy was accepted.</returns>
        Task<bool> DeleteLegalHoldPolicyAsync(string legalHoldPolicyId);

        /// <summary>
        /// Get details of a single assignment.
        /// </summary>
        /// <param name="assignmentId">Id of the assignment.</param>
        /// <returns>If the assignmentId is valid, information about the Assignment is returned </returns>
        Task<BoxLegalHoldPolicyAssignment> GetAssignmentAsync(string assignmentId);

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
        Task<BoxCollectionMarkerBased<BoxLegalHoldPolicyAssignment>> GetAssignmentsAsync(string legalHoldPolicyId, string fields = null, string assignToType = null, string assignToId = null, int limit = 100, string marker = null, bool autoPaginate = false);

        /// <summary>
        /// Create a new Assignment, which will apply the Legal Hold Policy to the target of the Assignment.
        /// </summary>
        /// <param name="createRequest">BoxLegalHoldPolicyAssignmentRequest object.</param>
        /// <returns>For a successful request, returns object with information about the Assignment created. 
        /// If the policy or assign-to target cannot be found, null will be returned.
        /// </returns>
        Task<BoxLegalHoldPolicyAssignment> CreateAssignmentAsync(BoxLegalHoldPolicyAssignmentRequest createRequest);

        /// <summary>
        /// Sends request to delete an existing Assignment.
        /// Note that this is an asynchronous process - the Assignment will not be fully deleted yet when the response comes back.
        /// </summary>
        /// <param name="assignmentId">ID of the legal holds assignment.</param>
        /// <returns>A successful request returns 204 No Content. If the Assignment is still initializing, will return a 409.</returns>
        Task<bool> DeleteAssignmentAsync(string assignmentId);

        /// <summary>
        /// Get details of a single File Version Legal Hold.
        /// </summary>
        /// <param name="fileVersionLegalHoldId">ID of the file version legal hold.</param>
        /// <returns>If the ID is valid, information about the Hold is returned with a 200.
        /// If the ID is for a non-existent Hold, a 404 is returned.</returns>
        Task<BoxFileVersionLegalHold> GetFileVersionLegalHoldAsync(string fileVersionLegalHoldId);

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
        Task<BoxCollectionMarkerBased<BoxFileVersionLegalHold>> GetFileVersionLegalHoldsAsync(string policyId, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false);
    }
}
