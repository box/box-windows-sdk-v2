Configuration
=============

- [URLs configuration](#urls-configuration)
  - [Base URL](#base-url)
  - [Account URL](#token-url)
  - [Upload URL](#upload-url)
- [Timeout](#timeout)

URLs configuration
------------------

### Base URL
The default base URL used for making API calls to Box can be changed by calling `SetBoxApiHostUri()` method. Default value is https://api.box.com.

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
The default URL used for uploads can be changed by calling `SetBoxUploadApiUri()` method. Default value is https://upload.box.com/api.

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