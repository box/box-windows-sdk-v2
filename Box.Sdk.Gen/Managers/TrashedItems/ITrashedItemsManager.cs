using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ITrashedItemsManager {
        /// <summary>
    /// Retrieves the files and folders that have been moved
    /// to the trash.
    /// 
    /// Any attribute in the full files or folders objects can be passed
    /// in with the `fields` parameter to retrieve those specific
    /// attributes that are not returned by default.
    /// 
    /// This endpoint defaults to use offset-based pagination, yet also supports
    /// marker-based pagination using the `marker` parameter.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getTrashedItems method
    /// </param>
    /// <param name="headers">
    /// Headers of getTrashedItems method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Items> GetTrashedItemsAsync(GetTrashedItemsQueryParams? queryParams = default, GetTrashedItemsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}