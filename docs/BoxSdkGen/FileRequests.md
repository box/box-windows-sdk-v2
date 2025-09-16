# IFileRequestsManager


- [Get file request](#get-file-request)
- [Update file request](#update-file-request)
- [Delete file request](#delete-file-request)
- [Copy file request](#copy-file-request)

## Get file request

Retrieves the information about a file request.

This operation is performed by calling function `GetFileRequestById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-file-requests-id/).

<!-- sample get_file_requests_id -->
```
await client.FileRequests.GetFileRequestByIdAsync(fileRequestId: fileRequestId);
```

### Arguments

- fileRequestId `string`
  - The unique identifier that represent a file request.  The ID for any file request can be determined by visiting a file request builder in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/filerequest/123` the `file_request_id` is `123`. Example: "123"
- headers `GetFileRequestByIdHeaders`
  - Headers of getFileRequestById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `FileRequest`.

Returns a file request object.


## Update file request

Updates a file request. This can be used to activate or
deactivate a file request.

This operation is performed by calling function `UpdateFileRequestById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-file-requests-id/).

<!-- sample put_file_requests_id -->
```
await client.FileRequests.UpdateFileRequestByIdAsync(fileRequestId: copiedFileRequest.Id, requestBody: new FileRequestUpdateRequest() { Title = "updated title", Description = "updated description" });
```

### Arguments

- fileRequestId `string`
  - The unique identifier that represent a file request.  The ID for any file request can be determined by visiting a file request builder in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/filerequest/123` the `file_request_id` is `123`. Example: "123"
- requestBody `FileRequestUpdateRequest`
  - Request body of updateFileRequestById method
- headers `UpdateFileRequestByIdHeaders`
  - Headers of updateFileRequestById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `FileRequest`.

Returns the updated file request object.


## Delete file request

Deletes a file request permanently.

This operation is performed by calling function `DeleteFileRequestById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-file-requests-id/).

<!-- sample delete_file_requests_id -->
```
await client.FileRequests.DeleteFileRequestByIdAsync(fileRequestId: updatedFileRequest.Id);
```

### Arguments

- fileRequestId `string`
  - The unique identifier that represent a file request.  The ID for any file request can be determined by visiting a file request builder in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/filerequest/123` the `file_request_id` is `123`. Example: "123"
- headers `DeleteFileRequestByIdHeaders`
  - Headers of deleteFileRequestById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the file request has been successfully
deleted.


## Copy file request

Copies an existing file request that is already present on one folder,
and applies it to another folder.

This operation is performed by calling function `CreateFileRequestCopy`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-file-requests-id-copy/).

<!-- sample post_file_requests_id_copy -->
```
await client.FileRequests.CreateFileRequestCopyAsync(fileRequestId: fileRequestId, requestBody: new FileRequestCopyRequest(folder: new FileRequestCopyRequestFolderField(id: fileRequest.Folder.Id) { Type = FileRequestCopyRequestFolderTypeField.Folder }));
```

### Arguments

- fileRequestId `string`
  - The unique identifier that represent a file request.  The ID for any file request can be determined by visiting a file request builder in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/filerequest/123` the `file_request_id` is `123`. Example: "123"
- requestBody `FileRequestCopyRequest`
  - Request body of createFileRequestCopy method
- headers `CreateFileRequestCopyHeaders`
  - Headers of createFileRequestCopy method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `FileRequest`.

Returns updated file request object.


