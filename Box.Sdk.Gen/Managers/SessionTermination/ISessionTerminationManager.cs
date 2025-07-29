using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ISessionTerminationManager {
        /// <summary>
    /// Validates the roles and permissions of the user,
    /// and creates asynchronous jobs
    /// to terminate the user's sessions.
    /// Returns the status for the POST request.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of terminateUsersSessions method
    /// </param>
    /// <param name="headers">
    /// Headers of terminateUsersSessions method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<SessionTerminationMessage> TerminateUsersSessionsAsync(TerminateUsersSessionsRequestBody requestBody, TerminateUsersSessionsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Validates the roles and permissions of the group,
    /// and creates asynchronous jobs
    /// to terminate the group's sessions.
    /// Returns the status for the POST request.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of terminateGroupsSessions method
    /// </param>
    /// <param name="headers">
    /// Headers of terminateGroupsSessions method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<SessionTerminationMessage> TerminateGroupsSessionsAsync(TerminateGroupsSessionsRequestBody requestBody, TerminateGroupsSessionsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}