using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IGroupsManager {
        /// <summary>
    /// Retrieves all of the groups for a given enterprise. The user
    /// must have admin permissions to inspect enterprise's groups.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getGroups method
    /// </param>
    /// <param name="headers">
    /// Headers of getGroups method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Groups> GetGroupsAsync(GetGroupsQueryParams? queryParams = default, GetGroupsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a new group of users in an enterprise. Only users with admin
    /// permissions can create new groups.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createGroup method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of createGroup method
    /// </param>
    /// <param name="headers">
    /// Headers of createGroup method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<GroupFull> CreateGroupAsync(CreateGroupRequestBody requestBody, CreateGroupQueryParams? queryParams = default, CreateGroupHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves information about a group. Only members of this
    /// group or users with admin-level permissions will be able to
    /// use this API.
    /// </summary>
    /// <param name="groupId">
    /// The ID of the group.
    /// Example: "57645"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getGroupById method
    /// </param>
    /// <param name="headers">
    /// Headers of getGroupById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<GroupFull> GetGroupByIdAsync(string groupId, GetGroupByIdQueryParams? queryParams = default, GetGroupByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a specific group. Only admins of this
    /// group or users with admin-level permissions will be able to
    /// use this API.
    /// </summary>
    /// <param name="groupId">
    /// The ID of the group.
    /// Example: "57645"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateGroupById method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of updateGroupById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateGroupById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<GroupFull> UpdateGroupByIdAsync(string groupId, UpdateGroupByIdRequestBody? requestBody = default, UpdateGroupByIdQueryParams? queryParams = default, UpdateGroupByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Permanently deletes a group. Only users with
    /// admin-level permissions will be able to use this API.
    /// </summary>
    /// <param name="groupId">
    /// The ID of the group.
    /// Example: "57645"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteGroupById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteGroupByIdAsync(string groupId, DeleteGroupByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}