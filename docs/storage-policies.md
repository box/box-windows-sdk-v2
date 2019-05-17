Storage Policies
================

Storage policies allow enterprise administrators to choose where their content is physically stored;
different policies can be purchased and assigned either as the default policy for the entire enterprise
or on a per-user basis.

Get Available Storage Policies for an Enterprise
------------------------------------------------

To get a list of the storage policies that are available for the current user's enterprise, call the 
`StoragePoliciesManager.GetListStoragePoliciesAsync(string fields = null, string marker = null, int limit = 100, bool autoPaginate = false)`
method.

<!-- sample get_storage_policies -->
```c#
BoxCollectionMarkerBased<BoxStoragePolicy> policies = await client.StoragePoliciesManager
    .GetListStoragePoliciesAsync();
```


Get Information About a Specific Storage Policy
-----------------------------------------------

Information about a specific storage policy (by its ID) can be retrieved by calling
the `StoragePoliciesManager.GetStoragePolicyAsync(String policyId)` method with the ID of
the storage policy to retrieve.

<!-- sample get_storage_policies_id -->
```c#
BoxStoragePolicy policy = await client.StoragePoliciesManager.GetStoragePolicyAsync(policyId: "6");
```

Assign a Storage Policy to a User
---------------------------------

To assign a storage policy to a user, call the 
`StoragePoliciesManager.AssignAsync(string userId, string storagePolicyId)`
method with the ID of the storage policy to assign and the ID of the user to which it should be assigned.

> __Note:__ This method will check if an assignment already exists for the user and take appropriate action.
> It should work regardless of the current status of the user.

```c#
BoxStoragePolicyAssignment assignment = await client.StoragePoliciesManager
    .AssignAsync(userId: "22222", storagePolicyId: "6");
```

Get Information About a Specific Storage Policy Assignment
----------------------------------------------------------

To get information about a specific storage policy assignment by ID, call the
`StoragePoliciesManager.GetAssignmentAsync(string assignmentId)` method
with the ID of the storage policy assignment.

<!-- sample get_storage_policy_assignments_id -->
```c#
BoxStoragePolicyAssignment assignment = await client.StoragePoliciesManager
    .GetAssignmentAsync(assignmentId: "dXNlcl8yMjIyMg==");
```

Get the Storage Policy Assigned to a User
-----------------------------------------

To determine which storage policy is assigned to a user, call
`StoragePoliciesManager.GetAssignmentForTargetAsync(string entityId, string entityType = "user")`
with the ID of the user.

<!-- sample get_storage_policy_assignments -->
```c#
BoxStoragePolicyAssignment assignment = client.StoragePoliciesManager
    .GetAssignmentForTargetAsync("22222");
```

Create a Storage Policy Assignment
----------------------------------

To create a new storage policy assignment, call the
`StoragePoliciesManager.CreateAssignmentAsync(string userId, string policyId)` method
with the ID of the storage policy to assign and the ID of the user to assign it to.

> __Note:__ This method only works if the user does not already have an assignment.
> If the current state of the user is not known, use the [`AssignAsync()`](#assign-a-storage-policy-to-a-user)
> method instead.

<!-- sample post_storage_policy_assignments -->
```c#
BoxStoragePolicyAssignment assignment = client.StoragePoliciesManager
    .CreateAssignmentAsync(userId: "22222", policyId: "6");
```

Update a Storage Policy Assignment
----------------------------------

To update a storage policy assignment, for example to update which storage policy is
asisgned to a user, call the `StoragePoliciesManager.UpdateStoragePolicyAssignment(string assignmentId, String policyId)`
method with the ID of the assignment to update and the new policy ID to assign.

<!-- sample put_storage_policy_assignments_id -->
```c#
// Reassign user 1234 to storage policy 7
BoxStoragePolicyAssignment assignment = await client.StoragePoliciesManager
    .UpdateStoragePolicyAssignment(assignmentId: "dXNlcl8yMjIyMg==", policyId: "7");
```

Remove a Storage Policy Assignment
----------------------------------

To remove a storage policy assignment and return the user it was assigned to to the
default storage policy for the enterprise, call
`StoragePoliciesManager.DeleteAssignmentAsync(string assignmentId)` with
the ID of the assignment to remove.

<!-- sample delete_storage_policy_assignments -->
```c#
await client.StoragePoliciesManager.DeleteAssignmentAsync(assignmentId: "dXNlcl8yMjIyMg==");
```
