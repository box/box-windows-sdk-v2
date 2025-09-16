using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class ClassificationsManager : IClassificationsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public ClassificationsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves the classification metadata template and lists all the
        /// classifications available to this enterprise.
        /// 
        /// This API can also be called by including the enterprise ID in the
        /// URL explicitly, for example
        /// `/metadata_templates/enterprise_12345/securityClassification-6VMVochwUWo/schema`.
        /// </summary>
        /// <param name="headers">
        /// Headers of getClassificationTemplate method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ClassificationTemplate> GetClassificationTemplateAsync(GetClassificationTemplateHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetClassificationTemplateHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_templates/enterprise/securityClassification-6VMVochwUWo/schema"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ClassificationTemplate>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Adds one or more new classifications to the list of classifications
        /// available to the enterprise.
        /// 
        /// This API can also be called by including the enterprise ID in the
        /// URL explicitly, for example
        /// `/metadata_templates/enterprise_12345/securityClassification-6VMVochwUWo/schema`.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of addClassification method
        /// </param>
        /// <param name="headers">
        /// Headers of addClassification method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ClassificationTemplate> AddClassificationAsync(IReadOnlyList<AddClassificationRequestBody> requestBody, AddClassificationHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new AddClassificationHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_templates/enterprise/securityClassification-6VMVochwUWo/schema#add"), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ClassificationTemplate>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates the labels and descriptions of one or more classifications
        /// available to the enterprise.
        /// 
        /// This API can also be called by including the enterprise ID in the
        /// URL explicitly, for example
        /// `/metadata_templates/enterprise_12345/securityClassification-6VMVochwUWo/schema`.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of updateClassification method
        /// </param>
        /// <param name="headers">
        /// Headers of updateClassification method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ClassificationTemplate> UpdateClassificationAsync(IReadOnlyList<UpdateClassificationRequestBody> requestBody, UpdateClassificationHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new UpdateClassificationHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_templates/enterprise/securityClassification-6VMVochwUWo/schema#update"), method: "PUT", contentType: "application/json-patch+json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ClassificationTemplate>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// When an enterprise does not yet have any classifications, this API call
        /// initializes the classification template with an initial set of
        /// classifications.
        /// 
        /// If an enterprise already has a classification, the template will already
        /// exist and instead an API call should be made to add additional
        /// classifications.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createClassificationTemplate method
        /// </param>
        /// <param name="headers">
        /// Headers of createClassificationTemplate method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ClassificationTemplate> CreateClassificationTemplateAsync(CreateClassificationTemplateRequestBody requestBody, CreateClassificationTemplateHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateClassificationTemplateHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_templates/schema#classifications"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ClassificationTemplate>(NullableUtils.Unwrap(response.Data));
        }

    }
}