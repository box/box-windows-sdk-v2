using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class FolderWatermarksManagerTests {
        public BoxClient client { get; }

        public FolderWatermarksManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateGetDeleteFolderWatermark() {
            string folderName = Utils.GetUUID();
            FolderFull folder = await client.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: folderName, parent: new CreateFolderRequestBodyParentField(id: "0")));
            Watermark createdWatermark = await client.FolderWatermarks.UpdateFolderWatermarkAsync(folderId: folder.Id, requestBody: new UpdateFolderWatermarkRequestBody(watermark: new UpdateFolderWatermarkRequestBodyWatermarkField(imprint: UpdateFolderWatermarkRequestBodyWatermarkImprintField.Default)));
            Watermark watermark = await client.FolderWatermarks.GetFolderWatermarkAsync(folderId: folder.Id);
            await client.FolderWatermarks.DeleteFolderWatermarkAsync(folderId: folder.Id);
            await Assert.That.IsExceptionAsync(async() => await client.FolderWatermarks.GetFolderWatermarkAsync(folderId: folder.Id));
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

    }
}