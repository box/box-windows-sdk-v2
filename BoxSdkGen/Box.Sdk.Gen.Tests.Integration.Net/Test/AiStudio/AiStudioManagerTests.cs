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
    public class AiStudioManagerTests {
        public BoxClient client { get; }

        public AiStudioManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestAiStudioCrud() {
            string agentName = Utils.GetUUID();
            AiSingleAgentResponseFull createdAgent = await client.AiStudio.CreateAiAgentAsync(requestBody: new CreateAiAgent(name: agentName, accessState: "enabled") { Ask = new AiStudioAgentAsk(accessState: "enabled", description: "desc1") });
            Assert.IsTrue(createdAgent.Name == agentName);
            AiMultipleAgentResponse agents = await client.AiStudio.GetAiAgentsAsync();
            int numAgents = agents.Entries.Count;
            Assert.IsTrue(StringUtils.ToStringRepresentation(agents.Entries[0].Type?.Value) == "ai_agent");
            AiSingleAgentResponseFull retrievedAgent = await client.AiStudio.GetAiAgentByIdAsync(agentId: createdAgent.Id, queryParams: new GetAiAgentByIdQueryParams() { Fields = Array.AsReadOnly(new [] {"ask"}) });
            Assert.IsTrue(retrievedAgent.Name == agentName);
            Assert.IsTrue(StringUtils.ToStringRepresentation(retrievedAgent.AccessState) == "enabled");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(retrievedAgent.Ask).AccessState) == "enabled");
            Assert.IsTrue(NullableUtils.Unwrap(retrievedAgent.Ask).Description == "desc1");
            AiSingleAgentResponseFull updatedAgent = await client.AiStudio.UpdateAiAgentByIdAsync(agentId: createdAgent.Id, requestBody: new CreateAiAgent(name: agentName, accessState: "enabled") { Ask = new AiStudioAgentAsk(accessState: "disabled", description: "desc2") });
            Assert.IsTrue(StringUtils.ToStringRepresentation(updatedAgent.AccessState) == "enabled");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(updatedAgent.Ask).AccessState) == "disabled");
            Assert.IsTrue(NullableUtils.Unwrap(updatedAgent.Ask).Description == "desc2");
            await client.AiStudio.DeleteAiAgentByIdAsync(agentId: createdAgent.Id);
            AiMultipleAgentResponse agentsAfterDelete = await client.AiStudio.GetAiAgentsAsync();
            Assert.IsTrue(agentsAfterDelete.Entries.Count == numAgents - 1);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestUseAiAgentReferenceInAiAsk() {
            string agentName = Utils.GetUUID();
            AiSingleAgentResponseFull createdAgent = await client.AiStudio.CreateAiAgentAsync(requestBody: new CreateAiAgent(name: agentName, accessState: "enabled") { Ask = new AiStudioAgentAsk(accessState: "enabled", description: "desc1") });
            FileFull fileToAsk = await new CommonsManager().UploadNewFileAsync();
            AiResponseFull? response = await client.Ai.CreateAiAskAsync(requestBody: new AiAsk(mode: AiAskModeField.SingleItemQa, prompt: "which direction sun rises", items: Array.AsReadOnly(new [] {new AiItemAsk(id: fileToAsk.Id, type: AiItemAskTypeField.File) { Content = "Sun rises in the East" }})) { AiAgent = new AiAgentReference() { Id = createdAgent.Id } });
            Assert.IsTrue(NullableUtils.Unwrap(response).Answer.Contains("East"));
            Assert.IsTrue(NullableUtils.Unwrap(response).CompletionReason == "done");
            Assert.IsTrue(NullableUtils.Unwrap(NullableUtils.Unwrap(NullableUtils.Unwrap(response).AiAgentInfo).Models).Count > 0);
            await client.Files.DeleteFileByIdAsync(fileId: fileToAsk.Id);
            await client.AiStudio.DeleteAiAgentByIdAsync(agentId: createdAgent.Id);
        }

    }
}