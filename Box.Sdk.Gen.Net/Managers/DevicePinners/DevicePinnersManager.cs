using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class DevicePinnersManager : IDevicePinnersManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public DevicePinnersManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves information about an individual device pin.
        /// </summary>
        /// <param name="devicePinnerId">
        /// The ID of the device pin.
        /// Example: "2324234"
        /// </param>
        /// <param name="headers">
        /// Headers of getDevicePinnerById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DevicePinner> GetDevicePinnerByIdAsync(string devicePinnerId, GetDevicePinnerByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetDevicePinnerByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/device_pinners/", StringUtils.ToStringRepresentation(devicePinnerId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DevicePinner>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Deletes an individual device pin.
        /// </summary>
        /// <param name="devicePinnerId">
        /// The ID of the device pin.
        /// Example: "2324234"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteDevicePinnerById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteDevicePinnerByIdAsync(string devicePinnerId, DeleteDevicePinnerByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteDevicePinnerByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/device_pinners/", StringUtils.ToStringRepresentation(devicePinnerId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves all the device pins within an enterprise.
        /// 
        /// The user must have admin privileges, and the application
        /// needs the "manage enterprise" scope to make this call.
        /// </summary>
        /// <param name="enterpriseId">
        /// The ID of the enterprise.
        /// Example: "3442311"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getEnterpriseDevicePinners method
        /// </param>
        /// <param name="headers">
        /// Headers of getEnterpriseDevicePinners method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DevicePinners> GetEnterpriseDevicePinnersAsync(string enterpriseId, GetEnterpriseDevicePinnersQueryParams? queryParams = default, GetEnterpriseDevicePinnersHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetEnterpriseDevicePinnersQueryParams();
            headers = headers ?? new GetEnterpriseDevicePinnersHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) }, { "direction", StringUtils.ToStringRepresentation(queryParams.Direction?.Value) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/enterprises/", StringUtils.ToStringRepresentation(enterpriseId), "/device_pinners"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DevicePinners>(NullableUtils.Unwrap(response.Data));
        }

    }
}