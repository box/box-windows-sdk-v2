using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class TrashedItemsManagerTests {
        public BoxClient client { get; }

        public TrashedItemsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestListTrashedItems() {
            FileFull file = await new CommonsManager().UploadNewFileAsync();
            await client.Files.DeleteFileByIdAsync(fileId: file.Id);
            Items trashedItems = await client.TrashedItems.GetTrashedItemsAsync();
            Assert.IsTrue(NullableUtils.Unwrap(trashedItems.Entries).Count > 0);
        }

    }
}