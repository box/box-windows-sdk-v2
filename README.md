<p align="center">
  <img src="https://github.com/box/sdks/blob/master/images/box-dev-logo.png" alt= “box-dev-logo” width="30%" height="50%">
</p>

# Box Dotnet SDK Gen

[![Project Status](http://opensource.box.com/badges/active.svg)](http://opensource.box.com/badges)
![build](https://github.com/box/box-dotnet-sdk-gen/actions/workflows/build.yml/badge.svg)
[![nuget version](https://img.shields.io/nuget/v/box.sdk.gen.svg)](https://badge.fury.io/nu/box.sdk.gen)
[![image](https://img.shields.io/nuget/dt/box.sdk.gen.svg)](https://badge.fury.io/nu/box.sdk.gen)
![Platform](https://img.shields.io/badge/.NET-6%2B-brightgreen)
[![Coverage](https://coveralls.io/repos/github/box/box-dotnet-sdk-gen/badge.svg?branch=main)](https://coveralls.io/github/box/box-dotnet-sdk-gen?branch=main)

We are excited to introduce the stable release of the latest generation of Box Dotnet SDK Gen, designed to elevate the developer experience and streamline your integration with the Box Content Cloud.

With this SDK, you’ll have access to:

1. Full API Support: The new generation of Box SDKs empowers developers with complete coverage of the Box API ecosystem. You can now access all the latest features and functionalities offered by Box, allowing you to build even more sophisticated and feature-rich applications.
2. Rapid API Updates: Say goodbye to waiting for new Box APIs to be incorporated into the SDK. With our new auto-generation development approach, we can now add new Box APIs to the SDK at a much faster pace (in a matter of days). This means you can leverage the most up-to-date features in your applications without delay.
3. Embedded Documentation: We understand that easy access to information is crucial for developers. With our new approach, we have included comprehensive documentation for all objects and parameters directly in the source code of the SDK. This means you no longer need to look up this information on the developer portal, saving you time and streamlining your development process.
4. Enhanced Convenience Methods: Our commitment to enhancing your development experience continues with the introduction of convenience methods. These methods cover various aspects such as chunk uploads, classification, and much more.
5. Seamless Start: The new SDKs integrate essential functionalities like authentication, automatic retries with exponential backoff, exception handling, request cancellation, and type checking, enabling you to focus solely on your application's business logic.

Embrace the new generation of Box SDKs and unlock the full potential of the Box Content Cloud.

# Table of contents

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Box Dotnet SDK Gen](#box-dotnet-sdk-gen)
- [Table of contents](#table-of-contents)
- [Installing](#installing)
  - [BouncyCastle runtime integrity check](#bouncycastle-runtime-integrity-check)
- [Getting Started](#getting-started)
- [Documentation](#documentation)
- [Upgrades](#upgrades)
- [Integration Tests](#integration-tests)
  - [Running integration tests locally](#running-integration-tests-locally)
    - [Create Platform Application](#create-platform-application)
    - [Export configuration](#export-configuration)
    - [Running tests](#running-tests)
- [Questions, Bugs, and Feature Requests?](#questions-bugs-and-feature-requests)
- [Copyright and License](#copyright-and-license)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

# Installing

You can install SDK using Nuget

```pwsh
Install-Package Box.Sdk.Gen
```

Alternatively, you can find this package and it's latest version [on nuget](https://www.nuget.org/packages/Box.Sdk.Gen) and manually add it to the `.csproj` file as a reference:

```xml
<ItemGroup>
  <PackageReference Include="Box.Sdk.Gen" Version="X.Y.Z" />
</ItemGroup>
```

## BouncyCastle runtime integrity check

The version of BouncyCastle included in the SDK performs a checksum validation at runtime. As a result, any modifications to the .dll file, such as those introduced by optimizations like [ReadyToRun (R2R)](https://learn.microsoft.com/en-us/dotnet/core/deploying/ready-to-run) compilation, can alter the checksum, causing the validation to fail. This can lead to issues with SDK functionalities that rely on BouncyCastle, such as JWT authentication unusable.

You can exclude BouncyCastle from ReadyToRun compilation by adding the following to your `.csproj` file:

```xml
<ItemGroup>
  <PublishReadyToRunExclude Include="bc-fips-1.0.2.dll" />
  <PublishReadyToRunExclude Include="bcpkix-fips-1.0.2.dll" />
</ItemGroup>
```

# Getting Started

To get started with the SDK, get a Developer Token from the Configuration page of your app in the [Box Developer
Console](https://app.box.com/developers/console). You can use this token to make test calls for your own Box account.

The SDK provides a `BoxDeveloperTokenAuth` class, which allows you to authenticate using your Developer Token.
Use instance of `BoxDeveloperTokenAuth` to initialize `BoxClient` object.
Using `BoxClient` object you can access managers, which allow you to perform some operations on your Box account.

The example below demonstrates how to authenticate with Developer Token and print names of all items inside a root folder.

```c#
using Box.Sdk.Gen;

var auth = new BoxDeveloperTokenAuth(token: "DEVELOPER_TOKEN_GOES_HERE");
var client = new BoxClient(auth: auth);

var items = await client.Folders.GetFolderItemsAsync(folderId: "0");
if (items.Entries != null)
{
    foreach (var item in items.Entries)
    {
        if (item.FileFull != null)
        {
            Console.WriteLine(item.FileFull.Name);
        }
        else if (item.FolderMini != null)
        {
            Console.WriteLine(item.FolderMini.Name);
        }
        else if (item.WebLink != null)
        {
            Console.WriteLine(item.WebLink.Name);
        }
    }
}
```

The usage docs that show how to make calls to the Box API with the SDK can be found [here](https://github.com/box/box-Dotnet-sdk-gen/tree/main/docs).

We recommend, familiarizing yourself with the remaining [authentication methods](https://github.com/box/box-Dotnet-sdk-gen/tree/main/docs/Authentication.md), [uploading files](https://github.com/box/box-Dotnet-sdk-gen/tree/main/docs/Uploads.md) and [downloading files](https://github.com/box/box-Dotnet-sdk-gen/tree/main/docs/Downloads.md).

# Documentation

Browse the [docs](docs/README.md) or see [API Reference](https://developer.box.com/reference/) for more information.

# Upgrades

Upgrading from our legacy SDKs to the new generation SDKs is a straightforward process. See our [migration guide](migration-guide.md) and [changelog](CHANGELOG.md) for more information.

# Integration Tests

## Running integration tests locally

### Create Platform Application

To run integration tests locally you will need a `Custom App` created in the [Box Developer
Console](https://app.box.com/developers/console)
with `Server Authentication (with JWT)` selected as authentication method.
Once created you can edit properties of the application:

- In section `App Access Level` select `App + Enterprise Access`. You can enable all `Application Scopes`.
- In section `Advanced Features` enable `Make API calls using the as-user header` and `Generate user access tokens`.

Now select `Authorization` and submit application to be reviewed by account admin.

### Export configuration

1. Select `Configuration` tab and in the bottom in the section `App Settings`
   download your app configuration settings as JSON.
2. Encode configuration file to Base64, e.g. using command: `base64 -i path_to_json_file`
3. Set environment variable: `JWT_CONFIG_BASE_64` with base64 encoded jwt configuration file
4. Set environment variable: `BOX_FILE_REQUEST_ID` with ID of file request already created in the user account, `BOX_EXTERNAL_USER_EMAIL` with email of free external user which not belongs to any enterprise.
5. Set environment variable: `WORKFLOW_FOLDER_ID` with the ID of the Relay workflow that deletes the file that triggered the workflow. The workflow should have a manual start to be able to start it from the API.
6. Set environment variable: `APP_ITEM_ASSOCIATION_FILE_ID` to the ID of the file with associated app item and `APP_ITEM_ASSOCIATION_FOLDER_ID` to the ID of the folder with associated app item.
7. Set environment variable: `APP_ITEM_SHARED_LINK` to the shared link associated with app item.
8. Set environment variable: `SLACK_AUTOMATION_USER_ID` to the ID of the user responsible for the Slack automation, `SLACK_ORG_ID` to the ID of the Slack organization and `SLACK_PARTNER_ITEM_ID` to the ID of the Slack partner item.

### Running tests

To run integration tests locally:

1. `dotnet test`

# Questions, Bugs, and Feature Requests?

Need to contact us directly? [Browse the issues
tickets](https://github.com/box/box-Dotnet-sdk-gen/issues)! Or, if that
doesn't work, [file a new
one](https://github.com/box/box-Dotnet-sdk-gen/issues/new) and we will get
back to you. If you have general questions about the Box API, you can
post to the [Box Developer Forum](https://forum.box.com/).

# Copyright and License

Copyright 2023 Box, Inc. All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

For third party notices visit [THIRD-PARTY-NOTICES](THIRD-PARTY-NOTICES.txt)
