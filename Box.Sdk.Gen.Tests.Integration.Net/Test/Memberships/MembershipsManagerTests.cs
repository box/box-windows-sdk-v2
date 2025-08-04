using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class MembershipsManagerTests {
        public BoxClient client { get; }

        public MembershipsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestMemberships() {
            UserFull user = await client.Users.CreateUserAsync(requestBody: new CreateUserRequestBody(name: Utils.GetUUID()) { Login = string.Concat(Utils.GetUUID(), "@boxdemo.com") });
            GroupMemberships userMemberships = await client.Memberships.GetUserMembershipsAsync(userId: user.Id);
            Assert.IsTrue(userMemberships.TotalCount == 0);
            GroupFull group = await client.Groups.CreateGroupAsync(requestBody: new CreateGroupRequestBody(name: Utils.GetUUID()));
            GroupMemberships groupMemberships = await client.Memberships.GetGroupMembershipsAsync(groupId: group.Id);
            Assert.IsTrue(groupMemberships.TotalCount == 0);
            GroupMembership groupMembership = await client.Memberships.CreateGroupMembershipAsync(requestBody: new CreateGroupMembershipRequestBody(user: new CreateGroupMembershipRequestBodyUserField(id: user.Id), group: new CreateGroupMembershipRequestBodyGroupField(id: group.Id)));
            Assert.IsTrue(NullableUtils.Unwrap(groupMembership.User).Id == user.Id);
            Assert.IsTrue(NullableUtils.Unwrap(groupMembership.Group).Id == group.Id);
            Assert.IsTrue(StringUtils.ToStringRepresentation(groupMembership.Role?.Value) == "member");
            GroupMembership getGroupMembership = await client.Memberships.GetGroupMembershipByIdAsync(groupMembershipId: NullableUtils.Unwrap(groupMembership.Id));
            Assert.IsTrue(getGroupMembership.Id == groupMembership.Id);
            GroupMembership updatedGroupMembership = await client.Memberships.UpdateGroupMembershipByIdAsync(groupMembershipId: NullableUtils.Unwrap(groupMembership.Id), requestBody: new UpdateGroupMembershipByIdRequestBody() { Role = UpdateGroupMembershipByIdRequestBodyRoleField.Admin });
            Assert.IsTrue(updatedGroupMembership.Id == groupMembership.Id);
            Assert.IsTrue(StringUtils.ToStringRepresentation(updatedGroupMembership.Role?.Value) == "admin");
            await client.Memberships.DeleteGroupMembershipByIdAsync(groupMembershipId: NullableUtils.Unwrap(groupMembership.Id));
            await Assert.That.IsExceptionAsync(async() => await client.Memberships.GetGroupMembershipByIdAsync(groupMembershipId: NullableUtils.Unwrap(groupMembership.Id)));
            await client.Groups.DeleteGroupByIdAsync(groupId: group.Id);
            await client.Users.DeleteUserByIdAsync(userId: user.Id);
        }

    }
}