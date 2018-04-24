Files
=====

File objects represent individual files in Box. They can be used to download a
file's contents, upload new versions, and perform other common file operations
(move, copy, delete, etc.).

Get a File's Information
------------------------

To get information about a specific file, call
`FilesManager.GetInformationAsync(string id, IEnumerable<string> fields = null)` with the
ID of the file.

```c#
BoxFile file = await client.FilesManager.GetInformationAsync(id: "11111");
```

Update a File's Information
---------------------------

Updating a file's information is done by calling
`FilesManager.UpdateInformationAsync(BoxFileRequest fileRequest, string etag = null, IEnumerable<string> fields = null)`
with the fields of the file object to update.

```c#
// Rename file 11111
var requestParams = new BoxFileRequest()
{
    Id = "11111",
    Name = "New name.pdf"
};
BoxFile updatedFile = await client.FilesManager.UpdateInformationAsync(requestParams);
```

Download a File
---------------

A file can be downloaded by calling
`FilesManager.DownloadStreamAsync(string id, string versionId = null, TimeSpan? timeout = null, int? startOffsetInBytes = null, int? endOffsetInBytes = null)`,
which provides a `Stream` that will yield the file's contents.

```c#
Stream fileContents = await client.FilesManager.DownloadStreamAsync(id: "11111");
```

Get a File's Download URL
-------------------------

The download URL of a file an be retrieved by calling
`FilesManager.GetDownloadUriAsync(string id, string versionId = null)` with the ID
of the file.

```c#
Uri downloadUri = await client.FilesManager.GetDownloadUriAsync(id: "11111");
```

Upload a File
-------------

The simplest way to upload a file to a folder is by calling
`FilesManager.UploadAsync(BoxFileRequest fileRequest, Stream stream, IEnumerable<string> fields = null, TimeSpan? timeout = null, byte[] contentMD5 = null, bool setStreamPositionToZero = true, Uri uploadUri = null)`
with the upload parameters and a stream of the file contents to upload.

```c#
using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
{
    BoxFileRequest requestParams = new BoxFileRequest()
    {
        Name = uploadFileName,
        Parent = new BoxRequestEntity() { Id = "0" }
    };

    BoxFile file = await client.FilesManager.UploadAsync(requestParams, fileStream);
}
```

Copy a File
-----------

A file can be copied to a new folder with the
`FilesManager.CopyAsync(BoxFileRequest fileRequest, IEnumerable<string> fields = null)`
method.

```c#
string fileId = "11111";
string destinationFolderId = "22222";
var requestParams = new BoxFileRequest()
{
    Id = fileId,
    Parent = new BoxRequestEntity()
    {
        Id = destinationFolderId
    }
};

BoxFile fileCopy = await client.FilesManager.CopyAsync(requestParams);
```

Delete a File
-------------

Calling the
`FilesManager.DeleteAsync(string id, string etag = null)`
method will move the file to the user's trash.

```c#
await client.FilesManager.DeleteAsync(id: "11111");
```

Get Previous File Versions
--------------------------

Retrieve a list of previous versions of a file by calling
`FilesManager.ViewVersionsAsync(string id, IEnumerable<string> fields = null)`
with the ID of the file.

```c#
BoxCollection<BoxFileVersion> previousVersions = await client.FilesManager
    .ViewVersionsAsync(id: "11111");
```

Upload a New Version of a File
------------------------------

A new version of a file can be uploaded by calling
`FilesManager.UploadNewVersionAsync(string fileName, string fileId, Stream stream, string etag = null, IEnumerable<string> fields = null, TimeSpan? timeout = null, byte[] contentMD5 = null, bool setStreamPositionToZero = true, Uri uploadUri = null)`
with the name and ID of the file, and a `Stream` of the new contents of the file.

```c#
using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
{
    BoxFile file = await client.FilesManager
        .UploadNewVersionAsync("File v2.pdf", "11111", fileStream);
}
```

Download a Previous Version of a File
-------------------------------------

For users with premium accounts, previous versions of a file can be downloaded
by calling
`FilesManager.DownloadStreamAsync(string id, string versionId = null, TimeSpan? timeout = null, int? startOffsetInBytes = null, int? endOffsetInBytes = null)`
with the ID of the file and the optional version ID.

```c#
string fileId = "11111";
Stream versionContents = await client.FilesManager.DownloadStreamAsync(fileId, versionId: "22222");
```

Promote Version
---------------

Promote file version to the top of the stack by calling `FilesManager.PromoteVersionAsync(string id, string versionId)`
with the ID of the file and the ID of the version to make the current version.  This will create a new file version with
the same contents as the previous version, as the current version.

```c#
string fileId = "11111";
BoxFileVersion current = await client.FilesManager.PromoteVersionAsync(fileId, versionId: "22222");
```

Delete a Previous File Version
------------------------------

An old version of a file can be moved to the trash by calling
`FilesManager.DeleteOldVersionAsync(string id, string versionId, string etag = null)`
with the ID of the file and the ID of the file version to delete.

```c#
string fileId = "11111";
await client.FilesManager.DeleteOldVersionAsync(fileId, versionId: "22222");
```

Lock a File
-----------

A file can be locked, which prevents other users from editing the file, by calling the
`FilesManager.LockAsync(BoxFileLockRequest lockFileRequest, string id)`
method.  You may optionally prevent other users from downloading the file, as well as set
an expiration time for the lock.

```c#
var lockParams = new BoxFileLockRequest()
{
    Lock = new BoxFileLock()
    {
        IsDownloadPrevented = true
    };
};
string fileId = "11111";
BoxFileLock lock = await client.FilesManager.LockAsync(lockParams, fileId);
```

Unlock a File
-------------

A file can be unlocked by calling `FilesManager.UnLock(string id)` with the ID of the file to unlock.

```c#
await client.FilesManager.UnLock(id: "11111");
```

Create a Shared Link
--------------------

A shared link for a file can be generated by calling
`FilesManager.CreateSharedLinkAsync(string id, BoxSharedLinkRequest sharedLinkRequest, IEnumerable<string> fields = null)`.

```c#
string fileId = "11111";
var sharedLinkParams = new BoxSharedLinkRequest()
{
    Access = BoxSharedLinkAccessType.open
};
BoxFile file = client.FilesManager.CreateSharedLinkAsync(fileId, sharedLinkParams);
string sharedLinkUrl = file.SharedLink.Url;
```

Get Embed Link
--------------

An embed link for a file can be generated by calling the `FilesManager.GetPreviewLinkAsync(string id)`
method with the ID of the file.  The embed link is an expiring URL for embedding a Box file preview into a webpage,
usually via an `<iframe>` element.

For more information, see the [API documentation](https://docs.box.com/reference#get-embed-link).

```c#
Uri embedUri = await client.FilesManager.GetPreviewLinkAsync(id: "11111");
```

Get Representation Info
-----------------------

A file's representation info can be retrieved by calling
`FilesManager.GetRepresentationsAsync(BoxRepresentationRequest representationRequest)`.
You will be able to fetch information regarding PDF representation, thumbnail representation, multi-page images
representation, and extracted text representation.

You can specify your own set of representations to get info for by manually constructing the
[X-Rep-Hints value][x-rep-hints] and passing it as `representationTypes`.

```c#
var requestParams = new BoxRepresentationRequest()
{
    FileId = "11111",
    XRepHints = "[pdf]"
};
BoxRepresentationCollection<BoxRepresentation> representations = client.FilesManager
    .GetRepresentationsAsync(requestParams);
```

[x-rep-hints]: https://developer.box.com/reference#section-x-rep-hints-header

Get Thumbnail
-------------

A thumbnail for a file can be retrieved by calling
`FilesManager.GetThumbnailAsync(string id, int? minHeight = null, int? minWidth = null, int? maxHeight = null, int? maxWidth = null, bool throttle = true, bool handleRetry = true)`.

```c#
Stream thumbnailContents = await client.FilesManager.GetThumbnailAsync("11111", maxWidth: 160, maxHeight: 160);
```