# Client

This is the central entrypoint for all SDK interaction. The BoxClient houses all the API endpoints
divided across resource managers.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Make custom HTTP request](#make-custom-http-request)
  - [JSON request](#json-request)
  - [Multi-part request](#multi-part-request)
  - [Binary response](#binary-response)
- [Additional headers](#additional-headers)
  - [As-User header](#as-user-header)
  - [Suppress notifications](#suppress-notifications)
  - [Custom headers](#custom-headers)
- [Custom Base URLs](#custom-base-urls)
- [Use Proxy for API calls](#use-proxy-for-api-calls)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

# Make custom HTTP request

You can make custom HTTP requests using the `client.MakeRequestAsync()` method.
This method allows you to make any HTTP request to the Box API. It will automatically use authentication and
network configuration settings from the client.
The method accepts a `FetchOptions` object as an argument and returns a `FetchResponse` object.

## JSON request

The following example demonstrates how to make a custom POST request to create a new folder in the root folder.

```c#
string requestBodyPost = "{\"name\": \"newFolderName\", \"parent\": {\"id\": \"0\"}}";
FetchResponse response = await client.MakeRequestAsync(fetchOptions: new FetchOptions(method: "POST", url: "https://api.box.com/2.0/folders") { Data = JsonUtils.JsonToSerializedData(text: requestBodyPost) });
Console.WriteLine("Received status code: " + response.status);
Console.WriteLine("Created folder name: " + response.data["name"]);
```

## Multi-part request

The following example demonstrates how to make a custom multipart request that uploads a file to a folder.

```c#
string multipartAttributes = "{\"name\": \"newFileName\", \"parent\": {\"id\": \"newFolderId\"}}";
FetchResponse response = await client.MakeRequestAsync(fetchOptions: new FetchOptions(method: "POST", url: "https://upload.box.com/api/2.0/files/content", contentType: "multipart/form-data") { FileStream = fileContentStream, MultipartData = Array.AsReadOnly(new [] {new MultipartItem(partName: "attributes") { Data = JsonUtils.JsonToSerializedData(text: multipartAttributes) },new MultipartItem(partName: "file") { FileStream = fileContentStream }}) });
Console.WriteLine("Received status code: " + response.status);
```

## Binary response

The following example demonstrates how to make a custom request that expects a binary response.
It is required to specify the `responseFormat` parameter in the `FetchOptions` object to `Box.Sdk.Gen.ResponseFormat.Binary`.

```c#
string fileId = "123456789";
FetchResponse response = await client.MakeRequestAsync(fetchOptions: new FetchOptions(method: "GET", url: string.Concat("https://api.box.com/2.0/files/", fileId, "/content"), responseFormat: Box.Sdk.Gen.ResponseFormat.Binary));
Console.WriteLine("Received status code: " + response.status);
string filePath = "output.txt";
using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
{
    responseStream.CopyTo(response.content);
}
```

# Additional headers

BoxClient provides a convenient methods, which allow passing additional headers, which will be included
in every API call made by the client.

## As-User header

The As-User header is used by enterprise admins to make API calls on behalf of their enterprise's users.
This requires the API request to pass an As-User: USER-ID header. For more details see the [documentation on As-User](https://developer.box.com/en/guides/authentication/oauth2/as-user/).

The following example assume that the client has been instantiated with an access token belonging to an admin-level user
or Service Account with appropriate privileges to make As-User calls.

Calling the `client.WithAsUserHeader()` method creates a new client to impersonate user with the provided ID.
All calls made with the new client will be made in context of the impersonated user, leaving the original client unmodified.

<!-- sample x_auth init_with_as_user_header -->

```c#
var userClient = client.WithAsUserHeader(useId: "1234567");
```

## Suppress notifications

If you are making administrative API calls (that is, your application has “Manage an Enterprise”
scope, and the user signing in is a co-admin with the correct "Edit settings for your company"
permission) then you can suppress both email and webhook notifications. This can be used, for
example, for a virus-scanning tool to download copies of everyone’s files in an enterprise,
without every collaborator on the file getting an email. All actions will still appear in users'
updates feed and audit logs.

> **Note:** This functionality is only available for approved applications.

Calling the `client.WithSuppressedNotifications()` method creates a new client.
For all calls made with the new client the notifications will be suppressed.

```c#
var newClient = client.WithSuppressedNotifications();
```

## Custom headers

You can also specify the custom set of headers, which will be included in every API call made by client.
Calling the `client.WithExtraHeaders()` method creates a new client, leaving the original client unmodified.

```c#
var extraHeaders = new Dictionary<string, string?>()
        {
           { "customHeader", "customValue" }
        };
var newClient = client.WithExtraHeaders(extraHeaders: extraHeaders);
```

# Custom Base URLs

You can also specify the custom base URLs, which will be used for API calls made by client.
Calling the `client.WithCustomBaseUrls()` method creates a new client, leaving the original client unmodified.

```c#
var newClient = client.WithCustomBaseUrls(new BaseUrls(
  baseUrl: "https://api.box2.com",
  uploadUrl: "https://upload.box.com/api",
  oauth2Url: "https://account.box.com/api/oauth2"
));
```

# Use Proxy for API calls

In order to use a proxy for API calls, call the `client.WithProxy(proxyConfig)` method that creates a new client with proxy, leaving the original client unmodified.
In config, you can specify the username, password and domain for the proxy server.
Alternatively you can set 'UseDefaultCredentials' to true to use the credentials of the currently logged on user - `DefaultCredentials`.
NOTE: Setting UseDefaultCredentials takes precedence over Username, Password and Domain fields. If UseDefaultCredentials is set to true, the Username, Password and Domain fields will be ignored.

```c#
var newClient = client.WithProxy(new ProxyConfig("http://proxy.com") { Username = "username", Password = "password", Domain = "example" });
```

To use proxy with default credentials:

```c#
var newClient = client.WithProxy(new ProxyConfig("http://proxy.com") { UseDefaultCredentials = true });
```
