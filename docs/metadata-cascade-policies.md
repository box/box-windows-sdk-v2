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
BoxMetadataCascadePolicy metadataCascadePolicy = await client.MetadataCascadePolicyManager.CreateCascadePolicyAsync("22222", "enterprise_11111", "templateKey");
```

Get a Metadata Cascade Policy
-----------------------------

To get information about a specific metadata cascade policy, call
`MetadataCascadePolicyManager.GetCascadePolicyAsync(string policyId)`

```c#
BoxMetadataCascadePolicy retrievedCascadePolicy = await client.MetadataCascadePolicyManager.GetCascadePolicyAsync("12345");
```

Get Metadata Cascade Policies for Folder
----------------------------------------

To retrieve a collection of metadata cascade policies within a given folder for the current enterprise, use
`MetadataCascadePolicyManager.GetAllMetadataCascadePoliciesAsync(string folderId)`

```c#
BoxCollectionMarkerBased<BoxMetadataCascadePolicy> metadataCascadePolicies = await client.MetadataCascadePolicyManager.GetAllMetadataCascadePoliciesAsync("12345");
```

You can also retrieve metadata cascade policies for another enterprise with specific fields to retrieve by using
`MetadataCascadePolicyManager.GetAllMetadataCascadePoliciesAsync(string folderId, string ownerEnterpriseId, int limit, IEnumberable<string> fields)`

```c#
string folderId = "1111";
string ownerEnterpriseId = "2222";
BoxCollectionMarkerBased<BoxMetadataCascadePolicy> metadataCascadePolicies = await client.MetadataCascadePolicyManager.GetAllMetadataCascadePoliciesAsync(folderId, ownerEnterpriseId);
```

Force Apply Metadata Cascade Policy
-----------------------------------

To apply a policy on a folder that already has one, use
`MetadataCascadePolicyManager.ForceApplyCascadePolicyAsync(string policyId, string conflictResolution)`

```c#
string policyId = "11111";
string conflictResolution = "overwrite";
BoxMetadataCascadePolicy newCascadePolicy = client.MetadataCascadePolicyManager.ForceApplyCascadePolicyAsync(policyId, conflictResolution);
```

The conflict_resolution field can be set to either `none` which will preserve the existing value on the file, or `overwrite`, which will force-apply the cascade policy's value over any existing value. 


