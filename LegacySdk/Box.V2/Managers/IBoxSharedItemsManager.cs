using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    /// <summary>
    /// Shared items are any files or folders that are represented by a shared link. 
    /// </summary>
    /// <seealso cref="Box.V2.Managers.BoxResourceManager" />
    public interface IBoxSharedItemsManager
    {
        /// <summary>
        /// Shared items are any files or folders that are represented by a shared link. Shared items are different from other API resources in that a shared resource doesn’t necessarily have to be in the account of the user accessing it. The actual shared link itself is used along with a normal access token.
        /// </summary>
        /// <param name="sharedLink">The shared link for this item.</param>
        /// <param name="sharedLinkPassword">The password for the shared link (if required)</param>
        /// <returns>A full file or folder object is returned if the shared link is valid and the user has access to it. An error may be returned if the link is invalid, if a password is required, or if the user does not have access to the file.</returns>
        Task<BoxItem> SharedItemsAsync(string sharedLink, string sharedLinkPassword = null);
    }
}
