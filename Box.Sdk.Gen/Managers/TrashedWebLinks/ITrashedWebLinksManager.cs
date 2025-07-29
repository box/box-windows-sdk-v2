using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ITrashedWebLinksManager {
        /// <summary>
    /// Restores a web link that has been moved to the trash.
    /// 
    /// An optional new parent ID can be provided to restore the  web link to in case
    /// the original folder has been deleted.
    /// </summary>
    /// <param name="webLinkId">
    /// The ID of the web link.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of restoreWeblinkFromTrash method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of restoreWeblinkFromTrash method
    /// </param>
    /// <param name="headers">
    /// Headers of restoreWeblinkFromTrash method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TrashWebLinkRestored> RestoreWeblinkFromTrashAsync(string webLinkId, RestoreWeblinkFromTrashRequestBody? requestBody = default, RestoreWeblinkFromTrashQueryParams? queryParams = default, RestoreWeblinkFromTrashHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a web link that has been moved to the trash.
    /// </summary>
    /// <param name="webLinkId">
    /// The ID of the web link.
    /// Example: "12345"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getTrashedWebLinkById method
    /// </param>
    /// <param name="headers">
    /// Headers of getTrashedWebLinkById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TrashWebLink> GetTrashedWebLinkByIdAsync(string webLinkId, GetTrashedWebLinkByIdQueryParams? queryParams = default, GetTrashedWebLinkByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Permanently deletes a web link that is in the trash.
    /// This action cannot be undone.
    /// </summary>
    /// <param name="webLinkId">
    /// The ID of the web link.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteTrashedWebLinkById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteTrashedWebLinkByIdAsync(string webLinkId, DeleteTrashedWebLinkByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}