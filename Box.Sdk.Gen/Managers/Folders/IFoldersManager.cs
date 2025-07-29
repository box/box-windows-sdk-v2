using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IFoldersManager {
        /// <summary>
    /// Retrieves details for a folder, including the first 100 entries
    /// in the folder.
    /// 
    /// Passing `sort`, `direction`, `offset`, and `limit`
    /// parameters in query allows you to manage the
    /// list of returned
    /// [folder items](r://folder--full#param-item-collection).
    /// 
    /// To fetch more items within the folder, use the
    /// [Get items in a folder](e://get-folders-id-items) endpoint.
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
    /// <param name="queryParams">
    /// Query parameters of getFolderById method
    /// </param>
    /// <param name="headers">
    /// Headers of getFolderById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FolderFull> GetFolderByIdAsync(string folderId, GetFolderByIdQueryParams? queryParams = default, GetFolderByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a folder. This can be also be used to move the folder,
    /// create shared links, update collaborations, and more.
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
    /// <param name="requestBody">
    /// Request body of updateFolderById method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of updateFolderById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateFolderById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FolderFull> UpdateFolderByIdAsync(string folderId, UpdateFolderByIdRequestBody? requestBody = default, UpdateFolderByIdQueryParams? queryParams = default, UpdateFolderByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a folder, either permanently or by moving it to
    /// the trash.
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
    /// <param name="queryParams">
    /// Query parameters of deleteFolderById method
    /// </param>
    /// <param name="headers">
    /// Headers of deleteFolderById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteFolderByIdAsync(string folderId, DeleteFolderByIdQueryParams? queryParams = default, DeleteFolderByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a page of items in a folder. These items can be files,
    /// folders, and web links.
    /// 
    /// To request more information about the folder itself, like its size,
    /// use the [Get a folder](#get-folders-id) endpoint instead.
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
    /// <param name="queryParams">
    /// Query parameters of getFolderItems method
    /// </param>
    /// <param name="headers">
    /// Headers of getFolderItems method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Items> GetFolderItemsAsync(string folderId, GetFolderItemsQueryParams? queryParams = default, GetFolderItemsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a new empty folder within the specified parent folder.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createFolder method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of createFolder method
    /// </param>
    /// <param name="headers">
    /// Headers of createFolder method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FolderFull> CreateFolderAsync(CreateFolderRequestBody requestBody, CreateFolderQueryParams? queryParams = default, CreateFolderHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a copy of a folder within a destination folder.
    /// 
    /// The original folder will not be changed.
    /// </summary>
    /// <param name="folderId">
    /// The unique identifier of the folder to copy.
    /// 
    /// The ID for any folder can be determined
    /// by visiting this folder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/folder/123`
    /// the `folder_id` is `123`.
    /// 
    /// The root folder with the ID `0` can not be copied.
    /// Example: "0"
    /// </param>
    /// <param name="requestBody">
    /// Request body of copyFolder method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of copyFolder method
    /// </param>
    /// <param name="headers">
    /// Headers of copyFolder method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FolderFull> CopyFolderAsync(string folderId, CopyFolderRequestBody requestBody, CopyFolderQueryParams? queryParams = default, CopyFolderHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}