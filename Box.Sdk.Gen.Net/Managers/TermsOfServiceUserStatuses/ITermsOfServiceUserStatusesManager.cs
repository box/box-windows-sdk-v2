using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ITermsOfServiceUserStatusesManager {
        /// <summary>
    /// Retrieves an overview of users and their status for a
    /// terms of service, including Whether they have accepted
    /// the terms and when.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getTermsOfServiceUserStatuses method
    /// </param>
    /// <param name="headers">
    /// Headers of getTermsOfServiceUserStatuses method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TermsOfServiceUserStatuses> GetTermsOfServiceUserStatusesAsync(GetTermsOfServiceUserStatusesQueryParams queryParams, GetTermsOfServiceUserStatusesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Sets the status for a terms of service for a user.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createTermsOfServiceStatusForUser method
    /// </param>
    /// <param name="headers">
    /// Headers of createTermsOfServiceStatusForUser method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TermsOfServiceUserStatus> CreateTermsOfServiceStatusForUserAsync(CreateTermsOfServiceStatusForUserRequestBody requestBody, CreateTermsOfServiceStatusForUserHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates the status for a terms of service for a user.
    /// </summary>
    /// <param name="termsOfServiceUserStatusId">
    /// The ID of the terms of service status.
    /// Example: "324234"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateTermsOfServiceStatusForUserById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateTermsOfServiceStatusForUserById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TermsOfServiceUserStatus> UpdateTermsOfServiceStatusForUserByIdAsync(string termsOfServiceUserStatusId, UpdateTermsOfServiceStatusForUserByIdRequestBody requestBody, UpdateTermsOfServiceStatusForUserByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}