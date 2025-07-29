using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class FileRequestsManagerTests {
        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetCopyUpdateDeleteFileRequest() {
            string fileRequestId = Utils.GetEnvVar(name: "BOX_FILE_REQUEST_ID");
            string userId = Utils.GetEnvVar(name: "USER_ID");
            BoxClient client = new CommonsManager().GetDefaultClientWithUserSubject(userId: userId);
            FileRequest fileRequest = await client.FileRequests.GetFileRequestByIdAsync(fileRequestId: fileRequestId);
            Assert.IsTrue(fileRequest.Id == fileRequestId);
            Assert.IsTrue(StringUtils.ToStringRepresentation(fileRequest.Type?.Value) == "file_request");
            FileRequest copiedFileRequest = await client.FileRequests.CreateFileRequestCopyAsync(fileRequestId: fileRequestId, requestBody: new FileRequestCopyRequest(folder: new FileRequestCopyRequestFolderField(id: fileRequest.Folder.Id) { Type = FileRequestCopyRequestFolderTypeField.Folder }));
            Assert.IsTrue(copiedFileRequest.Id != fileRequestId);
            Assert.IsTrue(copiedFileRequest.Title == fileRequest.Title);
            Assert.IsTrue(copiedFileRequest.Description == fileRequest.Description);
            FileRequest updatedFileRequest = await client.FileRequests.UpdateFileRequestByIdAsync(fileRequestId: copiedFileRequest.Id, requestBody: new FileRequestUpdateRequest() { Title = "updated title", Description = "updated description" });
            Assert.IsTrue(updatedFileRequest.Id == copiedFileRequest.Id);
            Assert.IsTrue(updatedFileRequest.Title == "updated title");
            Assert.IsTrue(updatedFileRequest.Description == "updated description");
            await client.FileRequests.DeleteFileRequestByIdAsync(fileRequestId: updatedFileRequest.Id);
            await Assert.That.IsExceptionAsync(async() => await client.FileRequests.GetFileRequestByIdAsync(fileRequestId: updatedFileRequest.Id));
        }

    }
}