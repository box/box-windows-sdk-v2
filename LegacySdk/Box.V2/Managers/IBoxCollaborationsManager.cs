using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    public interface IBoxCollaborationsManager
    {
        /// <summary>
        /// Used to add a collaboration for a single user or a single group to a folder or file. 
        /// Either an email address, a user ID, or a group id can be used to create the collaboration. 
        /// If the collaboration is being created with a group, access to this endpoint is granted based on the group's invitability_level.
        /// </summary>
        /// <param name="collaborationRequest">BoxCollaborationRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="notify">Determines if the user, (or all the users in the group) should receive email notification of the collaboration.</param>
        /// <returns>The new collaboration object is returned. Errors may occur if the IDs are invalid or if the user does not have permissions to create a collaboration.</returns>
        Task<BoxCollaboration> AddCollaborationAsync(BoxCollaborationRequest collaborationRequest, IEnumerable<string> fields = null, bool? notify = null);

        /// <summary>
        /// Used to edit an existing collaboration. Descriptions of the various roles can be found here
        /// </summary>
        /// <param name="collaborationRequest">BoxCollaborationRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The updated collaboration object is returned. If the role is changed to owner, the collaboration is deleted with a new one created for the previous owner and a 204 is returned.
        /// Errors may occur if the IDs are invalid or if the user does not have permissions to edit the collaboration.
        /// </returns>
        Task<BoxCollaboration> EditCollaborationAsync(BoxCollaborationRequest collaborationRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to delete a single collaboration.
        /// </summary>
        /// <param name="id">Id of the collaboration to delete.</param>
        /// <returns>True is returned if the ID is valid, and the user has permissions to remove the collaboration.</returns>
        Task<bool> RemoveCollaborationAsync(string id);

        /// <summary>
        /// Used to get information about a single collaboration.
        /// </summary>
        /// <param name="id">Id of the collaboration object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The collaboration object is returned. Errors may occur if id is invalid, the collaboration has been rejected by the user, or if the user does not have permissions to see the collaboration.</returns>
        Task<BoxCollaboration> GetCollaborationAsync(string id, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to retrieve all pending collaboration invites for this user (with user being determined by access token or As-User header value).
        /// </summary>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A collection of pending collaboration objects are returned. If the user has no pending collaborations, the collection will be empty.</returns>
        Task<BoxCollection<BoxCollaboration>> GetPendingCollaborationAsync(IEnumerable<string> fields = null);
    }
}
