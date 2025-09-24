using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;

namespace Box.V2.Managers
{
    /// <summary>
    /// Managing collections  
    /// </summary>
    public class BoxCollectionsManager : BoxResourceManager, IBoxCollectionsManager
    {

        public BoxCollectionsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// To add or remove an item from a collection, you do a PUT on that item and change the list of collections it belongs to.
        /// </summary>
        /// <param name="folderId">Id of the folder.</param>
        /// <param name="collectionsRequest">The request which contains collections ids</param>
        /// <returns>A full folder object is returned.</returns>
        public async Task<BoxFolder> CreateOrDeleteCollectionsForFolderAsync(string folderId, BoxCollectionsRequest collectionsRequest)
        {
            folderId.ThrowIfNullOrWhiteSpace("folderId");
            collectionsRequest.ThrowIfNull("collectionsRequest")
                .Collections.ThrowIfNull("collectionsRequest.Collections");

            foreach (var collection in collectionsRequest.Collections)
            {
                collection.Type = null;
            }

            BoxRequest request = new BoxRequest(_config.FoldersEndpointUri, folderId)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize(collectionsRequest));

            IBoxResponse<BoxFolder> response = await ToResponseAsync<BoxFolder>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// To add or remove an item from a collection, you do a PUT on that item and change the list of collections it belongs to.
        /// </summary>
        /// <param name="fileId">Id of the file.</param>
        /// <param name="collectionsRequest">The request which contains collections ids</param>
        /// <returns>A full file object is returned.</returns>
        public async Task<BoxFile> CreateOrDeleteCollectionsForFileAsync(string fileId, BoxCollectionsRequest collectionsRequest)
        {
            fileId.ThrowIfNullOrWhiteSpace("fileId");
            collectionsRequest.ThrowIfNull("collectionsRequest")
                .Collections.ThrowIfNull("collectionsRequest.Collections");

            foreach (var collection in collectionsRequest.Collections)
            {
                collection.Type = null;
            }

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, fileId)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize(collectionsRequest));

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves the collections for the given user. Currently, only the favorites collection is supported.
        /// </summary>
        /// <returns>An array of collection instances</returns>
        public async Task<BoxCollection<BoxCollectionItem>> GetCollectionsAsync()
        {
            BoxRequest request = new BoxRequest(_config.CollectionsEndpointUri, null)
                .Method(RequestMethod.Get);

            IBoxResponse<BoxCollection<BoxCollectionItem>> response = await ToResponseAsync<BoxCollection<BoxCollectionItem>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }
        /// <summary>
        /// Retrieves the files and/or folders contained within this collection. Collection item lists behave a lot like getting a folder’s items.
        /// </summary>
        /// <param name="collectionId">The collection identifier.</param>
        /// <param name="limit">The maximum number of items to return in a page.</param>
        /// <param name="offset">The offset at which to begin the response. An offset of value of 0 will start at the beginning of the folder-listing. Offset of 2 would start at the 2nd record, not the second page. Note: If there are hidden items in your previous response, your next offset should be = offset + limit, not the # of records you received back.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns></returns>
        public async Task<BoxCollection<BoxItem>> GetCollectionItemsAsync(string collectionId, int limit = 100, int offset = 0, IEnumerable<string> fields = null, bool autoPaginate = false)
        {
            collectionId.ThrowIfNullOrWhiteSpace("collectionId");

            BoxRequest request = new BoxRequest(_config.CollectionsEndpointUri, string.Format(Constants.ItemsPathString, collectionId))
                .Method(RequestMethod.Get)
                .Param(ParamFields, fields)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString());

            if (autoPaginate)
            {
                return await AutoPaginateLimitOffset<BoxItem>(request, limit);
            }
            else
            {
                IBoxResponse<BoxCollection<BoxItem>> response = await ToResponseAsync<BoxCollection<BoxItem>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }
    }
}
