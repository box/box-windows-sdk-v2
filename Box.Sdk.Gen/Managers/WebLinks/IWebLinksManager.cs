using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IWebLinksManager {
        /// <summary>
    /// Creates a web link object within a folder.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createWebLink method
    /// </param>
    /// <param name="headers">
    /// Headers of createWebLink method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<WebLink> CreateWebLinkAsync(CreateWebLinkRequestBody requestBody, CreateWebLinkHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieve information about a web link.
    /// </summary>
    /// <param name="webLinkId">
    /// The ID of the web link.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of getWebLinkById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<WebLink> GetWebLinkByIdAsync(string webLinkId, GetWebLinkByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a web link object.
    /// </summary>
    /// <param name="webLinkId">
    /// The ID of the web link.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateWebLinkById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateWebLinkById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<WebLink> UpdateWebLinkByIdAsync(string webLinkId, UpdateWebLinkByIdRequestBody? requestBody = default, UpdateWebLinkByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a web link.
    /// </summary>
    /// <param name="webLinkId">
    /// The ID of the web link.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteWebLinkById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteWebLinkByIdAsync(string webLinkId, DeleteWebLinkByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}