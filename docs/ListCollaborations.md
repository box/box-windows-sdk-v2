# IListCollaborationsManager


- [List file collaborations](#list-file-collaborations)
- [List folder collaborations](#list-folder-collaborations)
- [List pending collaborations](#list-pending-collaborations)
- [List group collaborations](#list-group-collaborations)

## List file collaborations

Retrieves a list of pending and active collaborations for a
file. This returns all the users that have access to the file
or have been invited to the file.

This operation is performed by calling function `GetFileCollaborations`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-files-id-collaborations/).

<!-- sample get_files_id_collaborations -->
```
await client.ListCollaborations.GetFileCollaborationsAsync(fileId: file.Id);
```

### Arguments

- fileId `string`
  - The unique identifier that represents a file.  The ID for any file can be determined by visiting a file in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/files/123` the `file_id` is `123`. Example: "12345"
- queryParams `GetFileCollaborationsQueryParams`
  - Query parameters of getFileCollaborations method
- headers `GetFileCollaborationsHeaders`
  - Headers of getFileCollaborations method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `Collaborations`.

Returns a collection of collaboration objects. If there are no
collaborations on this file an empty collection will be returned.

This list includes pending collaborations, for which the `status`
is set to `pending`, indicating invitations that have been sent but not
yet accepted.


## List folder collaborations

Retrieves a list of pending and active collaborations for a
folder. This returns all the users that have access to the folder
or have been invited to the folder.

This operation is performed by calling function `GetFolderCollaborations`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-folders-id-collaborations/).

<!-- sample get_folders_id_collaborations -->
```
await client.ListCollaborations.GetFolderCollaborationsAsync(folderId: folder.Id);
```

### Arguments

- folderId `string`
  - The unique identifier that represent a folder.  The ID for any folder can be determined by visiting this folder in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/folder/123` the `folder_id` is `123`. Example: "12345"
- queryParams `GetFolderCollaborationsQueryParams`
  - Query parameters of getFolderCollaborations method
- headers `GetFolderCollaborationsHeaders`
  - Headers of getFolderCollaborations method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `Collaborations`.

Returns a collection of collaboration objects. If there are no
collaborations on this folder an empty collection will be returned.

This list includes pending collaborations, for which the `status`
is set to `pending`, indicating invitations that have been sent but not
yet accepted.


## List pending collaborations

Retrieves all pending collaboration invites for this user.

This operation is performed by calling function `GetCollaborations`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-collaborations/).

<!-- sample get_collaborations -->
```
await client.ListCollaborations.GetCollaborationsAsync(queryParams: new GetCollaborationsQueryParams(status: GetCollaborationsQueryParamsStatusField.Pending));
```

### Arguments

- queryParams `GetCollaborationsQueryParams`
  - Query parameters of getCollaborations method
- headers `GetCollaborationsHeaders`
  - Headers of getCollaborations method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `CollaborationsOffsetPaginated`.

Returns a collection of pending collaboration objects.

If the user has no pending collaborations, the collection
will be empty.


## List group collaborations

Retrieves all the collaborations for a group. The user
must have admin permissions to inspect enterprise's groups.

Each collaboration object has details on which files or
folders the group has access to and with what role.

This operation is performed by calling function `GetGroupCollaborations`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-groups-id-collaborations/).

<!-- sample get_groups_id_collaborations -->
```
await client.ListCollaborations.GetGroupCollaborationsAsync(groupId: group.Id);
```

### Arguments

- groupId `string`
  - The ID of the group. Example: "57645"
- queryParams `GetGroupCollaborationsQueryParams`
  - Query parameters of getGroupCollaborations method
- headers `GetGroupCollaborationsHeaders`
  - Headers of getGroupCollaborations method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `CollaborationsOffsetPaginated`.

Returns a collection of collaboration objects. If there are no
collaborations, an empty collection will be returned.


