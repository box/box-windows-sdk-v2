using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    public class BoxFoldersManager : BoxResourceManager
    {
        public BoxFoldersManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Retrieves the files and/or folders contained in the provided folder id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        [Obsolete("This endpoint is not officially supported by the API and is not guaranteed to be available in the next version. Please use GetFolderItemsAsync")]
        public async Task<BoxFolder> GetItemsAsync(string id, int limit, int offset = 0, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, id)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString())
                .Param(ParamFields, fields);

            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

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
        /// <returns>A collection of items contained in the folder is returned. An error is thrown if the folder does not exist, 
        /// or if any of the parameters are invalid. The total_count returned may not match the number of entries when using enterprise scope, 
        /// because external folders are hidden the list of entries.</returns>
        public async Task<BoxCollection<BoxItem>> GetFolderItemsAsync(string id, int limit, int offset = 0, List<string> fields = null, bool autoPaginate=false)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, string.Format(Constants.ItemsPathString, id))
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString())
                .Param(ParamFields, fields);

            if (autoPaginate)
            {
                return await AutoPaginateLimitOffset<BoxItem>(request, limit);
            }
            else
            {
                IBoxResponse<BoxCollection<BoxItem>> response = await ToResponseAsync<BoxCollection<BoxItem>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }   
        }

        /// <summary>
        /// Used to create a new empty folder. The new folder will be created inside of the specified parent folder.
        /// </summary>
        /// <param name="folderRequest">BoxFolderRequest object</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>A full folder object is returned if the parent folder ID is valid and if no name collisions occur.</returns>
        public async Task<BoxFolder> CreateAsync(BoxFolderRequest folderRequest, List<string> fields = null)
        {
            folderRequest.ThrowIfNull("folderRequest")
                .Name.ThrowIfNullOrWhiteSpace("folderRequest.Name");
            folderRequest.Parent.ThrowIfNull("folderRequest.Parent")
                .Id.ThrowIfNullOrWhiteSpace("folderRequest.Parent.Id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri)
                .Method(RequestMethod.Post)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize<BoxFolderRequest>(folderRequest));

            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves the full metadata about a folder, including information about when it was last updated 
        /// as well as the files and folders contained in it. To retrieve information about the root folder of a Box account use the id “0″.
        /// </summary>
        /// <param name="id">The folder id</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>A full folder object is returned, including the most current information available about it. 
        /// An exception is thrown if the folder does not exist or if the user does not have access to it.</returns>
        public async Task<BoxFolder> GetInformationAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, id)
                .Method(RequestMethod.Get)
                .Param(ParamFields, fields);

            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to create a copy of a folder in another folder. The original version of the folder will not be altered.
        /// </summary>
        /// <param name="folderRequest">BoxFolderRequest object</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>A full folder object is returned if the ID is valid and if the update is successful.</returns>
        public async Task<BoxFolder> CopyAsync(BoxFolderRequest folderRequest, List<string> fields = null)
        {
            folderRequest.ThrowIfNull("folderRequest")
                .Id.ThrowIfNullOrWhiteSpace("folderRequest.Id");
            folderRequest.Parent.ThrowIfNull("folderRequest.Parent")
                .Id.ThrowIfNullOrWhiteSpace("folderRequest.Parent.Id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, string.Format(Constants.CopyPathString, folderRequest.Id))
                    .Method(RequestMethod.Post)
                    .Param(ParamFields, fields)
                    .Payload(_converter.Serialize(folderRequest));

            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to delete a folder. A recursive parameter must be included in order to delete folders that have items 
        /// inside of them. An optional If-Match header can be included using the etag parameter to ensure that client only deletes the folder 
        /// if it knows about the latest version.
        /// </summary>
        /// <param name="id">Id of the folder</param>
        /// <param name="recursive">Whether to delete this folder if it has items inside of it.</param>
        /// <param name="etag">This ‘etag’ field of the folder object to set in the If-Match header</param>
        /// <returns>True will be returned upon successful deletion</returns>
        public async Task<bool> DeleteAsync(string id, bool recursive = false, string etag = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, id)
                .Method(RequestMethod.Delete)
                .Header(Constants.RequestParameters.IfMatch, etag)
                .Param("recursive", recursive.ToString().ToLowerInvariant());

            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Used to update information about the folder. To move a folder, update the ID of its parent. To enable an 
        /// email address that can be used to upload files to this folder, update the folder_upload_email attribute. 
        /// An optional If-Match header can be included using the etag parameter to ensure that client only updates the folder 
        /// if it knows about the latest version.
        /// </summary>
        /// <param name="folderRequest">BoxFolderRequest object</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <param name="etag">This ‘etag’ field of the folder object to set in the If-Match header</param>
        /// <returns>The updated folder is returned if the name is valid. Errors generally occur only if there is a name collision.</returns>
        public async Task<BoxFolder> UpdateInformationAsync(BoxFolderRequest folderRequest, List<string> fields = null, string etag = null)
        {
            folderRequest.ThrowIfNull("folderRequest")
                .Id.ThrowIfNullOrWhiteSpace("folderRequest.Id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, folderRequest.Id)
                    .Header(Constants.RequestParameters.IfMatch, etag)
                    .Param(ParamFields, fields)
                    .Payload(_converter.Serialize(folderRequest))
                    .Method(RequestMethod.Put);

            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to create a shared link for this particular folder. In order to get default shared link status, set it to an empty access level.
        /// To delete a shared link use the DeleteSharedLinkAsync method of this class.
        /// </summary>
        /// <param name="id">Id of the folder to create shared link for</param>
        /// <param name="sharedLinkRequest">Shared link request object</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>A full folder object is returned if the ID is valid and if the shared link is created.</returns>
        public async Task<BoxFolder> CreateSharedLinkAsync(string id, BoxSharedLinkRequest sharedLinkRequest, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, id)
                .Method(RequestMethod.Put)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(new BoxItemRequest() { SharedLink = sharedLinkRequest }));

            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to delete the shared link for the given folder id. 
        /// </summary>
        /// <returns>A full folder object is returned if the ID is valid and if the shared link is deleted.</returns>
        public async Task<BoxFolder> DeleteSharedLinkAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, id)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize(new BoxDeleteSharedLinkRequest()));

            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }


        /// <summary>
        /// Use this to get a list of all the collaborations on a folder i.e. all of the users that have access to that folder.
        /// </summary>
        /// <param name="id">Id of the folder</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>List of all the collaborations on a folder</returns>
        public async Task<BoxCollection<BoxCollaboration>> GetCollaborationsAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, string.Format(Constants.CollaborationsPathString, id))
                .Param(ParamFields, fields);


            IBoxResponse<BoxCollection<BoxCollaboration>> response = await ToResponseAsync<BoxCollection<BoxCollaboration>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

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
        /// <returns>A collection of items contained in the trash is returned. An error is thrown if any of the parameters are invalid.</returns>
        public async Task<BoxCollection<BoxItem>> GetTrashItemsAsync(int limit, int offset = 0, List<string> fields = null, bool autoPaginate=false)
        {
            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, Constants.TrashItemsPathString)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString())
                .Param(ParamFields, fields);

            if (autoPaginate)
            {
                return await AutoPaginateLimitOffset<BoxItem>(request, limit);
            }
            else
            {
                IBoxResponse<BoxCollection<BoxItem>> response = await ToResponseAsync<BoxCollection<BoxItem>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }    
        }

        /// <summary>
        /// Retrieves the files and/or folders that have been moved to the trash. Any attribute in the full files 
        /// or folders objects can be passed in with the fields parameter to get specific attributes, and only those 
        /// specific attributes back; otherwise, the mini format is returned for each item by default. Multiple 
        /// attributes can be passed in separated by commas e.g. fields=name,created_at. Paginated results can be 
        /// retrieved using the limit and offset parameters.
        /// </summary>
        /// <param name="id">This param is not used in implementation</param>
        /// <param name="limit">The maximum number of items to return</param>
        /// <param name="offset">The item at which to begin the response</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>A collection of items contained in the trash is returned. An error is thrown if any of the parameters are invalid.</returns>
        [Obsolete("This method will be removed in a future update. Please use the GetTrashItemsAsync(int, int, List<string>) overload")]
        public async Task<BoxCollection<BoxItem>> GetTrashItemsAsync(string id, int limit, int offset = 0, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, Constants.TrashItemsPathString)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString())
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxItem>> response = await ToResponseAsync<BoxCollection<BoxItem>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Restores an item that has been moved to the trash. Default behavior is to restore the item to the folder it was in 
        /// before it was moved to the trash. If that parent folder no longer exists or if there is now an item with the same 
        /// name in that parent folder, the new parent folder and/or new name will need to be included in the request.
        /// </summary>
        /// <param name="folderRequest">BoxFolderRequest object (specify Parent.Id if you wish to restore to a different parent)</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>The full item will be returned if success. By default it is restored to the parent folder it was in before it was trashed.</returns>
        public async Task<BoxFolder> RestoreTrashedFolderAsync(BoxFolderRequest folderRequest, List<string> fields = null)
        {
            folderRequest.ThrowIfNull("folderRequest")
                .Id.ThrowIfNullOrWhiteSpace("folderRequest.Id");
            
            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, folderRequest.Id)
                    .Method(RequestMethod.Post)
                    .Param(ParamFields, fields);

            // ID shall not be used in request body it is used only as url attribute
            string oldId = folderRequest.Id;
            folderRequest.Id = null;

            request.Payload(_converter.Serialize(folderRequest));

            folderRequest.Id = oldId;

            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Permanently deletes a folder that is in the trash. The folder will no longer exist in Box. This action cannot be undone.
        /// </summary>
        /// <param name="id">Id of the folder</param>
        /// <returns>True will be returned upon successful deletion</returns>
        public async Task<bool> PurgeTrashedFolderAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, string.Format(Constants.TrashFolderPathString, id))
                .Method(RequestMethod.Delete);


            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Retrieves a folder that has been moved to the trash.
        /// </summary>
        /// <param name="id">Id of the folder</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>The full folder will be returned, including information about when it was moved to the trash</returns>
        public async Task<BoxFolder> GetTrashedFolderAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, string.Format(Constants.TrashFolderPathString, id))
                .Param(ParamFields, fields);


            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to retrieve the watermark for a corresponding Box folder.
        /// </summary>
        /// <param name="id">Id of the folder.</param>
        /// <returns>An object containing information about the watermark associated for this folder. If the folder does not have a watermark applied to it than return null</returns>
        public async Task<BoxWatermark> GetWatermarkAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, string.Format(Constants.WatermarkPathString, id))
               .Method(RequestMethod.Get);

            IBoxResponse<BoxWatermarkResponse> response = await ToResponseAsync<BoxWatermarkResponse>(request).ConfigureAwait(false);
            if (response.Status == ResponseStatus.Success)
            {
                return response.ResponseObject.Watermark;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Used to apply or update the watermark for a corresponding Box folder.
        /// </summary>
        /// <param name="id">Id of the folder.</param>
        /// <param name="applyWatermarkRequest">BoxApplyWatermarkRequest object. Can be null, for using default values - imprint="default" </param>
        /// <returns>An object containing information about the watermark associated for this folder.</returns>
        public async Task<BoxWatermark> ApplyWatermarkAsync(string id, BoxApplyWatermarkRequest applyWatermarkRequest = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            if (applyWatermarkRequest == null)
            {
                applyWatermarkRequest = new BoxApplyWatermarkRequest() { Watermark = new BoxWatermarkRequest() { Imprint = "default" } };
            }

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, string.Format(Constants.WatermarkPathString, id))
               .Method(RequestMethod.Put)
               .Payload(_converter.Serialize(applyWatermarkRequest));

            IBoxResponse<BoxWatermarkResponse> response = await ToResponseAsync<BoxWatermarkResponse>(request).ConfigureAwait(false);

            if (response.Status == ResponseStatus.Success)
            {
                return response.ResponseObject.Watermark;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Used to remove the watermark for a corresponding Box folder.
        /// </summary>
        /// <param name="id">Id of the folder.</param>
        /// <returns>True to confirm the watermark has been removed. If the folder did not have a watermark applied to it, than False will be returned.</returns>
        public async Task<bool> RemoveWatermarkAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, string.Format(Constants.WatermarkPathString, id))
               .Method(RequestMethod.Delete);

            IBoxResponse<BoxWatermarkResponse> response = await ToResponseAsync<BoxWatermarkResponse>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

    }
}
