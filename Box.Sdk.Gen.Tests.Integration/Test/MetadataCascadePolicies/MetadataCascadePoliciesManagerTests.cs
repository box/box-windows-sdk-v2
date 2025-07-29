using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class MetadataCascadePoliciesManagerTests {
        public BoxClient client { get; }

        public MetadataCascadePoliciesManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestMetadataCascadePolicies() {
            string templateKey = string.Concat("key", Utils.GetUUID());
            await client.MetadataTemplates.CreateMetadataTemplateAsync(requestBody: new CreateMetadataTemplateRequestBody(scope: "enterprise", displayName: templateKey) { TemplateKey = templateKey, Fields = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.String, key: "testName", displayName: "testName")}) });
            FolderFull folder = await new CommonsManager().CreateNewFolderAsync();
            string enterpriseId = Utils.GetEnvVar(name: "ENTERPRISE_ID");
            MetadataCascadePolicy cascadePolicy = await client.MetadataCascadePolicies.CreateMetadataCascadePolicyAsync(requestBody: new CreateMetadataCascadePolicyRequestBody(folderId: folder.Id, scope: CreateMetadataCascadePolicyRequestBodyScopeField.Enterprise, templateKey: templateKey));
            Assert.IsTrue(StringUtils.ToStringRepresentation(cascadePolicy.Type?.Value) == "metadata_cascade_policy");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(NullableUtils.Unwrap(cascadePolicy.OwnerEnterprise).Type)) == "enterprise");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(NullableUtils.Unwrap(cascadePolicy.OwnerEnterprise).Id)) == enterpriseId);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(NullableUtils.Unwrap(cascadePolicy.Parent).Type)) == "folder");
            Assert.IsTrue(NullableUtils.Unwrap(NullableUtils.Unwrap(cascadePolicy.Parent).Id) == folder.Id);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(cascadePolicy.Scope)) == string.Concat("enterprise_", enterpriseId));
            Assert.IsTrue(NullableUtils.Unwrap(cascadePolicy.TemplateKey) == templateKey);
            string cascadePolicyId = cascadePolicy.Id;
            MetadataCascadePolicy policyFromTheApi = await client.MetadataCascadePolicies.GetMetadataCascadePolicyByIdAsync(metadataCascadePolicyId: cascadePolicyId);
            Assert.IsTrue(cascadePolicyId == policyFromTheApi.Id);
            MetadataCascadePolicies policies = await client.MetadataCascadePolicies.GetMetadataCascadePoliciesAsync(queryParams: new GetMetadataCascadePoliciesQueryParams(folderId: folder.Id));
            Assert.IsTrue(NullableUtils.Unwrap(policies.Entries).Count == 1);
            await Assert.That.IsExceptionAsync(async() => await client.MetadataCascadePolicies.ApplyMetadataCascadePolicyAsync(metadataCascadePolicyId: cascadePolicyId, requestBody: new ApplyMetadataCascadePolicyRequestBody(conflictResolution: ApplyMetadataCascadePolicyRequestBodyConflictResolutionField.Overwrite)));
            await client.FolderMetadata.CreateFolderMetadataByIdAsync(folderId: folder.Id, scope: CreateFolderMetadataByIdScope.Enterprise, templateKey: templateKey, requestBody: new Dictionary<string, object>() { { "testName", "xyz" } });
            await client.MetadataCascadePolicies.ApplyMetadataCascadePolicyAsync(metadataCascadePolicyId: cascadePolicyId, requestBody: new ApplyMetadataCascadePolicyRequestBody(conflictResolution: ApplyMetadataCascadePolicyRequestBodyConflictResolutionField.Overwrite));
            await client.MetadataCascadePolicies.DeleteMetadataCascadePolicyByIdAsync(metadataCascadePolicyId: cascadePolicyId);
            await Assert.That.IsExceptionAsync(async() => await client.MetadataCascadePolicies.GetMetadataCascadePolicyByIdAsync(metadataCascadePolicyId: cascadePolicyId));
            await client.MetadataTemplates.DeleteMetadataTemplateAsync(scope: DeleteMetadataTemplateScope.Enterprise, templateKey: templateKey);
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

    }
}