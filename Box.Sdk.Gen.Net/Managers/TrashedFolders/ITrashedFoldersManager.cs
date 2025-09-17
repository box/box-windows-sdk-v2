using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ITrashedFoldersManager {
        /// <summary>
    /// Restores a folder that has been moved to the trash.
    /// 
    /// An optional new parent ID can be provided to restore the folder to in case the
    /// original folder has been deleted.
    /// 
    /// During this operation, part of the file tree will be locked, mainly
    /// the source folder and all of its descendants, as well as the destination
    /// folder.
    /// 
    /// For the duration of the operation, no other move, copy, delete, or restore
    /// operation can performed on any of the locked folders.
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
    /// Request body of restoreFolderFromTrash method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of restoreFolderFromTrash method
    /// </param>
    /// <param name="headers">
    /// Headers of restoreFolderFromTrash method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TrashFolderRestored> RestoreFolderFromTrashAsync(string folderId, RestoreFolderFromTrashRequestBody? requestBody = default, RestoreFolderFromTrashQueryParams? queryParams = default, RestoreFolderFromTrashHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a folder that has been moved to the trash.
    /// 
    /// Please note that only if the folder itself has been moved to the
    /// trash can it be retrieved with this API call. If instead one of
    /// its parent folders was moved to the trash, only that folder
    /// can be inspected using the
    /// [`GET /folders/:id/trash`](e://get_folders_id_trash) API.
    /// 
    /// To list all items that have been moved to the trash, please
    /// use the [`GET /folders/trash/items`](e://get-folders-trash-items/)
    /// API.
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
    /// Query parameters of getTrashedFolderById method
    /// </param>
    /// <param name="headers">
    /// Headers of getTrashedFolderById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TrashFolder> GetTrashedFolderByIdAsync(string folderId, GetTrashedFolderByIdQueryParams? queryParams = default, GetTrashedFolderByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Permanently deletes a folder that is in the trash.
    /// This action cannot be undone.
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
    /// Headers of deleteTrashedFolderById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteTrashedFolderByIdAsync(string folderId, DeleteTrashedFolderByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}