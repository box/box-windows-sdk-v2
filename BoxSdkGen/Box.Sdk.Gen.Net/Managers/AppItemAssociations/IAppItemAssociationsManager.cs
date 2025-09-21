using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IAppItemAssociationsManager {
        /// <summary>
    /// **This is a beta feature, which means that its availability might be limited.**
    /// Returns all app items the file is associated with. This includes app items
    /// associated with ancestors of the file. Assuming the context user has access
    /// to the file, the type/ids are revealed even if the context user does not
    /// have **View** permission on the app item.
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
    /// Query parameters of getFileAppItemAssociations method
    /// </param>
    /// <param name="headers">
    /// Headers of getFileAppItemAssociations method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<AppItemAssociations> GetFileAppItemAssociationsAsync(string fileId, GetFileAppItemAssociationsQueryParams? queryParams = default, GetFileAppItemAssociationsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// **This is a beta feature, which means that its availability might be limited.**
    /// Returns all app items the folder is associated with. This includes app items
    /// associated with ancestors of the folder. Assuming the context user has access
    /// to the folder, the type/ids are revealed even if the context user does not
    /// have **View** permission on the app item.
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
    /// Query parameters of getFolderAppItemAssociations method
    /// </param>
    /// <param name="headers">
    /// Headers of getFolderAppItemAssociations method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<AppItemAssociations> GetFolderAppItemAssociationsAsync(string folderId, GetFolderAppItemAssociationsQueryParams? queryParams = default, GetFolderAppItemAssociationsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}