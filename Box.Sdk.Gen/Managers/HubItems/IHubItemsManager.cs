using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IHubItemsManager {
        /// <summary>
    /// Retrieves all items associated with a Hub.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getHubItemsV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getHubItemsV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubItemsV2025R0> GetHubItemsV2025R0Async(GetHubItemsV2025R0QueryParams queryParams, GetHubItemsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Adds and/or removes Hub items from a Hub.
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
    /// Request body of manageHubItemsV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of manageHubItemsV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubItemsManageResponseV2025R0> ManageHubItemsV2025R0Async(string hubId, HubItemsManageRequestV2025R0 requestBody, ManageHubItemsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}