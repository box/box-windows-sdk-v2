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

        [TestMethod]
        public async Task UsersInformation_LiveSession_ValidResponse()
        {
            BoxUser user = await _client.UsersManager.GetCurrentUserInformationAsync();

            Assert.AreEqual("215917383", user.Id);
            Assert.AreEqual("Box Windows", user.Name);
            Assert.AreEqual("boxwinintegration@gmail.com", user.Login, true);
        }

        [TestMethod]
        public async Task EnterpriseUsersInformation_LiveSession_ValidResponse()
        {
            BoxCollection<BoxUser> users = await _client.UsersManager.GetEnterpriseUsersAsync("jhoerr");

            Assert.AreEqual(users.TotalCount, 1);
            Assert.AreEqual(users.Entries.First().Name, "John Hoerr");
            Assert.AreEqual(users.Entries.First().Login, "jhoerr@iu.edu");
        }

        [TestMethod]
        public async Task EnterpriseUser_GetAliases_LiveSession_ValidResponse()
        {
            BoxCollection<BoxEmailAlias> aliases = await _client.UsersManager.GetEmailAliasesAsync("176915787");
            
            Assert.AreEqual(aliases.TotalCount, 1);
            Assert.AreEqual(aliases.Entries.First().Email, "jhoerr@indiana.edu");
            Assert.AreEqual(aliases.Entries.First().IsConfirmed, true);
        }

        [TestMethod]
        public async Task EnterpriseUser_AddAlias_LiveSession_ValidResponse()
        {
            var request = new BoxEmailAliasRequest() { Email = "jhoerr@iupui.edu", User = new BoxRequestEntity() { Id = "176915787" } };
            
            var alias = await _client.UsersManager.AddEmailAliasAsync(request);
            
            Assert.IsNotNull(alias);
            Assert.AreEqual(alias.Email, "jhoerr@iupui.edu");
            Assert.IsTrue(alias.IsConfirmed);
        }

        [TestMethod]
        public async Task EnterpriseUser_RemoveAlias_LiveSession_ValidResponse()
        {
            var success = await _client.UsersManager.RemoveEmailAliasAsync("176915787", "1210868");
            Assert.IsTrue(success);
        }
    }
}
