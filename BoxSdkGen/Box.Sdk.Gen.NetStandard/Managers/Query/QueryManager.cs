using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class QueryManager {
        public IAuthentication Auth { get; set; }

        public NetworkSession NetworkSession { get; set; }

        public QueryManager(NetworkSession networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
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
        public async System.Threading.Tasks.Task<QueryResultsV2026R0> CreateQueryV2026R0Async(QueryRequestBodyV2026R0 requestBody, CreateQueryV2026R0Headers headers = default, System.Threading.CancellationToken cancellationToken = default) {
            headers = headers ?? new CreateQueryV2026R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/query"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<QueryResultsV2026R0>(NullableUtils.Unwrap(response.Data));
        }

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
        public async System.Threading.Tasks.Task<QueryInsightsV2026R0> CreateQueryInsightV2026R0Async(QueryInsightsRequestBodyV2026R0 requestBody, CreateQueryInsightV2026R0Headers headers = default, System.Threading.CancellationToken cancellationToken = default) {
            headers = headers ?? new CreateQueryInsightV2026R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/query_insights"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<QueryInsightsV2026R0>(NullableUtils.Unwrap(response.Data));
        }

    }
}