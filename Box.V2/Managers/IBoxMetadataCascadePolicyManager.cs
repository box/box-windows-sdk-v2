using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    /// <summary>
    /// The class managing the Box API's Metadata Cascade Policies endpoint.
    /// </summary>
    public interface IBoxMetadataCascadePolicyManager
    {
        /// <summary>
        /// Creates a metadata cascade policy on a folder and the sub folder items. 
        /// </summary>
        /// <param name="folderId">The id of the folder to assign the cascade policy to.</param>
        /// <param name="scope">The scope of the metadata cascade policy.</param>
        /// <param name="templateKey">The template key of the metadata cascade policy.</param>
        /// <returns>The metadata cascade policy if successfully created.</returns>
        Task<BoxMetadataCascadePolicy> CreateCascadePolicyAsync(string folderId, string scope, string templateKey);

        /// <summary>
        /// Retrieves a metadata cascade policy by policy Id. 
        /// </summary>
        /// <param name="policyId">The metadata cascade policy Id to retrieve.</param>
        /// <param name="fields">Optional fields to retrieve on metadata cascade policy.</param>
        /// <returns>The metadata cascade policy retrieved by Id.</returns>
        Task<BoxMetadataCascadePolicy> GetCascadePolicyAsync(string policyId, IEnumerable<string> fields = null);

        Task<BoxCollectionMarkerBased<BoxMetadataCascadePolicy>> GetAllMetadataCascadePoliciesAsync(string folderId, string ownerEnterpriseId = null, int limit = 100, string nextMarker = null, IEnumerable<string> fields = null, bool autopaginate = false);

        /// <summary>
        /// If a policy already exists on the specified folder, this will apply that new policy to the folder and the sub-folder items. 
        /// </summary>
        /// <param name="policyId">The policy Id to force apply.</param>
        /// <param name="conflictResolution">The desired behavior if a conflict exists. Set to either "none" or "overwrite".</param>
        Task<bool> ForceApplyCascadePolicyAsync(string policyId, string conflictResolution);

        /// <summary>
        /// Deletes a metadata cascade policy. 
        /// </summary>
        /// <param name="policyId">The id of the metadata cascade policy to delete.</param>
        /// <returns>True if metadata cascade policy was deleted successfully.</returns>
        Task<bool> DeleteCascadePolicyAsync(string policyId);
    }
}
