using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IFolderMetadataManager {
        /// <summary>
    /// Retrieves all metadata for a given folder. This can not be used on the root
    /// folder with ID `0`.
    /// </summary>
    /// <param name="folderId">
    /// The unique identifier that represent a folder.
    /// 
    /// The ID for any folder can be determined
    /// by visiting this folder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/folder/123`
    /// the `folder_id` is `123`.
    /// 
    /// The root folder of a Box account is
    /// always represented by the ID `0`.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of getFolderMetadata method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Metadatas> GetFolderMetadataAsync(string folderId, GetFolderMetadataHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves the instance of a metadata template that has been applied to a
    /// folder. This can not be used on the root folder with ID `0`.
    /// </summary>
    /// <param name="folderId">
    /// The unique identifier that represent a folder.
    /// 
    /// The ID for any folder can be determined
    /// by visiting this folder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/folder/123`
    /// the `folder_id` is `123`.
    /// 
    /// The root folder of a Box account is
    /// always represented by the ID `0`.
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
    /// Headers of getFolderMetadataById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataFull> GetFolderMetadataByIdAsync(string folderId, GetFolderMetadataByIdScope scope, string templateKey, GetFolderMetadataByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Applies an instance of a metadata template to a folder.
    /// 
    /// In most cases only values that are present in the metadata template
    /// will be accepted, except for the `global.properties` template which accepts
    /// any key-value pair.
    /// 
    /// To display the metadata template in the Box web app the enterprise needs to be
    /// configured to enable **Cascading Folder Level Metadata** for the user in the
    /// admin console.
    /// </summary>
    /// <param name="folderId">
    /// The unique identifier that represent a folder.
    /// 
    /// The ID for any folder can be determined
    /// by visiting this folder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/folder/123`
    /// the `folder_id` is `123`.
    /// 
    /// The root folder of a Box account is
    /// always represented by the ID `0`.
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
    /// Request body of createFolderMetadataById method
    /// </param>
    /// <param name="headers">
    /// Headers of createFolderMetadataById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataFull> CreateFolderMetadataByIdAsync(string folderId, CreateFolderMetadataByIdScope scope, string templateKey, Dictionary<string, object> requestBody, CreateFolderMetadataByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a piece of metadata on a folder.
    /// 
    /// The metadata instance can only be updated if the template has already been
    /// applied to the folder before. When editing metadata, only values that match
    /// the metadata template schema will be accepted.
    /// 
    /// The update is applied atomically. If any errors occur during the
    /// application of the operations, the metadata instance will not be changed.
    /// </summary>
    /// <param name="folderId">
    /// The unique identifier that represent a folder.
    /// 
    /// The ID for any folder can be determined
    /// by visiting this folder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/folder/123`
    /// the `folder_id` is `123`.
    /// 
    /// The root folder of a Box account is
    /// always represented by the ID `0`.
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
    /// Request body of updateFolderMetadataById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateFolderMetadataById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataFull> UpdateFolderMetadataByIdAsync(string folderId, UpdateFolderMetadataByIdScope scope, string templateKey, IReadOnlyList<UpdateFolderMetadataByIdRequestBody> requestBody, UpdateFolderMetadataByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a piece of folder metadata.
    /// </summary>
    /// <param name="folderId">
    /// The unique identifier that represent a folder.
    /// 
    /// The ID for any folder can be determined
    /// by visiting this folder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/folder/123`
    /// the `folder_id` is `123`.
    /// 
    /// The root folder of a Box account is
    /// always represented by the ID `0`.
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
    /// Headers of deleteFolderMetadataById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteFolderMetadataByIdAsync(string folderId, DeleteFolderMetadataByIdScope scope, string templateKey, DeleteFolderMetadataByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}