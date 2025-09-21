using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IMembershipsManager {
        /// <summary>
    /// Retrieves all the groups for a user. Only members of this
    /// group or users with admin-level permissions will be able to
    /// use this API.
    /// </summary>
    /// <param name="userId">
    /// The ID of the user.
    /// Example: "12345"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getUserMemberships method
    /// </param>
    /// <param name="headers">
    /// Headers of getUserMemberships method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<GroupMemberships> GetUserMembershipsAsync(string userId, GetUserMembershipsQueryParams? queryParams = default, GetUserMembershipsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves all the members for a group. Only members of this
    /// group or users with admin-level permissions will be able to
    /// use this API.
    /// </summary>
    /// <param name="groupId">
    /// The ID of the group.
    /// Example: "57645"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getGroupMemberships method
    /// </param>
    /// <param name="headers">
    /// Headers of getGroupMemberships method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<GroupMemberships> GetGroupMembershipsAsync(string groupId, GetGroupMembershipsQueryParams? queryParams = default, GetGroupMembershipsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a group membership. Only users with
    /// admin-level permissions will be able to use this API.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createGroupMembership method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of createGroupMembership method
    /// </param>
    /// <param name="headers">
    /// Headers of createGroupMembership method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<GroupMembership> CreateGroupMembershipAsync(CreateGroupMembershipRequestBody requestBody, CreateGroupMembershipQueryParams? queryParams = default, CreateGroupMembershipHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a specific group membership. Only admins of this
    /// group or users with admin-level permissions will be able to
    /// use this API.
    /// </summary>
    /// <param name="groupMembershipId">
    /// The ID of the group membership.
    /// Example: "434534"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getGroupMembershipById method
    /// </param>
    /// <param name="headers">
    /// Headers of getGroupMembershipById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<GroupMembership> GetGroupMembershipByIdAsync(string groupMembershipId, GetGroupMembershipByIdQueryParams? queryParams = default, GetGroupMembershipByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a user's group membership. Only admins of this
    /// group or users with admin-level permissions will be able to
    /// use this API.
    /// </summary>
    /// <param name="groupMembershipId">
    /// The ID of the group membership.
    /// Example: "434534"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateGroupMembershipById method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of updateGroupMembershipById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateGroupMembershipById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<GroupMembership> UpdateGroupMembershipByIdAsync(string groupMembershipId, UpdateGroupMembershipByIdRequestBody? requestBody = default, UpdateGroupMembershipByIdQueryParams? queryParams = default, UpdateGroupMembershipByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a specific group membership. Only admins of this
    /// group or users with admin-level permissions will be able to
    /// use this API.
    /// </summary>
    /// <param name="groupMembershipId">
    /// The ID of the group membership.
    /// Example: "434534"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteGroupMembershipById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteGroupMembershipByIdAsync(string groupMembershipId, DeleteGroupMembershipByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}