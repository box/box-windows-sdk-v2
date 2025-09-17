using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IFileRequestsManager {
        /// <summary>
    /// Retrieves the information about a file request.
    /// </summary>
    /// <param name="fileRequestId">
    /// The unique identifier that represent a file request.
    /// 
    /// The ID for any file request can be determined
    /// by visiting a file request builder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/filerequest/123`
    /// the `file_request_id` is `123`.
    /// Example: "123"
    /// </param>
    /// <param name="headers">
    /// Headers of getFileRequestById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileRequest> GetFileRequestByIdAsync(string fileRequestId, GetFileRequestByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a file request. This can be used to activate or
    /// deactivate a file request.
    /// </summary>
    /// <param name="fileRequestId">
    /// The unique identifier that represent a file request.
    /// 
    /// The ID for any file request can be determined
    /// by visiting a file request builder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/filerequest/123`
    /// the `file_request_id` is `123`.
    /// Example: "123"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateFileRequestById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateFileRequestById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileRequest> UpdateFileRequestByIdAsync(string fileRequestId, FileRequestUpdateRequest requestBody, UpdateFileRequestByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a file request permanently.
    /// </summary>
    /// <param name="fileRequestId">
    /// The unique identifier that represent a file request.
    /// 
    /// The ID for any file request can be determined
    /// by visiting a file request builder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/filerequest/123`
    /// the `file_request_id` is `123`.
    /// Example: "123"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteFileRequestById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteFileRequestByIdAsync(string fileRequestId, DeleteFileRequestByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Copies an existing file request that is already present on one folder,
    /// and applies it to another folder.
    /// </summary>
    /// <param name="fileRequestId">
    /// The unique identifier that represent a file request.
    /// 
    /// The ID for any file request can be determined
    /// by visiting a file request builder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/filerequest/123`
    /// the `file_request_id` is `123`.
    /// Example: "123"
    /// </param>
    /// <param name="requestBody">
    /// Request body of createFileRequestCopy method
    /// </param>
    /// <param name="headers">
    /// Headers of createFileRequestCopy method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileRequest> CreateFileRequestCopyAsync(string fileRequestId, FileRequestCopyRequest requestBody, CreateFileRequestCopyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}