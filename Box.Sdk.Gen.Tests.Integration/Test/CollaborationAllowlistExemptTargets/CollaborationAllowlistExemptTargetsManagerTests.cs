using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class CollaborationAllowlistExemptTargetsManagerTests {
        public BoxClient client { get; }

        public CollaborationAllowlistExemptTargetsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCollaborationAllowlistExemptTargets() {
            CollaborationAllowlistExemptTargets exemptTargets = await client.CollaborationAllowlistExemptTargets.GetCollaborationWhitelistExemptTargetsAsync();
            Assert.IsTrue(NullableUtils.Unwrap(exemptTargets.Entries).Count >= 0);
            UserFull user = await client.Users.CreateUserAsync(requestBody: new CreateUserRequestBody(name: Utils.GetUUID()) { Login = string.Concat(Utils.GetUUID(), "@boxdemo.com"), IsPlatformAccessOnly = true });
            CollaborationAllowlistExemptTarget newExemptTarget = await client.CollaborationAllowlistExemptTargets.CreateCollaborationWhitelistExemptTargetAsync(requestBody: new CreateCollaborationWhitelistExemptTargetRequestBody(user: new CreateCollaborationWhitelistExemptTargetRequestBodyUserField(id: user.Id)));
            Assert.IsTrue(StringUtils.ToStringRepresentation(newExemptTarget.Type?.Value) == "collaboration_whitelist_exempt_target");
            Assert.IsTrue(NullableUtils.Unwrap(newExemptTarget.User).Id == user.Id);
            CollaborationAllowlistExemptTarget exemptTarget = await client.CollaborationAllowlistExemptTargets.GetCollaborationWhitelistExemptTargetByIdAsync(collaborationWhitelistExemptTargetId: NullableUtils.Unwrap(newExemptTarget.Id));
            Assert.IsTrue(exemptTarget.Id == newExemptTarget.Id);
            Assert.IsTrue(NullableUtils.Unwrap(exemptTarget.User).Id == user.Id);
            await client.CollaborationAllowlistExemptTargets.DeleteCollaborationWhitelistExemptTargetByIdAsync(collaborationWhitelistExemptTargetId: NullableUtils.Unwrap(exemptTarget.Id));
            await Assert.That.IsExceptionAsync(async() => await client.CollaborationAllowlistExemptTargets.GetCollaborationWhitelistExemptTargetByIdAsync(collaborationWhitelistExemptTargetId: NullableUtils.Unwrap(exemptTarget.Id)));
            await client.Users.DeleteUserByIdAsync(userId: user.Id);
        }

    }
}