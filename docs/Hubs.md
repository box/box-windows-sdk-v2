# IHubsManager


- [List all hubs](#list-all-hubs)
- [Create hub](#create-hub)
- [List all hubs for requesting enterprise](#list-all-hubs-for-requesting-enterprise)
- [Get hub information by ID](#get-hub-information-by-id)
- [Update hub information by ID](#update-hub-information-by-id)
- [Delete hub](#delete-hub)
- [Copy hub](#copy-hub)

## List all hubs

Retrieves all hubs for requesting user.

This operation is performed by calling function `GetHubsV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-hubs/).

<!-- sample get_hubs_v2025.0 -->
```
await client.Hubs.GetHubsV2025R0Async(queryParams: new GetHubsV2025R0QueryParams() { Scope = "all", Sort = "name", Direction = GetHubsV2025R0QueryParamsDirectionField.Asc });
```

### Arguments

- queryParams `GetHubsV2025R0QueryParams`
  - Query parameters of getHubsV2025R0 method
- headers `GetHubsV2025R0Headers`
  - Headers of getHubsV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `HubsV2025R0`.

Returns all hubs for the given user or enterprise.


## Create hub

Creates a new Hub.

This operation is performed by calling function `CreateHubV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/post-hubs/).

<!-- sample post_hubs_v2025.0 -->
```
await client.Hubs.CreateHubV2025R0Async(requestBody: new HubCreateRequestV2025R0(title: hubTitle) { Description = hubDescription });
```

### Arguments

- requestBody `HubCreateRequestV2025R0`
  - Request body of createHubV2025R0 method
- headers `CreateHubV2025R0Headers`
  - Headers of createHubV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `HubV2025R0`.

Returns a new Hub object.


## List all hubs for requesting enterprise

Retrieves all hubs for a given enterprise.

Admins or Hub Co-admins of an enterprise
with GCM scope can make this call.

This operation is performed by calling function `GetEnterpriseHubsV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-enterprise-hubs/).

<!-- sample get_enterprise_hubs_v2025.0 -->
```
await client.Hubs.GetEnterpriseHubsV2025R0Async(queryParams: new GetEnterpriseHubsV2025R0QueryParams() { Sort = "name", Direction = GetEnterpriseHubsV2025R0QueryParamsDirectionField.Asc });
```

### Arguments

- queryParams `GetEnterpriseHubsV2025R0QueryParams`
  - Query parameters of getEnterpriseHubsV2025R0 method
- headers `GetEnterpriseHubsV2025R0Headers`
  - Headers of getEnterpriseHubsV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `HubsV2025R0`.

Returns all hubs for the given user or enterprise.


## Get hub information by ID

Retrieves details for a hub by its ID.

This operation is performed by calling function `GetHubByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-hubs-id/).

<!-- sample get_hubs_id_v2025.0 -->
```
await client.Hubs.GetHubByIdV2025R0Async(hubId: hubId);
```

### Arguments

- hubId `string`
  - The unique identifier that represent a hub.  The ID for any hub can be determined by visiting this hub in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/hubs/123` the `hub_id` is `123`. Example: "12345"
- headers `GetHubByIdV2025R0Headers`
  - Headers of getHubByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `HubV2025R0`.

Returns a hub object.


## Update hub information by ID

Updates a Hub. Can be used to change title, description, or Hub settings.

This operation is performed by calling function `UpdateHubByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/put-hubs-id/).

<!-- sample put_hubs_id_v2025.0 -->
```
await client.Hubs.UpdateHubByIdV2025R0Async(hubId: hubId, requestBody: new HubUpdateRequestV2025R0() { Title = newHubTitle, Description = newHubDescription });
```

### Arguments

- hubId `string`
  - The unique identifier that represent a hub.  The ID for any hub can be determined by visiting this hub in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/hubs/123` the `hub_id` is `123`. Example: "12345"
- requestBody `HubUpdateRequestV2025R0`
  - Request body of updateHubByIdV2025R0 method
- headers `UpdateHubByIdV2025R0Headers`
  - Headers of updateHubByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `HubV2025R0`.

Returns a Hub object.


## Delete hub

Deletes a single hub.

This operation is performed by calling function `DeleteHubByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/delete-hubs-id/).

<!-- sample delete_hubs_id_v2025.0 -->
```
await client.Hubs.DeleteHubByIdV2025R0Async(hubId: hubId);
```

### Arguments

- hubId `string`
  - The unique identifier that represent a hub.  The ID for any hub can be determined by visiting this hub in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/hubs/123` the `hub_id` is `123`. Example: "12345"
- headers `DeleteHubByIdV2025R0Headers`
  - Headers of deleteHubByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

A blank response is returned if the hub was
successfully deleted.


## Copy hub

Creates a copy of a Hub.

The original Hub will not be modified.

This operation is performed by calling function `CopyHubV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/post-hubs-id-copy/).

<!-- sample post_hubs_id_copy_v2025.0 -->
```
await client.Hubs.CopyHubV2025R0Async(hubId: createdHub.Id, requestBody: new HubCopyRequestV2025R0() { Title = copiedHubTitle, Description = copiedHubDescription });
```

### Arguments

- hubId `string`
  - The unique identifier that represent a hub.  The ID for any hub can be determined by visiting this hub in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/hubs/123` the `hub_id` is `123`. Example: "12345"
- requestBody `HubCopyRequestV2025R0`
  - Request body of copyHubV2025R0 method
- headers `CopyHubV2025R0Headers`
  - Headers of copyHubV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `HubV2025R0`.

Returns a new Hub object.


