<p align="center">
  <img src="https://github.com/box/sdks/blob/master/images/box-dev-logo.png" alt= “box-dev-logo” width="30%" height="50%">
</p>

# Box Windows V2 SDK

[![Project Status](http://opensource.box.com/badges/active.svg)](http://opensource.box.com/badges)
![Platform Framework](https://img.shields.io/badge/.NET%20Framework-%3E%3D4.6.2-blue)
![Platform Core](https://img.shields.io/badge/.NET%20Core-%3E%3D2.0-blue)
[![License](https://img.shields.io/badge/license-Apache2-blue)](https://raw.githubusercontent.com/box/box-windows-sdk-v2/main/LICENSE)
[![Build](https://github.com/box/box-windows-sdk-v2/actions/workflows/build_and_test.yml/badge.svg)](https://github.com/box/box-windows-sdk-v2/actions/workflows/build_and_test.yml)

The Box .NET SDK can be used to make API calls to the Box APIs in a .NET project.

The SDK is available for both .NET Framework 4.6.2 and .NET Core 2.0 or above. The installation of the SDK depends on the platform used.

## Table of contents

- [Getting Started](#getting-started)
  - [Installation](#installation)
  - [Authentication](#authentication)
  - [Sample Apps](#sample-apps)
- [Usage](#usage)
- [Other Resources](#additional-resources)
- [Versions](#versions)
  - [Supported Version](#supported-version)
  - [Version schedule](#version-schedule)
- [Questions, Bugs, and Feature Requests?](#questions-bugs-and-feature-requests)
- [Contributing](#contributing)
- [Copyright and License](#copyright-and-license)

## Getting Started

### Installation

You can install SDK library using Nuget

If you want to use .NET Core
```bash
PM> Install-Package Box.V2.Core
```

If you want to use .NET Framework
```bash
PM> Install-Package Box.V2
```

Or you can add it to your project directly in Visual Studio.

You can also download latest version from our [Github's release page](https://github.com/box/box-windows-sdk-v2/releases)

### Authentication

Our .NET SDK supports the following authentication methods:
- [Server Auth with JWT](/docs/authentication.md#server-auth-with-jwt)
- [Server Auth with CCG](/docs/authentication.md#server-auth-with-ccg)
- [Traditional 3-Legged OAuth2](/docs/authentication.md#traditional-3-legged-oauth2)
- [Developer token](/docs/authentication.md#developer-token)

### Sample apps

You can check one of our sample apps included in this repository to see how to use the SDK
- [Create App User](https://github.com/box/box-windows-sdk-v2/tree/main/Box.V2.Samples.Core.AppUser.Create/)
- [Upload File](https://github.com/box/box-windows-sdk-v2/tree/main/Box.V2.Samples.Core.File.Upload/)
- [Proxy example](https://github.com/box/box-windows-sdk-v2/tree/main/Box.V2.Samples.Core.HttpProxy/)
- [JWT Auth](https://github.com/box/box-windows-sdk-v2/tree/main/Box.V2.Samples.JWTAuth/)
- [Token exchange](https://github.com/box/box-windows-sdk-v2/tree/main/Box.V2.Samples.TransactionalAuth/)

## Usage

You can find detailed usage documentation and code samples under [docs](/docs/README.md) directory.

## Other resources
- [API Reference](https://developer.box.com/reference/)
- [API Guides](https://developer.box.com/guides/)
- [SDK Nuget Package](https://www.nuget.org/packages/Box.V2/)
- [.NET Core SDK Nuget Package](https://www.nuget.org/packages/Box.V2.Core/)
- [Box Windows SDK Video Tutorial](https://youtu.be/hqko0hxbaXU)
- [Getting Started Docs](https://developer.box.com/guides/tooling/sdks/dotnet/)

## Versions

We use a modified version of [Semantic Versioning](https://semver.org/) for all changes. See [version strategy](VERSIONS.md) for details which is effective from 30 July 2022.

### Supported Version
<!-- textlint-disable -->
Only the current MAJOR version of SDK is supported. New features, functionality, bug fixes, and security updates will only be added to the current MAJOR version.
<!-- textlint-enable -->
A current release is on the leading edge of our SDK development, and is intended for customers who are in active development and want the latest and greatest features.  Instead of stating a release date for a new feature, we set a fixed minor or patch release cadence of maximum 2-3 months (while we may release more often). At the same time, there is no schedule for major or breaking release. Instead, we will communicate one quarter in advance the upcoming breaking change to allow customers to plan for the upgrade. We always recommend that all users run the latest available minor release for whatever major version is in use. We highly recommend upgrading to the latest SDK major release at the earliest convenient time and before the EOL date.

### Version schedule

| Version | Supported Environments                   | State     | First Release | EOL/Terminated |
|---------|------------------------------------------|-----------|---------------|----------------|
| 5       | .NET Framework 4.6.2+ and .NET Core 2.0+ | Supported | 02 Nov 2021   | TBD            |
| 4       | .NET Framework 4.5+ and .NET Core 2.0+   | EOL       | 02 Nov 2021   | TBD            |
| 3       |                                          | EOL       | 28 Jul 2017   | 02 Nov 2021    |
| 2       |                                          | EOL       | 05 Nov 2015   | 28 Jul 2017    |

### Migrating from the old version?

If you are migrating from the old major version visit our [upgrade documentation](/docs/upgrades/).

## Questions, Bugs, and Feature Requests?

[Browse the issues tickets](https://github.com/box/box-windows-sdk-v2/issues)! Or, if that doesn't work, [file a new one](https://github.com/box/box-windows-sdk-v2/issues/new) and someone will get back to you. If you have general questions about the
Box API, you can post to the [Box Developer Forum](https://community.box.com/t5/Developer-Forum/bd-p/DeveloperForum).

## Contributing

All contributions to this project are welcome! For more information, please see our [Contribution guidelines](/CONTRIBUTING.md).

## Copyright and License

Copyright 2018 Box, Inc. All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
