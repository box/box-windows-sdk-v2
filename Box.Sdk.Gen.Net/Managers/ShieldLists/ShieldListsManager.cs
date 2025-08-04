using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class ShieldListsManager : IShieldListsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public ShieldListsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves all shield lists in the enterprise.
        /// </summary>
        /// <param name="headers">
        /// Headers of getShieldListsV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ShieldListsV2025R0> GetShieldListsV2025R0Async(GetShieldListsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetShieldListsV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/shield_lists"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ShieldListsV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a shield list.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createShieldListV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of createShieldListV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ShieldListV2025R0> CreateShieldListV2025R0Async(ShieldListsCreateV2025R0 requestBody, CreateShieldListV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateShieldListV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/shield_lists"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ShieldListV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Retrieves a single shield list by its ID.
        /// </summary>
        /// <param name="shieldListId">
        /// The unique identifier that represents a shield list.
        /// The ID for any Shield List can be determined by the response from the endpoint
        /// fetching all shield lists for the enterprise.
        /// Example: "90fb0e17-c332-40ed-b4f9-fa8908fbbb24 "
        /// </param>
        /// <param name="headers">
        /// Headers of getShieldListByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ShieldListV2025R0> GetShieldListByIdV2025R0Async(string shieldListId, GetShieldListByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetShieldListByIdV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/shield_lists/", StringUtils.ToStringRepresentation(shieldListId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ShieldListV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Delete a single shield list by its ID.
        /// </summary>
        /// <param name="shieldListId">
        /// The unique identifier that represents a shield list.
        /// The ID for any Shield List can be determined by the response from the endpoint
        /// fetching all shield lists for the enterprise.
        /// Example: "90fb0e17-c332-40ed-b4f9-fa8908fbbb24 "
        /// </param>
        /// <param name="headers">
        /// Headers of deleteShieldListByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteShieldListByIdV2025R0Async(string shieldListId, DeleteShieldListByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteShieldListByIdV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/shield_lists/", StringUtils.ToStringRepresentation(shieldListId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates a shield list.
        /// </summary>
        /// <param name="shieldListId">
        /// The unique identifier that represents a shield list.
        /// The ID for any Shield List can be determined by the response from the endpoint
        /// fetching all shield lists for the enterprise.
        /// Example: "90fb0e17-c332-40ed-b4f9-fa8908fbbb24 "
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateShieldListByIdV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of updateShieldListByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ShieldListV2025R0> UpdateShieldListByIdV2025R0Async(string shieldListId, ShieldListsUpdateV2025R0 requestBody, UpdateShieldListByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new UpdateShieldListByIdV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/shield_lists/", StringUtils.ToStringRepresentation(shieldListId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ShieldListV2025R0>(NullableUtils.Unwrap(response.Data));
        }

    }
}