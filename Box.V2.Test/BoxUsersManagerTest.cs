using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Managers;
using Box.V2.Models;
using Box.V2.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxUsersManagerTest : BoxResourceManagerTest
    {
        private readonly BoxUsersManager _usersManager;

        public BoxUsersManagerTest()
        {
            _usersManager = new BoxUsersManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task GetUserInformation_ValidResponse_ValidUser()
        {
            /*** Arrange ***/
            var responseString = "{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\",\"created_at\":\"2012-03-26T15:43:07-07:00\",\"modified_at\":\"2012-12-12T11:34:29-08:00\",\"language\":\"en\",\"space_amount\":5368709120,\"space_used\":2377016,\"max_upload_size\":262144000,\"status\":\"active\",\"job_title\":\"Employee\",\"phone\":\"5555555555\",\"address\":\"555 Office Drive\",\"avatar_url\":\"https://www.box.com/api/avatar/large/17738362\"}";
            Handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
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
        public async Task GetUserInformation_WithField_ValidResponse_ValidUser()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            var responseString = "{\"type\": \"user\", \"id\": \"12345\", \"status\": \"active\", \"notification_field\": []}";
            Handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            string[] fields = { "status", "notification_email" };
            BoxUser user = await _usersManager.GetUserInformationAsync(userId: "12345", fields: fields);

            /*** Request Check ***/
            var parameter = boxRequest.Parameters.Values.FirstOrDefault();
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual("status,notification_email", parameter);

            /*** Assert ***/
            Assert.AreEqual("12345", user.Id);
            Assert.AreEqual("user", user.Type);
            Assert.AreEqual("active", user.Status);
            Assert.AreEqual(null, user.NotificationEmail);
        }

        [TestMethod]
        public async Task GetUserInformation_TrackingCodes()
        {
            /*** Arrange ***/
            var responseString = @"{
                ""type"": ""user"",
                ""id"": ""12345"",
                ""name"": ""Tracked User"",
                ""tracking_codes"": [
                    {
                        ""type"": ""tracking_code"",
                        ""name"": ""tc1"",
                        ""value"": ""value1""
                    },
                    {
                        ""type"": ""tracking_code"",
                        ""name"": ""tc2"",
                        ""value"": ""value2""
                    }
                ]
            }";
            Handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            string[] fields = { "name", "tracking_codes" };
            BoxUser user = await _usersManager.GetCurrentUserInformationAsync(fields);

            /*** Assert ***/
            Assert.AreEqual("12345", user.Id);
            Assert.AreEqual("Tracked User", user.Name);
            Assert.AreEqual(2, user.TrackingCodes.Count);
            Assert.AreEqual("tc1", user.TrackingCodes[0].Name);
            Assert.AreEqual("value1", user.TrackingCodes[0].Value);
            Assert.AreEqual("tc2", user.TrackingCodes[1].Name);
            Assert.AreEqual("value2", user.TrackingCodes[1].Value);
        }

        [TestMethod]
        public async Task GetUserInformation_ExtraFields()
        {
            /*** Arrange ***/
            var responseString = @"{
                ""type"": ""user"",
                ""id"": ""12345"",
                ""name"": ""Example User"",
                ""timezone"": ""America/Los_Angeles"",
                ""is_external_collab_restricted"": true,
                ""my_tags"": [
                    ""important""
                ],
                ""hostname"": ""https://example.app.box.com/"",
                ""notification_email"": null
            }";
            Handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            string[] fields = { "name", "timezone", "is_external_collab_restricted", "my_tags", "hostname", "notification_email" };
            BoxUser user = await _usersManager.GetCurrentUserInformationAsync(fields);

            /*** Assert ***/
            Assert.AreEqual("12345", user.Id);
            Assert.AreEqual("America/Los_Angeles", user.Timezone);
            Assert.IsTrue(user.IsExternalCollabRestricted.HasValue);
            Assert.IsTrue(user.IsExternalCollabRestricted.Value);
            Assert.AreEqual("important", user.Tags[0]);
            Assert.AreEqual("https://example.app.box.com/", user.Hostname);
            Assert.AreEqual(user.SpaceAmount, null);
            Assert.AreEqual(user.NotificationEmail, null);
        }

        [TestMethod]
        public async Task UpdateUser_ValidResponse_ValidUser()
        {
            /*** Arrange ***/
            var responseString = "{\"type\":\"user\",\"id\":\"181216415\",\"name\":\"sean\",\"login\":\"sean+awesome@box.com\",\"created_at\":\"2012-05-03T21:39:11-07:00\",\"modified_at\":\"2012-12-06T18:17:16-08:00\",\"role\":\"admin\",\"language\":\"en\",\"space_amount\":5368709120,\"space_used\":1237179286,\"max_upload_size\":2147483648,\"tracking_codes\":[],\"can_see_managed_users\":true,\"is_sync_enabled\":true,\"status\":\"active\",\"job_title\":\"\",\"phone\":\"6509241374\",\"address\":\"\",\"avatar_url\":\"https://www.box.com/api/avatar/large/181216415\",\"is_exempt_from_device_limits\":false,\"is_exempt_from_login_verification\":false, \"notification_email\": { \"email\": \"test@example.com\", \"is_confirmed\": true}}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var userRequest = new BoxUserRequest()
            {
                Id = "181216415",
                Name = "sean",
                IsExternalCollabRestricted = true,
                NotificationEmail = new BoxNotificationEmailField
                {
                    Email = "test@example.com"
                }
            };
            BoxUser user = await _usersManager.UpdateUserInformationAsync(userRequest);

            /*** Assert ***/

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(UserUri + "181216415", boxRequest.AbsoluteUri.AbsoluteUri);
            BoxUserRequest payload = JsonConvert.DeserializeObject<BoxUserRequest>(boxRequest.Payload);
            Assert.AreEqual(userRequest.Id, payload.Id);
            Assert.AreEqual(userRequest.Name, payload.Name);
            Assert.AreEqual(userRequest.IsExternalCollabRestricted, payload.IsExternalCollabRestricted);
            Assert.AreEqual(userRequest.NotificationEmail.Email, payload.NotificationEmail.Email);

            //Response check
            Assert.AreEqual("181216415", user.Id);
            Assert.AreEqual("sean", user.Name);
            Assert.AreEqual("sean+awesome@box.com", user.Login);
            Assert.AreEqual("user", user.Type);
            Assert.AreEqual("test@example.com", user.NotificationEmail.Email);
        }

        [TestMethod]
        public async Task RolloutUserFromEnterprise_ValidResponse_ValidUser()
        {
            /*** Arrange ***/
            var responseString = "{\"type\":\"user\",\"id\":\"181216415\",\"name\":\"sean\",\"login\":\"sean+awesome@box.com\",\"created_at\":\"2012-05-03T21:39:11-07:00\",\"modified_at\":\"2012-12-06T18:17:16-08:00\",\"role\":\"admin\",\"language\":\"en\",\"space_amount\":5368709120,\"space_used\":1237179286,\"max_upload_size\":2147483648,\"tracking_codes\":[],\"can_see_managed_users\":true,\"is_sync_enabled\":true,\"status\":\"active\",\"job_title\":\"\",\"phone\":\"6509241374\",\"address\":\"\",\"avatar_url\":\"https://www.box.com/api/avatar/large/181216415\",\"is_exempt_from_device_limits\":false,\"is_exempt_from_login_verification\":false, \"notification_email\": { \"email\": \"test@example.com\", \"is_confirmed\": true}}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var userRequest = new BoxUserRollOutRequest()
            {
                Id = "181216415",
            };
            BoxUser user = await _usersManager.UpdateUserInformationAsync(userRequest);

            /*** Assert ***/

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(UserUri + "181216415", boxRequest.AbsoluteUri.AbsoluteUri);
            BoxUserRequest payload = JsonConvert.DeserializeObject<BoxUserRequest>(boxRequest.Payload);
            Assert.AreEqual(userRequest.Id, payload.Id);
            Assert.AreEqual(boxRequest.Payload, "{\"enterprise\":null,\"id\":\"181216415\"}");
        }

        [TestMethod]
        public async Task InviteUser_ValidResponse_ValidUser()
        {
            /*** Arrange ***/
            var responseString = "{ \"type\":\"invite\",\"id\":\"238632\",\"invited_to\":{ \"type\":\"enterprise\",\"id\":\"42500\",\"name\":\"Blosser Account\"},\"actionable_by\":{ \"type\":\"user\",\"id\":\"229667663\",\"name\":\"Lleyton Hewitt\",\"login\":\"freeuser@box.com\"},\"invited_by\":{ \"type\":\"user\",\"id\":\"10523870\",\"name\":\"Ted Blosser\",\"login\":\"ted@box.com\"},\"status\":\"pending\",\"created_at\":\"2014-12-23T12:55:53-08:00\",\"modified_at\":\"2014-12-23T12:55:53-08:00\"}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxUserInvite>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxUserInvite>>(new BoxResponse<BoxUserInvite>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var userInviteRequest = new BoxUserInviteRequest()
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

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(InviteUri, boxRequest.AbsoluteUri.AbsoluteUri);
            BoxUserInviteRequest payload = JsonConvert.DeserializeObject<BoxUserInviteRequest>(boxRequest.Payload);
            Assert.AreEqual(userInviteRequest.Enterprise.Id, payload.Enterprise.Id);
            Assert.AreEqual(userInviteRequest.ActionableBy.Login, payload.ActionableBy.Login);

            //Response check
            Assert.AreEqual("238632", userInvite.Id);
            Assert.AreEqual("freeuser@box.com", userInvite.ActionableBy.Login);
        }

        [TestMethod]
        public async Task GetUserInvite_ValidResponse_ValidUser()
        {
            /*** Arrange ***/
            var responseString = "{ \"type\":\"invite\",\"id\":\"238632\",\"invited_to\":{ \"type\":\"enterprise\",\"id\":\"42500\",\"name\":\"Blosser Account\"},\"actionable_by\":{ \"type\":\"user\",\"id\":\"229667663\",\"name\":\"Lleyton Hewitt\",\"login\":\"freeuser@box.com\"},\"invited_by\":{ \"type\":\"user\",\"id\":\"10523870\",\"name\":\"Ted Blosser\",\"login\":\"ted@box.com\"},\"status\":\"pending\",\"created_at\":\"2014-12-23T12:55:53-08:00\",\"modified_at\":\"2014-12-23T12:55:53-08:00\"}";
            Handler.Setup(h => h.ExecuteAsync<BoxUserInvite>(It.IsAny<IBoxRequest>()))
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
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxUser>>(It.IsAny<IBoxRequest>()))
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
        public async Task GetEnterpriseUsersWithMarker_ValidReponse()
        {
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxUser>>(It.IsAny<IBoxRequest>()))
           .Returns(() => Task.FromResult<IBoxResponse<BoxCollectionMarkerBased<BoxUser>>>(new BoxResponse<BoxCollectionMarkerBased<BoxUser>>()
           {
               Status = ResponseStatus.Success,
               ContentString = "{\"entries\":[{\"type\":\"user\",\"id\":\"1234567890\",\"name\":\"Joey Burns\",\"login\":\"jburns@example.com\",\"created_at\":\"2020-01-01T01:01:01-07:00\",\"modified_at\":\"2020-01-01T01:01:01-08:00\",\"language\":\"en\",\"timezone\":\"America/Los_Angeles\",\"space_amount\":10737418240,\"space_used\":0,\"max_upload_size\":5368709120,\"status\":\"active\",\"job_title\":\"\",\"phone\":\"\",\"address\":\"\",\"avatar_url\":\"https://example.app.box.com/api/avatar/large/1234567890\",\"notification_email\":{}}],\"limit\":1,\"next_marker\":\"zxcvbnmasdfghjklqwertyuiop1234567890QWERTYUIOPASDFGHJKLZXCVBNM\"}"
           }));

            var marker = "qwertyuiopASDFGHJKLzxcvbnm1234567890QWERTYUIOPasdfghjklZXCVBNM";
            BoxCollectionMarkerBased<BoxUser> items = await _usersManager.GetEnterpriseUsersWithMarkerAsync(marker);
            Assert.AreEqual(items.Limit, 1);
            Assert.AreEqual(items.Entries.Count(), 1);
            Assert.AreEqual(items.Entries.First().Name, "Joey Burns");
        }

        [TestMethod]
        public async Task GetEnterpriseUsers_EmailSpecialCharacters_ValidReponse()
        {
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxUser>>(It.IsAny<IBoxRequest>()))
           .Returns(() => Task.FromResult<IBoxResponse<BoxCollection<BoxUser>>>(new BoxResponse<BoxCollection<BoxUser>>()
           {
               Status = ResponseStatus.Success,
               ContentString = "{\"total_count\":2,\"entries\":[{\"type\":\"user\",\"id\":\"1923882\",\"name\":\"Joey Burns\",\"role\":\"coadmin\"},{\"type\":\"user\",\"id\":\"23412412\",\"name\":\"John Covertino\",\"role\":\"coadmin\"}]}"
           }))
           .Callback<IBoxRequest>(r => boxRequest = r);

            BoxCollection<BoxUser> items = await _usersManager.GetEnterpriseUsersAsync(filterTerm: "user+alias@example.com");

            Assert.AreEqual("filter_term=user%2Balias%40example.com&offset=0&limit=100", boxRequest.GetQueryString());
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
            Handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
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
            Assert.AreEqual(UserUri + "userId", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.IsNotNull(boxRequest.Payload);
            Assert.IsTrue(AreJsonStringsEqual("{\"login\":\"userLogin\"}", boxRequest.Payload));

            // Response check
            Assert.AreEqual("user", result.Type);
            Assert.AreEqual("18180156", result.Id);
            Assert.AreEqual("Dan Glover", result.Name);
            Assert.AreEqual("dglover2@box.com", result.Login);
            Assert.AreEqual(DateTimeOffset.Parse("2012-09-13T10:19:51-07:00"), result.CreatedAt);
            Assert.AreEqual("user", result.Role);
            Assert.AreEqual("en", result.Language);
            Assert.AreEqual(5368709120, result.SpaceAmount);
            Assert.AreEqual(0, result.SpaceUsed);
            Assert.AreEqual(1073741824, result.MaxUploadSize);
            Assert.AreEqual(0, result.TrackingCodes.Count);
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
            var responseString = "{\"type\": \"user\", \"id\": \"187273718\", \"name\": \"Ned Stark\",  \"login\": \"eddard@box.com\",  \"created_at\": \"2012-11-15T16:34:28-08:00\", \"modified_at\": \"2012-11-15T16:34:29-08:00\", \"role\": \"user\", \"language\": \"en\", \"space_amount\": 5368709120, \"space_used\": 0, \"max_upload_size\": 2147483648, \"tracking_codes\": [], \"can_see_managed_users\": true, \"is_sync_enabled\": true, \"status\": \"active\", \"job_title\": \"\", \"phone\": \"555-555-5555\", \"address\": \"555 Box Lane\", \"avatar_url\": \"https://www.box.com/api/avatar/large/187273718\", \"is_exempt_from_device_limits\": false,\"is_exempt_from_login_verification\": false }";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
           .Returns(() => Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
           {
               Status = ResponseStatus.Success,
               ContentString = responseString
           }))
           .Callback<IBoxRequest>(r => boxRequest = r);

            var userRequest = new BoxUserRequest()
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
            Assert.AreEqual(UserUri, boxRequest.AbsoluteUri.AbsoluteUri);
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
            Assert.AreEqual(DateTimeOffset.Parse("2012-11-15T16:34:28-08:00"), result.CreatedAt);
            Assert.AreEqual("user", result.Role);
            Assert.AreEqual("en", result.Language);
            Assert.AreEqual(5368709120, result.SpaceAmount);
            Assert.AreEqual(0, result.SpaceUsed);
            Assert.AreEqual(2147483648, result.MaxUploadSize);
            Assert.AreEqual(0, result.TrackingCodes.Count);
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
            var responseString = "";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
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
            Assert.AreEqual(UserUri + "userid" + "?notify=True&force=False", boxRequest.AbsoluteUri.AbsoluteUri);

            // Response check
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task DeleteEmailAliasAsync_ValidReponse()
        {
            /*** Arrange ***/
            var responseString = "";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
           .Returns(() => Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
           {
               Status = ResponseStatus.Success,
               ContentString = responseString
           }))
           .Callback<IBoxRequest>(r => boxRequest = r);

            var result = await _usersManager.DeleteEmailAliasAsync("userid", "aliasid");

            /*** Assert ***/

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(UserUri + "userid/email_aliases/aliasid", boxRequest.AbsoluteUri.AbsoluteUri);

            // Response check
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task GetUserInformationByUserId_ValidResponse_ValidUser()
        {
            /*** Arrange ***/
            var responseString = "{\"type\": \"user\", \"id\": \"10543463\", \"name\": \"Arielle Frey\", \"login\": \"ariellefrey@box.com\", \"created_at\": \"2011-01-07T12:37:09-08:00\", \"modified_at\": \"2014-05-30T10:39:47-07:00\", \"language\": \"en\", \"timezone\": \"America/Los_Angeles\", \"space_amount\": 10737418240,\"space_used\":558732,\"max_upload_size\": 5368709120,\"status\": \"active\",\"job_title\": \"\",\"phone\": \"\",\"address\": \"\",\"avatar_url\":\"https://blosserdemoaccount.app.box.com/api/avatar/large/10543465\"}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxUser>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxUser>>(new BoxResponse<BoxUser>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxUser user = await _usersManager.GetUserInformationAsync("10543463");

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(UserUri + "10543463", boxRequest.AbsoluteUri.AbsoluteUri);
            // Response check
            Assert.AreEqual("10543463", user.Id);
            Assert.AreEqual("Arielle Frey", user.Name);
            Assert.AreEqual("ariellefrey@box.com", user.Login);
            Assert.AreEqual("user", user.Type);
        }
        [TestMethod]
        public async Task GetEmailAliases_ValidResponse_ValidUser()
        {
            IBoxRequest boxRequest = null;
            /*** Arrange ***/
            var responseString = "{\"total_count\":1,\"entries\":[{\"type\":\"email_alias\",\"id\":\"1234\",\"is_confirmed\":true,\"email\":\"dglover2@box.com\"},{\"type\":\"email_alias\",\"id\":\"1235\",\"is_confirmed\":true,\"email\":\"dglover3@box.com\"}]}";
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxEmailAlias>>(It.IsAny<IBoxRequest>()))
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
            Assert.AreEqual(UserUri + "1234/email_aliases/", boxRequest.AbsoluteUri.AbsoluteUri);

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
            var responseString = "{\"type\":\"email_alias\",\"id\":\"1234\",\"is_confirmed\":true,\"email\":\"dglover2@box.com\"}";
            Handler.Setup(h => h.ExecuteAsync<BoxEmailAlias>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxEmailAlias>>(new BoxResponse<BoxEmailAlias>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxEmailAlias emailALias = await _usersManager.AddEmailAliasAsync("1234", "mail@server.com");

            /*** Assert ***/
            // request
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(UserUri + "1234/email_aliases/", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.AreEqual("{\"email\":\"mail@server.com\"}", boxRequest.Payload);

            // response
            Assert.IsNotNull(emailALias);
            Assert.AreEqual("email_alias", emailALias.Type);
            Assert.AreEqual("1234", emailALias.Id);
            Assert.AreEqual(true, emailALias.IsConfirmed);
            Assert.AreEqual("dglover2@box.com", emailALias.Email);
        }

        [TestMethod]
        public async Task MoveUserFolder_ValidResponse_ValidFolder()
        {
            IBoxRequest boxRequest = null;
            /*** Arrange ***/
            var responseString = "{\"type\":\"folder\",\"id\":\"11446498\",\"sequence_id\":\"1\",\"etag\":\"1\",\"name\":\"Pictures\",\"created_at\":\"2012-12-12T10:53:43-08:00\",\"modified_at\":\"2012-12-12T11:15:04-08:00\",\"description\":\"Some pictures I took\",\"size\":629644,\"path_collection\":{\"total_count\":1,\"entries\":[{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"}]},\"created_by\":{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\"},\"modified_by\":{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\"},\"owned_by\":{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\"},\"shared_link\":{\"url\":\"https://www.box.com/s/vspke7y05sb214wjokpk\",\"download_url\":null,\"vanity_url\":null,\"is_password_enabled\":false,\"unshared_at\":null,\"download_count\":0,\"preview_count\":0,\"access\":\"open\",\"permissions\":{\"can_download\":true,\"can_preview\":true}},\"folder_upload_email\":{\"access\":\"open\",\"email\":\"upload.Picture.k13sdz1@u.box.com\"},\"parent\":{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"},\"item_status\":\"active\",\"item_collection\":{\"total_count\":1,\"entries\":[{\"type\":\"file\",\"id\":\"5000948880\",\"sequence_id\":\"3\",\"etag\":\"3\",\"sha1\":\"134b65991ed521fcfe4724b7d814ab8ded5185dc\",\"name\":\"tigers.jpeg\"}],\"offset\":0,\"limit\":100}}";
            Handler.Setup(h => h.ExecuteAsync<BoxFolder>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFolder>>(new BoxResponse<BoxFolder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxFolder result = await _usersManager.MoveUserFolderAsync("12345678", "17738362");

            /*** Assert ***/
            // request
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(UserUri + "12345678/folders/0?notify=False", boxRequest.AbsoluteUri.AbsoluteUri);
            BoxMoveUserFolderRequest payload = JsonConvert.DeserializeObject<BoxMoveUserFolderRequest>(boxRequest.Payload);
            Assert.AreEqual("17738362", payload.OwnedBy.Id);

            // response
            Assert.IsNotNull(result);
            Assert.AreEqual("11446498", result.Id);
            Assert.AreEqual("Pictures", result.Name);
            Assert.AreEqual("Some pictures I took", result.Description);
            Assert.AreEqual("folder", result.Type);
        }

        [TestMethod]
        public async Task GetMembershipsForUser_ValidResponse_ValidFolder()
        {
            IBoxRequest boxRequest = null;
            /*** Arrange ***/
            var responseString = "{\"total_count\":1,\"entries\":[{\"type\":\"group_membership\",\"id\":\"1560354\",\"user\":{\"type\":\"user\",\"id\":\"13130406\",\"name\":\"Alison Wonderland\",\"login\":\"alice@gmail.com\"},\"group\":{\"type\":\"group\",\"id\":\"119720\",\"name\":\"family\"},\"role\":\"member\"}],\"limit\":100,\"offset\":0}";
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxGroupMembership>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxGroupMembership>>>(new BoxResponse<BoxCollection<BoxGroupMembership>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCollection<BoxGroupMembership> result = await _usersManager.GetMembershipsForUserAsync("13130406");

            /*** Assert ***/
            // request
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(UserUri + "13130406/memberships?offset=0&limit=100", boxRequest.AbsoluteUri.AbsoluteUri);

            // response
            Assert.IsNotNull(result);
            Assert.AreEqual("1560354", result.Entries[0].Id);
            Assert.AreEqual("13130406", result.Entries[0].User.Id);
            Assert.AreEqual("119720", result.Entries[0].Group.Id);
            Assert.AreEqual("member", result.Entries[0].Role);
        }

        [TestMethod]
        public async Task GetUserAvatar_ValidResponse_ValidStream()
        {
            byte[] avatarBytes = { 1, 2, 3 };
            IBoxRequest boxRequest = null;
            using (var avatar = new MemoryStream(avatarBytes))
            {
                /*** Arrange ***/

                Handler.Setup(h => h.ExecuteAsync<Stream>(It.IsAny<IBoxRequest>()))

                    .Returns(Task.FromResult<IBoxResponse<Stream>>(new BoxResponse<Stream>()
                    {
                        Status = ResponseStatus.Success,
                        ResponseObject = avatar
                    }))
                    .Callback<IBoxRequest>(r => boxRequest = r);

                /*** Act ***/
                Stream result = await _usersManager.GetUserAvatar("88888");

                /*** Assert ***/
                Assert.AreEqual(new Uri("https://api.box.com/2.0/users/88888/avatar"), boxRequest.AbsoluteUri);
                Assert.IsNotNull(result, "Stream is Null");
                Assert.AreEqual(3, result.Length);

            }
        }

        [TestMethod]
        public async Task AddOrUpdateUserAvatar_ValidResponse_ValidStream()
        {
            /*** Arrange ***/
            var responseString = LoadFixtureFromJson("Fixtures/BoxUsers/AddOrUpdateUserAvatar200.json");
            BoxMultiPartRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxUploadAvatarResponse>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxUploadAvatarResponse>>(new BoxResponse<BoxUploadAvatarResponse>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r as BoxMultiPartRequest);

            var fakeStream = new Mock<Stream>();
            var fileName = "newAvatar.png";

            /*** Act ***/
            BoxUploadAvatarResponse response = await _usersManager.AddOrUpdateUserAvatarAsync("11111", fakeStream.Object, fileName);

            /*** Assert ***/
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/users/11111/avatar"), boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.IsNotNull(boxRequest.Parts[0] as BoxFileFormPart);
            Assert.AreEqual(fileName, (boxRequest.Parts[0] as BoxFileFormPart).FileName);
            Assert.AreEqual("image/png", (boxRequest.Parts[0] as BoxFileFormPart).ContentType);
            Assert.IsTrue(ReferenceEquals(fakeStream.Object, (boxRequest.Parts[0] as BoxFileFormPart).Value));

            Assert.IsNotNull(response.PicUrls);
            Assert.IsNotNull(response.PicUrls.Preview);
            Assert.IsNotNull(response.PicUrls.Small);
            Assert.IsNotNull(response.PicUrls.Large);
        }

        [TestMethod]
        public async Task AddOrUpdateUserAvatar_ValidResponse_ValidFileStream()
        {
            var responseString = LoadFixtureFromJson("Fixtures/BoxUsers/AddOrUpdateUserAvatar200.json");
            BoxMultiPartRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxUploadAvatarResponse>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxUploadAvatarResponse>>(new BoxResponse<BoxUploadAvatarResponse>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r as BoxMultiPartRequest);

            var stream = new FileStream("newAvatar.png", FileMode.OpenOrCreate);

            BoxUploadAvatarResponse response = await _usersManager.AddOrUpdateUserAvatarAsync("11111", stream);

            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/users/11111/avatar"), boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.IsNotNull(boxRequest.Parts[0] as BoxFileFormPart);
            Assert.AreEqual("newAvatar.png", (boxRequest.Parts[0] as BoxFileFormPart).FileName);
            Assert.AreEqual("image/png", (boxRequest.Parts[0] as BoxFileFormPart).ContentType);
            Assert.IsTrue(ReferenceEquals(stream, (boxRequest.Parts[0] as BoxFileFormPart).Value));

            Assert.IsNotNull(response.PicUrls);
            Assert.IsNotNull(response.PicUrls.Preview);
            Assert.IsNotNull(response.PicUrls.Small);
            Assert.IsNotNull(response.PicUrls.Large);
        }

        [TestMethod]
        public async Task AddOrUpdateUserAvatar_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = "";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxEntity>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxEntity>>(new BoxResponse<BoxEntity>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var result = await _usersManager.DeleteUserAvatarAsync("11111");

            /*** Assert ***/
            /*** Request ***/
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/users/11111/avatar"), boxRequest.AbsoluteUri.AbsoluteUri);
            /*** Response ***/
            Assert.AreEqual(true, result);
        }
    }
}
