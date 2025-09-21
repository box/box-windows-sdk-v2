# IRetentionPoliciesManager


- [List retention policies](#list-retention-policies)
- [Create retention policy](#create-retention-policy)
- [Get retention policy](#get-retention-policy)
- [Update retention policy](#update-retention-policy)
- [Delete retention policy](#delete-retention-policy)

## List retention policies

Retrieves all of the retention policies for an enterprise.

This operation is performed by calling function `GetRetentionPolicies`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-retention-policies/).

<!-- sample get_retention_policies -->
```
await client.RetentionPolicies.GetRetentionPoliciesAsync();
```

### Arguments

- queryParams `GetRetentionPoliciesQueryParams`
  - Query parameters of getRetentionPolicies method
- headers `GetRetentionPoliciesHeaders`
  - Headers of getRetentionPolicies method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `RetentionPolicies`.

Returns a list retention policies in the enterprise.


## Create retention policy

Creates a retention policy.

This operation is performed by calling function `CreateRetentionPolicy`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-retention-policies/).

<!-- sample post_retention_policies -->
```
await client.RetentionPolicies.CreateRetentionPolicyAsync(requestBody: new CreateRetentionPolicyRequestBody(policyName: retentionPolicyName, policyType: CreateRetentionPolicyRequestBodyPolicyTypeField.Finite, dispositionAction: CreateRetentionPolicyRequestBodyDispositionActionField.RemoveRetention) { AreOwnersNotified = true, CanOwnerExtendRetention = true, Description = retentionDescription, RetentionLength = "1", RetentionType = CreateRetentionPolicyRequestBodyRetentionTypeField.Modifiable });
```

### Arguments

- requestBody `CreateRetentionPolicyRequestBody`
  - Request body of createRetentionPolicy method
- headers `CreateRetentionPolicyHeaders`
  - Headers of createRetentionPolicy method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `RetentionPolicy`.

Returns a new retention policy object.


## Get retention policy

Retrieves a retention policy.

This operation is performed by calling function `GetRetentionPolicyById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-retention-policies-id/).

<!-- sample get_retention_policies_id -->
```
await client.RetentionPolicies.GetRetentionPolicyByIdAsync(retentionPolicyId: retentionPolicy.Id);
```

### Arguments

- retentionPolicyId `string`
  - The ID of the retention policy. Example: "982312"
- queryParams `GetRetentionPolicyByIdQueryParams`
  - Query parameters of getRetentionPolicyById method
- headers `GetRetentionPolicyByIdHeaders`
  - Headers of getRetentionPolicyById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `RetentionPolicy`.

Returns the retention policy object.


## Update retention policy

Updates a retention policy.

This operation is performed by calling function `UpdateRetentionPolicyById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/put-retention-policies-id/).

<!-- sample put_retention_policies_id -->
```
await client.RetentionPolicies.UpdateRetentionPolicyByIdAsync(retentionPolicyId: retentionPolicy.Id, requestBody: new UpdateRetentionPolicyByIdRequestBody() { PolicyName = updatedRetentionPolicyName });
```

### Arguments

- retentionPolicyId `string`
  - The ID of the retention policy. Example: "982312"
- requestBody `UpdateRetentionPolicyByIdRequestBody`
  - Request body of updateRetentionPolicyById method
- headers `UpdateRetentionPolicyByIdHeaders`
  - Headers of updateRetentionPolicyById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `RetentionPolicy`.

Returns the updated retention policy object.


## Delete retention policy

Permanently deletes a retention policy.

This operation is performed by calling function `DeleteRetentionPolicyById`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/delete-retention-policies-id/).

<!-- sample delete_retention_policies_id -->
```
await client.RetentionPolicies.DeleteRetentionPolicyByIdAsync(retentionPolicyId: retentionPolicy.Id);
```

### Arguments

- retentionPolicyId `string`
  - The ID of the retention policy. Example: "982312"
- headers `DeleteRetentionPolicyByIdHeaders`
  - Headers of deleteRetentionPolicyById method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Returns an empty response when the policy has been deleted.


