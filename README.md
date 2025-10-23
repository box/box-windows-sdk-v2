<p align="center">
  <img src="https://github.com/box/sdks/blob/master/images/box-dev-logo.png" alt= “box-dev-logo” width="30%" height="50%">
</p>

# Box Windows V2 SDK v10

[![Project Status](http://opensource.box.com/badges/active.svg)](http://opensource.box.com/badges)
![build](https://github.com/box/box-windows-sdk-v2/actions/workflows/build.yml/badge.svg)
[![nuget version](https://img.shields.io/nuget/v/box.v2.core.svg)](https://badge.fury.io/nu/box.v2.core)
[![image](https://img.shields.io/nuget/dt/box.v2.core.svg)](https://badge.fury.io/nu/box.v2.core)
![Platform](https://img.shields.io/badge/.NET-8%2B-brightgreen)
![Platform](https://img.shields.io/badge/.NET-462%2B-brightgreen)
[![Coverage](https://coveralls.io/repos/github/box/box-windows-sdk-v2/badge.svg?branch=sdk-gen)](https://coveralls.io/github/box/box-windows-sdk-v2?branch=sdk-gen)

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Introduction](#introduction)
- [Supported versions](#supported-versions)
  - [Version v6](#version-v6)
  - [Version v10](#version-v10)
  - [Which Version Should I Use?](#which-version-should-i-use)
- [Installing](#installing)
- [Getting Started](#getting-started)
- [Authentication](#authentication)
- [Documentation](#documentation)
- [Migration guides](#migration-guides)
- [Versioning](#versioning)
  - [Version schedule](#version-schedule)
- [Contributing](#contributing)
- [Questions, Bugs, and Feature Requests?](#questions-bugs-and-feature-requests)
- [Copyright and License](#copyright-and-license)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

# Introduction

We are excited to introduce the v10 major release of the Box Windows V2 SDK,
designed to elevate the developer experience and streamline your integration with the Box Content Cloud.

With this SDK version, we provide the `Box.Sdk.Gen` namespace, which gives you access to:

1. Full API Support: The new generation of Box SDKs empowers developers with complete coverage of the Box API ecosystem. You can now access all the latest features and functionalities offered by Box, allowing you to build even more sophisticated and feature-rich applications.
2. Rapid API Updates: Say goodbye to waiting for new Box APIs to be incorporated into the SDK. With our new auto-generation development approach, we can now add new Box APIs to the SDK at a much faster pace (in a matter of days). This means you can leverage the most up-to-date features in your applications without delay.
3. Embedded Documentation: We understand that easy access to information is crucial for developers. With our new approach, we have included comprehensive documentation for all objects and parameters directly in the source code of the SDK. This means you no longer need to look up this information on the developer portal, saving you time and streamlining your development process.
4. Enhanced Convenience Methods: Our commitment to enhancing your development experience continues with the introduction of convenience methods. These methods cover various aspects such as chunk uploads, classification, and much more.
5. Seamless Start: The new SDKs integrate essential functionalities like authentication, automatic retries with exponential backoff, exception handling, request cancellation, and type checking, enabling you to focus solely on your application's business logic.

Embrace the new generation of Box SDKs and unlock the full potential of the Box Content Cloud.

# Supported versions

To enhance developer experience, we have introduced the new generated codebase through the `Box.Sdk.Gen` namespace.
The `Box.Sdk.Gen` namespace is available in two major supported versions: v6 and v10.

## Version v6

In v6 of the Box Windows SDK V2, we are introducing a version that consolidates both the manually written namespace (`Box.V2`)
and the new generated namespace (`Box.Sdk.Gen`). This allows developers to use both namespaces simultaneously within a single project

The codebase for v6 of the Box Windows SDK V2 is currently available on the [combined-sdk](https://github.com/box/box-windows-sdk-v2/tree/combined-sdk) branch.
Migration guide which would help with migration from `Box.V2` to `Box.Sdk.Gen` can be found [here](https://github.com/box/box-windows-sdk-v2/tree/sdk-gen/migration-guides/from-dotnet-sdk-gen-v1-to-box-windows-sdk.md).

Version v6 is intended for:

- Existing developers of the Box Windows V2 SDK v5 who want to access new API features while keeping their current codebase largely unchanged.
- Existing developers who are in the process of migrating to `Box.Sdk.Gen`, but do not want to move all their code to the new namespace immediately.

## Version v10

Starting with v10, the SDK is built entirely on the generated `Box.Sdk.Gen` namespace, which fully and exclusively replaces the old `Box.V2` namespace.
The codebase for v10 of the Box Windows SDK V2 is currently available on the [sdk-gen](https://github.com/box/box-windows-sdk-v2/tree/sdk-gen) branch.

Version v10 is intended for:

- New users of the Box Windows SDK V2.
- Developers already working with the generated Box Windows SDK V2 previously available under the [Box Dotnet SDK Gen repository](https://github.com/box/box-dotnet-sdk-gen).

## Which Version Should I Use?

| Scenario                                                                                                                        | Recommended Version                                                          | Example dependency (.csproj / NuGet)                          |
| ------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------- | ------------------------------------------------------------- |
| Creating a new application                                                                                                      | Use [v10](https://github.com/box/box-windows-sdk-v2/tree/sdk-gen)            | `<PackageReference Include="Box.Sdk.Gen" Version="10.0.0" />` |
| App using [Box.Sdk.Gen](https://github.com/box/box-dotnet-sdk-gen)                                                              | Migrate to [v10](https://github.com/box/box-windows-sdk-v2/tree/sdk-gen)     | `<PackageReference Include="Box.Sdk.Gen" Version="10.0.0" />` |
| App using both [Box.Sdk.Gen](https://github.com/box/box-dotnet-sdk-gen) and [Box.V2](https://github.com/box/box-windows-sdk-v2) | Upgrade to [v6](https://github.com/box/box-windows-sdk-v2/tree/combined-sdk) | `<PackageReference Include="Box.V2.Core" Version="6.0.0" />`  |
| App using v5 of [Box.V2](https://github.com/box/box-windows-sdk-v2)                                                             | Upgrade to [v6](https://github.com/box/box-windows-sdk-v2/tree/combined-sdk) | `<PackageReference Include="Box.V2.Core" Version="6.0.0" />`  |

For full guidance on SDK versioning, see the [Box SDK Versioning Guide](https://developer.box.com/guides/tooling/sdks/sdk-versioning/).

# Installing

You can find `Box.V2.Core` package, and it's latest version [on nuget](https://www.nuget.org/packages/Box.V2.Core). You can install this SDK via powershell:

```pwsh
Install-Package Box.V2.Core
```

Alternatively, you can manually add it to the `.csproj` file as a reference:

```xml
<ItemGroup>
  <PackageReference Include="Box.V2.Core" Version="10.x.x" />
</ItemGroup>
```

`Box.V2` version of the SDK can also be found [on nuget](https://www.nuget.org/packages/Box.V2). If you were using `Box.V2` previously, consider migrating to `Box.V2.Core`. If that is not possible, you can still keep using `Box.V2` by installing it with the following powershell command:

```pwsh
Install-Package Box.V2
```

Alternatively, you can manually add it to the `.csproj` file as a reference:

```xml
<ItemGroup>
  <PackageReference Include="Box.V2" Version="10.x.x" />
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

The usage docs that show how to make calls to the Box API with the SDK can be found [here](https://github.com/box/box-windows-sdk-v2/tree/sdk-gen/docs).

We recommend, familiarizing yourself with the remaining [authentication methods](https://github.com/box/box-windows-sdk-v2/tree/sdk-gen/docs/Authentication.md), [uploading files](https://github.com/box/box-windows-sdk-v2/tree/sdk-gen/docs/Uploads.md) and [downloading files](https://github.com/box/box-windows-sdk-v2/tree/sdk-gen/docs/Downloads.md).

# Authentication

Box Windows V2 SDK v10 supports multiple authentication methods including Developer Token, OAuth 2.0,
Client Credentials Grant, and JSON Web Token (JWT).

You can find detailed instructions and example code for each authentication method in
[Authentication](https://github.com/box/box-windows-sdk-v2/tree/sdk-gen/docs/Authentication.md) document.

# Documentation

Browse the [docs](https://github.com/box/box-windows-sdk-v2/tree/sdk-gen/docs/README.md) or see [API Reference](https://developer.box.com/reference/) for more information.

# Migration guides

Migration guides which help you to migrate to supported major SDK versions can be found [here](https://github.com/box/box-windows-sdk-v2/tree/sdk-gen/migration-guides).

# Versioning

We use a modified version of [Semantic Versioning](https://semver.org/) for all changes. See [version strategy](https://github.com/box/box-windows-sdk-v2/tree/sdk-gen/VERSIONS.md) for details which is effective from 30 July 2022.

A current release is on the leading edge of our SDK development, and is intended for customers who are in active development and want the latest and greatest features.  
Instead of stating a release date for a new feature, we set a fixed minor or patch release cadence of maximum 2-3 months (while we may release more often). At the same time, there is no schedule for major or breaking release.
Instead, we will communicate one quarter in advance the upcoming breaking change to allow customers to plan for the upgrade.
We always recommend that all users run the latest available minor release for whatever major version is in use.
We highly recommend upgrading to the latest SDK major release at the earliest convenient time and before the EOL date.

### Version schedule

| Version | Supported Environments                   | State     | First Release | EOL/Terminated         |
| ------- | ---------------------------------------- | --------- | ------------- | ---------------------- |
| 10      | .NET Framework 4.6.2+ and .NET 8+        | Supported | 17 Sep 2025   | TBD                    |
| 6       | .NET Framework 4.6.2+ and .NET 8+        | Supported | 23 Oct 2025   | 2027 or v7 is released |
| 5       | .NET Framework 4.6.2+ and .NET Core 2.0+ | Supported | 12 Jan 2023   | 23 Oct 2025            |
| 4       | .NET Framework 4.5+ and .NET Core 2.0+   | EOL       | 02 Nov 2021   | 12 Jan 2023            |
| 3       |                                          | EOL       | 28 Jul 2017   | 02 Nov 2021            |
| 2       |                                          | EOL       | 05 Nov 2015   | 28 Jul 2017            |

# Contributing

See [CONTRIBUTING.md](https://github.com/box/box-windows-sdk-v2/tree/sdk-gen/CONTRIBUTING.md).

# Questions, Bugs, and Feature Requests?

Need to contact us directly? [Browse the issues tickets](https://github.com/box/box-windows-sdk-v2/issues)! Or, if that
doesn't work, [file a new one](https://github.com/box/box-windows-sdk-v2/issues/new) and we will get
back to you. If you have general questions about the Box API, you can post to the [Box Developer Forum](https://community.box.com/box-platform-5).

# Copyright and License

Copyright 2025 Box, Inc. All rights reserved.

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
