using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class DocgenTemplateManager : IDocgenTemplateManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public DocgenTemplateManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Marks a file as a Box Doc Gen template.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createDocgenTemplateV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of createDocgenTemplateV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DocGenTemplateBaseV2025R0> CreateDocgenTemplateV2025R0Async(DocGenTemplateCreateRequestV2025R0 requestBody, CreateDocgenTemplateV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateDocgenTemplateV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/docgen_templates"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DocGenTemplateBaseV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Lists Box Doc Gen templates on which the user is a collaborator.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getDocgenTemplatesV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getDocgenTemplatesV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DocGenTemplatesV2025R0> GetDocgenTemplatesV2025R0Async(GetDocgenTemplatesV2025R0QueryParams? queryParams = default, GetDocgenTemplatesV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetDocgenTemplatesV2025R0QueryParams();
            headers = headers ?? new GetDocgenTemplatesV2025R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/docgen_templates"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DocGenTemplatesV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Unmarks file as Box Doc Gen template.
        /// </summary>
        /// <param name="templateId">
        /// ID of the file which will no longer be marked as a Box Doc Gen template.
        /// Example: "123"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteDocgenTemplateByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteDocgenTemplateByIdV2025R0Async(string templateId, DeleteDocgenTemplateByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteDocgenTemplateByIdV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/docgen_templates/", StringUtils.ToStringRepresentation(templateId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Lists details of a specific Box Doc Gen template.
        /// </summary>
        /// <param name="templateId">
        /// The ID of a Box Doc Gen template.
        /// Example: 123
        /// </param>
        /// <param name="headers">
        /// Headers of getDocgenTemplateByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DocGenTemplateV2025R0> GetDocgenTemplateByIdV2025R0Async(string templateId, GetDocgenTemplateByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetDocgenTemplateByIdV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/docgen_templates/", StringUtils.ToStringRepresentation(templateId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DocGenTemplateV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Lists all tags in a Box Doc Gen template.
        /// </summary>
        /// <param name="templateId">
        /// ID of template.
        /// Example: 123
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getDocgenTemplateTagsV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getDocgenTemplateTagsV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DocGenTagsV2025R0> GetDocgenTemplateTagsV2025R0Async(string templateId, GetDocgenTemplateTagsV2025R0QueryParams? queryParams = default, GetDocgenTemplateTagsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetDocgenTemplateTagsV2025R0QueryParams();
            headers = headers ?? new GetDocgenTemplateTagsV2025R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "template_version_id", StringUtils.ToStringRepresentation(queryParams.TemplateVersionId) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/docgen_templates/", StringUtils.ToStringRepresentation(templateId), "/tags"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DocGenTagsV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Lists the users jobs which use this template.
        /// </summary>
        /// <param name="templateId">
        /// Id of template to fetch jobs for.
        /// Example: 123
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getDocgenTemplateJobByIdV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getDocgenTemplateJobByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<DocGenJobsV2025R0> GetDocgenTemplateJobByIdV2025R0Async(string templateId, GetDocgenTemplateJobByIdV2025R0QueryParams? queryParams = default, GetDocgenTemplateJobByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetDocgenTemplateJobByIdV2025R0QueryParams();
            headers = headers ?? new GetDocgenTemplateJobByIdV2025R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/docgen_template_jobs/", StringUtils.ToStringRepresentation(templateId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<DocGenJobsV2025R0>(NullableUtils.Unwrap(response.Data));
        }

    }
}