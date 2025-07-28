# IFileVersionsManager


- [List all file versions](#list-all-file-versions)
- [Get file version](#get-file-version)
- [Remove file version](#remove-file-version)
- [Restore file version](#restore-file-version)
- [Promote file version](#promote-file-version)

## List all file versions

Retrieve a list of the past versions for a file.

Versions are only tracked by Box users with premium accounts. To fetch the ID
of the current version of a file, use the `GET /file/:id` API.

This operation is performed by calling function `GetFileVersions`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-files-id-versions/).

<!-- sample get_files_id_versions -->
```
await client.FileVersions.GetFileVersionsAsync(fileId: file.Id);
```

### Arguments

- fileId `string`
  - The unique identifier that represents a file.  The ID for any file can be determined by visiting a file in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/files/123` the `file_id` is `123`. Example: "12345"
- queryParams `GetFileVersionsQueryParams`
  - Query parameters of getFileVersions method
- headers `GetFileVersionsHeaders`
  - Headers of getFileVersions method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `FileVersions`.

Returns an array of past versions for this file.


## Get file version

Retrieve a specific version of a file.

Versions are only tracked for Box users with premium accounts.

This operation is performed by calling function `GetFileVersionById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-files-id-versions-id/).

<!-- sample get_files_id_versions_id -->
```
await client.FileVersions.GetFileVersionByIdAsync(fileId: file.Id, fileVersionId: NullableUtils.Unwrap(fileVersions.Entries)[0].Id);
```

### Arguments

- fileId `string`
  - The unique identifier that represents a file.  The ID for any file can be determined by visiting a file in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/files/123` the `file_id` is `123`. Example: "12345"
- fileVersionId `string`
  - The ID of the file version. Example: "1234"
- queryParams `GetFileVersionByIdQueryParams`
  - Query parameters of getFileVersionById method
- headers `GetFileVersionByIdHeaders`
  - Headers of getFileVersionById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `FileVersionFull`.

Returns a specific version of a file.

Not all available fields are returned by default. Use the
[fields](#param-fields) query parameter to explicitly request
any specific fields.


## Remove file version

Move a file version to the trash.

Versions are only tracked for Box users with premium accounts.

This operation is performed by calling function `DeleteFileVersionById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-files-id-versions-id/).

<!-- sample delete_files_id_versions_id -->
```
await client.FileVersions.DeleteFileVersionByIdAsync(fileId: file.Id, fileVersionId: fileVersion.Id);
```

### Arguments

- fileId `string`
  - The unique identifier that represents a file.  The ID for any file can be determined by visiting a file in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/files/123` the `file_id` is `123`. Example: "12345"
- fileVersionId `string`
  - The ID of the file version. Example: "1234"
- headers `DeleteFileVersionByIdHeaders`
  - Headers of deleteFileVersionById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the file has been successfully
deleted.


## Restore file version

Restores a specific version of a file after it was deleted.
Don't use this endpoint to restore Box Notes,
as it works with file formats such as PDF, DOC,
PPTX or similar.

This operation is performed by calling function `UpdateFileVersionById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-files-id-versions-id/).

<!-- sample put_files_id_versions_id -->
```
await client.FileVersions.UpdateFileVersionByIdAsync(fileId: file.Id, fileVersionId: fileVersion.Id, requestBody: new UpdateFileVersionByIdRequestBody() { TrashedAt = null });
```

### Arguments

- fileId `string`
  - The unique identifier that represents a file.  The ID for any file can be determined by visiting a file in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/files/123` the `file_id` is `123`. Example: "12345"
- fileVersionId `string`
  - The ID of the file version. Example: "1234"
- requestBody `UpdateFileVersionByIdRequestBody`
  - Request body of updateFileVersionById method
- headers `UpdateFileVersionByIdHeaders`
  - Headers of updateFileVersionById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `FileVersionFull`.

Returns a restored file version object.


## Promote file version

Promote a specific version of a file.

If previous versions exist, this method can be used to
promote one of the older versions to the top of the version history.

This creates a new copy of the old version and puts it at the
top of the versions history. The file will have the exact same contents
as the older version, with the the same hash digest, `etag`, and
name as the original.

Other properties such as comments do not get updated to their
former values.

Don't use this endpoint to restore Box Notes,
as it works with file formats such as PDF, DOC,
PPTX or similar.

This operation is performed by calling function `PromoteFileVersion`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-files-id-versions-current/).

<!-- sample post_files_id_versions_current -->
```
await client.FileVersions.PromoteFileVersionAsync(fileId: file.Id, requestBody: new PromoteFileVersionRequestBody() { Id = NullableUtils.Unwrap(fileVersions.Entries)[0].Id, Type = PromoteFileVersionRequestBodyTypeField.FileVersion });
```

### Arguments

- fileId `string`
  - The unique identifier that represents a file.  The ID for any file can be determined by visiting a file in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/files/123` the `file_id` is `123`. Example: "12345"
- requestBody `PromoteFileVersionRequestBody`
  - Request body of promoteFileVersion method
- queryParams `PromoteFileVersionQueryParams`
  - Query parameters of promoteFileVersion method
- headers `PromoteFileVersionHeaders`
  - Headers of promoteFileVersion method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `FileVersionFull`.

Returns a newly created file version object.


