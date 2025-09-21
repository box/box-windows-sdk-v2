# ISessionTerminationManager


- [Create jobs to terminate users session](#create-jobs-to-terminate-users-session)
- [Create jobs to terminate user group session](#create-jobs-to-terminate-user-group-session)

## Create jobs to terminate users session

Validates the roles and permissions of the user,
and creates asynchronous jobs
to terminate the user's sessions.
Returns the status for the POST request.

This operation is performed by calling function `TerminateUsersSessions`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-users-terminate-sessions/).

<!-- sample post_users_terminate_sessions -->
```
await client.SessionTermination.TerminateUsersSessionsAsync(requestBody: new TerminateUsersSessionsRequestBody(userIds: Array.AsReadOnly(new [] {Utils.GetEnvVar("USER_ID")}), userLogins: Array.AsReadOnly(new [] {NullableUtils.Unwrap(user.Login)})));
```

### Arguments

- requestBody `TerminateUsersSessionsRequestBody`
  - Request body of terminateUsersSessions method
- headers `TerminateUsersSessionsHeaders`
  - Headers of terminateUsersSessions method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `SessionTerminationMessage`.

Returns a message about the request status.


## Create jobs to terminate user group session

Validates the roles and permissions of the group,
and creates asynchronous jobs
to terminate the group's sessions.
Returns the status for the POST request.

This operation is performed by calling function `TerminateGroupsSessions`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-groups-terminate-sessions/).

<!-- sample post_groups_terminate_sessions -->
```
await client.SessionTermination.TerminateGroupsSessionsAsync(requestBody: new TerminateGroupsSessionsRequestBody(groupIds: Array.AsReadOnly(new [] {group.Id})));
```

### Arguments

- requestBody `TerminateGroupsSessionsRequestBody`
  - Request body of terminateGroupsSessions method
- headers `TerminateGroupsSessionsHeaders`
  - Headers of terminateGroupsSessions method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `SessionTerminationMessage`.

Returns a message about the request status.


