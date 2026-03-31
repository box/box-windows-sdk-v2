using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IHubDocumentManager {
        /// <summary>
    /// Retrieves a list of Hub Document Pages for the specified hub.
    /// Includes both root-level pages and sub pages.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getHubDocumentPagesV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getHubDocumentPagesV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubDocumentPagesV2025R0> GetHubDocumentPagesV2025R0Async(GetHubDocumentPagesV2025R0QueryParams queryParams, GetHubDocumentPagesV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a sorted list of all Hub Document Blocks on a specified page in the hub document, excluding items.
    /// Blocks are hierarchically organized by their `parent_id`.
    /// Blocks are sorted in order based on user specification in the user interface.
    /// The response will only include content blocks that belong to the specified page. This will not include sub pages or sub page content blocks.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getHubDocumentBlocksV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getHubDocumentBlocksV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<HubDocumentBlocksV2025R0> GetHubDocumentBlocksV2025R0Async(GetHubDocumentBlocksV2025R0QueryParams queryParams, GetHubDocumentBlocksV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}