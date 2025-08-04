using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ISharedLinksFilesManager {
        /// <summary>
    /// Returns the file represented by a shared link.
    /// 
    /// A shared file can be represented by a shared link,
    /// which can originate within the current enterprise or within another.
    /// 
    /// This endpoint allows an application to retrieve information about a
    /// shared file when only given a shared link.
    /// 
    /// The `shared_link_permission_options` array field can be returned
    /// by requesting it in the `fields` query parameter.
    /// </summary>
    /// <param name="headers">
    /// Headers of findFileForSharedLink method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of findFileForSharedLink method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileFull> FindFileForSharedLinkAsync(FindFileForSharedLinkHeaders headers, FindFileForSharedLinkQueryParams? queryParams = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Gets the information for a shared link on a file.
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
    /// Query parameters of getSharedLinkForFile method
    /// </param>
    /// <param name="headers">
    /// Headers of getSharedLinkForFile method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileFull> GetSharedLinkForFileAsync(string fileId, GetSharedLinkForFileQueryParams queryParams, GetSharedLinkForFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Adds a shared link to a file.
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
    /// Query parameters of addShareLinkToFile method
    /// </param>
    /// <param name="requestBody">
    /// Request body of addShareLinkToFile method
    /// </param>
    /// <param name="headers">
    /// Headers of addShareLinkToFile method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileFull> AddShareLinkToFileAsync(string fileId, AddShareLinkToFileQueryParams queryParams, AddShareLinkToFileRequestBody? requestBody = default, AddShareLinkToFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a shared link on a file.
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
    /// Query parameters of updateSharedLinkOnFile method
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateSharedLinkOnFile method
    /// </param>
    /// <param name="headers">
    /// Headers of updateSharedLinkOnFile method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileFull> UpdateSharedLinkOnFileAsync(string fileId, UpdateSharedLinkOnFileQueryParams queryParams, UpdateSharedLinkOnFileRequestBody? requestBody = default, UpdateSharedLinkOnFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Removes a shared link from a file.
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
    /// Query parameters of removeSharedLinkFromFile method
    /// </param>
    /// <param name="requestBody">
    /// Request body of removeSharedLinkFromFile method
    /// </param>
    /// <param name="headers">
    /// Headers of removeSharedLinkFromFile method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FileFull> RemoveSharedLinkFromFileAsync(string fileId, RemoveSharedLinkFromFileQueryParams queryParams, RemoveSharedLinkFromFileRequestBody? requestBody = default, RemoveSharedLinkFromFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}