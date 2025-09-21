using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IRetentionPoliciesManager {
        /// <summary>
    /// Retrieves all of the retention policies for an enterprise.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getRetentionPolicies method
    /// </param>
    /// <param name="headers">
    /// Headers of getRetentionPolicies method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<RetentionPolicies> GetRetentionPoliciesAsync(GetRetentionPoliciesQueryParams? queryParams = default, GetRetentionPoliciesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a retention policy.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createRetentionPolicy method
    /// </param>
    /// <param name="headers">
    /// Headers of createRetentionPolicy method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<RetentionPolicy> CreateRetentionPolicyAsync(CreateRetentionPolicyRequestBody requestBody, CreateRetentionPolicyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a retention policy.
    /// </summary>
    /// <param name="retentionPolicyId">
    /// The ID of the retention policy.
    /// Example: "982312"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getRetentionPolicyById method
    /// </param>
    /// <param name="headers">
    /// Headers of getRetentionPolicyById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<RetentionPolicy> GetRetentionPolicyByIdAsync(string retentionPolicyId, GetRetentionPolicyByIdQueryParams? queryParams = default, GetRetentionPolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a retention policy.
    /// </summary>
    /// <param name="retentionPolicyId">
    /// The ID of the retention policy.
    /// Example: "982312"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateRetentionPolicyById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateRetentionPolicyById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<RetentionPolicy> UpdateRetentionPolicyByIdAsync(string retentionPolicyId, UpdateRetentionPolicyByIdRequestBody? requestBody = default, UpdateRetentionPolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Permanently deletes a retention policy.
    /// </summary>
    /// <param name="retentionPolicyId">
    /// The ID of the retention policy.
    /// Example: "982312"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteRetentionPolicyById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteRetentionPolicyByIdAsync(string retentionPolicyId, DeleteRetentionPolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}