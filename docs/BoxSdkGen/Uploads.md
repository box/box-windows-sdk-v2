# IUploadsManager


- [Upload file version](#upload-file-version)
- [Preflight check before upload](#preflight-check-before-upload)
- [Upload file](#upload-file)
- [Upload a file with a preflight check](#upload-a-file-with-a-preflight-check)

## Upload file version

Update a file's content. For file sizes over 50MB we recommend
using the Chunk Upload APIs.

The `attributes` part of the body must come **before** the
`file` part. Requests that do not follow this format when
uploading the file will receive a HTTP `400` error with a
`metadata_after_file_contents` error code.

This operation is performed by calling function `UploadFileVersion`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-files-id-content/).

<!-- sample post_files_id_content -->
```
await client.Uploads.UploadFileVersionAsync(fileId: uploadedFile.Id, requestBody: new UploadFileVersionRequestBody(attributes: new UploadFileVersionRequestBodyAttributesField(name: newFileVersionName), file: newFileContentStream));
```

### Arguments

- fileId `string`
  - The unique identifier that represents a file.  The ID for any file can be determined by visiting a file in the web application and copying the ID from the URL. For example, for the URL `https://*.app.box.com/files/123` the `file_id` is `123`. Example: "12345"
- requestBody `UploadFileVersionRequestBody`
  - Request body of uploadFileVersion method
- queryParams `UploadFileVersionQueryParams`
  - Query parameters of uploadFileVersion method
- headers `UploadFileVersionHeaders`
  - Headers of uploadFileVersion method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `Files`.

Returns the new file object in a list.


## Preflight check before upload

Performs a check to verify that a file will be accepted by Box
before you upload the entire file.

This operation is performed by calling function `PreflightFileUploadCheck`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/options-files-content/).

<!-- sample options_files_content -->
```
await client.Uploads.PreflightFileUploadCheckAsync(requestBody: new PreflightFileUploadCheckRequestBody() { Name = newFileName, Size = 1024 * 1024, Parent = new PreflightFileUploadCheckRequestBodyParentField() { Id = "0" } });
```

### Arguments

- requestBody `PreflightFileUploadCheckRequestBody`
  - Request body of preflightFileUploadCheck method
- headers `PreflightFileUploadCheckHeaders`
  - Headers of preflightFileUploadCheck method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `UploadUrl`.

If the check passed, the response will include a session URL that
can be used to upload the file to.


## Upload file

Uploads a small file to Box. For file sizes over 50MB we recommend
using the Chunk Upload APIs.

The `attributes` part of the body must come **before** the
`file` part. Requests that do not follow this format when
uploading the file will receive a HTTP `400` error with a
`metadata_after_file_contents` error code.

This operation is performed by calling function `UploadFile`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-files-content/).

<!-- sample post_files_content -->
```
await client.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: newFileName, parent: new UploadFileRequestBodyAttributesParentField(id: "0")), file: fileContentStream));
```

### Arguments

- requestBody `UploadFileRequestBody`
  - Request body of uploadFile method
- queryParams `UploadFileQueryParams`
  - Query parameters of uploadFile method
- headers `UploadFileHeaders`
  - Headers of uploadFile method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `Files`.

Returns the new file object in a list.


## Upload a file with a preflight check

 Upload a file with a preflight check

This operation is performed by calling function `UploadWithPreflightCheck`.



```
await client.Uploads.UploadWithPreflightCheckAsync(requestBody: new UploadWithPreflightCheckRequestBody(attributes: new UploadWithPreflightCheckRequestBodyAttributesField(name: newFileName, size: -1, parent: new UploadWithPreflightCheckRequestBodyAttributesParentField(id: "0")), file: fileContentStream));
```

### Arguments

- requestBody `UploadWithPreflightCheckRequestBody`
  
- queryParams `UploadWithPreflightCheckQueryParams`
  - Query parameters of uploadFile method
- headers `UploadWithPreflightCheckHeaders`
  - Headers of uploadFile method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `Files`.




