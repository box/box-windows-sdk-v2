using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxUsersManagerTest : BoxResourceManagerTest
    {
        protected BoxUsersManager _usersManager;

        public BoxUsersManagerTest()
        {
            _usersManager = new BoxUsersManager(_config.Object, _service, _converter, _authRepository);
        }

        [TestMethod]
        public async Task GetUserInformation_ValidResponse_ValidUser()
        {
            /*** Arrange ***/
            string responseString = "{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\",\"created_at\":\"2012-03-26T15:43:07-07:00\",\"modified_at\":\"2012-12-12T11:34:29-08:00\",\"language\":\"en\",\"space_amount\":5368709120,\"space_used\":2377016,\"max_upload_size\":262144000,\"status\":\"active\",\"job_title\":\"Employee\",\"phone\":\"5555555555\",\"address\":\"555 Office Drive\",\"avatar_url\":\"https://www.box.com/api/avatar/large/17738362\"}";
            _handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxUser user = await _usersManager.GetCurrentUserInformationAsync();

            /*** Assert ***/
            Assert.AreEqual("17738362", user.Id);
            Assert.AreEqual("sean rose", user.Name);
            Assert.AreEqual("sean@box.com", user.Login);
            Assert.AreEqual("user", user.Type);
        }

        [TestMethod]
        public async Task UpdateUser_ValidResponse_ValidUser()
        {
            /*** Arrange ***/
            string responseString = "{\"type\":\"user\",\"id\":\"181216415\",\"name\":\"sean\",\"login\":\"sean+awesome@box.com\",\"created_at\":\"2012-05-03T21:39:11-07:00\",\"modified_at\":\"2012-12-06T18:17:16-08:00\",\"role\":\"admin\",\"language\":\"en\",\"space_amount\":5368709120,\"space_used\":1237179286,\"max_upload_size\":2147483648,\"tracking_codes\":[],\"can_see_managed_users\":true,\"is_sync_enabled\":true,\"status\":\"active\",\"job_title\":\"\",\"phone\":\"6509241374\",\"address\":\"\",\"avatar_url\":\"https://www.box.com/api/avatar/large/181216415\",\"is_exempt_from_device_limits\":false,\"is_exempt_from_login_verification\":false}";
            _handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxUser user = await _usersManager.GetCurrentUserInformationAsync();

            /*** Assert ***/
            Assert.AreEqual("181216415", user.Id);
            Assert.AreEqual("sean", user.Name);
            Assert.AreEqual("sean+awesome@box.com", user.Login);
            Assert.AreEqual("user", user.Type);
        }

    }
}