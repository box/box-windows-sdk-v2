using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Box.V2.Models;
using System.Net;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxFilesManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        private const string FileId = "8599229215";

        private const string savePath = @"C:\Users\btang\Downloads\{0}";

        private const string MobileBoxerFileId = "8250445374";

        [TestMethod]
        public async Task GetInformation_Fields_ValidResponse()
        {
            var test = await _client.FilesManager.GetInformationAsync(FileId, new List<string> { BoxFile.FieldName, BoxFile.FieldModifiedAt, BoxFile.FieldOwnedBy });
        }

        [TestMethod]
        public async Task GetStreamResponse()
        {
            var filePreview = await _client.FilesManager.GetFilePreviewAsync(MobileBoxerFileId, 1);

            Assert.AreEqual(1, filePreview.CurrentPage);
            Assert.AreEqual(4, filePreview.TotalPages);
            Assert.AreEqual(HttpStatusCode.OK, filePreview.ReturnedStatusCode);
        }


        [TestMethod]
        public async Task Download_ValidRequest_ValidStream()
        {
            var test = await _client.FilesManager.DownloadStreamAsync(FileId);
        }

        [TestMethod]
        public async Task BatchDownload_ValidRequest_ValidResponse()
        {
            /*** Arrange ***/
            List<Task<Stream>> tasks = new List<Task<Stream>>();

            int size = 1420;
            int numTasks = 50;

            /*** Act ***/
            for (int i = 0; i < numTasks; ++i)
                tasks.Add(_client.FilesManager.DownloadStreamAsync(FileId));

            await Task.WhenAll(tasks);

            /*** Assert ***/
            foreach (var t in tasks)
            {
                Assert.AreEqual(size, (await t).Length);
            }
        }

        [TestMethod]
        public async Task GetInformation_ValidRequest_ValidFile()
        {
            var f = await _client.FilesManager.GetInformationAsync(FileId);
            Assert.AreEqual(FileId, f.Id);
        }

        [TestMethod]
        public async Task GetThumbnail_ValidRequest_ValidThumbnail()
        {
            using (Stream stream = await _client.FilesManager.GetThumbnailAsync(FileId, 256, 256))
            using (FileStream fs = new FileStream(string.Format(savePath, "thumb.png"), FileMode.OpenOrCreate))
            {
                await stream.CopyToAsync(fs);
            }
        }

        [TestMethod]
        public async Task GetSharedLink_ValidRequest_ValidSharedLink()
        {
            BoxSharedLinkRequest linkReq = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open
            };
            
            BoxFile fileLink = await _client.FilesManager.CreateSharedLinkAsync("11999421592", linkReq);
            Assert.AreEqual(BoxSharedLinkAccessType.open, fileLink.SharedLink.Access);
        }

        [TestMethod]
        public async Task FileWorkflow_ValidRequest_ValidResponse()
        {
            string fileName = "reimages.zip";
            string saveName = "reimages2.zip";


            string filePath = string.Format(savePath, fileName);
            string dlPath = string.Format(savePath, saveName);

            // Test upload a file
            BoxFile file;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BoxFileRequest req = new BoxFileRequest()
                {
                    Name = "reimages.zip",
                    Parent = new BoxRequestEntity() { Id = "0" }
                };

                file = await _client.FilesManager.UploadAsync(req, fs);
            }

            Assert.AreEqual(fileName, file.Name);
            Assert.AreEqual("0", file.Parent.Id);

            // Test upload a new version
            BoxFile newFile;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                newFile = await _client.FilesManager.UploadNewVersionAsync(fileName, file.Id, fs, file.ETag);
            }

            Assert.AreEqual(newFile.Name, fileName);
            Assert.AreEqual(newFile.Parent.Id, "0");

            // Test view file versions (Requires upgraded acct)
            //BoxCollection<BoxFile> versions = await _client.FilesManager.ViewVersionsAsync(newFile.Id);

            //Assert.AreEqual(versions.TotalCount, 2);
            //foreach (var f in versions.Entries)
            //{
            //    Assert.AreEqual(fileName, f.Name);
            //    Assert.AreEqual("0", f.Parent.Id);
            //}

            // Test update a file
            string updateName = GetUniqueName();
            BoxFileRequest updateReq = new BoxFileRequest()
            {
                Id = file.Id,
                Name = updateName,
                Description = updateName
            };
            BoxFile fileUpdate = await _client.FilesManager.UpdateInformationAsync(updateReq);

            Assert.AreEqual(file.Id, fileUpdate.Id);
            Assert.AreEqual(updateName, fileUpdate.Name);
            Assert.AreEqual(updateName, fileUpdate.Description);

            // Test create shared link
            BoxSharedLinkRequest linkReq = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open
            };
            BoxFile fileLink = await _client.FilesManager.CreateSharedLinkAsync(newFile.Id, linkReq);

            Assert.AreEqual(BoxSharedLinkAccessType.open, fileLink.SharedLink.Access);

            // Test copy a file
            string copyName = GetUniqueName();
            BoxFileRequest copyReq = new BoxFileRequest()
            {
                Id = newFile.Id,
                Name = copyName,
                Parent = new BoxRequestEntity() { Id = "0" }
            };
            BoxFile fileCopy = await _client.FilesManager.CopyAsync(copyReq);

            Assert.AreEqual(fileCopy.Name, copyName);
            Assert.AreEqual(fileCopy.Parent.Id, "0");

            // Test download a file
            using (FileStream fs = new FileStream(dlPath, FileMode.OpenOrCreate))
            {
                Stream stream = await _client.FilesManager.DownloadStreamAsync(file.Id);
                await stream.CopyToAsync(fs);
            }

            // Test get file information
            BoxFile fileInfo = await _client.FilesManager.GetInformationAsync(file.Id);

            Assert.AreEqual(updateName, fileInfo.Name);
            Assert.AreEqual("0", file.Parent.Id);

            BoxFile newFileInfo = await _client.FilesManager.GetInformationAsync(newFile.Id);

            Assert.AreEqual(updateName, newFileInfo.Name);
            Assert.AreEqual("0", newFileInfo.Parent.Id);

            // Test delete a file
            await _client.FilesManager.DeleteAsync(fileCopy.Id, fileCopy.ETag);
            await _client.FilesManager.DeleteAsync(newFile.Id, newFileInfo.ETag);


        }
    }
}
