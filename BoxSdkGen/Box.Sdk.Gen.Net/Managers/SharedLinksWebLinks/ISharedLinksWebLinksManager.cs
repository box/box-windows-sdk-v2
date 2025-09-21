using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ISharedLinksWebLinksManager {
        /// <summary>
    /// Returns the web link represented by a shared link.
    /// 
    /// A shared web link can be represented by a shared link,
    /// which can originate within the current enterprise or within another.
    /// 
    /// This endpoint allows an application to retrieve information about a
    /// shared web link when only given a shared link.
    /// </summary>
    /// <param name="headers">
    /// Headers of findWebLinkForSharedLink method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of findWebLinkForSharedLink method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<WebLink> FindWebLinkForSharedLinkAsync(FindWebLinkForSharedLinkHeaders headers, FindWebLinkForSharedLinkQueryParams? queryParams = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Gets the information for a shared link on a web link.
    /// </summary>
    /// <param name="webLinkId">
    /// The ID of the web link.
    /// Example: "12345"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getSharedLinkForWebLink method
    /// </param>
    /// <param name="headers">
    /// Headers of getSharedLinkForWebLink method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<WebLink> GetSharedLinkForWebLinkAsync(string webLinkId, GetSharedLinkForWebLinkQueryParams queryParams, GetSharedLinkForWebLinkHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Adds a shared link to a web link.
    /// </summary>
    /// <param name="webLinkId">
    /// The ID of the web link.
    /// Example: "12345"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of addShareLinkToWebLink method
    /// </param>
    /// <param name="requestBody">
    /// Request body of addShareLinkToWebLink method
    /// </param>
    /// <param name="headers">
    /// Headers of addShareLinkToWebLink method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<WebLink> AddShareLinkToWebLinkAsync(string webLinkId, AddShareLinkToWebLinkQueryParams queryParams, AddShareLinkToWebLinkRequestBody? requestBody = default, AddShareLinkToWebLinkHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a shared link on a web link.
    /// </summary>
    /// <param name="webLinkId">
    /// The ID of the web link.
    /// Example: "12345"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of updateSharedLinkOnWebLink method
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateSharedLinkOnWebLink method
    /// </param>
    /// <param name="headers">
    /// Headers of updateSharedLinkOnWebLink method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<WebLink> UpdateSharedLinkOnWebLinkAsync(string webLinkId, UpdateSharedLinkOnWebLinkQueryParams queryParams, UpdateSharedLinkOnWebLinkRequestBody? requestBody = default, UpdateSharedLinkOnWebLinkHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Removes a shared link from a web link.
    /// </summary>
    /// <param name="webLinkId">
    /// The ID of the web link.
    /// Example: "12345"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of removeSharedLinkFromWebLink method
    /// </param>
    /// <param name="requestBody">
    /// Request body of removeSharedLinkFromWebLink method
    /// </param>
    /// <param name="headers">
    /// Headers of removeSharedLinkFromWebLink method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<WebLink> RemoveSharedLinkFromWebLinkAsync(string webLinkId, RemoveSharedLinkFromWebLinkQueryParams queryParams, RemoveSharedLinkFromWebLinkRequestBody? requestBody = default, RemoveSharedLinkFromWebLinkHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}