using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ISharedLinksFoldersManager {
        /// <summary>
    /// Return the folder represented by a shared link.
    /// 
    /// A shared folder can be represented by a shared link,
    /// which can originate within the current enterprise or within another.
    /// 
    /// This endpoint allows an application to retrieve information about a
    /// shared folder when only given a shared link.
    /// </summary>
    /// <param name="headers">
    /// Headers of findFolderForSharedLink method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of findFolderForSharedLink method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FolderFull> FindFolderForSharedLinkAsync(FindFolderForSharedLinkHeaders headers, FindFolderForSharedLinkQueryParams? queryParams = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Gets the information for a shared link on a folder.
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
    /// Query parameters of getSharedLinkForFolder method
    /// </param>
    /// <param name="headers">
    /// Headers of getSharedLinkForFolder method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FolderFull> GetSharedLinkForFolderAsync(string folderId, GetSharedLinkForFolderQueryParams queryParams, GetSharedLinkForFolderHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Adds a shared link to a folder.
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
    /// Query parameters of addShareLinkToFolder method
    /// </param>
    /// <param name="requestBody">
    /// Request body of addShareLinkToFolder method
    /// </param>
    /// <param name="headers">
    /// Headers of addShareLinkToFolder method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FolderFull> AddShareLinkToFolderAsync(string folderId, AddShareLinkToFolderQueryParams queryParams, AddShareLinkToFolderRequestBody? requestBody = default, AddShareLinkToFolderHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a shared link on a folder.
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
    /// Query parameters of updateSharedLinkOnFolder method
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateSharedLinkOnFolder method
    /// </param>
    /// <param name="headers">
    /// Headers of updateSharedLinkOnFolder method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FolderFull> UpdateSharedLinkOnFolderAsync(string folderId, UpdateSharedLinkOnFolderQueryParams queryParams, UpdateSharedLinkOnFolderRequestBody? requestBody = default, UpdateSharedLinkOnFolderHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Removes a shared link from a folder.
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
    /// Query parameters of removeSharedLinkFromFolder method
    /// </param>
    /// <param name="requestBody">
    /// Request body of removeSharedLinkFromFolder method
    /// </param>
    /// <param name="headers">
    /// Headers of removeSharedLinkFromFolder method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FolderFull> RemoveSharedLinkFromFolderAsync(string folderId, RemoveSharedLinkFromFolderQueryParams queryParams, RemoveSharedLinkFromFolderRequestBody? requestBody = default, RemoveSharedLinkFromFolderHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}