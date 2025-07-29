using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Managers {
    public interface ISearchManager {
        /// <summary>
    /// Create a search using SQL-like syntax to return items that match specific
    /// metadata.
    /// 
    /// By default, this endpoint returns only the most basic info about the items for
    /// which the query matches. To get additional fields for each item, including any
    /// of the metadata, use the `fields` attribute in the query.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of searchByMetadataQuery method
    /// </param>
    /// <param name="headers">
    /// Headers of searchByMetadataQuery method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<MetadataQueryResults> SearchByMetadataQueryAsync(MetadataQuery requestBody, SearchByMetadataQueryHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Searches for files, folders, web links, and shared files across the
    /// users content or across the entire enterprise.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of searchForContent method
    /// </param>
    /// <param name="headers">
    /// Headers of searchForContent method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<SearchResultsOrSearchResultsWithSharedLinks> SearchForContentAsync(SearchForContentQueryParams? queryParams = default, SearchForContentHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}