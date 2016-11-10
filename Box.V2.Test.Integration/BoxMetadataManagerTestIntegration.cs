using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxMetadataManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        const string TEMPLATE_KEY = "testtemplate";
        const string SCOPE = "enterprise";
        const string ATTR1 = "blah";
        const int ATTR2 = 5;
        readonly DateTime ATTR3 = new DateTime(2015, 1, 1);
        const string ATTR4 = "value1";

        [TestMethod]
        public async Task Metadata_File_CRUD_LiveSession()
        {
            const string FILE_ID = "16894937607";

            //create metadata
            var md = new Dictionary<string, object>() {
                                                        { "attr1", ATTR1 },
                                                        { "attr2", ATTR2 },
                                                        { "attr3", ATTR3 },
                                                        { "attr4", ATTR4 }
                                                      };
            var createdMD = await _client.MetadataManager.CreateFileMetadataAsync(FILE_ID, md, SCOPE, TEMPLATE_KEY);
            Assert.IsTrue(createdMD.Keys.Contains("attr1"), "Failed to correctly create file metadata");

            //get metadata
            var fetchedMD = await _client.MetadataManager.GetFileMetadataAsync(FILE_ID, SCOPE, TEMPLATE_KEY);
            Assert.IsTrue(fetchedMD.Keys.Contains("attr1"), "Failed to correctly fetch file metadata");

            //update metadata
            var update = new BoxMetadataUpdate() { Op = MetadataUpdateOp.copy, Path = "/attr1", From = "/attr4" };
            var update2 = new BoxMetadataUpdate() { Op = MetadataUpdateOp.replace, Path = "/attr4", Value = "value2" };
            var updatedMD = await _client.MetadataManager.UpdateFileMetadataAsync(FILE_ID, new List<BoxMetadataUpdate>() { update, update2 }, SCOPE, TEMPLATE_KEY);
            Assert.AreEqual(updatedMD["attr1"], ATTR4, "Failed to update metadata on file");
            Assert.AreEqual(updatedMD["attr4"], "value2", "Failed to update metadata on file");

            //get all file metadata
            var allMD = await _client.MetadataManager.GetAllFileMetadataTemplatesAsync(FILE_ID);
            Assert.AreEqual(allMD.Entries.Count, 1, "Failed to get all file metadata");

            //delete metadata
            var result = await _client.MetadataManager.DeleteFileMetadataAsync(FILE_ID, SCOPE, TEMPLATE_KEY);

        }

        [TestMethod]
        public async Task Metadata_Folder_CRUD_LiveSession()
        {
            const string FOLDER_ID = "1927308583";

            //create metadata
            var md = new Dictionary<string, object>() {
                                                        { "attr1", ATTR1 },
                                                        { "attr2", ATTR2 },
                                                        { "attr3", ATTR3 },
                                                        { "attr4", ATTR4 }
                                                      };
            var createdMD = await _client.MetadataManager.CreateFolderMetadataAsync(FOLDER_ID, md, SCOPE, TEMPLATE_KEY);
            Assert.IsTrue(createdMD.Keys.Contains("attr1"), "Failed to correctly create folder metadata");

            //get metadata
            var fetchedMD = await _client.MetadataManager.GetFolderMetadataAsync(FOLDER_ID, SCOPE, TEMPLATE_KEY);
            Assert.IsTrue(fetchedMD.Keys.Contains("attr1"), "Failed to correctly fetch folder metadata");

            //update metadata
            var update = new BoxMetadataUpdate() { Op = MetadataUpdateOp.copy, Path = "/attr1", From = "/attr4" };
            var update2 = new BoxMetadataUpdate() { Op = MetadataUpdateOp.replace, Path = "/attr4", Value = "value2" };
            var updatedMD = await _client.MetadataManager.UpdateFolderMetadataAsync(FOLDER_ID, new List<BoxMetadataUpdate>() { update, update2 }, SCOPE, TEMPLATE_KEY);
            Assert.AreEqual(updatedMD["attr1"], ATTR4, "Failed to update metadata on folder");
            Assert.AreEqual(updatedMD["attr4"], "value2", "Failed to update metadata on folder");

            //get all folder metadata
            var allMD = await _client.MetadataManager.GetAllFolderMetadataTemplatesAsync(FOLDER_ID);
            Assert.AreEqual(allMD.Entries.Count, 1, "Failed to get all folder metadata");

            //delete metadata
            var result = await _client.MetadataManager.DeleteFolderMetadataAsync(FOLDER_ID, SCOPE, TEMPLATE_KEY);

        }

        [TestMethod]
        public async Task Metadata_GetAllEnterprise_LiveSession()
        {
            var templates = await _client.MetadataManager.GetEnterpriseMetadataAsync();
            Assert.AreEqual(templates.Entries.Count, 1, "Failed to get enterprise metadata templates");
            Assert.AreEqual(templates.Entries.First().TemplateKey, TEMPLATE_KEY, "Failed to get enterprise metadata templates");
        }

        [TestMethod]
        public async Task Metadata_GetTemplate_LiveSession()
        {
            var template = await _client.MetadataManager.GetMetadataTemplate(SCOPE, TEMPLATE_KEY);
            Assert.AreEqual(template.TemplateKey, TEMPLATE_KEY, "Failed to get metadata template");
            Assert.AreEqual(template.Fields.Count, 4, "Failed to get metadata template");
        }
    }
}
