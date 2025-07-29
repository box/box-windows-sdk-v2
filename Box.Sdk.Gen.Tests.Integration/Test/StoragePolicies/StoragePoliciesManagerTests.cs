using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class StoragePoliciesManagerTests {
        public string userId { get; }

        public StoragePoliciesManagerTests() {
            userId = Utils.GetEnvVar(name: "USER_ID");
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetStoragePolicies() {
            BoxClient client = new CommonsManager().GetDefaultClientWithUserSubject(userId: userId);
            StoragePolicies storagePolicies = await client.StoragePolicies.GetStoragePoliciesAsync();
            StoragePolicy storagePolicy = NullableUtils.Unwrap(storagePolicies.Entries)[0];
            Assert.IsTrue(StringUtils.ToStringRepresentation(storagePolicy.Type?.Value) == "storage_policy");
            StoragePolicy getStoragePolicy = await client.StoragePolicies.GetStoragePolicyByIdAsync(storagePolicyId: storagePolicy.Id);
            Assert.IsTrue(getStoragePolicy.Id == storagePolicy.Id);
        }

    }
}