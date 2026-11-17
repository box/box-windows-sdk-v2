using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class AiSessionsManager {
        public IAuthentication Auth { get; set; }

        public NetworkSession NetworkSession { get; set; }

        public AiSessionsManager(NetworkSession networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Returns a marker-paginated list of the AI sessions that the authenticated
        /// user can read and that were created through the Box public API. Entries are
        /// ordered by `sort` and `direction`, defaulting to `updated_at` descending so
        /// the most recently active session appears first.
        /// 
        /// The access token must have both the `ai.readwrite` and `root_readwrite`
        /// scopes.
        /// 
        /// A page may contain fewer than `limit` entries while further pages remain.
        /// Treat a non-null `next_marker` as the only signal that more results exist.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getAiSessionsV2026R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getAiSessionsV2026R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<AiSessionsV2026R0> GetAiSessionsV2026R0Async(GetAiSessionsV2026R0QueryParams queryParams, GetAiSessionsV2026R0Headers headers = default, System.Threading.CancellationToken cancellationToken = default) {
            headers = headers ?? new GetAiSessionsV2026R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string>() { { "source", StringUtils.ToStringRepresentation(queryParams.Source?.Value) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "sort", StringUtils.ToStringRepresentation(queryParams.Sort?.Value) }, { "direction", StringUtils.ToStringRepresentation(queryParams.Direction?.Value) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) }, { "client_ids", StringUtils.ToStringRepresentation(queryParams.ClientIds) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/ai_sessions"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<AiSessionsV2026R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Deletes an AI session created through the Box public API, together with its
        /// agent tasks. The session's agent-generated files and uploaded files are
        /// moved to the trash. Files attached as sources from elsewhere in Box are not
        /// affected. Deleting a session does not stop a running task or notify the
        /// agent.
        /// 
        /// The access token must have both the `ai.readwrite` and `root_readwrite`
        /// scopes.
        /// </summary>
        /// <param name="aiSessionId">
        /// The unique identifier of the AI session. This is the same value returned as
        /// `id` by the list endpoint and used as the A2A `contextId`.
        /// Example: "23489723"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteAiSessionByIdV2026R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteAiSessionByIdV2026R0Async(string aiSessionId, DeleteAiSessionByIdV2026R0Headers headers = default, System.Threading.CancellationToken cancellationToken = default) {
            headers = headers ?? new DeleteAiSessionByIdV2026R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/ai_sessions/", NullableUtils.Unwrap(StringUtils.ToStringRepresentation(aiSessionId))), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}