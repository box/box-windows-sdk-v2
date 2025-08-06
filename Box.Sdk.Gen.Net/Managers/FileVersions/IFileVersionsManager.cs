using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IFileVersionsManager {
        /// <summary>
    /// Retrieve a list of the past versions for a file.
    /// 
    /// Versions are only tracked by Box users with premium accounts. To fetch the ID
    /// of the current version of a file, use the `GET /file/:id` API.
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
    /// <param name="queryParams">
    /// Query parameters of getFileVersions method
    /// </param>
    /// <param name="headers">
    /// Headers of getFileVersions method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileVersions> GetFileVersionsAsync(string fileId, GetFileVersionsQueryParams? queryParams = default, GetFileVersionsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieve a specific version of a file.
    /// 
    /// Versions are only tracked for Box users with premium accounts.
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
    /// <param name="fileVersionId">
    /// The ID of the file version.
    /// Example: "1234"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getFileVersionById method
    /// </param>
    /// <param name="headers">
    /// Headers of getFileVersionById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileVersionFull> GetFileVersionByIdAsync(string fileId, string fileVersionId, GetFileVersionByIdQueryParams? queryParams = default, GetFileVersionByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Move a file version to the trash.
    /// 
    /// Versions are only tracked for Box users with premium accounts.
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
    /// <param name="fileVersionId">
    /// The ID of the file version.
    /// Example: "1234"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteFileVersionById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteFileVersionByIdAsync(string fileId, string fileVersionId, DeleteFileVersionByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Restores a specific version of a file after it was deleted.
    /// Don't use this endpoint to restore Box Notes,
    /// as it works with file formats such as PDF, DOC,
    /// PPTX or similar.
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
    /// <param name="fileVersionId">
    /// The ID of the file version.
    /// Example: "1234"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateFileVersionById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateFileVersionById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileVersionFull> UpdateFileVersionByIdAsync(string fileId, string fileVersionId, UpdateFileVersionByIdRequestBody? requestBody = default, UpdateFileVersionByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Promote a specific version of a file.
    /// 
    /// If previous versions exist, this method can be used to
    /// promote one of the older versions to the top of the version history.
    /// 
    /// This creates a new copy of the old version and puts it at the
    /// top of the versions history. The file will have the exact same contents
    /// as the older version, with the the same hash digest, `etag`, and
    /// name as the original.
    /// 
    /// Other properties such as comments do not get updated to their
    /// former values.
    /// 
    /// Don't use this endpoint to restore Box Notes,
    /// as it works with file formats such as PDF, DOC,
    /// PPTX or similar.
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
    /// Request body of promoteFileVersion method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of promoteFileVersion method
    /// </param>
    /// <param name="headers">
    /// Headers of promoteFileVersion method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileVersionFull> PromoteFileVersionAsync(string fileId, PromoteFileVersionRequestBody? requestBody = default, PromoteFileVersionQueryParams? queryParams = default, PromoteFileVersionHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}