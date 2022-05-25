Users
=====

Users represent an individual's account on Box.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Get the Current User's Information](#get-the-current-users-information)
- [Get User's Information](#get-users-information)
- [Get User Avatar](#get-user-avatar)
- [Add or update User Avatar](#add-or-update-user-avatar)
- [Delete User Avatar](#delete-user-avatar)
- [Add New User](#add-new-user)
- [Add New App User](#add-new-app-user)
- [Update User](#update-user)
- [Delete User](#delete-user)
- [Get Email Aliases](#get-email-aliases)
- [Add Email Alias](#add-email-alias)
- [Delete Email Alias](#delete-email-alias)
- [Get Enterprise Users](#get-enterprise-users)
- [Transfer User Content](#transfer-user-content)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Get the Current User's Information
----------------------------------

To get the current user call the `UsersManager.GetCurrentUserInformationAsync(IEnumerable<string> fields = null)`
method.

<!-- sample get_users_me -->
```c#
BoxUser currentUser = await client.UsersManager.GetCurrentUserInformationAsync();
```

Get User's Information
----------------------

To get a user call `UsersManager.GetUserInformationAsync(string userId)` with the ID of the user.

<!-- sample get_users_id -->
```c#
BoxUser user = await client.UsersManager.GetUserInformationAsync(userId: "33333");
```

Get User Avatar
---------------

To retrieve the avatar image for a user, call
`UsersManager.GetUserAvatar(string userId)` with the ID of the user.

<!-- sample get_users_id_avatar -->
```c#
Stream imageStream = await client.UsersManager.GetUserAvatar(string userId);
```

Add or Update User Avatar
---------------

To add or update user avatar call the
`UsersManager.AddOrUpdateUserAvatarAsync(string userId, FileStream stream)` method with the ID of the user and and a fileStream of the avatar contents to upload.

<!-- sample post_users_id_avatar -->
```c#
using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
{
    BoxUploadAvatarResponse response = await client.UsersManager.AddOrUpdateUserAvatarAsync(userId, fileStream);
}
```

Alternatively, you can use a generic stream (e.g. MemoryStream) and provide filename explicitly. The filename should also contain file extension (.jpg or .png).

```c#
Stream genericStream;
BoxUploadAvatarResponse response = await client.UsersManager.AddOrUpdateUserAvatarAsync(userId, genericStream, "avatar.png");
```

Delete User Avatar
---------------

To remove existing user avatar call the
`UsersManager.DeleteUserAvatarAsync(string userId)` method with the ID of the user.

<!-- sample delete_users_id_avatar -->
```c#
bool isDeleted = await client.UsersManager.DeleteUserAvatarAsync(userId);
```

Add New User
------------

To provision a new managed user within the current enterprise, call the
`UsersManager.CreateEnterpriseUserAsync(BoxUserRequest userRequest, IEnumerable<string> fields = null)`
method with the email address the user will use to log in and the user's name.

<!-- sample post_users -->
```c#
var userParams = new BoxUserRequest()
{
    Name = "Example User",
    Login = "user@example.com"
};
BoxUser newUser = await client.UsersManager.CreateEnterpriseUserAsync(userParams);
```

Add New App User
----------------

To provision a new app user within the current enterprise, call the
`UsersManager.CreateEnterpriseUserAsync(BoxUserRequest userRequest, IEnumerable<string> fields = null)`
method with the `BoxUserRequest.IsPlatformAccessOnly` property set to `true`.

<!-- sample post_users_app -->
```c#
var userParams = new BoxUserRequest()
{
    Name = "App User 12",
    ExternalAppUserId = "external-id",
    IsPlatformAccessOnly = true
};
BoxUser newUser = await client.UsersManager.CreateEnterpriseUserAsync(userParams);
```

Update User
-----------

To update a user's information, call
`UsersManager.UpdateUserInformationAsync(BoxUserRequest userRequest, IEnumerable<string> fields = null)`
with the fields to update.

<!-- sample put_users_id -->
```c#
var updates = new BoxUserRequest()
{
    Id = "44444",
    Role = "coadmin",
    CanSeeManagedUsers = true
};
BoxUser updatedUser = await client.UsersManager.UpdateUserInformationAsync(updates);
```

Delete User
-----------

To delete a user call the
`UsersManager.DeleteEnterpriseUserAsync(string userId, bool notify, bool force)`
method.  If the user still has files in their account and the `force` parameter
is not sent, an error is returned.

<!-- sample delete_users_id -->
```c#
await client.UsersManager.DeleteEnterpriseUserAsync("44444", notify: false, force: true);
```

Get Email Aliases
-----------------

To get a users email aliases, call `UsersManager.GetEmailAliasesAsync(string userId)`
with the ID of the user.

<!-- sample get_users_id_email_aliases -->
```c#
BoxCollection<BoxEmailAlias> aliases = await client.UsersManager
    .GetEmailAliasesAsync(userId: "33333");
```

Add Email Alias
---------------

To add an email alias for a user, call `UsersManager.AddEmailAliasAsync(string userId, string email)`
with the ID of the user and the email address to add as an alias.

<!-- sample post_users_id_email_aliases -->
```c#
BoxEmailAlias alias = await client.UsersManager
    .AddEmailAliasAsync(userId: "33333", email: "user+foo@example.com");
```

Delete Email Alias
------------------

To delete a users email alias, call `UsersManager.DeleteEmailAliasAsync(string userId, string emailAliasId)`
with the ID of the user to whom the alias belongs and the ID of the email alias.

<!-- sample delete_users_id_email_aliases_id -->
```c#
await client.UsersManager.DeleteEmailAliasAsync(userId: "33333", emailAliasId: "12345");
```

Get Enterprise Users
--------------------

Get a list of users in the current enterprise by calling the
`UsersManager.GetEnterpriseUsersAsync(string filterTerm = null, uint offset = 0, uint limit = 100, IEnumerable<string> fields = null, string userType = null, string externalAppUserId = null, bool autoPaginate = false)`
method.

<!-- sample get_users -->
```c#
BoxCollection<BoxUser> users = await client.UsersManager.GetEnterpriseUsersAsync();
```

Transfer User Content
---------------------

To transfer one managed user's content to another user's account, call the
`MoveUserFolderAsync(string userId, string ownedByUserId, string folderId = "0", bool notify = false)`
method with the IDs of the source and destination users.

> __Note:__ Currently, only moving the user's root folder (with ID "0") is supported.

<!-- sample put_users_id_folders_0 -->
```c#
var sourceUserId = "33333";
var destinationUserId = "44444";
BoxFolder movedFolder = await client.MoveUserFolderAsync(sourceUserId, destinationUserId);
```
