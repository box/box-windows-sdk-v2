using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IFolderClassificationsManager {
        /// <summary>
    /// Retrieves the classification metadata instance that
    /// has been applied to a folder.
    /// 
    /// This API can also be called by including the enterprise ID in the
    /// URL explicitly, for example
    /// `/folders/:id/enterprise_12345/securityClassification-6VMVochwUWo`.
    /// </summary>
    /// <param name="folderId">
    /// The unique identifier that represent a folder.
    /// 
    /// The ID for any folder can be determined
    /// by visiting this folder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/folder/123`
    /// the `folder_id` is `123`.
    /// 
    /// The root folder of a Box account is
    /// always represented by the ID `0`.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of getClassificationOnFolder method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Classification> GetClassificationOnFolderAsync(string folderId, GetClassificationOnFolderHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Adds a classification to a folder by specifying the label of the
    /// classification to add.
    /// 
    /// This API can also be called by including the enterprise ID in the
    /// URL explicitly, for example
    /// `/folders/:id/enterprise_12345/securityClassification-6VMVochwUWo`.
    /// </summary>
    /// <param name="folderId">
    /// The unique identifier that represent a folder.
    /// 
    /// The ID for any folder can be determined
    /// by visiting this folder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/folder/123`
    /// the `folder_id` is `123`.
    /// 
    /// The root folder of a Box account is
    /// always represented by the ID `0`.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of addClassificationToFolder method
    /// </param>
    /// <param name="headers">
    /// Headers of addClassificationToFolder method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Classification> AddClassificationToFolderAsync(string folderId, AddClassificationToFolderRequestBody? requestBody = default, AddClassificationToFolderHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a classification on a folder.
    /// 
    /// The classification can only be updated if a classification has already been
    /// applied to the folder before. When editing classifications, only values are
    /// defined for the enterprise will be accepted.
    /// </summary>
    /// <param name="folderId">
    /// The unique identifier that represent a folder.
    /// 
    /// The ID for any folder can be determined
    /// by visiting this folder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/folder/123`
    /// the `folder_id` is `123`.
    /// 
    /// The root folder of a Box account is
    /// always represented by the ID `0`.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateClassificationOnFolder method
    /// </param>
    /// <param name="headers">
    /// Headers of updateClassificationOnFolder method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Classification> UpdateClassificationOnFolderAsync(string folderId, IReadOnlyList<UpdateClassificationOnFolderRequestBody> requestBody, UpdateClassificationOnFolderHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Removes any classifications from a folder.
    /// 
    /// This API can also be called by including the enterprise ID in the
    /// URL explicitly, for example
    /// `/folders/:id/enterprise_12345/securityClassification-6VMVochwUWo`.
    /// </summary>
    /// <param name="folderId">
    /// The unique identifier that represent a folder.
    /// 
    /// The ID for any folder can be determined
    /// by visiting this folder in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/folder/123`
    /// the `folder_id` is `123`.
    /// 
    /// The root folder of a Box account is
    /// always represented by the ID `0`.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteClassificationFromFolder method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteClassificationFromFolderAsync(string folderId, DeleteClassificationFromFolderHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}