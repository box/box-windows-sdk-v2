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
        //[TestMethod]
        //public async Task GetStreamResponse()
        //{
        //    const string pdfFileId = "16894929979";
        //    const int totalPages = 227;
        //    var filePreview = await _client.FilesManager.GetFilePreviewAsync(pdfFileId, 1);

        //    Assert.AreEqual(1, filePreview.CurrentPage, "Invalid current page");
        //    Assert.AreEqual(totalPages, filePreview.TotalPages, "Invalid total pages");
        //    Assert.AreEqual(HttpStatusCode.OK, filePreview.ReturnedStatusCode, "Invalid status code");
        //}

        [TestMethod]
        public async Task GetInformation_Fields_ValidResponse()
        {
            const string fileId = "16894947279";
            var file = await _client.FilesManager.GetInformationAsync(fileId, new List<string> { BoxFile.FieldName, BoxFile.FieldModifiedAt, BoxFile.FieldOwnedBy });

            Assert.AreEqual(fileId, file.Id, "Incorrect file id");
            Assert.IsNotNull(file.Name, "File Name is null");
            Assert.IsNotNull(file.ModifiedAt, "ModifiedAt field is null");
            Assert.IsNotNull(file.OwnedBy, "OwnedBy field is null");
        }

        [TestMethod]
        public async Task Download_ValidRequest_ValidStream()
        {
            const string fileId = "16894947279";
            var responseStream = await _client.FilesManager.DownloadStreamAsync(fileId);
            Assert.IsNotNull(responseStream, "Response stream is null");
        }

        [TestMethod]
        public async Task GetThumbnail_ValidRequest_ValidThumbnail()
        {
            const string fileId = "16894947279";

            using (Stream stream = await _client.FilesManager.GetThumbnailAsync(fileId, 256, 256))
            using (FileStream fs = new FileStream(string.Format(GetSaveFolderPath(), "thumb.png"), FileMode.OpenOrCreate))
            {
                Assert.IsNotNull(stream, "Stream is Null");
                
                await stream.CopyToAsync(fs);

                Assert.IsNotNull(fs, "FileStream is null");
            }
        }


        [TestMethod]
        public async Task GetSharedLink_ValidRequest_ValidSharedLink()
        {
            string imageFileId1 = "16894947279";

            BoxSharedLinkRequest linkReq = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open
            };

            BoxFile fileLink = await _client.FilesManager.CreateSharedLinkAsync(imageFileId1, linkReq);
            Assert.AreEqual(BoxSharedLinkAccessType.open, fileLink.SharedLink.Access);
        }

        [TestMethod]
        public async Task Watermark_Files_CRUD()
        {
            const string fileId = "16894944949";

            var mylist = new List<string>(new string[] { "watermark_info" });
            var file = await _client.FilesManager.GetInformationAsync(fileId, mylist);
            Assert.IsFalse(file.WatermarkInfo.IsWatermarked);

            var watermark = await _client.FilesManager.ApplyWatermarkAsync(fileId);
            Assert.IsNotNull(watermark, "Failed to apply watermark to file");

            file = await _client.FilesManager.GetInformationAsync(fileId, mylist);
            Assert.IsTrue(file.WatermarkInfo.IsWatermarked);

            var fetchedWatermark = await _client.FilesManager.GetWatermarkAsync(fileId);
            Assert.IsNotNull(fetchedWatermark, "Failed to fetch watermark of file");

            var result = await _client.FilesManager.RemoveWatermarkAsync(fileId);
            Assert.IsTrue(result, "Failed to remove watermark from file");

        }

        [TestMethod]
        public async Task FileWorkflow_ValidRequest_ValidResponse()
        {
            //file Ids of the two files to download
            string imageFileId1 = "16894947279";
            string imageFileId2 = "16894946307";
            
            // paths to store the two downloaded files
            var dlPath1 = string.Format(GetSaveFolderPath(), "thumbnail1.png");
            var dlPath2 = string.Format(GetSaveFolderPath(), "thumbnail2.png");

            //download 2 files
            using (FileStream fs = new FileStream(dlPath1, FileMode.OpenOrCreate))
            {
                Stream stream = await _client.FilesManager.DownloadStreamAsync(imageFileId1);
                await stream.CopyToAsync(fs);
            }

            using (FileStream fs = new FileStream(dlPath2, FileMode.OpenOrCreate))
            {
                Stream stream = await _client.FilesManager.DownloadStreamAsync(imageFileId2);
                await stream.CopyToAsync(fs);
            }

            // File name to use to upload files
            string uploadFileName = "testUpload.png";

            // Upload file at dlPath1
            BoxFile file;
            using (FileStream fs = new FileStream(dlPath1, FileMode.Open))
            {
                BoxFileRequest req = new BoxFileRequest()
                {
                    Name = uploadFileName,
                    Parent = new BoxRequestEntity() { Id = "0" }
                };

                file = await _client.FilesManager.UploadAsync(req, fs);
            }

            Assert.AreEqual(uploadFileName, file.Name, "Incorrect file name");
            Assert.AreEqual("0", file.Parent.Id, "Incorrect destination folder");

            // Upload file at dlPath2 as new version of 'file'
            BoxFile newFileVersion;
            using (FileStream fs = new FileStream(dlPath2, FileMode.Open))
            {
                newFileVersion = await _client.FilesManager.UploadNewVersionAsync(uploadFileName, file.Id, fs, file.ETag);
            }

            Assert.AreEqual(newFileVersion.Name, uploadFileName);
            Assert.AreEqual(newFileVersion.Parent.Id, "0");

            //View file versions (Requires upgraded acct)
            BoxCollection<BoxFileVersion> fileVersions = await _client.FilesManager.ViewVersionsAsync(newFileVersion.Id);

            Assert.AreEqual(fileVersions.TotalCount, 1, "Incorrect number of versions");

            foreach (var f in fileVersions.Entries)
            {
                Assert.AreEqual(uploadFileName, f.Name);
            }

            // Update the file name of a file
            string updateName = GetUniqueName();

            BoxFileRequest updateReq = new BoxFileRequest()
            {
                Id = file.Id,
                Name = updateName
            };
            BoxFile fileUpdate = await _client.FilesManager.UpdateInformationAsync(updateReq);

            Assert.AreEqual(file.Id, fileUpdate.Id, "File Ids are not the same");
            Assert.AreEqual(updateName, fileUpdate.Name, "File Names are not the same");

            // Test create shared link
            BoxSharedLinkRequest linkReq = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open
            };
            BoxFile fileWithLink = await _client.FilesManager.CreateSharedLinkAsync(newFileVersion.Id, linkReq);

            Assert.AreEqual(BoxSharedLinkAccessType.open, fileWithLink.SharedLink.Access, "Incorrect access for shared link");

            // Copy file in same folder
            string copyName = GetUniqueName();
            BoxFileRequest copyReq = new BoxFileRequest()
            {
                Id = fileWithLink.Id,
                Name = copyName,
                Parent = new BoxRequestEntity() { Id = "0" }
            };
            BoxFile fileCopy = await _client.FilesManager.CopyAsync(copyReq);

            Assert.AreEqual(fileCopy.Name, copyName, "Incorrect Name for copied file");
            Assert.AreEqual(fileCopy.Parent.Id, "0", "Incorrect parent folder for copied file");

            // Test get file information
            BoxFile fileInfo = await _client.FilesManager.GetInformationAsync(fileWithLink.Id);

            Assert.AreEqual(updateName, fileWithLink.Name, "File name is incorrect");
            Assert.AreEqual("0", fileWithLink.Parent.Id, "Parent folder is incorrect");

            // Delete both files
            await _client.FilesManager.DeleteAsync(fileCopy.Id, fileCopy.ETag);
            await _client.FilesManager.DeleteAsync(fileWithLink.Id, fileWithLink.ETag);
        }
        
        //[TestMethod]
        //public async Task BatchDownload_ValidRequest_ValidResponse()
        //{
        //    const string fileId = "16894947279";

        //    /*** Arrange ***/
        //    List<Task<Stream>> tasks = new List<Task<Stream>>();

        //    int size = 1420;
        //    int numTasks = 5;

        //    /*** Act ***/
        //    for (int i = 0; i < numTasks; ++i)
        //        tasks.Add(_client.FilesManager.DownloadStreamAsync(fileId));

        //    await Task.WhenAll(tasks);

        //    /*** Assert ***/
        //    foreach (var t in tasks)
        //    {
        //        Assert.AreEqual(size, (await t).Length);
        //    }
        //}

        private string GetSaveFolderPath()
        {
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(pathUser, "Downloads") + "\\{0}";
        }
    }
}
