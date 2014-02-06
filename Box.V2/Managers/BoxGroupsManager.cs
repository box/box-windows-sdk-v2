using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    /// <summary>
    /// The class managing the Box API's Groups endpoint
    /// </summary>
    public class BoxGroupsManager : BoxResourceManager
    {
        /// <summary>
        /// Create a new Boxgroupmanager object
        /// </summary>
        /// <param name="config"></param>
        /// <param name="service"></param>
        /// <param name="converter"></param>
        /// <param name="auth"></param>
        public BoxGroupsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth) 
            : base(config, service, converter, auth) { }

        /// <summary>
        /// Returns all the groups created by the current user
        /// </summary>
        /// <param name="limit">The number of results to return with this request. Refer to the Box API for defaults.</param>
        /// <param name="offset">The offset of the results. Refer to the Box API for more details.</param>
        /// <param name="fields">The fields to return for this request.</param>
        /// <returns>A collection of groups</returns>
        public async Task<BoxCollection<BoxGroup>> GetAllGroupsAsync(int? limit = null, int? offset = null, List<string> fields = null)
        {
            BoxRequest request = new BoxRequest(_config.GroupsEndpointUri)
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString());

            IBoxResponse<BoxCollection<BoxGroup>> response = await ToResponseAsync<BoxCollection<BoxGroup>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Returns the group with the specified id
        /// </summary>
        /// <param name="id">The id of the group to return</param>
        /// <param name="fields">The fields to return for this request.</param>
        /// <returns>Group with id='id'</returns>
        public async Task<BoxGroup> GetGroupAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.GroupsEndpointUri, id)
                .Param(ParamFields, fields);

            IBoxResponse<BoxGroup> response = await ToResponseAsync<BoxGroup>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Create a new group
        /// </summary>
        /// <param name="groupRequest">The request that contains the name of the group to create</param>
        /// <param name="fields">Optional fields to return</param>
        /// <returns>The newly created group</returns>
        public async Task<BoxGroup> CreateAsync(BoxGroupRequest groupRequest, List<string> fields = null)
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
        /// Delete an existing group
        /// </summary>
        /// <param name="id">The id of the group to delete</param>
        /// <returns>True if delete was successful</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.GroupsEndpointUri, id)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxGroup> response = await ToResponseAsync<BoxGroup>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Update an existing group
        /// </summary>
        /// <param name="id">Id of the group to update</param>
        /// <param name="groupRequest">Request containing the update, e.g. updated name</param>
        /// <param name="fields">Optional fields to return</param>
        /// <returns>The updated group</returns>
        public async Task<BoxGroup> UpdateAsync(string id, BoxGroupRequest groupRequest, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");
            groupRequest.ThrowIfNull("groupRequest").Name.ThrowIfNullOrWhiteSpace("groupRequest.Name");

            BoxRequest request = new BoxRequest(_config.GroupsEndpointUri, id).
                Method(RequestMethod.Put).
                Param(ParamFields, fields).
                Payload(_converter.Serialize<BoxGroupRequest>(groupRequest));

            IBoxResponse<BoxGroup> response = await ToResponseAsync<BoxGroup>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Add a user to a group
        /// </summary>
        /// <param name="membershipRequest">The request object that contains the user Id and group Id</param>
        /// <param name="fields">Optional fields to return</param>
        /// <returns>The group membership created</returns>
        public async Task<BoxGroupMembership> AddMemberToGroupAsync(BoxGroupMembershipRequest membershipRequest, List<string> fields = null) 
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
        /// Delete a group membership
        /// </summary>
        /// <param name="id">The id of the groupmembership to delete</param>
        /// <returns>True if delete was successful</returns>
        public async Task<bool> DeleteGroupMembershipAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.GroupMembershipEndpointUri, id)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxGroup> response = await ToResponseAsync<BoxGroup>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Get the list of group memberships for a given group
        /// </summary>
        /// <param name="groupId">The id of the group to get the list of memberships for</param>
        /// <param name="limit">The number of results to return with this request. Refer to the Box API for defaults.</param>
        /// <param name="offset">The offset of the results. Refer to the Box API for more details.</param>
        /// <param name="fields">The fields to return for this request.</param>
        /// <returns>A collection of group memberships for the specified group id</returns>
        public async Task<BoxCollection<BoxGroupMembership>> GetAllGroupMembershipsForGroupAsync(string groupId, int? limit = null, int? offset = null, List<string> fields = null)
        {
            groupId.ThrowIfNullOrWhiteSpace("groupId");

            BoxRequest request = new BoxRequest(_config.GroupsEndpointUri, string.Format(Constants.GroupMembershipPathString, groupId))
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString()); ;

            IBoxResponse<BoxCollection<BoxGroupMembership>> response = await ToResponseAsync<BoxCollection<BoxGroupMembership>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Get the list of group memberships for a given user
        /// </summary>
        /// <param name="userId">The id of the user to get the list of memberships for</param>
        /// <param name="limit">The number of results to return with this request. Refer to the Box API for defaults.</param>
        /// <param name="offset">The offset of the results. Refer to the Box API for more details.</param>
        /// <param name="fields">The optional fields to return for this request.</param>
        /// <returns>A collection of group memberships for the specified user id</returns>
        public async Task<BoxCollection<BoxGroupMembership>> GetAllGroupMembershipsForUserAsync(string userId, int? limit = null, int? offset = null, List<string> fields = null)
        {
            userId.ThrowIfNullOrWhiteSpace("userId");

            BoxRequest request = new BoxRequest(_config.UserEndpointUri, string.Format(Constants.GroupMembershipPathString, userId))
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString()); ;

            IBoxResponse<BoxCollection<BoxGroupMembership>> response = await ToResponseAsync<BoxCollection<BoxGroupMembership>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Returns the group membership with the specified id
        /// </summary>
        /// <param name="id">The id of the group membership to return.</param>
        /// <param name="fields">The fields to return for this request.</param>
        /// <returns>Group membership with id='id'</returns>
        public async Task<BoxGroupMembership> GetGroupMembershipAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.GroupMembershipEndpointUri, id)
                .Param(ParamFields, fields);

            IBoxResponse<BoxGroupMembership> response = await ToResponseAsync<BoxGroupMembership>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Updates the specified group membership with the specified request
        /// </summary>
        /// <param name="membershipId">It of the group membership to update</param>
        /// <param name="memRequest">The request specifying the update</param>
        /// <param name="fields">Optional fields to return</param>
        /// <returns>The updated group membership</returns>
        public async Task<BoxGroupMembership> UpdateGroupMembershipAsync(string membershipId, BoxGroupMembershipRequest memRequest, List<string> fields = null) 
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
