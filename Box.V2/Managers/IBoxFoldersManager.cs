using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    public interface IBoxFoldersManager
    {
        /// <summary>
        /// Retrieves the files and/or folders contained within this folder without any other metadata about the folder. 
        /// Any attribute in the full files or folders objects can be passed in with the fields parameter to get specific attributes, 
        /// and only those specific attributes back; otherwise, the mini format is returned for each item by default.
        /// Multiple attributes can be passed in using the fields parameter. Paginated results can be 
        /// retrieved using the limit and offset parameters.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="limit">The maximum number of items to return in a page. The default is 100 and the max is 1000.</param>
        /// <param name="offset">The offset at which to begin the response. An offset of value of 0 will start at the beginning of the folder-listing. 
        /// Note: If there are hidden items in your previous response, your next offset should be = offset + limit, not the # of records you received back. 
        /// The default is 0.</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <param name="sort">The field to sort items on</param>
        /// <param name="direction">The direction to sort results in: ascending or descending</param>
        /// <param name="sharedLink">The shared link for this folder</param>
        /// <param name="sharedLinkPassword">The password for the shared link (if required)</param>
        /// <returns>A collection of items contained in the folder is returned. An error is thrown if the folder does not exist, 
        /// or if any of the parameters are invalid. The total_count returned may not match the number of entries when using enterprise scope, 
        /// because external folders are hidden the list of entries.</returns>
        Task<BoxCollection<BoxItem>> GetFolderItemsAsync(string id, int limit, int offset = 0, IEnumerable<string> fields = null, bool autoPaginate = false, string sort = null, BoxSortDirection? direction = null,
            string sharedLink = null, string sharedLinkPassword = null);

        /// <summary>
        /// Retrieves the files and/or folders contained within this folder without any other metadata about the folder.
        /// Uses marker-based pagination.
        /// Any attribute in the full files or folders objects can be passed in with the fields parameter to get specific attributes, 
        /// and only those specific attributes back; otherwise, the mini format is returned for each item by default.
        /// Multiple attributes can be passed in using the fields parameter. Paginated results can be 
        /// retrieved using the limit and marker parameters.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="limit">The maximum number of items to return in a page. The default is 100 and the max is 1000.</param>
        /// <param name="marker">Position to return results from..</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <param name="sort">The field to sort items on</param>
        /// <param name="direction">The direction to sort results in: ascending or descending</param>
        /// <param name="sharedLink">The shared link for this folder</param>
        /// <param name="sharedLinkPassword">The password for the shared link (if required)</param>
        /// <returns>A collection of items contained in the folder is returned. An error is thrown if the folder does not exist, 
        /// or if any of the parameters are invalid. The total_count returned may not match the number of entries when using enterprise scope, 
        /// because external folders are hidden the list of entries.</returns>
        Task<BoxCollectionMarkerBased<BoxItem>> GetFolderItemsMarkerBasedAsync(string id, int limit, string marker = null, IEnumerable<string> fields = null, bool autoPaginate = false, string sort = null, BoxSortDirection? direction = null,
            string sharedLink = null, string sharedLinkPassword = null);

        /// <summary>
        /// Used to create a new empty folder. The new folder will be created inside of the specified parent folder.
        /// </summary>
        /// <param name="folderRequest">BoxFolderRequest object</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>A full folder object is returned if the parent folder ID is valid and if no name collisions occur.</returns>
        Task<BoxFolder> CreateAsync(BoxFolderRequest folderRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Retrieves the full metadata about a folder, including information about when it was last updated 
        /// as well as the files and folders contained in it. To retrieve information about the root folder of a Box account use the id “0″.
        /// </summary>
        /// <param name="id">The folder id</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <param name="sharedLink">The shared link for this folder</param>
        /// <param name="sharedLinkPassword">The password for the shared link (if required)</param>
        /// <returns>A full folder object is returned, including the most current information available about it.
        /// An exception is thrown if the folder does not exist or if the user does not have access to it.</returns>
        Task<BoxFolder> GetInformationAsync(string id, IEnumerable<string> fields = null, string sharedLink = null, string sharedLinkPassword = null);

        /// <summary>
        /// Used to create a copy of a folder in another folder. The original version of the folder will not be altered.
        /// </summary>
        /// <param name="folderRequest">BoxFolderRequest object</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>A full folder object is returned if the ID is valid and if the update is successful.</returns>
        Task<BoxFolder> CopyAsync(BoxFolderRequest folderRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to delete a folder. A recursive parameter must be included in order to delete folders that have items 
        /// inside of them. An optional If-Match header can be included using the etag parameter to ensure that client only deletes the folder 
        /// if it knows about the latest version.
        /// </summary>
        /// <param name="id">Id of the folder</param>
        /// <param name="recursive">Whether to delete this folder if it has items inside of it.</param>
        /// <param name="etag">This ‘etag’ field of the folder object to set in the If-Match header</param>
        /// <returns>True will be returned upon successful deletion</returns>
        Task<bool> DeleteAsync(string id, bool recursive = false, string etag = null);

        /// <summary>
        /// Used to update information about the folder. To move a folder, update the ID of its parent. To enable an 
        /// email address that can be used to upload files to this folder, update the folder_upload_email attribute. 
        /// An optional If-Match header can be included using the etag parameter to ensure that client only updates the folder 
        /// if it knows about the latest version.
        /// </summary>
        /// <param name="folderRequest">BoxFolderRequest object</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <param name="etag">This ‘etag’ field of the folder object to set in the If-Match header</param>
        /// <param name="timeout">Optional timeout for response.</param>
        /// <returns>The updated folder is returned if the name is valid. Errors generally occur only if there is a name collision.</returns>
        Task<BoxFolder> UpdateInformationAsync(BoxFolderRequest folderRequest, IEnumerable<string> fields = null, string etag = null, TimeSpan? timeout = null);

        /// <summary>
        /// Used to create a shared link for this particular folder. In order to get default shared link status, set it to an empty access level.
        /// To delete a shared link use the DeleteSharedLinkAsync method of this class.
        /// </summary>
        /// <param name="id">Id of the folder to create shared link for</param>
        /// <param name="sharedLinkRequest">Shared link request object</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>A full folder object is returned if the ID is valid and if the shared link is created.</returns>
        Task<BoxFolder> CreateSharedLinkAsync(string id, BoxSharedLinkRequest sharedLinkRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to delete the shared link for the given folder id. 
        /// </summary>
        /// <returns>A full folder object is returned if the ID is valid and if the shared link is deleted.</returns>
        Task<BoxFolder> DeleteSharedLinkAsync(string id);

        /// <summary>
        /// Use this to get a list of all the collaborations on a folder i.e. all of the users that have access to that folder.
        /// </summary>
        /// <param name="id">Id of the folder</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>List of all the collaborations on a folder</returns>
        Task<BoxCollection<BoxCollaboration>> GetCollaborationsAsync(string id, IEnumerable<string> fields = null);

        /// <summary>
        /// Retrieves the files and/or folders that have been moved to the trash. Any attribute in the full files 
        /// or folders objects can be passed in with the fields parameter to get specific attributes, and only those 
        /// specific attributes back; otherwise, the mini format is returned for each item by default. Multiple 
        /// attributes can be passed in using the fields parameter. Paginated results can be 
        /// retrieved using the limit and offset parameters.
        /// </summary>
        /// <param name="limit">The maximum number of items to return</param>
        /// <param name="offset">The item at which to begin the response</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <param name="sort">The field to sort items on</param>
        /// <param name="direction">The direction to sort results in: ascending or descending</param>
        /// <returns>A collection of items contained in the trash is returned. An error is thrown if any of the parameters are invalid.</returns>
        Task<BoxCollection<BoxItem>> GetTrashItemsAsync(int limit, int offset = 0, IEnumerable<string> fields = null, bool autoPaginate = false, string sort = null, BoxSortDirection? direction = null);

        /// <summary>
        /// Restores an item that has been moved to the trash. Default behavior is to restore the item to the folder it was in 
        /// before it was moved to the trash. If that parent folder no longer exists or if there is now an item with the same 
        /// name in that parent folder, the new parent folder and/or new name will need to be included in the request.
        /// </summary>
        /// <param name="folderRequest">BoxFolderRequest object (specify Parent.Id if you wish to restore to a different parent)</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>The full item will be returned if success. By default it is restored to the parent folder it was in before it was trashed.</returns>
        Task<BoxFolder> RestoreTrashedFolderAsync(BoxFolderRequest folderRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Permanently deletes a folder that is in the trash. The folder will no longer exist in Box. This action cannot be undone.
        /// </summary>
        /// <param name="id">Id of the folder</param>
        /// <returns>True will be returned upon successful deletion</returns>
        Task<bool> PurgeTrashedFolderAsync(string id);

        /// <summary>
        /// Retrieves a folder that has been moved to the trash.
        /// </summary>
        /// <param name="id">Id of the folder</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>The full folder will be returned, including information about when it was moved to the trash</returns>
        Task<BoxFolder> GetTrashedFolderAsync(string id, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to retrieve the watermark for a corresponding Box folder.
        /// </summary>
        /// <param name="id">Id of the folder.</param>
        /// <returns>An object containing information about the watermark associated for this folder. If the folder does not have a watermark applied to it than return null</returns>
        Task<BoxWatermark> GetWatermarkAsync(string id);

        /// <summary>
        /// Used to apply or update the watermark for a corresponding Box folder.
        /// </summary>
        /// <param name="id">Id of the folder.</param>
        /// <param name="applyWatermarkRequest">BoxApplyWatermarkRequest object. Can be null, for using default values - imprint="default" </param>
        /// <returns>An object containing information about the watermark associated for this folder.</returns>
        Task<BoxWatermark> ApplyWatermarkAsync(string id, BoxApplyWatermarkRequest applyWatermarkRequest = null);

        /// <summary>
        /// Used to remove the watermark for a corresponding Box folder.
        /// </summary>
        /// <param name="id">Id of the folder.</param>
        /// <returns>True to confirm the watermark has been removed. If the folder did not have a watermark applied to it, than False will be returned.</returns>
        Task<bool> RemoveWatermarkAsync(string id);

        /// <summary>
        /// Creates a folder lock on a folder, preventing it from being moved and/or deleted.
        /// </summary>
        /// <param name="id">Id of the folder to create a lock on</param>
        /// <returns>An object representing the lock on the folder</returns>
        Task<BoxFolderLock> CreateLockAsync(string id);

        /// <summary>
        /// Lists all folder locks for a given folder.
        /// </summary>
        /// <param name="id">Id of the folder</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all locks. Currently only one lock can exist per folder.; defaults to false.</param>
        /// <returns>A collection of locks on the folder</returns>
        Task<BoxCollection<BoxFolderLock>> GetLocksAsync(string id, bool autoPaginate = false);

        /// <summary>
        /// Delete a folder lock on a folder
        /// </summary>
        /// <param name="id">Id of the folder lock</param>
        /// <returns>True will be returned upon successful deletionr</returns>
        Task<bool> DeleteLockAsync(string id);
    }
}
