Trash
=====

Under normal circumstances, when an item in Box is deleted, it is not actually
erased immediately.  Instead, it is moved to the Trash.  The Trash allows you to
recover files and folders that have been deleted. By default, items in the Trash
will be purged after 30 days.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Get Trashed Items](#get-trashed-items)
- [Get a Trashed File](#get-a-trashed-file)
- [Get a Trashed Folder](#get-a-trashed-folder)
- [Purge a File from the Trash](#purge-a-file-from-the-trash)
- [Purge a Folder from the Trash](#purge-a-folder-from-the-trash)
- [Restore a File From Trash](#restore-a-file-from-trash)
- [Restore a Folder from Trash](#restore-a-folder-from-trash)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Get Trashed Items
-----------------

To retrieve files and folders that have been moved to the Trash, call
`FoldersManager.GetTrashItemsAsync(int limit, int offset = 0, IEnumerable<string> fields = null, bool autoPaginate=false)`.

<!-- sample get_folders_trash_items -->
```c#
BoxCollection<BoxItem> trashedItems = await client.FoldersManager.GetTrashItemsAsync(limit: 100);
```

Get a Trashed File
------------------

Information about a file in the trash can be retrieved by calling the
`FilesManager.GetTrashedAsync(string id, IEnumerable<string> fields = null)`
method with the ID of the file in the trash.

<!-- sample get_files_id_trash -->
```c#
BoxFile trashedFile = await client.FilesManager.GetTrashedAsync("11111");
```

Get a Trashed Folder
--------------------

Information about a folder in the trash can be retrieved by calling the
`FoldersManager.GetTrashedFolderAsync(string id, IEnumerable<string> fields = null)`
method with the ID of the folder in the trash.

<!-- sample get_folders_id_trash -->
```c#
BoxFolder trashedFolder = await client.FoldersManager.GetTrashedFolderAsync("22222");
```

Purge a File from the Trash
----------------------------

Calling the `FilesManager.PurgeTrashedAsync(string id)` method will remove the file permanently from the user's trash.

<!-- sample delete_files_id_trash -->
```c#
await client.FilesManager.PurgeTrashedAsync("11111");
```

Purge a Folder from the Trash
-----------------------------

Calling the `FoldersManager.PurgeTrashedFolderAsync(string id)` method will remove the folder permanently from
the user's trash.

<!-- sample delete_folders_id_trash -->
```c#
await client.FoldersManager.PurgeTrashedFolderAsync("22222");
```

Restore a File From Trash
-------------------------

Calling `FilesManager.RestoreTrashedAsync(BoxFileRequest fileRequest, IEnumerable<string> fields = null)`
will restore an item from the user's trash.  Default behavior is to restore the item
to the folder it was in before it was moved to the trash. Options are available
to handle possible failure cases: if an item with the same name already exists in
folder's old location, the restored folder can be given an alternate name with
the `Name` option.  If the folder's old location no longer exists, it can be
placed inside a new parent folder with the `Parent.Id` option.

<!-- sample post_files_id -->
```c#
var requestParams = new BoxFileRequest()
{
    Name = "Name in case of conflict",
    Parent = new BoxRequestEntity()
    {
        // File will be placed in this folder if original location no longer exists
        Id = "12345" 
    }
};
BoxFile restoredFile = await client.FilesManager.RestoreTrashedAsync(requestParams);
```

Restore a Folder from Trash
---------------------------

A folder can be restored from the trash with the
`FoldersManager.RestoreTrashedFolderAsync(BoxFolderRequest folderRequest, IEnumerable<string> fields = null)`
method.  Default behavior is to restore the item to the folder it was in before
it was moved to the trash.  Options are available to handle possible failure
cases: if an item with the same name already exists in folder's old location, the
restored folder can be given an alternate name with the `Name` option.  If the
folder's old location no longer exists, it can be placed inside a new parent
folder with the `Parent.Id` option.

<!-- sample post_folders_id -->
```c#
var requestParams = new BoxFolderRequest()
{
    Name = "Name in case of conflict",
    Parent = new BoxRequestEntity()
    {
        // Folder will be placed in this parent folder if original location no longer exists
        Id = "12345" 
    }
};
BoxFolder restoredFolder = await client.FoldersManager.RestoreTrashedFolderAsync(requestParams);
```