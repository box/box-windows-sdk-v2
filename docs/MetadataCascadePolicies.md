# IMetadataCascadePoliciesManager


- [List metadata cascade policies](#list-metadata-cascade-policies)
- [Create metadata cascade policy](#create-metadata-cascade-policy)
- [Get metadata cascade policy](#get-metadata-cascade-policy)
- [Remove metadata cascade policy](#remove-metadata-cascade-policy)
- [Force-apply metadata cascade policy to folder](#force-apply-metadata-cascade-policy-to-folder)

## List metadata cascade policies

Retrieves a list of all the metadata cascade policies
that are applied to a given folder. This can not be used on the root
folder with ID `0`.

This operation is performed by calling function `GetMetadataCascadePolicies`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-metadata-cascade-policies/).

<!-- sample get_metadata_cascade_policies -->
```
await client.MetadataCascadePolicies.GetMetadataCascadePoliciesAsync(queryParams: new GetMetadataCascadePoliciesQueryParams(folderId: folder.Id));
```

### Arguments

- queryParams `GetMetadataCascadePoliciesQueryParams`
  - Query parameters of getMetadataCascadePolicies method
- headers `GetMetadataCascadePoliciesHeaders`
  - Headers of getMetadataCascadePolicies method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataCascadePolicies`.

Returns a list of metadata cascade policies.


## Create metadata cascade policy

Creates a new metadata cascade policy that applies a given
metadata template to a given folder and automatically
cascades it down to any files within that folder.

In order for the policy to be applied a metadata instance must first
be applied to the folder the policy is to be applied to.

This operation is performed by calling function `CreateMetadataCascadePolicy`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-metadata-cascade-policies/).

<!-- sample post_metadata_cascade_policies -->
```
await client.MetadataCascadePolicies.CreateMetadataCascadePolicyAsync(requestBody: new CreateMetadataCascadePolicyRequestBody(folderId: folder.Id, scope: CreateMetadataCascadePolicyRequestBodyScopeField.Enterprise, templateKey: templateKey));
```

### Arguments

- requestBody `CreateMetadataCascadePolicyRequestBody`
  - Request body of createMetadataCascadePolicy method
- headers `CreateMetadataCascadePolicyHeaders`
  - Headers of createMetadataCascadePolicy method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataCascadePolicy`.

Returns a new of metadata cascade policy.


## Get metadata cascade policy

Retrieve a specific metadata cascade policy assigned to a folder.

This operation is performed by calling function `GetMetadataCascadePolicyById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-metadata-cascade-policies-id/).

<!-- sample get_metadata_cascade_policies_id -->
```
await client.MetadataCascadePolicies.GetMetadataCascadePolicyByIdAsync(metadataCascadePolicyId: cascadePolicyId);
```

### Arguments

- metadataCascadePolicyId `string`
  - The ID of the metadata cascade policy. Example: "6fd4ff89-8fc1-42cf-8b29-1890dedd26d7"
- headers `GetMetadataCascadePolicyByIdHeaders`
  - Headers of getMetadataCascadePolicyById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `MetadataCascadePolicy`.

Returns a metadata cascade policy.


## Remove metadata cascade policy

Deletes a metadata cascade policy.

This operation is performed by calling function `DeleteMetadataCascadePolicyById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-metadata-cascade-policies-id/).

<!-- sample delete_metadata_cascade_policies_id -->
```
await client.MetadataCascadePolicies.DeleteMetadataCascadePolicyByIdAsync(metadataCascadePolicyId: cascadePolicyId);
```

### Arguments

- metadataCascadePolicyId `string`
  - The ID of the metadata cascade policy. Example: "6fd4ff89-8fc1-42cf-8b29-1890dedd26d7"
- headers `DeleteMetadataCascadePolicyByIdHeaders`
  - Headers of deleteMetadataCascadePolicyById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the policy
is successfully deleted.


## Force-apply metadata cascade policy to folder

Force the metadata on a folder with a metadata cascade policy to be applied to
all of its children. This can be used after creating a new cascade policy to
enforce the metadata to be cascaded down to all existing files within that
folder.

This operation is performed by calling function `ApplyMetadataCascadePolicy`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-metadata-cascade-policies-id-apply/).

<!-- sample post_metadata_cascade_policies_id_apply -->
```
await client.MetadataCascadePolicies.ApplyMetadataCascadePolicyAsync(metadataCascadePolicyId: cascadePolicyId, requestBody: new ApplyMetadataCascadePolicyRequestBody(conflictResolution: ApplyMetadataCascadePolicyRequestBodyConflictResolutionField.Overwrite));
```

### Arguments

- metadataCascadePolicyId `string`
  - The ID of the cascade policy to force-apply. Example: "6fd4ff89-8fc1-42cf-8b29-1890dedd26d7"
- requestBody `ApplyMetadataCascadePolicyRequestBody`
  - Request body of applyMetadataCascadePolicy method
- headers `ApplyMetadataCascadePolicyHeaders`
  - Headers of applyMetadataCascadePolicy method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the API call was successful. The metadata
cascade operation will be performed asynchronously.

The API call will return directly, before the cascade operation
is complete. There is currently no API to check for the status of this
operation.


