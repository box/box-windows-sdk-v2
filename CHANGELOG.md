# Changelog

## 3.24.0 [2020-07-21]
- Add path parameter sanitization
- Add support for the classification field for Files and Folders
- Fix bug with notification email field deserializing for `BoxUser` 
- Add `fields` parameter for metadata query
- Add ability to set a request timeout for `FoldersManager.UpdateInformationAsync()` and `UsersManager.MoverUserFolderAsync`

## 3.23.0 [2020-05-12]
- Add ability to get and set a notification email address for a user
- Fix deadlock issue for JWT authentication in UI elements
- Add support for the uploader display name field for Files and File Versions

## 3.22.0 [2020-02-25]
- Fixed Authentication Request Retries
- Added the ability to query Box items based on their metadata. The method to do so is `MetadataManager.ExecuteMetadataQueryAsync()`.
- Added `TrashedAt` field to `BoxItem` objects (file, folder, weblink).
- Added marker based pagination for get users methods
- Updated retry logic to retry on 503 status codes returned by the API
- Provide better details for debugging, if the HttpClient used to make API requests times out

## 3.21.0 [2019-12-05]
- Added `fields` parameter to `UsersManager.GetUserInformationAsync()`
- Added `ExternalAppUserId` property to `BoxUser` model
- Added the ability to set the `TrackingCodes` property when updating or creating a user (thanks @Cpcrook!)

## 3.20.0 [2019-09-19]
- Added missing fields for File Version object.

## 3.19.0 [2019-08-29]
- Added `FILE_VERSION_RESTORE` constant to Admin Event.
- Added action_by field to Enterprise Event.
- Audited missing fields on BoxFile and BoxFolder objects.
- Better error handling and messaging for errors pertaining to OAuth2 error responses.

## 3.18.0 [2019-06-20]

- Added `sort` and `direction` parameters to `FoldersManager.GetFolderItemsAsync()` to enable
  sorting the folder items returned
- Added a new `SearchManager.QueryAsync()` method with correct types for file size filter parameters
- Deprecated the `SearchManager.SearchAsync()` method, which is superseded by `SearchManager.QueryAsync()`
- Added support for setting the `IsExternalCollabRestricted` parameter when creating and updating Users
- Added a `WebProxy` property to `BoxConfig` instances, which can be used to manually set the network proxy
  used by the SDK

## 3.17.0 [2019-05-09]

- Fixed the encoding of dates in the query parameters for Events and Search endpoints
- Deprecated `FilesManager.DownloadStreamAsync()` and introduced a replacement method with correct parameter types for byte offsets: `FilesManager.DownloadAsync()`

## 3.16.0 [2019-04-29]

- Added `sort` and `direction` parameters to `client.SearchManager.SearchAsync()` to  control sort order
- Added `extension` parameter to `client.FilesManager.GetThumbnailAsync()` to control which thumbnail format is returned (thanks @guilmori!)
- Fixed a bug where query string parameters were not correctly encoded
- Added `SetFileMetadataAsync()` and `SetFolderMetadataAsync()` methods to `client.MetadataManager` to set metadata
  keys and values, overwriting existing values for the provided keys.
- Automatically retry most API calls when the API responds with a transient error status code

## 3.15.0 [2019-03-28]

- Added support for passing custom IBoxService to BoxJWTAuth constructor.

## 3.14.1 [2019-03-07]

- Removed unnecessary package.config from sample files.

## 3.14.0 [2019-02-28]

- Added trace ID to API response exception message.
- Fix deserialization of translated task assignment status.

## 3.13.1 [2019-02-21]

- Fixed an issue where some objects related to Events did not have their `.Id` property correctly deserialized from JSON

## 3.13.0 [2019-02-14]

- Added the `.InviteEmail` property to `BoxCollaboration` objects, which displays the email address for the invited
  user in a pending collaboration
- Added `.Timezone`, `.IsExternalCollabRestricted`, `.Tags`, and `.Hostname` properties to `BoxUser` objects

## 3.12.0 [2019-02-07]

- Added `client.FilesManager.GetCollaborationsCollectionAsync()` and deprecated
  `client.FilesManager.GetCollaborationsAsync()` to enable paging through the entire
  collection of collaborations on a file
- Added `client.WebLinksManager.CopyAsync()`, `client.WebLinksManager.CreateSharedLinkAsync()`,
  and `client.WebLinksManager.DeleteSharedLinkAsync()`
- Added `client.UsersManager.GetUserAvatarAsync()` for retrieving a user's avatar image

## 3.11.0 [2019-01-17]

- Added support for reading and writing more Group fields
- Fixed an issue where the `UnsharedAt` field of a shared link could not be set to `null`
- Fixed renaming a file on new version upload
- Added the ability to set the content modification timestamp on file version upload
- Fixed issues around reading the source of an event when the source item is a web link

## 3.10.0 [2018-12-14]

- Added support for Metadata Cascade Policies

## 3.9.3 [2018-09-04]

- Strong named the assembly.

## 3.9.2 [2018-06-14]

- Added support for [setting flag](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Models/Request/BoxFolderRequest.cs#L39) allowing non owners of a folder to invite collaborators.

## 3.9.1 [2018-06-07]

- Fixed bug where Xamarin applications would run out of connections

## 3.9.0 [2018-05-10]

- Added support for Storage Policies

## 3.8.0 [2018-04-30]

- Fixed an issue where users could not create `BoxClient` on Xamarin
- Added `File` property to `BoxLock` objects in events
- Added `MetadataManager.DeleteMetadataTemplate(string scope, string template)` for deleting a Metadata template 
- Made API URLs modifiable in `BoxConfig`
- Improved API response error objects/messages

## 3.7.0 [2018-04-10]

- Added support for assigning a Retention Policy to a metadata template
- Added `CONTENT_ACCESS` event type to enum

## 3.6.0 [2018-03-27]

- Fixed an issue where a "Security protocol not supported" exception could be thrown on MacOS
- Added `client.FilesManager.GetRepresentationContentAsync()` for fetching a stream over representation contents
- Fixed parsing of some `Source` objects on `BoxEvent` objects

## 3.5.2 [2018-03-21]

- Switched to exponential backoff when the SDK receives a rate limit or server error response. 
- Force support for TLSv1.1 or higher when available to improve the security of connections to the Box API.
- Perform modified retry on JWT auth for when the local clock and the Box Server clock are not aligned as well as if the JWT ID has already been consumed.
- Made `name` parameter optional on `RestoreTrashedAsync()`.

## 3.4.2 [2018-01-31]

- Deprecated `uploadFileVersionUsingSessionAsync()`(which returned just a Box File Version) in favor of `uploadNewVersionUsingSessionAsync()`(which returns the entire Box File object containing the Box File Version).
- Added support for OAuth2 access token creation type to the AdminEventTypesEnum 
- Added `ExpiresAt` param to `BoxCollaborationRequest`. 

## 3.4.1 [2018-01-09]

- Added support for [Collaboration Whitelist](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxCollaborationWhitelistManager.cs) endpoint
- Added [Event Types Enum](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Config/Constants.cs#L287)
- Fixed deserialization issue with BoxRepresentationStatus (#429)

## 3.3.0 [2017-11-22]

- Added support for [Terms of Service](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxTermsOfServiceManager.cs) endpoint
- Added support for [Metadata Template ID](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxMetadataManager.cs#L175) endpoint
- Added missing fields for Folder Model (#414) 

## 3.2.0 [2017-10-04]

- Added support for [Representations](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxFilesManager.cs#L1216) endpoint
- Added support for [Chunked Upload New File Version](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxFilesManager.cs#L423)
- Fixed BoxEventsManager DateTime formatter (#400)

## 3.1.0 [2017-08-18]

- Added Unified Metadata Support (#379)

## 3.0.0 [2017-07-28]

- Major version bump to 3, targeting net45
- Upgrading the whole sln to vs2017
- Added support for [Recents](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxRecentItemsManager.cs#L1) endpoint
- New operation on [Metadata](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxMetadataManager.cs#L1) endpoint
- Progress on [Chunked Upload New File](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxFilesManager.cs#L463)
- Minor bug fixes
