using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class ExternalUsersManager : IExternalUsersManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public ExternalUsersManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Delete external users from current user enterprise. This will remove each
        /// external user from all invited collaborations within the current enterprise.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createExternalUserSubmitDeleteJobV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of createExternalUserSubmitDeleteJobV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ExternalUsersSubmitDeleteJobResponseV2025R0> CreateExternalUserSubmitDeleteJobV2025R0Async(ExternalUsersSubmitDeleteJobRequestV2025R0 requestBody, CreateExternalUserSubmitDeleteJobV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateExternalUserSubmitDeleteJobV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/external_users/submit_delete_job"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ExternalUsersSubmitDeleteJobResponseV2025R0>(NullableUtils.Unwrap(response.Data));
        }

    }
}