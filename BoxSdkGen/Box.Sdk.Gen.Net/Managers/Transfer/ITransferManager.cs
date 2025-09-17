using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ITransferManager {
        /// <summary>
    /// Move all of the items (files, folders and workflows) owned by a user into
    /// another user's account
    /// 
    /// Only the root folder (`0`) can be transferred.
    /// 
    /// Folders can only be moved across users by users with administrative
    /// permissions.
    /// 
    /// All existing shared links and folder-level collaborations are transferred
    /// during the operation. Please note that while collaborations at the individual
    /// file-level are transferred during the operation, the collaborations are
    /// deleted when the original user is deleted.
    /// 
    /// If the user has a large number of items across all folders, the call will
    /// be run asynchronously. If the operation is not completed within 10 minutes,
    /// the user will receive a 200 OK response, and the operation will continue running.
    /// 
    /// If the destination path has a metadata cascade policy attached to any of
    /// the parent folders, a metadata cascade operation will be kicked off
    /// asynchronously.
    /// 
    /// There is currently no way to check for when this operation is finished.
    /// 
    /// The destination folder's name will be in the format `{User}'s Files and
    /// Folders`, where `{User}` is the display name of the user.
    /// 
    /// To make this API call your application will need to have the "Read and write
    /// all files and folders stored in Box" scope enabled.
    /// 
    /// Please make sure the destination user has access to `Relay` or `Relay Lite`,
    /// and has access to the files and folders involved in the workflows being
    /// transferred.
    /// 
    /// Admins will receive an email when the operation is completed.
    /// </summary>
    /// <param name="userId">
    /// The ID of the user.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of transferOwnedFolder method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of transferOwnedFolder method
    /// </param>
    /// <param name="headers">
    /// Headers of transferOwnedFolder method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FolderFull> TransferOwnedFolderAsync(string userId, TransferOwnedFolderRequestBody requestBody, TransferOwnedFolderQueryParams? queryParams = default, TransferOwnedFolderHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}