using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class DocumentSplitManager : IDocumentSplitManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public DocumentSplitManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Initiates a Document Split job for the supplied source file. The job runs
        /// asynchronously; clients can poll the returned job ID to track progress.
        /// 
        /// All split-specific input lives under `split_input`. When
        /// `split_input.split_type` is `manual`, provide
        /// `split_input.split_definition` (continuous page ranges; `version` may be
        /// omitted or `v_1`). When `split_input.split_type` is `page_selection`,
        /// provide `split_input.page_selection` (non-linear page segments; `version`
        /// may be omitted or `v_2`). `split_input.prompt` must not be provided for
        /// either of those variants. When `split_input.split_type` is `smart`,
        /// `split_input.prompt` is required and neither
        /// `split_input.split_definition` nor `split_input.page_selection` must be
        /// provided.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createDocumentSplitV2026R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of createDocumentSplitV2026R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DocumentSplitCreateResponseV2026R0> CreateDocumentSplitV2026R0Async(DocumentSplitCreateRequestV2026R0 requestBody, CreateDocumentSplitV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateDocumentSplitV2026R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/document_splits"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DocumentSplitCreateResponseV2026R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Returns a paginated list of Document Split jobs accessible to the
        /// authenticated user.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getDocumentSplitsV2026R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getDocumentSplitsV2026R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DocumentSplitsV2026R0> GetDocumentSplitsV2026R0Async(GetDocumentSplitsV2026R0QueryParams? queryParams = default, GetDocumentSplitsV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetDocumentSplitsV2026R0QueryParams();
            headers = headers ?? new GetDocumentSplitsV2026R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/document_splits"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DocumentSplitsV2026R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Returns details for a single Document Split job identified by its ID,
        /// including the current status, the input definition, and any output files
        /// produced by the job.
        /// </summary>
        /// <param name="documentSplitId">
        /// The unique identifier for the Document Split job.
        /// Example: "1748426"
        /// </param>
        /// <param name="headers">
        /// Headers of getDocumentSplitByIdV2026R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DocumentSplitV2026R0> GetDocumentSplitByIdV2026R0Async(string documentSplitId, GetDocumentSplitByIdV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetDocumentSplitByIdV2026R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/document_splits/", NullableUtils.Unwrap(StringUtils.ToStringRepresentation(documentSplitId))), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DocumentSplitV2026R0>(NullableUtils.Unwrap(response.Data));
        }

    }
}