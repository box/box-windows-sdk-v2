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
    public class FileClassificationsManagerTests {
        public BoxClient client { get; }

        public FileClassificationsManagerTests() {
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
        public async System.Threading.Tasks.Task TestFileClassifications() {
            ClassificationTemplate classificationTemplate = await new CommonsManager().GetOrCreateClassificationTemplateAsync();
            ClassificationTemplateFieldsOptionsField classification = await new CommonsManager().GetOrCreateClassificationAsync(classificationTemplate: classificationTemplate);
            FileFull file = await new CommonsManager().UploadNewFileAsync();
            await Assert.That.IsExceptionAsync(async() => await client.FileClassifications.GetClassificationOnFileAsync(fileId: file.Id));
            Classification createdFileClassification = await client.FileClassifications.AddClassificationToFileAsync(fileId: file.Id, requestBody: new AddClassificationToFileRequestBody() { BoxSecurityClassificationKey = classification.Key });
            Assert.IsTrue(createdFileClassification.BoxSecurityClassificationKey == classification.Key);
            Classification fileClassification = await client.FileClassifications.GetClassificationOnFileAsync(fileId: file.Id);
            Assert.IsTrue(fileClassification.BoxSecurityClassificationKey == classification.Key);
            ClassificationTemplateFieldsOptionsField secondClassification = await GetOrCreateSecondClassificationAsync(classificationTemplate: classificationTemplate);
            Classification updatedFileClassification = await client.FileClassifications.UpdateClassificationOnFileAsync(fileId: file.Id, requestBody: Array.AsReadOnly(new [] {new UpdateClassificationOnFileRequestBody(value: secondClassification.Key)}));
            Assert.IsTrue(updatedFileClassification.BoxSecurityClassificationKey == secondClassification.Key);
            await client.FileClassifications.DeleteClassificationFromFileAsync(fileId: file.Id);
            await Assert.That.IsExceptionAsync(async() => await client.FileClassifications.GetClassificationOnFileAsync(fileId: file.Id));
            await client.Files.DeleteFileByIdAsync(fileId: file.Id);
        }

    }
}