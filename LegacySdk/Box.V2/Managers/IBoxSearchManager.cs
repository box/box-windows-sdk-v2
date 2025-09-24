using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents the search endpoint
    /// </summary>
    public interface IBoxSearchManager
    {
        /// <summary>
        /// Search for items that are accessible by a single user or an entire enterprise.
        /// </summary>
        /// <param name="query">The string to search for. Box matches the search string against object names, descriptions, text contents of files, and other data.</param>
        /// <param name="scope">The scope for which you want to limit your search to. Can be user_content for a search limited to only the current user or enterprise_content for the entire enterprise. To enable the enterprise_content scope for an administrator, please contact Box.</param>
        /// <param name="fileExtensions">Limit searches to specific file extension(s).</param>
        /// <param name="createdAfter">The from date for when the item was created</param>
        /// <param name="createdBefore">The to date for when the item was created</param>
        /// <param name="updatedAfter">The from date for when the item was last updated</param>
        /// <param name="updatedBefore">The to date for when the item was last updated</param>
        /// <param name="sizeLowerBound">The lower bound of the file size range in bytes</param>
        /// <param name="sizeUpperBound">The upper bound of the file size range in bytes</param>
        /// <param name="ownerUserIds">Search by item owners</param>
        /// <param name="ancestorFolderIds">Limit searches to specific parent folders</param>
        /// <param name="contentTypes">Limit searches to specific Box designated content types. Can be name, description, file_content, comments, or tags.</param>
        /// <param name="type">The type you want to return in your search. Can be file, folder, or web_link</param>
        /// <param name="trashContent">Allows you to search within the trash. Can be trashed_only or non_trashed_only. Searches without this parameter default to non_trashed_only</param>
        /// <param name="mdFilters">Filters for a specific metadata template for files with metadata object associations. NOTE: For searches with the mdfilters param, a query string is not required. Currenly only one BoxMetadataFilterRequest element is allowed.</param>
        /// <param name="limit">Number of search results to return. The default is 30 and the maximum is 200.</param>
        /// <param name="offset">The search result at which to start the response. The default is 0.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="sort">The field to sort the search results by, e.g. "modified_at".</param>
        /// <param name="direction">The direction to return the results. "ASC" for ascending and "DESC" for descending.</param>
        /// <returns>A collection of search results is returned. If there are no matching search results, the collection will be empty.</returns>
        Task<BoxCollection<BoxItem>> QueryAsync(string query,
            string scope = null,
            IEnumerable<string> fileExtensions = null,
            DateTimeOffset? createdAfter = null,
            DateTimeOffset? createdBefore = null,
            DateTimeOffset? updatedAfter = null,
            DateTimeOffset? updatedBefore = null,
            long? sizeLowerBound = null,
            long? sizeUpperBound = null,
            IEnumerable<string> ownerUserIds = null,
            IEnumerable<string> ancestorFolderIds = null,
            IEnumerable<string> contentTypes = null,
            string type = null,
            string trashContent = null,
            List<BoxMetadataFilterRequest> mdFilters = null,
            int limit = 30,
            int offset = 0,
            IEnumerable<string> fields = null,
            string sort = null,
            BoxSortDirection? direction = null);
    }
}
