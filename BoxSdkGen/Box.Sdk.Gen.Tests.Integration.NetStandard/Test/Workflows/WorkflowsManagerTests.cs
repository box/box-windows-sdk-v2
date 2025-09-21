using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class WorkflowsManagerTests {
        public BoxClient client { get; }

        public WorkflowsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestWorkflows() {
            BoxClient adminClient = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
            string workflowFolderId = Utils.GetEnvVar(name: "WORKFLOW_FOLDER_ID");
            Files uploadedFiles = await adminClient.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: Utils.GetUUID(), parent: new UploadFileRequestBodyAttributesParentField(id: workflowFolderId)), file: Utils.GenerateByteStream(size: 1024 * 1024)));
            FileFull file = NullableUtils.Unwrap(uploadedFiles.Entries)[0];
            string workflowFileId = file.Id;
            Workflows workflows = await adminClient.Workflows.GetWorkflowsAsync(queryParams: new GetWorkflowsQueryParams(folderId: workflowFolderId));
            Assert.IsTrue(NullableUtils.Unwrap(workflows.Entries).Count == 1);
            Workflow workflowToRun = NullableUtils.Unwrap(workflows.Entries)[0];
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(workflowToRun.Type)) == "workflow");
            Assert.IsTrue(NullableUtils.Unwrap(workflowToRun.IsEnabled) == true);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(NullableUtils.Unwrap(workflowToRun.Flows)[0].Type)) == "flow");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(NullableUtils.Unwrap(NullableUtils.Unwrap(workflowToRun.Flows)[0].Trigger).Type)) == "trigger");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(NullableUtils.Unwrap(NullableUtils.Unwrap(workflowToRun.Flows)[0].Trigger).TriggerType)) == "WORKFLOW_MANUAL_START");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(NullableUtils.Unwrap(NullableUtils.Unwrap(workflowToRun.Flows)[0].Outcomes)[0].ActionType)) == "delete_file");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(NullableUtils.Unwrap(NullableUtils.Unwrap(workflowToRun.Flows)[0].Outcomes)[0].Type)) == "outcome");
            await adminClient.Workflows.StartWorkflowAsync(workflowId: NullableUtils.Unwrap(workflowToRun.Id), requestBody: new StartWorkflowRequestBody(flow: new StartWorkflowRequestBodyFlowField() { Type = "flow", Id = NullableUtils.Unwrap(NullableUtils.Unwrap(workflowToRun.Flows)[0].Id) }, files: Array.AsReadOnly(new [] {new StartWorkflowRequestBodyFilesField() { Type = StartWorkflowRequestBodyFilesTypeField.File, Id = workflowFileId }}), folder: new StartWorkflowRequestBodyFolderField() { Type = StartWorkflowRequestBodyFolderTypeField.Folder, Id = workflowFolderId }) { Type = StartWorkflowRequestBodyTypeField.WorkflowParameters });
        }

    }
}