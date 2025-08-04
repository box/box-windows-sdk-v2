using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class RecentItemsManagerTests {
        [RetryableTest]
        public async System.Threading.Tasks.Task TestRecentItems() {
            BoxClient client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
            RecentItems recentItems = await client.RecentItems.GetRecentItemsAsync();
            Assert.IsTrue(NullableUtils.Unwrap(recentItems.Entries).Count >= 0);
        }

    }
}