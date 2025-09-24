using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Services;

namespace Box.V2.Managers
{
    /// <summary>
    /// The class managing the Box API's Groups endpoint.
    /// </summary>
    public class BoxGroupsManager : BoxResourceManager, IBoxGroupsManager
    {
        /// <summary>
        /// Create a new Boxgroupmanager object.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="service"></param>
        /// <param name="converter"></param>
        /// <param name="auth"></param>
        public BoxGroupsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Retrieves all of the groups for given enterprise. Must have permissions to see an enterprise's groups.
        /// </summary>
        /// <param name="limit">The number of results to return with this request. Refer to the Box API for defaults.</param>
        /// <param name="offset">The offset of the results. Refer to the Box API for more details.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all groups; defaults to false.</param>
        /// <param name="filterTerm">Limits the results to only groups whose name starts with the search term.</param>
        /// <returns>A collection of groups.</returns>
        public async Task<BoxCollection<BoxGroup>> GetAllGroupsAsync(int? limit = null, int? offset = null, IEnumerable<string> fields = null, bool autoPaginate = false, string filterTerm = null)
        {
            BoxRequest request = new BoxRequest(_config.GroupsEndpointUri)
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString());

            if (filterTerm != null)
            {
                request.Param("filter_term", filterTerm);
            }

            if (autoPaginate)
            {
                if (!limit.HasValue)
                {
                    limit = 100;
                    request.Param("limit", limit.ToString());
                }
                if (!offset.HasValue)
                    request.Param("offset", "0");

                return await AutoPaginateLimitOffset<BoxGroup>(request, limit.Value).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollection<BoxGroup>> response = await ToResponseAsync<BoxCollection<BoxGroup>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Returns the group with the specified id.
        /// </summary>
        /// <param name="id">The id of the group to return.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Group with id='id'.</returns>
        public async Task<BoxGroup> GetGroupAsync(string id, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.GroupsEndpointUri, id)
                .Param(ParamFields, fields);

            IBoxResponse<BoxGroup> response = await ToResponseAsync<BoxGroup>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Create a new group.
        /// </summary>
        /// <param name="groupRequest">BoxGroupRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The newly created group.</returns>
        public async Task<BoxGroup> CreateAsync(BoxGroupRequest groupRequest, IEnumerable<string> fields = null)
        {
            groupRequest.ThrowIfNull("groupRequest")
                .Name.ThrowIfNullOrWhiteSpace("groupRequest.Name");

            BoxRequest request = new BoxRequest(_config.GroupsEndpointUri)
                .Param(ParamFields, fields)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize<BoxGroupRequest>(groupRequest));

            IBoxResponse<BoxGroup> response = await ToResponseAsync<BoxGroup>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Delete an existing group.
        /// </summary>
        /// <param name="id">The id of the group to delete.</param>
        /// <returns>True if delete was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.GroupsEndpointUri, id)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxGroup> response = await ToResponseAsync<BoxGroup>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Update an existing group.
        /// </summary>
        /// <param name="id">Id of the group to update.</param>
        /// <param name="groupRequest">BoxGroupRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The updated group.</returns>
        public async Task<BoxGroup> UpdateAsync(string id, BoxGroupRequest groupRequest, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");
            groupRequest.ThrowIfNull("groupRequest");

            BoxRequest request = new BoxRequest(_config.GroupsEndpointUri, id).
                Method(RequestMethod.Put).
                Param(ParamFields, fields).
                Payload(_converter.Serialize<BoxGroupRequest>(groupRequest));

            IBoxResponse<BoxGroup> response = await ToResponseAsync<BoxGroup>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Add a user to a group.
        /// </summary>
        /// <param name="membershipRequest">BoxGroupMembershipRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The group membership created.</returns>
        public async Task<BoxGroupMembership> AddMemberToGroupAsync(BoxGroupMembershipRequest membershipRequest, IEnumerable<string> fields = null)
        {
            membershipRequest.ThrowIfNull("membershipRequest")
                .Group.Id.ThrowIfNullOrWhiteSpace("Group.Id");
            membershipRequest.User.Id.ThrowIfNullOrWhiteSpace("User.Id");

            BoxRequest request = new BoxRequest(_config.GroupMembershipEndpointUri)
                .Method(RequestMethod.Post)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize<BoxGroupMembershipRequest>(membershipRequest));

            IBoxResponse<BoxGroupMembership> response = await ToResponseAsync<BoxGroupMembership>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Delete a group membership.
        /// </summary>
        /// <param name="id">The id of the groupmembership to delete.</param>
        /// <returns>True if delete was successful.</returns>
        public async Task<bool> DeleteGroupMembershipAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.GroupMembershipEndpointUri, id)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxGroup> response = await ToResponseAsync<BoxGroup>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Retrieves all of the group collaborations for a given group. Note this is only available to group admins.
        /// </summary>
        /// <param name="groupId">The id of the group to get the list of collaborations for.</param>
        /// <param name="limit">The number of results to return with this request. Refer to the Box API for defaults.</param>
        /// <param name="offset">The offset of the results. Refer to the Box API for more details.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all group collaborations; defaults to false.</param>
        /// <returns>A collection of collaborations for the specified group id.</returns>
        public async Task<BoxCollection<BoxCollaboration>> GetCollaborationsForGroupAsync(string groupId, int? limit = null, int? offset = null,
                                                                                          IEnumerable<string> fields = null, bool autoPaginate = false)
        {
            var request = new BoxRequest(_config.GroupsEndpointUri, string.Format(Constants.CollaborationsPathString, groupId))
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString()); ;

            if (autoPaginate)
            {
                if (!limit.HasValue)
                {
                    limit = 100;
                    request.Param("limit", limit.ToString());
                }
                if (!offset.HasValue)
                    request.Param("offset", "0");

                return await AutoPaginateLimitOffset<BoxCollaboration>(request, limit.Value).ConfigureAwait(false);
            }
            else
            {
                var response = await ToResponseAsync<BoxCollection<BoxCollaboration>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Get the list of group memberships for a given group.
        /// </summary>
        /// <param name="groupId">The id of the group to get the list of memberships for.</param>
        /// <param name="limit">The number of results to return with this request. Refer to the Box API for defaults.</param>
        /// <param name="offset">The offset of the results. Refer to the Box API for more details.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all group memberships; defaults to false.</param>
        /// <returns>A collection of group memberships for the specified group id.</returns>
        public async Task<BoxCollection<BoxGroupMembership>> GetAllGroupMembershipsForGroupAsync(string groupId, int? limit = null, int? offset = null,
                                                                                                 IEnumerable<string> fields = null, bool autoPaginate = false)
        {
            groupId.ThrowIfNullOrWhiteSpace("groupId");

            BoxRequest request = new BoxRequest(_config.GroupsEndpointUri, string.Format(Constants.GroupMembershipPathString, groupId))
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString());

            if (autoPaginate)
            {
                if (!limit.HasValue)
                {
                    limit = 100;
                    request.Param("limit", limit.ToString());
                }
                if (!offset.HasValue)
                    request.Param("offset", "0");

                return await AutoPaginateLimitOffset<BoxGroupMembership>(request, limit.Value).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollection<BoxGroupMembership>> response = await ToResponseAsync<BoxCollection<BoxGroupMembership>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Get the list of group memberships for a given user.
        /// </summary>
        /// <param name="userId">The id of the user to get the list of memberships for.</param>
        /// <param name="limit">The number of results to return with this request. Refer to the Box API for defaults.</param>
        /// <param name="offset">The offset of the results. Refer to the Box API for more details.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all group memberships; defaults to false.</param>
        /// <returns>A collection of group memberships for the specified user id.</returns>
        public async Task<BoxCollection<BoxGroupMembership>> GetAllGroupMembershipsForUserAsync(string userId, int? limit = null, int? offset = null,
                                                                                                IEnumerable<string> fields = null, bool autoPaginate = false)
        {
            userId.ThrowIfNullOrWhiteSpace("userId");

            BoxRequest request = new BoxRequest(_config.UserEndpointUri, string.Format(Constants.GroupMembershipPathString, userId))
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString());

            if (autoPaginate)
            {
                if (!limit.HasValue)
                {
                    limit = 100;
                    request.Param("limit", limit.ToString());
                }
                if (!offset.HasValue)
                {
                    request.Param("offset", "0");
                }

                return await AutoPaginateLimitOffset<BoxGroupMembership>(request, limit.Value).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollection<BoxGroupMembership>> response = await ToResponseAsync<BoxCollection<BoxGroupMembership>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Returns the group membership with the specified id.
        /// </summary>
        /// <param name="id">The id of the group membership to return.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>Group membership with id='id'.</returns>
        public async Task<BoxGroupMembership> GetGroupMembershipAsync(string id, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.GroupMembershipEndpointUri, id)
                .Param(ParamFields, fields);

            IBoxResponse<BoxGroupMembership> response = await ToResponseAsync<BoxGroupMembership>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Updates the specified group membership with the specified request.
        /// </summary>
        /// <param name="membershipId">It of the group membership to update.</param>
        /// <param name="memRequest">BoxGroupMembershipRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The updated group membership.</returns>
        public async Task<BoxGroupMembership> UpdateGroupMembershipAsync(string membershipId, BoxGroupMembershipRequest memRequest, IEnumerable<string> fields = null)
        {
            membershipId.ThrowIfNullOrWhiteSpace("membershipId");
            memRequest.ThrowIfNull("memRequest").Role.ThrowIfNullOrWhiteSpace("role");

            BoxRequest request = new BoxRequest(_config.GroupMembershipEndpointUri, membershipId)
                .Param(ParamFields, fields)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize<BoxGroupMembershipRequest>(memRequest));

            IBoxResponse<BoxGroupMembership> response = await ToResponseAsync<BoxGroupMembership>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }
    }
}
