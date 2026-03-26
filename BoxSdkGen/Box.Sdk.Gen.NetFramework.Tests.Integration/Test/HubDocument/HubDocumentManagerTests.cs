using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class HubDocumentManagerTests {
        public BoxClient client { get; }

        public HubDocumentManagerTests() {
            client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetHubDocumentPagesAndBlocks() {
            string hubTitle = Utils.GetUUID();
            HubV2025R0 createdHub = await client.Hubs.CreateHubV2025R0Async(requestBody: new HubCreateRequestV2025R0(title: hubTitle));
            string hubId = createdHub.Id;
            HubDocumentPagesV2025R0 pages = await client.HubDocument.GetHubDocumentPagesV2025R0Async(queryParams: new GetHubDocumentPagesV2025R0QueryParams(hubId: hubId));
            Assert.IsTrue(pages.Entries.Count > 0);
            Assert.IsTrue(StringUtils.ToStringRepresentation(pages.Type?.Value) == "document_pages");
            HubDocumentPageV2025R0 firstPage = pages.Entries[0];
            Assert.IsTrue(StringUtils.ToStringRepresentation(firstPage.Type) == "page");
            string pageId = firstPage.Id;
            HubDocumentBlocksV2025R0 blocks = await client.HubDocument.GetHubDocumentBlocksV2025R0Async(queryParams: new GetHubDocumentBlocksV2025R0QueryParams(hubId: hubId, pageId: pageId));
            Assert.IsTrue(StringUtils.ToStringRepresentation(blocks.Type?.Value) == "document_blocks");
            Assert.IsTrue(blocks.Entries.Count > 0);
            await client.Hubs.DeleteHubByIdV2025R0Async(hubId: hubId);
        }

    }
}