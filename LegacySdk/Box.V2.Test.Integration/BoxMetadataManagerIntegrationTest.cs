using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Test.Integration.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
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

        [TestMethod]
        public async Task GetEnterpriseMetadataAsync_ForExistingMetadataTemplate_ShouldReturnThisMetadataTemplate()
        {
            var metadataTemplate = await CreateMetadataTemplate();

            var templates = await AdminClient.MetadataManager.GetEnterpriseMetadataAsync();

            Assert.IsTrue(templates.Entries.Any(x => x.TemplateKey == metadataTemplate.TemplateKey));
        }

        [TestMethod]
        public async Task GetMetadataTemplate_ForExistingMetadataTemplate_ShouldReturnThisMetadataTemplate()
        {
            var metadataTemplate = await CreateMetadataTemplate();

            var template = await AdminClient.MetadataManager.GetMetadataTemplate("enterprise", metadataTemplate.TemplateKey);

            Assert.AreEqual(template.TemplateKey, metadataTemplate.TemplateKey);
        }


        [TestMethod]
        public async Task GetMetadataTemplateById_ForExistingMetadataTemplate_ShouldReturnThisMetadataTemplate()
        {
            var metadataTemplate = await CreateMetadataTemplate();

            var fetchedMetadataTemplate = await AdminClient.MetadataManager.GetMetadataTemplateById(metadataTemplate.Id);

            Assert.AreEqual(fetchedMetadataTemplate.TemplateKey, metadataTemplate.TemplateKey);
        }
    }
}
