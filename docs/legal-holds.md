Legal Hold Policies
==================

A legal hold policy blocks permanent deletion of content during ongoing litigation.
Admins can create legal hold policies and then later assign them to specific folders,
files, or users.

Get Legal Hold Policy
--------------------

To retrieve information about a specific legal hold policy, call
`LegalHoldPoliciesManager.GetLegalHoldPolicyAsync(string legalHoldId)`
with the ID of the legal hold policy.

```c#
BoxLegalHoldPolicy policy = await client.LegalHoldPoliciesManager.GetLegalHoldPolicyAsync("11111");
```

Get Enterprise Legal Hold Policies
----------------------------------

To retrieve all of the legal hold policies for the given enterprise, call
`LegalHoldPoliciesManager.GetListLegalHoldPoliciesAsync(string policyName = null, string fields = null, int limit = 100, string marker = null, bool autoPaginate = false)`.

```c#
BoxCollectionMarkerBased<BoxLegalHoldPolicy> policies = await client.LegalHoldPoliciesManager
    .GetListLegalHoldPoliciesAsync();
```

Create Legal Hold Policy
-----------------------

To create a new legal hold policy, call
`LegalHoldPoliciesManager.CreateLegalHoldPolicyAsync(BoxLegalHoldPolicyRequest createRequest)`.

```c#
var policyParams = new BoxLegalHoldPolicyRequest()
{
    PolicyName = "IRS Audit"
};
BoxLegalHoldPolicy policy = await client.LegalHoldPoliciesManager
    .CreateLegalHoldPolicyAsync(policyParams);
```

Update Legal Hold Policy
------------------------

To update or modify an existing legal hold policy, call
`LegalHoldPoliciesManager.UpdateLegalHoldPolicyAsync(string legalHoldPolicyId, BoxLegalHoldPolicyRequest updateRequest)`
with the ID of the policy to update and the fields to update.

```c#
var updates = new BoxLegalHoldPolicyRequest()
{
    Description = "Hold for documents related to the IRS audit"
};
BoxLegalHoldPolicy updatedPolicy = await client.LegalHoldPoliciesManager
    .UpdateLegalHoldPolicyAsync("11111", updates);
```

Delete Legal Hold Policy
------------------------

To delete a legal hold policy, call
`LegalHoldPoliciesManager.DeleteLegalHoldPolicyAsync(string legalHoldPolicyId)`.
Note that this is an asynchronous process - the policy will not be fully deleted
yet when the response comes back.

```c#
await client.LegalHoldPoliciesManager.DeleteLegalHoldPolicyAsync("11111");
```

Get Legal Hold Policy Assignment
--------------------------------

To retrieve information about a legal hold policy assignment, call
`LegalHoldPoliciesManager.GetAssignmentAsync(string assignmentId)` with the ID of the assignment object.

```c#
BoxLegalHoldPolicyAssignment assignment = await client.LegalHoldPoliciesManager
    .GetAssignmentAsync(assignmentId: "22222");
```

Get Legal Hold Policy Assignments
---------------------------------

To get a list of all legal hold policy assignments associated with a specified legal hold policy, call
`LegalHoldPoliciesManager.GetAssignmentsAsync(string legalHoldPolicyId, string fields = null, string assignToType = null, string assignToId = null, int limit = 100, string marker = null, bool autoPaginate = false)`
with the ID of the policy.

```c#
BoxCollectionMarkerBased<BoxLegalHoldPolicyAssignment> assignments = await client.LegalHoldPoliciesManager
    .GetAssignmentsAsync(legalHoldPolicyId: "11111");
```

Assign Legal Hold Policy
-----------------------

To assign a legal hold policy, call
`LegalHoldPoliciesManager.CreateAssignmentAsync(BoxLegalHoldPolicyAssignmentRequest createRequest)`.

```c#
var requestParams = new BoxLegalHoldPolicyAssignmentRequest()
{
    PolicyId = "11111",
    AssignTo = new BoxRequestEntity()
    {
        Type = "folder",
        Id = "12345"
    }
};
BoxLegalHoldPolicyAssignment assignment = await client.LegalHoldPoliciesManager
    .CreateAssignmentAsync(requestParams);
```

Delete Legal Hold Policy Assignment
-----------------------------------

To delete a legal hold assignment and remove a legal hold policy from an item, call the
`LegalHoldPoliciesManager.DeleteAssignmentAsync(string assignmentId)`
method.  Note that this is an asynchronous process - the assignment will not be fully deleted
yet when the response comes back.

```c#
await client.LegalHoldPoliciesManager.DeleteAssignmentAsync("22222");
```

Get File Version Legal Hold
---------------------------

A file version legal hold is a record for a held file version.  To get information
for a specific file version legal hold record, call
`LegalHoldPoliciesManager.GetFileVersionLegalHoldAsync(string fileVersionLegalHoldId)`
with the ID of the file version legal hold record.

```c#
BoxFileVersionLegalHold hold = await client.LegalHoldPoliciesManager
    .GetFileVersionLegalHoldAsync("55555");
```

Get File Version Legal Holds
----------------------------

To retrieve a list of all file version legal holds for a given policy, call
`LegalHoldPoliciesManager.GetFileVersionLegalHoldsAsync(string policyId, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false)`
with the ID of the legal hold policy.

```c#
BoxCollectionMarkerBased<BoxFileVersionLegalHold> holds = await client.LegalHoldPoliciesManager
    .GetFileVersionLegalHoldsAsync(policyId: "11111");
```