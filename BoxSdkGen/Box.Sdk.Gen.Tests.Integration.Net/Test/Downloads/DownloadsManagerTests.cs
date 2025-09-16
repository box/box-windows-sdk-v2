using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class DownloadsManagerTests {
        public BoxClient client { get; }

        public DownloadsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestDownloadFile() {
            string newFileName = Utils.GetUUID();
            byte[] fileBuffer = Utils.GenerateByteBuffer(size: 1024 * 1024);
            System.IO.Stream fileContentStream = Utils.GenerateByteStreamFromBuffer(buffer: fileBuffer);
            Files uploadedFiles = await client.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: newFileName, parent: new UploadFileRequestBodyAttributesParentField(id: "0")), file: fileContentStream));
            FileFull uploadedFile = NullableUtils.Unwrap(uploadedFiles.Entries)[0];
            System.IO.Stream? downloadedFileContent = await client.Downloads.DownloadFileAsync(fileId: uploadedFile.Id);
            Assert.IsTrue(Utils.BufferEquals(buffer1: await Utils.ReadByteStreamAsync(byteStream: NullableUtils.Unwrap(downloadedFileContent)), buffer2: fileBuffer));
            await client.Files.DeleteFileByIdAsync(fileId: uploadedFile.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetDownloadUrl() {
            FileFull uploadedFile = await new CommonsManager().UploadNewFileAsync();
            string downloadUrl = await client.Downloads.GetDownloadFileUrlAsync(fileId: uploadedFile.Id);
            Assert.IsTrue(downloadUrl != null);
            Assert.IsTrue(downloadUrl.Contains("https://"));
            await client.Files.DeleteFileByIdAsync(fileId: uploadedFile.Id);
        }

    }
}