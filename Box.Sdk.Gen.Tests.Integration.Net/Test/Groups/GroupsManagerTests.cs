using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class GroupsManagerTests {
        public BoxClient client { get; }

        public GroupsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetGroups() {
            Groups groups = await client.Groups.GetGroupsAsync();
            Assert.IsTrue(NullableUtils.Unwrap(groups.TotalCount) >= 0);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateGetDeleteGroup() {
            string groupName = Utils.GetUUID();
            const string groupDescription = "Group description";
            GroupFull group = await client.Groups.CreateGroupAsync(requestBody: new CreateGroupRequestBody(name: groupName) { Description = groupDescription });
            Assert.IsTrue(group.Name == groupName);
            GroupFull groupById = await client.Groups.GetGroupByIdAsync(groupId: group.Id, queryParams: new GetGroupByIdQueryParams() { Fields = Array.AsReadOnly(new [] {"id","name","description","group_type"}) });
            Assert.IsTrue(groupById.Id == group.Id);
            Assert.IsTrue(groupById.Description == groupDescription);
            string updatedGroupName = Utils.GetUUID();
            GroupFull updatedGroup = await client.Groups.UpdateGroupByIdAsync(groupId: group.Id, requestBody: new UpdateGroupByIdRequestBody() { Name = updatedGroupName });
            Assert.IsTrue(updatedGroup.Name == updatedGroupName);
            await client.Groups.DeleteGroupByIdAsync(groupId: group.Id);
            await Assert.That.IsExceptionAsync(async() => await client.Groups.GetGroupByIdAsync(groupId: group.Id));
        }

    }
}