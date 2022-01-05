using System;
using System.Threading.Tasks;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxWebLinkManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public async Task WebLinks_CRUD_LiveSession()
        {
            var url = new Uri("http://www.box.com");
            const string Description = "A weblink to Box.com";
            const string Name = "Box.com website";

            //create weblink
            var wlr = new BoxWebLinkRequest() { Url = url, Name = Name, Description = Description, Parent = new BoxRequestEntity() { Id = "0" } };
            var weblink = await Client.WebLinksManager.CreateWebLinkAsync(wlr);
            Assert.AreEqual(Name, weblink.Name, "Failed to create weblink.");
            Assert.AreEqual(url, weblink.Url);

            //get weblink
            var fetchedWeblink = await Client.WebLinksManager.GetWebLinkAsync(weblink.Id);
            Assert.AreEqual(weblink.Id, fetchedWeblink.Id, "Failed to fetch existing weblink.");
            Assert.AreEqual(weblink.Name, fetchedWeblink.Name, "Failed to fetch existing weblink.");

            //update weblink
            var newUrl = new Uri("http://www.google.com");
            var newName = "Google website";
            var newDescription = "A weblink to Google.com";
            wlr = new BoxWebLinkRequest() { Url = newUrl, Description = newDescription, Name = newName };
            var updatedWeblink = await Client.WebLinksManager.UpdateWebLinkAsync(fetchedWeblink.Id, wlr);
            Assert.AreEqual(fetchedWeblink.Id, updatedWeblink.Id, "Failed to update existing weblink.");
            Assert.AreEqual(newUrl, updatedWeblink.Url, "Failed to update existing weblink.");
            Assert.AreEqual(newDescription, updatedWeblink.Description, "Failed to update existing weblink.");
            Assert.AreEqual(newName, updatedWeblink.Name, "Failed to update existing weblink.");

            //delete weblink
            var result = await Client.WebLinksManager.DeleteWebLinkAsync(updatedWeblink.Id);
            Assert.IsTrue(result, "Failed to delete weblink.");

        }
    }
}
