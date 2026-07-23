using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class QueryManager : IQueryManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public QueryManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
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
        public async System.Threading.Tasks.Task<QueryResultsV2026R0> CreateQueryV2026R0Async(QueryRequestBodyV2026R0 requestBody, CreateQueryV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateQueryV2026R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
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
        public async System.Threading.Tasks.Task<QueryInsightsV2026R0> CreateQueryInsightV2026R0Async(QueryInsightsRequestBodyV2026R0 requestBody, CreateQueryInsightV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateQueryInsightV2026R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/query_insights"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<QueryInsightsV2026R0>(NullableUtils.Unwrap(response.Data));
        }

    }
}