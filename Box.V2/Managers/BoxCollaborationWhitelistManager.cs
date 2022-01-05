using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using Newtonsoft.Json.Linq;

namespace Box.V2.Managers
{
    public class BoxCollaborationWhitelistManager : BoxResourceManager, IBoxCollaborationWhitelistManager
    {
        public BoxCollaborationWhitelistManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Used to whitelist a domain for a Box collaborator. You can specify the domain and direction of the whitelist. When whitelisted successfully, only users from the whitelisted
        /// domain can be invited as a collaborator. 
        /// </summary>
        /// <param name="domainToWhitelist">This is the domain to whitelist collaboration.</param>
        /// <param name="directionForWhitelist">Can be set to inbound, outbound, or both for the direction of the whitelist.</param>
        /// <returns>The whitelist entry if successfully created.</returns>
        public async Task<BoxCollaborationWhitelistEntry> AddCollaborationWhitelistEntryAsync(string domainToWhitelist, string directionForWhitelist)
        {
            domainToWhitelist.ThrowIfNullOrWhiteSpace("domainToWhitelist");
            directionForWhitelist.ThrowIfNullOrWhiteSpace("directionForWhitelist");

            dynamic req = new JObject();
            req.domain = domainToWhitelist;
            req.direction = directionForWhitelist;

            string jsonStr = req.ToString();

            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistEntryUri)
                .Method(RequestMethod.Post)
                .Payload(jsonStr);

            IBoxResponse<BoxCollaborationWhitelistEntry> response = await ToResponseAsync<BoxCollaborationWhitelistEntry>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to get information about a single collaboration whitelist for domain.
        /// </summary>
        /// <param name="id">Id of the domain whitelist object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The domain collaboration whitelist object is returned. Errors may occur if id is invalid.</returns>
        public async Task<BoxCollaborationWhitelistEntry> GetCollaborationWhitelistEntryAsync(string id, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistEntryUri, id)
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollaborationWhitelistEntry> response = await ToResponseAsync<BoxCollaborationWhitelistEntry>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to get information about all domain collaboration whitelists.
        /// </summary>
        /// <param name="marker">Position to return results from.</param>
        /// <param name="limit">Maximum number of entries to return. Default is 100.</param>
        /// <returns>The collection of domain collaboration whitelist objects is returned.</returns>
        public async Task<BoxCollectionMarkerBased<BoxCollaborationWhitelistEntry>> GetAllCollaborationWhitelistEntriesAsync(int limit = 100, string nextMarker = null, bool autoPaginate = false)
        {
            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistEntryUri)
                .Method(RequestMethod.Get)
                .Param("limit", limit.ToString())
                .Param("marker", nextMarker);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxCollaborationWhitelistEntry>(request, limit).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBased<BoxCollaborationWhitelistEntry>> response =
                    await ToResponseAsync<BoxCollectionMarkerBased<BoxCollaborationWhitelistEntry>>(request).ConfigureAwait(false);

                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Used to delete a domain collaboration whitelists.
        /// </summary>
        /// <param name="id">The id of the collaboration whitelist to delete.</param>
        /// <returns>A boolean value indicating whether or not the collaboration whitelist was successfully deleted.</returns>
        public async Task<bool> DeleteCollaborationWhitelistEntryAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistEntryUri, id)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxCollaborationWhitelistEntry> response = await ToResponseAsync<BoxCollaborationWhitelistEntry>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Used to add a user to the exempt user list. Once on the exempt user list this user is whitelisted as a collaborator.
        /// </summary>
        /// <param name="userId">This is the Box User to add to the exempt list.</param>
        /// <returns>The specific exempt user or user on the collaborator whitelist.</returns>
        public async Task<BoxCollaborationWhitelistTargetEntry> AddCollaborationWhitelistExemptUserAsync(string userId)
        {
            userId.ThrowIfNullOrWhiteSpace("userId");

            dynamic user = new JObject();
            dynamic req = new JObject();

            user.id = userId;
            user.type = "user";
            req.user = user;

            string jsonStr = req.ToString();

            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistTargetEntryUri)
                .Method(RequestMethod.Post)
                .Payload(jsonStr);

            IBoxResponse<BoxCollaborationWhitelistTargetEntry> response = await ToResponseAsync<BoxCollaborationWhitelistTargetEntry>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to get information about a single collaboration whitelist for a user.
        /// </summary>
        /// <param name="id">Id of the collaboration whitelist exempt target object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The collaboration whitelist object for a user is returned. Errors may occur if id is invalid.</returns>
        public async Task<BoxCollaborationWhitelistTargetEntry> GetCollaborationWhitelistExemptUserAsync(string id, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistTargetEntryUri, id)
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollaborationWhitelistTargetEntry> response = await ToResponseAsync<BoxCollaborationWhitelistTargetEntry>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to get information about all collaboration whitelists for users.
        /// </summary>
        /// <param name="marker">Position to return results from.</param>
        /// <param name="limit">Maximum number of entries to return. Default is 100.</param>
        /// <returns>The collection of collaboration whitelist object is returned for users.</returns>
        public async Task<BoxCollectionMarkerBased<BoxCollaborationWhitelistTargetEntry>> GetAllCollaborationWhitelistExemptUsersAsync(int limit = 100, string nextMarker = null, bool autoPaginate = false)
        {
            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistTargetEntryUri)
                .Method(RequestMethod.Get)
                .Param("limit", limit.ToString())
                .Param("marker", nextMarker);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxCollaborationWhitelistTargetEntry>(request, limit).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBased<BoxCollaborationWhitelistTargetEntry>> response =
                    await ToResponseAsync<BoxCollectionMarkerBased<BoxCollaborationWhitelistTargetEntry>>(request).ConfigureAwait(false);

                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Used to delete a user from the exemption list or collaboration whitelist.
        /// </summary>
        /// <param name="id">The id of the collaboration whitelist to delete for user.</param>
        /// <returns>A boolean value indicating whether or not the user was successfully deleted from the collaboration whitelist.</returns>
        public async Task<bool> DeleteCollaborationWhitelistExemptUserAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.CollaborationWhitelistTargetEntryUri, id)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxCollaborationWhitelistTargetEntry> response = await ToResponseAsync<BoxCollaborationWhitelistTargetEntry>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }
    }
}
