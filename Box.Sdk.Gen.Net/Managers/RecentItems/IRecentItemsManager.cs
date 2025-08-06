using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IRecentItemsManager {
        /// <summary>
    /// Returns information about the recent items accessed
    /// by a user, either in the last 90 days or up to the last
    /// 1000 items accessed.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getRecentItems method
    /// </param>
    /// <param name="headers">
    /// Headers of getRecentItems method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<RecentItems> GetRecentItemsAsync(GetRecentItemsQueryParams? queryParams = default, GetRecentItemsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}