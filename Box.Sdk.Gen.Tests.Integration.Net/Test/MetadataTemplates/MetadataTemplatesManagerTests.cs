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
    public class MetadataTemplatesManagerTests {
        public BoxClient client { get; }

        public MetadataTemplatesManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestMetadataTemplates() {
            string templateKey = string.Concat("key", Utils.GetUUID());
            MetadataTemplate template = await client.MetadataTemplates.CreateMetadataTemplateAsync(requestBody: new CreateMetadataTemplateRequestBody(scope: "enterprise", displayName: templateKey) { TemplateKey = templateKey, Fields = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.String, key: "testName", displayName: "testName"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Float, key: "age", displayName: "age"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Date, key: "birthDate", displayName: "birthDate"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Enum, key: "countryCode", displayName: "countryCode") { Options = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "US"),new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "CA")}) },new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.MultiSelect, key: "sports", displayName: "sports") { Options = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "basketball"),new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "football"),new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "tennis")}) }}) });
            Assert.IsTrue(template.TemplateKey == templateKey);
            Assert.IsTrue(template.DisplayName == templateKey);
            Assert.IsTrue(NullableUtils.Unwrap(template.Fields).Count == 5);
            Assert.IsTrue(NullableUtils.Unwrap(template.Fields)[0].Key == "testName");
            Assert.IsTrue(NullableUtils.Unwrap(template.Fields)[0].DisplayName == "testName");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(template.Fields)[0].Type) == "string");
            Assert.IsTrue(NullableUtils.Unwrap(template.Fields)[1].Key == "age");
            Assert.IsTrue(NullableUtils.Unwrap(template.Fields)[1].DisplayName == "age");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(template.Fields)[1].Type) == "float");
            Assert.IsTrue(NullableUtils.Unwrap(template.Fields)[2].Key == "birthDate");
            Assert.IsTrue(NullableUtils.Unwrap(template.Fields)[2].DisplayName == "birthDate");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(template.Fields)[2].Type) == "date");
            Assert.IsTrue(NullableUtils.Unwrap(template.Fields)[3].Key == "countryCode");
            Assert.IsTrue(NullableUtils.Unwrap(template.Fields)[3].DisplayName == "countryCode");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(template.Fields)[3].Type) == "enum");
            Assert.IsTrue(NullableUtils.Unwrap(template.Fields)[4].Key == "sports");
            Assert.IsTrue(NullableUtils.Unwrap(template.Fields)[4].DisplayName == "sports");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(template.Fields)[4].Type) == "multiSelect");
            MetadataTemplate updatedTemplate = await client.MetadataTemplates.UpdateMetadataTemplateAsync(scope: UpdateMetadataTemplateScope.Enterprise, templateKey: templateKey, requestBody: Array.AsReadOnly(new [] {new UpdateMetadataTemplateRequestBody(op: UpdateMetadataTemplateRequestBodyOpField.AddField) { FieldKey = "newfieldname", Data = new Dictionary<string, object>() { { "type", "string" }, { "displayName", "newFieldName" } } }}));
            Assert.IsTrue(NullableUtils.Unwrap(updatedTemplate.Fields).Count == 6);
            Assert.IsTrue(NullableUtils.Unwrap(updatedTemplate.Fields)[5].Key == "newfieldname");
            Assert.IsTrue(NullableUtils.Unwrap(updatedTemplate.Fields)[5].DisplayName == "newFieldName");
            MetadataTemplate getMetadataTemplate = await client.MetadataTemplates.GetMetadataTemplateByIdAsync(templateId: template.Id);
            Assert.IsTrue(getMetadataTemplate.Id == template.Id);
            MetadataTemplate getMetadataTemplateSchema = await client.MetadataTemplates.GetMetadataTemplateAsync(scope: GetMetadataTemplateScope.Enterprise, templateKey: NullableUtils.Unwrap(template.TemplateKey));
            Assert.IsTrue(getMetadataTemplateSchema.Id == template.Id);
            MetadataTemplates enterpriseMetadataTemplates = await client.MetadataTemplates.GetEnterpriseMetadataTemplatesAsync();
            Assert.IsTrue(NullableUtils.Unwrap(enterpriseMetadataTemplates.Entries).Count > 0);
            MetadataTemplates globalMetadataTemplates = await client.MetadataTemplates.GetGlobalMetadataTemplatesAsync();
            Assert.IsTrue(NullableUtils.Unwrap(globalMetadataTemplates.Entries).Count > 0);
            await client.MetadataTemplates.DeleteMetadataTemplateAsync(scope: DeleteMetadataTemplateScope.Enterprise, templateKey: NullableUtils.Unwrap(template.TemplateKey));
            await Assert.That.IsExceptionAsync(async() => await client.MetadataTemplates.DeleteMetadataTemplateAsync(scope: DeleteMetadataTemplateScope.Enterprise, templateKey: NullableUtils.Unwrap(template.TemplateKey)));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetMetadataTemplateByInstance() {
            FileFull file = await new CommonsManager().UploadNewFileAsync();
            string templateKey = string.Concat("key", Utils.GetUUID());
            MetadataTemplate template = await client.MetadataTemplates.CreateMetadataTemplateAsync(requestBody: new CreateMetadataTemplateRequestBody(scope: "enterprise", displayName: templateKey) { TemplateKey = templateKey, Fields = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.String, key: "testName", displayName: "testName")}) });
            MetadataFull createdMetadataInstance = await client.FileMetadata.CreateFileMetadataByIdAsync(fileId: file.Id, scope: CreateFileMetadataByIdScope.Enterprise, templateKey: templateKey, requestBody: new Dictionary<string, object>() { { "testName", "xyz" } });
            MetadataTemplates metadataTemplates = await client.MetadataTemplates.GetMetadataTemplatesByInstanceIdAsync(queryParams: new GetMetadataTemplatesByInstanceIdQueryParams(metadataInstanceId: NullableUtils.Unwrap(createdMetadataInstance.Id)));
            Assert.IsTrue(NullableUtils.Unwrap(metadataTemplates.Entries).Count == 1);
            Assert.IsTrue(NullableUtils.Unwrap(metadataTemplates.Entries)[0].DisplayName == templateKey);
            Assert.IsTrue(NullableUtils.Unwrap(metadataTemplates.Entries)[0].TemplateKey == templateKey);
            await client.FileMetadata.DeleteFileMetadataByIdAsync(fileId: file.Id, scope: DeleteFileMetadataByIdScope.Enterprise, templateKey: templateKey);
            await client.MetadataTemplates.DeleteMetadataTemplateAsync(scope: DeleteMetadataTemplateScope.Enterprise, templateKey: NullableUtils.Unwrap(template.TemplateKey));
            await client.Files.DeleteFileByIdAsync(fileId: file.Id);
        }

    }
}