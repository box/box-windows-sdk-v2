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
    /// The user must already have a Box account and cannot be
    /// part of another enterprise. Once invited, the user receives
    /// an email and is prompted to accept the invitation in
    /// the Box web application.
    /// 
    /// If `is_auto_roll_in_enabled` is set to `true`, the user does not have to
    /// accept the invitation and is added to the enterprise automatically after
    /// 14 days. Only unmanaged users can be invited this way. An unmanaged user
    /// is a free user whose email domain matches one of the enterprise's
    /// domains. Availability depends on the enterprise's plan.
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