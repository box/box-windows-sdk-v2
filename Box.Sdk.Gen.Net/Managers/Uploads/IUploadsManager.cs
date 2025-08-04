using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IUploadsManager {
        /// <summary>
    /// Update a file's content. For file sizes over 50MB we recommend
    /// using the Chunk Upload APIs.
    /// 
    /// The `attributes` part of the body must come **before** the
    /// `file` part. Requests that do not follow this format when
    /// uploading the file will receive a HTTP `400` error with a
    /// `metadata_after_file_contents` error code.
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
    /// Request body of uploadFileVersion method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of uploadFileVersion method
    /// </param>
    /// <param name="headers">
    /// Headers of uploadFileVersion method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Files> UploadFileVersionAsync(string fileId, UploadFileVersionRequestBody requestBody, UploadFileVersionQueryParams? queryParams = default, UploadFileVersionHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Performs a check to verify that a file will be accepted by Box
    /// before you upload the entire file.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of preflightFileUploadCheck method
    /// </param>
    /// <param name="headers">
    /// Headers of preflightFileUploadCheck method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UploadUrl> PreflightFileUploadCheckAsync(PreflightFileUploadCheckRequestBody? requestBody = default, PreflightFileUploadCheckHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Uploads a small file to Box. For file sizes over 50MB we recommend
    /// using the Chunk Upload APIs.
    /// 
    /// The `attributes` part of the body must come **before** the
    /// `file` part. Requests that do not follow this format when
    /// uploading the file will receive a HTTP `400` error with a
    /// `metadata_after_file_contents` error code.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of uploadFile method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of uploadFile method
    /// </param>
    /// <param name="headers">
    /// Headers of uploadFile method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Files> UploadFileAsync(UploadFileRequestBody requestBody, UploadFileQueryParams? queryParams = default, UploadFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    ///  Upload a file with a preflight check
    /// </summary>
    /// <param name="requestBody">
    /// 
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of uploadFile method
    /// </param>
    /// <param name="headers">
    /// Headers of uploadFile method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Files> UploadWithPreflightCheckAsync(UploadWithPreflightCheckRequestBody requestBody, UploadWithPreflightCheckQueryParams? queryParams = default, UploadWithPreflightCheckHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}