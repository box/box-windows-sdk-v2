using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IHubCollaborationsManager {
        /// <summary>
    /// Retrieves all collaborations for a Box Hub.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getHubCollaborationsV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getHubCollaborationsV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubCollaborationsV2025R0> GetHubCollaborationsV2025R0Async(GetHubCollaborationsV2025R0QueryParams queryParams, GetHubCollaborationsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Adds a collaboration for a single user or a single group to a Box Hub.
    /// 
    /// Collaborations can be created using email address, user IDs, or group IDs.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createHubCollaborationV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createHubCollaborationV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubCollaborationV2025R0> CreateHubCollaborationV2025R0Async(HubCollaborationCreateRequestV2025R0 requestBody, CreateHubCollaborationV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves details for a Box Hub collaboration by collaboration ID.
    /// </summary>
    /// <param name="hubCollaborationId">
    /// The ID of the hub collaboration.
    /// Example: "1234"
    /// </param>
    /// <param name="headers">
    /// Headers of getHubCollaborationByIdV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubCollaborationV2025R0> GetHubCollaborationByIdV2025R0Async(string hubCollaborationId, GetHubCollaborationByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a Box Hub collaboration.
    /// Can be used to change the Box Hub role.
    /// </summary>
    /// <param name="hubCollaborationId">
    /// The ID of the hub collaboration.
    /// Example: "1234"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateHubCollaborationByIdV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of updateHubCollaborationByIdV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubCollaborationV2025R0> UpdateHubCollaborationByIdV2025R0Async(string hubCollaborationId, HubCollaborationUpdateRequestV2025R0 requestBody, UpdateHubCollaborationByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a single Box Hub collaboration.
    /// </summary>
    /// <param name="hubCollaborationId">
    /// The ID of the hub collaboration.
    /// Example: "1234"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteHubCollaborationByIdV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteHubCollaborationByIdV2025R0Async(string hubCollaborationId, DeleteHubCollaborationByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}