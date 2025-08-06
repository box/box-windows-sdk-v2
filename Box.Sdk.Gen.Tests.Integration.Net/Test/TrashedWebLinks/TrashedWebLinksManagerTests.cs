using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class TrashedWebLinksManagerTests {
        public BoxClient client { get; }

        public TrashedWebLinksManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestTrashedWebLinks() {
            const string url = "https://www.box.com";
            FolderFull parent = await client.Folders.GetFolderByIdAsync(folderId: "0");
            string name = Utils.GetUUID();
            const string description = "Weblink description";
            WebLink weblink = await client.WebLinks.CreateWebLinkAsync(requestBody: new CreateWebLinkRequestBody(url: url, parent: new CreateWebLinkRequestBodyParentField(id: parent.Id)) { Name = name, Description = description });
            await client.WebLinks.DeleteWebLinkByIdAsync(webLinkId: weblink.Id);
            TrashWebLink fromTrash = await client.TrashedWebLinks.GetTrashedWebLinkByIdAsync(webLinkId: weblink.Id);
            Assert.IsTrue(fromTrash.Id == weblink.Id);
            Assert.IsTrue(fromTrash.Name == weblink.Name);
            WebLink fromApiAfterTrashed = await client.WebLinks.GetWebLinkByIdAsync(webLinkId: weblink.Id);
            Assert.IsTrue(StringUtils.ToStringRepresentation(fromApiAfterTrashed.ItemStatus) == "trashed");
            TrashWebLinkRestored restoredWeblink = await client.TrashedWebLinks.RestoreWeblinkFromTrashAsync(webLinkId: weblink.Id);
            WebLink fromApi = await client.WebLinks.GetWebLinkByIdAsync(webLinkId: weblink.Id);
            Assert.IsTrue(restoredWeblink.Id == fromApi.Id);
            Assert.IsTrue(restoredWeblink.Name == fromApi.Name);
            await client.WebLinks.DeleteWebLinkByIdAsync(webLinkId: weblink.Id);
            await client.TrashedWebLinks.DeleteTrashedWebLinkByIdAsync(webLinkId: weblink.Id);
            await Assert.That.IsExceptionAsync(async() => await client.TrashedWebLinks.GetTrashedWebLinkByIdAsync(webLinkId: weblink.Id));
        }

    }
}