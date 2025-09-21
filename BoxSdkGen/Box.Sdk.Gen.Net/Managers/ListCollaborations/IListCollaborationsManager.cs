using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IListCollaborationsManager {
        /// <summary>
    /// Retrieves a list of pending and active collaborations for a
    /// file. This returns all the users that have access to the file
    /// or have been invited to the file.
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
    /// Query parameters of getFileCollaborations method
    /// </param>
    /// <param name="headers">
    /// Headers of getFileCollaborations method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Collaborations> GetFileCollaborationsAsync(string fileId, GetFileCollaborationsQueryParams? queryParams = default, GetFileCollaborationsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a list of pending and active collaborations for a
    /// folder. This returns all the users that have access to the folder
    /// or have been invited to the folder.
    /// </summary>
    /// <param name="folderId">
    /// The unique identifier that represent a folder.
    /// 
    /// The ID for any folder can be determined
    /// by visiting this folder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/folder/123`
    /// the `folder_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getFolderCollaborations method
    /// </param>
    /// <param name="headers">
    /// Headers of getFolderCollaborations method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Collaborations> GetFolderCollaborationsAsync(string folderId, GetFolderCollaborationsQueryParams? queryParams = default, GetFolderCollaborationsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves all pending collaboration invites for this user.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getCollaborations method
    /// </param>
    /// <param name="headers">
    /// Headers of getCollaborations method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<CollaborationsOffsetPaginated> GetCollaborationsAsync(GetCollaborationsQueryParams queryParams, GetCollaborationsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves all the collaborations for a group. The user
    /// must have admin permissions to inspect enterprise's groups.
    /// 
    /// Each collaboration object has details on which files or
    /// folders the group has access to and with what role.
    /// </summary>
    /// <param name="groupId">
    /// The ID of the group.
    /// Example: "57645"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getGroupCollaborations method
    /// </param>
    /// <param name="headers">
    /// Headers of getGroupCollaborations method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<CollaborationsOffsetPaginated> GetGroupCollaborationsAsync(string groupId, GetGroupCollaborationsQueryParams? queryParams = default, GetGroupCollaborationsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}