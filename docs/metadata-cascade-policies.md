Metadata Cascade Policies
=========================

A metadata cascade policy indicates if a metadata instance value should be cascaded to files and subfolders in a specific folder.

Any user with edit permisions on a folder can create metadata cascade policies for that given folder. Policies are asigned to exactly one folder and exactly one metadata instance on that folder. It should be noted that there is some delay from file upload to metadata application.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Create Metadata Cascade Policy](#create-metadata-cascade-policy)
- [Get a Metadata Cascade Policy](#get-a-metadata-cascade-policy)
- [Get Metadata Cascade Policies for Folder](#get-metadata-cascade-policies-for-folder)
- [Force Apply Metadata Cascade Policy](#force-apply-metadata-cascade-policy)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Create Metadata Cascade Policy
------------------------------

To create a new metadata cascade policy, call
`MetadataCascadePolicyManager.CreateCascadePolicyAsync(String folderId, string scope, string templateKey)`.

```c#
var metadataCascadePolicy = await client.MetadataCascadePolicyManager.CreateCascadePolicyAsync("22222", "enterprise_11111", "templateKey");
```

Get a Metadata Cascade Policy
-----------------------------

To get information about a specific metadata cascade policy, call
`MetadataCascadePolicyManager.GetCascadePolicyAsync(string policyId)`

```c#
var retrievedCascadePolicy = await client.MetadataCascadePolicyManager.GetCascadePolicyAsync("12345");
```

Get Metadata Cascade Policies for Folder
----------------------------------------

To retrieve a collection of metadata cascade policies within a given folder for the current enterprise, use
`MetadataCascadePolicyManager.GetAllMetadataCascadePoliciesAsync(string folderId)`

```c#
var metadataCascadePolicies = await client.MetadataCascadePolicyManager.GetAllMetadataCascadePoliciesAsync("12345");
```

You can also specify the ID of the owner of the metadata cascade policies by calling
`MetadataCascadePolicyManager.GetAllMetadataCascadePoliciesAsync(string folderId, string ownerEnterpriseId)`

```c#
string folderId = "12345";
string ownerEnterpriseId = "11111";
var metadataCascadePolicies = await client.MetadataCascadePolicyManager.GetAllMetadataCascadePolicies(folderId, ownerEnterpriseId)
```

Force Apply Metadata Cascade Policy
-----------------------------------

To apply a policy on a folder that already has one, use
`MetadataCascadePolicyManager.ForceApplyCascadePolicyAsync(string policyId, string conflictResolution)`

```c#
string policyId = "11111";
string conflictResolution = "overwrite";
client.MetadataCascadePolicyManager.ForceApplyCascadePolicyAsync(policyId, conflictResolution);
```

The conflict_resolution field can be set to either none which will preserve the existing value on the file, and overwrite, which will force-apply the cascade policy's value over any existing value. 


