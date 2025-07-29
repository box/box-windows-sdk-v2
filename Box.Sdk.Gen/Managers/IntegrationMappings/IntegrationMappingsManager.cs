using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class IntegrationMappingsManager : IIntegrationMappingsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public IntegrationMappingsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Lists [Slack integration mappings](https://support.box.com/hc/en-us/articles/4415585987859-Box-as-the-Content-Layer-for-Slack) in a users' enterprise.
        /// 
        /// You need Admin or Co-Admin role to
        /// use this endpoint.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getSlackIntegrationMapping method
        /// </param>
        /// <param name="headers">
        /// Headers of getSlackIntegrationMapping method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<IntegrationMappings> GetSlackIntegrationMappingAsync(GetSlackIntegrationMappingQueryParams? queryParams = default, GetSlackIntegrationMappingHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetSlackIntegrationMappingQueryParams();
            headers = headers ?? new GetSlackIntegrationMappingHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) }, { "partner_item_type", StringUtils.ToStringRepresentation(queryParams.PartnerItemType) }, { "partner_item_id", StringUtils.ToStringRepresentation(queryParams.PartnerItemId) }, { "box_item_id", StringUtils.ToStringRepresentation(queryParams.BoxItemId) }, { "box_item_type", StringUtils.ToStringRepresentation(queryParams.BoxItemType) }, { "is_manually_created", StringUtils.ToStringRepresentation(queryParams.IsManuallyCreated) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/integration_mappings/slack"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<IntegrationMappings>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a [Slack integration mapping](https://support.box.com/hc/en-us/articles/4415585987859-Box-as-the-Content-Layer-for-Slack)
        /// by mapping a Slack channel to a Box item.
        /// 
        /// You need Admin or Co-Admin role to
        /// use this endpoint.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createSlackIntegrationMapping method
        /// </param>
        /// <param name="headers">
        /// Headers of createSlackIntegrationMapping method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<IntegrationMapping> CreateSlackIntegrationMappingAsync(IntegrationMappingSlackCreateRequest requestBody, CreateSlackIntegrationMappingHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateSlackIntegrationMappingHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/integration_mappings/slack"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<IntegrationMapping>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates a [Slack integration mapping](https://support.box.com/hc/en-us/articles/4415585987859-Box-as-the-Content-Layer-for-Slack).
        /// Supports updating the Box folder ID and options.
        /// 
        /// You need Admin or Co-Admin role to
        /// use this endpoint.
        /// </summary>
        /// <param name="integrationMappingId">
        /// An ID of an integration mapping.
        /// Example: "11235432"
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateSlackIntegrationMappingById method
        /// </param>
        /// <param name="headers">
        /// Headers of updateSlackIntegrationMappingById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<IntegrationMapping> UpdateSlackIntegrationMappingByIdAsync(string integrationMappingId, UpdateSlackIntegrationMappingByIdRequestBody? requestBody = default, UpdateSlackIntegrationMappingByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            requestBody = requestBody ?? new UpdateSlackIntegrationMappingByIdRequestBody();
            headers = headers ?? new UpdateSlackIntegrationMappingByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/integration_mappings/slack/", StringUtils.ToStringRepresentation(integrationMappingId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<IntegrationMapping>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Deletes a [Slack integration mapping](https://support.box.com/hc/en-us/articles/4415585987859-Box-as-the-Content-Layer-for-Slack).
        /// 
        /// 
        /// You need Admin or Co-Admin role to
        /// use this endpoint.
        /// </summary>
        /// <param name="integrationMappingId">
        /// An ID of an integration mapping.
        /// Example: "11235432"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteSlackIntegrationMappingById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteSlackIntegrationMappingByIdAsync(string integrationMappingId, DeleteSlackIntegrationMappingByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteSlackIntegrationMappingByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/integration_mappings/slack/", StringUtils.ToStringRepresentation(integrationMappingId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Lists [Teams integration mappings](https://support.box.com/hc/en-us/articles/360044681474-Using-Box-for-Teams) in a users' enterprise.
        /// You need Admin or Co-Admin role to
        /// use this endpoint.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getTeamsIntegrationMapping method
        /// </param>
        /// <param name="headers">
        /// Headers of getTeamsIntegrationMapping method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<IntegrationMappingsTeams> GetTeamsIntegrationMappingAsync(GetTeamsIntegrationMappingQueryParams? queryParams = default, GetTeamsIntegrationMappingHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetTeamsIntegrationMappingQueryParams();
            headers = headers ?? new GetTeamsIntegrationMappingHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "partner_item_type", StringUtils.ToStringRepresentation(queryParams.PartnerItemType) }, { "partner_item_id", StringUtils.ToStringRepresentation(queryParams.PartnerItemId) }, { "box_item_id", StringUtils.ToStringRepresentation(queryParams.BoxItemId) }, { "box_item_type", StringUtils.ToStringRepresentation(queryParams.BoxItemType) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/integration_mappings/teams"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<IntegrationMappingsTeams>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a [Teams integration mapping](https://support.box.com/hc/en-us/articles/360044681474-Using-Box-for-Teams)
        /// by mapping a Teams channel to a Box item.
        /// You need Admin or Co-Admin role to
        /// use this endpoint.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createTeamsIntegrationMapping method
        /// </param>
        /// <param name="headers">
        /// Headers of createTeamsIntegrationMapping method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<IntegrationMappingTeams> CreateTeamsIntegrationMappingAsync(IntegrationMappingTeamsCreateRequest requestBody, CreateTeamsIntegrationMappingHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateTeamsIntegrationMappingHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/integration_mappings/teams"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<IntegrationMappingTeams>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates a [Teams integration mapping](https://support.box.com/hc/en-us/articles/360044681474-Using-Box-for-Teams).
        /// Supports updating the Box folder ID and options.
        /// You need Admin or Co-Admin role to
        /// use this endpoint.
        /// </summary>
        /// <param name="integrationMappingId">
        /// An ID of an integration mapping.
        /// Example: "11235432"
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateTeamsIntegrationMappingById method
        /// </param>
        /// <param name="headers">
        /// Headers of updateTeamsIntegrationMappingById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<IntegrationMappingTeams> UpdateTeamsIntegrationMappingByIdAsync(string integrationMappingId, UpdateTeamsIntegrationMappingByIdRequestBody? requestBody = default, UpdateTeamsIntegrationMappingByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            requestBody = requestBody ?? new UpdateTeamsIntegrationMappingByIdRequestBody();
            headers = headers ?? new UpdateTeamsIntegrationMappingByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/integration_mappings/teams/", StringUtils.ToStringRepresentation(integrationMappingId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<IntegrationMappingTeams>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Deletes a [Teams integration mapping](https://support.box.com/hc/en-us/articles/360044681474-Using-Box-for-Teams).
        /// You need Admin or Co-Admin role to
        /// use this endpoint.
        /// </summary>
        /// <param name="integrationMappingId">
        /// An ID of an integration mapping.
        /// Example: "11235432"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteTeamsIntegrationMappingById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteTeamsIntegrationMappingByIdAsync(string integrationMappingId, DeleteTeamsIntegrationMappingByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteTeamsIntegrationMappingByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/integration_mappings/teams/", StringUtils.ToStringRepresentation(integrationMappingId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}