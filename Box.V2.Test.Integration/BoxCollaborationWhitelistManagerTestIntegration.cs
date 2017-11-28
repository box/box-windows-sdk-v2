using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxCollaborationWhitelistManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task UserWhitelist_LiveSession_ValidResponse()
        {
            var userWhitelist = await _client.CollaborationWhitelistManager.AddCollaborationWhitelistExemptUserAsync("275343214");
            var retrievedUserWhitelist = await _client.CollaborationWhitelistManager.GetCollaborationWhitelistExemptUserAsync(userWhitelist.Id);
            var deletedUserWhitelist = await _client.CollaborationWhitelistManager.DeleteCollaborationWhitelistExemptUserAsync(userWhitelist.Id);

            Assert.IsNotNull(userWhitelist, "Was not able to whitelist user");
            Assert.IsNotNull(retrievedUserWhitelist, "Was not able to retrieve whitelist for user");
            Assert.IsTrue(deletedUserWhitelist, "User collab whitelist was unable to be deleted");
        }

        [TestMethod]
        public async Task GetAllUserWhitelist_LiveSession_ValidResponse()
        {
            var allUserWhitelistItems = await _client.CollaborationWhitelistManager.GetCollaborationWhitelistExemptUsersAsync();
            List<BoxCollaborationWhitelistTargetEntry> allUserCollabWhitelist = allUserWhitelistItems.Entries;

            foreach (BoxCollaborationWhitelistTargetEntry userWhitelist in allUserCollabWhitelist)
            {
                Assert.IsNotNull(userWhitelist, "collab whitelist was not retrieved");
            }
        }

        [TestMethod]
        public async Task DomainWhitelist_LiveSession_ValidResponse()
        {
            var whiteList = await _client.CollaborationWhitelistManager.AddCollaborationWhitelistEntryAsync("test5.com", "both");
            var retrievedWhiteList = await _client.CollaborationWhitelistManager.GetCollaborationWhitelistEntryAsync(whiteList.Id);
            var deletedWhiteList = await _client.CollaborationWhitelistManager.DeleteCollaborationWhitelistEntryAsync(whiteList.Id);

            Assert.IsNotNull(whiteList, "Was not able to create collab whitelist for domain");
            Assert.IsNotNull(retrievedWhiteList, "Was not able to retrieve collab whitelist by ID");
            Assert.IsTrue(deletedWhiteList, "Collab whitelist was unable to be deleted");    
        }

        [TestMethod]
        public async Task GetAllDomainWhitelist_LiveSession_ValidResponse()
        {
            var allCollabWhitelistItems = await _client.CollaborationWhitelistManager.GetAllCollaborationWhitelistEntriesAsync();
            List<BoxCollaborationWhitelistEntry> allCollabWhitelist = allCollabWhitelistItems.Entries;

            foreach(BoxCollaborationWhitelistEntry whitelist in allCollabWhitelist)
            {
                Assert.IsNotNull(whitelist, "collab whitelist was not retrieved");
            }
        }
    }
}
