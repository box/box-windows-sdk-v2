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
            Assert.AreEqual(ATTR4, updatedMD["attr1"], "Failed to update metadata on file");
            Assert.AreEqual("value2", updatedMD["attr4"], "Failed to update metadata on file");

            //get all file metadata
            var allMD = await _client.MetadataManager.GetAllFileMetadataTemplatesAsync(FILE_ID);
            Assert.AreEqual(1, allMD.Entries.Count, "Failed to get all file metadata");

            //delete metadata
            var result = await _client.MetadataManager.DeleteFileMetadataAsync(FILE_ID, SCOPE, TEMPLATE_KEY);
            Assert.IsTrue(result, "Failed to delete file metadata");
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
            Assert.AreEqual(ATTR4, updatedMD["attr1"], "Failed to update metadata on folder");
            Assert.AreEqual("value2", updatedMD["attr4"], "Failed to update metadata on folder");

            //get all folder metadata
            var allMD = await _client.MetadataManager.GetAllFolderMetadataTemplatesAsync(FOLDER_ID);
            Assert.AreEqual(1, allMD.Entries.Count, "Failed to get all folder metadata");

            //delete metadata
            var result = await _client.MetadataManager.DeleteFolderMetadataAsync(FOLDER_ID, SCOPE, TEMPLATE_KEY);
            Assert.IsTrue(result, "Failed to delete folder metadata");
        }

        [TestMethod]
        public async Task Metadata_GetAllEnterprise_LiveSession()
        {
            var templates = await _client.MetadataManager.GetEnterpriseMetadataAsync();
            Assert.IsTrue(templates.Entries.Count >1 , "Failed to get enterprise metadata templates");
            Assert.IsNotNull(templates.Entries.Single(t=> t.TemplateKey == TEMPLATE_KEY ), "Failed to get enterprise metadata templates");
        }

        [TestMethod]
        public async Task Metadata_GetTemplate_LiveSession()
        {
            var template = await _client.MetadataManager.GetMetadataTemplate(SCOPE, TEMPLATE_KEY);
            Assert.AreEqual(TEMPLATE_KEY, template.TemplateKey, "Failed to get metadata template");
            Assert.AreEqual(4, template.Fields.Count, "Failed to get metadata template");
        }

        // This test is disabled because our test account has hit the maximum number of metadata templates (50).
        // Until we can figure out how to delete some templates or increase the limit this test will fail.
        //[TestMethod]
        public async Task Metadata_CreateTemplate_LiveSession()
        {
            var templateKey = "template-" + Guid.NewGuid().ToString().Replace("-","").Substring(0,8);
            var createdTemplate = await createTestTemplate(templateKey);

            Assert.AreEqual(templateKey, createdTemplate.TemplateKey, "Failed to create metadata template");
            Assert.AreEqual(4, createdTemplate.Fields.Count, "Failed to create metadata template");
            Assert.AreEqual("string", createdTemplate.Fields.First(f => f.Key == "attr1").Type, "Failed to create metadata template");
            Assert.IsTrue(createdTemplate.Fields.First(f => f.Key == "attr1").Hidden.Value, "Failed to create metadata template");
            Assert.AreEqual("float", createdTemplate.Fields.First(f => f.Key == "attr2").Type, "Failed to create metadata template");
            Assert.IsFalse(createdTemplate.Fields.First(f => f.Key == "attr2").Hidden.Value, "Failed to create metadata template");
            Assert.AreEqual("date", createdTemplate.Fields.First(f => f.Key == "attr3").Type, "Failed to create metadata template");
            Assert.AreEqual("enum", createdTemplate.Fields.First(f => f.Key == "attr4").Type, "Failed to create metadata template");
            Assert.AreEqual(2, createdTemplate.Fields.First(f => f.Key == "attr4").Options.Count, "Failed to create metadata template");
            Assert.IsTrue(createdTemplate.Hidden.Value, "Failed to create metadata template");

        }

        // This test is disabled because our test account has hit the maximum number of metadata templates (50).
        // Until we can figure out how to delete some templates or increase the limit this test will fail.
        //[TestMethod]
        public async Task Metadata_UpdateTemplate_LiveSession()
        {
            var templateKey = "template-" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
            var createdTemplate = await createTestTemplate(templateKey);

            //addField operation
            var newField = new BoxMetadataTemplateField() { Key = "attr5", DisplayName = "a string", Type = "string" };
            var update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.addField, Data = newField };
            var updates = new List<BoxMetadataTemplateUpdate>() { update };
            var updatedTemplate = await _client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.Fields.Any(f => f.Key == "attr5"), "addField operation failed on metadata update");

            //editField operation
            var fieldUpdate = new BoxMetadataTemplateField() { DisplayName = "a string edited"};
            update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.editField, FieldKey="attr1", Data = fieldUpdate };
            updates = new List<BoxMetadataTemplateUpdate>() { update };
            updatedTemplate = await _client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.Fields.Single(f => f.Key == "attr1").DisplayName == "a string edited", "editField operation failed on metadata update");

            //editTemplate operation
            var templateUpdate = new BoxMetadataTemplate() { DisplayName = "new display name" };
            update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.editTemplate, Data = templateUpdate };
            updates = new List<BoxMetadataTemplateUpdate>() { update };
            updatedTemplate = await _client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.DisplayName == "new display name", "editTemplate operation failed on metadata update");

            //addEnumOption operation
            var newValue = new BoxMetadataTemplateFieldOption() { Key = "value3" };
            update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.addEnumOption, FieldKey = "attr4", Data = newValue };
            updates = new List<BoxMetadataTemplateUpdate>() { update };
            updatedTemplate = await _client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.Fields.Single(f => f.Key == "attr4").Options.Count == 3, "addEnumOption operation failed on metadata update");

            //reorderEnumOptions operation
            var newValueOrder = new List<string>() { "value2", "value1", "value3" };
            update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.reorderEnumOptions, FieldKey = "attr4", EnumOptionKeys = newValueOrder };
            updates = new List<BoxMetadataTemplateUpdate>() { update };
            updatedTemplate = await _client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.Fields.Single(f => f.Key == "attr4").Options[0].Key == "value2", "reorderEnumOptions operation failed on metadata update");

            //reorderFields operation
            var newFieldOrder = new List<string>() { "attr5", "attr4", "attr3", "attr2", "attr1" };
            update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.reorderFields, FieldKeys=newFieldOrder };
            updates = new List<BoxMetadataTemplateUpdate>() { update };
            updatedTemplate = await _client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.Fields[0].Key == "attr5", "reorderFields operation failed on metadata update");

        }

        private async Task<BoxMetadataTemplate> createTestTemplate(string templateKey)
        {
            var field1 = new BoxMetadataTemplateField() { Key = "attr1", DisplayName = "a string", Type = "string", Hidden = true };
            var field2 = new BoxMetadataTemplateField() { Key = "attr2", DisplayName = "a float", Type = "float" };
            var field3 = new BoxMetadataTemplateField() { Key = "attr3", DisplayName = "a date", Type = "date" };
            var options = new List<BoxMetadataTemplateFieldOption>() { new BoxMetadataTemplateFieldOption() { Key = "value1" }, new BoxMetadataTemplateFieldOption() { Key = "value2" } };
            var field4 = new BoxMetadataTemplateField() { Key = "attr4", DisplayName = "a enum", Type = "enum", Options = options };
            var fields = new List<BoxMetadataTemplateField>() { field1, field2, field3, field4 };
            var templateToCreate = new BoxMetadataTemplate() { TemplateKey = templateKey, DisplayName = templateKey, Fields = fields, Hidden = true, Scope = SCOPE };
            var createdTemplate = await _client.MetadataManager.CreateMetadataTemplate(templateToCreate);
            return createdTemplate;
        }
    }
}
