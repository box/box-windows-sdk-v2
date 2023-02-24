using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using Box.V2.Utility;

namespace Box.V2.Managers
{

    /// <summary>
    /// Shared items are any files or folders that are represented by a shared link. 
    /// </summary>
    /// <seealso cref="Box.V2.Managers.BoxResourceManager" />
    public class BoxSharedItemsManager : BoxResourceManager, IBoxSharedItemsManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoxSharedItemsManager"/> class.
        /// </summary>
        /// <param name="config">The config object to use</param>
        /// <param name="service">The Box service object</param>
        /// <param name="converter">The box converter object to use</param>
        /// <param name="auth">The auth repository object to use</param>
        /// <param name="asUser">The user ID to set as the 'As-User' header parameter; used to make calls in the context of a user using an admin token</param>
        /// <param name="suppressNotifications">Whether or not to suppress both email and webhook notifications. Typically used for administrative API calls. Your application must have “Manage an Enterprise” scope, and the user making the API calls is a co-admin with the correct "Edit settings for your company" permission.</param>
        public BoxSharedItemsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Shared items are any files or folders that are represented by a shared link. Shared items are different from other API resources in that a shared resource doesn’t necessarily have to be in the account of the user accessing it. The actual shared link itself is used along with a normal access token.
        /// </summary>
        /// <param name="sharedLink">The shared link for this item.</param>
        /// <param name="sharedLinkPassword">The password for the shared link (if required)</param>
        /// <returns>A full file or folder object is returned if the shared link is valid and the user has access to it. An error may be returned if the link is invalid, if a password is required, or if the user does not have access to the file.</returns>
        public async Task<BoxItem> SharedItemsAsync(string sharedLink, string sharedLinkPassword = null)
        {
            sharedLink.ThrowIfNullOrWhiteSpace("sharedLink");
            var sharedLinkHeader = SharedLinkUtils.GetSharedLinkHeader(sharedLink, sharedLinkPassword);
            BoxRequest request = new BoxRequest(_config.SharedItemsUri, null)
                .Header(sharedLinkHeader.Item1, sharedLinkHeader.Item2);
            IBoxResponse<BoxItem> response = await ToResponseAsync<BoxItem>(request).ConfigureAwait(false);
            return response.ResponseObject;
        }
    }
}
