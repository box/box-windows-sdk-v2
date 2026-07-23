using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IQueryManager {
        /// <summary>
    /// Runs a query to discover Box items using a logical predicate that can filter
    /// across item fields and metadata templates. Results can be sorted, paginated,
    /// and shaped to include additional item or metadata fields.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createQueryV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createQueryV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<QueryResultsV2026R0> CreateQueryV2026R0Async(QueryRequestBodyV2026R0 requestBody, CreateQueryV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Computes aggregated metrics over Box items matching a query predicate.
    /// Filters are applied first, followed by optional grouping, after which the
    /// requested metrics (such as `sum`, `avg`, `min`, `max`, and `count`) are
    /// computed for each resulting group or over the entire filtered dataset.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createQueryInsightV2026R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createQueryInsightV2026R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<QueryInsightsV2026R0> CreateQueryInsightV2026R0Async(QueryInsightsRequestBodyV2026R0 requestBody, CreateQueryInsightV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}