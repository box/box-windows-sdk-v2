# IUserCollaborationsManager


- [Get collaboration](#get-collaboration)
- [Update collaboration](#update-collaboration)
- [Remove collaboration](#remove-collaboration)
- [Create collaboration](#create-collaboration)

## Get collaboration

Retrieves a single collaboration.

This operation is performed by calling function `GetCollaborationById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-collaborations-id/).

<!-- sample get_collaborations_id -->
```
await client.UserCollaborations.GetCollaborationByIdAsync(collaborationId: collaborationId);
```

### Arguments

- collaborationId `string`
  - The ID of the collaboration. Example: "1234"
- queryParams `GetCollaborationByIdQueryParams`
  - Query parameters of getCollaborationById method
- headers `GetCollaborationByIdHeaders`
  - Headers of getCollaborationById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `Collaboration`.

Returns a collaboration object.


## Update collaboration

Updates a collaboration.
Can be used to change the owner of an item, or to
accept collaboration invites.

This operation is performed by calling function `UpdateCollaborationById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-collaborations-id/).

<!-- sample put_collaborations_id -->
```
await client.UserCollaborations.UpdateCollaborationByIdAsync(collaborationId: collaborationId, requestBody: new UpdateCollaborationByIdRequestBody(role: UpdateCollaborationByIdRequestBodyRoleField.Viewer));
```

### Arguments

- collaborationId `string`
  - The ID of the collaboration. Example: "1234"
- requestBody `UpdateCollaborationByIdRequestBody`
  - Request body of updateCollaborationById method
- headers `UpdateCollaborationByIdHeaders`
  - Headers of updateCollaborationById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `Collaboration?`.

Returns an updated collaboration object unless the owner has changed.If the role is changed to `owner`, the collaboration is deleted
and a new collaboration is created. The previous `owner` of
the old collaboration will be a `co-owner` on the new collaboration.


## Remove collaboration

Deletes a single collaboration.

This operation is performed by calling function `DeleteCollaborationById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-collaborations-id/).

<!-- sample delete_collaborations_id -->
```
await client.UserCollaborations.DeleteCollaborationByIdAsync(collaborationId: collaborationId);
```

### Arguments

- collaborationId `string`
  - The ID of the collaboration. Example: "1234"
- headers `DeleteCollaborationByIdHeaders`
  - Headers of deleteCollaborationById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

A blank response is returned if the collaboration was
successfully deleted.


## Create collaboration

Adds a collaboration for a single user or a single group to a file
or folder.

Collaborations can be created using email address, user IDs, or a
group IDs.

If a collaboration is being created with a group, access to
this endpoint is dependent on the group's ability to be invited.

If collaboration is in `pending` status, the following fields
are redacted:
- `login` and `name` are hidden if a collaboration was created
using `user_id`,
-  `name` is hidden if a collaboration was created using `login`.

This operation is performed by calling function `CreateCollaboration`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-collaborations/).

<!-- sample post_collaborations -->
```
await client.UserCollaborations.CreateCollaborationAsync(requestBody: new CreateCollaborationRequestBody(item: new CreateCollaborationRequestBodyItemField() { Type = CreateCollaborationRequestBodyItemTypeField.Folder, Id = folder.Id }, accessibleBy: new CreateCollaborationRequestBodyAccessibleByField(type: CreateCollaborationRequestBodyAccessibleByTypeField.User) { Id = user.Id }, role: CreateCollaborationRequestBodyRoleField.Editor));
```

### Arguments

- requestBody `CreateCollaborationRequestBody`
  - Request body of createCollaboration method
- queryParams `CreateCollaborationQueryParams`
  - Query parameters of createCollaboration method
- headers `CreateCollaborationHeaders`
  - Headers of createCollaboration method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `Collaboration`.

Returns a new collaboration object.


