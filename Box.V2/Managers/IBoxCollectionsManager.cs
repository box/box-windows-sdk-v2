using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    /// <summary>
    /// Managing collections  
    /// </summary>
    public interface IBoxCollectionsManager
    {
        /// <summary>
        /// To add or remove an item from a collection, you do a PUT on that item and change the list of collections it belongs to.
        /// </summary>
        /// <param name="folderId">Id of the folder.</param>
        /// <param name="collectionsRequest">The request which contains collections ids</param>
        /// <returns>A full folder object is returned.</returns>
        Task<BoxFolder> CreateOrDeleteCollectionsForFolderAsync(string folderId, BoxCollectionsRequest collectionsRequest);

        /// <summary>
        /// To add or remove an item from a collection, you do a PUT on that item and change the list of collections it belongs to.
        /// </summary>
        /// <param name="fileId">Id of the file.</param>
        /// <param name="collectionsRequest">The request which contains collections ids</param>
        /// <returns>A full file object is returned.</returns>
        Task<BoxFile> CreateOrDeleteCollectionsForFileAsync(string fileId, BoxCollectionsRequest collectionsRequest);

        /// <summary>
        /// Retrieves the collections for the given user. Currently, only the favorites collection is supported.
        /// </summary>
        /// <returns>An array of collection instances</returns>
        Task<BoxCollection<BoxCollectionItem>> GetCollectionsAsync();

        /// <summary>
        /// Retrieves the files and/or folders contained within this collection. Collection item lists behave a lot like getting a folder’s items.
        /// </summary>
        /// <param name="collectionId">The collection identifier.</param>
        /// <param name="limit">The maximum number of items to return in a page.</param>
        /// <param name="offset">The offset at which to begin the response. An offset of value of 0 will start at the beginning of the folder-listing. Offset of 2 would start at the 2nd record, not the second page. Note: If there are hidden items in your previous response, your next offset should be = offset + limit, not the # of records you received back.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns></returns>
        Task<BoxCollection<BoxItem>> GetCollectionItemsAsync(string collectionId, int limit = 100, int offset = 0, IEnumerable<string> fields = null, bool autoPaginate = false);
    }
}
