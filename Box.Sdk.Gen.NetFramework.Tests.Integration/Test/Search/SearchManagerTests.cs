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
    public class SearchManagerTests {
        public BoxClient client { get; }

        public SearchManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateMetaDataQueryExecuteRead() {
            string templateKey = string.Concat("key", Utils.GetUUID());
            MetadataTemplate template = await client.MetadataTemplates.CreateMetadataTemplateAsync(requestBody: new CreateMetadataTemplateRequestBody(scope: "enterprise", displayName: templateKey) { TemplateKey = templateKey, Fields = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.String, key: "name", displayName: "name"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Float, key: "age", displayName: "age"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Date, key: "birthDate", displayName: "birthDate"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Enum, key: "countryCode", displayName: "countryCode") { Options = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "US"),new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "CA")}) },new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.MultiSelect, key: "sports", displayName: "sports") { Options = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "basketball"),new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "football"),new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "tennis")}) }}) });
            Assert.IsTrue(template.TemplateKey == templateKey);
            Files files = await client.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: Utils.GetUUID(), parent: new UploadFileRequestBodyAttributesParentField(id: "0")), file: Utils.GenerateByteStream(size: 10)));
            FileFull file = NullableUtils.Unwrap(files.Entries)[0];
            MetadataFull metadata = await client.FileMetadata.CreateFileMetadataByIdAsync(fileId: file.Id, scope: CreateFileMetadataByIdScope.Enterprise, templateKey: templateKey, requestBody: new Dictionary<string, object>() { { "name", "John" }, { "age", 23 }, { "birthDate", "2001-01-03T02:20:50.520Z" }, { "countryCode", "US" }, { "sports", Array.AsReadOnly(new [] {"basketball","tennis"}) } });
            Assert.IsTrue(metadata.Template == templateKey);
            Assert.IsTrue(metadata.Scope == template.Scope);
            await Utils.DelayInSecondsAsync(seconds: 5);
            string searchFrom = string.Concat(NullableUtils.Unwrap(template.Scope), ".", NullableUtils.Unwrap(template.TemplateKey));
            MetadataQueryResults query = await client.Search.SearchByMetadataQueryAsync(requestBody: new MetadataQuery(ancestorFolderId: "0", from: searchFrom) { Query = "name = :name AND age < :age AND birthDate >= :birthDate AND countryCode = :countryCode AND sports = :sports", QueryParams = new Dictionary<string, object>() { { "name", "John" }, { "age", 50 }, { "birthDate", "2001-01-01T02:20:10.120Z" }, { "countryCode", "US" }, { "sports", Array.AsReadOnly(new [] {"basketball","tennis"}) } } });
            Assert.IsTrue(NullableUtils.Unwrap(query.Entries).Count >= 0);
            await client.MetadataTemplates.DeleteMetadataTemplateAsync(scope: DeleteMetadataTemplateScope.Enterprise, templateKey: NullableUtils.Unwrap(template.TemplateKey));
            await client.Files.DeleteFileByIdAsync(fileId: file.Id);
        }

    }
}