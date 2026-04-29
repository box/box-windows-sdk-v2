# IAutomateWorkflowsManager


- [List Automate workflows defined as callable actions](#list-automate-workflows-defined-as-callable-actions)
- [Start Automate workflow](#start-automate-workflow)

## List Automate workflows defined as callable actions

Returns workflow actions from Automate for a folder, using the
`WORKFLOW` action category.

This operation is performed by calling function `GetAutomateWorkflowsV2026R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2026.0/get-automate-workflows/).

<!-- sample get_automate_workflows_v2026.0 -->
```
await adminClient.AutomateWorkflows.GetAutomateWorkflowsV2026R0Async(queryParams: new GetAutomateWorkflowsV2026R0QueryParams(folderId: workflowFolderId));
```

### Arguments

- queryParams `GetAutomateWorkflowsV2026R0QueryParams`
  - Query parameters of getAutomateWorkflowsV2026R0 method
- headers `GetAutomateWorkflowsV2026R0Headers`
  - Headers of getAutomateWorkflowsV2026R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `AutomateWorkflowsV2026R0`.

Returns workflow actions that can be manually started.


## Start Automate workflow

Starts an Automate workflow manually by using a workflow action ID and file IDs.

This operation is performed by calling function `CreateAutomateWorkflowStartV2026R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2026.0/post-automate-workflows-id-start/).

<!-- sample post_automate_workflows_id_start_v2026.0 -->
```
await adminClient.AutomateWorkflows.CreateAutomateWorkflowStartV2026R0Async(workflowId: workflowAction.Workflow.Id, requestBody: new AutomateWorkflowStartRequestV2026R0(workflowActionId: workflowAction.Id, fileIds: Array.AsReadOnly(new [] {workflowFileId})));
```

### Arguments

- workflowId `string`
  - The ID of the workflow. Example: "12345"
- requestBody `AutomateWorkflowStartRequestV2026R0`
  - Request body of createAutomateWorkflowStartV2026R0 method
- headers `CreateAutomateWorkflowStartV2026R0Headers`
  - Headers of createAutomateWorkflowStartV2026R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Starts the workflow.


