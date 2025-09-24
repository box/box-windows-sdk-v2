using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;

namespace Box.V2.Managers
{
    /// <summary>
    /// Allow create, update, get, delete weblink for folder.
    /// </summary>
    public class BoxWebLinksManager : BoxResourceManager, IBoxWebLinksManager
    {

        public BoxWebLinksManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Creates a web link object within a given folder.
        /// </summary>
        /// <param name="createWebLinkRequest">BoxWebLinkRequest object</param>
        /// <returns>The web link object is returned.</returns>
        public async Task<BoxWebLink> CreateWebLinkAsync(BoxWebLinkRequest createWebLinkRequest)
        {
            createWebLinkRequest.ThrowIfNull("createWebLinkRequest")
                .Parent.ThrowIfNull("createWebLinkRequest.Parent")
                .Id.ThrowIfNullOrWhiteSpace("createWebLinkRequest.Parent.Id");

            BoxRequest request = new BoxRequest(_config.WebLinksEndpointUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(createWebLinkRequest));

            IBoxResponse<BoxWebLink> response = await ToResponseAsync<BoxWebLink>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Deletes a web link and moves it to the trash.
        /// </summary>
        /// <param name="webLinkId">Id of the weblink.</param>
        /// <returns>True, if successfully deleted and moved to trash</returns>
        public async Task<bool> DeleteWebLinkAsync(string webLinkId)
        {
            webLinkId.ThrowIfNullOrWhiteSpace("webLinkId");

            BoxRequest request = new BoxRequest(_config.WebLinksEndpointUri, webLinkId)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxWebLink> response = await ToResponseAsync<BoxWebLink>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Use to get information about the web link.
        /// </summary>
        /// <param name="webLinkId">Id of the weblink.</param>
        /// <returns>The web link object is returned.</returns>
        public async Task<BoxWebLink> GetWebLinkAsync(string webLinkId)
        {
            webLinkId.ThrowIfNullOrWhiteSpace("webLinkId");

            BoxRequest request = new BoxRequest(_config.WebLinksEndpointUri, webLinkId)
                .Method(RequestMethod.Get);

            IBoxResponse<BoxWebLink> response = await ToResponseAsync<BoxWebLink>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Updates information for a web link.
        /// </summary>
        /// <param name="webLinkId">Id of the weblink.</param>
        /// <param name="updateWebLinkRequest">BoxWebLinkRequest object</param>
        /// <returns>An updated web link object if the update was successful.</returns>
        public async Task<BoxWebLink> UpdateWebLinkAsync(string webLinkId, BoxWebLinkRequest updateWebLinkRequest)
        {
            webLinkId.ThrowIfNullOrWhiteSpace("webLinkId");
            updateWebLinkRequest.ThrowIfNull("updateWebLinkRequest");

            BoxRequest request = new BoxRequest(_config.WebLinksEndpointUri, webLinkId)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize(updateWebLinkRequest));

            IBoxResponse<BoxWebLink> response = await ToResponseAsync<BoxWebLink>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to create a copy of a web link in another folder. The original version of the web link will not be altered.
        /// </summary>
        /// <param name="webLinkId">The Id of the web link to copy.</param>
        /// <param name="destinationFolderId">The Id of the destination folder, where the new copy will be created.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>
        /// A full web link object is returned if the ID is valid and if the update is successful. 
        /// Errors can be thrown if the destination folder is invalid or if a name collision occurs. 
        /// </returns>
        public async Task<BoxWebLink> CopyAsync(string webLinkId, string destinationFolderId, IEnumerable<string> fields = null)
        {
            webLinkId.ThrowIfNullOrWhiteSpace("webLinkId");
            destinationFolderId.ThrowIfNullOrWhiteSpace("destinationFolderId");

            BoxRequest request = new BoxRequest(_config.WebLinksEndpointUri, string.Format(Constants.CopyPathString, webLinkId))
                .Method(RequestMethod.Post)
                .Payload($"{{\"parent\":{{\"id\":\"{destinationFolderId}\"}}}}")
                .Param(ParamFields, fields);

            IBoxResponse<BoxWebLink> response = await ToResponseAsync<BoxWebLink>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to create a shared link for a web link.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="sharedLinkRequest">BoxSharedLinkRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A full web link object containing the updated shared link is returned
        /// if the ID is valid and if the update is successful.</returns>
        public async Task<BoxWebLink> CreateSharedLinkAsync(string id, BoxSharedLinkRequest sharedLinkRequest, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");
            sharedLinkRequest.ThrowIfNull("sharedLinkRequest");

            if (sharedLinkRequest?.Permissions != null)
                sharedLinkRequest.Permissions.Edit.ThrowIfDifferent("sharedLinkRequest.permissions.edit", false);

            BoxRequest request = new BoxRequest(_config.WebLinksEndpointUri, id)
                .Method(RequestMethod.Put)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(new BoxItemRequest() { SharedLink = sharedLinkRequest }));

            IBoxResponse<BoxWebLink> response = await ToResponseAsync<BoxWebLink>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to delete the shared link for this particular file.
        /// </summary>
        /// <param name="id">The id of the web link to remove the shared link from.</param>
        /// <returns>A full web link object with the shared link removed is returned
        /// if the ID is valid and if the update is successful.</returns>
        public async Task<BoxWebLink> DeleteSharedLinkAsync(string id, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.WebLinksEndpointUri, id)
                .Method(RequestMethod.Put)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(new BoxDeleteSharedLinkRequest()));

            IBoxResponse<BoxWebLink> response = await ToResponseAsync<BoxWebLink>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }
    }
}
