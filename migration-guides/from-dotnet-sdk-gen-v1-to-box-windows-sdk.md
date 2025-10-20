# Migration guide: migrate from `Box Dotnet Sdk Gen` (v1.X) to `Box Windows SDK V2` v6.X or v10.X

Note: This guide applies only to migrations targeting Box Windows SDK V2 v6.X.Y or v10.X.Y. It does not apply to other major versions (e.g., v7.X, v11.X).

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Introduction](#introduction)
  - [Installation](#installation)
    - [How to migrate](#how-to-migrate)
      - [v10 installation](#v10-installation)
      - [v6 installation](#v6-installation)
  - [Union classes name changes](#union-classes-name-changes)
    - [How to migrate](#how-to-migrate-1)
  - [Dropping support for .NET 6](#dropping-support-for-net-6)
    - [How to migrate](#how-to-migrate-2)
  - [Removed unused models from schemas namespace](#removed-unused-models-from-schemas-namespace)
    - [How to migrate](#how-to-migrate-3)
  - [Usage](#usage)
    - [Using the Box Windows SDK v10](#using-the-box-windows-sdk-v10)
    - [Using the Box Windows SDK v6](#using-the-box-windows-sdk-v6)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

# Introduction

From the `Box Dotnet Sdk Gen` in version v1.X.Y you can migrate either to v6 or v10 of the `Box Windows SDK V2` SDK and your choice should depend on whether you want to continue using the legacy SDK (`Box Windows SDK V2` v5) alongside the generated one or not.

The v6 version of the `Box Windows SDK V2` contains both the legacy SDK namespace `Box.V2` and the generated one `Box.Sdk.Gen`.
If previously you were using both of SDKs `Box.V2` v5 and `Box.Sdk.Gen` v1.X.Y, you should migrate to v6 version of the `Box Windows SDK V2` which contains `Box.V2` and `Box.Sdk.Gen` namespaces.

If however you were only using the generated SDK namespace `Box.Sdk.Gen` in version v1.X.Y you should migrate to v10 version of the `Box Windows SDK V2` which contains only the generated SDK namespace `Box.Sdk.Gen`.

| Scenario                                     | Your current usage                                     | Recommended target | Namespaces included in target                 | Why this choice                                                          | Notes                                                                                                  |
| -------------------------------------------- | ------------------------------------------------------ | ------------------ | --------------------------------------------- | ------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------ |
| Using both legacy and generated SDK together | `Box.V2` v5 + `Box.Sdk.Gen` v1.X.Y in the same project | v6.X.Y             | `Box.V2` (legacy) + `Box.Sdk.Gen` (generated) | Keep existing v5 code while adopting new features from the generated SDK | Run both namespaces side-by-side; use type aliases to avoid name conflicts (e.g., `BoxClient`, `User`) |
| Using only the generated SDK                 | `Box.Sdk.Gen` v1.X.Y only                              | v10.X.Y            | `Box.Sdk.Gen` (generated) only                | Clean upgrade path with no legacy namespace; simpler dependency surface  | Best when you don’t need the legacy `Box.V2` namespace                                                 |

## Installation

The installation process for v6 and v10 versions of the `Box Windows SDK V2` is similar, but there are just some differences in the version number you need to set.

You need to set the version to `6.X.Y` if you are migrating to v6 or `10.X.Y` if you are migrating to v10.
The name of the package you need to reference has changed from `Box.Sdk.Gen` to `Box.V2`.

### How to migrate

#### v10 installation

To start using the new version of `Box Windows SDK V2` in .NET project you need to change the project reference.
In you .csproj file of your project you need to change the following:

**New (`Box Windows SDK V2`) v10**

You can find Box.V2.Core package, and it's latest version on [nuget](https://www.nuget.org/packages/Box.V2.Core).
You can install this SDK via powershell:

```pwsh
Install-Package Box.V2.Core -Version 10.X.Y
```

Alternatively, you can manually add it to the .csproj file as a reference:

```xml
<ItemGroup>
  <PackageReference Include="Box.V2.Core" Version="10.X.Y" />
</ItemGroup>
```

Box.V2 version of the SDK can also be found on [nuget](https://www.nuget.org/packages/Box.V2).
If you were using Box.V2 previously, consider migrating to Box.V2.Core.
If that is not possible, you can still keep using Box.V2 by installing it with the following powershell command:

```pwsh
Install-Package Box.V2 -Version 10.X.Y
```

Alternatively, you can manually add it to the .csproj file as a reference:

```xml
<ItemGroup>
  <PackageReference Include="Box.V2" Version="10.X.Y" />
</ItemGroup>
```

#### v6 installation

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

## Union classes name changes

In the v6 and v10 versions of the `Box Windows SDK V2` our `OneOf` class names (representing unions from the OpenAPI specification) were fully auto-generated based on the included variants.
This often resulted in overly long names that were difficult to work with in tools like Git. For example: `MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString`. Additionally, every time the new variant was added to the `OneOf`, the class name itself changed.
Starting in v6 and v10 versions, the names of `OneOf` classes are defined directly in the specification. This ensures that they are meaningful, short, and stable over time.

### How to migrate

If your code references any of the renamed classes, replace the old name with the new one.
If you were not explicitly using the type names, no changes are needed, since only the class names changed and their behavior remains the same.

List of changed `OneOf` classes and types associated with them:

| Old name                                                                                   | New name                                                           |
| ------------------------------------------------------------------------------------------ | ------------------------------------------------------------------ |
| AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen                       | AiAgent                                                            |
| AiAgentAskOrAiAgentReference                                                               | AiAskAgent                                                         |
| AiAgentExtractOrAiAgentReference                                                           | AiExtractAgent                                                     |
| AiAgentExtractStructuredOrAiAgentReference                                                 | AiExtractStructuredAgent                                           |
| AiAgentReferenceOrAiAgentTextGen                                                           | AiTextGenAgent                                                     |
| AppItemEventSourceOrEventSourceOrFileOrFolderOrGenericSourceOrUser                         | EventSourceResource                                                |
| FileBaseOrFolderBaseOrWebLinkBase                                                          | AppItemAssociatedItem                                              |
| FileFullOrFolderFull                                                                       | MetadataQueryResultItem                                            |
| FileFullOrFolderFullOrWebLink                                                              | SearchResultWithSharedLinkItem/RecentItemResource/SearchResultItem |
| FileFullOrFolderMiniOrWebLink                                                              | Item                                                               |
| FileMiniOrFolderMini                                                                       | Resource                                                           |
| FileOrFolderOrWebLink                                                                      | LegalHoldPolicyAssignedItem/CollaborationItem                      |
| FileOrFolderScope                                                                          | ResourceScope                                                      |
| FileOrFolderScopeScopeField                                                                | ResourceScopeScopeField                                            |
| FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0                                    | HubItemReferenceV2025R0                                            |
| GroupMiniOrUserCollaborations                                                              | CollaborationAccessGrantee                                         |
| IntegrationMappingPartnerItemSlackUnion                                                    | IntegrationMappingPartnerItemSlack                                 |
| IntegrationMappingPartnerItemTeamsUnion                                                    | IntegrationMappingPartnerItemTeams                                 |
| KeywordSkillCardOrStatusSkillCardOrTimelineSkillCardOrTranscriptSkillCard                  | SkillCard                                                          |
| MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString | MetadataFilterValue                                                |
| SearchResultsOrSearchResultsWithSharedLinks                                                | SearchResultsResponse                                              |

Some classes were split into multiple ones depending on context.

Manager functions affected by these changes:

| Function                                    | Old return type                                                      | New return type       |
| ------------------------------------------- | -------------------------------------------------------------------- | --------------------- |
| AiManager.GetAiAgentDefaultConfigAsync(...) | AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen | AiAgent               |
| SearchManager.SearchForContentAsync(...)    | SearchResultsOrSearchResultsWithSharedLinks                          | SearchResultsResponse |

## Dropping support for .NET 6

With v6 and v10 of `Box Windows SDK V2`, support for .NET 6 has been dropped.
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

## Removed unused models from schemas namespace

Several unused types (classes and enums) have been removed from the schemas because they were not used by any SDK functions or by the Box API.

### How to migrate

Here is the full list of removed types:

| Removed classes/enums                      |
| ------------------------------------------ |
| FileOrFolder                               |
| HubActionV2025R0                           |
| MetadataQueryIndex                         |
| MetadataQueryIndexFieldsField              |
| MetadataQueryIndexFieldsSortDirectionField |
| MetadataQueryIndexStatusField              |
| RetentionPolicyAssignmentBase              |
| RetentionPolicyAssignmentBaseTypeField     |
| SkillInvocation                            |
| SkillInvocationEnterpriseField             |
| SkillInvocationEnterpriseTypeField         |
| SkillInvocationSkillField                  |
| SkillInvocationSkillTypeField              |
| SkillInvocationStatusField                 |
| SkillInvocationStatusStateField            |
| SkillInvocationTokenField                  |
| SkillInvocationTokenReadField              |
| SkillInvocationTokenReadTokenTypeField     |
| SkillInvocationTokenWriteField             |
| SkillInvocationTokenWriteTokenTypeField    |
| SkillInvocationTypeField                   |
| WebhookInvocation                          |
| WebhookInvocationTriggerField              |
| WebhookInvocationTypeField                 |
| WorkflowFull                               |

If your code references any of these types, remove those references.

## Usage

### Using the Box Windows SDK v10

After migration from Box.Sdk.Gen in version v1.X.Y to v10 version of the Box Windows Sdk V2, you can still use the `Box.Sdk.Gen` namespace in the same way as before.
You have to import the `Box.Sdk.Gen` namespace as it was before and use the `BoxClient` class to create an instance of the client.

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

### Using the Box Windows SDK v6

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
