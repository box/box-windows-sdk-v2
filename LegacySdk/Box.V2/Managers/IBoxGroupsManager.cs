using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;

namespace Box.V2.Managers
{
    /// <summary>
    /// The class managing the Box API's Groups endpoint.
    /// </summary>
    public interface IBoxGroupsManager
    {
        /// <summary>
        /// Retrieves all of the groups for given enterprise. Must have permissions to see an enterprise's groups.
        /// </summary>
        /// <param name="limit">The number of results to return with this request. Refer to the Box API for defaults.</param>
        /// <param name="offset">The offset of the results. Refer to the Box API for more details.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all groups; defaults to false.</param>
        /// <param name="filterTerm">Limits the results to only groups whose name starts with the search term.</param>
        /// <returns>A collection of groups.</returns>
        Task<BoxCollection<BoxGroup>> GetAllGroupsAsync(int? limit = null, int? offset = null, IEnumerable<string> fields = null, bool autoPaginate = false, string filterTerm = null);

        /// <summary>
        /// Returns the group with the specified id.
        /// </summary>
        /// <param name="id">The id of the group to return.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Group with id='id'.</returns>
        Task<BoxGroup> GetGroupAsync(string id, IEnumerable<string> fields = null);

        /// <summary>
        /// Create a new group.
        /// </summary>
        /// <param name="groupRequest">BoxGroupRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The newly created group.</returns>
        Task<BoxGroup> CreateAsync(BoxGroupRequest groupRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Delete an existing group.
        /// </summary>
        /// <param name="id">The id of the group to delete.</param>
        /// <returns>True if delete was successful.</returns>
        Task<bool> DeleteAsync(string id);

        /// <summary>
        /// Update an existing group.
        /// </summary>
        /// <param name="id">Id of the group to update.</param>
        /// <param name="groupRequest">BoxGroupRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The updated group.</returns>
        Task<BoxGroup> UpdateAsync(string id, BoxGroupRequest groupRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Add a user to a group.
        /// </summary>
        /// <param name="membershipRequest">BoxGroupMembershipRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The group membership created.</returns>
        Task<BoxGroupMembership> AddMemberToGroupAsync(BoxGroupMembershipRequest membershipRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Delete a group membership.
        /// </summary>
        /// <param name="id">The id of the groupmembership to delete.</param>
        /// <returns>True if delete was successful.</returns>
        Task<bool> DeleteGroupMembershipAsync(string id);

        /// <summary>
        /// Retrieves all of the group collaborations for a given group. Note this is only available to group admins.
        /// </summary>
        /// <param name="groupId">The id of the group to get the list of collaborations for.</param>
        /// <param name="limit">The number of results to return with this request. Refer to the Box API for defaults.</param>
        /// <param name="offset">The offset of the results. Refer to the Box API for more details.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all group collaborations; defaults to false.</param>
        /// <returns>A collection of collaborations for the specified group id.</returns>
        Task<BoxCollection<BoxCollaboration>> GetCollaborationsForGroupAsync(string groupId, int? limit = null, int? offset = null,
            IEnumerable<string> fields = null, bool autoPaginate = false);

        /// <summary>
        /// Get the list of group memberships for a given group.
        /// </summary>
        /// <param name="groupId">The id of the group to get the list of memberships for.</param>
        /// <param name="limit">The number of results to return with this request. Refer to the Box API for defaults.</param>
        /// <param name="offset">The offset of the results. Refer to the Box API for more details.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all group memberships; defaults to false.</param>
        /// <returns>A collection of group memberships for the specified group id.</returns>
        Task<BoxCollection<BoxGroupMembership>> GetAllGroupMembershipsForGroupAsync(string groupId, int? limit = null, int? offset = null,
            IEnumerable<string> fields = null, bool autoPaginate = false);

        /// <summary>
        /// Get the list of group memberships for a given user.
        /// </summary>
        /// <param name="userId">The id of the user to get the list of memberships for.</param>
        /// <param name="limit">The number of results to return with this request. Refer to the Box API for defaults.</param>
        /// <param name="offset">The offset of the results. Refer to the Box API for more details.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all group memberships; defaults to false.</param>
        /// <returns>A collection of group memberships for the specified user id.</returns>
        Task<BoxCollection<BoxGroupMembership>> GetAllGroupMembershipsForUserAsync(string userId, int? limit = null, int? offset = null,
            IEnumerable<string> fields = null, bool autoPaginate = false);

        /// <summary>
        /// Returns the group membership with the specified id.
        /// </summary>
        /// <param name="id">The id of the group membership to return.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Group membership with id='id'.</returns>
        Task<BoxGroupMembership> GetGroupMembershipAsync(string id, IEnumerable<string> fields = null);

        /// <summary>
        /// Updates the specified group membership with the specified request.
        /// </summary>
        /// <param name="membershipId">It of the group membership to update.</param>
        /// <param name="memRequest">BoxGroupMembershipRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The updated group membership.</returns>
        Task<BoxGroupMembership> UpdateGroupMembershipAsync(string membershipId, BoxGroupMembershipRequest memRequest, IEnumerable<string> fields = null);
    }
}
