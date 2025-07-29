using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ISharedLinksAppItemsManager {
        /// <summary>
    /// Returns the app item represented by a shared link.
    /// 
    /// The link can originate from the current enterprise or another.
    /// </summary>
    /// <param name="headers">
    /// Headers of findAppItemForSharedLink method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<AppItem> FindAppItemForSharedLinkAsync(FindAppItemForSharedLinkHeaders headers, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}