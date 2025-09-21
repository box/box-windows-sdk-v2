using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IUserCollaborationsManager {
        /// <summary>
    /// Retrieves a single collaboration.
    /// </summary>
    /// <param name="collaborationId">
    /// The ID of the collaboration.
    /// Example: "1234"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getCollaborationById method
    /// </param>
    /// <param name="headers">
    /// Headers of getCollaborationById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Collaboration> GetCollaborationByIdAsync(string collaborationId, GetCollaborationByIdQueryParams? queryParams = default, GetCollaborationByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a collaboration.
    /// Can be used to change the owner of an item, or to
    /// accept collaboration invites.
    /// </summary>
    /// <param name="collaborationId">
    /// The ID of the collaboration.
    /// Example: "1234"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateCollaborationById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateCollaborationById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Collaboration?> UpdateCollaborationByIdAsync(string collaborationId, UpdateCollaborationByIdRequestBody requestBody, UpdateCollaborationByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a single collaboration.
    /// </summary>
    /// <param name="collaborationId">
    /// The ID of the collaboration.
    /// Example: "1234"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteCollaborationById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteCollaborationByIdAsync(string collaborationId, DeleteCollaborationByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Adds a collaboration for a single user or a single group to a file
    /// or folder.
    /// 
    /// Collaborations can be created using email address, user IDs, or a
    /// group IDs.
    /// 
    /// If a collaboration is being created with a group, access to
    /// this endpoint is dependent on the group's ability to be invited.
    /// 
    /// If collaboration is in `pending` status, the following fields
    /// are redacted:
    /// - `login` and `name` are hidden if a collaboration was created
    /// using `user_id`,
    /// -  `name` is hidden if a collaboration was created using `login`.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createCollaboration method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of createCollaboration method
    /// </param>
    /// <param name="headers">
    /// Headers of createCollaboration method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Collaboration> CreateCollaborationAsync(CreateCollaborationRequestBody requestBody, CreateCollaborationQueryParams? queryParams = default, CreateCollaborationHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}