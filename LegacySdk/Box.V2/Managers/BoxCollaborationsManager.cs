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
    public class BoxCollaborationsManager : BoxResourceManager, IBoxCollaborationsManager
    {

        public BoxCollaborationsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Used to add a collaboration for a single user or a single group to a folder or file. 
        /// Either an email address, a user ID, or a group id can be used to create the collaboration. 
        /// If the collaboration is being created with a group, access to this endpoint is granted based on the group's invitability_level.
        /// </summary>
        /// <param name="collaborationRequest">BoxCollaborationRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="notify">Determines if the user, (or all the users in the group) should receive email notification of the collaboration.</param>
        /// <returns>The new collaboration object is returned. Errors may occur if the IDs are invalid or if the user does not have permissions to create a collaboration.</returns>
        public async Task<BoxCollaboration> AddCollaborationAsync(BoxCollaborationRequest collaborationRequest, IEnumerable<string> fields = null, bool? notify = null)
        {
            collaborationRequest.ThrowIfNull("collaborationRequest")
                .Item.ThrowIfNull("collaborationRequest.Item")
                .Id.ThrowIfNullOrWhiteSpace("collaborationRequest.Item.Id");
            collaborationRequest.AccessibleBy.ThrowIfNull("collaborationRequest.AccessibleBy");
            collaborationRequest.Role.ThrowIfNullOrWhiteSpace("Role");

            BoxRequest request = new BoxRequest(_config.CollaborationsEndpointUri)
                .Method(RequestMethod.Post)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(collaborationRequest));

            if (notify.HasValue)
            {
                var value = notify.Value ? "true" : "false";
                request.Param("notify", value);
            }

            IBoxResponse<BoxCollaboration> response = await ToResponseAsync<BoxCollaboration>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to edit an existing collaboration. Descriptions of the various roles can be found here
        /// </summary>
        /// <param name="collaborationRequest">BoxCollaborationRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The updated collaboration object is returned. If the role is changed to owner, the collaboration is deleted with a new one created for the previous owner and a 204 is returned.
        /// Errors may occur if the IDs are invalid or if the user does not have permissions to edit the collaboration.
        /// </returns>
        public async Task<BoxCollaboration> EditCollaborationAsync(BoxCollaborationRequest collaborationRequest, IEnumerable<string> fields = null)
        {
            collaborationRequest.ThrowIfNull("collaborationRequest")
                .Id.ThrowIfNullOrWhiteSpace("collaborationRequest.Id");

            BoxRequest request = new BoxRequest(_config.CollaborationsEndpointUri, collaborationRequest.Id)
                .Method(RequestMethod.Put)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(collaborationRequest));

            IBoxResponse<BoxCollaboration> response = await ToResponseAsync<BoxCollaboration>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to delete a single collaboration.
        /// </summary>
        /// <param name="id">Id of the collaboration to delete.</param>
        /// <returns>True is returned if the ID is valid, and the user has permissions to remove the collaboration.</returns>
        public async Task<bool> RemoveCollaborationAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.CollaborationsEndpointUri, id)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxCollaboration> response = await ToResponseAsync<BoxCollaboration>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Used to get information about a single collaboration.
        /// </summary>
        /// <param name="id">Id of the collaboration object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The collaboration object is returned. Errors may occur if id is invalid, the collaboration has been rejected by the user, or if the user does not have permissions to see the collaboration.</returns>
        public async Task<BoxCollaboration> GetCollaborationAsync(string id, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.CollaborationsEndpointUri, id)
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollaboration> response = await ToResponseAsync<BoxCollaboration>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }
        /// <summary>
        /// Used to retrieve all pending collaboration invites for this user (with user being determined by access token or As-User header value).
        /// </summary>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A collection of pending collaboration objects are returned. If the user has no pending collaborations, the collection will be empty.</returns>
        public async Task<BoxCollection<BoxCollaboration>> GetPendingCollaborationAsync(IEnumerable<string> fields = null)
        {

            BoxRequest request = new BoxRequest(_config.CollaborationsEndpointUri, null)
               .Param(Constants.RequestParameters.Status, Constants.RequestParameters.Pending)
               .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxCollaboration>> response = await ToResponseAsync<BoxCollection<BoxCollaboration>>(request).ConfigureAwait(false);
            return response.ResponseObject;
        }
    }
}
