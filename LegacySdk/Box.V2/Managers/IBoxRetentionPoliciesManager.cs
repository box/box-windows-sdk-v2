using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;

namespace Box.V2.Managers
{
    /// <summary>
    /// The class managing the Box API's Retention Policies endpoint.
    /// Retention Management is a feature of the Box Governance package, which can be added on to any Business Plus or Enterprise account.
    /// To use this feature, you must have the manage retention policies scope enabled for your API key via your application management console.
    /// </summary>
    public interface IBoxRetentionPoliciesManager
    {
        /// <summary>
        /// Used to create a new retention policy.
        /// </summary>
        /// <param name="retentionPolicyRequest">BoxRetentionPolicyRequest object.</param>
        /// <returns>A new retention policy object will be returned upon success.</returns>
        Task<BoxRetentionPolicy> CreateRetentionPolicyAsync(BoxRetentionPolicyRequest retentionPolicyRequest);

        /// <summary>
        /// Used to retrieve information about a retention policy.
        /// </summary>
        /// <param name="id">ID of the retention policy.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The specified retention policy will be returned upon success.</returns>
        Task<BoxRetentionPolicy> GetRetentionPolicyAsync(string id, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to update a retention policy.
        /// </summary>
        /// <param name="id">ID of the retention policy.</param>
        /// <param name="retentionPolicyRequest">BoxRetentionPolicyRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>An updated retention policy object will be returned upon success.</returns>
        Task<BoxRetentionPolicy> UpdateRetentionPolicyAsync(string id, BoxRetentionPolicyRequest retentionPolicyRequest, IEnumerable<string> fields = null);

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
        Task<BoxCollectionMarkerBased<BoxRetentionPolicy>> GetRetentionPoliciesAsync(string policyName = null, string policyType = null, string createdByUserId = null, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false);

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
        Task<BoxCollectionMarkerBased<BoxRetentionPolicyAssignment>> GetRetentionPolicyAssignmentsAsync(string retentionPolicyId, string type = null, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false);

        /// <summary>
        /// Creates a retention policy assignment that associates a retention policy with either a folder or an enterprise
        /// </summary>
        /// <param name="policyAssignmentRequest"></param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A new retention policy assignment will be returned upon success.</returns>
        Task<BoxRetentionPolicyAssignment> CreateRetentionPolicyAssignmentAsync(BoxRetentionPolicyAssignmentRequest policyAssignmentRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to retrieve information about a retention policy assignment.
        /// </summary>
        /// <param name="retentionPolicyAssignmentId">ID of the retention policy assignment.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The specified retention policy assignment will be returned upon success.</returns>
        Task<BoxRetentionPolicyAssignment> GetRetentionPolicyAssignmentAsync(string retentionPolicyAssignmentId, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to delete a retention policy assignment.
        /// </summary>
        /// <param name="retentionPolicyAssignmentId">ID of the retention policy assignment.</param>
        /// <returns>True if the retention policy assignment was successfully deleted.</returns>
        Task<bool> DeleteRetentionPolicyAssignmentAsync(string retentionPolicyAssignmentId);

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
        Task<BoxCollectionMarkerBased<BoxFileVersionRetention>> GetFileVersionRetentionsAsync(IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false, string fileId = null, string fileVersionId = null, string policyId = null, DateTimeOffset? dispositionBefore = null, DateTimeOffset? dispositionAfter = null, DispositionAction? dispositionAction = null);

        /// <summary>
        /// Used to retrieve information about a file version retention.
        /// </summary>
        /// <param name="fileVersionRetentionId">ID of the file version retention policy.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Returns the list of all file version retentions for the enterprise.</returns>
        Task<BoxFileVersionRetention> GetFileVersionRetentionAsync(string fileVersionRetentionId, IEnumerable<string> fields = null);

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
        Task<BoxCollectionMarkerBased<BoxFile>> GetFilesUnderRetentionForAssignmentAsync(string retentionPolicyAssignmentId, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false);

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
        Task<BoxCollectionMarkerBased<BoxFile>> GetFileVersionsUnderRetentionForAssignmentAsync(string retentionPolicyAssignmentId, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false);
    }
}
