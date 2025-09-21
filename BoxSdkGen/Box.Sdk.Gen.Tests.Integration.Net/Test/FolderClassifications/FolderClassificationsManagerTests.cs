using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class FolderClassificationsManagerTests {
        public BoxClient client { get; }

        public FolderClassificationsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        public async System.Threading.Tasks.Task<ClassificationTemplateFieldsOptionsField> GetOrCreateSecondClassificationAsync(ClassificationTemplate classificationTemplate) {
            IReadOnlyList<ClassificationTemplateFieldsOptionsField> classifications = classificationTemplate.Fields[0].Options;
            int currentNumberOfClassifications = classifications.Count;
            if (currentNumberOfClassifications == 1) {
                ClassificationTemplate classificationTemplateWithNewClassification = await client.Classifications.AddClassificationAsync(requestBody: Array.AsReadOnly(new [] {new AddClassificationRequestBody(data: new AddClassificationRequestBodyDataField(key: Utils.GetUUID()) { StaticConfig = new AddClassificationRequestBodyDataStaticConfigField() { Classification = new AddClassificationRequestBodyDataStaticConfigClassificationField() { ColorId = 4, ClassificationDefinition = "Other description" } } })}));
                return classificationTemplateWithNewClassification.Fields[0].Options[1];
            }
            return classifications.ElementAt(1);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestFolderClassifications() {
            ClassificationTemplate classificationTemplate = await new CommonsManager().GetOrCreateClassificationTemplateAsync();
            ClassificationTemplateFieldsOptionsField classification = await new CommonsManager().GetOrCreateClassificationAsync(classificationTemplate: classificationTemplate);
            FolderFull folder = await new CommonsManager().CreateNewFolderAsync();
            await Assert.That.IsExceptionAsync(async() => await client.FolderClassifications.GetClassificationOnFolderAsync(folderId: folder.Id));
            Classification createdFolderClassification = await client.FolderClassifications.AddClassificationToFolderAsync(folderId: folder.Id, requestBody: new AddClassificationToFolderRequestBody() { BoxSecurityClassificationKey = classification.Key });
            Assert.IsTrue(createdFolderClassification.BoxSecurityClassificationKey == classification.Key);
            Classification folderClassification = await client.FolderClassifications.GetClassificationOnFolderAsync(folderId: folder.Id);
            Assert.IsTrue(folderClassification.BoxSecurityClassificationKey == classification.Key);
            ClassificationTemplateFieldsOptionsField secondClassification = await GetOrCreateSecondClassificationAsync(classificationTemplate: classificationTemplate);
            Classification updatedFolderClassification = await client.FolderClassifications.UpdateClassificationOnFolderAsync(folderId: folder.Id, requestBody: Array.AsReadOnly(new [] {new UpdateClassificationOnFolderRequestBody(value: secondClassification.Key)}));
            Assert.IsTrue(updatedFolderClassification.BoxSecurityClassificationKey == secondClassification.Key);
            await client.FolderClassifications.DeleteClassificationFromFolderAsync(folderId: folder.Id);
            await Assert.That.IsExceptionAsync(async() => await client.FolderClassifications.GetClassificationOnFolderAsync(folderId: folder.Id));
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

    }
}