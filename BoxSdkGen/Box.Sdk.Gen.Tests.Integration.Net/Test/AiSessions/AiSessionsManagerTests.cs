using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class AiSessionsManagerTests {
        public BoxClient client { get; }

        public AiSessionsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestAiSessionsWithNonExistentIdV2026R0() {
            const string aiSessionId = "123456";
            AiSessionsV2026R0 aiSessions = await client.AiSessions.GetAiSessionsV2026R0Async(queryParams: new GetAiSessionsV2026R0QueryParams(source: GetAiSessionsV2026R0QueryParamsSourceField.Api));
            Assert.IsTrue(aiSessions.Entries.Count >= 0);
            await Assert.That.IsExceptionAsync(async() => await client.AiSessions.DeleteAiSessionByIdV2026R0Async(aiSessionId: aiSessionId));
        }

    }
}