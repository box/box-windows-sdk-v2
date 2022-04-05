using System;
using System.Threading.Tasks;
using Box.V2.Managers;
using Box.V2.Models;
using Box.V2.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxGroupsManagerTest : BoxResourceManagerTest
    {
        private readonly BoxGroupsManager _groupsManager;

        public BoxGroupsManagerTest()
        {
            _groupsManager = new BoxGroupsManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task GetGroupItems_ValidResponse_ValidGroups()
        {
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxGroup>>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxCollection<BoxGroup>>>(new BoxResponse<BoxCollection<BoxGroup>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = @"{""total_count"": 14, 
                                    ""entries"": [  {""type"": ""group"", ""id"": ""26477"", ""name"": ""adfasdf"", ""created_at"": ""2011-02-15T14:07:22-08:00"", ""modified_at"": ""2011-10-05T19:04:40-07:00"", ""invitability_level"": ""none""}, 
                                                    {""type"": ""group"", ""id"": ""1263"", ""name"": ""Enterprise Migration"", ""created_at"": ""2009-04-20T19:36:17-07:00"", ""modified_at"": ""2011-10-05T19:05:10-07:00"", ""invitability_level"": ""admins_only""}],
                                    ""limit"": 2, ""offset"": 0}"
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCollection<BoxGroup> items = await _groupsManager.GetAllGroupsAsync(filterTerm: "test");

            /*** Assert ***/

            // Request check
            Assert.AreEqual("filter_term=test", boxRequest.GetQueryString());

            // Response check
            Assert.AreEqual(items.TotalCount, 14, "Wrong total count");
            Assert.AreEqual(items.Entries.Count, 2, "Wrong count of entries");
            Assert.AreEqual(items.Entries[0].Type, "group", "Wrong type");
            Assert.AreEqual(items.Entries[0].Id, "26477", "Wrong id");
            Assert.AreEqual(items.Entries[0].Name, "adfasdf", "Wrong name");
            Assert.AreEqual(items.Entries[0].InvitabilityLevel, "none");
            Assert.AreEqual(items.Entries[0].CreatedAt, DateTimeOffset.Parse("2011-02-15T14:07:22-08:00"), "Wrong created at");
            Assert.AreEqual(items.Entries[0].ModifiedAt, DateTimeOffset.Parse("2011-10-05T19:04:40-07:00"), "Wrong modified at");
            Assert.AreEqual(items.Offset, 0, "Wrong offset");
            Assert.AreEqual(items.Limit, 2, "Wrong limit");
        }

        [TestMethod]
        public async Task GetGroup_ValidResponse_ValidGroup()
        {
            Handler.Setup(h => h.ExecuteAsync<BoxGroup>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxGroup>>(new BoxResponse<BoxGroup>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = @"{""type"": ""group"", ""id"": ""26477"", ""name"": ""adfasdf"", ""created_at"": ""2011-02-15T14:07:22-08:00"", ""modified_at"": ""2011-10-05T19:04:40-07:00""}"
                }));

            BoxGroup group = await _groupsManager.GetGroupAsync("222");

            Assert.AreEqual("26477", group.Id, "Wrong Id");
            Assert.AreEqual("group", group.Type, "Wrong type");
            Assert.AreEqual("adfasdf", group.Name, "Wrong name");
            Assert.AreEqual(DateTimeOffset.Parse("2011-02-15T14:07:22-08:00"), group.CreatedAt, "Wrong created at");
            Assert.AreEqual(DateTimeOffset.Parse("2011-10-05T19:04:40-07:00"), group.ModifiedAt, "Wrong modified at");
        }

        [TestMethod]
        public async Task GetGroupItems_ValidResponse_NoGroups()
        {
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxGroup>>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxCollection<BoxGroup>>>(new BoxResponse<BoxCollection<BoxGroup>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = @"{""total_count"": 0, 
                                    ""limit"": 2, ""offset"": 0}"
                }));

            BoxCollection<BoxGroup> items = await _groupsManager.GetAllGroupsAsync();

            Assert.AreEqual(0, items.TotalCount, "Wrong count");
            Assert.AreEqual(0, items.Offset, "Wrong offset");
            Assert.AreEqual(2, items.Limit, "Wrong limit");
        }

        [TestMethod]
        public async Task CreateGroup_ValidResponse_NewGroup()
        {
            Handler.Setup(h => h.ExecuteAsync<BoxGroup>(It.IsAny<BoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxGroup>>(new BoxResponse<BoxGroup>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = @"{ ""type"": ""group"", ""id"": ""159322"", ""name"": ""TestGroup2"", ""created_at"": ""2013-11-12T15:19:47-08:00"", ""modified_at"": ""2013-11-12T15:19:47-08:00"" }"
                }));

            var request = new BoxGroupRequest() { Name = "NewGroup" };

            BoxGroup group = await _groupsManager.CreateAsync(request);

            Assert.AreEqual<string>("TestGroup2", group.Name, "Wrong group name");
            Assert.AreEqual<string>("159322", group.Id, "Wrong id");
            Assert.AreEqual(DateTimeOffset.Parse("2013-11-12T15:19:47-08:00"), group.ModifiedAt, "Wrong modified at");
            Assert.AreEqual(DateTimeOffset.Parse("2013-11-12T15:19:47-08:00"), group.CreatedAt, "Wrong created at");
        }

        [TestMethod]
        public async Task DeleteGroup_ValidResponse_ValidGroup()
        {
            Handler.Setup(h => h.ExecuteAsync<BoxGroup>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxGroup>>(new BoxResponse<BoxGroup>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = ""
                }));

            var result = await _groupsManager.DeleteAsync("1234");
            Assert.IsTrue(result, "Unsuccessful delete");
        }

        [TestMethod]
        public async Task UpdateGroup_ValidResponse_ValidGroup()
        {
            Handler.Setup(h => h.ExecuteAsync<BoxGroup>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxGroup>>(new BoxResponse<BoxGroup>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = @"{ ""type"": ""group"", ""id"": ""159322"", ""name"": ""TestGroup2"", ""created_at"": ""2013-11-12T15:19:47-08:00"", ""modified_at"": ""2013-11-12T15:19:47-08:00"" }"
                }));

            var request = new BoxGroupRequest() { Name = "groupName" };

            BoxGroup group = await _groupsManager.UpdateAsync("123", request);

            Assert.AreEqual<string>("TestGroup2", group.Name, "Wrong Group name");
            Assert.AreEqual<string>("159322", group.Id, "Wrong group id");
            Assert.AreEqual(DateTimeOffset.Parse("2013-11-12T15:19:47-08:00"), group.ModifiedAt, "Wrong modified at");
            Assert.AreEqual(DateTimeOffset.Parse("2013-11-12T15:19:47-08:00"), group.CreatedAt, "Wrong created at");
        }

        [TestMethod]
        public async Task UpdateGroup_ExtraFields_ValidGroup()
        {
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxGroup>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxGroup>>(new BoxResponse<BoxGroup>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = @"{
                        ""type"": ""group"",
                        ""id"": ""159322"",
                        ""description"": ""A group from Okta"",
                        ""external_sync_identifier"": ""foo"",
                        ""provenance"": ""Okta"",
                        ""invitability_level"": ""admins_only"",
                        ""member_viewability_level"": ""admins_only""
                    }"
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            var request = new BoxGroupRequest()
            {
                Description = "A group from Okta",
                ExternalSyncIdentifier = "foo",
                Provenance = "Okta",
                InvitabilityLevel = "admins_only",
                MemberViewabilityLevel = "admins_only"
            };

            var fields = new string[]
            {
               BoxGroup.FieldDescription,
               BoxGroup.FieldExternalSyncIdentifier,
               BoxGroup.FieldProvenance,
               BoxGroup.FieldInvitabilityLevel,
               BoxGroup.FieldMemberViewabilityLevel
            };

            BoxGroup group = await _groupsManager.UpdateAsync("123", request, fields: fields);

            Assert.AreEqual("{\"description\":\"A group from Okta\",\"provenance\":\"Okta\",\"external_sync_identifier\":\"foo\",\"invitability_level\":\"admins_only\",\"member_viewability_level\":\"admins_only\"}", boxRequest.Payload);
            Assert.AreEqual("A group from Okta", group.Description);
            Assert.AreEqual("foo", group.ExternalSyncIdentifier);
            Assert.AreEqual("Okta", group.Provenance);
            Assert.AreEqual("admins_only", group.InvitabilityLevel);
            Assert.AreEqual("admins_only", group.MemberViewabilityLevel);
        }

        [TestMethod]
        public async Task AddGroupMembership_ValidResponse()
        {
            Handler.Setup(h => h.ExecuteAsync<BoxGroupMembership>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxGroupMembership>>(new BoxResponse<BoxGroupMembership>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = @"{ ""type"": ""group_membership"", ""id"": ""2381570"",
                                        ""user"": { ""type"": ""user"", ""id"": ""4940223"", ""name"": ""<script>alert(12);</script>"", ""login"": ""testmaster@box.net""},
                                        ""group"": { ""type"": ""group"", ""id"": ""159622"", ""name"": ""test204bbee0-f3b0-43b4-b213-214fcd2cb9a7"" },
                                        ""role"": ""member"",
                                        ""created_at"": ""2013-11-13T13:19:44-08:00"",
                                        ""modified_at"": ""2013-11-13T13:19:44-08:00""}"
                }));

            var request = new BoxGroupMembershipRequest()
            {
                User = new BoxRequestEntity() { Id = "123" },
                Group = new BoxGroupRequest() { Id = "456" }
            };

            BoxGroupMembership response = await _groupsManager.AddMemberToGroupAsync(request);

            Assert.AreEqual("2381570", response.Id, "Wrong Membership id");
            Assert.AreEqual("group_membership", response.Type, "wrong type");
            Assert.AreEqual("159622", response.Group.Id, "Wrong group id");
            Assert.AreEqual("4940223", response.User.Id, "Wrong user id");
            Assert.AreEqual("member", response.Role, "Wrong role");
            Assert.AreEqual(DateTimeOffset.Parse("2013-11-13T13:19:44-08:00"), response.CreatedAt, "Wrong created at");
            Assert.AreEqual(DateTimeOffset.Parse("2013-11-13T13:19:44-08:00"), response.ModifiedAt, "Wrong modified at");
        }

        [TestMethod]
        public async Task DeleteMembership_ValidResponse_ValidGroup()
        {
            Handler.Setup(h => h.ExecuteAsync<BoxGroup>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxGroup>>(new BoxResponse<BoxGroup>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = ""
                }));

            var result = await _groupsManager.DeleteGroupMembershipAsync("1234");
            Assert.IsTrue(result, "Unsuccessful group membership delete");
        }

        [TestMethod]
        public async Task GetAllMemberships_ValidResponse_ValidGroup()
        {
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxGroupMembership>>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxCollection<BoxGroupMembership>>>(new BoxResponse<BoxCollection<BoxGroupMembership>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = @"{ ""total_count"": 2, ""entries"": [{
                                        ""type"": ""group_membership"", ""id"": ""136639"",
                                        ""user"": {""type"": ""user"",""id"": ""6102564"", ""name"": ""<script>alert('atk');</script>"", ""login"": ""testanyregister@box.net""},
                                        ""group"": {""type"": ""group"",""id"": ""26477"",""name"": ""adfasdf""},""role"": ""member""}, 
                                        {""type"": ""group_membership"",""id"": ""273529"",
                                        ""user"": {""type"": ""user"",""id"": ""13928063"",""name"": ""spootie"",""login"": ""tevanspratt++39478@box.net""},
                                        ""group"": {""type"": ""group"", ""id"": ""26477"", ""name"": ""adfasdf""}, ""role"": ""member""}], ""offset"": 0,""limit"": 100}"
                }));

            BoxCollection<BoxGroupMembership> response = await _groupsManager.GetAllGroupMembershipsForGroupAsync("123");

            Assert.AreEqual(2, response.TotalCount, "Wrong total count");
            Assert.AreEqual(2, response.Entries.Count, "Wrong number of entries");
            Assert.AreEqual("group_membership", response.Entries[0].Type, "Wrong type");
            Assert.AreEqual("6102564", response.Entries[0].User.Id, "Wrong user id");
            Assert.AreEqual("26477", response.Entries[0].Group.Id, "Wrong group id");
        }

        [TestMethod]
        public async Task GetAllMemberships_ValidResponse_ValidUser()
        {
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxGroupMembership>>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxCollection<BoxGroupMembership>>>(new BoxResponse<BoxCollection<BoxGroupMembership>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = @"{ ""total_count"": 2, ""entries"": [{
                                        ""type"": ""group_membership"", ""id"": ""136639"",
                                        ""user"": {""type"": ""user"",""id"": ""6102564"", ""name"": ""<script>alert('atk');</script>"", ""login"": ""testanyregister@box.net""},
                                        ""group"": {""type"": ""group"",""id"": ""26477"",""name"": ""adfasdf""},""role"": ""member""}, 
                                        {""type"": ""group_membership"",""id"": ""273529"",
                                        ""user"": {""type"": ""user"",""id"": ""13928063"",""name"": ""spootie"",""login"": ""tevanspratt++39478@box.net""},
                                        ""group"": {""type"": ""group"", ""id"": ""26477"", ""name"": ""adfasdf""}, ""role"": ""member""}], ""offset"": 0,""limit"": 100}"
                }));

            BoxCollection<BoxGroupMembership> response = await _groupsManager.GetAllGroupMembershipsForUserAsync("123");

            Assert.AreEqual(2, response.TotalCount, "Wrong total count");
            Assert.AreEqual(2, response.Entries.Count, "Wrong number of entries");
            Assert.AreEqual("group_membership", response.Entries[0].Type, "Wrong type");
            Assert.AreEqual("6102564", response.Entries[0].User.Id, "Wrong user id");
            Assert.AreEqual("26477", response.Entries[0].Group.Id, "Wrong group id");
        }

        [TestMethod]
        public async Task GetGroupMembership_ValidResponse()
        {
            Handler.Setup(h => h.ExecuteAsync<BoxGroupMembership>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxGroupMembership>>(new BoxResponse<BoxGroupMembership>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = @"{ ""type"": ""group_membership"", ""id"": ""2381570"",
                                        ""user"": { ""type"": ""user"", ""id"": ""4940223"", ""name"": ""<script>alert(12);</script>"", ""login"": ""testmaster@box.net""},
                                        ""group"": { ""type"": ""group"", ""id"": ""159622"", ""name"": ""test204bbee0-f3b0-43b4-b213-214fcd2cb9a7"" },
                                        ""role"": ""member"",
                                        ""created_at"": ""2013-11-13T13:19:44-08:00"",
                                        ""modified_at"": ""2013-11-13T13:19:44-08:00""}"
                }));

            BoxGroupMembership response = await _groupsManager.GetGroupMembershipAsync("123");

            Assert.AreEqual("2381570", response.Id, "Wrong Membership id");
            Assert.AreEqual("group_membership", response.Type, "wrong type");
            Assert.AreEqual("159622", response.Group.Id, "Wrong group id");
            Assert.AreEqual("4940223", response.User.Id, "Wrong user id");
            Assert.AreEqual("member", response.Role, "Wrong role");
            Assert.AreEqual(DateTimeOffset.Parse("2013-11-13T13:19:44-08:00"), response.CreatedAt, "Wrong created at");
            Assert.AreEqual(DateTimeOffset.Parse("2013-11-13T13:19:44-08:00"), response.ModifiedAt, "Wrong modified at");
        }

        [TestMethod]
        public async Task UpdateGroupMembership_ValidResponse()
        {
            Handler.Setup(h => h.ExecuteAsync<BoxGroupMembership>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxGroupMembership>>(new BoxResponse<BoxGroupMembership>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = @"{ ""type"": ""group_membership"", ""id"": ""2381570"",
                                        ""user"": { ""type"": ""user"", ""id"": ""4940223"", ""name"": ""<script>alert(12);</script>"", ""login"": ""testmaster@box.net""},
                                        ""group"": { ""type"": ""group"", ""id"": ""159622"", ""name"": ""test204bbee0-f3b0-43b4-b213-214fcd2cb9a7"" },
                                        ""role"": ""admin"",
                                        ""created_at"": ""2013-11-13T13:19:44-08:00"",
                                        ""modified_at"": ""2013-11-13T13:19:44-08:00""}"
                }));

            var request = new BoxGroupMembershipRequest() { Role = "anything" };

            BoxGroupMembership response = await _groupsManager.UpdateGroupMembershipAsync("123", request);

            Assert.AreEqual("2381570", response.Id, "Wrong Membership id");
            Assert.AreEqual("group_membership", response.Type, "wrong type");
            Assert.AreEqual("159622", response.Group.Id, "Wrong group id");
            Assert.AreEqual("4940223", response.User.Id, "Wrong user id");
            Assert.AreEqual("admin", response.Role, "Wrong role");
            Assert.AreEqual(DateTimeOffset.Parse("2013-11-13T13:19:44-08:00"), response.CreatedAt, "Wrong created at");
            Assert.AreEqual(DateTimeOffset.Parse("2013-11-13T13:19:44-08:00"), response.ModifiedAt, "Wrong modified at");
        }
    }
}
