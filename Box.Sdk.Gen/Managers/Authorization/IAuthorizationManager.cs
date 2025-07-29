using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IAuthorizationManager {
        /// <summary>
    /// Authorize a user by sending them through the [Box](https://box.com)
    /// website and request their permission to act on their behalf.
    /// 
    /// This is the first step when authenticating a user using
    /// OAuth 2.0. To request a user's authorization to use the Box APIs
    /// on their behalf you will need to send a user to the URL with this
    /// format.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of authorizeUser method
    /// </param>
    /// <param name="headers">
    /// Headers of authorizeUser method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task AuthorizeUserAsync(AuthorizeUserQueryParams queryParams, AuthorizeUserHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Request an Access Token using either a client-side obtained OAuth 2.0
    /// authorization code or a server-side JWT assertion.
    /// 
    /// An Access Token is a string that enables Box to verify that a
    /// request belongs to an authorized session. In the normal order of
    /// operations you will begin by requesting authentication from the
    /// [authorize](#get-authorize) endpoint and Box will send you an
    /// authorization code.
    /// 
    /// You will then send this code to this endpoint to exchange it for
    /// an Access Token. The returned Access Token can then be used to to make
    /// Box API calls.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of requestAccessToken method
    /// </param>
    /// <param name="headers">
    /// Headers of requestAccessToken method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<AccessToken> RequestAccessTokenAsync(PostOAuth2Token requestBody, RequestAccessTokenHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Refresh an Access Token using its client ID, secret, and refresh token.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of refreshAccessToken method
    /// </param>
    /// <param name="headers">
    /// Headers of refreshAccessToken method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<AccessToken> RefreshAccessTokenAsync(PostOAuth2TokenRefreshAccessToken requestBody, RefreshAccessTokenHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Revoke an active Access Token, effectively logging a user out
    /// that has been previously authenticated.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of revokeAccessToken method
    /// </param>
    /// <param name="headers">
    /// Headers of revokeAccessToken method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task RevokeAccessTokenAsync(PostOAuth2Revoke requestBody, RevokeAccessTokenHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}