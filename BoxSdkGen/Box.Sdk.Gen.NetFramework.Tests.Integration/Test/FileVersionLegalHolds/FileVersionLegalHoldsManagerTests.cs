using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class FileVersionLegalHoldsManagerTests {
        public BoxClient client { get; }

        public FileVersionLegalHoldsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetFileVersionLegalHolds() {
            const string policyId = "1234567890";
            FileVersionLegalHolds fileVersionLegalHolds = await client.FileVersionLegalHolds.GetFileVersionLegalHoldsAsync(queryParams: new GetFileVersionLegalHoldsQueryParams(policyId: policyId));
            int fileVersionLegalHoldsCount = NullableUtils.Unwrap(fileVersionLegalHolds.Entries).Count;
            Assert.IsTrue(fileVersionLegalHoldsCount >= 0);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetFileVersionLegalHoldById() {
            const string fileVersionLegalHoldId = "987654321";
            await Assert.That.IsExceptionAsync(async() => await client.FileVersionLegalHolds.GetFileVersionLegalHoldByIdAsync(fileVersionLegalHoldId: fileVersionLegalHoldId));
        }

    }
}