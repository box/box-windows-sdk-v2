using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IArchivesManager {
        /// <summary>
    /// Retrieves archives for an enterprise.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getArchivesV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of getArchivesV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ArchivesV2025R0> GetArchivesV2025R0Async(GetArchivesV2025R0QueryParams? queryParams = default, GetArchivesV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates an archive.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createArchiveV2025R0 method
    /// </param>
    /// <param name="headers">
    /// Headers of createArchiveV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ArchiveV2025R0> CreateArchiveV2025R0Async(CreateArchiveV2025R0RequestBody requestBody, CreateArchiveV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Permanently deletes an archive.
    /// </summary>
    /// <param name="archiveId">
    /// The ID of the archive.
    /// Example: "982312"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteArchiveByIdV2025R0 method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteArchiveByIdV2025R0Async(string archiveId, DeleteArchiveByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}