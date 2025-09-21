using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ICollaborationAllowlistExemptTargetsManager {
        /// <summary>
    /// Returns a list of users who have been exempt from the collaboration
    /// domain restrictions.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getCollaborationWhitelistExemptTargets method
    /// </param>
    /// <param name="headers">
    /// Headers of getCollaborationWhitelistExemptTargets method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<CollaborationAllowlistExemptTargets> GetCollaborationWhitelistExemptTargetsAsync(GetCollaborationWhitelistExemptTargetsQueryParams? queryParams = default, GetCollaborationWhitelistExemptTargetsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Exempts a user from the restrictions set out by the allowed list of domains
    /// for collaborations.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createCollaborationWhitelistExemptTarget method
    /// </param>
    /// <param name="headers">
    /// Headers of createCollaborationWhitelistExemptTarget method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<CollaborationAllowlistExemptTarget> CreateCollaborationWhitelistExemptTargetAsync(CreateCollaborationWhitelistExemptTargetRequestBody requestBody, CreateCollaborationWhitelistExemptTargetHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns a users who has been exempt from the collaboration
    /// domain restrictions.
    /// </summary>
    /// <param name="collaborationWhitelistExemptTargetId">
    /// The ID of the exemption to the list.
    /// Example: "984923"
    /// </param>
    /// <param name="headers">
    /// Headers of getCollaborationWhitelistExemptTargetById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<CollaborationAllowlistExemptTarget> GetCollaborationWhitelistExemptTargetByIdAsync(string collaborationWhitelistExemptTargetId, GetCollaborationWhitelistExemptTargetByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Removes a user's exemption from the restrictions set out by the allowed list
    /// of domains for collaborations.
    /// </summary>
    /// <param name="collaborationWhitelistExemptTargetId">
    /// The ID of the exemption to the list.
    /// Example: "984923"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteCollaborationWhitelistExemptTargetById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteCollaborationWhitelistExemptTargetByIdAsync(string collaborationWhitelistExemptTargetId, DeleteCollaborationWhitelistExemptTargetByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}