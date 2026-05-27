using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class NotesManagerTests {
        public BoxClient client { get; }

        public NotesManagerTests() {
            client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestConvertMarkdownToBoxNote() {
            string noteName = Utils.GetUUID();
            const string markdownContent = "# Heading\n\nSome text";
            NotesConvertResponseV2026R0 response = await client.Notes.CreateNoteConvertV2026R0Async(requestBody: new NotesConvertRequestBodyV2026R0(content: markdownContent, contentFormat: NotesConvertRequestBodyV2026R0ContentFormatField.Markdown, parent: new FolderReferenceV2026R0(id: "0"), name: noteName));
            Assert.IsTrue(response.Id != "");
            Assert.IsTrue(StringUtils.ToStringRepresentation(response.Type?.Value) == "file");
            FileFull file = await client.Files.GetFileByIdAsync(fileId: response.Id);
            Assert.IsTrue(file.Name == string.Concat(noteName, ".boxnote"));
            Assert.IsTrue(NullableUtils.Unwrap(file.Parent).Id == "0");
            await client.Files.DeleteFileByIdAsync(fileId: response.Id);
        }

    }
}