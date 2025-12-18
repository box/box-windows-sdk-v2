using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System.Collections.ObjectModel;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IMetadataTaxonomiesManager {
        /// <summary>
    /// Creates a new metadata taxonomy that can be used in
    /// metadata templates.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createMetadataTaxonomy method
    /// </param>
    /// <param name="headers">
    /// Headers of createMetadataTaxonomy method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataTaxonomy> CreateMetadataTaxonomyAsync(CreateMetadataTaxonomyRequestBody requestBody, CreateMetadataTaxonomyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Used to retrieve all metadata taxonomies in a namespace.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getMetadataTaxonomies method
    /// </param>
    /// <param name="headers">
    /// Headers of getMetadataTaxonomies method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataTaxonomies> GetMetadataTaxonomiesAsync(string namespaceParam, GetMetadataTaxonomiesQueryParams? queryParams = default, GetMetadataTaxonomiesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Used to retrieve a metadata taxonomy by taxonomy key.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="taxonomyKey">
    /// The key of the metadata taxonomy.
    /// Example: "geography"
    /// </param>
    /// <param name="headers">
    /// Headers of getMetadataTaxonomyByKey method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataTaxonomy> GetMetadataTaxonomyByKeyAsync(string namespaceParam, string taxonomyKey, GetMetadataTaxonomyByKeyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates an existing metadata taxonomy.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="taxonomyKey">
    /// The key of the metadata taxonomy.
    /// Example: "geography"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateMetadataTaxonomy method
    /// </param>
    /// <param name="headers">
    /// Headers of updateMetadataTaxonomy method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataTaxonomy> UpdateMetadataTaxonomyAsync(string namespaceParam, string taxonomyKey, UpdateMetadataTaxonomyRequestBody requestBody, UpdateMetadataTaxonomyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Delete a metadata taxonomy.
    /// This deletion is permanent and cannot be reverted.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="taxonomyKey">
    /// The key of the metadata taxonomy.
    /// Example: "geography"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteMetadataTaxonomy method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteMetadataTaxonomyAsync(string namespaceParam, string taxonomyKey, DeleteMetadataTaxonomyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates new metadata taxonomy levels.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="taxonomyKey">
    /// The key of the metadata taxonomy.
    /// Example: "geography"
    /// </param>
    /// <param name="requestBody">
    /// Request body of createMetadataTaxonomyLevel method
    /// </param>
    /// <param name="headers">
    /// Headers of createMetadataTaxonomyLevel method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataTaxonomyLevels> CreateMetadataTaxonomyLevelAsync(string namespaceParam, string taxonomyKey, IReadOnlyList<MetadataTaxonomyLevel> requestBody, CreateMetadataTaxonomyLevelHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates an existing metadata taxonomy level.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="taxonomyKey">
    /// The key of the metadata taxonomy.
    /// Example: "geography"
    /// </param>
    /// <param name="levelIndex">
    /// The index of the metadata taxonomy level.
    /// Example: 1
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateMetadataTaxonomyLevelById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateMetadataTaxonomyLevelById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataTaxonomyLevel> UpdateMetadataTaxonomyLevelByIdAsync(string namespaceParam, string taxonomyKey, long levelIndex, UpdateMetadataTaxonomyLevelByIdRequestBody requestBody, UpdateMetadataTaxonomyLevelByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a new metadata taxonomy level and appends it to the existing levels.
    /// If there are no levels defined yet, this will create the first level.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="taxonomyKey">
    /// The key of the metadata taxonomy.
    /// Example: "geography"
    /// </param>
    /// <param name="requestBody">
    /// Request body of addMetadataTaxonomyLevel method
    /// </param>
    /// <param name="headers">
    /// Headers of addMetadataTaxonomyLevel method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataTaxonomyLevels> AddMetadataTaxonomyLevelAsync(string namespaceParam, string taxonomyKey, AddMetadataTaxonomyLevelRequestBody requestBody, AddMetadataTaxonomyLevelHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes the last level of the metadata taxonomy.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="taxonomyKey">
    /// The key of the metadata taxonomy.
    /// Example: "geography"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteMetadataTaxonomyLevel method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataTaxonomyLevels> DeleteMetadataTaxonomyLevelAsync(string namespaceParam, string taxonomyKey, DeleteMetadataTaxonomyLevelHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Used to retrieve metadata taxonomy nodes based on the parameters specified. 
    /// Results are sorted in lexicographic order unless a `query` parameter is passed. 
    /// With a `query` parameter specified, results are sorted in order of relevance.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="taxonomyKey">
    /// The key of the metadata taxonomy.
    /// Example: "geography"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getMetadataTaxonomyNodes method
    /// </param>
    /// <param name="headers">
    /// Headers of getMetadataTaxonomyNodes method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataTaxonomyNodes> GetMetadataTaxonomyNodesAsync(string namespaceParam, string taxonomyKey, GetMetadataTaxonomyNodesQueryParams? queryParams = default, GetMetadataTaxonomyNodesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a new metadata taxonomy node.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="taxonomyKey">
    /// The key of the metadata taxonomy.
    /// Example: "geography"
    /// </param>
    /// <param name="requestBody">
    /// Request body of createMetadataTaxonomyNode method
    /// </param>
    /// <param name="headers">
    /// Headers of createMetadataTaxonomyNode method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataTaxonomyNode> CreateMetadataTaxonomyNodeAsync(string namespaceParam, string taxonomyKey, CreateMetadataTaxonomyNodeRequestBody requestBody, CreateMetadataTaxonomyNodeHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a metadata taxonomy node by its identifier.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="taxonomyKey">
    /// The key of the metadata taxonomy.
    /// Example: "geography"
    /// </param>
    /// <param name="nodeId">
    /// The identifier of the metadata taxonomy node.
    /// Example: "14d3d433-c77f-49c5-b146-9dea370f6e32"
    /// </param>
    /// <param name="headers">
    /// Headers of getMetadataTaxonomyNodeById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataTaxonomyNode> GetMetadataTaxonomyNodeByIdAsync(string namespaceParam, string taxonomyKey, string nodeId, GetMetadataTaxonomyNodeByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates an existing metadata taxonomy node.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="taxonomyKey">
    /// The key of the metadata taxonomy.
    /// Example: "geography"
    /// </param>
    /// <param name="nodeId">
    /// The identifier of the metadata taxonomy node.
    /// Example: "14d3d433-c77f-49c5-b146-9dea370f6e32"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateMetadataTaxonomyNode method
    /// </param>
    /// <param name="headers">
    /// Headers of updateMetadataTaxonomyNode method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataTaxonomyNode> UpdateMetadataTaxonomyNodeAsync(string namespaceParam, string taxonomyKey, string nodeId, UpdateMetadataTaxonomyNodeRequestBody? requestBody = default, UpdateMetadataTaxonomyNodeHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Delete a metadata taxonomy node.
    /// This deletion is permanent and cannot be reverted.
    /// Only metadata taxonomy nodes without any children can be deleted.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="taxonomyKey">
    /// The key of the metadata taxonomy.
    /// Example: "geography"
    /// </param>
    /// <param name="nodeId">
    /// The identifier of the metadata taxonomy node.
    /// Example: "14d3d433-c77f-49c5-b146-9dea370f6e32"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteMetadataTaxonomyNode method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteMetadataTaxonomyNodeAsync(string namespaceParam, string taxonomyKey, string nodeId, DeleteMetadataTaxonomyNodeHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Used to retrieve metadata taxonomy nodes which are available for the taxonomy field based 
    /// on its configuration and the parameters specified. 
    /// Results are sorted in lexicographic order unless a `query` parameter is passed. 
    /// With a `query` parameter specified, results are sorted in order of relevance.
    /// </summary>
    /// <param name="namespaceParam">
    /// The namespace of the metadata taxonomy.
    /// Example: "enterprise_123456"
    /// </param>
    /// <param name="templateKey">
    /// The name of the metadata template.
    /// Example: "properties"
    /// </param>
    /// <param name="fieldKey">
    /// The key of the metadata taxonomy field in the template.
    /// Example: "geography"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getMetadataTemplateFieldOptions method
    /// </param>
    /// <param name="headers">
    /// Headers of getMetadataTemplateFieldOptions method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataTaxonomyNodes> GetMetadataTemplateFieldOptionsAsync(string namespaceParam, string templateKey, string fieldKey, GetMetadataTemplateFieldOptionsQueryParams? queryParams = default, GetMetadataTemplateFieldOptionsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}