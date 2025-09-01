# Migration guide from the v1 version of the `dotnet-sdk-gen (Box.Sdk.Gen)` to the v10 version of the `Box Windows SDK V2 (Box.V2/Box.V2.Core)`

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Installation](#installation)
  - [How to migrate](#how-to-migrate)
- [Union classes name changes](#union-classes-name-changes)
  - [How to migrate](#how-to-migrate-1)
- [Dropping support for .NET 6](#dropping-support-for-net-6)
  - [How to migrate](#how-to-migrate-2)
- [Removed unused models from schemas namespace](#removed-unused-models-from-schemas-namespace)
  - [How to migrate](#how-to-migrate-3)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Installation

In order to start using v10 version of the Box Windows SDK V2, you need to change the dependency in your project.
The artifact name has changed from `Box.Sdk.Gen` to `Box.V2`/`Box.V2.Core`.
You also need to set the version to `10.0.0` or higher.

### How to migrate

To start using v10 version of Box Windows SDK V2 in your project replace the dependency in your project file (.csproj)
or install the new package via the command line.

**Old (`dotnet-sdk-gen-v1`)**

```xml
<ItemGroup>
  <PackageReference Include="Box.Sdk.Gen" Version="1.x.x" />
</ItemGroup>
```

```console
Install-Package Box.Sdk.Gen
```

**New (`Box Windows V2 SDK v10`)**

You can find Box.V2.Core package, and it's latest version on [nuget](https://www.nuget.org/packages/Box.V2.Core).
You can install this SDK via powershell:

```pwsh
Install-Package Box.V2.Core
```

Alternatively, you can manually add it to the .csproj file as a reference:

```xml
<ItemGroup>
  <PackageReference Include="Box.V2.Core" Version="10.x.x" />
</ItemGroup>
```

Box.V2 version of the SDK can also be found on [nuget](https://www.nuget.org/packages/Box.V2).
If you were using Box.V2 previously, consider migrating to Box.V2.Core.
If that is not possible, you can still keep using Box.V2 by installing it with the following powershell command:

```pwsh
Install-Package Box.V2
```

Alternatively, you can manually add it to the .csproj file as a reference:

```xml
<ItemGroup>
  <PackageReference Include="Box.V2" Version="10.x.x" />
</ItemGroup>
```

## Union classes name changes

In the v1 version of the `dotnet-sdk-gen` our `OneOf` class names (representing unions from the OpenAPI specification) were fully auto-generated based on the included variants.
This often resulted in overly long names that were difficult to work with in tools like Git. For example: `MetadataFieldFilterDateRangeOrMetadataFieldFilterFloatRangeOrArrayOfStringOrNumberOrString`. Additionally, every time the new variant was added to the `OneOf`, the class name itself changed.
Starting in v10, the names of `OneOf` classes are defined directly in the specification. This ensures that they are meaningful, short, and stable over time.

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

With v10 of `Box Windows SDK V2`, support for .NET 6 has been dropped.
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
