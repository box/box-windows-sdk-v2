using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ILegalHoldPoliciesManager {
        /// <summary>
    /// Retrieves a list of legal hold policies that belong to
    /// an enterprise.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getLegalHoldPolicies method
    /// </param>
    /// <param name="headers">
    /// Headers of getLegalHoldPolicies method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<LegalHoldPolicies> GetLegalHoldPoliciesAsync(GetLegalHoldPoliciesQueryParams? queryParams = default, GetLegalHoldPoliciesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Create a new legal hold policy.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createLegalHoldPolicy method
    /// </param>
    /// <param name="headers">
    /// Headers of createLegalHoldPolicy method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<LegalHoldPolicy> CreateLegalHoldPolicyAsync(CreateLegalHoldPolicyRequestBody requestBody, CreateLegalHoldPolicyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieve a legal hold policy.
    /// </summary>
    /// <param name="legalHoldPolicyId">
    /// The ID of the legal hold policy.
    /// Example: "324432"
    /// </param>
    /// <param name="headers">
    /// Headers of getLegalHoldPolicyById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<LegalHoldPolicy> GetLegalHoldPolicyByIdAsync(string legalHoldPolicyId, GetLegalHoldPolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Update legal hold policy.
    /// </summary>
    /// <param name="legalHoldPolicyId">
    /// The ID of the legal hold policy.
    /// Example: "324432"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateLegalHoldPolicyById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateLegalHoldPolicyById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<LegalHoldPolicy> UpdateLegalHoldPolicyByIdAsync(string legalHoldPolicyId, UpdateLegalHoldPolicyByIdRequestBody? requestBody = default, UpdateLegalHoldPolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Delete an existing legal hold policy.
    /// 
    /// This is an asynchronous process. The policy will not be
    /// fully deleted yet when the response returns.
    /// </summary>
    /// <param name="legalHoldPolicyId">
    /// The ID of the legal hold policy.
    /// Example: "324432"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteLegalHoldPolicyById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteLegalHoldPolicyByIdAsync(string legalHoldPolicyId, DeleteLegalHoldPolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}