using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class SharedLinksWebLinksManagerTests {
        public BoxClient client { get; }

        public SharedLinksWebLinksManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestSharedLinksWebLinks() {
            FolderFull parent = await client.Folders.GetFolderByIdAsync(folderId: "0");
            WebLink webLink = await client.WebLinks.CreateWebLinkAsync(requestBody: new CreateWebLinkRequestBody(url: "https://www.box.com", parent: new CreateWebLinkRequestBodyParentField(id: parent.Id)) { Name = Utils.GetUUID(), Description = "Weblink description" });
            string webLinkId = webLink.Id;
            await client.SharedLinksWebLinks.AddShareLinkToWebLinkAsync(webLinkId: webLinkId, requestBody: new AddShareLinkToWebLinkRequestBody() { SharedLink = new AddShareLinkToWebLinkRequestBodySharedLinkField() { Access = AddShareLinkToWebLinkRequestBodySharedLinkAccessField.Open, Password = "Secret123@" } }, queryParams: new AddShareLinkToWebLinkQueryParams(fields: "shared_link"));
            WebLink webLinkFromApi = await client.SharedLinksWebLinks.GetSharedLinkForWebLinkAsync(webLinkId: webLinkId, queryParams: new GetSharedLinkForWebLinkQueryParams(fields: "shared_link"));
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(webLinkFromApi.SharedLink).Access) == "open");
            string userId = Utils.GetEnvVar(name: "USER_ID");
            BoxClient userClient = new CommonsManager().GetDefaultClientWithUserSubject(userId: userId);
            WebLink webLinkFromSharedLinkPassword = await userClient.SharedLinksWebLinks.FindWebLinkForSharedLinkAsync(queryParams: new FindWebLinkForSharedLinkQueryParams(), headers: new FindWebLinkForSharedLinkHeaders(boxapi: string.Concat("shared_link=", NullableUtils.Unwrap(webLinkFromApi.SharedLink).Url, "&shared_link_password=Secret123@")));
            Assert.IsTrue(webLinkId == webLinkFromSharedLinkPassword.Id);
            await Assert.That.IsExceptionAsync(async() => await userClient.SharedLinksWebLinks.FindWebLinkForSharedLinkAsync(queryParams: new FindWebLinkForSharedLinkQueryParams(), headers: new FindWebLinkForSharedLinkHeaders(boxapi: string.Concat("shared_link=", NullableUtils.Unwrap(webLinkFromApi.SharedLink).Url, "&shared_link_password=incorrectPassword"))));
            WebLink updatedWebLink = await client.SharedLinksWebLinks.UpdateSharedLinkOnWebLinkAsync(webLinkId: webLinkId, requestBody: new UpdateSharedLinkOnWebLinkRequestBody() { SharedLink = new UpdateSharedLinkOnWebLinkRequestBodySharedLinkField() { Access = UpdateSharedLinkOnWebLinkRequestBodySharedLinkAccessField.Collaborators } }, queryParams: new UpdateSharedLinkOnWebLinkQueryParams(fields: "shared_link"));
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(updatedWebLink.SharedLink).Access) == "collaborators");
            await client.SharedLinksWebLinks.RemoveSharedLinkFromWebLinkAsync(webLinkId: webLinkId, requestBody: new RemoveSharedLinkFromWebLinkRequestBody() { SharedLink = null }, queryParams: new RemoveSharedLinkFromWebLinkQueryParams(fields: "shared_link"));
            WebLink webLinkFromApiAfterRemove = await client.SharedLinksWebLinks.GetSharedLinkForWebLinkAsync(webLinkId: webLinkId, queryParams: new GetSharedLinkForWebLinkQueryParams(fields: "shared_link"));
            Assert.IsTrue(webLinkFromApiAfterRemove.SharedLink == null);
            await client.WebLinks.DeleteWebLinkByIdAsync(webLinkId: webLinkId);
        }

    }
}