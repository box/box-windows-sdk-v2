using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class FolderMetadataManagerTests {
        public BoxClient client { get; }

        public FolderMetadataManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestUpdatingFolderMetadata() {
            FolderFull folder = await new CommonsManager().CreateNewFolderAsync();
            string templateKey = string.Concat("key", Utils.GetUUID());
            MetadataTemplate template = await client.MetadataTemplates.CreateMetadataTemplateAsync(requestBody: new CreateMetadataTemplateRequestBody(scope: "enterprise", displayName: templateKey) { TemplateKey = templateKey, Fields = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.String, key: "name", displayName: "name"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Float, key: "age", displayName: "age"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Date, key: "birthDate", displayName: "birthDate"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Enum, key: "countryCode", displayName: "countryCode") { Options = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "US"),new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "CA")}) },new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.MultiSelect, key: "sports", displayName: "sports") { Options = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "basketball"),new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "football"),new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "tennis")}) }}) });
            MetadataFull createdMetadata = await client.FolderMetadata.CreateFolderMetadataByIdAsync(folderId: folder.Id, scope: CreateFolderMetadataByIdScope.Enterprise, templateKey: templateKey, requestBody: new Dictionary<string, object>() { { "name", "John" }, { "age", 23 }, { "birthDate", "2001-01-03T02:20:50.520Z" }, { "countryCode", "US" }, { "sports", Array.AsReadOnly(new [] {"basketball","tennis"}) } });
            MetadataFull updatedMetadata = await client.FolderMetadata.UpdateFolderMetadataByIdAsync(folderId: folder.Id, scope: UpdateFolderMetadataByIdScope.Enterprise, templateKey: templateKey, requestBody: Array.AsReadOnly(new [] {new UpdateFolderMetadataByIdRequestBody() { Op = UpdateFolderMetadataByIdRequestBodyOpField.Replace, Path = "/name", Value = "Jack" },new UpdateFolderMetadataByIdRequestBody() { Op = UpdateFolderMetadataByIdRequestBodyOpField.Replace, Path = "/age", Value = 24 },new UpdateFolderMetadataByIdRequestBody() { Op = UpdateFolderMetadataByIdRequestBodyOpField.Replace, Path = "/birthDate", Value = "2000-01-03T02:20:50.520Z" },new UpdateFolderMetadataByIdRequestBody() { Op = UpdateFolderMetadataByIdRequestBodyOpField.Replace, Path = "/countryCode", Value = "CA" },new UpdateFolderMetadataByIdRequestBody() { Op = UpdateFolderMetadataByIdRequestBodyOpField.Replace, Path = "/sports", Value = Array.AsReadOnly(new [] {"football"}) }}));
            Assert.IsTrue(StringUtils.ToStringRepresentation(updatedMetadata.Template) == templateKey);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(updatedMetadata.ExtraData)["name"]) == "Jack");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(updatedMetadata.ExtraData)["age"]) == "24");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(updatedMetadata.ExtraData)["birthDate"]) == "2000-01-03T02:20:50.520Z");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(updatedMetadata.ExtraData)["countryCode"]) == "CA");
            await client.FolderMetadata.DeleteFolderMetadataByIdAsync(folderId: folder.Id, scope: DeleteFolderMetadataByIdScope.Enterprise, templateKey: templateKey);
            await client.MetadataTemplates.DeleteMetadataTemplateAsync(scope: DeleteMetadataTemplateScope.Enterprise, templateKey: templateKey);
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestGlobalFolderMetadata() {
            FolderFull folder = await new CommonsManager().CreateNewFolderAsync();
            Metadatas folderMetadata = await client.FolderMetadata.GetFolderMetadataAsync(folderId: folder.Id);
            Assert.IsTrue(NullableUtils.Unwrap(folderMetadata.Entries).Count == 0);
            MetadataFull createdMetadata = await client.FolderMetadata.CreateFolderMetadataByIdAsync(folderId: folder.Id, scope: CreateFolderMetadataByIdScope.Global, templateKey: "properties", requestBody: new Dictionary<string, object>() { { "abc", "xyz" } });
            Assert.IsTrue(StringUtils.ToStringRepresentation(createdMetadata.Template) == "properties");
            Assert.IsTrue(StringUtils.ToStringRepresentation(createdMetadata.Scope) == "global");
            Assert.IsTrue(createdMetadata.Version == 0);
            MetadataFull receivedMetadata = await client.FolderMetadata.GetFolderMetadataByIdAsync(folderId: folder.Id, scope: GetFolderMetadataByIdScope.Global, templateKey: "properties");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(receivedMetadata.ExtraData)["abc"]) == "xyz");
            const string newValue = "bar";
            await client.FolderMetadata.UpdateFolderMetadataByIdAsync(folderId: folder.Id, scope: UpdateFolderMetadataByIdScope.Global, templateKey: "properties", requestBody: Array.AsReadOnly(new [] {new UpdateFolderMetadataByIdRequestBody() { Op = UpdateFolderMetadataByIdRequestBodyOpField.Replace, Path = "/abc", Value = newValue }}));
            MetadataFull receivedUpdatedMetadata = await client.FolderMetadata.GetFolderMetadataByIdAsync(folderId: folder.Id, scope: GetFolderMetadataByIdScope.Global, templateKey: "properties");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(receivedUpdatedMetadata.ExtraData)["abc"]) == newValue);
            await client.FolderMetadata.DeleteFolderMetadataByIdAsync(folderId: folder.Id, scope: DeleteFolderMetadataByIdScope.Global, templateKey: "properties");
            await Assert.That.IsExceptionAsync(async() => await client.FolderMetadata.GetFolderMetadataByIdAsync(folderId: folder.Id, scope: GetFolderMetadataByIdScope.Global, templateKey: "properties"));
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestEnterpriseFolderMetadata() {
            FolderFull folder = await new CommonsManager().CreateNewFolderAsync();
            string templateKey = string.Concat("key", Utils.GetUUID());
            MetadataTemplate template = await client.MetadataTemplates.CreateMetadataTemplateAsync(requestBody: new CreateMetadataTemplateRequestBody(scope: "enterprise", displayName: templateKey) { TemplateKey = templateKey, Fields = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.String, key: "name", displayName: "name"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Float, key: "age", displayName: "age"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Date, key: "birthDate", displayName: "birthDate"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Enum, key: "countryCode", displayName: "countryCode") { Options = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "US"),new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "CA")}) },new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.MultiSelect, key: "sports", displayName: "sports") { Options = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "basketball"),new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "football"),new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "tennis")}) }}) });
            MetadataFull createdMetadata = await client.FolderMetadata.CreateFolderMetadataByIdAsync(folderId: folder.Id, scope: CreateFolderMetadataByIdScope.Enterprise, templateKey: templateKey, requestBody: new Dictionary<string, object>() { { "name", "John" }, { "age", 23 }, { "birthDate", "2001-01-03T02:20:50.520Z" }, { "countryCode", "US" }, { "sports", Array.AsReadOnly(new [] {"basketball","tennis"}) } });
            Assert.IsTrue(StringUtils.ToStringRepresentation(createdMetadata.Template) == templateKey);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(createdMetadata.ExtraData)["name"]) == "John");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(createdMetadata.ExtraData)["age"]) == "23");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(createdMetadata.ExtraData)["birthDate"]) == "2001-01-03T02:20:50.520Z");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(createdMetadata.ExtraData)["countryCode"]) == "US");
            await client.FolderMetadata.DeleteFolderMetadataByIdAsync(folderId: folder.Id, scope: DeleteFolderMetadataByIdScope.Enterprise, templateKey: templateKey);
            await client.MetadataTemplates.DeleteMetadataTemplateAsync(scope: DeleteMetadataTemplateScope.Enterprise, templateKey: templateKey);
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

    }
}