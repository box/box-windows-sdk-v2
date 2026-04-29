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
    public class AutomateWorkflowsManagerTests {
        public BoxClient client { get; }

        public AutomateWorkflowsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestAutomateWorkflows() {
            BoxClient adminClient = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
            string workflowFolderId = Utils.GetEnvVar(name: "AUTOMATE_WORKFLOW_FOLDER_ID");
            Files uploadedFiles = await adminClient.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: Utils.GetUUID(), parent: new UploadFileRequestBodyAttributesParentField(id: workflowFolderId)), file: Utils.GenerateByteStream(size: 1024 * 1024)));
            FileFull file = NullableUtils.Unwrap(uploadedFiles.Entries)[0];
            string workflowFileId = file.Id;
            AutomateWorkflowsV2026R0 automateWorkflows = await adminClient.AutomateWorkflows.GetAutomateWorkflowsV2026R0Async(queryParams: new GetAutomateWorkflowsV2026R0QueryParams(folderId: workflowFolderId));
            Assert.IsTrue(NullableUtils.Unwrap(automateWorkflows.Entries).Count == 1);
            AutomateWorkflowActionV2026R0 workflowAction = NullableUtils.Unwrap(automateWorkflows.Entries)[0];
            Assert.IsTrue(StringUtils.ToStringRepresentation(workflowAction.Type?.Value) == "workflow_action");
            Assert.IsTrue(StringUtils.ToStringRepresentation(workflowAction.ActionType) == "run_workflow");
            Assert.IsTrue(StringUtils.ToStringRepresentation(workflowAction.Workflow.Type?.Value) == "workflow");
            await adminClient.AutomateWorkflows.CreateAutomateWorkflowStartV2026R0Async(workflowId: workflowAction.Workflow.Id, requestBody: new AutomateWorkflowStartRequestV2026R0(workflowActionId: workflowAction.Id, fileIds: Array.AsReadOnly(new [] {workflowFileId})));
        }

    }
}