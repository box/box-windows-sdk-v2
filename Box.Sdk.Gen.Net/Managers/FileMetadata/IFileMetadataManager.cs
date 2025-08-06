using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IFileMetadataManager {
        /// <summary>
    /// Retrieves all metadata for a given file.
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
    /// Headers of getFileMetadata method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Metadatas> GetFileMetadataAsync(string fileId, GetFileMetadataHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves the instance of a metadata template that has been applied to a
    /// file.
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
    /// <param name="scope">
    /// The scope of the metadata template.
    /// Example: "global"
    /// </param>
    /// <param name="templateKey">
    /// The name of the metadata template.
    /// Example: "properties"
    /// </param>
    /// <param name="headers">
    /// Headers of getFileMetadataById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataFull> GetFileMetadataByIdAsync(string fileId, GetFileMetadataByIdScope scope, string templateKey, GetFileMetadataByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Applies an instance of a metadata template to a file.
    /// 
    /// In most cases only values that are present in the metadata template
    /// will be accepted, except for the `global.properties` template which accepts
    /// any key-value pair.
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
    /// <param name="scope">
    /// The scope of the metadata template.
    /// Example: "global"
    /// </param>
    /// <param name="templateKey">
    /// The name of the metadata template.
    /// Example: "properties"
    /// </param>
    /// <param name="requestBody">
    /// Request body of createFileMetadataById method
    /// </param>
    /// <param name="headers">
    /// Headers of createFileMetadataById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataFull> CreateFileMetadataByIdAsync(string fileId, CreateFileMetadataByIdScope scope, string templateKey, Dictionary<string, object> requestBody, CreateFileMetadataByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a piece of metadata on a file.
    /// 
    /// The metadata instance can only be updated if the template has already been
    /// applied to the file before. When editing metadata, only values that match
    /// the metadata template schema will be accepted.
    /// 
    /// The update is applied atomically. If any errors occur during the
    /// application of the operations, the metadata instance will not be changed.
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
    /// <param name="scope">
    /// The scope of the metadata template.
    /// Example: "global"
    /// </param>
    /// <param name="templateKey">
    /// The name of the metadata template.
    /// Example: "properties"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateFileMetadataById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateFileMetadataById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataFull> UpdateFileMetadataByIdAsync(string fileId, UpdateFileMetadataByIdScope scope, string templateKey, IReadOnlyList<UpdateFileMetadataByIdRequestBody> requestBody, UpdateFileMetadataByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a piece of file metadata.
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
    /// <param name="scope">
    /// The scope of the metadata template.
    /// Example: "global"
    /// </param>
    /// <param name="templateKey">
    /// The name of the metadata template.
    /// Example: "properties"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteFileMetadataById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteFileMetadataByIdAsync(string fileId, DeleteFileMetadataByIdScope scope, string templateKey, DeleteFileMetadataByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}