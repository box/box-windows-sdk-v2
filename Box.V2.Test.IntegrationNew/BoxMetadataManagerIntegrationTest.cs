using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Test.IntegrationNew.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.IntegrationNew
{
    [TestClass]
    public class BoxMetadataManagerIntegrationTest : TestInFolder
    {
        [TestMethod]
        public async Task ExecuteMetadataQuery_ForSmallFile_ShouldBeAbleToFindThisFileByMetadata()
        {
            var adminFolder = await CreateFolderAsAdmin("0");
            var metadataFields = new Dictionary<string, object> { { "status", "active" } };
            var uploadedFileWithTemplateKey = await CreateSmallFileWithMetadata(adminFolder.Id, metadataFields);
            var templateKey = uploadedFileWithTemplateKey.Item2;

            var metadataQueryKey = $"enterprise_{EnterpriseId}.{templateKey}";
            var queryRequest = new BoxMetadataQueryRequest
            {
                AncestorFolderId = adminFolder.Id,
                From = metadataQueryKey,
                Fields = new List<string>() { $"metadata.{metadataQueryKey}.status" },
                Query = "status = :value",
                QueryParameters = new Dictionary<string, object>() { { "value", "active" } }
            };

            var response = await AdminClient.MetadataManager.ExecuteMetadataQueryAsync(queryRequest);

            Assert.AreEqual(response.Entries.Count, 1);
            Assert.AreEqual(((BoxFile)response.Entries[0]).Metadata[$"enterprise_{EnterpriseId}"][templateKey]["status"].Value, "active");
        }
    }
}
