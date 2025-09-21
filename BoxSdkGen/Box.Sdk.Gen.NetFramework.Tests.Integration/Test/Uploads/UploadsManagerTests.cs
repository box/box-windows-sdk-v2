using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class UploadsManagerTests {
        public BoxClient client { get; }

        public UploadsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestUploadFileAndFileVersion() {
            string newFileName = Utils.GetUUID();
            System.IO.Stream fileContentStream = Utils.GenerateByteStream(size: 1024 * 1024);
            Files uploadedFiles = await client.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: newFileName, parent: new UploadFileRequestBodyAttributesParentField(id: "0")), file: fileContentStream));
            FileFull uploadedFile = NullableUtils.Unwrap(uploadedFiles.Entries)[0];
            Assert.IsTrue(uploadedFile.Name == newFileName);
            string newFileVersionName = Utils.GetUUID();
            System.IO.Stream newFileContentStream = Utils.GenerateByteStream(size: 1024 * 1024);
            Files uploadedFilesVersion = await client.Uploads.UploadFileVersionAsync(fileId: uploadedFile.Id, requestBody: new UploadFileVersionRequestBody(attributes: new UploadFileVersionRequestBodyAttributesField(name: newFileVersionName), file: newFileContentStream));
            FileFull newFileVersion = NullableUtils.Unwrap(uploadedFilesVersion.Entries)[0];
            Assert.IsTrue(newFileVersion.Name == newFileVersionName);
            await client.Files.DeleteFileByIdAsync(fileId: newFileVersion.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestRequestCancellation() {
            int fileSize = 1024 * 1024;
            string fileName = Utils.GetUUID();
            System.IO.Stream fileByteStream = Utils.GenerateByteStream(size: fileSize);
            System.Threading.CancellationToken cancellationToken = Utils.CreateTokenAndCancelAfter(delay: 1);
            await Assert.That.IsExceptionAsync(async() => await client.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: fileName, parent: new UploadFileRequestBodyAttributesParentField(id: "0")), file: fileByteStream), queryParams: new UploadFileQueryParams(), headers: new UploadFileHeaders(), cancellationToken: cancellationToken));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestUploadFileWithPreflightCheck() {
            string newFileName = Utils.GetUUID();
            System.IO.Stream fileContentStream = Utils.GenerateByteStream(size: 1024 * 1024);
            await Assert.That.IsExceptionAsync(async() => await client.Uploads.UploadWithPreflightCheckAsync(requestBody: new UploadWithPreflightCheckRequestBody(attributes: new UploadWithPreflightCheckRequestBodyAttributesField(name: newFileName, size: -1, parent: new UploadWithPreflightCheckRequestBodyAttributesParentField(id: "0")), file: fileContentStream)));
            Files uploadFilesWithPreflight = await client.Uploads.UploadWithPreflightCheckAsync(requestBody: new UploadWithPreflightCheckRequestBody(attributes: new UploadWithPreflightCheckRequestBodyAttributesField(name: newFileName, size: 1024 * 1024, parent: new UploadWithPreflightCheckRequestBodyAttributesParentField(id: "0")), file: fileContentStream));
            FileFull file = NullableUtils.Unwrap(uploadFilesWithPreflight.Entries)[0];
            Assert.IsTrue(file.Name == newFileName);
            Assert.IsTrue(file.Size == 1024 * 1024);
            await Assert.That.IsExceptionAsync(async() => await client.Uploads.UploadWithPreflightCheckAsync(requestBody: new UploadWithPreflightCheckRequestBody(attributes: new UploadWithPreflightCheckRequestBodyAttributesField(name: newFileName, size: 1024 * 1024, parent: new UploadWithPreflightCheckRequestBodyAttributesParentField(id: "0")), file: fileContentStream)));
            await client.Files.DeleteFileByIdAsync(fileId: file.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestPreflightCheck() {
            string newFileName = Utils.GetUUID();
            UploadUrl preflightCheckResult = await client.Uploads.PreflightFileUploadCheckAsync(requestBody: new PreflightFileUploadCheckRequestBody() { Name = newFileName, Size = 1024 * 1024, Parent = new PreflightFileUploadCheckRequestBodyParentField() { Id = "0" } });
            Assert.IsTrue(preflightCheckResult.UploadUrlField != "");
        }

    }
}