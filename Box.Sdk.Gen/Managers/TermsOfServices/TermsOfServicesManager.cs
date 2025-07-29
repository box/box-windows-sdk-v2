using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class TermsOfServicesManager : ITermsOfServicesManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public TermsOfServicesManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Returns the current terms of service text and settings
        /// for the enterprise.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getTermsOfService method
        /// </param>
        /// <param name="headers">
        /// Headers of getTermsOfService method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<TermsOfServices> GetTermsOfServiceAsync(GetTermsOfServiceQueryParams? queryParams = default, GetTermsOfServiceHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetTermsOfServiceQueryParams();
            headers = headers ?? new GetTermsOfServiceHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "tos_type", StringUtils.ToStringRepresentation(queryParams.TosType) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/terms_of_services"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<TermsOfServices>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a terms of service for a given enterprise
        /// and type of user.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createTermsOfService method
        /// </param>
        /// <param name="headers">
        /// Headers of createTermsOfService method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<TermsOfService> CreateTermsOfServiceAsync(CreateTermsOfServiceRequestBody requestBody, CreateTermsOfServiceHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateTermsOfServiceHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/terms_of_services"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<TermsOfService>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Fetches a specific terms of service.
        /// </summary>
        /// <param name="termsOfServiceId">
        /// The ID of the terms of service.
        /// Example: "324234"
        /// </param>
        /// <param name="headers">
        /// Headers of getTermsOfServiceById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<TermsOfService> GetTermsOfServiceByIdAsync(string termsOfServiceId, GetTermsOfServiceByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetTermsOfServiceByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/terms_of_services/", StringUtils.ToStringRepresentation(termsOfServiceId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<TermsOfService>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates a specific terms of service.
        /// </summary>
        /// <param name="termsOfServiceId">
        /// The ID of the terms of service.
        /// Example: "324234"
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateTermsOfServiceById method
        /// </param>
        /// <param name="headers">
        /// Headers of updateTermsOfServiceById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<TermsOfService> UpdateTermsOfServiceByIdAsync(string termsOfServiceId, UpdateTermsOfServiceByIdRequestBody requestBody, UpdateTermsOfServiceByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new UpdateTermsOfServiceByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/terms_of_services/", StringUtils.ToStringRepresentation(termsOfServiceId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<TermsOfService>(NullableUtils.Unwrap(response.Data));
        }

    }
}