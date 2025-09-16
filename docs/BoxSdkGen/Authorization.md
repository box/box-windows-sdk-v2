# IAuthorizationManager


- [Authorize user](#authorize-user)
- [Request access token](#request-access-token)
- [Refresh access token](#refresh-access-token)
- [Revoke access token](#revoke-access-token)

## Authorize user

Authorize a user by sending them through the [Box](https://box.com)
website and request their permission to act on their behalf.

This is the first step when authenticating a user using
OAuth 2.0. To request a user's authorization to use the Box APIs
on their behalf you will need to send a user to the URL with this
format.

This operation is performed by calling function `AuthorizeUser`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-authorize/).

*Currently we don't have an example for calling `AuthorizeUser` in integration tests*

### Arguments

- queryParams `AuthorizeUserQueryParams`
  - Query parameters of authorizeUser method
- headers `AuthorizeUserHeaders`
  - Headers of authorizeUser method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Does not return any data, but rather should be used in the browser.


## Request access token

Request an Access Token using either a client-side obtained OAuth 2.0
authorization code or a server-side JWT assertion.

An Access Token is a string that enables Box to verify that a
request belongs to an authorized session. In the normal order of
operations you will begin by requesting authentication from the
[authorize](#get-authorize) endpoint and Box will send you an
authorization code.

You will then send this code to this endpoint to exchange it for
an Access Token. The returned Access Token can then be used to to make
Box API calls.

This operation is performed by calling function `RequestAccessToken`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-oauth2-token/).

*Currently we don't have an example for calling `RequestAccessToken` in integration tests*

### Arguments

- requestBody `PostOAuth2Token`
  - Request body of requestAccessToken method
- headers `RequestAccessTokenHeaders`
  - Headers of requestAccessToken method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `AccessToken`.

Returns a new Access Token that can be used to make authenticated
API calls by passing along the token in a authorization header as
follows `Authorization: Bearer <Token>`.


## Refresh access token

Refresh an Access Token using its client ID, secret, and refresh token.

This operation is performed by calling function `RefreshAccessToken`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-oauth2-token--refresh/).

*Currently we don't have an example for calling `RefreshAccessToken` in integration tests*

### Arguments

- requestBody `PostOAuth2TokenRefreshAccessToken`
  - Request body of refreshAccessToken method
- headers `RefreshAccessTokenHeaders`
  - Headers of refreshAccessToken method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `AccessToken`.

Returns a new Access Token that can be used to make authenticated
API calls by passing along the token in a authorization header as
follows `Authorization: Bearer <Token>`.


## Revoke access token

Revoke an active Access Token, effectively logging a user out
that has been previously authenticated.

This operation is performed by calling function `RevokeAccessToken`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-oauth2-revoke/).

*Currently we don't have an example for calling `RevokeAccessToken` in integration tests*

### Arguments

- requestBody `PostOAuth2Revoke`
  - Request body of revokeAccessToken method
- headers `RevokeAccessTokenHeaders`
  - Headers of revokeAccessToken method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the token was successfully revoked.


