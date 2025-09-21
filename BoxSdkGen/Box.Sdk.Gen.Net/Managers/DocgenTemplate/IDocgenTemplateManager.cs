using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IDocgenTemplateManager {
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
    public System.Threading.Tasks.Task<DocGenTemplateBaseV2025R0> CreateDocgenTemplateV2025R0Async(DocGenTemplateCreateRequestV2025R0 requestBody, CreateDocgenTemplateV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

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
    public System.Threading.Tasks.Task<DocGenTemplatesV2025R0> GetDocgenTemplatesV2025R0Async(GetDocgenTemplatesV2025R0QueryParams? queryParams = default, GetDocgenTemplatesV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

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
    public System.Threading.Tasks.Task DeleteDocgenTemplateByIdV2025R0Async(string templateId, DeleteDocgenTemplateByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

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
    public System.Threading.Tasks.Task<DocGenTemplateV2025R0> GetDocgenTemplateByIdV2025R0Async(string templateId, GetDocgenTemplateByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

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
    public System.Threading.Tasks.Task<DocGenTagsV2025R0> GetDocgenTemplateTagsV2025R0Async(string templateId, GetDocgenTemplateTagsV2025R0QueryParams? queryParams = default, GetDocgenTemplateTagsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

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
    public System.Threading.Tasks.Task<DocGenJobsV2025R0> GetDocgenTemplateJobByIdV2025R0Async(string templateId, GetDocgenTemplateJobByIdV2025R0QueryParams? queryParams = default, GetDocgenTemplateJobByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}