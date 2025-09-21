# IWorkflowsManager


- [List workflows](#list-workflows)
- [Starts workflow based on request body](#starts-workflow-based-on-request-body)

## List workflows

Returns list of workflows that act on a given `folder ID`, and
have a flow with a trigger type of `WORKFLOW_MANUAL_START`.

You application must be authorized to use the `Manage Box Relay` application
scope within the developer console in to use this endpoint.

This operation is performed by calling function `GetWorkflows`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/get-workflows/).

<!-- sample get_workflows -->
```
await adminClient.Workflows.GetWorkflowsAsync(queryParams: new GetWorkflowsQueryParams(folderId: workflowFolderId));
```

### Arguments

- queryParams `GetWorkflowsQueryParams`
  - Query parameters of getWorkflows method
- headers `GetWorkflowsHeaders`
  - Headers of getWorkflows method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `Workflows`.

Returns the workflow.


## Starts workflow based on request body

Initiates a flow with a trigger type of `WORKFLOW_MANUAL_START`.

You application must be authorized to use the `Manage Box Relay` application
scope within the developer console.

This operation is performed by calling function `StartWorkflow`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/post-workflows-id-start/).

<!-- sample post_workflows_id_start -->
```
await adminClient.Workflows.StartWorkflowAsync(workflowId: NullableUtils.Unwrap(workflowToRun.Id), requestBody: new StartWorkflowRequestBody(flow: new StartWorkflowRequestBodyFlowField() { Type = "flow", Id = NullableUtils.Unwrap(NullableUtils.Unwrap(workflowToRun.Flows)[0].Id) }, files: Array.AsReadOnly(new [] {new StartWorkflowRequestBodyFilesField() { Type = StartWorkflowRequestBodyFilesTypeField.File, Id = workflowFileId }}), folder: new StartWorkflowRequestBodyFolderField() { Type = StartWorkflowRequestBodyFolderTypeField.Folder, Id = workflowFolderId }) { Type = StartWorkflowRequestBodyTypeField.WorkflowParameters });
```

### Arguments

- workflowId `string`
  - The ID of the workflow. Example: "12345"
- requestBody `StartWorkflowRequestBody`
  - Request body of startWorkflow method
- headers `StartWorkflowHeaders`
  - Headers of startWorkflow method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `null`.

Starts the workflow.


