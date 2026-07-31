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
    public class QueryManagerTests {
        public BoxClient client { get; }

        public QueryManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateQueryV2026R0() {
            string templateKey = string.Concat("key", Utils.GetUUID());
            MetadataTemplate template = await client.MetadataTemplates.CreateMetadataTemplateAsync(requestBody: new CreateMetadataTemplateRequestBody(scope: "enterprise", displayName: templateKey) { TemplateKey = templateKey, Fields = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.String, key: "name", displayName: "name"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Float, key: "age", displayName: "age"),new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Date, key: "birthDate", displayName: "birthDate")}) });
            Assert.IsTrue(template.TemplateKey == templateKey);
            FileFull file = await new CommonsManager().UploadNewFileAsync();
            MetadataFull metadata = await client.FileMetadata.CreateFileMetadataByIdAsync(fileId: file.Id, scope: CreateFileMetadataByIdScope.Enterprise, templateKey: templateKey, requestBody: new Dictionary<string, object>() { { "name", "John" }, { "age", 23 }, { "birthDate", "2001-01-03T02:20:50.520Z" } });
            Assert.IsTrue(metadata.Template == templateKey);
            Assert.IsTrue(metadata.Scope == template.Scope);
            await Utils.DelayInSecondsAsync(seconds: 10);
            string searchFrom = string.Concat(NullableUtils.Unwrap(template.Scope), ":", NullableUtils.Unwrap(template.TemplateKey));
            string mdPrefix = string.Concat("metadata.", NullableUtils.Unwrap(template.Scope), ".\"", NullableUtils.Unwrap(template.TemplateKey), "\"");
            string predicate = string.Concat(mdPrefix, ".name = :name AND ", mdPrefix, ".age < :age");
            QueryResultsV2026R0 queryResult = await client.Query.CreateQueryV2026R0Async(requestBody: new QueryRequestBodyV2026R0(query: new QueryRequestBodyV2026R0QueryField(predicate: predicate) { Parameters = new Dictionary<string, object>() { { "name", "John" }, { "age", 50 } }, Ancestors = Array.AsReadOnly(new [] {new QueryAncestorReferenceV2026R0(id: "0", type: "folder")}) }) { Limit = 10, Fields = Array.AsReadOnly(new [] {"box:item:name",searchFrom}) });
            Assert.IsTrue(queryResult.Entries.Count >= 0);
            await client.MetadataTemplates.DeleteMetadataTemplateAsync(scope: DeleteMetadataTemplateScope.Enterprise, templateKey: NullableUtils.Unwrap(template.TemplateKey));
            await client.Files.DeleteFileByIdAsync(fileId: file.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateQueryInsightV2026R0() {
            string templateKey = string.Concat("key", Utils.GetUUID());
            MetadataTemplate template = await client.MetadataTemplates.CreateMetadataTemplateAsync(requestBody: new CreateMetadataTemplateRequestBody(scope: "enterprise", displayName: templateKey) { TemplateKey = templateKey, Fields = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Enum, key: "category", displayName: "category") { Options = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "Sales"),new CreateMetadataTemplateRequestBodyFieldsOptionsField(key: "Support")}) },new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Float, key: "amount", displayName: "amount")}) });
            Assert.IsTrue(template.TemplateKey == templateKey);
            FileFull file = await new CommonsManager().UploadNewFileAsync();
            MetadataFull metadata = await client.FileMetadata.CreateFileMetadataByIdAsync(fileId: file.Id, scope: CreateFileMetadataByIdScope.Enterprise, templateKey: templateKey, requestBody: new Dictionary<string, object>() { { "category", "Sales" }, { "amount", 150 } });
            Assert.IsTrue(metadata.Template == templateKey);
            await Utils.DelayInSecondsAsync(seconds: 5);
            string mdPrefix = string.Concat("metadata.", NullableUtils.Unwrap(template.Scope), ".\"", NullableUtils.Unwrap(template.TemplateKey), "\"");
            string predicate = string.Concat(mdPrefix, ".amount > :minAmount");
            Dictionary<string, QueryInsightsMetricDefinitionV2026R0> metrics = new Dictionary<string, QueryInsightsMetricDefinitionV2026R0>() { { "totalAmount", new QueryInsightsMetricDefinitionV2026R0(type: QueryInsightsMetricDefinitionV2026R0TypeField.Sum, field: string.Concat(mdPrefix, ".amount")) }, { "countItems", new QueryInsightsMetricDefinitionV2026R0(type: QueryInsightsMetricDefinitionV2026R0TypeField.Count, field: string.Concat(mdPrefix, ".category")) } };
            QueryInsightsV2026R0 insightResult = await client.Query.CreateQueryInsightV2026R0Async(requestBody: new QueryInsightsRequestBodyV2026R0(query: new QueryInsightsRequestBodyV2026R0QueryField(predicate: predicate) { Parameters = new Dictionary<string, object>() { { "minAmount", 0 } }, Ancestors = Array.AsReadOnly(new [] {new QueryAncestorReferenceV2026R0(id: "0", type: "folder")}), GroupBy = Array.AsReadOnly(new [] {new QueryInsightsGroupByV2026R0(field: string.Concat(mdPrefix, ".category")) { BucketLimit = 5 }}) }, metrics: metrics));
            Assert.IsTrue(insightResult.Insights.Count >= 0);
            await client.MetadataTemplates.DeleteMetadataTemplateAsync(scope: DeleteMetadataTemplateScope.Enterprise, templateKey: NullableUtils.Unwrap(template.TemplateKey));
            await client.Files.DeleteFileByIdAsync(fileId: file.Id);
        }

    }
}