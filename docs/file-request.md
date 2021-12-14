File Requests
=============

File request objects represent a file request associated with a folder.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Get a File Request's Information](#get-a-file-requests-information)
- [Copy a File Request's Information](#copy-a-file-requests-information)
- [Update a File Request's Information](#update-a-file-requests-information)
- [Delete a File Request](#delete-a-file-request)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Get a File Request's Information
------------------------

Calling `FileRequestsManager.GetFileRequestByIdAsync(string fileRequestId)`returns information on a file request.

<!-- sample get_file_requests_id -->
```c#
BoxFileRequestObject fileRequest = await client.FileRequestsManager.GetFileRequestByIdAsync("12345");
```

Copy a File Request's Information
---------------------------

Calling  `FileRequestsManager.CopyFileRequestAsync(string fileRequestId, BoxFileRequestCopyRequest copyRequest)` copies an existing file request that is already present 
on one folder, and applies it to another folder.

<!-- sample post_file_requests_id_copy -->
```c#
var destinationFolder = new BoxRequestEntity
{
    Id = "123456",
    Type = BoxType.folder
};

var copyRequest = new BoxFileRequestCopyRequest
{
    Description = "New file request description",
    Folder = destinationFolder
};

BoxFileRequestObject fileRequest = await client.FileRequestsManager.CopyFileRequestAsync("12345", copyRequest);
```

Update a File Request's Information
---------------------------

Calling `FileRequestsManager.UpdateFileRequestAsync(string fileRequestId, BoxFileRequestUpdateRequest updateRequest)` updates a file request. This can be used to activate 
or deactivate a file request.

<!-- sample put_file_requests_id -->
```c#
var updateRequest = new BoxFileRequestUpdateRequest
{
    Description = "New file request description",
    Status = BoxFileRequestStatus.inactive
};

BoxFileRequestObject fileRequest = await client.FileRequestsManager.UpdateFileRequestAsync("12345", updateRequest);
```

Delete a File Request
-------------

Calling `FileRequestsManager.DeleteFileRequestAsync(string fileRequestId)` deletes a file request permanently.

<!-- sample delete_file_requests_id -->
```c#
bool isSuccess = await client.FileRequestsManager.DeleteFileRequestAsync("12345");
```