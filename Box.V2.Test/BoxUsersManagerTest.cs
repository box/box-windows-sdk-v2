using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
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
        public async Task ChangeUsersLogin_ValidReponse()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
           .Returns(() => Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
           {
               Status = ResponseStatus.Success,
               ContentString = "{\"type\":\"user\",\"id\":\"18180156\",\"name\":\"Dan Glover\",\"login\":\"dglover2@box.com\",\"created_at\":\"2012-09-13T10:19:51-07:00\",\"modified_at\":\"2012-09-21T10:24:51-07:00\",\"role\":\"user\",\"language\":\"en\",\"space_amount\":5368709120,\"space_used\":0,\"max_upload_size\":1073741824,\"tracking_codes\":[],\"can_see_managed_users\":false,\"is_sync_enabled\":false,\"status\":\"active\",\"job_title\":\"\",\"phone\":\"\",\"address\":\"\",\"avatar_url\":\"\"}"
           }))
           .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxUser result = await _usersManager.ChangeUsersLoginAsync("userId", "userLogin");

            /*** Assert ***/

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(_UserUri + "userId", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.IsNotNull(boxRequest.Payload);
            Assert.IsTrue(AreJsonStringsEqual("{\"login\":\"userLogin\"}", boxRequest.Payload));

            // Response check
            Assert.AreEqual("user", result.Type);
            Assert.AreEqual("18180156", result.Id);
            Assert.AreEqual("Dan Glover", result.Name);
            Assert.AreEqual("dglover2@box.com", result.Login);
            Assert.AreEqual(DateTime.Parse("2012-09-13T10:19:51-07:00"), result.CreatedAt);
            Assert.AreEqual("user", result.Role);
            Assert.AreEqual("en", result.Language);
            Assert.AreEqual(5368709120, result.SpaceAmount);
            Assert.AreEqual(0, result.SpaceUsed);
            Assert.AreEqual(1073741824, result.MaxUploadSize);
            Assert.AreEqual(0, result.TrackingCodes.Length);
            Assert.AreEqual(false, result.CanSeeManagedUsers);
            Assert.AreEqual(false, result.IsSyncEnabled);
            Assert.AreEqual("active", result.Status);
            Assert.IsTrue(string.IsNullOrEmpty(result.JobTitle));
            Assert.IsTrue(string.IsNullOrEmpty(result.Phone));
            Assert.IsTrue(string.IsNullOrEmpty(result.Address));
            Assert.IsTrue(string.IsNullOrEmpty(result.AvatarUrl));

        }

        [TestMethod]
        public async Task CreateEnterpriseUser_ValidReponse()
        {
            /*** Arrange ***/
            string responseString = "{\"type\": \"user\", \"id\": \"187273718\", \"name\": \"Ned Stark\",  \"login\": \"eddard@box.com\",  \"created_at\": \"2012-11-15T16:34:28-08:00\", \"modified_at\": \"2012-11-15T16:34:29-08:00\", \"role\": \"user\", \"language\": \"en\", \"space_amount\": 5368709120, \"space_used\": 0, \"max_upload_size\": 2147483648, \"tracking_codes\": [], \"can_see_managed_users\": true, \"is_sync_enabled\": true, \"status\": \"active\", \"job_title\": \"\", \"phone\": \"555-555-5555\", \"address\": \"555 Box Lane\", \"avatar_url\": \"https://www.box.com/api/avatar/large/187273718\", \"is_exempt_from_device_limits\": false,\"is_exempt_from_login_verification\": false }";
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
           .Returns(() => Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
           {
               Status = ResponseStatus.Success,
               ContentString = responseString
           }))
           .Callback<IBoxRequest>(r => boxRequest = r);

            BoxUserRequest userRequest = new BoxUserRequest()
            {
                Login = "eddard@box.com",
                Name = "Ned Stark",
                Role = "user",
                Language = "en",
                IsSyncEnabled = true,
                Phone = "555-555-5555",
                Address = "555 Box Lane",
            };

            /*** Act ***/
            BoxUser result = await _usersManager.CreateEnterpriseUserAsync(userRequest);

            /*** Assert ***/

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(_UserUri, boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.IsNotNull(boxRequest.Payload);
            BoxUserRequest payload = JsonConvert.DeserializeObject<BoxUserRequest>(boxRequest.Payload);
            Assert.AreEqual(userRequest.Login, payload.Login);
            Assert.AreEqual(userRequest.Name, payload.Name);
            Assert.AreEqual(userRequest.IsSyncEnabled, payload.IsSyncEnabled);
            Assert.AreEqual(userRequest.Phone, payload.Phone);
            Assert.AreEqual(userRequest.Address, payload.Address);

            // Response check
            Assert.AreEqual("user", result.Type);
            Assert.AreEqual("187273718", result.Id);
            Assert.AreEqual("Ned Stark", result.Name);
            Assert.AreEqual("eddard@box.com", result.Login);
            Assert.AreEqual(DateTime.Parse("2012-11-15T16:34:28-08:00"), result.CreatedAt);
            Assert.AreEqual("user", result.Role);
            Assert.AreEqual("en", result.Language);
            Assert.AreEqual(5368709120, result.SpaceAmount);
            Assert.AreEqual(0, result.SpaceUsed);
            Assert.AreEqual(2147483648, result.MaxUploadSize);
            Assert.AreEqual(0, result.TrackingCodes.Length);
            Assert.AreEqual(true, result.CanSeeManagedUsers);
            Assert.AreEqual(true, result.IsSyncEnabled);
            Assert.AreEqual("active", result.Status);
            Assert.IsTrue(string.IsNullOrEmpty(result.JobTitle));
            Assert.AreEqual("555-555-5555", result.Phone);
            Assert.AreEqual("555 Box Lane", result.Address);
            Assert.AreEqual("https://www.box.com/api/avatar/large/187273718", result.AvatarUrl);

        }

        [TestMethod]
        public async Task DeleteEnterpriseUser_ValidReponse()
        {
            /*** Arrange ***/
            string responseString = "";
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
           .Returns(() => Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
           {
               Status = ResponseStatus.Success,
               ContentString = responseString
           }))
           .Callback<IBoxRequest>(r => boxRequest = r);

            BoxUser result = await _usersManager.DeleteEnterpriseUserAsync("userid", true, false);

            /*** Assert ***/

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(_UserUri + "userid" + "?notify=True&force=False", boxRequest.AbsoluteUri.AbsoluteUri);

            // Response check
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task DeleteEmailAliasAsync_ValidReponse()
        {
            /*** Arrange ***/
            string responseString = "";
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
           .Returns(() => Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
           {
               Status = ResponseStatus.Success,
               ContentString = responseString
           }))
           .Callback<IBoxRequest>(r => boxRequest = r);

            bool result = await _usersManager.DeleteEmailAliasAsync("userid", "aliasid");

            /*** Assert ***/

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(_UserUri + "userid/email_aliases/aliasid", boxRequest.AbsoluteUri.AbsoluteUri);

            // Response check
            Assert.AreEqual(true, result);
        }
    }
}