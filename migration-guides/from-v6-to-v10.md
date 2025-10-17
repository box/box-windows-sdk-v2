# Migration guide: migrate from v5 to v6 of `Box Windows SDK V2`

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Introduction](#introduction)
- [Installation](#installation)
  - [How to migrate](#how-to-migrate)
- [Migration Scope and Namespace Compatibility](#migration-scope-and-namespace-compatibility)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Introduction

Version 10 of the `Box Windows SDK V2` is a modern, auto-generated SDK built entirely from the `Box.Sdk.Gen` namespace.

In version 6, the SDK shipped two namespaces side-by-side: the legacy manually-maintained `Box.V2` and the generated `Box.Sdk.Gen`. In version 10 and later, `Box.V2` is removed and only `Box.Sdk.Gen` namespace remains.

This document helps teams migrate projects currently on v6 to v10 by:

- Moving any remaining `Box.V2` usage to the `Box.Sdk.Gen` API surface
- Aligning configuration, authentication, and convenience APIs with v10 conventions

If you are migrating code between the namespaces themselves (Box.V2 → Box.Sdk.Gen), see also the dedicated namespace guide [Migration guide: migrate from Box.V2 to Box.Sdk.Gen namespace](./from-box-v2-to-box-sdk-gen-namespace.md).

## Installation

Starting with v10, the legacy `Box.V2` namespace is no longer included. Installing v10 provides only the `Box.Sdk.Gen` namespace.

### How to migrate

To start using the new version of `Box Windows SDK V2` in .NET project you need to change the project reference.
In you .csproj file of your project you need to change the following:

**New (`Box Windows SDK V2`) v6**

You can find Box.V2.Core package, and it's version 10 of it on [nuget](https://www.nuget.org/packages/Box.V2.Core).
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

## Migration Scope and Namespace Compatibility

If your project only uses `Box.Sdk.Gen` from v6, no code changes are needed to migrate to v10. The generated API surface is the same in v6 and v10.

If you still have code using the legacy `Box.V2` namespace, follow the dedicated guide to update that code: [Migration guide: migrate from Box.V2 to Box.Sdk.Gen namespace](./from-box-v2-to-box-sdk-gen-namespace.md).
