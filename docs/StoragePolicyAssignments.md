# IStoragePolicyAssignmentsManager


- [List storage policy assignments](#list-storage-policy-assignments)
- [Assign storage policy](#assign-storage-policy)
- [Get storage policy assignment](#get-storage-policy-assignment)
- [Update storage policy assignment](#update-storage-policy-assignment)
- [Unassign storage policy](#unassign-storage-policy)

## List storage policy assignments

Fetches all the storage policy assignment for an enterprise or user.

This operation is performed by calling function `GetStoragePolicyAssignments`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-storage-policy-assignments/).

<!-- sample get_storage_policy_assignments -->
```
await client.StoragePolicyAssignments.GetStoragePolicyAssignmentsAsync(queryParams: new GetStoragePolicyAssignmentsQueryParams(resolvedForType: GetStoragePolicyAssignmentsQueryParamsResolvedForTypeField.User, resolvedForId: userId));
```

### Arguments

- queryParams `GetStoragePolicyAssignmentsQueryParams`
  - Query parameters of getStoragePolicyAssignments method
- headers `GetStoragePolicyAssignmentsHeaders`
  - Headers of getStoragePolicyAssignments method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `StoragePolicyAssignments`.

Returns a collection of storage policies for
the enterprise or user.


## Assign storage policy

Creates a storage policy assignment for an enterprise or user.

This operation is performed by calling function `CreateStoragePolicyAssignment`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-storage-policy-assignments/).

<!-- sample post_storage_policy_assignments -->
```
await client.StoragePolicyAssignments.CreateStoragePolicyAssignmentAsync(requestBody: new CreateStoragePolicyAssignmentRequestBody(storagePolicy: new CreateStoragePolicyAssignmentRequestBodyStoragePolicyField(id: policyId), assignedTo: new CreateStoragePolicyAssignmentRequestBodyAssignedToField(id: userId, type: CreateStoragePolicyAssignmentRequestBodyAssignedToTypeField.User)));
```

### Arguments

- requestBody `CreateStoragePolicyAssignmentRequestBody`
  - Request body of createStoragePolicyAssignment method
- headers `CreateStoragePolicyAssignmentHeaders`
  - Headers of createStoragePolicyAssignment method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `StoragePolicyAssignment`.

Returns the new storage policy assignment created.


## Get storage policy assignment

Fetches a specific storage policy assignment.

This operation is performed by calling function `GetStoragePolicyAssignmentById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-storage-policy-assignments-id/).

<!-- sample get_storage_policy_assignments_id -->
```
await client.StoragePolicyAssignments.GetStoragePolicyAssignmentByIdAsync(storagePolicyAssignmentId: storagePolicyAssignment.Id);
```

### Arguments

- storagePolicyAssignmentId `string`
  - The ID of the storage policy assignment. Example: "932483"
- headers `GetStoragePolicyAssignmentByIdHeaders`
  - Headers of getStoragePolicyAssignmentById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `StoragePolicyAssignment`.

Returns a storage policy assignment object.


## Update storage policy assignment

Updates a specific storage policy assignment.

This operation is performed by calling function `UpdateStoragePolicyAssignmentById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-storage-policy-assignments-id/).

<!-- sample put_storage_policy_assignments_id -->
```
await client.StoragePolicyAssignments.UpdateStoragePolicyAssignmentByIdAsync(storagePolicyAssignmentId: storagePolicyAssignment.Id, requestBody: new UpdateStoragePolicyAssignmentByIdRequestBody(storagePolicy: new UpdateStoragePolicyAssignmentByIdRequestBodyStoragePolicyField(id: storagePolicy2.Id)));
```

### Arguments

- storagePolicyAssignmentId `string`
  - The ID of the storage policy assignment. Example: "932483"
- requestBody `UpdateStoragePolicyAssignmentByIdRequestBody`
  - Request body of updateStoragePolicyAssignmentById method
- headers `UpdateStoragePolicyAssignmentByIdHeaders`
  - Headers of updateStoragePolicyAssignmentById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `StoragePolicyAssignment`.

Returns an updated storage policy assignment object.


## Unassign storage policy

Delete a storage policy assignment.

Deleting a storage policy assignment on a user
will have the user inherit the enterprise's default
storage policy.

There is a rate limit for calling this endpoint of only
twice per user in a 24 hour time frame.

This operation is performed by calling function `DeleteStoragePolicyAssignmentById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-storage-policy-assignments-id/).

<!-- sample delete_storage_policy_assignments_id -->
```
await client.StoragePolicyAssignments.DeleteStoragePolicyAssignmentByIdAsync(storagePolicyAssignmentId: storagePolicyAssignment.Id);
```

### Arguments

- storagePolicyAssignmentId `string`
  - The ID of the storage policy assignment. Example: "932483"
- headers `DeleteStoragePolicyAssignmentByIdHeaders`
  - Headers of deleteStoragePolicyAssignmentById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the storage policy
assignment is successfully deleted.


