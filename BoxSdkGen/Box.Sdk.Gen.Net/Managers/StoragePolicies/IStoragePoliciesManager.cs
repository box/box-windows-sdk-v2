using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IStoragePoliciesManager {
        /// <summary>
    /// Fetches all the storage policies in the enterprise.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getStoragePolicies method
    /// </param>
    /// <param name="headers">
    /// Headers of getStoragePolicies method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<StoragePolicies> GetStoragePoliciesAsync(GetStoragePoliciesQueryParams? queryParams = default, GetStoragePoliciesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Fetches a specific storage policy.
    /// </summary>
    /// <param name="storagePolicyId">
    /// The ID of the storage policy.
    /// Example: "34342"
    /// </param>
    /// <param name="headers">
    /// Headers of getStoragePolicyById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<StoragePolicy> GetStoragePolicyByIdAsync(string storagePolicyId, GetStoragePolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}