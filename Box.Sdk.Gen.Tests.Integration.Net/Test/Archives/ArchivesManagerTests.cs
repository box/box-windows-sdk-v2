using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class ArchivesManagerTests {
        public string userId { get; }

        public BoxClient client { get; }

        public ArchivesManagerTests() {
            userId = Utils.GetEnvVar(name: "USER_ID");
            client = new CommonsManager().GetDefaultClientWithUserSubject(userId: userId);
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestArchivesCreateListDelete() {
            string archiveName = Utils.GetUUID();
            const string archiveDescription = "Test Archive Description";
            ArchiveV2025R0 archive = await client.Archives.CreateArchiveV2025R0Async(requestBody: new CreateArchiveV2025R0RequestBody(name: archiveName) { Description = archiveDescription });
            Assert.IsTrue(StringUtils.ToStringRepresentation(archive.Type?.Value) == "archive");
            Assert.IsTrue(archive.Name == archiveName);
            Assert.IsTrue(archive.Description == archiveDescription);
            string newArchiveName = Utils.GetUUID();
            const string newArchiveDescription = "Updated Archive Description";
            ArchiveV2025R0 updatedArchive = await client.Archives.UpdateArchiveByIdV2025R0Async(archiveId: archive.Id, requestBody: new UpdateArchiveByIdV2025R0RequestBody() { Name = newArchiveName, Description = newArchiveDescription });
            Assert.IsTrue(updatedArchive.Name == newArchiveName);
            Assert.IsTrue(NullableUtils.Unwrap(updatedArchive.Description) == newArchiveDescription);
            ArchivesV2025R0 archives = await client.Archives.GetArchivesV2025R0Async(queryParams: new GetArchivesV2025R0QueryParams() { Limit = 100L });
            Assert.IsTrue(NullableUtils.Unwrap(archives.Entries).Count > 0);
            await Assert.That.IsExceptionAsync(async() => await client.Archives.DeleteArchiveByIdV2025R0Async(archiveId: archive.Id));
        }

    }
}