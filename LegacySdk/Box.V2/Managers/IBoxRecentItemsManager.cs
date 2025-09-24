using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    /// <summary>
    /// Returns information about files that have been accessed by a user not long ago.
    /// </summary>
    public interface IBoxRecentItemsManager
    {
        /// <summary>
        /// Returns information about files that have been accessed by a user not long ago.
        /// </summary>
        /// <param name="limit">The default is 100 and the maximum is 1,000. Less than limit number of items may be returned (even when a user has more) in cases of deleted items or lost permission.</param>
        /// <param name="marker">The position marker at which to begin the response.</param>
        /// <param name="fields">Comma-separated list of fields to include in the response.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>BoxRecentItem in collection.</returns>
        Task<BoxCollectionMarkerBasedV2<BoxRecentItem>> GetRecentItemsAsync(int limit = 100, string marker = null, IEnumerable<string> fields = null, bool autoPaginate = false);
    }
}
