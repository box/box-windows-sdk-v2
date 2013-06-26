using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Auth
{
    public interface IAuthRepository
    {
        /// <summary>
        /// The active OAuth2 session
        /// </summary>
        OAuthSession Session { get; }

        /// <summary>
        /// Performs the 2nd step of the OAuth2 workflow and exchanges the auth code
        /// for an Access and Refresh token
        /// </summary>
        /// <param name="authCode">The auth code received from step 1 of the OAuth2 workflow</param>
        /// <returns>A fully authenticated OAuth2 session</returns>
        Task<OAuthSession> AuthenticateAsync(string authCode);

        /// <summary>
        /// Exchanges an expired access token for a renewed one using the current refresh token
        /// </summary>
        /// <param name="accessToken">The expired access token</param>
        /// <returns>A fully authenticated OAuth2 session</returns>
        Task<OAuthSession> RefreshAccessTokenAsync(string accessToken);

        /// <summary>
        /// Revokes access by invalidating both the access token and refresh token
        /// </summary>
        /// <returns>Task</returns>
        Task LogoutAsync();
    }
}
