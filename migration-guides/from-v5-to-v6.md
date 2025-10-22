# Migration guide: migrate from v5 to v6 of `Box Windows SDK V2`

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Introduction](#introduction)
- [Installation](#installation)
  - [How to migrate](#how-to-migrate)
- [Dropping support for .NET 6](#dropping-support-for-net-6)
  - [How to migrate](#how-to-migrate-1)
- [Highlighting the Key Differences](#highlighting-the-key-differences)
- [Using both `Box.Sdk.Gen` and `Box.V2` namespaces](#using-both-boxsdkgen-and-boxv2-namespaces)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Introduction

The version 6 of the `Box Windows SDK V2` is a transitional release designed to help you migrate from the legacy v5 SDK to the modern, auto-generated v10+ SDK.

It combines two namespaces into a single package:

- `Box.V2`: The manually-maintained namespace from v5.
- `Box.Sdk.Gen`: The new namespace, auto-generated from the OpenAPI specification (this is the same namespace that makes up the entirety of the v10 SDK).

This approach allows you to adopt new features from the `Box.Sdk.Gen` namespace at your own pace, without needing to immediately rewrite your existing v5 integration.

## Installation

When migrating from v5 to v6, you will need to update the version dependency in your project.

### How to migrate

To start using the new version of `Box Windows SDK V2` in .NET project you need to change the project reference.
In you .csproj file of your project you need to change the following:

**New (`Box Windows SDK V2`) v6**

You can find Box.V2.Core package, and it's version 6 of it on [nuget](https://www.nuget.org/packages/Box.V2.Core).
You can install this SDK via powershell:

```pwsh
Install-Package Box.V2.Core -Version 6.X.Y
```

Alternatively, you can manually add it to the .csproj file as a reference:

```xml
<ItemGroup>
  <PackageReference Include="Box.V2.Core" Version="6.X.Y" />
</ItemGroup>
```

Box.V2 version of the SDK can also be found on [nuget](https://www.nuget.org/packages/Box.V2).
If you were using Box.V2 previously, consider migrating to Box.V2.Core.
If that is not possible, you can still keep using Box.V2 by installing it with the following powershell command:

```pwsh
Install-Package Box.V2 -Version 6.X.Y
```

Alternatively, you can manually add it to the .csproj file as a reference:

```xml
<ItemGroup>
  <PackageReference Include="Box.V2" Version="6.X.Y" />
</ItemGroup>
```

## Dropping support for .NET 6

With v6 of `Box Windows SDK V2`, support for .NET 6 has been dropped.
We follow the official [.NET release lifecycle](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core).
Since November 12, 2024, .NET 6 is no longer officially supported. While it may still be possible to compile and use this SDK under .NET 6, we do not provide support for issues encountered on that version.

### How to migrate

To migrate your project to .NET 8, update the target framework in your project file (.csproj)

Before:

```xml
<TargetFrameworks>net6.0</TargetFrameworks>
```

After:

```xml
<TargetFrameworks>net8.0</TargetFrameworks>
```

Additional changes may be required depending on your project and dependencies.

## Highlighting the Key Differences

The `Box.V2` namespace usage in v6 remains the same as in v5 and is not covered in this document.

If you are migrating code from `Box.V2` to `Box.Sdk.Gen`, which we recommend, the key differences between the namespaces (imports, async/await, method signatures, authentication, configuration, convenience methods) are documented in:

- [Migration guide: migrate from Box.V2 to Box.Sdk.Gen namespace](./from-box-v2-to-box-sdk-gen-namespace.md)

## Using both `Box.Sdk.Gen` and `Box.V2` namespaces

After migration to `Box Windows SDK V2` version 6, you can use both the legacy `Box.V2` namespace and the generated one `Box.Sdk.Gen` namespace.
You just have to import the appropriate namespace depending on which SDK you want to use, which is either `Box.V2` or `Box.Sdk.Gen`.
However, some classes (like `BoxClient` and `User`) exist in both namespaces. If you import both `Box.V2` and `Box.Sdk.Gen` in the same file, you will get error due to the name ambiguity

To avoid this ambiguity you can use [fully qualified names](https://en.wikipedia.org/wiki/Fully_qualified_name) when refering to the types.

```c#
using Box.Sdk.Gen;
using Box.V2;

// Use Box.Sdk.Gen
var auth = new BoxDeveloperTokenAuth(token: "DEVELOPER_TOKEN_GOES_HERE");
// using fully qualified name
var client = new Box.Sdk.Gen.BoxClient(auth: auth);

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

// Use Box.V2
var config = new BoxConfigBuilder("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", new Uri("http://localhost")).Build();
var session = new OAuthSession("YOUR_DEVELOPER_TOKEN", "N/A", 3600, "bearer");
// using fully qualified name
var legacyClient = new Box.V2.BoxClient(config, session);

var legacyItems = await legacyClient.FoldersManager.GetFolderItemsAsync(id: "0", limit: 1000);
if (legacyItems.Entries != null)
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

Alternatively, you could use [aliases](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-12.0/using-alias-types) to solve ambiguity issue.
