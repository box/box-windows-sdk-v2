using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ICollectionsManager {
        /// <summary>
    /// Retrieves all collections for a given user.
    /// 
    /// Currently, only the `favorites` collection
    /// is supported.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getCollections method
    /// </param>
    /// <param name="headers">
    /// Headers of getCollections method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Collections> GetCollectionsAsync(GetCollectionsQueryParams? queryParams = default, GetCollectionsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves the files and/or folders contained within
    /// this collection.
    /// </summary>
    /// <param name="collectionId">
    /// The ID of the collection.
    /// Example: "926489"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getCollectionItems method
    /// </param>
    /// <param name="headers">
    /// Headers of getCollectionItems method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<ItemsOffsetPaginated> GetCollectionItemsAsync(string collectionId, GetCollectionItemsQueryParams? queryParams = default, GetCollectionItemsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a collection by its ID.
    /// </summary>
    /// <param name="collectionId">
    /// The ID of the collection.
    /// Example: "926489"
    /// </param>
    /// <param name="headers">
    /// Headers of getCollectionById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Collection> GetCollectionByIdAsync(string collectionId, GetCollectionByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}