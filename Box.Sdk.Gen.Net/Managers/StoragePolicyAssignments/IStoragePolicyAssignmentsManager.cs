using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IStoragePolicyAssignmentsManager {
        /// <summary>
    /// Fetches all the storage policy assignment for an enterprise or user.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getStoragePolicyAssignments method
    /// </param>
    /// <param name="headers">
    /// Headers of getStoragePolicyAssignments method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<StoragePolicyAssignments> GetStoragePolicyAssignmentsAsync(GetStoragePolicyAssignmentsQueryParams queryParams, GetStoragePolicyAssignmentsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a storage policy assignment for an enterprise or user.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createStoragePolicyAssignment method
    /// </param>
    /// <param name="headers">
    /// Headers of createStoragePolicyAssignment method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<StoragePolicyAssignment> CreateStoragePolicyAssignmentAsync(CreateStoragePolicyAssignmentRequestBody requestBody, CreateStoragePolicyAssignmentHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Fetches a specific storage policy assignment.
    /// </summary>
    /// <param name="storagePolicyAssignmentId">
    /// The ID of the storage policy assignment.
    /// Example: "932483"
    /// </param>
    /// <param name="headers">
    /// Headers of getStoragePolicyAssignmentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<StoragePolicyAssignment> GetStoragePolicyAssignmentByIdAsync(string storagePolicyAssignmentId, GetStoragePolicyAssignmentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a specific storage policy assignment.
    /// </summary>
    /// <param name="storagePolicyAssignmentId">
    /// The ID of the storage policy assignment.
    /// Example: "932483"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateStoragePolicyAssignmentById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateStoragePolicyAssignmentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<StoragePolicyAssignment> UpdateStoragePolicyAssignmentByIdAsync(string storagePolicyAssignmentId, UpdateStoragePolicyAssignmentByIdRequestBody requestBody, UpdateStoragePolicyAssignmentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Delete a storage policy assignment.
    /// 
    /// Deleting a storage policy assignment on a user
    /// will have the user inherit the enterprise's default
    /// storage policy.
    /// 
    /// There is a rate limit for calling this endpoint of only
    /// twice per user in a 24 hour time frame.
    /// </summary>
    /// <param name="storagePolicyAssignmentId">
    /// The ID of the storage policy assignment.
    /// Example: "932483"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteStoragePolicyAssignmentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteStoragePolicyAssignmentByIdAsync(string storagePolicyAssignmentId, DeleteStoragePolicyAssignmentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}