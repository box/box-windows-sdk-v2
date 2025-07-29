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
    public class ClientManagerTests {
        public BoxClient client { get; }

        public ClientManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestMakeRequestJsonCrud() {
            string newFolderName = Utils.GetUUID();
            string requestBodyPost = string.Concat("{\"name\": \"", newFolderName, "\", \"parent\": { \"id\": \"0\"}}");
            FetchResponse createFolderResponse = await client.MakeRequestAsync(fetchOptions: new FetchOptions(method: "post", url: "https://api.box.com/2.0/folders") { Data = JsonUtils.JsonToSerializedData(text: requestBodyPost) });
            Assert.IsTrue(createFolderResponse.Status == 201);
            SerializedData createdFolder = NullableUtils.Unwrap(createFolderResponse.Data);
            Assert.IsTrue(JsonUtils.GetSdValueByKey(obj: createdFolder, key: "name") == newFolderName);
            string updatedName = Utils.GetUUID();
            string requestBodyPut = string.Concat("{\"name\": \"", updatedName, "\"}");
            FetchResponse updateFolderResponse = await client.MakeRequestAsync(fetchOptions: new FetchOptions(method: "put", url: string.Concat("https://api.box.com/2.0/folders/", JsonUtils.GetSdValueByKey(obj: createdFolder, key: "id"))) { Data = JsonUtils.JsonToSerializedData(text: requestBodyPut) });
            Assert.IsTrue(updateFolderResponse.Status == 200);
            SerializedData updatedFolder = NullableUtils.Unwrap(updateFolderResponse.Data);
            Assert.IsTrue(JsonUtils.GetSdValueByKey(obj: updatedFolder, key: "name") == updatedName);
            Assert.IsTrue(JsonUtils.GetSdValueByKey(obj: updatedFolder, key: "id") == JsonUtils.GetSdValueByKey(obj: createdFolder, key: "id"));
            FetchResponse getFolderResponse = await client.MakeRequestAsync(fetchOptions: new FetchOptions(url: string.Concat("https://api.box.com/2.0/folders/", JsonUtils.GetSdValueByKey(obj: createdFolder, key: "id")), method: "GET"));
            Assert.IsTrue(getFolderResponse.Status == 200);
            SerializedData receivedFolder = NullableUtils.Unwrap(getFolderResponse.Data);
            Assert.IsTrue(JsonUtils.GetSdValueByKey(obj: receivedFolder, key: "name") == updatedName);
            Assert.IsTrue(JsonUtils.GetSdValueByKey(obj: receivedFolder, key: "id") == JsonUtils.GetSdValueByKey(obj: updatedFolder, key: "id"));
            FetchResponse deleteFolderResponse = await client.MakeRequestAsync(fetchOptions: new FetchOptions(url: string.Concat("https://api.box.com/2.0/folders/", JsonUtils.GetSdValueByKey(obj: receivedFolder, key: "id")), method: "DELETE"));
            Assert.IsTrue(deleteFolderResponse.Status == 204);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestMakeRequestMultipart() {
            string newFolderName = Utils.GetUUID();
            FolderFull newFolder = await client.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: newFolderName, parent: new CreateFolderRequestBodyParentField(id: "0")));
            string newFolderId = newFolder.Id;
            string newFileName = string.Concat(Utils.GetUUID(), ".pdf");
            System.IO.Stream fileContentStream = Utils.GenerateByteStream(size: 1024 * 1024);
            string multipartAttributes = string.Concat("{\"name\": \"", newFileName, "\", \"parent\": { \"id\":", newFolderId, "}}");
            FetchResponse uploadFileResponse = await client.MakeRequestAsync(fetchOptions: new FetchOptions(method: "POST", url: "https://upload.box.com/api/2.0/files/content", contentType: "multipart/form-data") { MultipartData = Array.AsReadOnly(new [] {new MultipartItem(partName: "attributes") { Data = JsonUtils.JsonToSerializedData(text: multipartAttributes) },new MultipartItem(partName: "file") { FileStream = fileContentStream }}) });
            Assert.IsTrue(uploadFileResponse.Status == 201);
            await client.Folders.DeleteFolderByIdAsync(folderId: newFolderId, queryParams: new DeleteFolderByIdQueryParams() { Recursive = true });
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestMakeRequestBinaryFormat() {
            string newFileName = Utils.GetUUID();
            byte[] fileBuffer = Utils.GenerateByteBuffer(size: 1024 * 1024);
            System.IO.Stream fileContentStream = Utils.GenerateByteStreamFromBuffer(buffer: fileBuffer);
            Files uploadedFiles = await client.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: newFileName, parent: new UploadFileRequestBodyAttributesParentField(id: "0")), file: fileContentStream));
            FileFull uploadedFile = NullableUtils.Unwrap(uploadedFiles.Entries)[0];
            FetchResponse downloadFileResponse = await client.MakeRequestAsync(fetchOptions: new FetchOptions(method: "GET", url: string.Concat("https://api.box.com/2.0/files/", uploadedFile.Id, "/content"), responseFormat: Box.Sdk.Gen.ResponseFormat.Binary));
            Assert.IsTrue(downloadFileResponse.Status == 200);
            Assert.IsTrue(Utils.BufferEquals(buffer1: await Utils.ReadByteStreamAsync(byteStream: NullableUtils.Unwrap(downloadFileResponse.Content)), buffer2: fileBuffer));
            await client.Files.DeleteFileByIdAsync(fileId: uploadedFile.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestWithAsUserHeader() {
            string userName = Utils.GetUUID();
            UserFull createdUser = await client.Users.CreateUserAsync(requestBody: new CreateUserRequestBody(name: userName) { IsPlatformAccessOnly = true });
            BoxClient asUserClient = client.WithAsUserHeader(userId: createdUser.Id);
            UserFull adminUser = await client.Users.GetUserMeAsync();
            Assert.IsTrue(StringUtils.ToStringRepresentation(adminUser.Name) != userName);
            UserFull appUser = await asUserClient.Users.GetUserMeAsync();
            Assert.IsTrue(StringUtils.ToStringRepresentation(appUser.Name) == userName);
            await client.Users.DeleteUserByIdAsync(userId: createdUser.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestWithSuppressedNotifications() {
            BoxClient newClient = client.WithSuppressedNotifications();
            UserFull user = await newClient.Users.GetUserMeAsync();
            Assert.IsTrue(user.Id != "");
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestWithExtraHeaders() {
            string userName = Utils.GetUUID();
            UserFull createdUser = await client.Users.CreateUserAsync(requestBody: new CreateUserRequestBody(name: userName) { IsPlatformAccessOnly = true });
            BoxClient asUserClient = client.WithExtraHeaders(extraHeaders: new Dictionary<string, string>() { { "As-User", createdUser.Id } });
            UserFull adminUser = await client.Users.GetUserMeAsync();
            Assert.IsTrue(StringUtils.ToStringRepresentation(adminUser.Name) != userName);
            UserFull appUser = await asUserClient.Users.GetUserMeAsync();
            Assert.IsTrue(StringUtils.ToStringRepresentation(appUser.Name) == userName);
            await client.Users.DeleteUserByIdAsync(userId: createdUser.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestWithCustomBaseUrls() {
            BaseUrls newBaseUrls = new BaseUrls(baseUrl: "https://box.com/", uploadUrl: "https://box.com/", oauth2Url: "https://box.com/");
            BoxClient customBaseClient = client.WithCustomBaseUrls(baseUrls: newBaseUrls);
            await Assert.That.IsExceptionAsync(async() => await customBaseClient.Users.GetUserMeAsync());
        }

    }
}