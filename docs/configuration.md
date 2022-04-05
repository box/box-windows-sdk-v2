Configuration
=============

- [URLs configuration](#urls-configuration)
  - [Api URL](#api-url)
  - [Token URL](#token-url)
  - [Authorize URL](#authorize-url)
  - [Revoke URL](#revoke-url)
  - [Upload URL](#upload-url)
- [Timeout](#timeout)

URLs configuration
------------------

### Api URL
The default base URL used for making API calls to Box can be changed by calling `SetBoxApiUri()` method. Default value is https://api.box.com/2.0/.

```c#
var customUri = new Uri("https://custom-api-url.com/2.0/");

var boxConfig = new BoxConfigBuilder("clientID", "clientSecret")
    .SetBoxApiUri(customUri)
    .Build();
```

### Token URL
The default URL used for getting token can be changed by calling `SetBoxTokenApiUri()` method. Default value is https://api.box.com/oauth2/token.

```c#
var customUri = new Uri("https://custom-api-host-url.com/token");

var boxConfig = new BoxConfigBuilder("clientID", "clientSecret")
    .SetBoxTokenApiUri(customUri)
    .Build();
```

### Authorize URL
The default URL used for authorizationcan be changed by calling `SetBoxAuthorizeApiUri()` method. Default value is https://account.box.com/api/oauth2/authorize.

```c#
var customUri = new Uri("https://custom-api-host-url.com/authorize");

var boxConfig = new BoxConfigBuilder("clientID", "clientSecret")
    .SetBoxAuthorizeApiUri(customUri)
    .Build();
```

### Revoke URL
The default URL used for invalidating token can be changed by calling `SetBoxRevokeApiUri()` method. Default value is https://api.box.com/oauth2/revoke.

```c#
var customUri = new Uri("https://custom-api-host-url.com/revoke");

var boxConfig = new BoxConfigBuilder("clientID", "clientSecret")
    .SetBoxRevokeApiUri(customUri)
    .Build();
```

### Upload URL
The default URL used for uploads can be changed by calling `SetBoxApiUri()` method. Default value is https://upload.box.com/api/2.0/.

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