using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IDocgenManager {
        /// <summary>
    /// Get details of the Box Doc Gen job.
    /// </summary>
    /// <param name="jobId">
    /// Box Doc Gen job ID.
    /// Example: 123
    /// </param>
    /// <param name="headers">
    /// Headers of getDocgenJobByIdV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<DocGenJobV2025R0> GetDocgenJobByIdV2025R0Async(string jobId, GetDocgenJobByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Lists all Box Doc Gen jobs for a user.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getDocgenJobsV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getDocgenJobsV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<DocGenJobsFullV2025R0> GetDocgenJobsV2025R0Async(GetDocgenJobsV2025R0QueryParams? queryParams = default, GetDocgenJobsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Lists Box Doc Gen jobs in a batch.
    /// </summary>
    /// <param name="batchId">
    /// Box Doc Gen batch ID.
    /// Example: 123
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getDocgenBatchJobByIdV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getDocgenBatchJobByIdV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<DocGenJobsV2025R0> GetDocgenBatchJobByIdV2025R0Async(string batchId, GetDocgenBatchJobByIdV2025R0QueryParams? queryParams = default, GetDocgenBatchJobByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Generates a document using a Box Doc Gen template.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createDocgenBatchV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createDocgenBatchV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<DocGenBatchBaseV2025R0> CreateDocgenBatchV2025R0Async(DocGenBatchCreateRequestV2025R0 requestBody, CreateDocgenBatchV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}