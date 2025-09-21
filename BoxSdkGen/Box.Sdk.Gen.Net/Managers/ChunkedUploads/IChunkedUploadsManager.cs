using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IChunkedUploadsManager {
        /// <summary>
    /// Creates an upload session for a new file.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createFileUploadSession method
    /// </param>
    /// <param name="headers">
    /// Headers of createFileUploadSession method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UploadSession> CreateFileUploadSessionAsync(CreateFileUploadSessionRequestBody requestBody, CreateFileUploadSessionHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates an upload session for an existing file.
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
    /// Request body of createFileUploadSessionForExistingFile method
    /// </param>
    /// <param name="headers">
    /// Headers of createFileUploadSessionForExistingFile method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UploadSession> CreateFileUploadSessionForExistingFileAsync(string fileId, CreateFileUploadSessionForExistingFileRequestBody requestBody, CreateFileUploadSessionForExistingFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Using this method with urls provided in response when creating a new upload session is preferred to use over GetFileUploadSessionById method. 
    /// This allows to always upload your content to the closest Box data center and can significantly improve upload speed.
    ///  Return information about an upload session.
    /// 
    /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions) endpoint.
    /// </summary>
    /// <param name="url">
    /// URL of getFileUploadSessionById method
    /// </param>
    /// <param name="headers">
    /// Headers of getFileUploadSessionById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UploadSession> GetFileUploadSessionByUrlAsync(string url, GetFileUploadSessionByUrlHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Return information about an upload session.
    /// 
    /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions) endpoint.
    /// </summary>
    /// <param name="uploadSessionId">
    /// The ID of the upload session.
    /// Example: "D5E3F7A"
    /// </param>
    /// <param name="headers">
    /// Headers of getFileUploadSessionById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UploadSession> GetFileUploadSessionByIdAsync(string uploadSessionId, GetFileUploadSessionByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Using this method with urls provided in response when creating a new upload session is preferred to use over UploadFilePart method. 
    /// This allows to always upload your content to the closest Box data center and can significantly improve upload speed.
    ///  Uploads a chunk of a file for an upload session.
    /// 
    /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
    /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
    /// </summary>
    /// <param name="url">
    /// URL of uploadFilePart method
    /// </param>
    /// <param name="requestBody">
    /// Request body of uploadFilePart method
    /// </param>
    /// <param name="headers">
    /// Headers of uploadFilePart method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UploadedPart> UploadFilePartByUrlAsync(string url, System.IO.Stream requestBody, UploadFilePartByUrlHeaders headers, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Uploads a chunk of a file for an upload session.
    /// 
    /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
    /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
    /// </summary>
    /// <param name="uploadSessionId">
    /// The ID of the upload session.
    /// Example: "D5E3F7A"
    /// </param>
    /// <param name="requestBody">
    /// Request body of uploadFilePart method
    /// </param>
    /// <param name="headers">
    /// Headers of uploadFilePart method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UploadedPart> UploadFilePartAsync(string uploadSessionId, System.IO.Stream requestBody, UploadFilePartHeaders headers, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Using this method with urls provided in response when creating a new upload session is preferred to use over DeleteFileUploadSessionById method. 
    /// This allows to always upload your content to the closest Box data center and can significantly improve upload speed.
    ///  Abort an upload session and discard all data uploaded.
    /// 
    /// This cannot be reversed.
    /// 
    /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
    /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
    /// </summary>
    /// <param name="url">
    /// URL of deleteFileUploadSessionById method
    /// </param>
    /// <param name="headers">
    /// Headers of deleteFileUploadSessionById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteFileUploadSessionByUrlAsync(string url, DeleteFileUploadSessionByUrlHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Abort an upload session and discard all data uploaded.
    /// 
    /// This cannot be reversed.
    /// 
    /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
    /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
    /// </summary>
    /// <param name="uploadSessionId">
    /// The ID of the upload session.
    /// Example: "D5E3F7A"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteFileUploadSessionById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteFileUploadSessionByIdAsync(string uploadSessionId, DeleteFileUploadSessionByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Using this method with urls provided in response when creating a new upload session is preferred to use over GetFileUploadSessionParts method. 
    /// This allows to always upload your content to the closest Box data center and can significantly improve upload speed.
    ///  Return a list of the chunks uploaded to the upload session so far.
    /// 
    /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
    /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
    /// </summary>
    /// <param name="url">
    /// URL of getFileUploadSessionParts method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getFileUploadSessionParts method
    /// </param>
    /// <param name="headers">
    /// Headers of getFileUploadSessionParts method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UploadParts> GetFileUploadSessionPartsByUrlAsync(string url, GetFileUploadSessionPartsByUrlQueryParams? queryParams = default, GetFileUploadSessionPartsByUrlHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Return a list of the chunks uploaded to the upload session so far.
    /// 
    /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
    /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
    /// </summary>
    /// <param name="uploadSessionId">
    /// The ID of the upload session.
    /// Example: "D5E3F7A"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getFileUploadSessionParts method
    /// </param>
    /// <param name="headers">
    /// Headers of getFileUploadSessionParts method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<UploadParts> GetFileUploadSessionPartsAsync(string uploadSessionId, GetFileUploadSessionPartsQueryParams? queryParams = default, GetFileUploadSessionPartsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Using this method with urls provided in response when creating a new upload session is preferred to use over CreateFileUploadSessionCommit method. 
    /// This allows to always upload your content to the closest Box data center and can significantly improve upload speed.
    ///  Close an upload session and create a file from the uploaded chunks.
    /// 
    /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
    /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
    /// </summary>
    /// <param name="url">
    /// URL of createFileUploadSessionCommit method
    /// </param>
    /// <param name="requestBody">
    /// Request body of createFileUploadSessionCommit method
    /// </param>
    /// <param name="headers">
    /// Headers of createFileUploadSessionCommit method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Files?> CreateFileUploadSessionCommitByUrlAsync(string url, CreateFileUploadSessionCommitByUrlRequestBody requestBody, CreateFileUploadSessionCommitByUrlHeaders headers, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Close an upload session and create a file from the uploaded chunks.
    /// 
    /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
    /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
    /// </summary>
    /// <param name="uploadSessionId">
    /// The ID of the upload session.
    /// Example: "D5E3F7A"
    /// </param>
    /// <param name="requestBody">
    /// Request body of createFileUploadSessionCommit method
    /// </param>
    /// <param name="headers">
    /// Headers of createFileUploadSessionCommit method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Files?> CreateFileUploadSessionCommitAsync(string uploadSessionId, CreateFileUploadSessionCommitRequestBody requestBody, CreateFileUploadSessionCommitHeaders headers, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Starts the process of chunk uploading a big file. Should return a File object representing uploaded file.
    /// </summary>
    /// <param name="file">
    /// The stream of the file to upload.
    /// </param>
    /// <param name="fileName">
    /// The name of the file, which will be used for storage in Box.
    /// </param>
    /// <param name="fileSize">
    /// The total size of the file for the chunked upload in bytes.
    /// </param>
    /// <param name="parentFolderId">
    /// The ID of the folder where the file should be uploaded.
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileFull> UploadBigFileAsync(System.IO.Stream file, string fileName, long fileSize, string parentFolderId, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}