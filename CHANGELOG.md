# Changelog

## 3.8.0

- Fixed an issue where users could not create `BoxClient` on Xamarin
- Added `File` property to `BoxLock` objects in events
- Added `MetadataManager.DeleteMetadataTemplate(string scope, string template)` for deleting a Metadata template 
- Made API URLs modifiable in `BoxConfig`
- Improved API response error objects/messages

## 3.7.0

- Added support for assigning a Retention Policy to a metadata template
- Added `CONTENT_ACCESS` event type to enum

## 3.6.0

- Fixed an issue where a "Security protocol not supported" exception could be thrown on MacOS
- Added `client.FilesManager.GetRepresentationContentAsync()` for fetching a stream over representation contents
- Fixed parsing of some `Source` objects on `BoxEvent` objects

## 3.5.2

- Switched to exponential backoff when the SDK receives a rate limit or server error response. 
- Force support for TLSv1.1 or higher when available to improve the security of connections to the Box API.
- Perform modified retry on JWT auth for when the local clock and the Box Server clock are not aligned as well as if the JWT ID has already been consumed.
- Made `name` parameter optional on `RestoreTrashedAsync()`.

## 3.4.2

- Deprecated `uploadFileVersionUsingSessionAsync()`(which returned just a Box File Version) in favor of `uploadNewVersionUsingSessionAsync()`(which returns the entire Box File object containing the Box File Version).
- Added support for OAuth2 access token creation type to the AdminEventTypesEnum 
- Added `ExpiresAt` param to `BoxCollaborationRequest`. 

## 3.4.1 

- Added support for [Collaboration Whitelist](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxCollaborationWhitelistManager.cs) endpoint
- Added [Event Types Enum](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Config/Constants.cs#L287)
- Fixed deserialization issue with BoxRepresentationStatus (#429)

## 3.3.0

- Added support for [Terms of Service](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxTermsOfServiceManager.cs) endpoint
- Added support for [Metadata Template ID](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxMetadataManager.cs#L175) endpoint
- Added missing fields for Folder Model (#414) 

## 3.2.0

- Added support for [Representations](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxFilesManager.cs#L1216) endpoint
- Added support for [Chunked Upload New File Version](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxFilesManager.cs#L423)
- Fixed BoxEventsManager DateTime formatter (#400)

## 3.1.0

- Added Unified Metadata Support (#379)

## 3.0.0

- Major version bump to 3, targeting net45
- Upgrading the whole sln to vs2017
- Added support for [Recents](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxRecentItemsManager.cs#L1) endpoint
- New operation on [Metadata](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxMetadataManager.cs#L1) endpoint
- Progress on [Chunked Upload New File](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxFilesManager.cs#L463)
- Minor bug fixes
