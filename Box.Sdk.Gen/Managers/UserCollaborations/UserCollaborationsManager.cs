using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UserCollaborationsManager : IUserCollaborationsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public UserCollaborationsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves a single collaboration.
        /// </summary>
        /// <param name="collaborationId">
        /// The ID of the collaboration.
        /// Example: "1234"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getCollaborationById method
        /// </param>
        /// <param name="headers">
        /// Headers of getCollaborationById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<Collaboration> GetCollaborationByIdAsync(string collaborationId, GetCollaborationByIdQueryParams? queryParams = default, GetCollaborationByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetCollaborationByIdQueryParams();
            headers = headers ?? new GetCollaborationByIdHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/collaborations/", StringUtils.ToStringRepresentation(collaborationId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<Collaboration>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates a collaboration.
        /// Can be used to change the owner of an item, or to
        /// accept collaboration invites.
        /// </summary>
        /// <param name="collaborationId">
        /// The ID of the collaboration.
        /// Example: "1234"
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateCollaborationById method
        /// </param>
        /// <param name="headers">
        /// Headers of updateCollaborationById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<Collaboration?> UpdateCollaborationByIdAsync(string collaborationId, UpdateCollaborationByIdRequestBody requestBody, UpdateCollaborationByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new UpdateCollaborationByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/collaborations/", StringUtils.ToStringRepresentation(collaborationId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            if (StringUtils.ToStringRepresentation(response.Status) == "204") {
                return null;
            }
            return SimpleJsonSerializer.Deserialize<Collaboration>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Deletes a single collaboration.
        /// </summary>
        /// <param name="collaborationId">
        /// The ID of the collaboration.
        /// Example: "1234"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteCollaborationById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteCollaborationByIdAsync(string collaborationId, DeleteCollaborationByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteCollaborationByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/collaborations/", StringUtils.ToStringRepresentation(collaborationId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds a collaboration for a single user or a single group to a file
        /// or folder.
        /// 
        /// Collaborations can be created using email address, user IDs, or a
        /// group IDs.
        /// 
        /// If a collaboration is being created with a group, access to
        /// this endpoint is dependent on the group's ability to be invited.
        /// 
        /// If collaboration is in `pending` status, the following fields
        /// are redacted:
        /// - `login` and `name` are hidden if a collaboration was created
        /// using `user_id`,
        /// -  `name` is hidden if a collaboration was created using `login`.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createCollaboration method
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of createCollaboration method
        /// </param>
        /// <param name="headers">
        /// Headers of createCollaboration method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<Collaboration> CreateCollaborationAsync(CreateCollaborationRequestBody requestBody, CreateCollaborationQueryParams? queryParams = default, CreateCollaborationHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new CreateCollaborationQueryParams();
            headers = headers ?? new CreateCollaborationHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) }, { "notify", StringUtils.ToStringRepresentation(queryParams.Notify) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/collaborations"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<Collaboration>(NullableUtils.Unwrap(response.Data));
        }

    }
}