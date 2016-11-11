using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxWebLinkManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task WebLinks_CRUD_LiveSession()
        {
            const string subFolderId = "1927308583";
            var url = new Uri("http://www.box.com");
            const string description = "A weblink to Box.com";
            const string name = "Box.com website";

            //create weblink
            var wlr = new BoxWebLinkRequest() { Url = url, Name = name, Description = description, Parent = new BoxRequestEntity() { Id = "0" } };
            var weblink = await _client.WebLinksManager.CreateWebLinkAsync(wlr);
            Assert.AreEqual(name, weblink.Name, "Failed to create weblink.");
            Assert.AreEqual(url, weblink.Url);

            //get weblink
            var fetchedWeblink = await _client.WebLinksManager.GetWebLinkAsync(weblink.Id);
            Assert.AreEqual(weblink.Id, fetchedWeblink.Id, "Failed to fetch existing weblink.");
            Assert.AreEqual(weblink.Name, fetchedWeblink.Name, "Failed to fetch existing weblink.");

            //update weblink
            var newUrl = new Uri("http://www.google.com");
            var newName = "Google website";
            var newDescription = "A weblink to Google.com";
            wlr = new BoxWebLinkRequest() { Url = newUrl, Description = newDescription, Name = newName };
            var updatedWeblink = await _client.WebLinksManager.UpdateWebLinkAsync(fetchedWeblink.Id, wlr);
            Assert.AreEqual(fetchedWeblink.Id, updatedWeblink.Id, "Failed to update existing weblink.");
            Assert.AreEqual(newUrl, updatedWeblink.Url, "Failed to update existing weblink.");
            Assert.AreEqual(newDescription, updatedWeblink.Description, "Failed to update existing weblink.");
            Assert.AreEqual(newName, updatedWeblink.Name, "Failed to update existing weblink.");

            //delete weblink
            var result = await _client.WebLinksManager.DeleteWebLinkAsync(updatedWeblink.Id);
            Assert.IsTrue(result, "Failed to delete weblink.");

        }
    }
}
