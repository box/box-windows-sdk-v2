using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class DocgenTemplateManagerTests {
        public BoxClient client { get; }

        public DocgenTemplateManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestDocgenTemplateCrud() {
            FileFull file = await new CommonsManager().UploadNewFileAsync();
            DocGenTemplateBaseV2025R0 createdDocgenTemplate = await client.DocgenTemplate.CreateDocgenTemplateV2025R0Async(requestBody: new DocGenTemplateCreateRequestV2025R0(file: new FileReferenceV2025R0(id: file.Id)));
            DocGenTemplatesV2025R0 docgenTemplates = await client.DocgenTemplate.GetDocgenTemplatesV2025R0Async();
            Assert.IsTrue(NullableUtils.Unwrap(docgenTemplates.Entries).Count > 0);
            DocGenTemplateV2025R0 fetchedDocgenTemplate = await client.DocgenTemplate.GetDocgenTemplateByIdV2025R0Async(templateId: NullableUtils.Unwrap(createdDocgenTemplate.File).Id);
            Assert.IsTrue(NullableUtils.Unwrap(fetchedDocgenTemplate.File).Id == NullableUtils.Unwrap(createdDocgenTemplate.File).Id);
            DocGenTagsV2025R0 docgenTemplateTags = await client.DocgenTemplate.GetDocgenTemplateTagsV2025R0Async(templateId: NullableUtils.Unwrap(fetchedDocgenTemplate.File).Id);
            DocGenJobsV2025R0 docgenTemplateJobs = await client.DocgenTemplate.GetDocgenTemplateJobByIdV2025R0Async(templateId: NullableUtils.Unwrap(fetchedDocgenTemplate.File).Id);
            Assert.IsTrue(NullableUtils.Unwrap(docgenTemplateJobs.Entries).Count == 0);
            await client.DocgenTemplate.DeleteDocgenTemplateByIdV2025R0Async(templateId: NullableUtils.Unwrap(createdDocgenTemplate.File).Id);
            await client.Files.DeleteFileByIdAsync(fileId: file.Id);
        }

    }
}