# Changelog
## 3.4.1 
- Added support for [Collaboration Whitelist](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxCollaborationWhitelistManager.cs) endpoint
- Added [Event Types Enum](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Config/Constants.cs#L287)
- Fixed deserialization issue with BoxRepresentationStatus(#429)

## 3.3.0
- Added support for [Terms of Service](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxTermsOfServiceManager.cs) endpoint
- Added support for [Metadata Template ID](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxMetadataManager.cs#L175) endpoint
- Added missing fields for Folder Model(#414) 

## 3.2.0
- Added support for [Representations](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxFilesManager.cs#L1216) endpoint
- Added support for [Chunked Upload New File Version](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxFilesManager.cs#L423)
- Fixed BoxEventsManager DateTime formatter(#400)

## 3.1.0
- Added Unified Metadata Support(#379)

## 3.0.0
- Major version bump to 3, targeting net45
- Upgrading the whole sln to vs2017
- Added support for [Recents](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxRecentItemsManager.cs#L1) endpoint
- New operation on [Metadata](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxMetadataManager.cs#L1) endpoint
- Progress on [Chunked Upload New File](https://github.com/box/box-windows-sdk-v2/blob/master/Box.V2/Managers/BoxFilesManager.cs#L463)
- Minor bug fixes
