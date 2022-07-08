Configuration
=============

- [URLs configuration](#urls-configuration)
  - [Base URL](#base-url)
  - [Account URL](#account-url)
  - [Upload URL](#upload-url)
- [Timeout](#timeout)
- [Proxy](#proxy)
- [Supress notifications](#supress-notifications)
- [Make API calls As-User](#make-api-calls-as-user)

URLs configuration
------------------

### Base URL
The default base URL used for making API calls to Box can be changed by calling `SetBoxApiHostUri()` method. Default value is https://api.box.com/.

```c#
var customUri = new Uri("https://custom-api-url.com");

var boxConfig = new BoxConfigBuilder("clientID", "clientSecret")
    .SetBoxApiHostUri(customUri)
    .Build();
```

### Account URL
The default account URL used to create OAuth authorization URL can be changed by calling `SetBoxAccountApiHostUri()` method. Default value is https://account.box.com/api/.

```c#
var customUri = new Uri("https://custom-account-url.com");

var boxConfig = new BoxConfigBuilder("clientID", "clientSecret")
    .SetBoxAccountApiHostUri(customUri)
    .Build();
```

### Upload URL
The default URL used for uploads can be changed by calling `SetBoxUploadApiUri()` method. Default value is https://upload.box.com/api/.

```c#
var customUri = new Uri("https://custom-upload-url.com");

var boxConfig = new BoxConfigBuilder("clientID", "clientSecret")
    .SetBoxUploadApiUri(customUri)
    .Build();
```

Timeout
-------

The default request timeout can be changed by calling `SetTimeout()` method. Default timeout is 100 seconds.

```c#
var timeout = TimeSpan.FromSeconds(200);

var boxConfig = new BoxConfigBuilder("clientID", "clientSecret")
    .SetTimeout(timeout)
    .Build();
```

Proxy
-------

`BoxClient` uses .NET `WebProxy` class to support Proxy. To use proxy you need to call `SetWebProxy()` when building configuration.

```c#
var proxy = new WebProxy
{
    Address = new Uri("https://my-proxy.com"),
    Credentials = new NetworkCredential
    {
        Domain = "myDomain",
        UserName = "username",
        Password = "password"
    }
};

var config = new BoxConfigBuilder("clientID", "clientSecret")
    .SetWebProxy(proxy)
    .Build();
```

Supress Notifications
---------------------

If you are making administrative API calls (that is, your application has “Manage an Enterprise” scope, and the user making the API call is a co-admin with the correct "Edit settings for your company" permission) then you can suppress both email and webhook notifications.
```c#
var config = new BoxConfigBuilder("clientID", "clientSecret", "redirect_uri").Build();
var auth = new OAuthSession("access_token", "refresh_token", 3600, "bearer");

var adminClient = new BoxClient(config, auth, suppressNotifications: true);
```

Make API calls As-User
----------------------

If you have an admin token with appropriate permissions, you can make API calls in the context of a managed user. In order to do this you must request Box.com to activate As-User functionality for your API key (see developer site for instructions). 

```c#
var config = new BoxConfigBuilder("clientID", "clientSecret", "redirect_uri").Build();
var auth = new OAuthSession("access_token", "refresh_token", 3600, "bearer");

var userId = "12345678"
var userClient = new BoxClient(config, auth, asUser: userId);

//returns root folder items for the user with ID '12345678'
var items  = await userClient.FoldersManager.GetFolderItemsAsync("0", 500);
```