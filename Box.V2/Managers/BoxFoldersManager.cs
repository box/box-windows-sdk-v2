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
        /// Retrieves the files and/or folders contained in the provided folder id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        public async Task<BoxCollection<BoxItem>> GetFolderItemsAsync(string id, int limit, int offset = 0, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, string.Format(Constants.ItemsPathString, id))
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString())
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxItem>> response = await ToResponseAsync<BoxCollection<BoxItem>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to create a new empty folder. The new folder will be created inside of the specified parent folder
        /// </summary>
        /// <param name="folderRequest">
        /// folderRequest.Name - The desired name for the folder
        /// folderRequest.Parent.Id -  The ID of the parent folder
        /// </param>
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
        /// as well as the files and folders contained in it. The root folder of a Box account is always 
        /// represented by the id “0″.
        /// </summary>
        /// <param name="id">The folder id</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>A full folder object is returned, including the most current information available about it. 
        /// An 404 error is thrown if the folder does not exist or a 4xx if the user does not have access to it.</returns>
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
        /// <param name="folderRequest">
        /// folderRequest.Id - Id of the file
        /// folderRequest.Parent.Id - The ID of the destination folder
        /// folderRequest.Name - An optional new name for the folder.
        /// </param>
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
        /// inside of them. An optional If-Match header can be included to ensure that client only deletes the folder 
        /// if it knows about the latest version.
        /// </summary>
        /// <param name="id">Id of the folder</param>
        /// <param name="recursive">Whether to delete this folder if it has items inside of it.</param>
        /// <param name="etag">This is in the ‘etag’ field of the folder object</param>
        /// <returns>True will be returned upon successful deletion</returns>
        public async Task<bool> DeleteAsync(string id, bool recursive = false, string etag = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, id)
                .Method(RequestMethod.Delete)
                .Header("If-Match", etag)
                .Param("recursive", recursive.ToString().ToLowerInvariant());

            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Used to update information about the folder. To move a folder, update the ID of its parent. To enable an 
        /// email address that can be used to upload files to this folder, update the folder_upload_email attribute. 
        /// An optional If-Match header can be included to ensure that client only updates the folder if it knows 
        /// about the latest version.
        /// </summary>
        /// <returns></returns>
        public async Task<BoxFolder> UpdateInformationAsync(BoxFolderRequest folderRequest, List<string> fields = null)
        {
            folderRequest.ThrowIfNull("folderRequest")
                .Id.ThrowIfNullOrWhiteSpace("folderRequest.Id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, folderRequest.Id)
                    .Param(ParamFields, fields)
                    .Payload(_converter.Serialize(folderRequest))
                    .Method(RequestMethod.Put);

            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to create a shared link for this particular folder. Please see here for more information on the 
        /// permissions available for shared links. In order to disable a shared link, send this same type of PUT 
        /// request with the value of shared_link set to null, i.e. {"shared_link": null}
        /// </summary>
        /// <returns></returns>
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
        /// Used to delete the shared link for this particular folder. 
        /// </summary>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// attributes can be passed in separated by commas e.g. fields=name,created_at. Paginated results can be 
        /// retrieved using the limit and offset parameters.
        /// </summary>
        /// <returns></returns>
        public async Task<BoxCollection<BoxItem>> GetTrashItemsAsync(int limit, int offset = 0, List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, Constants.TrashItemsPathString)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString())
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxItem>> response = await ToResponseAsync<BoxCollection<BoxItem>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves the files and/or folders that have been moved to the trash. Any attribute in the full files 
        /// or folders objects can be passed in with the fields parameter to get specific attributes, and only those 
        /// specific attributes back; otherwise, the mini format is returned for each item by default. Multiple 
        /// attributes can be passed in separated by commas e.g. fields=name,created_at. Paginated results can be 
        /// retrieved using the limit and offset parameters.
        /// </summary>
        /// <returns></returns>
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
        /// <returns></returns>
        public async Task<BoxFolder> RestoreTrashedFolderAsync(BoxFolderRequest folderRequest, List<string> fields = null)
        {
            folderRequest.ThrowIfNull("folderRequest")
                .Id.ThrowIfNullOrWhiteSpace("folderRequest.Id");
            folderRequest.Parent.ThrowIfNull("folderRequest.Parent")
                .Id.ThrowIfNullOrWhiteSpace("folderRequest.Parent.Id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, folderRequest.Id)
                    .Method(RequestMethod.Post)
                    .Param(ParamFields, fields)
                    .Payload(_converter.Serialize(folderRequest));


            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Permanently deletes an item that is in the trash. The item will no longer exist in Box. This action cannot be undone.
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
        /// <param name="id">Id of the Folder</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <returns>The full folder will be returned, including information about when the it was moved to the trash</returns>
        public async Task<BoxFolder> GetTrashedFolderAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, string.Format(Constants.TrashFolderPathString, id))
                .Param(ParamFields, fields);


            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

    }
}
