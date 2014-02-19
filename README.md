Box Windows V2 SDK
==================

Windows SDK for v2 of the Box API. The SDK is built upon the Portable Class Library and targets the following frameworks: .NET for Windows Store apps, .NET Framework 4.0, Silverlight 4 and higher, Windows Phone 7.5 and higher.


###Prerequisites
* Git  
* Visual Studio 2012 w/ Update 2 CTP  
* Windows Phone SDK 8.0 (if running Windows Phone samples)
* Windows Store SDK (if running Windows Store samples)

Quick Start
-----------

### Configuration

Set your configuration parameters and initialize the client:
```c#
var config = new BoxConfig(<Client_Id>, <Client_Secret>, "https://boxsdk");
var client = new BoxClient(config);
```
If you dont' have a client id or client secret, you can get one here: https://app.box.com/developers/services

### Authenticate
Bundled with the SDK are sample applications for both Windows 8 and Windows Phone which include sample OAuth2 Workflows. The authentication workflow is a 2-step process that first retrieves an Auth Code and then exchanges it for an Access/Refresh Token

*Windows 8*
```c#
string authCode = await OAuth2Sample.GetAuthCode(config.AuthCodeUri, new Uri(config.RedirectUri));
await client.Auth.AuthenticateAsync(authCode);
```

*Windows Phone*
```c#
// Ensure the OAuth2Sample control is placed at the root level of the application page xaml and named "oAuth2Sample"
// Subscribe to the received call back 
oAuth2Sample.AuthCodeReceived += async (s, e) =>
{
    var auth = s as OAuth2Sample;
    await client.Auth.AuthenticateAsync(auth.AuthCode);
};
// Navigate and show the login page
oauth.GetAuthCode(config.AuthCodeUri, config.RedirectUri);
```

Alternatively, a completely custom OAuth2 authentication process can be used in place of the provided workflows. In this scenario, a fully formed OAuthSession object will be passed in when instantiating the BoxClient.

```c#
OAuthSession session = // Create session from custom implementation
var client = new BoxClient(config, session);
```

### Get Folder Items
```c#
// Get root folder with default properties
BoxFolder f = await client.FoldersManager.GetItemsAsync("0", 50, 0);

// Get root folder with specific properties
BoxFolder f = await client.FoldersManager.GetItemsAsync("0", 50, 0, new List<string>() { 
  BoxFolder.FieldModifiedAt,
        BoxItem.FieldName, 
	BoxFolder.FieldItemCollection, 
        BoxFolder.FieldPathCollection
});
```

### Get File Information
```c#
BoxFile f = await client.FilesManager.GetInformationAsync(fileId);
```

### Update a Files Information
```c#
// Create request object with new property values
BoxFileRequest request = new BoxFileRequest()
{
    Id = fileId,
    Name = "NewName",
    Description = "New Description"
};
BoxFile f = await client.FilesManager.UpdateInformationAsync(request );
```


### Upload a New File
```c#
// Create request object with name and parent folder the file should be uploaded to
BoxFileRequest req = new BoxFileRequest()
{
	Name = "NewFile",
	Parent = new BoxRequestEntity() { Id = "0" }
};
BoxFile f = await client.FilesManager.UploadAsync(request, stream);
```

### Download a File
```c#
Stream stream = await client.FilesManager.DownloadStreamAsync(fileId);
```

File/Folder Picker
------------------
The Box Windows SDK includes a user control that allows developers an easy way to drop in a file and or folder picker in just one line of code

*File Picker*
```xml
<controls:BoxItemPickerLauncher Client="{Binding Client}" />
```

*Folder Picker*
```xml
<controls:BoxItemPickerLauncher Client="{Binding Client}" ItemPickerType="Folder" />
```

You can attach an event handler to the ItemSelected event to handle when an Item is selected. Please see sample apps for additional detail on how the controls look and work. 

Tests
-----
Unit tests are included that use Moq to simulate network requests and responses. These tests can be found in the Box.V2.Test project

Documentation
-------------
Documentation of all classes and methods are provided through the standard ```<summary></summary>``` xml tags. The easiest way to view these is through Visual Studio's built in "Object Browser" (VIEW -> Object Browser, or CTRL+W, J). 

Known Issues
------------
Windows 8 Sample OAuth2 uses desktop login screen instead of mobile. Pending fix from platform team.


## Copyright and License

Copyright 2014 Box, Inc. All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
