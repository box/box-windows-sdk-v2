using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxCollectionsManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task Collections_LiveSession()
        {
            var collections = await Client.CollectionsManager.GetCollectionsAsync();
            var favorites = collections.Entries.FirstOrDefault(c => c.CollectionType == "favorites");
            Assert.IsNotNull(favorites, "Could not find 'favorites' collection");

            //add a file to favorites
            const string FileId = "16894944949";
            var bcr = new BoxCollectionsRequest() { Collections = new List<BoxRequestEntity>() { new BoxRequestEntity() { Id = favorites.Id } } };
            var updatedFile = await Client.CollectionsManager.CreateOrDeleteCollectionsForFileAsync(FileId, bcr);
            Assert.AreEqual(updatedFile.Id, FileId, "Could not add item to favorites collection");

            //get list of collection items
            var items = await Client.CollectionsManager.GetCollectionItemsAsync(favorites.Id);
            Assert.AreEqual(1, items.Entries.Count, "Wrong number of items in favorites collection");
            Assert.AreEqual(items.Entries[0].Id, FileId, "Incorrect file in favorites collection");

            //clear out favorites
            bcr = new BoxCollectionsRequest() { Collections = new List<BoxRequestEntity>() };
            updatedFile = await Client.CollectionsManager.CreateOrDeleteCollectionsForFileAsync(FileId, bcr);
            Assert.AreEqual(updatedFile.Id, FileId, "Could not remove item from favorites collection");
            items = await Client.CollectionsManager.GetCollectionItemsAsync(favorites.Id);
            Assert.AreEqual(0, items.Entries.Count, "Wrong number of items in favorites collection");
        }
    }
}
