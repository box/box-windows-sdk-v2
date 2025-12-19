using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class MetadataTaxonomiesManagerTests {
        public BoxClient client { get; }

        public MetadataTaxonomiesManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestMetadataTaxonomiesCrud() {
            string namespaceParam = string.Concat("enterprise_", Utils.GetEnvVar(name: "ENTERPRISE_ID"));
            string uuid = Utils.GetUUID();
            string taxonomyKey = string.Concat("geography", uuid);
            string displayName = string.Concat("Geography Taxonomy", uuid);
            MetadataTaxonomy createdTaxonomy = await client.MetadataTaxonomies.CreateMetadataTaxonomyAsync(requestBody: new CreateMetadataTaxonomyRequestBody(displayName: displayName, namespaceParam: namespaceParam) { Key = taxonomyKey });
            Assert.IsTrue(createdTaxonomy.DisplayName == displayName);
            Assert.IsTrue(createdTaxonomy.NamespaceParam == namespaceParam);
            MetadataTaxonomies taxonomies = await client.MetadataTaxonomies.GetMetadataTaxonomiesAsync(namespaceParam: namespaceParam);
            Assert.IsTrue(NullableUtils.Unwrap(taxonomies.Entries).Count > 0);
            Assert.IsTrue(NullableUtils.Unwrap(taxonomies.Entries)[0].NamespaceParam == namespaceParam);
            string updatedDisplayName = string.Concat("Geography Taxonomy UPDATED", uuid);
            MetadataTaxonomy updatedTaxonomy = await client.MetadataTaxonomies.UpdateMetadataTaxonomyAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey, requestBody: new UpdateMetadataTaxonomyRequestBody(displayName: updatedDisplayName));
            Assert.IsTrue(updatedTaxonomy.DisplayName == updatedDisplayName);
            Assert.IsTrue(updatedTaxonomy.NamespaceParam == namespaceParam);
            Assert.IsTrue(updatedTaxonomy.Id == createdTaxonomy.Id);
            MetadataTaxonomy getTaxonomy = await client.MetadataTaxonomies.GetMetadataTaxonomyByKeyAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey);
            Assert.IsTrue(getTaxonomy.DisplayName == updatedDisplayName);
            Assert.IsTrue(getTaxonomy.NamespaceParam == namespaceParam);
            Assert.IsTrue(getTaxonomy.Id == createdTaxonomy.Id);
            await client.MetadataTaxonomies.DeleteMetadataTaxonomyAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey);
            await Assert.That.IsExceptionAsync(async() => await client.MetadataTaxonomies.GetMetadataTaxonomyByKeyAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestMetadataTaxonomiesNodes() {
            string namespaceParam = string.Concat("enterprise_", Utils.GetEnvVar(name: "ENTERPRISE_ID"));
            string uuid = Utils.GetUUID();
            string taxonomyKey = string.Concat("geography", uuid);
            string displayName = string.Concat("Geography Taxonomy", uuid);
            MetadataTaxonomy createdTaxonomy = await client.MetadataTaxonomies.CreateMetadataTaxonomyAsync(requestBody: new CreateMetadataTaxonomyRequestBody(displayName: displayName, namespaceParam: namespaceParam) { Key = taxonomyKey });
            Assert.IsTrue(createdTaxonomy.DisplayName == displayName);
            Assert.IsTrue(createdTaxonomy.NamespaceParam == namespaceParam);
            MetadataTaxonomyLevels taxonomyLevels = await client.MetadataTaxonomies.CreateMetadataTaxonomyLevelAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey, requestBody: Array.AsReadOnly(new [] {new MetadataTaxonomyLevel() { DisplayName = "Continent", Description = "Continent Level" },new MetadataTaxonomyLevel() { DisplayName = "Country", Description = "Country Level" }}));
            Assert.IsTrue(NullableUtils.Unwrap(taxonomyLevels.Entries).Count == 2);
            Assert.IsTrue(NullableUtils.Unwrap(taxonomyLevels.Entries)[0].DisplayName == "Continent");
            Assert.IsTrue(NullableUtils.Unwrap(taxonomyLevels.Entries)[1].DisplayName == "Country");
            MetadataTaxonomyLevel updatedTaxonomyLevels = await client.MetadataTaxonomies.UpdateMetadataTaxonomyLevelByIdAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey, levelIndex: 1L, requestBody: new UpdateMetadataTaxonomyLevelByIdRequestBody(displayName: "Continent UPDATED") { Description = "Continent Level UPDATED" });
            Assert.IsTrue(updatedTaxonomyLevels.DisplayName == "Continent UPDATED");
            Assert.IsTrue(updatedTaxonomyLevels.Description == "Continent Level UPDATED");
            Assert.IsTrue(updatedTaxonomyLevels.Level == NullableUtils.Unwrap(taxonomyLevels.Entries)[0].Level);
            MetadataTaxonomyLevels taxonomyLevelsAfterAddition = await client.MetadataTaxonomies.AddMetadataTaxonomyLevelAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey, requestBody: new AddMetadataTaxonomyLevelRequestBody(displayName: "Region") { Description = "Region Description" });
            Assert.IsTrue(NullableUtils.Unwrap(taxonomyLevelsAfterAddition.Entries).Count == 3);
            Assert.IsTrue(NullableUtils.Unwrap(taxonomyLevelsAfterAddition.Entries)[2].DisplayName == "Region");
            MetadataTaxonomyLevels taxonomyLevelsAfterDeletion = await client.MetadataTaxonomies.DeleteMetadataTaxonomyLevelAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey);
            Assert.IsTrue(NullableUtils.Unwrap(taxonomyLevelsAfterDeletion.Entries).Count == 2);
            Assert.IsTrue(NullableUtils.Unwrap(taxonomyLevelsAfterDeletion.Entries)[0].DisplayName == "Continent UPDATED");
            Assert.IsTrue(NullableUtils.Unwrap(taxonomyLevelsAfterDeletion.Entries)[1].DisplayName == "Country");
            MetadataTaxonomyNode continentNode = await client.MetadataTaxonomies.CreateMetadataTaxonomyNodeAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey, requestBody: new CreateMetadataTaxonomyNodeRequestBody(displayName: "Europe", level: 1));
            Assert.IsTrue(continentNode.DisplayName == "Europe");
            Assert.IsTrue(continentNode.Level == 1);
            MetadataTaxonomyNode countryNode = await client.MetadataTaxonomies.CreateMetadataTaxonomyNodeAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey, requestBody: new CreateMetadataTaxonomyNodeRequestBody(displayName: "Poland", level: 2) { ParentId = continentNode.Id });
            Assert.IsTrue(countryNode.DisplayName == "Poland");
            Assert.IsTrue(countryNode.Level == 2);
            Assert.IsTrue(countryNode.ParentId == continentNode.Id);
            await Utils.DelayInSecondsAsync(seconds: 5);
            MetadataTaxonomyNodes allNodes = await client.MetadataTaxonomies.GetMetadataTaxonomyNodesAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey);
            Assert.IsTrue(NullableUtils.Unwrap(allNodes.Entries).Count == 2);
            MetadataTaxonomyNode updatedCountryNode = await client.MetadataTaxonomies.UpdateMetadataTaxonomyNodeAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey, nodeId: countryNode.Id, requestBody: new UpdateMetadataTaxonomyNodeRequestBody() { DisplayName = "Poland UPDATED" });
            Assert.IsTrue(updatedCountryNode.DisplayName == "Poland UPDATED");
            Assert.IsTrue(updatedCountryNode.Level == 2);
            Assert.IsTrue(updatedCountryNode.ParentId == countryNode.ParentId);
            Assert.IsTrue(updatedCountryNode.Id == countryNode.Id);
            MetadataTaxonomyNode getCountryNode = await client.MetadataTaxonomies.GetMetadataTaxonomyNodeByIdAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey, nodeId: countryNode.Id);
            Assert.IsTrue(getCountryNode.DisplayName == "Poland UPDATED");
            Assert.IsTrue(getCountryNode.Id == countryNode.Id);
            string metadataTemplateKey = string.Concat("templateKey", Utils.GetUUID());
            MetadataTemplate metadataTemplate = await client.MetadataTemplates.CreateMetadataTemplateAsync(requestBody: new CreateMetadataTemplateRequestBody(scope: "enterprise", displayName: metadataTemplateKey) { TemplateKey = metadataTemplateKey, Fields = Array.AsReadOnly(new [] {new CreateMetadataTemplateRequestBodyFieldsField(type: CreateMetadataTemplateRequestBodyFieldsTypeField.Taxonomy, key: "taxonomy", displayName: "taxonomy") { TaxonomyKey = taxonomyKey, NamespaceParam = namespaceParam, OptionsRules = new CreateMetadataTemplateRequestBodyFieldsOptionsRulesField() { MultiSelect = true, SelectableLevels = Array.AsReadOnly(new [] {1L}) } }}) });
            Assert.IsTrue(metadataTemplate.TemplateKey == metadataTemplateKey);
            Assert.IsTrue(metadataTemplate.DisplayName == metadataTemplateKey);
            Assert.IsTrue(NullableUtils.Unwrap(metadataTemplate.Fields).Count == 1);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(metadataTemplate.Fields)[0].Type) == "taxonomy");
            MetadataTaxonomyNodes options = await client.MetadataTaxonomies.GetMetadataTemplateFieldOptionsAsync(namespaceParam: namespaceParam, templateKey: metadataTemplateKey, fieldKey: "taxonomy");
            Assert.IsTrue(NullableUtils.Unwrap(options.Entries).Count == 1);
            await client.MetadataTemplates.DeleteMetadataTemplateAsync(scope: DeleteMetadataTemplateScope.Enterprise, templateKey: metadataTemplateKey);
            await client.MetadataTaxonomies.DeleteMetadataTaxonomyNodeAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey, nodeId: countryNode.Id);
            await client.MetadataTaxonomies.DeleteMetadataTaxonomyNodeAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey, nodeId: continentNode.Id);
            await Utils.DelayInSecondsAsync(seconds: 5);
            MetadataTaxonomyNodes allNodesAfterDeletion = await client.MetadataTaxonomies.GetMetadataTaxonomyNodesAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey);
            Assert.IsTrue(NullableUtils.Unwrap(allNodesAfterDeletion.Entries).Count == 0);
            await client.MetadataTaxonomies.DeleteMetadataTaxonomyAsync(namespaceParam: namespaceParam, taxonomyKey: taxonomyKey);
        }

    }
}