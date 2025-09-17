# ISharedLinksWebLinksManager


- [Find web link for shared link](#find-web-link-for-shared-link)
- [Get shared link for web link](#get-shared-link-for-web-link)
- [Add shared link to web link](#add-shared-link-to-web-link)
- [Update shared link on web link](#update-shared-link-on-web-link)
- [Remove shared link from web link](#remove-shared-link-from-web-link)

## Find web link for shared link

Returns the web link represented by a shared link.

A shared web link can be represented by a shared link,
which can originate within the current enterprise or within another.

This endpoint allows an application to retrieve information about a
shared web link when only given a shared link.

This operation is performed by calling function `FindWebLinkForSharedLink`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-shared-items--web-links/).

<!-- sample get_shared_items#web_links -->
```
await userClient.SharedLinksWebLinks.FindWebLinkForSharedLinkAsync(queryParams: new FindWebLinkForSharedLinkQueryParams(), headers: new FindWebLinkForSharedLinkHeaders(boxapi: string.Concat("shared_link=", NullableUtils.Unwrap(webLinkFromApi.SharedLink).Url, "&shared_link_password=Secret123@")));
```

### Arguments

- queryParams `FindWebLinkForSharedLinkQueryParams`
  - Query parameters of findWebLinkForSharedLink method
- headers `FindWebLinkForSharedLinkHeaders`
  - Headers of findWebLinkForSharedLink method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `WebLink`.

Returns a full web link resource if the shared link is valid and
the user has access to it.


## Get shared link for web link

Gets the information for a shared link on a web link.

This operation is performed by calling function `GetSharedLinkForWebLink`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-web-links-id--get-shared-link/).

<!-- sample get_web_links_id#get_shared_link -->
```
await client.SharedLinksWebLinks.GetSharedLinkForWebLinkAsync(webLinkId: webLinkId, queryParams: new GetSharedLinkForWebLinkQueryParams(fields: "shared_link"));
```

### Arguments

- webLinkId `string`
  - The ID of the web link. Example: "12345"
- queryParams `GetSharedLinkForWebLinkQueryParams`
  - Query parameters of getSharedLinkForWebLink method
- headers `GetSharedLinkForWebLinkHeaders`
  - Headers of getSharedLinkForWebLink method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `WebLink`.

Returns the base representation of a web link with the
additional shared link information.


## Add shared link to web link

Adds a shared link to a web link.

This operation is performed by calling function `AddShareLinkToWebLink`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-web-links-id--add-shared-link/).

<!-- sample put_web_links_id#add_shared_link -->
```
await client.SharedLinksWebLinks.AddShareLinkToWebLinkAsync(webLinkId: webLinkId, requestBody: new AddShareLinkToWebLinkRequestBody() { SharedLink = new AddShareLinkToWebLinkRequestBodySharedLinkField() { Access = AddShareLinkToWebLinkRequestBodySharedLinkAccessField.Open, Password = "Secret123@" } }, queryParams: new AddShareLinkToWebLinkQueryParams(fields: "shared_link"));
```

### Arguments

- webLinkId `string`
  - The ID of the web link. Example: "12345"
- requestBody `AddShareLinkToWebLinkRequestBody`
  - Request body of addShareLinkToWebLink method
- queryParams `AddShareLinkToWebLinkQueryParams`
  - Query parameters of addShareLinkToWebLink method
- headers `AddShareLinkToWebLinkHeaders`
  - Headers of addShareLinkToWebLink method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `WebLink`.

Returns the base representation of a web link with a new shared
link attached.


## Update shared link on web link

Updates a shared link on a web link.

This operation is performed by calling function `UpdateSharedLinkOnWebLink`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-web-links-id--update-shared-link/).

<!-- sample put_web_links_id#update_shared_link -->
```
await client.SharedLinksWebLinks.UpdateSharedLinkOnWebLinkAsync(webLinkId: webLinkId, requestBody: new UpdateSharedLinkOnWebLinkRequestBody() { SharedLink = new UpdateSharedLinkOnWebLinkRequestBodySharedLinkField() { Access = UpdateSharedLinkOnWebLinkRequestBodySharedLinkAccessField.Collaborators } }, queryParams: new UpdateSharedLinkOnWebLinkQueryParams(fields: "shared_link"));
```

### Arguments

- webLinkId `string`
  - The ID of the web link. Example: "12345"
- requestBody `UpdateSharedLinkOnWebLinkRequestBody`
  - Request body of updateSharedLinkOnWebLink method
- queryParams `UpdateSharedLinkOnWebLinkQueryParams`
  - Query parameters of updateSharedLinkOnWebLink method
- headers `UpdateSharedLinkOnWebLinkHeaders`
  - Headers of updateSharedLinkOnWebLink method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `WebLink`.

Returns a basic representation of the web link, with the updated shared
link attached.


## Remove shared link from web link

Removes a shared link from a web link.

This operation is performed by calling function `RemoveSharedLinkFromWebLink`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-web-links-id--remove-shared-link/).

<!-- sample put_web_links_id#remove_shared_link -->
```
await client.SharedLinksWebLinks.RemoveSharedLinkFromWebLinkAsync(webLinkId: webLinkId, requestBody: new RemoveSharedLinkFromWebLinkRequestBody() { SharedLink = null }, queryParams: new RemoveSharedLinkFromWebLinkQueryParams(fields: "shared_link"));
```

### Arguments

- webLinkId `string`
  - The ID of the web link. Example: "12345"
- requestBody `RemoveSharedLinkFromWebLinkRequestBody`
  - Request body of removeSharedLinkFromWebLink method
- queryParams `RemoveSharedLinkFromWebLinkQueryParams`
  - Query parameters of removeSharedLinkFromWebLink method
- headers `RemoveSharedLinkFromWebLinkHeaders`
  - Headers of removeSharedLinkFromWebLink method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `WebLink`.

Returns a basic representation of a web link, with the
shared link removed.


