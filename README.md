[![Project Status](http://opensource.box.com/badges/active.svg)](http://opensource.box.com/badges)

Box Windows V2 SDK
==================

Windows .NET SDK for V2 of the Box API that is usable from the following frameworks: 
* .NET Framework 4.0.3 and higher
* .NET for Windows Store apps
* Silverlight 4 and higher
* Windows Phone 7.5 and higher

###Prerequisites
* Visual Studio 2012 w/ Update 2 CTP (or higher)
* Windows Phone SDK 8.0 (if running Windows Phone samples)
* Windows Store SDK (if running Windows Store samples)

Quick Start
-----------

### Installation
Install the core SDK using Nuget
```bash
PM> Install-Package Box.V2
```
For use with Box Platform Developer or Box Platform Enterprise, also install JWT support using Nuget
```bash
PM> Install-Package Box.V2.JWTAuth
```

If you haven't already created an app in Box go to https://developers.box.com/ and click 'Sign Up'

### Using with Box Platform Developer or Box Platform Enterprise

#### Configure
```c#
var boxConfig = new BoxConfig(<Client_Id>, <Client_Secret>, <Enterprise_Id>, <Private_Key>, <JWT_Private_Key_Password>, <JWT_Public_Key_id>);
var boxJWT = new BoxJWTAuth(boxConfig);
```

#### Authenticate
```c#
var adminToken = boxJWT.AdminToken(); //valid for 60 minutes so should be cached and re-used
var adminClient = boxJWT.AdminClient(adminToken);

//for example, get the admin's root folder items
var items = await adminClient.FoldersManager.GetFolderItemsAsync("0", 500);
```
#### Create an App User
```c#
//NOTE: you must set IsPlatformAccessOnly=true for an App User
var userRequest = new BoxUserRequest() { Name = "test appuser", IsPlatformAccessOnly = true };
var appUser = await adminClient.UsersManager.CreateEnterpriseUserAsync(userRequest);

//get a user client
var userToken = boxJWT.UserToken(appUser.Id); //valid for 60 minutes so should be cached and re-used
var userClient = boxJWT.UserClient(userToken, appUser.Id);

//for example, look up the app user's details
var userDetails = await userClient.UsersManager.GetCurrentUserInformationAsync();
```

### Using with OAuth2

#### Configure
Set your configuration parameters and initialize the client:
```c#
var config = new BoxConfig(<Client_Id>, <Client_Secret>, <Redirect_Uri>);
var client = new BoxClient(config);
```

#### Authenticate
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

*Other (ASP.NET)*

Alternatively, a completely custom OAuth2 authentication process can be used in place of the provided workflows, for example, in a custom web application. In this scenario, a fully formed OAuthSession object should be passed in when instantiating the BoxClient. 

```c#
OAuthSession session = // Create session from custom implementation
var client = new BoxClient(config, session);
```
### Some Examples
#### Get Folder Items
```c#
// Get root folder with default properties
var items = await client.FoldersManager.GetFolderItemsAsync("0", 500);
```

#### Get File Information
```c#
BoxFile f = await client.FilesManager.GetInformationAsync(fileId);
```

#### Update a Files Information
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


#### Upload a New File
```c#
// Create request object with name and parent folder the file should be uploaded to
BoxFileRequest req = new BoxFileRequest()
{
	Name = "NewFile",
	Parent = new BoxRequestEntity() { Id = "0" }
};
BoxFile f = await client.FilesManager.UploadAsync(request, stream);
```

#### Download a File
```c#
Stream stream = await client.FilesManager.DownloadStreamAsync(fileId);
```

#### Get Temporary Download Uri for a file
This method will retrieve a temporary (15 minute) Uri for a file that can be used, for example, to send as a redirect to a browser, causing the browser to download the file directly from Box.
```c#
var downloadUri = await client.FilesManager.GetDownloadUriAsync(fileId);
```

#### Search using Metadata
```c#
var filter = new 
{ 
	someKey = "blah", 
	expiresOn = new {gt = new DateTime(2015,1,1), lt = new DateTime(2015, 9, 1)}
};

var mdFilter = new BoxMetadataFilterRequest() 
{ 
	TemplateKey = "yourTemplate", 
	Scope = "enterprise", 
	Filters = filter 
};

//currently only one BoxMetadataFilterRequest element is supported; in the future multiple will be supported (hence the List)
var results = await client.SearchManager.SearchAsync(mdFilters: new List<BoxMetadataFilterRequest>() { mdFilter });
```

#### Make API calls with As-User
If you have an admin token with appropriate permissions, you can make API calls in the context of a managed user. In order to do this you must request Box.com to activate As-User functionality for your API key (see developer site for instructions). 
```c#
var config = new BoxConfig(<Client_Id>, <Client_Secret>, <Redirect_Uri);
var auth = new OAuthSession(<Your_Access_Token>, <Your_Refresh_Token>, 3600, "bearer");

var userId = "12345678"
var userClient = new BoxClient(config, auth, asUser: userId);

//returns root folder items for the user with ID '12345678'
var items  = await userClient.FoldersManager.GetFolderItemsAsync("0", 500);
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

Other Resources
-------------
- Core SDK Nuget Package: https://www.nuget.org/packages/Box.V2/
- JWT Support Nuget Package: https://www.nuget.org/packages/Box.V2.JWTAuth/
- Box Windows SDK Video Tutorial: https://developers.box.com/box-windows-sdk-tutorial/

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
