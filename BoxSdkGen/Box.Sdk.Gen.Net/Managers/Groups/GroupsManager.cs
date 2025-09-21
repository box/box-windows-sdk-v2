using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GroupsManager : IGroupsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public GroupsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves all of the groups for a given enterprise. The user
        /// must have admin permissions to inspect enterprise's groups.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getGroups method
        /// </param>
        /// <param name="headers">
        /// Headers of getGroups method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<Groups> GetGroupsAsync(GetGroupsQueryParams? queryParams = default, GetGroupsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetGroupsQueryParams();
            headers = headers ?? new GetGroupsHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "filter_term", StringUtils.ToStringRepresentation(queryParams.FilterTerm) }, { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) }, { "offset", StringUtils.ToStringRepresentation(queryParams.Offset) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/groups"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<Groups>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a new group of users in an enterprise. Only users with admin
        /// permissions can create new groups.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createGroup method
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of createGroup method
        /// </param>
        /// <param name="headers">
        /// Headers of createGroup method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<GroupFull> CreateGroupAsync(CreateGroupRequestBody requestBody, CreateGroupQueryParams? queryParams = default, CreateGroupHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new CreateGroupQueryParams();
            headers = headers ?? new CreateGroupHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/groups"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<GroupFull>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Retrieves information about a group. Only members of this
        /// group or users with admin-level permissions will be able to
        /// use this API.
        /// </summary>
        /// <param name="groupId">
        /// The ID of the group.
        /// Example: "57645"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getGroupById method
        /// </param>
        /// <param name="headers">
        /// Headers of getGroupById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<GroupFull> GetGroupByIdAsync(string groupId, GetGroupByIdQueryParams? queryParams = default, GetGroupByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetGroupByIdQueryParams();
            headers = headers ?? new GetGroupByIdHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/groups/", StringUtils.ToStringRepresentation(groupId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<GroupFull>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates a specific group. Only admins of this
        /// group or users with admin-level permissions will be able to
        /// use this API.
        /// </summary>
        /// <param name="groupId">
        /// The ID of the group.
        /// Example: "57645"
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateGroupById method
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of updateGroupById method
        /// </param>
        /// <param name="headers">
        /// Headers of updateGroupById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<GroupFull> UpdateGroupByIdAsync(string groupId, UpdateGroupByIdRequestBody? requestBody = default, UpdateGroupByIdQueryParams? queryParams = default, UpdateGroupByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            requestBody = requestBody ?? new UpdateGroupByIdRequestBody();
            queryParams = queryParams ?? new UpdateGroupByIdQueryParams();
            headers = headers ?? new UpdateGroupByIdHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/groups/", StringUtils.ToStringRepresentation(groupId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<GroupFull>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Permanently deletes a group. Only users with
        /// admin-level permissions will be able to use this API.
        /// </summary>
        /// <param name="groupId">
        /// The ID of the group.
        /// Example: "57645"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteGroupById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteGroupByIdAsync(string groupId, DeleteGroupByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteGroupByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/groups/", StringUtils.ToStringRepresentation(groupId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}