Users
=====

Users represent an individual's account on Box.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Get the Current User's Information](#get-the-current-users-information)
- [Get User's Information](#get-users-information)
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

```c#
BoxUser currentUser = await client.UsersManager.GetCurrentUserInformationAsync();
```

Get User's Information
----------------------

To get a user call `UsersManager.GetUserInformationAsync(string userId)` with the ID of the user.

```c#
BoxUser user = await client.UsersManager.GetUserInformationAsync(userId: "33333");
```

Add New User
------------

To provision a new managed user within the current enterprise, call the
`UsersManager.CreateEnterpriseUserAsync(BoxUserRequest userRequest, IEnumerable<string> fields = null)`
method with the email address the user will use to log in and the user's name.

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

```c#
await client.UsersManager.DeleteEnterpriseUserAsync("44444", notify: false, force: true);
```

Get Email Aliases
-----------------

To get a users email aliases, call `UsersManager.GetEmailAliasesAsync(string userId)`
with the ID of the user.

```c#
BoxCollection<BoxEmailAlias> aliases = await client.UsersManager
    .GetEmailAliasesAsync(userId: "33333");
```

Add Email Alias
---------------

To add an email alias for a user, call `UsersManager.AddEmailAliasAsync(string userId, string email)`
with the ID of the user and the email address to add as an alias.

```c#
BoxEmailAlias alias = await client.UsersManager
    .AddEmailAliasAsync(userId: "33333", email: "user+foo@example.com");
```

Delete Email Alias
------------------

To delete a users email alias, call `UsersManager.DeleteEmailAliasAsync(string userId, string emailAliasId)`
with the ID of the user to whom the alias belongs and the ID of the email alias.

```c#
await client.UsersManager.DeleteEmailAliasAsync(userId: "33333", emailAliasId: "12345");
```

Get Enterprise Users
--------------------

Get a list of users in the current enterprise by calling the
`UsersManager.GetEnterpriseUsersAsync(string filterTerm = null, uint offset = 0, uint limit = 100, IEnumerable<string> fields = null, string userType = null, string externalAppUserId = null, bool autoPaginate = false)`
method.

```c#
BoxCollection<BoxUser> users = await client.UsersManager.GetEnterpriseUsersAsync();
```

Transfer User Content
---------------------

To transfer one managed user's content to another user's account, call the
`MoveUserFolderAsync(string userId, string ownedByUserId, string folderId = "0", bool notify = false)`
method with the IDs of the source and destination users.

> __Note:__ Currently, only moving the user's root folder (with ID "0") is supported.

```c#
var sourceUserId = "33333";
var destinationUserId = "44444";
BoxFolder movedFolder = await client.MoveUserFolderAsync(sourceUserId, destinationUserId);
```