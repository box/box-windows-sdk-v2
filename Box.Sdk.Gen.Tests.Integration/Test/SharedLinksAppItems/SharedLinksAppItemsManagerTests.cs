using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class SharedLinksAppItemsManagerTests {
        public BoxClient client { get; }

        public SharedLinksAppItemsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestSharedLinksAppItems() {
            string appItemSharedLink = Utils.GetEnvVar(name: "APP_ITEM_SHARED_LINK");
            AppItem appItem = await client.SharedLinksAppItems.FindAppItemForSharedLinkAsync(headers: new FindAppItemForSharedLinkHeaders(boxapi: string.Concat("shared_link=", appItemSharedLink)));
            Assert.IsTrue(StringUtils.ToStringRepresentation(appItem.Type?.Value) == "app_item");
            Assert.IsTrue(appItem.ApplicationType == "hubs");
        }

    }
}