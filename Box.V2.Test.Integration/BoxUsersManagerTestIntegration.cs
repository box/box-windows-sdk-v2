﻿using System;
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

            Assert.AreEqual("189912110", user.Id);
            Assert.AreEqual("Brian", user.Name);
            Assert.AreEqual("brianytang@gmail.com", user.Login);
        }

        [TestMethod]
        public async Task EnterpriseUsersInformation_LiveSession_ValidResponse()
        {
            BoxCollection<BoxUser> users = await _client.UsersManager.GetEnterpriseUsersAsync("jhoerr");

            Assert.AreEqual(users.TotalCount, 1);
            Assert.AreEqual(users.Entries.First().Name, "John Hoerr");
            Assert.AreEqual(users.Entries.First().Login, "jhoerr@iu.edu");
        }
    }
}
