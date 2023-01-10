Retention Policies
==================

A retention policy blocks permanent deletion of content for a specified amount of time.
Admins can create retention policies and then later assign them to specific folders or
their entire enterprise. To use this feature, you must have the manage retention
policies scope enabled for your API key via your application management console.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Create Retention Policy](#create-retention-policy)
- [Get Retention Policy](#get-retention-policy)
- [Update Retention Policy](#update-retention-policy)
- [Get Enterprise Retention Policies](#get-enterprise-retention-policies)
- [Get Retention Policy Assignments](#get-retention-policy-assignments)
- [Assign Retention Policy](#assign-retention-policy)
- [Get Retention Policy Assignment](#get-retention-policy-assignment)
- [Get File Version Retention](#get-file-version-retention)
- [Get File Version Retentions](#get-file-version-retentions) (will be deprecated in the future, use [Get Files Under Retention For Assignment](#get-files-under-retention-for-assignment) and [Get File Version Under Retention For Assignment](#get-file-versions-under-retention-for-assignment) instead)
- [Get Files Under Retention For Assignment](#get-files-under-retention-for-assignment)
- [Get File Version Under Retention For Assignment](#get-file-versions-under-retention-for-assignment)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Create Retention Policy
-----------------------

To create a new retention policy, call
`RetentionPoliciesManager.CreateRetentionPolicyAsync(BoxRetentionPolicyRequest retentionPolicyRequest)`
with the parameters for the new retention policy.

<!-- sample post_retention_policies -->
```c#
var policyParams = new BoxRetentionPolicyRequest()
{
    PolicyName = "Important Documents!",
    PolicyType = "finite",
    RetentionLength = 365,
    DispositionAction = "remove_retention"
};
BoxRetentionPolicy policy = await client.RetentionPoliciesManager
    .CreateRetentionPolicyAsync(policyParams);
```

Get Retention Policy
--------------------

To retrieve information about a specific retention policy, call
`RetentionPoliciesManager.GetRetentionPolicyAsync(string id, IEnumerable<string> fields = null)`
with the ID of the policy.

<!-- sample get_retention_policies_id -->
```c#
BoxRetentionPolicy policy = await client.RetentionPoliciesManager.GetRetentionPolicyAsync("11111");
```

Update Retention Policy
-----------------------

To update or modify an existing retention policy, call
`RetentionPoliciesManager.UpdateRetentionPolicyAsync(string id, BoxRetentionPolicyRequest retentionPolicyRequest, IEnumerable<string> fields = null)`
with the ID of the policy to update and the set of fields to update.

<!-- sample put_retention_policies_id -->
```c#
var updates = new BoxRetentionPolicyRequest()
{
    PolicyName = "New Policy Name"
};
BoxRetentionPolicy updatedPolicy = await client.RetentionPoliciesManager
    .UpdateRetentionPolicyAsync("11111", updates);
```

Get Enterprise Retention Policies
---------------------------------

To retrieve all of the retention policies for the given enterprise, call
`RetentionPoliciesManager.GetRetentionPoliciesAsync(string policyName = null, string policyType = null, string createdByUserId = null, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false)`.

<!-- sample get_retention_policies -->
```c#
BoxCollectionMarkerBased<BoxRetentionPolicy> policies = await client.RetentionPoliciesManager
    .GetRetentionPoliciesAsync();
```

Get Retention Policy Assignments
--------------------------------

To get a list of all retention policy assignments associated with a specified retention policy, call
`RetentionPoliciesManager.GetRetentionPolicyAssignmentsAsync(string retentionPolicyId, string type = null, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false)`
with the ID of the policy to get asisgnments for.

<!-- sample get_retention_policies_id_assignments -->
```c#
BoxCollectionMarkerBased<BoxRetentionPolicyAssignment> assignments = await client.RetentionPoliciesManager
    .GetRetentionPolicyAssignmentsAsync(retentionPolicyId: "11111");
```

Assign Retention Policy
-----------------------

To assign a retention policy, call
`RetentionPoliciesManager.CreateRetentionPolicyAssignmentAsync(BoxRetentionPolicyAssignmentRequest policyAssignmentRequest, IEnumerable<string> fields = null)`
with the parameters of the assignment.

<!-- sample post_retention_policy_assignments -->
```c#
var assignmentParams = new BoxRetentionPolicyAssignmentRequest()
{
    PolicyId = "11111",
    AssignTo = new BoxRequestEntity()
    {
        Type = BoxType.folder,
        Id = "22222"
    }
};
BoxRetentionPolicyAssignment assignment = await client.RetentionPoliciesManager
    .CreateRetentionPolicyAssignmentAsync(assignmentParams);
```

Get Retention Policy Assignment
-------------------------------

To retrieve information about a retention policy assignment, call
`RetentionPoliciesManager.GetRetentionPolicyAssignmentAsync(string retentionPolicyAssignmentId, IEnumerable<string> fields = null)`
with the ID of the assignment.

<!-- sample get_retention_policy_assignments_id -->
```c#
BoxRetentionPolicyAssignment assignment = await client.RetentionPoliciesManager
    .GetRetentionPolicyAssignmentAsync("33333");
```

Get File Version Retention
--------------------------

A file version retention is a record for a retained file version.  To get information
for a specific file version retention record, call the
`RetentionPoliciesManager.GetFileVersionRetentionAsync(string fileVersionRetentionId, IEnumerable<string> fields = null)`
method with the ID of the retention object.

<!-- sample get_file_version_retentions_id -->
```c#
BoxFileVersionRetention retention = await client.RetentionPoliciesManager
    .GetFileVersionRetentionAsync("55555");
```

Get File Version Retentions
---------------------------

To retrieve a list of all file version retentions for the given enterprise or to filter for
some category of file version retention records, call
`RetentionPoliciesManager.GetFileVersionRetentionsAsync(IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false, string fileId = null, string fileVersionId = null, string policyId = null, DateTime? dispositionBefore = null, DateTime? dispositionAfter = null, DispositionAction? dispositionAction = null)`.

<!-- sample get_file_version_retentions -->
```c#
BoxCollectionMarkerBased<BoxFileVersionRetention> retentions = await client.RetentionPoliciesManager
    .GetFileVersionRetentionsAsync();
```

Get Files Under Retention For Assignment
----------------------------------------

To retrieve a list of all files under retention for assignment, call
`RetentionPoliciesManager.GetFilesUnderRetentionForAssignmentAsync(string retentionPolicyAssignmentId, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false)`.

<!-- sample get_retention_policy_assignments_id_files_under_retention -->
```c#
BoxCollectionMarkerBased<BoxFile> filesUnderRetentionForAssignment = await client.RetentionPoliciesManager
    .GetFilesUnderRetentionForAssignmentAsync("55555");
```

Get File Versions Under Retention For Assignment
------------------------------------------------

To retrieve a list of all file versions under retention for assignment, call
`RetentionPoliciesManager.GetFileVersionsUnderRetentionForAssignmentAsync(string retentionPolicyAssignmentId, IEnumerable<string> fields = null, int limit = 100, string marker = null, bool autoPaginate = false)`.

<!-- sample get_retention_policy_assignments_id_file_versions_under_retention -->
```c#
BoxCollectionMarkerBased<BoxFileVersion> fileVersionsUnderRetentionForAssignment = await client.RetentionPoliciesManager
    .GetFileVersionsUnderRetentionForAssignmentAsync("55555");
```

