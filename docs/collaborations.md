Collaborations
==============

Collaborations are used to share folders and files between users or groups. They also define what permissions a user
has for a folder or file.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Add a Collaboration](#add-a-collaboration)
- [Edit a Collaboration](#edit-a-collaboration)
- [Remove a Collaboration](#remove-a-collaboration)
- [Get a Collaboration's Information](#get-a-collaborations-information)
- [Get the Collaborations on a Folder](#get-the-collaborations-on-a-folder)
- [Get the Collaborations on a File](#get-the-collaborations-on-a-file)
- [Get Pending Collaborations](#get-pending-collaborations)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Add a Collaboration
-------------------

A collaboration can be added for an existing user by calling
`CollaborationsManager.AddCollaborationAsync(BoxCollaborationRequest collaborationRequest, IEnumerable<string> fields = null, bool? notify = null)`.
The `Role` field of the `collaborationRequest` parameter determines what permissions the collaborator will have on the
folder or file. 

```c#
// collaborate folder 11111 with user 22222
BoxCollaborationRequest requestParams = new BoxCollaborationRequest()
{
    Item = new BoxRequestEntity()
    {
        Type = BoxType.Folder,
        Id = "11111"
    },
    Role = "editor",
    AccessibleBy = new BoxCollaborationUserRequest()
    {
        Id = "22222"
    }
};
BoxCollaboration collab = await client.CollaborationsManager.AddCollaborationAsync(requestParams);
```

Administrators can set the `notify` parameter to `false` to prevent the user who is being collaborated from receiving an
email notification about the collaboration.

Edit a Collaboration
--------------------

A collaboration can be edited by calling
`CollaborationsManager.EditCollaborationAsync(BoxCollaborationRequest collaborationRequest, IEnumerable<string> fields = null)`
with the fields to be updated.  For example, to change the role of a collaboration:

```c#
BoxCollaborationRequest requestParams = new BoxCollaborationRequest()
{
    Id = "12345",
    Role = "viewer"
};
BoxCollaboration collab = await client.CollaborationsManager.EditCollaborationAsync(requestParams);
```

Remove a Collaboration
----------------------

A collaboration can be removed by calling `CollaborationsManager.RemoveCollaborationAsync(string id)`.
This will generally remove the user or group's access to the collaborated item.

```c#
await client.CollaborationsManager.RemoveCollaborationAsync(id: "12345");
```

Get a Collaboration's Information
---------------------------------

To get information about a specific collaboration record, call
`CollaborationsManager.GetCollaborationAsync(string id, IEnumerable<string> fields = null)` with the
ID of the collaboration.

```c#
BoxCollaboration collab = await client.CollaborationsManager.GetCollaborationAsync(id: "22222");
```

Get the Collaborations on a Folder
----------------------------------

You can get all of the collaborations on a folder by calling
`FoldersManager.GetCollaborationsAsync(string id, IEnumerable<string> fields = null)` with the ID of the folder.

```c#
string folderId = "11111";
BoxCollection<BoxCollaboration> collaborations = await client.FoldersManager
    .GetCollaborationsAsync(folderId);
```

Get the Collaborations on a File
--------------------------------

You can get the collection of collaborations on a file by calling
`FilesManager.GetCollaborationsAsync(string id, IEnumerable<string> fields = null)`
with the ID of the file.

```c#
string fileId = "98765";
BoxCollection<BoxCollaboration> collaborations = await client.FilesManager
    .GetCollaborationsAsync(fileId);
```

Get Pending Collaborations
--------------------------

A collection of all the user's pending collaborations can be retrieved with
`CollaborationsManager.GetPendingCollaborationAsync(IEnumerable<string> fields = null)`.

```c#
BoxCollection<BoxCollaboration> pendingCollabs = await CollaborationsManager
    .GetPendingCollaborationAsync();
```