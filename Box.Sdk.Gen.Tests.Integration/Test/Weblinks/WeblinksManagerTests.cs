using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class WeblinksManagerTests {
        public BoxClient client { get; }

        public WeblinksManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateGetDeleteWeblink() {
            const string url = "https://www.box.com";
            FolderFull parent = await client.Folders.GetFolderByIdAsync(folderId: "0");
            string name = Utils.GetUUID();
            const string description = "Weblink description";
            const string password = "super-secret-password";
            WebLink weblink = await client.WebLinks.CreateWebLinkAsync(requestBody: new CreateWebLinkRequestBody(url: url, parent: new CreateWebLinkRequestBodyParentField(id: parent.Id)) { Name = name, Description = description });
            Assert.IsTrue(weblink.Url == url);
            Assert.IsTrue(NullableUtils.Unwrap(weblink.Parent).Id == parent.Id);
            Assert.IsTrue(weblink.Name == name);
            Assert.IsTrue(weblink.Description == description);
            WebLink weblinkById = await client.WebLinks.GetWebLinkByIdAsync(webLinkId: weblink.Id);
            Assert.IsTrue(weblinkById.Id == weblink.Id);
            Assert.IsTrue(weblinkById.Url == url);
            string updatedName = Utils.GetUUID();
            WebLink updatedWeblink = await client.WebLinks.UpdateWebLinkByIdAsync(webLinkId: weblink.Id, requestBody: new UpdateWebLinkByIdRequestBody() { Name = updatedName, SharedLink = new UpdateWebLinkByIdRequestBodySharedLinkField() { Access = UpdateWebLinkByIdRequestBodySharedLinkAccessField.Open, Password = password } });
            Assert.IsTrue(updatedWeblink.Name == updatedName);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(NullableUtils.Unwrap(updatedWeblink.SharedLink).Access)) == "open");
            await client.WebLinks.DeleteWebLinkByIdAsync(webLinkId: weblink.Id);
            WebLink deletedWeblink = await client.WebLinks.GetWebLinkByIdAsync(webLinkId: weblink.Id);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(deletedWeblink.ItemStatus)) == "trashed");
        }

    }
}