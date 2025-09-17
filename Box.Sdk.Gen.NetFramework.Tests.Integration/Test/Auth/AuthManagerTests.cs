using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class AuthManagerTests {
        [RetryableTest]
        public async System.Threading.Tasks.Task TestJwtAuth() {
            string userId = Utils.GetEnvVar(name: "USER_ID");
            string enterpriseId = Utils.GetEnvVar(name: "ENTERPRISE_ID");
            JwtConfig jwtConfig = JwtConfig.FromConfigJsonString(configJsonString: Utils.DecodeBase64(value: Utils.GetEnvVar(name: "JWT_CONFIG_BASE_64")));
            BoxJwtAuth auth = new BoxJwtAuth(config: jwtConfig);
            BoxJwtAuth userAuth = auth.WithUserSubject(userId: userId);
            BoxClient userClient = new BoxClient(auth: userAuth);
            UserFull currentUser = await userClient.Users.GetUserMeAsync();
            Assert.IsTrue(currentUser.Id == userId);
            BoxJwtAuth enterpriseAuth = auth.WithEnterpriseSubject(enterpriseId: enterpriseId);
            BoxClient enterpriseClient = new BoxClient(auth: enterpriseAuth);
            UserFull newUser = await enterpriseClient.Users.GetUserMeAsync(queryParams: new GetUserMeQueryParams() { Fields = Array.AsReadOnly(new [] {"enterprise"}) });
            Assert.IsTrue(newUser.Enterprise != null);
            Assert.IsTrue(NullableUtils.Unwrap(newUser.Enterprise).Id == enterpriseId);
            Assert.IsTrue(newUser.Id != userId);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestJwtAuthDownscope() {
            JwtConfig jwtConfig = JwtConfig.FromConfigJsonString(configJsonString: Utils.DecodeBase64(value: Utils.GetEnvVar(name: "JWT_CONFIG_BASE_64")));
            BoxJwtAuth auth = new BoxJwtAuth(config: jwtConfig);
            BoxClient parentClient = new BoxClient(auth: auth);
            Files uploadedFiles = await parentClient.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: Utils.GetUUID(), parent: new UploadFileRequestBodyAttributesParentField(id: "0")), file: Utils.GenerateByteStream(size: 1024 * 1024)));
            FileFull file = NullableUtils.Unwrap(uploadedFiles.Entries)[0];
            string resourcePath = string.Concat("https://api.box.com/2.0/files/", file.Id);
            AccessToken downscopedToken = await auth.DownscopeTokenAsync(scopes: Array.AsReadOnly(new [] {"item_rename","item_preview"}), resource: resourcePath);
            Assert.IsTrue(downscopedToken.AccessTokenField != null);
            BoxClient downscopedClient = new BoxClient(auth: new BoxDeveloperTokenAuth(token: downscopedToken.AccessTokenField));
            await downscopedClient.Files.UpdateFileByIdAsync(fileId: file.Id, requestBody: new UpdateFileByIdRequestBody() { Name = Utils.GetUUID() });
            await Assert.That.IsExceptionAsync(async() => await downscopedClient.Files.DeleteFileByIdAsync(fileId: file.Id));
            await parentClient.Files.DeleteFileByIdAsync(fileId: file.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestJwtDownscopeTokenSucceedsIfNoTokenAvailable() {
            JwtConfig jwtConfig = JwtConfig.FromConfigJsonString(configJsonString: Utils.DecodeBase64(value: Utils.GetEnvVar(name: "JWT_CONFIG_BASE_64")));
            BoxJwtAuth auth = new BoxJwtAuth(config: jwtConfig);
            AccessToken downscopedToken = await auth.DownscopeTokenAsync(scopes: Array.AsReadOnly(new [] {"root_readonly"}));
            Assert.IsTrue(downscopedToken.AccessTokenField != null);
            BoxClient downscopedClient = new BoxClient(auth: new BoxDeveloperTokenAuth(token: downscopedToken.AccessTokenField));
            await Assert.That.IsExceptionAsync(async() => await downscopedClient.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: Utils.GetUUID(), parent: new UploadFileRequestBodyAttributesParentField(id: "0")), file: Utils.GenerateByteStream(size: 1024 * 1024))));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestJwtAuthRevoke() {
            JwtConfig jwtConfig = JwtConfig.FromConfigJsonString(configJsonString: Utils.DecodeBase64(value: Utils.GetEnvVar(name: "JWT_CONFIG_BASE_64")));
            BoxJwtAuth auth = new BoxJwtAuth(config: jwtConfig);
            AccessToken tokenFromStorageBeforeRevoke = await auth.RetrieveTokenAsync();
            Assert.IsTrue(tokenFromStorageBeforeRevoke != null);
            await auth.RevokeTokenAsync();
            AccessToken tokenFromStorageAfterRevoke = await auth.RetrieveTokenAsync();
            Assert.IsTrue(tokenFromStorageBeforeRevoke.AccessTokenField != tokenFromStorageAfterRevoke.AccessTokenField);
        }

        [RetryableTest]
        public void TestOauthAuthAuthorizeUrl() {
            OAuthConfig config = new OAuthConfig(clientId: "OAUTH_CLIENT_ID", clientSecret: "OAUTH_CLIENT_SECRET");
            BoxOAuth auth = new BoxOAuth(config: config);
            string authUrl = auth.GetAuthorizeUrl();
            Assert.IsTrue(authUrl == "https://account.box.com/api/oauth2/authorize?client_id=OAUTH_CLIENT_ID&response_type=code" || authUrl == "https://account.box.com/api/oauth2/authorize?response_type=code&client_id=OAUTH_CLIENT_ID");
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestOauthDownscopeTokenSucceedsIfNoTokenAvailable() {
            OAuthConfig config = new OAuthConfig(clientId: Utils.GetEnvVar(name: "CLIENT_ID"), clientSecret: Utils.GetEnvVar(name: "CLIENT_SECRET"));
            BoxOAuth auth = new BoxOAuth(config: config);
            string resourcePath = string.Concat("https://api.box.com/2.0/files/12345");
            await Assert.That.IsExceptionAsync(async() => await auth.DownscopeTokenAsync(scopes: Array.AsReadOnly(new [] {"item_rename","item_preview"}), resource: resourcePath));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestCcgAuth() {
            string userId = Utils.GetEnvVar(name: "USER_ID");
            string enterpriseId = Utils.GetEnvVar(name: "ENTERPRISE_ID");
            CcgConfig ccgConfig = new CcgConfig(clientId: Utils.GetEnvVar(name: "CLIENT_ID"), clientSecret: Utils.GetEnvVar(name: "CLIENT_SECRET")) { EnterpriseId = enterpriseId, UserId = userId };
            BoxCcgAuth auth = new BoxCcgAuth(config: ccgConfig);
            BoxCcgAuth userAuth = auth.WithUserSubject(userId: userId);
            BoxClient userClient = new BoxClient(auth: userAuth);
            UserFull currentUser = await userClient.Users.GetUserMeAsync();
            Assert.IsTrue(currentUser.Id == userId);
            BoxCcgAuth enterpriseAuth = auth.WithEnterpriseSubject(enterpriseId: enterpriseId);
            BoxClient enterpriseClient = new BoxClient(auth: enterpriseAuth);
            UserFull newUser = await enterpriseClient.Users.GetUserMeAsync(queryParams: new GetUserMeQueryParams() { Fields = Array.AsReadOnly(new [] {"enterprise"}) });
            Assert.IsTrue(newUser.Enterprise != null);
            Assert.IsTrue(NullableUtils.Unwrap(newUser.Enterprise).Id == enterpriseId);
            Assert.IsTrue(newUser.Id != userId);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestCcgAuthDownscope() {
            CcgConfig ccgConfig = new CcgConfig(clientId: Utils.GetEnvVar(name: "CLIENT_ID"), clientSecret: Utils.GetEnvVar(name: "CLIENT_SECRET")) { UserId = Utils.GetEnvVar(name: "USER_ID") };
            BoxCcgAuth auth = new BoxCcgAuth(config: ccgConfig);
            BoxClient parentClient = new BoxClient(auth: auth);
            FolderFull folder = await parentClient.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: Utils.GetUUID(), parent: new CreateFolderRequestBodyParentField(id: "0")));
            string resourcePath = string.Concat("https://api.box.com/2.0/folders/", folder.Id);
            AccessToken downscopedToken = await auth.DownscopeTokenAsync(scopes: Array.AsReadOnly(new [] {"item_rename","item_preview"}), resource: resourcePath);
            Assert.IsTrue(downscopedToken.AccessTokenField != null);
            BoxClient downscopedClient = new BoxClient(auth: new BoxDeveloperTokenAuth(token: downscopedToken.AccessTokenField));
            await downscopedClient.Folders.UpdateFolderByIdAsync(folderId: folder.Id, requestBody: new UpdateFolderByIdRequestBody() { Name = Utils.GetUUID() });
            await Assert.That.IsExceptionAsync(async() => await downscopedClient.Folders.DeleteFolderByIdAsync(folderId: folder.Id));
            await parentClient.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestCcgDownscopeTokenSucceedsIfNoTokenAvailable() {
            CcgConfig ccgConfig = new CcgConfig(clientId: Utils.GetEnvVar(name: "CLIENT_ID"), clientSecret: Utils.GetEnvVar(name: "CLIENT_SECRET")) { UserId = Utils.GetEnvVar(name: "USER_ID") };
            BoxCcgAuth auth = new BoxCcgAuth(config: ccgConfig);
            AccessToken downscopedToken = await auth.DownscopeTokenAsync(scopes: Array.AsReadOnly(new [] {"root_readonly"}));
            Assert.IsTrue(downscopedToken.AccessTokenField != null);
            BoxClient downscopedClient = new BoxClient(auth: new BoxDeveloperTokenAuth(token: downscopedToken.AccessTokenField));
            await Assert.That.IsExceptionAsync(async() => await downscopedClient.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: Utils.GetUUID(), parent: new UploadFileRequestBodyAttributesParentField(id: "0")), file: Utils.GenerateByteStream(size: 1024 * 1024))));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestCcgAuthRevoke() {
            CcgConfig ccgConfig = new CcgConfig(clientId: Utils.GetEnvVar(name: "CLIENT_ID"), clientSecret: Utils.GetEnvVar(name: "CLIENT_SECRET")) { UserId = Utils.GetEnvVar(name: "USER_ID") };
            BoxCcgAuth auth = new BoxCcgAuth(config: ccgConfig);
            AccessToken tokenFromStorageBeforeRevoke = await auth.RetrieveTokenAsync();
            Assert.IsTrue(tokenFromStorageBeforeRevoke != null);
            await auth.RevokeTokenAsync();
            AccessToken tokenFromStorageAfterRevoke = await auth.RetrieveTokenAsync();
            Assert.IsTrue(tokenFromStorageBeforeRevoke.AccessTokenField != tokenFromStorageAfterRevoke.AccessTokenField);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestDeveloperDownscopeTokenSucceedsIfNoTokenAvailable() {
            DeveloperTokenConfig developerTokenConfig = new DeveloperTokenConfig() { ClientId = Utils.GetEnvVar(name: "CLIENT_ID"), ClientSecret = Utils.GetEnvVar(name: "CLIENT_SECRET") };
            BoxDeveloperTokenAuth auth = new BoxDeveloperTokenAuth(token: "", config: developerTokenConfig);
            string resourcePath = string.Concat("https://api.box.com/2.0/folders/12345");
            await Assert.That.IsExceptionAsync(async() => await auth.DownscopeTokenAsync(scopes: Array.AsReadOnly(new [] {"item_rename","item_preview"}), resource: resourcePath));
        }

        public async System.Threading.Tasks.Task<AccessToken> GetAccessTokenAsync() {
            string userId = Utils.GetEnvVar(name: "USER_ID");
            string enterpriseId = Utils.GetEnvVar(name: "ENTERPRISE_ID");
            CcgConfig ccgConfig = new CcgConfig(clientId: Utils.GetEnvVar(name: "CLIENT_ID"), clientSecret: Utils.GetEnvVar(name: "CLIENT_SECRET")) { EnterpriseId = enterpriseId, UserId = userId };
            BoxCcgAuth auth = new BoxCcgAuth(config: ccgConfig);
            BoxCcgAuth authUser = auth.WithUserSubject(userId: userId);
            return await authUser.RetrieveTokenAsync();
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestDeveloperTokenAuthRevoke() {
            DeveloperTokenConfig developerTokenConfig = new DeveloperTokenConfig() { ClientId = Utils.GetEnvVar(name: "CLIENT_ID"), ClientSecret = Utils.GetEnvVar(name: "CLIENT_SECRET") };
            AccessToken token = await GetAccessTokenAsync();
            BoxDeveloperTokenAuth auth = new BoxDeveloperTokenAuth(token: token.AccessTokenField, config: developerTokenConfig);
            AccessToken tokenFromStorageBeforeRevoke = await auth.RetrieveTokenAsync();
            Assert.IsTrue(tokenFromStorageBeforeRevoke != null);
            await auth.RevokeTokenAsync();
            await Assert.That.IsExceptionAsync(async() => await auth.RetrieveTokenAsync());
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestDeveloperTokenAuthDownscope() {
            DeveloperTokenConfig developerTokenConfig = new DeveloperTokenConfig() { ClientId = Utils.GetEnvVar(name: "CLIENT_ID"), ClientSecret = Utils.GetEnvVar(name: "CLIENT_SECRET") };
            AccessToken token = await GetAccessTokenAsync();
            BoxDeveloperTokenAuth auth = new BoxDeveloperTokenAuth(token: token.AccessTokenField, config: developerTokenConfig);
            BoxClient parentClient = new BoxClient(auth: auth);
            FolderFull folder = await parentClient.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: Utils.GetUUID(), parent: new CreateFolderRequestBodyParentField(id: "0")));
            string resourcePath = string.Concat("https://api.box.com/2.0/folders/", folder.Id);
            AccessToken downscopedToken = await auth.DownscopeTokenAsync(scopes: Array.AsReadOnly(new [] {"item_rename","item_preview"}), resource: resourcePath);
            Assert.IsTrue(downscopedToken.AccessTokenField != null);
            BoxClient downscopedClient = new BoxClient(auth: new BoxDeveloperTokenAuth(token: downscopedToken.AccessTokenField));
            await downscopedClient.Folders.UpdateFolderByIdAsync(folderId: folder.Id, requestBody: new UpdateFolderByIdRequestBody() { Name = Utils.GetUUID() });
            await Assert.That.IsExceptionAsync(async() => await downscopedClient.Folders.DeleteFolderByIdAsync(folderId: folder.Id));
            await parentClient.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestDeveloperTokenAuth() {
            string userId = Utils.GetEnvVar(name: "USER_ID");
            AccessToken token = await GetAccessTokenAsync();
            BoxDeveloperTokenAuth devAuth = new BoxDeveloperTokenAuth(token: token.AccessTokenField);
            BoxClient client = new BoxClient(auth: devAuth);
            UserFull currentUser = await client.Users.GetUserMeAsync();
            Assert.IsTrue(currentUser.Id == userId);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestOauthAuthRevoke() {
            AccessToken token = await GetAccessTokenAsync();
            InMemoryTokenStorage tokenStorage = new InMemoryTokenStorage(token: token);
            OAuthConfig config = new OAuthConfig(clientId: Utils.GetEnvVar(name: "CLIENT_ID"), clientSecret: Utils.GetEnvVar(name: "CLIENT_SECRET"), tokenStorage: tokenStorage);
            BoxOAuth auth = new BoxOAuth(config: config);
            BoxClient client = new BoxClient(auth: auth);
            await client.Users.GetUserMeAsync();
            await auth.RevokeTokenAsync();
            await Assert.That.IsExceptionAsync(async() => await client.Users.GetUserMeAsync());
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestOauthAuthDownscope() {
            AccessToken token = await GetAccessTokenAsync();
            InMemoryTokenStorage tokenStorage = new InMemoryTokenStorage(token: token);
            OAuthConfig config = new OAuthConfig(clientId: Utils.GetEnvVar(name: "CLIENT_ID"), clientSecret: Utils.GetEnvVar(name: "CLIENT_SECRET"), tokenStorage: tokenStorage);
            BoxOAuth auth = new BoxOAuth(config: config);
            BoxClient parentClient = new BoxClient(auth: auth);
            Files uploadedFiles = await parentClient.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: Utils.GetUUID(), parent: new UploadFileRequestBodyAttributesParentField(id: "0")), file: Utils.GenerateByteStream(size: 1024 * 1024)));
            FileFull file = NullableUtils.Unwrap(uploadedFiles.Entries)[0];
            string resourcePath = string.Concat("https://api.box.com/2.0/files/", file.Id);
            AccessToken downscopedToken = await auth.DownscopeTokenAsync(scopes: Array.AsReadOnly(new [] {"item_rename","item_preview"}), resource: resourcePath);
            Assert.IsTrue(downscopedToken.AccessTokenField != null);
            BoxClient downscopedClient = new BoxClient(auth: new BoxDeveloperTokenAuth(token: downscopedToken.AccessTokenField));
            await downscopedClient.Files.UpdateFileByIdAsync(fileId: file.Id, requestBody: new UpdateFileByIdRequestBody() { Name = Utils.GetUUID() });
            await Assert.That.IsExceptionAsync(async() => await downscopedClient.Files.DeleteFileByIdAsync(fileId: file.Id));
            await parentClient.Files.DeleteFileByIdAsync(fileId: file.Id);
        }

    }
}