using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class SessionTerminationManagerTests {
        public BoxClient client { get; }

        public SessionTerminationManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestSessionTerminationUser() {
            BoxClient adminClient = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
            UserFull user = await adminClient.Users.GetUserMeAsync();
            SessionTerminationMessage result = await client.SessionTermination.TerminateUsersSessionsAsync(requestBody: new TerminateUsersSessionsRequestBody(userIds: Array.AsReadOnly(new [] {Utils.GetEnvVar("USER_ID")}), userLogins: Array.AsReadOnly(new [] {NullableUtils.Unwrap(user.Login)})));
            Assert.IsTrue(result.Message == "Request is successful, please check the admin events for the status of the job");
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestSessionTerminationGroup() {
            string groupName = Utils.GetUUID();
            GroupFull group = await client.Groups.CreateGroupAsync(requestBody: new CreateGroupRequestBody(name: groupName));
            SessionTerminationMessage result = await client.SessionTermination.TerminateGroupsSessionsAsync(requestBody: new TerminateGroupsSessionsRequestBody(groupIds: Array.AsReadOnly(new [] {group.Id})));
            Assert.IsTrue(result.Message == "Request is successful, please check the admin events for the status of the job");
            await client.Groups.DeleteGroupByIdAsync(groupId: group.Id);
        }

    }
}