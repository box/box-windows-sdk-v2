# IAvatarsManager


- [Get user avatar](#get-user-avatar)
- [Add or update user avatar](#add-or-update-user-avatar)
- [Delete user avatar](#delete-user-avatar)

## Get user avatar

Retrieves an image of a the user's avatar.

This operation is performed by calling function `GetUserAvatar`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-users-id-avatar/).

<!-- sample get_users_id_avatar -->
```
await client.Avatars.GetUserAvatarAsync(userId: user.Id);
```

### Arguments

- userId `string`
  - The ID of the user. Example: "12345"
- headers `GetUserAvatarHeaders`
  - Headers of getUserAvatar method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `System.IO.Stream`.

When an avatar can be found for the user the
image data will be returned in the body of the
response.


## Add or update user avatar

Adds or updates a user avatar.

This operation is performed by calling function `CreateUserAvatar`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-users-id-avatar/).

<!-- sample post_users_id_avatar -->
```
await client.Avatars.CreateUserAvatarAsync(userId: user.Id, requestBody: new CreateUserAvatarRequestBody(pic: Utils.DecodeBase64ByteStream(data: "iVBORw0KGgoAAAANSUhEUgAAAQAAAAEAAQMAAABmvDolAAAAA1BMVEW10NBjBBbqAAAAH0lEQVRoge3BAQ0AAADCoPdPbQ43oAAAAAAAAAAAvg0hAAABmmDh1QAAAABJRU5ErkJggg==")) { PicContentType = "image/png", PicFileName = "avatar.png" });
```

### Arguments

- userId `string`
  - The ID of the user. Example: "12345"
- requestBody `CreateUserAvatarRequestBody`
  - Request body of createUserAvatar method
- headers `CreateUserAvatarHeaders`
  - Headers of createUserAvatar method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `UserAvatar`.

`ok`: Returns the `pic_urls` object with URLs to existing
user avatars that were updated.`created`: Returns the `pic_urls` object with URLS to user avatars
uploaded to Box with the request.


## Delete user avatar

Removes an existing user avatar.
You cannot reverse this operation.

This operation is performed by calling function `DeleteUserAvatar`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-users-id-avatar/).

<!-- sample delete_users_id_avatar -->
```
await client.Avatars.DeleteUserAvatarAsync(userId: user.Id);
```

### Arguments

- userId `string`
  - The ID of the user. Example: "12345"
- headers `DeleteUserAvatarHeaders`
  - Headers of deleteUserAvatar method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

`no_content`: Removes the avatar and returns an empty response.


