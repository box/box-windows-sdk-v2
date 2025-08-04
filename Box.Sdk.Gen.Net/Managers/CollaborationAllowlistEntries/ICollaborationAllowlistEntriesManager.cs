using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ICollaborationAllowlistEntriesManager {
        /// <summary>
    /// Returns the list domains that have been deemed safe to create collaborations
    /// for within the current enterprise.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getCollaborationWhitelistEntries method
    /// </param>
    /// <param name="headers">
    /// Headers of getCollaborationWhitelistEntries method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<CollaborationAllowlistEntries> GetCollaborationWhitelistEntriesAsync(GetCollaborationWhitelistEntriesQueryParams? queryParams = default, GetCollaborationWhitelistEntriesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a new entry in the list of allowed domains to allow
    /// collaboration for.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createCollaborationWhitelistEntry method
    /// </param>
    /// <param name="headers">
    /// Headers of createCollaborationWhitelistEntry method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<CollaborationAllowlistEntry> CreateCollaborationWhitelistEntryAsync(CreateCollaborationWhitelistEntryRequestBody requestBody, CreateCollaborationWhitelistEntryHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns a domain that has been deemed safe to create collaborations
    /// for within the current enterprise.
    /// </summary>
    /// <param name="collaborationWhitelistEntryId">
    /// The ID of the entry in the list.
    /// Example: "213123"
    /// </param>
    /// <param name="headers">
    /// Headers of getCollaborationWhitelistEntryById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<CollaborationAllowlistEntry> GetCollaborationWhitelistEntryByIdAsync(string collaborationWhitelistEntryId, GetCollaborationWhitelistEntryByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Removes a domain from the list of domains that have been deemed safe to create
    /// collaborations for within the current enterprise.
    /// </summary>
    /// <param name="collaborationWhitelistEntryId">
    /// The ID of the entry in the list.
    /// Example: "213123"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteCollaborationWhitelistEntryById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteCollaborationWhitelistEntryByIdAsync(string collaborationWhitelistEntryId, DeleteCollaborationWhitelistEntryByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}