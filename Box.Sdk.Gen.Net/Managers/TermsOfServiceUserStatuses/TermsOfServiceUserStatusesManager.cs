using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class TermsOfServiceUserStatusesManager : ITermsOfServiceUserStatusesManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public TermsOfServiceUserStatusesManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves an overview of users and their status for a
        /// terms of service, including Whether they have accepted
        /// the terms and when.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getTermsOfServiceUserStatuses method
        /// </param>
        /// <param name="headers">
        /// Headers of getTermsOfServiceUserStatuses method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<TermsOfServiceUserStatuses> GetTermsOfServiceUserStatusesAsync(GetTermsOfServiceUserStatusesQueryParams queryParams, GetTermsOfServiceUserStatusesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetTermsOfServiceUserStatusesHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "tos_id", StringUtils.ToStringRepresentation(queryParams.TosId) }, { "user_id", StringUtils.ToStringRepresentation(queryParams.UserId) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/terms_of_service_user_statuses"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<TermsOfServiceUserStatuses>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Sets the status for a terms of service for a user.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createTermsOfServiceStatusForUser method
        /// </param>
        /// <param name="headers">
        /// Headers of createTermsOfServiceStatusForUser method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<TermsOfServiceUserStatus> CreateTermsOfServiceStatusForUserAsync(CreateTermsOfServiceStatusForUserRequestBody requestBody, CreateTermsOfServiceStatusForUserHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateTermsOfServiceStatusForUserHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/terms_of_service_user_statuses"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<TermsOfServiceUserStatus>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates the status for a terms of service for a user.
        /// </summary>
        /// <param name="termsOfServiceUserStatusId">
        /// The ID of the terms of service status.
        /// Example: "324234"
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateTermsOfServiceStatusForUserById method
        /// </param>
        /// <param name="headers">
        /// Headers of updateTermsOfServiceStatusForUserById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<TermsOfServiceUserStatus> UpdateTermsOfServiceStatusForUserByIdAsync(string termsOfServiceUserStatusId, UpdateTermsOfServiceStatusForUserByIdRequestBody requestBody, UpdateTermsOfServiceStatusForUserByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new UpdateTermsOfServiceStatusForUserByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/terms_of_service_user_statuses/", StringUtils.ToStringRepresentation(termsOfServiceUserStatusId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<TermsOfServiceUserStatus>(NullableUtils.Unwrap(response.Data));
        }

    }
}