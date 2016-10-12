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

        [TestMethod]
        public async Task InviteUser_ValidResponse_ValidUser()
        {
            /*** Arrange ***/
            string responseString = "{ \"type\":\"invite\",\"id\":\"238632\",\"invited_to\":{ \"type\":\"enterprise\",\"id\":\"42500\",\"name\":\"Blosser Account\"},\"actionable_by\":{ \"type\":\"user\",\"id\":\"229667663\",\"name\":\"Lleyton Hewitt\",\"login\":\"freeuser@box.com\"},\"invited_by\":{ \"type\":\"user\",\"id\":\"10523870\",\"name\":\"Ted Blosser\",\"login\":\"ted@box.com\"},\"status\":\"pending\",\"created_at\":\"2014-12-23T12:55:53-08:00\",\"modified_at\":\"2014-12-23T12:55:53-08:00\"}";
            _handler.Setup(h => h.ExecuteAsync<BoxUserInvite>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxUserInvite>>(new BoxResponse<BoxUserInvite>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxUserInviteRequest userInviteRequest = new BoxUserInviteRequest()
            {
                Enterprise = new BoxRequestEntity()
                {
                    Id = "42500"
                },
                ActionableBy = new BoxActionableByRequest()
                {
                    Login = "freeuser@box.com"
                }
            };
            BoxUserInvite userInvite = await _usersManager.InviteUserToEnterpriseAsync(userInviteRequest);

            /*** Assert ***/
            Assert.AreEqual("freeuser@box.com", userInvite.ActionableBy.Login);
        }

        [TestMethod]
        public async Task GetUserInvite_ValidResponse_ValidUser()
        {
            /*** Arrange ***/
            string responseString = "{ \"type\":\"invite\",\"id\":\"238632\",\"invited_to\":{ \"type\":\"enterprise\",\"id\":\"42500\",\"name\":\"Blosser Account\"},\"actionable_by\":{ \"type\":\"user\",\"id\":\"229667663\",\"name\":\"Lleyton Hewitt\",\"login\":\"freeuser@box.com\"},\"invited_by\":{ \"type\":\"user\",\"id\":\"10523870\",\"name\":\"Ted Blosser\",\"login\":\"ted@box.com\"},\"status\":\"pending\",\"created_at\":\"2014-12-23T12:55:53-08:00\",\"modified_at\":\"2014-12-23T12:55:53-08:00\"}";
            _handler.Setup(h => h.ExecuteAsync<BoxUserInvite>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxUserInvite>>(new BoxResponse<BoxUserInvite>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxUserInvite userInvite = await _usersManager.GetUserInviteAsync("1234");

            /*** Assert ***/
            Assert.AreEqual("freeuser@box.com", userInvite.ActionableBy.Login);
        }

        [TestMethod]
        public async Task GetEnterpriseUsers_ValidReponse()
        {
            _handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxUser>>(It.IsAny<IBoxRequest>()))
           .Returns(() => Task.FromResult<IBoxResponse<BoxCollection<BoxUser>>>(new BoxResponse<BoxCollection<BoxUser>>()
           {
               Status = ResponseStatus.Success,
               ContentString = "{\"total_count\":2,\"entries\":[{\"type\":\"user\",\"id\":\"1923882\",\"name\":\"Joey Burns\",\"role\":\"coadmin\"},{\"type\":\"user\",\"id\":\"23412412\",\"name\":\"John Covertino\",\"role\":\"coadmin\"}]}"
           }));

            BoxCollection<BoxUser> items = await _usersManager.GetEnterpriseUsersAsync();
            Assert.AreEqual(items.TotalCount, 2);
            Assert.AreEqual(items.Entries.Count(), 2);
            Assert.AreEqual(items.Entries.First().Name, "Joey Burns");
            Assert.AreEqual(items.Entries.Last().Name, "John Covertino");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task GetEnterpriseUsers_LimitLow()
        {
            await _usersManager.GetEnterpriseUsersAsync("foo", 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public async Task GetEnterpriseUsers_LimitHigh()
        {
            await _usersManager.GetEnterpriseUsersAsync("foo", 0, 1001);
        }

        [TestMethod]
        public async Task GetEmailAliases_ValidResponse_ValidUser()
        {
            IBoxRequest boxRequest = null;
            /*** Arrange ***/
            string responseString = "{\"total_count\":1,\"entries\":[{\"type\":\"email_alias\",\"id\":\"1234\",\"is_confirmed\":true,\"email\":\"dglover2@box.com\"},{\"type\":\"email_alias\",\"id\":\"1235\",\"is_confirmed\":true,\"email\":\"dglover3@box.com\"}]}";
            _handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxEmailAlias>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxEmailAlias>>>(new BoxResponse<BoxCollection<BoxEmailAlias>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCollection<BoxEmailAlias> emailALiases = await _usersManager.GetEmailAliasesAsync("1234");

            /*** Assert ***/
            // request
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(_usersUri + "1234/email_aliases/", boxRequest.AbsoluteUri.AbsoluteUri);

            //response
            Assert.AreEqual(1, emailALiases.TotalCount);
            Assert.IsNotNull(emailALiases.Entries);
            Assert.AreEqual(2, emailALiases.Entries.Count);
            Assert.IsNotNull(emailALiases.Entries[0]);

            //1st entry
            Assert.AreEqual("email_alias", emailALiases.Entries[0].Type);
            Assert.AreEqual("1234", emailALiases.Entries[0].Id);
            Assert.AreEqual(true, emailALiases.Entries[0].IsConfirmed);
            Assert.AreEqual("dglover2@box.com", emailALiases.Entries[0].Email);

            // 2nd entry
            Assert.AreEqual("email_alias", emailALiases.Entries[1].Type);
            Assert.AreEqual("1235", emailALiases.Entries[1].Id);
            Assert.AreEqual(true, emailALiases.Entries[1].IsConfirmed);
            Assert.AreEqual("dglover3@box.com", emailALiases.Entries[1].Email);

        }

        [TestMethod]
        public async Task AddEmailAlias_ValidResponse_ValidUser()
        {
            IBoxRequest boxRequest = null;
            /*** Arrange ***/
            string responseString = "{\"type\":\"email_alias\",\"id\":\"1234\",\"is_confirmed\":true,\"email\":\"dglover2@box.com\"}";
            _handler.Setup(h => h.ExecuteAsync<BoxEmailAlias>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult <IBoxResponse<BoxEmailAlias>>(new BoxResponse<BoxEmailAlias>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxEmailAlias emailALias = await _usersManager.AddEmailAliasesAsync("1234", "mail@server.com");

            /*** Assert ***/
            // request
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(_usersUri + "1234/email_aliases/", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.AreEqual("{\"email\":\"mail@server.com\"}", boxRequest.Payload);

            // response
            Assert.IsNotNull(emailALias);
            Assert.AreEqual("email_alias", emailALias.Type);
            Assert.AreEqual("1234", emailALias.Id);
            Assert.AreEqual(true, emailALias.IsConfirmed);
            Assert.AreEqual("dglover2@box.com", emailALias.Email);
        }
    }
}