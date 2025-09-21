using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class SharedLinksFilesManagerTests {
        public BoxClient client { get; }

        public SharedLinksFilesManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestSharedLinksFiles() {
            Files uploadedFiles = await client.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: Utils.GetUUID(), parent: new UploadFileRequestBodyAttributesParentField(id: "0")), file: Utils.GenerateByteStream(size: 10)));
            string fileId = NullableUtils.Unwrap(uploadedFiles.Entries)[0].Id;
            await client.SharedLinksFiles.AddShareLinkToFileAsync(fileId: fileId, requestBody: new AddShareLinkToFileRequestBody() { SharedLink = new AddShareLinkToFileRequestBodySharedLinkField() { Access = AddShareLinkToFileRequestBodySharedLinkAccessField.Open, Password = "Secret123@" } }, queryParams: new AddShareLinkToFileQueryParams(fields: "shared_link"));
            FileFull fileFromApi = await client.SharedLinksFiles.GetSharedLinkForFileAsync(fileId: fileId, queryParams: new GetSharedLinkForFileQueryParams(fields: "shared_link"));
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(fileFromApi.SharedLink).Access) == "open");
            string userId = Utils.GetEnvVar(name: "USER_ID");
            BoxClient userClient = new CommonsManager().GetDefaultClientWithUserSubject(userId: userId);
            FileFull fileFromSharedLinkPassword = await userClient.SharedLinksFiles.FindFileForSharedLinkAsync(queryParams: new FindFileForSharedLinkQueryParams(), headers: new FindFileForSharedLinkHeaders(boxapi: string.Concat("shared_link=", NullableUtils.Unwrap(fileFromApi.SharedLink).Url, "&shared_link_password=Secret123@")));
            Assert.IsTrue(fileId == fileFromSharedLinkPassword.Id);
            await Assert.That.IsExceptionAsync(async() => await userClient.SharedLinksFiles.FindFileForSharedLinkAsync(queryParams: new FindFileForSharedLinkQueryParams(), headers: new FindFileForSharedLinkHeaders(boxapi: string.Concat("shared_link=", NullableUtils.Unwrap(fileFromApi.SharedLink).Url, "&shared_link_password=incorrectPassword"))));
            FileFull updatedFile = await client.SharedLinksFiles.UpdateSharedLinkOnFileAsync(fileId: fileId, requestBody: new UpdateSharedLinkOnFileRequestBody() { SharedLink = new UpdateSharedLinkOnFileRequestBodySharedLinkField() { Access = UpdateSharedLinkOnFileRequestBodySharedLinkAccessField.Collaborators } }, queryParams: new UpdateSharedLinkOnFileQueryParams(fields: "shared_link"));
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(updatedFile.SharedLink).Access) == "collaborators");
            await client.SharedLinksFiles.RemoveSharedLinkFromFileAsync(fileId: fileId, requestBody: new RemoveSharedLinkFromFileRequestBody() { SharedLink = null }, queryParams: new RemoveSharedLinkFromFileQueryParams(fields: "shared_link"));
            FileFull fileFromApiAfterRemove = await client.SharedLinksFiles.GetSharedLinkForFileAsync(fileId: fileId, queryParams: new GetSharedLinkForFileQueryParams(fields: "shared_link"));
            Assert.IsTrue(fileFromApiAfterRemove.SharedLink == null);
            await client.Files.DeleteFileByIdAsync(fileId: fileId);
        }

    }
}