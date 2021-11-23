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
    /// Returns information about files that have been accessed by a user not long ago.
    /// </summary>
    public class BoxRecentItemsManager : BoxResourceManager, IBoxRecentItemsManager
    {
        public BoxRecentItemsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Returns information about files that have been accessed by a user not long ago.
        /// </summary>
        /// <param name="limit">The default is 100 and the maximum is 1,000. Less than limit number of items may be returned (even when a user has more) in cases of deleted items or lost permission.</param>
        /// <param name="marker">The position marker at which to begin the response.</param>
        /// <param name="fields">Comma-separated list of fields to include in the response.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>BoxRecentItem in collection.</returns>
        public async Task<BoxCollectionMarkerBasedV2<BoxRecentItem>> GetRecentItemsAsync(int limit = 100, string marker = null, IEnumerable<string> fields = null, bool autoPaginate = false)
        {
            var request = new BoxRequest(_config.RecentItemsUri)
                .Param("limit", limit.ToString())
                .Param("marker", marker)
                .Param(ParamFields, fields);

            if (autoPaginate)
            {
                return await AutoPaginateMarkerV2<BoxRecentItem>(request, limit).ConfigureAwait(false);
            }
            else
            {
                var response = await ToResponseAsync<BoxCollectionMarkerBasedV2<BoxRecentItem>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }
    }
}
