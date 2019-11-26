Folders
=======

Folder objects represent a folder from a user's account. They can be used to
iterate through a folder's contents, collaborate a folder with another user or
group, and perform other common folder operations (move, copy, delete, etc.).

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Get a Folder's Items](#get-a-folders-items)
- [Get a Folder's Information](#get-a-folders-information)
- [Update a Folder's Information](#update-a-folders-information)
- [Create a Folder](#create-a-folder)
- [Copy a Folder](#copy-a-folder)
- [Delete a Folder](#delete-a-folder)
- [Create a Shared Link for a Folder](#create-a-shared-link-for-a-folder)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Get a Folder's Items
--------------------

Folder items can be retrieved by calling the
`FoldersManager.GetFolderItemsAsync(string id, int limit, int offset = 0, IEnumerable<string> fields = null, bool autoPaginate=false)`
method. Use the `fields` option to specify the desired fields.
Requesting information for only the fields you need can improve performance by reducing the size of the network response.

<!-- sample get_folders_id_items -->
```c#
BoxCollection<BoxItem> folderItems = await client.FoldersManager.GetFolderItemsAsync("11111", 100);
```

Get a Folder's Information
--------------------------

Folder information can be retrieved by calling
`FoldersManager.GetInformationAsync(string id, IEnumerable<string> fields = null)`
with the ID of the folder.

<!-- sample get_folders_id -->
```c#
BoxFolder folder = await client.FoldersManager.GetInformationAsync("11111");
```

Update a Folder's Information
-----------------------------

Updating a folder's information is done by calling the 
`FoldersManager.UpdateInformationAsync(BoxFolderRequest folderRequest, IEnumerable<string> fields = null, string etag = null)`
method.

<!-- sample put_folders_id -->
```c#
var requestParams = new BoxFolderRequest()
{
    Id = "11111",
    Name = "My Documents (2017)"
};
BoxFolder updatedFolder = await client.FoldersManager.UpdateInformationAsync(requestParams);
```

Create a Folder
---------------

Create a subfolder inside of another folder by calling
`FoldersManager.CreateAsync(BoxFolderRequest folderRequest, IEnumerable<string> fields = null)`.

<!-- sample post_folders -->
```c#
// Create a new folder in the user's root folder
var folderParams = new BoxFolderRequest()
{
    Name = "New folder",
    Parent = new BoxRequestEntity()
    {
        Id = "0"
    }
};
BoxFolder folder = client.FoldersManager.CreateAsync(folderParams);
```

Copy a Folder
-------------

To copy a folder from its current location into a different folder, call
`FoldersManager.CopyAsync(BoxFolderRequest folderRequest, IEnumerable<string> fields = null)`.

<!-- sample post_folders_id_copy -->
```c#
// Copy folder 11111 into folder 22222
var requestParams = new BoxFolderRequest()
{
    Id = "11111",
    Parent = new BoxRequestEntity()
    {
        Id = "22222"
    }
};
BoxFolder folderCopy = await client.FoldersManager.CopyAsync(requestParams);
```

Delete a Folder
---------------

A folder can be deleted by calling `FoldersManager.DeleteAsync(string id, bool recursive = false, string etag = null)`
with the ID of the folder to delete.  By default, the folder will only be deleted if it is empty; to delete the
folder and all of its contents, set the optional `recursive` parameter to `true`.

<!-- sample delete_folders_id -->
```c#
await client.FoldersManager.DeleteAsync("11111", recursive: true);
```

Create a Shared Link for a Folder
---------------------------------

You can create a shared link for a folder by calling
`FoldersManager.CreateSharedLinkAsync(string id, BoxSharedLinkRequest sharedLinkRequest, IEnumerable<string> fields = null)`
with the ID of the folder and the shared link parameters.

<!-- sample put_folders_id_shared_link_create -->
```c#
var sharedLinkParams = new BoxSharedLinkRequest()
{
    Access = BoxSharedLinkAccessType.open
};
BoxFolder folder = await client.FoldersManager.CreateSharedLinkAsync("11111", sharedLinkParams);
string sharedLinkUrl = folder.SharedLink.Url;
```
