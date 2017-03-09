using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Box.V2.Models;
using System.Net;
using System.Security.Cryptography;
using Box.V2.Utility;

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

        [TestMethod]
        public async Task UploadFileInSession_AbortRequest_FileNotCommmited()
        {
            long fileSize = 9000000;
            MemoryStream fileInMemoryStream = GetBigFileInMemoryStream(fileSize);
            string remoteFileName = "UploadedUsingSession-" + DateTime.Now.TimeOfDay;
            string parentFolderId = "0";

            BoxFileUploadSessionRequest boxFileUploadSessionRequest = new BoxFileUploadSessionRequest()
            {
                FolderId = parentFolderId,
                FileName = remoteFileName,
                FileSize = fileSize
            };
            // Create Upload Session
            BoxFileUploadSession boxFileUploadSession = await _client.FilesManager.CreateUploadSessionAsync(boxFileUploadSessionRequest);
            BoxSessionEndpoint boxSessionEndpoint = boxFileUploadSession.SessionEndpoints;
            Uri abortUri = new Uri(boxSessionEndpoint.Abort);
            Uri uploadPartUri = new Uri(boxSessionEndpoint.UploadPart);
            string partSize = boxFileUploadSession.PartSize;
            long partSizeLong;
            long.TryParse(partSize, out partSizeLong);
            int numberOfParts = Helper.GetNumberOfParts(fileSize, partSizeLong);

            // Upload parts in the session
            await UploadPartsInSessionAsync(uploadPartUri, numberOfParts, partSizeLong, fileInMemoryStream, fileSize);

            // Assert file is not committed/uploaded to box yet
            Assert.IsFalse(await DoesFileExistInFolder(parentFolderId, remoteFileName));

            // Abort
            await _client.FilesManager.DeleteUploadSessionAsync(abortUri);

            // Assert file is not committed/uploaded to box
            Assert.IsFalse(await DoesFileExistInFolder(parentFolderId, remoteFileName));
        }

        [TestMethod]
        public async Task UploadFileInSession_CommitSession_FilePresent()
        {
            long fileSize = 19000000;
            MemoryStream fileInMemoryStream = GetBigFileInMemoryStream(fileSize);
            string remoteFileName = "UploadedUsingSession-" + DateTime.Now.TimeOfDay;
            string parentFolderId = "0";

            BoxFileUploadSessionRequest boxFileUploadSessionRequest = new BoxFileUploadSessionRequest()
            {
                FolderId = parentFolderId,
                FileName = remoteFileName,
                FileSize = fileSize
            };
            // Create Upload Session
            BoxFileUploadSession boxFileUploadSession = await _client.FilesManager.CreateUploadSessionAsync(boxFileUploadSessionRequest);
            BoxSessionEndpoint boxSessionEndpoint = boxFileUploadSession.SessionEndpoints;
            Uri listPartsUri = new Uri(boxSessionEndpoint.ListParts);
            Uri uploadPartUri = new Uri(boxSessionEndpoint.UploadPart);
            Uri commitUri = new Uri(boxSessionEndpoint.Commit);
            string partSize = boxFileUploadSession.PartSize;
            long partSizeLong;
            long.TryParse(partSize, out partSizeLong);
            int numberOfParts = Helper.GetNumberOfParts(fileSize, partSizeLong);

            // Upload parts in the session
            await UploadPartsInSessionAsync(uploadPartUri, numberOfParts, partSizeLong, fileInMemoryStream, fileSize);

            // Assert file is not committed/uploaded to box yet
            Assert.IsFalse(await DoesFileExistInFolder(parentFolderId, remoteFileName));

            // Get upload parts (1 by 1) for Integration testing purposes
            List<BoxSessionPartInfo> allSessionParts = new List<BoxSessionPartInfo>();
            BoxSessionParts boxSessionParts = await _client.FilesManager.GetSessionUploadedPartsAsync(listPartsUri, null, 1);
            allSessionParts.AddRange(boxSessionParts.Parts);
            while ( !string.IsNullOrWhiteSpace(boxSessionParts.Marker) )
            {
                boxSessionParts = await _client.FilesManager.GetSessionUploadedPartsAsync(listPartsUri, boxSessionParts.Marker, 1);
                allSessionParts.AddRange(boxSessionParts.Parts);
            }
            BoxSessionParts sessionPartsForCommit = new BoxSessionParts(allSessionParts);

            // Commit
            await _client.FilesManager.CommitSessionAsync(commitUri, GetSha1Hash(fileInMemoryStream), sessionPartsForCommit);

            // Assert file is committed/uploaded to box
            Assert.IsTrue(await DoesFileExistInFolder(parentFolderId, remoteFileName));

            // Delete file
            string fileId = await GetFileId(parentFolderId, remoteFileName);
            if (!string.IsNullOrWhiteSpace(fileId))
            {
                await _client.FilesManager.DeleteAsync(fileId);
            }

            // Assert file has been deleted from Box
            Assert.IsFalse(await DoesFileExistInFolder(parentFolderId, remoteFileName));
        }

        [TestMethod]
        public async Task UploadFileInSession_Utility_Function_FilePresent()
        {
            long fileSize = 19000000;
            MemoryStream fileInMemoryStream = GetBigFileInMemoryStream(fileSize);
            string remoteFileName = "UploadedUsingSession-" + DateTime.Now.TimeOfDay;
            string parentFolderId = "0";

            // Call Utility function
            await _client.FilesManager.UploadUsingSessionAsync(fileInMemoryStream, remoteFileName, fileSize, parentFolderId, GetSha1Hash);

            // Assert file is committed/uploaded to box
            Assert.IsTrue(await DoesFileExistInFolder(parentFolderId, remoteFileName));

            // Delete file
            string fileId = await GetFileId(parentFolderId, remoteFileName);
            if (!string.IsNullOrWhiteSpace(fileId))
            {
                await _client.FilesManager.DeleteAsync(fileId);
            }

            // Assert file has been deleted from Box
            Assert.IsFalse(await DoesFileExistInFolder(parentFolderId, remoteFileName));
        }

        #region Private functions

        private string GetSaveFolderPath()
        {
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(pathUser, "Downloads") + "\\{0}";
        }

        private MemoryStream GetBigFileInMemoryStream(long fileSize)
        {
            // Create random data to write to the file.
            byte[] dataArray = new byte[fileSize];
            new Random().NextBytes(dataArray);
            MemoryStream memoryStream = new MemoryStream(dataArray);
            return memoryStream;
        }

        private async Task UploadPartsInSessionAsync(Uri uploadPartsUri, int numberOfParts, long partSize, Stream stream,
            long fileSize)
        {
            for (int i = 0; i < numberOfParts; i++)
            {
                string uniqueRandomPartId = Helper.GetRandomString(8);
                // Split file as per part size
                long partOffset = partSize * i;
                Stream partFileStream = Helper.GetFilePart(stream, partSize, partOffset);
                string sha = GetSha1Hash(partFileStream);
                partFileStream.Position = 0;
                await
                    _client.FilesManager.UploadPartAsync(uploadPartsUri, sha, uniqueRandomPartId, partOffset, fileSize,
                        partFileStream);
            }
        }

        private string GetSha1Hash(Stream stream)
        {
            stream.Position = 0;
            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(stream);
            string base64String = Convert.ToBase64String(hash);
            return base64String;
        }

        private async Task<bool> DoesFileExistInFolder(string folderId, string fileName)
        {
            // TODO: Paging
            BoxCollection<BoxItem> boxCollection = await _client.FoldersManager.GetFolderItemsAsync(folderId, 1000);
            return boxCollection.Entries.Any(item => item.Name == fileName);
        }

        private async Task<string> GetFileId(string folderId, string fileName)
        {
            // TODO: Paging
            BoxCollection<BoxItem> boxCollection = await _client.FoldersManager.GetFolderItemsAsync(folderId, 1000);
            return boxCollection.Entries.FirstOrDefault(item => item.Name == fileName)?.Id;
        }

        #endregion
    }
}