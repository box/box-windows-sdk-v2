# IHubCollaborationsManager


- [Get Box Hub collaborations](#get-box-hub-collaborations)
- [Create Box Hub collaboration](#create-box-hub-collaboration)
- [Get Box Hub collaboration by collaboration ID](#get-box-hub-collaboration-by-collaboration-id)
- [Update Box Hub collaboration](#update-box-hub-collaboration)
- [Remove Box Hub collaboration](#remove-box-hub-collaboration)

## Get Box Hub collaborations

Retrieves all collaborations for a Box Hub.

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

Retrieves the collaborations associated with the specified Box Hub.


## Create Box Hub collaboration

Adds a collaboration for a single user or a single group to a Box Hub.

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

Returns a new Box Hub collaboration object.


## Get Box Hub collaboration by collaboration ID

Retrieves details for a Box Hub collaboration by collaboration ID.

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

Returns a Box Hub collaboration object.


## Update Box Hub collaboration

Updates a Box Hub collaboration.
Can be used to change the Box Hub role.

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

Returns an updated Box Hub collaboration object.


## Remove Box Hub collaboration

Deletes a single Box Hub collaboration.

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

A blank response is returned if the Box Hub collaboration was
successfully deleted.


