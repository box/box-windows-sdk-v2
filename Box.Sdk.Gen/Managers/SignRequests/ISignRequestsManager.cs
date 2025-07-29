using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ISignRequestsManager {
        /// <summary>
    /// Cancels a sign request.
    /// </summary>
    /// <param name="signRequestId">
    /// The ID of the signature request.
    /// Example: "33243242"
    /// </param>
    /// <param name="headers">
    /// Headers of cancelSignRequest method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<SignRequest> CancelSignRequestAsync(string signRequestId, CancelSignRequestHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Resends a signature request email to all outstanding signers.
    /// </summary>
    /// <param name="signRequestId">
    /// The ID of the signature request.
    /// Example: "33243242"
    /// </param>
    /// <param name="headers">
    /// Headers of resendSignRequest method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task ResendSignRequestAsync(string signRequestId, ResendSignRequestHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Gets a sign request by ID.
    /// </summary>
    /// <param name="signRequestId">
    /// The ID of the signature request.
    /// Example: "33243242"
    /// </param>
    /// <param name="headers">
    /// Headers of getSignRequestById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<SignRequest> GetSignRequestByIdAsync(string signRequestId, GetSignRequestByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Gets signature requests created by a user. If the `sign_files` and/or
    /// `parent_folder` are deleted, the signature request will not return in the list.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getSignRequests method
    /// </param>
    /// <param name="headers">
    /// Headers of getSignRequests method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<SignRequests> GetSignRequestsAsync(GetSignRequestsQueryParams? queryParams = default, GetSignRequestsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a signature request. This involves preparing a document for signing and
    /// sending the signature request to signers.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createSignRequest method
    /// </param>
    /// <param name="headers">
    /// Headers of createSignRequest method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<SignRequest> CreateSignRequestAsync(SignRequestCreateRequest requestBody, CreateSignRequestHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}