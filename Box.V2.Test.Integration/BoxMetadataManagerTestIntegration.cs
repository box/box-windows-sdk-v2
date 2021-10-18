using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Exceptions;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxMetadataManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        private const string TEMPLATE_KEY = "testtemplate";
        private const string SCOPE = "enterprise";
        private const string ATTR1 = "blah";
        private const int ATTR2 = 5;
        private readonly DateTimeOffset _attr3 = new DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero);
        private const string ATTR4 = "value1";

        [TestMethod]
        public async Task Metadata_File_CRUD_LiveSession()
        {
            const string FILE_ID = "16894937607";

            //create metadata
            var md = new Dictionary<string, object>() {
                                                        { "attr1", ATTR1 },
                                                        { "attr2", ATTR2 },
                                                        { "attr3", _attr3 },
                                                        { "attr4", ATTR4 }
                                                      };
            var createdMD = await Client.MetadataManager.CreateFileMetadataAsync(FILE_ID, md, SCOPE, TEMPLATE_KEY);
            Assert.IsTrue(createdMD.Keys.Contains("attr1"), "Failed to correctly create file metadata");

            //get metadata
            var fetchedMD = await Client.MetadataManager.GetFileMetadataAsync(FILE_ID, SCOPE, TEMPLATE_KEY);
            Assert.IsTrue(fetchedMD.Keys.Contains("attr1"), "Failed to correctly fetch file metadata");

            //update metadata
            var update = new BoxMetadataUpdate() { Op = MetadataUpdateOp.copy, Path = "/attr1", From = "/attr4" };
            var update2 = new BoxMetadataUpdate() { Op = MetadataUpdateOp.replace, Path = "/attr4", Value = "value2" };
            var update3 = new BoxMetadataUpdate() { Op = MetadataUpdateOp.replace, Path = "/attr2", Value = 2 }; // Int update
            var updatedMD = await Client.MetadataManager.UpdateFileMetadataAsync(FILE_ID, new List<BoxMetadataUpdate>() { update, update2, update3 }, SCOPE, TEMPLATE_KEY);
            Assert.AreEqual(ATTR4, updatedMD["attr1"], "Failed to update metadata on file");
            Assert.AreEqual("value2", updatedMD["attr4"], "Failed to update metadata on file");
            Assert.AreEqual(Convert.ToInt64(2), updatedMD["attr2"], "Failed to update metadata on file");

            //get all file metadata
            var allMD = await Client.MetadataManager.GetAllFileMetadataTemplatesAsync(FILE_ID);
            Assert.AreEqual(1, allMD.Entries.Count, "Failed to get all file metadata");

            //delete metadata
            var result = await Client.MetadataManager.DeleteFileMetadataAsync(FILE_ID, SCOPE, TEMPLATE_KEY);
            Assert.IsTrue(result, "Failed to delete file metadata");
        }

        [TestMethod]
        public async Task Metadata_DeleteTemplate_LiveSession()
        {
            var templateKey = "testtemplate";
            var displayName = "Test Template";
            var scope = "enterprise";

            var templateToCreate = new BoxMetadataTemplate() { TemplateKey = templateKey, DisplayName = displayName, Fields = null, Hidden = true, Scope = scope };
            await Client.MetadataManager.CreateMetadataTemplate(templateToCreate);

            var templateIsDeleted = await Client.MetadataManager.DeleteMetadataTemplate(scope, templateKey);

            try
            {
                await Client.MetadataManager.GetMetadataTemplate(scope, templateKey);
            }
            catch (BoxException e)
            {
                Assert.IsNotNull(e);
            }
            Assert.IsTrue(templateIsDeleted, "Failed to delete metadata template");
        }

        [TestMethod]
        public async Task Metadata_Folder_CRUD_LiveSession()
        {
            const string FOLDER_ID = "1927308583";

            //create metadata
            var md = new Dictionary<string, object>() {
                                                        { "attr1", ATTR1 },
                                                        { "attr2", ATTR2 },
                                                        { "attr3", _attr3 },
                                                        { "attr4", ATTR4 }
                                                      };
            var createdMD = await Client.MetadataManager.CreateFolderMetadataAsync(FOLDER_ID, md, SCOPE, TEMPLATE_KEY);
            Assert.IsTrue(createdMD.Keys.Contains("attr1"), "Failed to correctly create folder metadata");

            //get metadata
            var fetchedMD = await Client.MetadataManager.GetFolderMetadataAsync(FOLDER_ID, SCOPE, TEMPLATE_KEY);
            Assert.IsTrue(fetchedMD.Keys.Contains("attr1"), "Failed to correctly fetch folder metadata");

            //update metadata
            var update = new BoxMetadataUpdate() { Op = MetadataUpdateOp.copy, Path = "/attr1", From = "/attr4" };
            var update2 = new BoxMetadataUpdate() { Op = MetadataUpdateOp.replace, Path = "/attr4", Value = "value2" };
            var updatedMD = await Client.MetadataManager.UpdateFolderMetadataAsync(FOLDER_ID, new List<BoxMetadataUpdate>() { update, update2 }, SCOPE, TEMPLATE_KEY);
            Assert.AreEqual(ATTR4, updatedMD["attr1"], "Failed to update metadata on folder");
            Assert.AreEqual("value2", updatedMD["attr4"], "Failed to update metadata on folder");

            //get all folder metadata
            var allMD = await Client.MetadataManager.GetAllFolderMetadataTemplatesAsync(FOLDER_ID);
            Assert.AreEqual(1, allMD.Entries.Count, "Failed to get all folder metadata");

            //delete metadata
            var result = await Client.MetadataManager.DeleteFolderMetadataAsync(FOLDER_ID, SCOPE, TEMPLATE_KEY);
            Assert.IsTrue(result, "Failed to delete folder metadata");
        }

        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public async Task Metadata_GetAllEnterprise_LiveSession()
        {
            var templates = await Client.MetadataManager.GetEnterpriseMetadataAsync();
            Assert.IsTrue(templates.Entries.Count > 1, "Failed to get enterprise metadata templates");
            Assert.IsNotNull(templates.Entries.Single(t => t.TemplateKey == TEMPLATE_KEY), "Failed to get enterprise metadata templates");
        }

        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public async Task Metadata_GetTemplate_LiveSession()
        {
            var template = await Client.MetadataManager.GetMetadataTemplate(SCOPE, TEMPLATE_KEY);
            Assert.AreEqual(TEMPLATE_KEY, template.TemplateKey, "Failed to get metadata template");
            Assert.AreEqual(4, template.Fields.Count, "Failed to get metadata template");
        }


        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public async Task Metadata_GetTemplateById_LiveSesion()
        {
            var templates = await Client.MetadataManager.GetEnterpriseMetadataAsync();
            var metadataTemplate = await Client.MetadataManager.GetMetadataTemplateById(templates.Entries[0].Id);
            Assert.IsNotNull(metadataTemplate);
            Assert.AreEqual(metadataTemplate.Type, "metadata_template");
        }

        [TestMethod]
        public async Task Metadata_ExecuteQueryAsync_LiveSession()
        {
            var folderName = ".Net Metadata Query Integration Test";
            var metadataTemplateScope = "enterprise";
            var metadataTemplateName = "testMetadataQueryTemplate";
            var metadataTemplateField = "testField";
            var metadataTemplateFieldValue = "testValue";

            // Check that test metadata template exists or create if not there
            BoxMetadataTemplate template;
            try
            {
                template = await Client.MetadataManager.GetMetadataTemplate(metadataTemplateScope, metadataTemplateName);
            }
            catch (BoxException)
            {
                var templateParams = new BoxMetadataTemplate()
                {
                    TemplateKey = metadataTemplateName,
                    DisplayName = "Test Metadata Query Template",
                    Scope = metadataTemplateScope,
                    Fields = new List<BoxMetadataTemplateField>()
                    {
                        new BoxMetadataTemplateField()
                        {
                            Type = "string",
                            Key = metadataTemplateField,
                            DisplayName = "Test Field"
                        }
                    }
                };
                template = await Client.MetadataManager.CreateMetadataTemplate(templateParams);
            }

            // Create folder and apply test metadata template. If folder is already there, assume that the folder has the correct metadata template from a previous integration test that might not have been able to delete the folder.
            BoxFolder folder = null;
            try
            {
                var folderParams = new BoxFolderRequest()
                {
                    Name = folderName,
                    Parent = new BoxRequestEntity()
                    {
                        Id = "0"
                    }
                };
                folder = await Client.FoldersManager.CreateAsync(folderParams);
                var metadataValues = new Dictionary<string, object>()
                {
                    { metadataTemplateField, metadataTemplateFieldValue }
                };
                Dictionary<string, object> metadata = await Client.MetadataManager.SetFolderMetadataAsync(folder.Id, metadataValues, template.Scope, template.TemplateKey);
            }
            catch { }

            /*** Act ***/
            var from = template.Scope + "." + template.TemplateKey;
            var query = metadataTemplateField + " = :arg";
            var queryParams = new Dictionary<string, object>
            {
                { "arg", metadataTemplateFieldValue }
            };
            var orderByList = new List<BoxMetadataQueryOrderBy>();
            var orderBy = new BoxMetadataQueryOrderBy()
            {
                FieldKey = metadataTemplateField,
                Direction = BoxSortDirection.DESC
            };
            orderByList.Add(orderBy);
            // Run metadata query
            BoxCollectionMarkerBased<BoxMetadataQueryItem> items = await Client.MetadataManager.ExecuteMetadataQueryAsync(from: from, query: query, queryParameters: queryParams, orderBy: orderByList, ancestorFolderId: "0", autoPaginate: false);
            // Delete folder if this test created a folder
            if (folder != null)
            {
                await Client.FoldersManager.DeleteAsync(folder.Id, recursive: true);
            }
            /*** Assert ***/
            Assert.AreEqual(items.Entries.Count, 1);
            Assert.AreEqual(items.Entries[0].Item.Name, folderName);
        }

        [TestMethod]
        public async Task Metadata_ExecuteQueryWithFieldsAsync_LiveSession()
        {
            var folderName = ".Net Metadata Query Integration Test";
            var metadataTemplateScope = "enterprise";
            var metadataTemplateName = "testMetadataQueryTemplate";
            var metadataTemplateField = "testField";
            var metadataTemplateFieldValue = "testValue";

            // Check that test metadata template exists or create if not there
            BoxMetadataTemplate template;
            try
            {
                template = await Client.MetadataManager.GetMetadataTemplate(metadataTemplateScope, metadataTemplateName);
            }
            catch (BoxException)
            {
                var templateParams = new BoxMetadataTemplate()
                {
                    TemplateKey = metadataTemplateName,
                    DisplayName = "Test Metadata Query Template",
                    Scope = metadataTemplateScope,
                    Fields = new List<BoxMetadataTemplateField>()
                    {
                        new BoxMetadataTemplateField()
                        {
                            Type = "string",
                            Key = metadataTemplateField,
                            DisplayName = "Test Field"
                        }
                    }
                };
                template = await Client.MetadataManager.CreateMetadataTemplate(templateParams);
            }

            // Create folder and apply test metadata template. If folder is already there, assume that the folder has the correct metadata template from a previous integration test that might not have been able to delete the folder.
            BoxFolder folder = null;
            try
            {
                var folderParams = new BoxFolderRequest()
                {
                    Name = folderName,
                    Parent = new BoxRequestEntity()
                    {
                        Id = "0"
                    }
                };
                folder = await Client.FoldersManager.CreateAsync(folderParams);
                var metadataValues = new Dictionary<string, object>()
                {
                    { metadataTemplateField, metadataTemplateFieldValue }
                };
                Dictionary<string, object> metadata = await Client.MetadataManager.SetFolderMetadataAsync(folder.Id, metadataValues, template.Scope, template.TemplateKey);
            }
            catch { }

            /*** Act ***/
            var from = template.Scope + "." + template.TemplateKey;
            var query = metadataTemplateField + " = :arg";
            var fields = new List<string>
            {
                "type",
                "id",
                "name",
                "metadata." + template.Scope + "." + template.TemplateKey + "." + metadataTemplateField
            };
            var queryParams = new Dictionary<string, object>
            {
                { "arg", metadataTemplateFieldValue }
            };
            var orderByList = new List<BoxMetadataQueryOrderBy>();
            var orderBy = new BoxMetadataQueryOrderBy()
            {
                FieldKey = metadataTemplateField,
                Direction = BoxSortDirection.DESC
            };
            orderByList.Add(orderBy);
            // Run metadata query
            BoxCollectionMarkerBased<BoxItem> items = await Client.MetadataManager.ExecuteMetadataQueryAsync(from, query: query, fields: fields, queryParameters: queryParams, orderBy: orderByList, ancestorFolderId: "0", autoPaginate: false);
            // Delete folder if this test created a folder
            if (folder != null)
            {
                await Client.FoldersManager.DeleteAsync(folder.Id, recursive: true);
            }
            /*** Assert ***/
            Assert.AreEqual(items.Entries.Count, 1);
            Assert.AreEqual(items.Entries[0].Name, folderName);
            var folderItem = (BoxFolder)items.Entries[0];
            Assert.IsNotNull(folderItem.Metadata);
        }

        // This test is disabled because our test account has hit the maximum number of metadata templates (50).
        // Until we can figure out how to delete some templates or increase the limit this test will fail.
        //[TestMethod]
        public async Task Metadata_CreateTemplate_LiveSession()
        {
            var templateKey = "template-" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
            var createdTemplate = await CreateTestTemplate(templateKey);

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
        [TestMethod]
        // [TestCategory("CI-APP-USER")]
        public async Task Metadata_UpdateTemplate_LiveSession()
        {
            /*
            if (!IsUnderCI())
            {
                return;
            }
            */

            // var templateKey = "template-" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
            // var createdTemplate = await createTestTemplate(templateKey);

            var templateKey = "template-fbc81a44";

            //addField operation
            var newField = new BoxMetadataTemplateField() { Key = "attr5", DisplayName = "a string", Type = "string" };
            var update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.addField, Data = newField };
            var updates = new List<BoxMetadataTemplateUpdate>() { update };
            var updatedTemplate = await Client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.Fields.Any(f => f.Key == "attr5"), "addField operation failed on metadata update");

            //editField operation
            var fieldUpdate = new BoxMetadataTemplateField() { DisplayName = "a string edited" };
            update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.editField, FieldKey = "attr1", Data = fieldUpdate };
            updates = new List<BoxMetadataTemplateUpdate>() { update };
            updatedTemplate = await Client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.Fields.Single(f => f.Key == "attr1").DisplayName == "a string edited", "editField operation failed on metadata update");

            //editTemplate operation
            var templateUpdate = new BoxMetadataTemplate() { DisplayName = "new display name" };
            update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.editTemplate, Data = templateUpdate };
            updates = new List<BoxMetadataTemplateUpdate>() { update };
            updatedTemplate = await Client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.DisplayName == "new display name", "editTemplate operation failed on metadata update");

            //addEnumOption operation
            var newValue = new BoxMetadataTemplateFieldOption() { Key = "value3" };
            update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.addEnumOption, FieldKey = "attr4", Data = newValue };
            updates = new List<BoxMetadataTemplateUpdate>() { update };
            updatedTemplate = await Client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.Fields.Single(f => f.Key == "attr4").Options.Count == 3, "addEnumOption operation failed on metadata update");

            //reorderEnumOptions operation
            var newValueOrder = new List<string>() { "value2", "value1", "value3" };
            update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.reorderEnumOptions, FieldKey = "attr4", EnumOptionKeys = newValueOrder };
            updates = new List<BoxMetadataTemplateUpdate>() { update };
            updatedTemplate = await Client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.Fields.Single(f => f.Key == "attr4").Options[0].Key == "value2", "reorderEnumOptions operation failed on metadata update");

            //editEnumOption operation
            newValue = new BoxMetadataTemplateFieldOption() { Key = "value31" };
            update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.editEnumOption, FieldKey = "attr4", EnumOptionKey = "value3", Data = newValue };
            updates = new List<BoxMetadataTemplateUpdate>() { update };
            updatedTemplate = await Client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.Fields.Single(f => f.Key == "attr4").Options.Count == 3, "editEnumOption operation failed on metadata update");

            //removeEnumOption operation
            update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.removeEnumOption, FieldKey = "attr4", EnumOptionKey = "value31" };
            updates = new List<BoxMetadataTemplateUpdate>() { update };
            updatedTemplate = await Client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.Fields.Single(f => f.Key == "attr4").Options.Count == 2, "removeEnumOption operation failed on metadata update");

            //reorderFields operation
            var newFieldOrder = new List<string>() { "attr5", "attr4", "attr3", "attr2", "attr1" };
            update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.reorderFields, FieldKeys = newFieldOrder };
            updates = new List<BoxMetadataTemplateUpdate>() { update };
            updatedTemplate = await Client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsTrue(updatedTemplate.Fields[0].Key == "attr5", "reorderFields operation failed on metadata update");

            //removeField operation
            update = new BoxMetadataTemplateUpdate() { Op = MetadataTemplateUpdateOp.removeField, FieldKey = "attr5" };
            updates = new List<BoxMetadataTemplateUpdate>() { update };
            updatedTemplate = await Client.MetadataManager.UpdateMetadataTemplate(updates, "enterprise", templateKey);
            Assert.IsFalse(updatedTemplate.Fields.Any(f => f.Key == "attr5"), "removeField operation failed on metadata update");
        }

        private async Task<BoxMetadataTemplate> CreateTestTemplate(string templateKey)
        {
            var field1 = new BoxMetadataTemplateField() { Key = "attr1", DisplayName = "a string", Type = "string", Hidden = true };
            var field2 = new BoxMetadataTemplateField() { Key = "attr2", DisplayName = "a float", Type = "float" };
            var field3 = new BoxMetadataTemplateField() { Key = "attr3", DisplayName = "a date", Type = "date" };
            var options = new List<BoxMetadataTemplateFieldOption>() { new BoxMetadataTemplateFieldOption() { Key = "value1" }, new BoxMetadataTemplateFieldOption() { Key = "value2" } };
            var field4 = new BoxMetadataTemplateField() { Key = "attr4", DisplayName = "a enum", Type = "enum", Options = options };
            var fields = new List<BoxMetadataTemplateField>() { field1, field2, field3, field4 };
            var templateToCreate = new BoxMetadataTemplate() { TemplateKey = templateKey, DisplayName = templateKey, Fields = fields, Hidden = true, Scope = SCOPE };
            var createdTemplate = await Client.MetadataManager.CreateMetadataTemplate(templateToCreate);
            return createdTemplate;
        }
    }
}
