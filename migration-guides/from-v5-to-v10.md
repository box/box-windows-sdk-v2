# Migration guide: migrate from v5 to v10 version of `Box Windows SDK V2`

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Introduction](#introduction)
- [Installation](#installation)
- [Highlighting the Key Differences](#highlighting-the-key-differences)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Introduction

The v10 release of `Box Windows SDK V2` library helps .NET developers to conveniently integrate with Box API.
In the contrary to the previous versions (v5 or lower), it is not manually maintained, but auto-generated
based on Open API Specification. This means you can leverage the most up-to-date Box API features in your
applications without delay. We introduced this major version bump to reflect the significant codebase changes
and to align with other Box SDKs, which will also adopt generated code starting from their v10 releases.
More information and benefits of using the new can be found in the
[README](https://github.com/box/box-windows-sdk-v2/blob/sdk-gen/README.md) file.

## Installation

The library is available on nuget in form of [Box.V2.Core](https://www.nuget.org/packages/Box.V2.Core) and [Box.V2](https://www.nuget.org/packages/Box.V2) package.
Using `Box.V2.Core` artifact should be preferred as it is compatible with the same target frameworks as `Box.V2` and more.

Soon we are going to introduce v6 version of Box Windows SDK V2 that will combine namespace `Box.V2` from
the v5 and `Box.Sdk.Gen` namespace from the v10 version of the SDK so that code from both versions could be used in the same project.
If you would like to use a feature available only in the new SDK, you won't need to necessarily migrate all your code
to use generated SDK at once. You will be able to use a new feature from the `Box.Sdk.Gen` namespace,
while keeping the rest of your code unchanged. Note that it may be required to use fully qualified class names
to avoid conflicts between two packages. However, we recommend to fully migrate to the v10 of the SDK eventually.

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

## Highlighting the Key Differences

There are important differences between the `Box.V2` (v5) and the generated `Box.Sdk.Gen` (v10) namespaces. We have prepared a separate document that presents the main differences and provides guidance to help you migrate. For side-by-side code examples, see: [Migration guide: migrate from Box.V2 to Box.Sdk.Gen namespace](./from-box-v2-to-box-sdk-gen-namespace.md).
