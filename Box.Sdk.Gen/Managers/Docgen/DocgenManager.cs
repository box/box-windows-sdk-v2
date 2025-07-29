using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class DocgenManager : IDocgenManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public DocgenManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Get details of the Box Doc Gen job.
        /// </summary>
        /// <param name="jobId">
        /// Box Doc Gen job ID.
        /// Example: 123
        /// </param>
        /// <param name="headers">
        /// Headers of getDocgenJobByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DocGenJobV2025R0> GetDocgenJobByIdV2025R0Async(string jobId, GetDocgenJobByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetDocgenJobByIdV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/docgen_jobs/", StringUtils.ToStringRepresentation(jobId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DocGenJobV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Lists all Box Doc Gen jobs for a user.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getDocgenJobsV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getDocgenJobsV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DocGenJobsFullV2025R0> GetDocgenJobsV2025R0Async(GetDocgenJobsV2025R0QueryParams? queryParams = default, GetDocgenJobsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetDocgenJobsV2025R0QueryParams();
            headers = headers ?? new GetDocgenJobsV2025R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/docgen_jobs"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DocGenJobsFullV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Lists Box Doc Gen jobs in a batch.
        /// </summary>
        /// <param name="batchId">
        /// Box Doc Gen batch ID.
        /// Example: 123
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getDocgenBatchJobByIdV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getDocgenBatchJobByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DocGenJobsV2025R0> GetDocgenBatchJobByIdV2025R0Async(string batchId, GetDocgenBatchJobByIdV2025R0QueryParams? queryParams = default, GetDocgenBatchJobByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetDocgenBatchJobByIdV2025R0QueryParams();
            headers = headers ?? new GetDocgenBatchJobByIdV2025R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/docgen_batch_jobs/", StringUtils.ToStringRepresentation(batchId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DocGenJobsV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Generates a document using a Box Doc Gen template.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createDocgenBatchV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of createDocgenBatchV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DocGenBatchBaseV2025R0> CreateDocgenBatchV2025R0Async(DocGenBatchCreateRequestV2025R0 requestBody, CreateDocgenBatchV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateDocgenBatchV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/docgen_batches"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DocGenBatchBaseV2025R0>(NullableUtils.Unwrap(response.Data));
        }

    }
}