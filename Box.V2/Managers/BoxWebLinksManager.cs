using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Extensions;
using Box.V2.Converter;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    /// <summary>
    /// Allow create, update, get, delete weblink for folder.
    /// </summary>
    public class BoxWebLinksManager : BoxResourceManager
    {

        public BoxWebLinksManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Creates a web link object within a given folder.
        /// </summary>
        /// <param name="createWeblinkRequest">Weblink request
        /// createWebLinkRequest.Url (Required) - URL you want the web link to point to. Must include http:// or https://,
        /// createWebLinkRequest.Parent.Id (Required) - The ID of the parent folder where you're creating the web link,
        /// createWebLinkRequest.Name - Name for the web link. Will default to the URL if empty,
        /// createWebLinkRequest.Description - Description of the web link. Will provide more context to users about the web link.
        /// </param>
        /// <returns>The web link object is returned.</returns>
        public async Task<BoxWebLink> CreateWeblinkAsync(BoxCreateWebLinkRequest createWebLinkRequest)
        {
            createWebLinkRequest.ThrowIfNull("createWebLinkRequest")
                .Parent.ThrowIfNull("createWebLinkRequest.Parent")
                .Id.ThrowIfNullOrWhiteSpace("createWebLinkRequest.Parent.Id");
            createWebLinkRequest.Url.ThrowIfNullOrWhiteSpace("createWebLinkRequest.Url");
            if (!createWebLinkRequest.Url.StartsWith("http://") && !createWebLinkRequest.Url.StartsWith("https://"))
            {
                throw new ArgumentException("Url must include http:// or https://", "createWebLinkRequest.Url");
            }
            createWebLinkRequest.Id = null;
            createWebLinkRequest.SharedLink = null;
            createWebLinkRequest.Tags = null;
            createWebLinkRequest.Parent.Type = null;

            BoxRequest request = new BoxRequest(_config.WebLinksEndpointUri)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize(createWebLinkRequest));

            IBoxResponse<BoxWebLink> response = await ToResponseAsync<BoxWebLink>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Deletes a web link and moves it to the trash.
        /// </summary>
        /// <param name="webLinkId">Id of the weblink.</param>
        /// <returns>True, if successfully deleted and moved to trash</returns>
        public async Task<bool> DeleteWeblinkAsync(string webLinkId)
        {
            webLinkId.ThrowIfNullOrWhiteSpace("webLinkId");

            BoxRequest request = new BoxRequest(_config.WebLinksEndpointUri, webLinkId)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxWebLink> response = await ToResponseAsync<BoxWebLink>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

    }
}
