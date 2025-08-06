using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IFolderLocksManager {
        /// <summary>
    /// Retrieves folder lock details for a given folder.
    /// 
    /// You must be authenticated as the owner or co-owner of the folder to
    /// use this endpoint.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getFolderLocks method
    /// </param>
    /// <param name="headers">
    /// Headers of getFolderLocks method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FolderLocks> GetFolderLocksAsync(GetFolderLocksQueryParams queryParams, GetFolderLocksHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a folder lock on a folder, preventing it from being moved and/or
    /// deleted.
    /// 
    /// You must be authenticated as the owner or co-owner of the folder to
    /// use this endpoint.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createFolderLock method
    /// </param>
    /// <param name="headers">
    /// Headers of createFolderLock method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FolderLock> CreateFolderLockAsync(CreateFolderLockRequestBody requestBody, CreateFolderLockHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a folder lock on a given folder.
    /// 
    /// You must be authenticated as the owner or co-owner of the folder to
    /// use this endpoint.
    /// </summary>
    /// <param name="folderLockId">
    /// The ID of the folder lock.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteFolderLockById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteFolderLockByIdAsync(string folderLockId, DeleteFolderLockByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}