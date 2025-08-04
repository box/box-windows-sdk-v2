using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class FileWatermarksManagerTests {
        public BoxClient client { get; }

        public FileWatermarksManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateGetDeleteFileWatermark() {
            string fileName = string.Concat(Utils.GetUUID(), ".txt");
            Files uploadedFiles = await client.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: fileName, parent: new UploadFileRequestBodyAttributesParentField(id: "0")), file: Utils.GenerateByteStream(size: 10)));
            FileFull file = NullableUtils.Unwrap(uploadedFiles.Entries)[0];
            Watermark createdWatermark = await client.FileWatermarks.UpdateFileWatermarkAsync(fileId: file.Id, requestBody: new UpdateFileWatermarkRequestBody(watermark: new UpdateFileWatermarkRequestBodyWatermarkField(imprint: UpdateFileWatermarkRequestBodyWatermarkImprintField.Default)));
            Watermark watermark = await client.FileWatermarks.GetFileWatermarkAsync(fileId: file.Id);
            await client.FileWatermarks.DeleteFileWatermarkAsync(fileId: file.Id);
            await Assert.That.IsExceptionAsync(async() => await client.FileWatermarks.GetFileWatermarkAsync(fileId: file.Id));
            await client.Files.DeleteFileByIdAsync(fileId: file.Id);
        }

    }
}