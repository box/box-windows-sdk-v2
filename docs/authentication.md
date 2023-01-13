Authentication
==============

The Box API uses OAuth2 for authentication, which can be difficult to implement.
The SDK makes it easier by providing classes that handle obtaining tokens and
automatically refreshing them when possible. See the
[OAuth 2 overview](https://developer.box.com/en/guides/authentication/) for a detailed
overview of how the Box API handles authentication.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Ways to Authenticate](#ways-to-authenticate)
  - [Developer Token](#developer-token)
  - [Server Auth with JWT](#server-auth-with-jwt)
  - [Server Auth with CCG](#server-auth-with-ccg)
  - [Traditional 3-Legged OAuth2](#traditional-3-legged-oauth2)
    - [AuthRepository Implementation](#authrepository-implementation)
  - [Box View Authentication with App Tokens](#box-view-authentication-with-app-tokens)
- [As-User](#as-user)
- [Token Exchange](#token-exchange)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Ways to Authenticate
--------------------

### Developer Token

The fastest way to get started using the API is with developer tokens. A
developer token is simply a short-lived access token that cannot be refreshed
and can only be used with your own account. Therefore, they're only useful for
testing an app and aren't suitable for production. You can obtain a developer
token from your application's
[developer console][dev-console] page.

The following example creates an API client with a developer token:

<!-- sample x_auth init_with_dev_token -->
```c#
var config = new BoxConfigBuilder("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", new Uri("http://localhost")).Build();
var session = new OAuthSession("YOUR_DEVELOPER_TOKEN", "N/A", 3600, "bearer");
var client = new BoxClient(config, session);
```

[dev-console]: https://app.box.com/developers/console

### Server Auth with JWT

Server auth allows your application to authenticate itself with the Box API
for a given enterprise.  By default, your application has a
[Service Account](https://developer.box.com/guides/getting-started/user-types/service-account/)
that represents it and can perform API calls.  The Service Account is separate
from the Box accounts of the application developer and the enterprise admin of
any enterprise that has authorized the app — files stored in that account are
not accessible in any other account by default, and vice versa.

If you generated your public and private keys automatically through the
[Box Developer Console][dev-console], you can use the JSON file created there
to configure the SDK and create a client to make calls as the
Service Account:

<!-- sample x_auth init_with_jwt_enterprise -->
```c#
var config = BoxConfigBuilder.CreateFromJsonString(jsonConfig).Build();
var session = new BoxJWTAuth(config);
var adminToken = await session.AdminTokenAsync(); //valid for 60 minutes so should be cached and re-used
BoxClient adminClient = session.AdminClient(adminToken);
```

Otherwise, you'll need to provide the necessary configuration fields directly
to the `BoxConfigBuilder` constructor:

<!-- sample x_auth init_with_jwt_enterprise_with_config -->
```c#
var boxConfig = new BoxConfigBuilder("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", "YOUR_ENTERPRISE_ID", "ENCRYPTED_PRIVATE_KEY", "PRIVATE_KEY_PASSWORD", "PUBLIC_KEY_ID").Build();
var boxJWT = new BoxJWTAuth(boxConfig);
var adminToken = await boxJWT.AdminTokenAsync(); //valid for 60 minutes so should be cached and re-used
BoxClient adminClient = boxJWT.AdminClient(adminToken);
adminClient.Auth.SessionAuthenticated += delegate(object o, SessionAuthenticatedEventArgs e)
{
    string newAccessToken = e.Session.AccessToken;
    // cache the new access token
};
```

App auth applications also often have associated [App Users](https://developer.box.com/guides/getting-started/user-types/app-users/), which are created and managed directly by the application
— they do not have normal login credentials, and can only be accessed through
the Box API by the application that created them.  You may authenticate as the
Service Account to provision and manage users, or as an individual app user to
make calls as that user.  See the [API documentation](https://developer.box.com/en/guides/applications/custom-apps/)
and [sample app](https://github.com/box/box-windows-sdk-v2/tree/main/Box.V2.Samples.JWTAuth)
for detailed instructions on how to use app auth.

Clients for making calls as an App User or Managed User can be created with the same `BoxJWTAuth`
instance as in the above examples, similarly to creating a Service Account client:

<!-- sample x_auth init_with_jwt_with_user_id -->
```c#
var appUserId = "12345";
var userToken = await boxJWT.UserTokenAsync(appUserID); //valid for 60 minutes so should be cached and re-used
BoxClient appUserClient = boxJWT.UserClient(userToken, appUserId);
appUserClient.Auth.SessionAuthenticated += delegate(object o, SessionAuthenticatedEventArgs e)
{
    string newAccessToken = e.Session.AccessToken;
    // cache the new access token
};
```

### Server Auth with CCG

Server auth allows your application to authenticate itself with the Box API
for a given enterprise. 
Client Credentials Grant (CCG) allows you to authenticate by providing `clientId` and `clientSecret` and `enterpriseId` of your app.
By default, your application has a
[Service Account](https://developer.box.com/guides/getting-started/user-types/service-account/)
that represents it and can perform API calls. The Service Account is separate
from the Box accounts of the application developer and the enterprise admin of
any enterprise that has authorized the app — files stored in that account are
not accessible in any other account by default, and vice versa.

You'll need to provide the necessary configuration fields directly
to the `BoxConfigBuilder` constructor:

<!-- sample x_auth with_client_credentials_enterprise -->
```c#
var boxConfig = new BoxConfigBuilder("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET")
                .SetEnterpriseId("YOUR_ENTERPRISE_ID")
                .Build();
var boxCCG = new BoxCCGAuth(boxConfig);
```

There are two ways to create an admin client, the first one uses explicit admin token:
```c#
var adminToken = await boxCCG.AdminTokenAsync(); //valid for 60 minutes so should be cached and re-used
IBoxClient adminClient = boxCCG.AdminClient(adminToken);
adminClient.Auth.SessionAuthenticated += delegate(object o, SessionAuthenticatedEventArgs e)
{
    string newAccessToken = e.Session.AccessToken;
    // cache the new access token
};
```

Second way leaves token management (caching) to the `Auth`, a new token is retrieved before the first call. Keep in mind that if you create multiple `adminClient` instances, the token won't be shared, it is expected that the `adminClient` instance is reused.
```c#
IBoxClient adminClient = boxCCG.AdminClient();
```

App auth applications also often have associated [App Users](https://developer.box.com/guides/getting-started/user-types/app-users/), which are created and managed directly by the application
— they do not have normal login credentials, and can only be accessed through
the Box API by the application that created them.  You may authenticate as the
Service Account to provision and manage users, or as an individual app user to
make calls as that user.  See the [API documentation](https://developer.box.com/en/guides/applications/custom-apps/)
for detailed instructions on how to use app auth.

Clients for making calls as an App User or Managed User can be created with the same `BoxCCGAuth`
instance as in the above examples, similarly to creating a Service Account client. You don't need to provide `enterpriseId` in this case:

<!-- sample x_auth with_client_credentials -->
```c#
var boxConfig = new BoxConfigBuilder("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET")
                .Build();
var boxCCG = new BoxCCGAuth(boxConfig);
```

Variant with explicit initial token:
```c#
var userToken = await boxCCG.UserTokenAsync("USER_ID"); //valid for 60 minutes so should be cached and re-used
IBoxClient userClient = boxCCG.UserClient(userToken, "USER_ID");
userClient.Auth.SessionAuthenticated += delegate(object o, SessionAuthenticatedEventArgs e)
{
    string newAccessToken = e.Session.AccessToken;
    // cache the new access token
};
```
Variant without initial token:
```c#
IBoxClient userClient = boxCCG.UserClient("USER_ID");
```

### Traditional 3-Legged OAuth2

If your application needs to integrate with existing Box users who will provide
their login credentials to grant your application access to their account, you
will need to go through the standard OAuth2 login flow.  A detailed guide for
this process is available in the
[Authentication with OAuth API documentation](https://developer.box.com/en/guides/applications/custom-apps/oauth2-setup/).

Using an auth code is the most common way of authenticating with the Box API for
existing Box users, to integrate with their accounts.
Your application must provide a way for the user to login to Box (usually with a
browser or web view) in order to obtain an auth code.

After a user logs in and grants your application access to their Box account,
they will be redirected to your application's `redirect_uri` which will contain
an auth code. This auth code can then be used along with your client ID and
client secret to establish an API connection.  The `BoxClient` will
automatically refresh the access token as needed.

```c#
var config = new BoxConfigBuilder("CLIENT_ID", "CLIENT_SECRET", new System.Uri("YOUR_REDIRECT_URL")).Build();
var client = new BoxClient(config);
OAuthSession session = // Create session from custom implementation
var client = new BoxClient(config, session);
```

#### AuthRepository Implementation

In order to maintain authentication and ensure that your users do not need to
log in again every time they use your application, you should persist their
token information to some sort of durable store (e.g. a database).  The SDK
provides an `AuthRepository` base class that you can extend to implement custom
logic around token storage and refresh.

### Box View Authentication with App Tokens

[Box View](https://developer.box.com/en/guides/embed/box-view/)
uses a long-lived access token that is generated from the
[Box Developer Console][dev-console] to make API calls.  These access tokens
cannot be automatically refreshed from the SDK, and must be manually changed in
your application code.

To use the primary or secondary access token generated in the Developer Console,
simply create a basic client with that token:

<!-- sample x_auth init_with_app_token -->
```c#
var config = new BoxConfigBuilder("YOUR_CLIENT_ID", "N/A", new Uri("http://localhost")).Build();
var session = new OAuthSession("YOUR_APP_TOKEN", "N/A", 3600, "bearer");
var client = new BoxClient(config, session);
```

As-User
-------

The As-User header is used by enterprise admins to make API calls on behalf of
their enterprise's users. This requires the API request to pass an
`As-User: USER-ID` header. For more details see the 
[documentation on As-User](https://developer.box.com/en/guides/authentication/oauth2/as-user/).

Constructing a `BoxClient` with the `asUser` parameter set will create a client
that will make calls on behalf of the specified user:

<!-- sample x_auth init_with_as_user_header -->
```c#
var userId = "12345";
var client = new BoxClient(config, session, asUser: userId);
```

Token Exchange
--------------

You can exchange a client's access token for one with a lower scope, in order
to restrict the permissions for a child client or to pass to a less secure
location (e.g. a browser-based app).  This is useful if you want to use the
[Box UI Kits](https://developer.box.com/en/guides/embed/ui-elements/), since they generally
do not need full read/write permissions to run.

To exchange the token held by a client for a new token with only `item_preview`
scope, restricted to a single file, suitable for the
[Content Preview UI Kit](https://developer.box.com/en/guides/embed/ui-elements/preview/):

<!-- sample post_oauth2_token downscope_token -->
```c#
var exchanger = new TokenExchange(client.Auth.Session.AccessToken, "item_preview");
exchanger.SetResource("https://api.box.com/2.0/files/123456789");
string downscopedToken = await exchanger.ExchangeAsync();
```

To exchange the client's token for one with scopes to upload and delete items, but not to view their contents,
which would be suitable for an less-trusted server-side process;
```c#
var scopes = new List<string>() { "item_upload", "item_download" };
var exchanger = new TokenExchange(client.Auth.Session.AccessToken, scopes);
string downscopedToken = await exchanger.ExchangeAsync();
```
