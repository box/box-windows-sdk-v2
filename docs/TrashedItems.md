# ITrashedItemsManager


- [List trashed items](#list-trashed-items)

## List trashed items

Retrieves the files and folders that have been moved
to the trash.

Any attribute in the full files or folders objects can be passed
in with the `fields` parameter to retrieve those specific
attributes that are not returned by default.

This endpoint defaults to use offset-based pagination, yet also supports
marker-based pagination using the `marker` parameter.

This operation is performed by calling function `GetTrashedItems`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-folders-trash-items/).

<!-- sample get_folders_trash_items -->
```
await client.TrashedItems.GetTrashedItemsAsync();
```

### Arguments

- queryParams `GetTrashedItemsQueryParams`
  - Query parameters of getTrashedItems method
- headers `GetTrashedItemsHeaders`
  - Headers of getTrashedItems method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `Items`.

Returns a list of items that have been deleted.


