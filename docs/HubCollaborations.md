# IHubCollaborationsManager


- [Get hub collaborations](#get-hub-collaborations)
- [Create hub collaboration](#create-hub-collaboration)
- [Get hub collaboration by collaboration ID](#get-hub-collaboration-by-collaboration-id)
- [Update hub collaboration](#update-hub-collaboration)
- [Remove hub collaboration](#remove-hub-collaboration)

## Get hub collaborations

Retrieves all collaborations for a hub.

This operation is performed by calling function `GetHubCollaborationsV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-hub-collaborations/).

<!-- sample get_hub_collaborations_v2025.0 -->
```
await client.HubCollaborations.GetHubCollaborationsV2025R0Async(queryParams: new GetHubCollaborationsV2025R0QueryParams(hubId: hub.Id));
```

### Arguments

- queryParams `GetHubCollaborationsV2025R0QueryParams`
  - Query parameters of getHubCollaborationsV2025R0 method
- headers `GetHubCollaborationsV2025R0Headers`
  - Headers of getHubCollaborationsV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `HubCollaborationsV2025R0`.

Retrieves the collaborations associated with the specified hub.


## Create hub collaboration

Adds a collaboration for a single user or a single group to a hub.

Collaborations can be created using email address, user IDs, or group IDs.

This operation is performed by calling function `CreateHubCollaborationV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/post-hub-collaborations/).

<!-- sample post_hub_collaborations_v2025.0 -->
```
await client.HubCollaborations.CreateHubCollaborationV2025R0Async(requestBody: new HubCollaborationCreateRequestV2025R0(hub: new HubCollaborationCreateRequestV2025R0HubField(id: hub.Id), accessibleBy: new HubCollaborationCreateRequestV2025R0AccessibleByField(type: "user") { Id = user.Id }, role: "viewer"));
```

### Arguments

- requestBody `HubCollaborationCreateRequestV2025R0`
  - Request body of createHubCollaborationV2025R0 method
- headers `CreateHubCollaborationV2025R0Headers`
  - Headers of createHubCollaborationV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `HubCollaborationV2025R0`.

Returns a new hub collaboration object.


## Get hub collaboration by collaboration ID

Retrieves details for a hub collaboration by collaboration ID.

This operation is performed by calling function `GetHubCollaborationByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-hub-collaborations-id/).

<!-- sample get_hub_collaborations_id_v2025.0 -->
```
await client.HubCollaborations.GetHubCollaborationByIdV2025R0Async(hubCollaborationId: createdCollaboration.Id);
```

### Arguments

- hubCollaborationId `string`
  - The ID of the hub collaboration. Example: "1234"
- headers `GetHubCollaborationByIdV2025R0Headers`
  - Headers of getHubCollaborationByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `HubCollaborationV2025R0`.

Returns a hub collaboration object.


## Update hub collaboration

Updates a hub collaboration.
Can be used to change the hub role.

This operation is performed by calling function `UpdateHubCollaborationByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/put-hub-collaborations-id/).

<!-- sample put_hub_collaborations_id_v2025.0 -->
```
await client.HubCollaborations.UpdateHubCollaborationByIdV2025R0Async(hubCollaborationId: createdCollaboration.Id, requestBody: new HubCollaborationUpdateRequestV2025R0() { Role = "editor" });
```

### Arguments

- hubCollaborationId `string`
  - The ID of the hub collaboration. Example: "1234"
- requestBody `HubCollaborationUpdateRequestV2025R0`
  - Request body of updateHubCollaborationByIdV2025R0 method
- headers `UpdateHubCollaborationByIdV2025R0Headers`
  - Headers of updateHubCollaborationByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `HubCollaborationV2025R0`.

Returns an updated hub collaboration object.


## Remove hub collaboration

Deletes a single hub collaboration.

This operation is performed by calling function `DeleteHubCollaborationByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/delete-hub-collaborations-id/).

<!-- sample delete_hub_collaborations_id_v2025.0 -->
```
await client.HubCollaborations.DeleteHubCollaborationByIdV2025R0Async(hubCollaborationId: createdCollaboration.Id);
```

### Arguments

- hubCollaborationId `string`
  - The ID of the hub collaboration. Example: "1234"
- headers `DeleteHubCollaborationByIdV2025R0Headers`
  - Headers of deleteHubCollaborationByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

A blank response is returned if the hub collaboration was
successfully deleted.


