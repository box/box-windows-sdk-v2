using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IRetentionPolicyAssignmentsManager {
        /// <summary>
    /// Returns a list of all retention policy assignments associated with a specified
    /// retention policy.
    /// </summary>
    /// <param name="retentionPolicyId">
    /// The ID of the retention policy.
    /// Example: "982312"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getRetentionPolicyAssignments method
    /// </param>
    /// <param name="headers">
    /// Headers of getRetentionPolicyAssignments method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<RetentionPolicyAssignments> GetRetentionPolicyAssignmentsAsync(string retentionPolicyId, GetRetentionPolicyAssignmentsQueryParams? queryParams = default, GetRetentionPolicyAssignmentsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Assigns a retention policy to an item.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createRetentionPolicyAssignment method
    /// </param>
    /// <param name="headers">
    /// Headers of createRetentionPolicyAssignment method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<RetentionPolicyAssignment> CreateRetentionPolicyAssignmentAsync(CreateRetentionPolicyAssignmentRequestBody requestBody, CreateRetentionPolicyAssignmentHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a retention policy assignment.
    /// </summary>
    /// <param name="retentionPolicyAssignmentId">
    /// The ID of the retention policy assignment.
    /// Example: "1233123"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getRetentionPolicyAssignmentById method
    /// </param>
    /// <param name="headers">
    /// Headers of getRetentionPolicyAssignmentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<RetentionPolicyAssignment> GetRetentionPolicyAssignmentByIdAsync(string retentionPolicyAssignmentId, GetRetentionPolicyAssignmentByIdQueryParams? queryParams = default, GetRetentionPolicyAssignmentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Removes a retention policy assignment
    /// applied to content.
    /// </summary>
    /// <param name="retentionPolicyAssignmentId">
    /// The ID of the retention policy assignment.
    /// Example: "1233123"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteRetentionPolicyAssignmentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteRetentionPolicyAssignmentByIdAsync(string retentionPolicyAssignmentId, DeleteRetentionPolicyAssignmentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Returns a list of files under retention for a retention policy assignment.
    /// </summary>
    /// <param name="retentionPolicyAssignmentId">
    /// The ID of the retention policy assignment.
    /// Example: "1233123"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getFilesUnderRetentionPolicyAssignment method
    /// </param>
    /// <param name="headers">
    /// Headers of getFilesUnderRetentionPolicyAssignment method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<FilesUnderRetention> GetFilesUnderRetentionPolicyAssignmentAsync(string retentionPolicyAssignmentId, GetFilesUnderRetentionPolicyAssignmentQueryParams? queryParams = default, GetFilesUnderRetentionPolicyAssignmentHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}