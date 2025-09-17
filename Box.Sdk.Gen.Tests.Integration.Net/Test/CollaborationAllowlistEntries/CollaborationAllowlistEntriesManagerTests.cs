using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class CollaborationAllowlistEntriesManagerTests {
        public BoxClient client { get; }

        public CollaborationAllowlistEntriesManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCollaborationAllowlistEntries() {
            CollaborationAllowlistEntries allowlist = await client.CollaborationAllowlistEntries.GetCollaborationWhitelistEntriesAsync();
            Assert.IsTrue(NullableUtils.Unwrap(allowlist.Entries).Count >= 0);
            string domain = string.Concat(Utils.GetUUID(), "example.com");
            CollaborationAllowlistEntry newEntry = await client.CollaborationAllowlistEntries.CreateCollaborationWhitelistEntryAsync(requestBody: new CreateCollaborationWhitelistEntryRequestBody(direction: CreateCollaborationWhitelistEntryRequestBodyDirectionField.Inbound, domain: domain));
            Assert.IsTrue(StringUtils.ToStringRepresentation(newEntry.Type?.Value) == "collaboration_whitelist_entry");
            Assert.IsTrue(StringUtils.ToStringRepresentation(newEntry.Direction?.Value) == "inbound");
            Assert.IsTrue(newEntry.Domain == domain);
            CollaborationAllowlistEntry entry = await client.CollaborationAllowlistEntries.GetCollaborationWhitelistEntryByIdAsync(collaborationWhitelistEntryId: NullableUtils.Unwrap(newEntry.Id));
            Assert.IsTrue(entry.Id == newEntry.Id);
            Assert.IsTrue(StringUtils.ToStringRepresentation(entry.Direction?.Value) == StringUtils.ToStringRepresentation(newEntry.Direction?.Value));
            Assert.IsTrue(entry.Domain == domain);
            await client.CollaborationAllowlistEntries.DeleteCollaborationWhitelistEntryByIdAsync(collaborationWhitelistEntryId: NullableUtils.Unwrap(entry.Id));
            await Assert.That.IsExceptionAsync(async() => await client.CollaborationAllowlistEntries.GetCollaborationWhitelistEntryByIdAsync(collaborationWhitelistEntryId: NullableUtils.Unwrap(entry.Id)));
        }

    }
}