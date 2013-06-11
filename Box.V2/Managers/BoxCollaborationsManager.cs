using Box.V2.Auth;
using Box.V2.Contracts;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    public class BoxCollaborationsManager : BoxResourceManager
    {

        public BoxCollaborationsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth) { }

        /// <summary>
        /// Used to add a collaboration for a single user to a folder. Descriptions of the various roles can be found 
        /// <see cref="https://support.box.com/entries/20366031-what-are-the-different-collaboration-permissions-and-what-access-do-they-provide"/>
        /// Either an email address or a user ID can be used to create the collaboration.
        /// </summary>
        /// <param name="collaborationRequest"></param>
        /// <returns></returns>
        public async Task<BoxCollaboration> AddCollaborationAsync(BoxCollaborationRequest collaborationRequest)
        {
            CheckPrerequisite(
                collaborationRequest.ThrowIfNull("collaborationRequest")
                    .Item.ThrowIfNull("collaborationRequest.Item").Id,
                    collaborationRequest.AccessibleBy.ThrowIfNull("collaborationRequest.AccessibleBy").Id,
                    collaborationRequest.AccessibleBy.Login);

            BoxRequest request = new BoxRequest(_config.CollaborationsEndpointUri)
                .Method(RequestMethod.POST)
                .Authorize(_auth.Session.AccessToken)
                .Payload(_converter.Serialize(collaborationRequest));

            IBoxResponse<BoxCollaboration> response = await ToResponseAsync<BoxCollaboration>(request);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to edit an existing collaboration. Descriptions of the various roles can be found 
        /// <see cref="https://support.box.com/entries/20366031-what-are-the-different-collaboration-permissions-and-what-access-do-they-provide"/>
        /// </summary>
        /// <param name="collaborationRequest"></param>
        /// <returns></returns>
        public async Task<BoxCollaboration> EditCollaborationAsync(BoxCollaborationRequest collaborationRequest)
        {
            CheckPrerequisite(
                collaborationRequest.ThrowIfNull("collaborationRequest").Id);

            BoxRequest request = new BoxRequest(_config.CollaborationsEndpointUri, collaborationRequest.Id)
                .Method(RequestMethod.PUT)
                .Authorize(_auth.Session.AccessToken)
                .Payload(_converter.Serialize(collaborationRequest));

            IBoxResponse<BoxCollaboration> response = await ToResponseAsync<BoxCollaboration>(request);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to delete a single collaboration.
        /// </summary>
        /// <param name="collaborationRequest"></param>
        /// <returns></returns>
        public async Task<bool> RemoveCollaborationAsync(BoxCollaborationRequest collaborationRequest)
        {
            CheckPrerequisite(collaborationRequest.ThrowIfNull("collaborationRequest").Id);

            BoxRequest request = new BoxRequest(_config.CollaborationsEndpointUri, collaborationRequest.Id)
                .Method(RequestMethod.DELETE)
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<BoxCollaboration> response = await ToResponseAsync<BoxCollaboration>(request);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// /// Used to get information about a single collaboration. A complete list of the user’s pending collaborations can also be retrieved.
        /// <see cref="http://developers.box.com/docs/#collaborations-get-pending-collaborations"/>
        /// </summary>
        /// <param name="collaborationRequest"></param>
        /// <returns></returns>
        public async Task<BoxCollaboration> GetCollaborationAsync(string id)
        {
            CheckPrerequisite(id);

            BoxRequest request = new BoxRequest(_config.CollaborationsEndpointUri, id)
                .Authorize(_auth.Session.AccessToken);

            IBoxResponse<BoxCollaboration> response = await ToResponseAsync<BoxCollaboration>(request);

            return response.ResponseObject;
        }
    }
}
