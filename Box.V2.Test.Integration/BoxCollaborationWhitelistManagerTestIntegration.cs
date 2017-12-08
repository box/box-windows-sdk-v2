using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Box.V2.Config;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxCollaborationWhitelistManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task GetAllUserWhitelist_LiveSession_ValidResponse()
        {
            var allUserWhitelistItems = await _client.CollaborationWhitelistManager.GetAllCollaborationWhitelistExemptUsersAsync(3, null, true);
            List<BoxCollaborationWhitelistTargetEntry> allUserCollabWhitelist = allUserWhitelistItems.Entries;

            foreach (BoxCollaborationWhitelistTargetEntry userWhitelist in allUserCollabWhitelist)
            {
                Assert.AreEqual("collaboration_whitelist_exempt_target", userWhitelist.Type);
                Assert.IsNotNull(userWhitelist, "collab whitelist was not retrieved");
                Assert.IsNotNull(userWhitelist.Id, "collaboration whitelist id does not exist");
            }
        }

        [TestMethod]
        public async Task GetAllDomainWhitelist_LiveSession_ValidResponse()
        {
            var allCollabWhitelistItems = await _client.CollaborationWhitelistManager.GetAllCollaborationWhitelistEntriesAsync(4, null, true);
            List<BoxCollaborationWhitelistEntry> allCollabWhitelist = allCollabWhitelistItems.Entries;

            foreach (BoxCollaborationWhitelistEntry whitelist in allCollabWhitelist)
            {
                Assert.AreEqual("collaboration_whitelist_entry", whitelist.Type);
                Assert.IsNotNull(whitelist, "collaboration whitelist was not retrieved");
                Assert.IsNotNull(whitelist.Id, "collaboration whitelist ID was not retrieved");
            }
        }

        [TestMethod]
        public async Task UserWhitelist_LiveSession_ValidResponse()
        {
            string userID = "286842893";

            var userWhitelist = await _client.CollaborationWhitelistManager.AddCollaborationWhitelistExemptUserAsync(userID);
            var retrievedUserWhitelist = await _client.CollaborationWhitelistManager.GetCollaborationWhitelistExemptUserAsync(userWhitelist.Id);
            var deletedUserWhitelist = await _client.CollaborationWhitelistManager.DeleteCollaborationWhitelistExemptUserAsync(userWhitelist.Id);

            Assert.IsNotNull(userWhitelist, "Was not able to whitelist user");
            Assert.AreEqual("collaboration_whitelist_exempt_target", userWhitelist.Type);
            Assert.AreEqual(userID, userWhitelist.User.Id);

            Assert.IsNotNull(retrievedUserWhitelist, "Was not able to retrieve whitelist for user");
            Assert.AreEqual(userWhitelist.Id, retrievedUserWhitelist.Id);
            Assert.AreEqual("collaboration_whitelist_exempt_target", retrievedUserWhitelist.Id);

            Assert.IsTrue(deletedUserWhitelist, "User collab whitelist was unable to be deleted");
        }

        [TestMethod]
        public async Task DomainWhitelist_LiveSession_ValidResponse()
        {
            string domain = "test6.com";

            var whiteList = await _client.CollaborationWhitelistManager.AddCollaborationWhitelistEntryAsync(domain, Constants.WhitelistDirections.Both);
            var retrievedWhiteList = await _client.CollaborationWhitelistManager.GetCollaborationWhitelistEntryAsync(whiteList.Id);
            var deletedWhiteList = await _client.CollaborationWhitelistManager.DeleteCollaborationWhitelistEntryAsync(whiteList.Id);

            Assert.IsNotNull(whiteList, "Was not able to create collab whitelist for domain");
            Assert.AreEqual("collaboration_whitelist_entry", whiteList.Id);
            Assert.AreEqual(domain, whiteList.Domain);
            Assert.AreEqual("both", whiteList.Direction);

            Assert.IsNotNull(retrievedWhiteList, "Was not able to retrieve collab whitelist by ID");
            Assert.AreEqual(retrievedWhiteList.Id, whiteList.Id);
            Assert.AreEqual("collaboration_whitelist_entry", retrievedWhiteList.Type);

            Assert.IsTrue(deletedWhiteList, "Collab whitelist was unable to be deleted");    
        }
    }
}
