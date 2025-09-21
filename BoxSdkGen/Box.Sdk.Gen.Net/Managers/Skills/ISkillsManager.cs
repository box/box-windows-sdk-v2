using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ISkillsManager {
        /// <summary>
    /// List the Box Skills metadata cards that are attached to a file.
    /// </summary>
    /// <param name="fileId">
    /// The unique identifier that represents a file.
    /// 
    /// The ID for any file can be determined
    /// by visiting a file in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/files/123`
    /// the `file_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of getBoxSkillCardsOnFile method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<SkillCardsMetadata> GetBoxSkillCardsOnFileAsync(string fileId, GetBoxSkillCardsOnFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Applies one or more Box Skills metadata cards to a file.
    /// </summary>
    /// <param name="fileId">
    /// The unique identifier that represents a file.
    /// 
    /// The ID for any file can be determined
    /// by visiting a file in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/files/123`
    /// the `file_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of createBoxSkillCardsOnFile method
    /// </param>
    /// <param name="headers">
    /// Headers of createBoxSkillCardsOnFile method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<SkillCardsMetadata> CreateBoxSkillCardsOnFileAsync(string fileId, CreateBoxSkillCardsOnFileRequestBody requestBody, CreateBoxSkillCardsOnFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates one or more Box Skills metadata cards to a file.
    /// </summary>
    /// <param name="fileId">
    /// The unique identifier that represents a file.
    /// 
    /// The ID for any file can be determined
    /// by visiting a file in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/files/123`
    /// the `file_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateBoxSkillCardsOnFile method
    /// </param>
    /// <param name="headers">
    /// Headers of updateBoxSkillCardsOnFile method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<SkillCardsMetadata> UpdateBoxSkillCardsOnFileAsync(string fileId, IReadOnlyList<UpdateBoxSkillCardsOnFileRequestBody> requestBody, UpdateBoxSkillCardsOnFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Removes any Box Skills cards metadata from a file.
    /// </summary>
    /// <param name="fileId">
    /// The unique identifier that represents a file.
    /// 
    /// The ID for any file can be determined
    /// by visiting a file in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/files/123`
    /// the `file_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteBoxSkillCardsFromFile method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteBoxSkillCardsFromFileAsync(string fileId, DeleteBoxSkillCardsFromFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// An alternative method that can be used to overwrite and update all Box Skill
    /// metadata cards on a file.
    /// </summary>
    /// <param name="skillId">
    /// The ID of the skill to apply this metadata for.
    /// Example: "33243242"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateAllSkillCardsOnFile method
    /// </param>
    /// <param name="headers">
    /// Headers of updateAllSkillCardsOnFile method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task UpdateAllSkillCardsOnFileAsync(string skillId, UpdateAllSkillCardsOnFileRequestBody requestBody, UpdateAllSkillCardsOnFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}