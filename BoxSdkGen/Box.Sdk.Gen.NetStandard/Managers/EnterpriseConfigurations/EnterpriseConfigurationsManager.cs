using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class EnterpriseConfigurationsManager {
        public IAuthentication Auth { get; set; }

        public NetworkSession NetworkSession { get; set; }

        public EnterpriseConfigurationsManager(NetworkSession networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves the configuration for an enterprise.
        /// </summary>
        /// <param name="enterpriseId">
        /// The ID of the enterprise.
        /// Example: "3442311"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getEnterpriseConfigurationByIdV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getEnterpriseConfigurationByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<EnterpriseConfigurationV2025R0> GetEnterpriseConfigurationByIdV2025R0Async(string enterpriseId, GetEnterpriseConfigurationByIdV2025R0QueryParams queryParams, GetEnterpriseConfigurationByIdV2025R0Headers headers = default, System.Threading.CancellationToken cancellationToken = default) {
            headers = headers ?? new GetEnterpriseConfigurationByIdV2025R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string>() { { "categories", StringUtils.ToStringRepresentation(queryParams.Categories) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/enterprise_configurations/", StringUtils.ToStringRepresentation(enterpriseId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<EnterpriseConfigurationV2025R0>(NullableUtils.Unwrap(response.Data));
        }

    }
}