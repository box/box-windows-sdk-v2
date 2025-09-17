using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IFileWatermarksManager {
        /// <summary>
    /// Retrieve the watermark for a file.
    /// </summary>
    /// <param name="fileId">
    /// The unique identifier that represents a file.
    /// 
    /// The ID for any file can be determined
    /// by visiting a file in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/files/123`
    /// the `file_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of getFileWatermark method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Watermark> GetFileWatermarkAsync(string fileId, GetFileWatermarkHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Applies or update a watermark on a file.
    /// </summary>
    /// <param name="fileId">
    /// The unique identifier that represents a file.
    /// 
    /// The ID for any file can be determined
    /// by visiting a file in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/files/123`
    /// the `file_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateFileWatermark method
    /// </param>
    /// <param name="headers">
    /// Headers of updateFileWatermark method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Watermark> UpdateFileWatermarkAsync(string fileId, UpdateFileWatermarkRequestBody requestBody, UpdateFileWatermarkHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Removes the watermark from a file.
    /// </summary>
    /// <param name="fileId">
    /// The unique identifier that represents a file.
    /// 
    /// The ID for any file can be determined
    /// by visiting a file in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/files/123`
    /// the `file_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteFileWatermark method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteFileWatermarkAsync(string fileId, DeleteFileWatermarkHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}