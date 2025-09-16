using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class ZipDownloadsManagerTests {
        public BoxClient client { get; }

        public ZipDownloadsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestZipDownload() {
            FileFull file1 = await new CommonsManager().UploadNewFileAsync();
            FileFull file2 = await new CommonsManager().UploadNewFileAsync();
            FolderFull folder1 = await new CommonsManager().CreateNewFolderAsync();
            System.IO.Stream zipStream = await client.ZipDownloads.DownloadZipAsync(requestBody: new ZipDownloadRequest(items: Array.AsReadOnly(new [] {new ZipDownloadRequestItemsField(id: file1.Id, type: ZipDownloadRequestItemsTypeField.File),new ZipDownloadRequestItemsField(id: file2.Id, type: ZipDownloadRequestItemsTypeField.File),new ZipDownloadRequestItemsField(id: folder1.Id, type: ZipDownloadRequestItemsTypeField.Folder)})) { DownloadFileName = "zip" });
            Assert.IsTrue(Utils.BufferEquals(buffer1: await Utils.ReadByteStreamAsync(byteStream: zipStream), buffer2: Utils.GenerateByteBuffer(size: 10)) == false);
            await client.Files.DeleteFileByIdAsync(fileId: file1.Id);
            await client.Files.DeleteFileByIdAsync(fileId: file2.Id);
            await client.Folders.DeleteFolderByIdAsync(folderId: folder1.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestManualZipDownloadAndCheckStatus() {
            FileFull file1 = await new CommonsManager().UploadNewFileAsync();
            FileFull file2 = await new CommonsManager().UploadNewFileAsync();
            FolderFull folder1 = await new CommonsManager().CreateNewFolderAsync();
            ZipDownload zipDownload = await client.ZipDownloads.CreateZipDownloadAsync(requestBody: new ZipDownloadRequest(items: Array.AsReadOnly(new [] {new ZipDownloadRequestItemsField(id: file1.Id, type: ZipDownloadRequestItemsTypeField.File),new ZipDownloadRequestItemsField(id: file2.Id, type: ZipDownloadRequestItemsTypeField.File),new ZipDownloadRequestItemsField(id: folder1.Id, type: ZipDownloadRequestItemsTypeField.Folder)})) { DownloadFileName = "zip" });
            Assert.IsTrue(zipDownload.DownloadUrl != "");
            Assert.IsTrue(zipDownload.StatusUrl != "");
            Assert.IsTrue(Utils.DateTimeToString(dateTime: NullableUtils.Unwrap(zipDownload.ExpiresAt)) != "");
            System.IO.Stream zipStream = await client.ZipDownloads.GetZipDownloadContentAsync(downloadUrl: NullableUtils.Unwrap(zipDownload.DownloadUrl));
            Assert.IsTrue(Utils.BufferEquals(buffer1: await Utils.ReadByteStreamAsync(byteStream: zipStream), buffer2: Utils.GenerateByteBuffer(size: 10)) == false);
            ZipDownloadStatus zipDownloadStatus = await client.ZipDownloads.GetZipDownloadStatusAsync(statusUrl: NullableUtils.Unwrap(zipDownload.StatusUrl));
            Assert.IsTrue(zipDownloadStatus.TotalFileCount == 2);
            Assert.IsTrue(zipDownloadStatus.DownloadedFileCount == 2);
            Assert.IsTrue(zipDownloadStatus.SkippedFileCount == 0);
            Assert.IsTrue(zipDownloadStatus.SkippedFolderCount == 0);
            Assert.IsTrue(StringUtils.ToStringRepresentation(zipDownloadStatus.State?.Value) != "failed");
            await client.Files.DeleteFileByIdAsync(fileId: file1.Id);
            await client.Files.DeleteFileByIdAsync(fileId: file2.Id);
            await client.Folders.DeleteFolderByIdAsync(folderId: folder1.Id);
        }

    }
}