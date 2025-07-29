using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IMetadataCascadePoliciesManager {
        /// <summary>
    /// Retrieves a list of all the metadata cascade policies
    /// that are applied to a given folder. This can not be used on the root
    /// folder with ID `0`.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getMetadataCascadePolicies method
    /// </param>
    /// <param name="headers">
    /// Headers of getMetadataCascadePolicies method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataCascadePolicies> GetMetadataCascadePoliciesAsync(GetMetadataCascadePoliciesQueryParams queryParams, GetMetadataCascadePoliciesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a new metadata cascade policy that applies a given
    /// metadata template to a given folder and automatically
    /// cascades it down to any files within that folder.
    /// 
    /// In order for the policy to be applied a metadata instance must first
    /// be applied to the folder the policy is to be applied to.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createMetadataCascadePolicy method
    /// </param>
    /// <param name="headers">
    /// Headers of createMetadataCascadePolicy method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataCascadePolicy> CreateMetadataCascadePolicyAsync(CreateMetadataCascadePolicyRequestBody requestBody, CreateMetadataCascadePolicyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieve a specific metadata cascade policy assigned to a folder.
    /// </summary>
    /// <param name="metadataCascadePolicyId">
    /// The ID of the metadata cascade policy.
    /// Example: "6fd4ff89-8fc1-42cf-8b29-1890dedd26d7"
    /// </param>
    /// <param name="headers">
    /// Headers of getMetadataCascadePolicyById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataCascadePolicy> GetMetadataCascadePolicyByIdAsync(string metadataCascadePolicyId, GetMetadataCascadePolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a metadata cascade policy.
    /// </summary>
    /// <param name="metadataCascadePolicyId">
    /// The ID of the metadata cascade policy.
    /// Example: "6fd4ff89-8fc1-42cf-8b29-1890dedd26d7"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteMetadataCascadePolicyById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteMetadataCascadePolicyByIdAsync(string metadataCascadePolicyId, DeleteMetadataCascadePolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Force the metadata on a folder with a metadata cascade policy to be applied to
    /// all of its children. This can be used after creating a new cascade policy to
    /// enforce the metadata to be cascaded down to all existing files within that
    /// folder.
    /// </summary>
    /// <param name="metadataCascadePolicyId">
    /// The ID of the cascade policy to force-apply.
    /// Example: "6fd4ff89-8fc1-42cf-8b29-1890dedd26d7"
    /// </param>
    /// <param name="requestBody">
    /// Request body of applyMetadataCascadePolicy method
    /// </param>
    /// <param name="headers">
    /// Headers of applyMetadataCascadePolicy method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task ApplyMetadataCascadePolicyAsync(string metadataCascadePolicyId, ApplyMetadataCascadePolicyRequestBody requestBody, ApplyMetadataCascadePolicyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}