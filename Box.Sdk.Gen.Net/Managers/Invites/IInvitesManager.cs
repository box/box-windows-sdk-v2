using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IInvitesManager {
        /// <summary>
    /// Invites an existing external user to join an enterprise.
    /// 
    /// The existing user can not be part of another enterprise and
    /// must already have a Box account. Once invited, the user will receive an
    /// email and are prompted to accept the invitation within the
    /// Box web application.
    /// 
    /// This method requires the "Manage An Enterprise" scope enabled for
    /// the application, which can be enabled within the developer console.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createInvite method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of createInvite method
    /// </param>
    /// <param name="headers">
    /// Headers of createInvite method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Invite> CreateInviteAsync(CreateInviteRequestBody requestBody, CreateInviteQueryParams? queryParams = default, CreateInviteHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns the status of a user invite.
    /// </summary>
    /// <param name="inviteId">
    /// The ID of an invite.
    /// Example: "213723"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getInviteById method
    /// </param>
    /// <param name="headers">
    /// Headers of getInviteById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Invite> GetInviteByIdAsync(string inviteId, GetInviteByIdQueryParams? queryParams = default, GetInviteByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}