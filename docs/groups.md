Groups
======

Groups contain a set of users, and can be used in place of individual users in some
operations, such as collaborations.

Create Group
------------

To create a new group, call
`GroupsManager.CreateAsync(BoxGroupRequest groupRequest, IEnumerable<string> fields = null)`
with the parameters for the group being created.

```c#
var groupParams = new BoxGroupRequest()
{
    Name = "Engineers"
};
BoxGroup group = await client.GroupsManager.CreateAsync(groupParams);
```

Get Group
---------

To retrieve the information for a group, call
`GroupsManager.GetGroupAsync(string id, IEnumerable<string> fields = null)`
with the ID of the group.

```c#
BoxGroup group = await client.GroupsManager.GetGroupAsync("11111");
```

Get All Groups
--------------

To get a list of all groups in the calling user's enterprise, call
`GroupsManager.GetAllGroupsAsync(int? limit = null, int? offset = null, IEnumerable<string> fields = null, bool autoPaginate = false)`.
Note that this requires permission to view an enterprise's groups, which is reserved for enterprise administrators.

```c#
BoxCollection<BoxGroup> groups = await client.GroupsManager.GetAllGroupsAsync();
```

Update Group
------------

To change the properties of a group object, call the
`GroupsManager.UpdateAsync(string id, BoxGroupRequest groupRequest, IEnumerable<string> fields = null)`
method with the set of properties to update.

```c#
var updates = new BoxGroupRequest()
{
    Name = "New group name"
};
BoxGroup updatedGroup = await client.GroupsManager.UpdateAsync("11111", updates);
```

Delete Group
------------

To delete a group, call `(string id)` with the ID of the group to delete.

```c#
await client.GroupsManager.DeleteAsync("11111");
```

Get Group Collaborations
------------------------

To get a list of collaborations for a group, which show which items the group has
access to, call
`GroupsManager.GetCollaborationsForGroupAsync(string groupId, int? limit = null, int? offset = null, IEnumerable<string> fields = null, bool autoPaginate = false)`
with the ID of the group.

```c#
BoxCollection<BoxCollaboration> groupCollaborations = await client.GroupsManager
    .GetCollaborationsForGroupAsync(groupId: "11111");
```

Add a User to a Group
---------------------

To add a user to a group, call
`GroupsManager.AddMemberToGroupAsync(BoxGroupMembershipRequest membershipRequest, IEnumerable<string> fields = null)`.

```c#
var requestParams = new BoxGroupMembershipRequest()
{
    User = new BoxRequestEntity()
    {
        Id = "22222"
    },
    Group = new BoxGroupRequest()
    {
        Id = "11111"
    }
};
BoxGroupMembership membership = await client.GroupsManager.AddMemberToGroupAsync(requestParams);
```

To retrieve information about a specific membership record, which shows that a
given user is in the group, call
`GroupsManager.GetGroupMembershipAsync(string id, IEnumerable<string> fields = null)`
with the ID of the membership object.

```c#
BoxGroupMembership membership = await client.GroupsManager.GetGroupMembershipAsync("33333");
```

Update Membership
-----------------

To update a membership record, call
`GroupsManager.UpdateGroupMembershipAsync(string membershipId, BoxGroupMembershipRequest memRequest, IEnumerable<string> fields = null)`
with the ID of the membership object and the fields to update.

```c#
var updates = new BoxGroupMembershipRequest()
{
    Role = "admin"
};
BoxGroupMembership updatedMembership = await client.GroupsManager
    .UpdateGroupMembershipAsync("33333", updates);
```

Remove Membership
-----------------

To remove a specific membership record, which removes a user from the group, call the
`GroupsManager.DeleteGroupMembershipAsync(string id)` method with the ID of the membership record to remove.

```c#
await client.GroupsManager.DeleteGroupMembershipAsync("33333");
```

Get Group Memberships
---------------------

To get a list of all memberships to a group, call the
`GroupsManager.GetAllGroupMembershipsForGroupAsync(string groupId, int? limit = null, int? offset = null, IEnumerable<string> fields = null, bool autoPaginate = false)`
method with the ID of the group to get the list of memberships for.

```c#
BoxCollection<BoxGroupMembership> memberships = await client.GroupsManager
    .GetAllGroupMembershipsForGroupAsync("11111");
```

Get Group Memberships for a User
--------------------------------

To get a list of groups to which a user belongs, call the
`GroupsManager.GetAllGroupMembershipsForUserAsync(string userId, int? limit = null, int? offset = null, IEnumerable<string> fields = null, bool autoPaginate = false)`
method.  Note that this method requires the calling user to have permission to
view groups, which is restricted to enterprise administrators.

```c#
BoxCollection<BoxGroupMembership> memberships = await client.GroupsManager
    .GetAllGroupMembershipsForUserAsync(userId: "11111");
```