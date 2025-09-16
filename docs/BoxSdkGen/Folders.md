# IFoldersManager


- [Get folder information](#get-folder-information)
- [Update folder](#update-folder)
- [Delete folder](#delete-folder)
- [List items in folder](#list-items-in-folder)
- [Create folder](#create-folder)
- [Copy folder](#copy-folder)

## Get folder information

Retrieves details for a folder, including the first 100 entries
in the folder.

Passing `sort`, `direction`, `offset`, and `limit`
parameters in query allows you to manage the
list of returned
[folder items](r://folder--full#param-item-collection).

To fetch more items within the folder, use the
[Get items in a folder](e://get-folders-id-items) endpoint.

This operation is performed by calling function `GetFolderById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-folders-id/).

<!-- sample get_folders_id -->
```
await client.Folders.GetFolderByIdAsync(folderId: "0");
```

### Arguments

- folderId `string`
  - The unique identifier that represent a folder.  The ID for any folder can be determined by visiting this folder in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/folder/123` the `folder_id` is `123`.  The root folder of a Box account is always represented by the ID `0`. Example: "12345"
- queryParams `GetFolderByIdQueryParams`
  - Query parameters of getFolderById method
- headers `GetFolderByIdHeaders`
  - Headers of getFolderById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `FolderFull`.

Returns a folder, including the first 100 entries in the folder.
If you used query parameters like
`sort`, `direction`, `offset`, or `limit`
the *folder items list* will be affected accordingly.

To fetch more items within the folder, use the
[Get items in a folder](e://get-folders-id-items)) endpoint.

Not all available fields are returned by default. Use the
[fields](#param-fields) query parameter to explicitly request
any specific fields.


## Update folder

Updates a folder. This can be also be used to move the folder,
create shared links, update collaborations, and more.

This operation is performed by calling function `UpdateFolderById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-folders-id/).

<!-- sample put_folders_id -->
```
await client.Folders.UpdateFolderByIdAsync(folderId: folderToUpdate.Id, requestBody: new UpdateFolderByIdRequestBody() { Name = updatedName, Description = "Updated description" });
```

### Arguments

- folderId `string`
  - The unique identifier that represent a folder.  The ID for any folder can be determined by visiting this folder in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/folder/123` the `folder_id` is `123`.  The root folder of a Box account is always represented by the ID `0`. Example: "12345"
- requestBody `UpdateFolderByIdRequestBody`
  - Request body of updateFolderById method
- queryParams `UpdateFolderByIdQueryParams`
  - Query parameters of updateFolderById method
- headers `UpdateFolderByIdHeaders`
  - Headers of updateFolderById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `FolderFull`.

Returns a folder object for the updated folder

Not all available fields are returned by default. Use the
[fields](#param-fields) query parameter to explicitly request
any specific fields.

If the user is moving folders with a large number of items in all of
their descendants, the call will be run asynchronously. If the
operation is not completed within 10 minutes, the user will receive
a 200 OK response, and the operation will continue running.


## Delete folder

Deletes a folder, either permanently or by moving it to
the trash.

This operation is performed by calling function `DeleteFolderById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-folders-id/).

<!-- sample delete_folders_id -->
```
await client.Folders.DeleteFolderByIdAsync(folderId: newFolder.Id);
```

### Arguments

- folderId `string`
  - The unique identifier that represent a folder.  The ID for any folder can be determined by visiting this folder in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/folder/123` the `folder_id` is `123`.  The root folder of a Box account is always represented by the ID `0`. Example: "12345"
- queryParams `DeleteFolderByIdQueryParams`
  - Query parameters of deleteFolderById method
- headers `DeleteFolderByIdHeaders`
  - Headers of deleteFolderById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the folder is successfully deleted
or moved to the trash.


## List items in folder

Retrieves a page of items in a folder. These items can be files,
folders, and web links.

To request more information about the folder itself, like its size,
use the [Get a folder](#get-folders-id) endpoint instead.

This operation is performed by calling function `GetFolderItems`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-folders-id-items/).

<!-- sample get_folders_id_items -->
```
await client.Folders.GetFolderItemsAsync(folderId: folderOrigin.Id);
```

### Arguments

- folderId `string`
  - The unique identifier that represent a folder.  The ID for any folder can be determined by visiting this folder in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/folder/123` the `folder_id` is `123`.  The root folder of a Box account is always represented by the ID `0`. Example: "12345"
- queryParams `GetFolderItemsQueryParams`
  - Query parameters of getFolderItems method
- headers `GetFolderItemsHeaders`
  - Headers of getFolderItems method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `Items`.

Returns a collection of files, folders, and web links contained in a folder.


## Create folder

Creates a new empty folder within the specified parent folder.

This operation is performed by calling function `CreateFolder`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-folders/).

<!-- sample post_folders -->
```
await client.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: newFolderName, parent: new CreateFolderRequestBodyParentField(id: "0")));
```

### Arguments

- requestBody `CreateFolderRequestBody`
  - Request body of createFolder method
- queryParams `CreateFolderQueryParams`
  - Query parameters of createFolder method
- headers `CreateFolderHeaders`
  - Headers of createFolder method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `FolderFull`.

Returns a folder object.

Not all available fields are returned by default. Use the
[fields](#param-fields) query parameter to explicitly request
any specific fields.


## Copy folder

Creates a copy of a folder within a destination folder.

The original folder will not be changed.

This operation is performed by calling function `CopyFolder`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-folders-id-copy/).

<!-- sample post_folders_id_copy -->
```
await client.Folders.CopyFolderAsync(folderId: folderOrigin.Id, requestBody: new CopyFolderRequestBody(parent: new CopyFolderRequestBodyParentField(id: "0")) { Name = copiedFolderName });
```

### Arguments

- folderId `string`
  - The unique identifier of the folder to copy.  The ID for any folder can be determined by visiting this folder in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/folder/123` the `folder_id` is `123`.  The root folder with the ID `0` can not be copied. Example: "0"
- requestBody `CopyFolderRequestBody`
  - Request body of copyFolder method
- queryParams `CopyFolderQueryParams`
  - Query parameters of copyFolder method
- headers `CopyFolderHeaders`
  - Headers of copyFolder method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `FolderFull`.

Returns a new folder object representing the copied folder.

Not all available fields are returned by default. Use the
[fields](#param-fields) query parameter to explicitly request
any specific fields.


