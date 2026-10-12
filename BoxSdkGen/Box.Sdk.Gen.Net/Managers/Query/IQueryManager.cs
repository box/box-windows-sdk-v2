using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IQueryManager {
        /// <summary>
    /// Runs a query to discover Box items using a logical predicate, similar to a SQL
    /// WHERE clause. The predicate can filter across multiple metadata templates and
    /// item fields in a single request. Results can be sorted, paginated with markers,
    /// and shaped with fields so each entry includes additional item or metadata
    /// attributes.
    /// 
    /// This is the item-retrieval endpoint in the Query API (version 2026.0). Unlike
    /// the [Metadata Query API](https://developer.box.com/reference/post-metadata-queries-execute-read),
    /// it is not limited to a single metadata template or to files and folders. For
    /// totals and grouped metrics over the same predicates, use
    /// [Query Insights](https://developer.box.com/reference/v2026.0/post-query-insights)
    /// instead of paging through every matching item.
    /// 
    /// For predicate syntax, parameters, examples, pagination behavior, and error
    /// codes, see the
    /// [Box Query guide](https://developer.box.com/guides/query/box-query). For shared
    /// response schemas, including Query results and result entries, see
    /// [Query resources](https://developer.box.com/reference/v2026.0/query-resources).
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