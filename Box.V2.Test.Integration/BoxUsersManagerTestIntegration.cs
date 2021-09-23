using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxUsersManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        private static Random random = new Random();

        protected static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [TestMethod]
        public async Task UsersInformation_LiveSession_ValidResponse()
        {
            BoxUser user = await _client.UsersManager.GetCurrentUserInformationAsync();

            Assert.AreEqual("215917383", user.Id);
            Assert.AreEqual("Box Windows", user.Name);
            Assert.AreEqual("boxwinintegration@gmail.com", user.Login, true);
        }

        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public async Task ExternalAppUserId_LiveSession_ValidResponse()
        {
            // Works only on adminClient
            if (adminClient == null)
            {
                return;
            }

            // Create 
            var userRequest = new BoxUserRequest
            {
                Name = "AppUser ExtId Test",
                IsPlatformAccessOnly = true, // creating application specific user, not a Box.com user
                ExternalAppUserId = "yhu-au" + RandomString(3)
            };

            var newUser = await adminClient.UsersManager.CreateEnterpriseUserAsync(userRequest);

            // Update
            var updateUserRequest = new BoxUserRequest
            {
                Id = newUser.Id,
                ExternalAppUserId = "yhu-au" + RandomString(3)
            };

            var updatedUser = await adminClient.UsersManager.UpdateUserInformationAsync(updateUserRequest);
            Assert.AreEqual(newUser.Id, updatedUser.Id);

            // Get
            var appUsers = await adminClient.UsersManager.GetEnterpriseUsersAsync(null, 0, 100, null, null, updateUserRequest.ExternalAppUserId);
            Assert.AreEqual(appUsers.Entries.Count, 1);

            // Delete
            await adminClient.UsersManager.DeleteEnterpriseUserAsync(appUsers.Entries[0].Id, false, true);
        }

        [TestMethod]
        public async Task EnterpriseUsersInformation_LiveSession_ValidResponse()
        {
            BoxCollection<BoxUser> users = await _client.UsersManager.GetEnterpriseUsersAsync("test.user", userType: "all");

            Assert.AreEqual(users.TotalCount, 1);
            Assert.AreEqual(users.Entries.First().Name, "Test User");
            Assert.AreEqual(users.Entries.First().Login, "test.user@example.com");
        }

        [TestMethod]
        public async Task EnterpriseUsersAutoPagination_LiveSession_ValidResponse()
        {
            BoxCollection<BoxUser> users = await _client.UsersManager.GetEnterpriseUsersAsync(limit: 1, userType: "all", autoPaginate: true);

            Assert.IsTrue(users.TotalCount > 2);
        }

        [TestMethod]
        public async Task EnterpriseUsersMarkerBasedPagination_LiveSession_ValidResponse()
        {
            BoxCollectionMarkerBased<BoxUser> users = await _client.UsersManager.GetEnterpriseUsersWithMarkerAsync(limit: 1);
            Assert.IsTrue(users.Entries.Count == 1);

            BoxCollectionMarkerBased<BoxUser> users2 = await _client.UsersManager.GetEnterpriseUsersWithMarkerAsync(marker: users.NextMarker, limit: 2);
            Assert.IsTrue(users2.Entries.Count == 2);
        }
    }
}
