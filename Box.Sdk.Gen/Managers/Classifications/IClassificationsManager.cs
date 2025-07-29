using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IClassificationsManager {
        /// <summary>
    /// Retrieves the classification metadata template and lists all the
    /// classifications available to this enterprise.
    /// 
    /// This API can also be called by including the enterprise ID in the
    /// URL explicitly, for example
    /// `/metadata_templates/enterprise_12345/securityClassification-6VMVochwUWo/schema`.
    /// </summary>
    /// <param name="headers">
    /// Headers of getClassificationTemplate method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ClassificationTemplate> GetClassificationTemplateAsync(GetClassificationTemplateHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Adds one or more new classifications to the list of classifications
    /// available to the enterprise.
    /// 
    /// This API can also be called by including the enterprise ID in the
    /// URL explicitly, for example
    /// `/metadata_templates/enterprise_12345/securityClassification-6VMVochwUWo/schema`.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of addClassification method
    /// </param>
    /// <param name="headers">
    /// Headers of addClassification method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ClassificationTemplate> AddClassificationAsync(IReadOnlyList<AddClassificationRequestBody> requestBody, AddClassificationHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates the labels and descriptions of one or more classifications
    /// available to the enterprise.
    /// 
    /// This API can also be called by including the enterprise ID in the
    /// URL explicitly, for example
    /// `/metadata_templates/enterprise_12345/securityClassification-6VMVochwUWo/schema`.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of updateClassification method
    /// </param>
    /// <param name="headers">
    /// Headers of updateClassification method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ClassificationTemplate> UpdateClassificationAsync(IReadOnlyList<UpdateClassificationRequestBody> requestBody, UpdateClassificationHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// When an enterprise does not yet have any classifications, this API call
    /// initializes the classification template with an initial set of
    /// classifications.
    /// 
    /// If an enterprise already has a classification, the template will already
    /// exist and instead an API call should be made to add additional
    /// classifications.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createClassificationTemplate method
    /// </param>
    /// <param name="headers">
    /// Headers of createClassificationTemplate method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ClassificationTemplate> CreateClassificationTemplateAsync(CreateClassificationTemplateRequestBody requestBody, CreateClassificationTemplateHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}