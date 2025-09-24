using System;
using System.Threading.Tasks;
using Box.V2.Exceptions;
using Box.V2.Models;
using Box.V2.Test.Integration.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxWebLinkManagerIntegrationTest : TestInFolder
    {
        [TestMethod]
        public async Task CreateWebLinkAsync_ForCorrectWeblinkRequest_ShouldCreateNewWeblink()
        {
            var url = new Uri("http://www.box.com");
            const string Description = "A weblink to Box.com";
            const string Name = "Box.com website";
            var webLinkRequest = new BoxWebLinkRequest() { Url = url, Name = Name, Description = Description, Parent = new BoxRequestEntity() { Id = "0" } };

            var weblink = await UserClient.WebLinksManager.CreateWebLinkAsync(webLinkRequest);

            Assert.AreEqual(Name, weblink.Name);
            Assert.AreEqual(url, weblink.Url);

            await UserClient.WebLinksManager.DeleteWebLinkAsync(weblink.Id);
        }

        [TestMethod]
        public async Task GetWebLinkAsync_ForExistingWebLink_ShouldReturnWebLink()
        {
            var webLink = await CreateWebLink(GetUniqueName("weblink"), FolderId);

            var fetchedWeblink = await UserClient.WebLinksManager.GetWebLinkAsync(webLink.Id);

            Assert.AreEqual(webLink.Id, fetchedWeblink.Id);
        }

        [TestMethod]
        public async Task UpdateWebLinkAsync_ForExistingWebLink_ShouldUpdateWebLink()
        {
            var webLink = await CreateWebLink(GetUniqueName("weblink"), FolderId);

            var newUrl = new Uri("http://www.box.com/v2");
            var updateWebLinkRequest = new BoxWebLinkRequest() { Url = newUrl };

            var updatedWeblink = await UserClient.WebLinksManager.UpdateWebLinkAsync(webLink.Id, updateWebLinkRequest);

            Assert.AreEqual(updatedWeblink.Url, newUrl);
        }

        [TestMethod]
        public async Task DeleteWebLinkAsync_ForExistingWebLink_ShouldDeleteWebLinkAndExceptionShouldBeThrown()
        {
            var url = new Uri("http://www.box.com");
            const string Description = "A weblink to Box.com";
            const string Name = "Box.com website";
            var webLinkRequest = new BoxWebLinkRequest() { Url = url, Name = Name, Description = Description, Parent = new BoxRequestEntity() { Id = FolderId } };
            var weblink = await UserClient.WebLinksManager.CreateWebLinkAsync(webLinkRequest);

            var result = await UserClient.WebLinksManager.DeleteWebLinkAsync(weblink.Id);

            await Assert.ThrowsExceptionAsync<BoxAPIException>(async () => { _ = await UserClient.WebLinksManager.GetWebLinkAsync(weblink.Id); });
        }

        [TestMethod]
        public async Task AddSharedLink_ForNewWeblink_ShouldCreateNewSharedLink()
        {
            var webLink = await CreateWebLink(GetUniqueName("weblink"), FolderId);

            var sharedLinkReq = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open
            };

            var response = await UserClient.WebLinksManager.CreateSharedLinkAsync(webLink.Id, sharedLinkReq);

            Assert.AreEqual(webLink.Id, response.Id);
            Assert.AreEqual(BoxSharedLinkAccessType.open, response.SharedLink.Access);
        }
    }
}
