using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Test.IntegrationNew.Configuration;
using Box.V2.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.IntegrationNew
{
    [TestClass]
    public class BoxFilesManagerIntegrationTest : TestInFolder
    {
        [TestMethod]
        public async Task UploadAsync_ForSmallFile_ShouldUploadFileToFolder()
        {
            using (var fileStream = new FileStream(GetSmallFilePath(), FileMode.OpenOrCreate))
            {
                var requestParams = new BoxFileRequest()
                {
                    Name = GetUniqueName("file"),
                    Parent = new BoxRequestEntity() { Id = FolderId }
                };

                BoxFile file = await Client.FilesManager.UploadAsync(requestParams, fileStream);

                Assert.IsNotNull(file.Id);

                await DeleteFile(file.Id);
            }
        }

        [TestMethod]
        public async Task DownloadAsync_ForUploadedFile_ShouldReturnSameFileAsTheUploadedFile()
        {
            var uploadedFile = await CreateSmallFile(FolderId);

            var downloadedFile = await Client.FilesManager.DownloadAsync(uploadedFile.Id);

            Stream fileContents = new MemoryStream();
            await downloadedFile.CopyToAsync(fileContents);
            fileContents.Position = 0;

            string base64DownloadedFile;
            string base64UploadedFile;

            base64DownloadedFile = Helper.GetSha1Hash(fileContents);

            using (var fileStream = new FileStream(GetSmallFilePath(), FileMode.OpenOrCreate))
            {
                base64UploadedFile = Helper.GetSha1Hash(fileStream);
            }

            Assert.AreEqual(base64DownloadedFile, base64UploadedFile);
        }

        [TestMethod]
        public async Task GetInformationAsync_ForCorrectFileId_ShouldReturnSameFileAsUploadedFile()
        {
            var uploadedFile = await CreateSmallFile(FolderId);

            var downloadedFile = await Client.FilesManager.GetInformationAsync(uploadedFile.Id);

            Assert.AreEqual(uploadedFile.Sha1, downloadedFile.Sha1);
        }

        [TestMethod]
        public async Task GetDownloadUriAsync_ForCorrectFileId_ShouldReturnCorrectUrl()
        {
            var uploadedFile = await CreateSmallFile(FolderId);

            var downloadUri = await Client.FilesManager.GetDownloadUriAsync(uploadedFile.Id);

            Assert.AreEqual("dl.boxcloud.com", downloadUri.Host);
        }

        [TestMethod]
        public async Task PreflightCheck_ForValidFile_ShouldReturnSuccess()
        {
            var request = new BoxPreflightCheckRequest
            {
                Name = GetUniqueName("file"),
                Parent = new BoxRequestEntity() { Id = FolderId }
            };

            var preflightCheck = await Client.FilesManager.PreflightCheck(request);

            Assert.IsTrue(preflightCheck.Success);
        }

        [TestMethod]
        public async Task PreflightCheckNewVersion_ForValidNewFileVersion_ShouldReturnSuccess()
        {
            var uploadedFile = await CreateSmallFile(FolderId);

            var request = new BoxPreflightCheckRequest
            {
                Id = uploadedFile.Id,
                Name = uploadedFile.Name,
                Size = 5000000
            };

            var preflightCheck = await Client.FilesManager.PreflightCheckNewVersion(uploadedFile.Id, request);

            Assert.IsTrue(preflightCheck.Success);
        }

        [TestMethod]
        public async Task CreateUploadSessionAsync_ForBigFileSize_ShouldReturnValidSession()
        {
            long fileSize = 50000000;

            var request = new BoxFileUploadSessionRequest()
            {
                FolderId = FolderId,
                FileName = GetUniqueName("file"),
                FileSize = fileSize
            };

            var uploadSession = await Client.FilesManager.CreateUploadSessionAsync(request);

            Assert.IsTrue(uploadSession.TotalParts * Convert.ToInt64(uploadSession.PartSize) >= fileSize);
        }

        [TestMethod]
        public async Task CreateNewVersionUploadSessionAsync_ForBigFileSize_ShouldReturnValidSession()
        {
            var uploadedFile = await CreateSmallFile(FolderId);
            long fileSize = 50000000;

            var request = new BoxFileUploadSessionRequest()
            {
                FileName = GetUniqueName("file"),
                FileSize = fileSize
            };

            var uploadSession = await Client.FilesManager.CreateNewVersionUploadSessionAsync(uploadedFile.Id, request);

            Assert.IsTrue(uploadSession.TotalParts * Convert.ToInt64(uploadSession.PartSize) >= fileSize);
        }

        [TestMethod]
        public async Task UploadNewVersionAsync_ForNewFileVersion_ShouldReturnDifferentFile()
        {
            var uploadedFile = await CreateSmallFile(FolderId);

            using (var fileStream = new FileStream(GetSmallFileV2Path(), FileMode.OpenOrCreate))
            {
                BoxFile newVersionFile = await Client.FilesManager.UploadNewVersionAsync(GetUniqueName("file"), uploadedFile.Id, fileStream);

                Assert.AreNotEqual(uploadedFile.FileVersion.Id, newVersionFile.FileVersion.Id);
            }
        }

        [TestMethod]
        public async Task UpdateFileInformation_ForNewDispositionDate_ShouldBeAbleToUpdateIt()
        {
            var uploadedFile = await CreateSmallFile(FolderId);
            await CreateRetentionPolicy(FolderId);

            var newDispositionDate = DateTimeOffset.Now.AddDays(1);
            var boxFileRequest = new BoxFileRequest
            {
                Id = uploadedFile.Id,
                DispositionAt = newDispositionDate
            };

            await Client.FilesManager.UpdateInformationAsync(boxFileRequest);

            var response = await Client.FilesManager.GetInformationAsync(uploadedFile.Id, new List<string>() { "disposition_at" });

            Assert.AreEqual(newDispositionDate, response.DispositionAt);
        }
    }
}
