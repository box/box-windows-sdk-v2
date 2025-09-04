using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IHubsManager {
        /// <summary>
    /// Retrieves all Box Hubs for requesting user.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getHubsV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getHubsV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubsV2025R0> GetHubsV2025R0Async(GetHubsV2025R0QueryParams? queryParams = default, GetHubsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a new Box Hub.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createHubV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createHubV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubV2025R0> CreateHubV2025R0Async(HubCreateRequestV2025R0 requestBody, CreateHubV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves all Box Hubs for a given enterprise.
    /// 
    /// Admins or Hub Co-admins of an enterprise
    /// with GCM scope can make this call.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getEnterpriseHubsV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getEnterpriseHubsV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubsV2025R0> GetEnterpriseHubsV2025R0Async(GetEnterpriseHubsV2025R0QueryParams? queryParams = default, GetEnterpriseHubsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves details for a Box Hub by its ID.
    /// </summary>
    /// <param name="hubId">
    /// The unique identifier that represent a hub.
    /// 
    /// The ID for any hub can be determined
    /// by visiting this hub in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/hubs/123`
    /// the `hub_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of getHubByIdV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubV2025R0> GetHubByIdV2025R0Async(string hubId, GetHubByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a Box Hub. Can be used to change title, description, or Box Hub settings.
    /// </summary>
    /// <param name="hubId">
    /// The unique identifier that represent a hub.
    /// 
    /// The ID for any hub can be determined
    /// by visiting this hub in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/hubs/123`
    /// the `hub_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateHubByIdV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of updateHubByIdV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubV2025R0> UpdateHubByIdV2025R0Async(string hubId, HubUpdateRequestV2025R0 requestBody, UpdateHubByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a single Box Hub.
    /// </summary>
    /// <param name="hubId">
    /// The unique identifier that represent a hub.
    /// 
    /// The ID for any hub can be determined
    /// by visiting this hub in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/hubs/123`
    /// the `hub_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteHubByIdV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteHubByIdV2025R0Async(string hubId, DeleteHubByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a copy of a Box Hub.
    /// 
    /// The original Box Hub will not be modified.
    /// </summary>
    /// <param name="hubId">
    /// The unique identifier that represent a hub.
    /// 
    /// The ID for any hub can be determined
    /// by visiting this hub in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/hubs/123`
    /// the `hub_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of copyHubV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of copyHubV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubV2025R0> CopyHubV2025R0Async(string hubId, HubCopyRequestV2025R0 requestBody, CopyHubV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}