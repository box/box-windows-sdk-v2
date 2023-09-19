using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Test.Integration.Configuration;
using Box.V2.Test.Integration.Configuration.Extensions;
using Box.V2.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
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

                BoxFile file = await UserClient.FilesManager.UploadAsync(requestParams, fileStream);

                Assert.IsNotNull(file.Id);

                await DeleteFile(file.Id);
            }
        }

        [TestMethod]
        public async Task DownloadAsync_ForUploadedFile_ShouldReturnSameFileAsTheUploadedFile()
        {
            var uploadedFile = await CreateSmallFile(FolderId);

            var downloadedFile = await UserClient.FilesManager.DownloadAsync(uploadedFile.Id);

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
        public async Task DownloadAsync_ForFileWithSharedLink_ShouldReturnSameFileAsTheUploadedFile()
        {
            var folder = await CreateFolderAsAdmin();
            var uploadedFile = await CreateSmallFileAsAdmin(folder.Id);

            var password = "SuperSecret123";
            var sharedLinkRequest = new BoxSharedLinkRequest
            {
                Access = BoxSharedLinkAccessType.open,
                Password = password
            };

            var sharedLink = await AdminClient.FilesManager.CreateSharedLinkAsync(uploadedFile.Id, sharedLinkRequest);

            var downloadedFile = await UserClient.FilesManager.DownloadAsync(uploadedFile.Id, sharedLink: sharedLink.SharedLink.Url, sharedLinkPassword: password);

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

            var downloadedFile = await UserClient.FilesManager.GetInformationAsync(uploadedFile.Id);

            Assert.AreEqual(uploadedFile.Sha1, downloadedFile.Sha1);
        }

        [TestMethod]
        public async Task GetInformationAsync_ForFileWithSharedLink_ShouldReturnSameFileAsUploadedFile()
        {
            var folder = await CreateFolderAsAdmin();
            var uploadedFile = await CreateSmallFileAsAdmin(folder.Id);

            var password = "SuperSecret123";
            var sharedLinkRequest = new BoxSharedLinkRequest
            {
                Access = BoxSharedLinkAccessType.open,
                Password = password
            };

            var sharedLink = await AdminClient.FilesManager.CreateSharedLinkAsync(uploadedFile.Id, sharedLinkRequest);

            var sharedItems = await UserClient.SharedItemsManager.SharedItemsAsync(sharedLink.SharedLink.Url, password);

            BoxFile file = await UserClient.FilesManager.GetInformationAsync(sharedItems.Id, sharedLink: sharedLink.SharedLink.Url, sharedLinkPassword: password);

            Assert.AreEqual(file.Id, uploadedFile.Id);
            Assert.AreEqual(file.Sha1, uploadedFile.Sha1);
        }

        [TestMethod]
        public async Task GetDownloadUriAsync_ForCorrectFileId_ShouldReturnCorrectUrl()
        {
            var uploadedFile = await CreateSmallFile(FolderId);

            var downloadUri = await UserClient.FilesManager.GetDownloadUriAsync(uploadedFile.Id);

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

            var preflightCheck = await UserClient.FilesManager.PreflightCheck(request);

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

            var preflightCheck = await UserClient.FilesManager.PreflightCheckNewVersion(uploadedFile.Id, request);

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

            var uploadSession = await UserClient.FilesManager.CreateUploadSessionAsync(request);

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

            var uploadSession = await UserClient.FilesManager.CreateNewVersionUploadSessionAsync(uploadedFile.Id, request);

            Assert.IsTrue(uploadSession.TotalParts * Convert.ToInt64(uploadSession.PartSize) >= fileSize);
        }

        [TestMethod]
        public async Task UploadNewVersionAsync_ForNewFileVersion_ShouldReturnDifferentFile()
        {
            var uploadedFile = await CreateSmallFile(FolderId);

            using (var fileStream = new FileStream(GetSmallFileV2Path(), FileMode.OpenOrCreate))
            {
                BoxFile newVersionFile = await UserClient.FilesManager.UploadNewVersionAsync(GetUniqueName("file"), uploadedFile.Id, fileStream);

                Assert.AreNotEqual(uploadedFile.FileVersion.Id, newVersionFile.FileVersion.Id);
            }
        }

        //flaky test (too fast update(?))
        [TestMethod]
        public async Task UpdateFileInformation_ForNewDispositionDate_ShouldBeAbleToUpdateIt()
        {
            var adminFolder = await CreateFolderAsAdmin("0");
            var uploadedFile = await CreateSmallFileAsAdmin(adminFolder.Id);
            await CreateRetentionPolicy(adminFolder.Id);

            var newDispositionDate = DateTimeOffset.Now.AddDays(1);
            var boxFileRequest = new BoxFileRequest
            {
                Id = uploadedFile.Id,
                DispositionAt = newDispositionDate
            };

            await Retry(async () =>
            {
                await AdminClient.FilesManager.UpdateInformationAsync(boxFileRequest);

                var response = await AdminClient.FilesManager.GetInformationAsync(uploadedFile.Id, new List<string>() { "disposition_at" });

                Assert.IsTrue(newDispositionDate.IsEqualUpToSeconds(response.DispositionAt.Value));
            });
        }

        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public async Task DownloadAsync_ForTimeoutShorterThanDownloadTime_ShouldAbortDownload()
        {
            var uploadedFile = await CreateSmallFile(FolderId);
            var timeout = new TimeSpan(0, 0, 0, 0, 1);

            await UserClient.FilesManager.DownloadAsync(uploadedFile.Id, timeout: timeout);
        }

        [TestMethod]
        public async Task UploadBigFileInSession_ShouldUploadTheFile_OnlyIfCommitIsCalled()
        {
            long fileSize = 20000000;
            MemoryStream fileInMemoryStream = CreateFileInMemoryStream(fileSize);
            var remoteFileName = GetUniqueName("UploadSession");

            var boxFileUploadSessionRequest = new BoxFileUploadSessionRequest()
            {
                FolderId = FolderId,
                FileName = remoteFileName,
                FileSize = fileSize
            };

            BoxFileUploadSession boxFileUploadSession = await UserClient.FilesManager.CreateUploadSessionAsync(boxFileUploadSessionRequest);
            BoxSessionEndpoint boxSessionEndpoint = boxFileUploadSession.SessionEndpoints;
            var uploadPartUri = new Uri(boxSessionEndpoint.UploadPart);
            var commitUri = new Uri(boxSessionEndpoint.Commit);
            var listPartsUri = new Uri(boxSessionEndpoint.ListParts);
            var partSize = boxFileUploadSession.PartSize;
            long.TryParse(partSize, out var partSizeLong);
            var numberOfParts = GetNumberOfParts(fileSize, partSizeLong);

            await UploadPartsInSessionAsync(uploadPartUri, numberOfParts, partSizeLong, fileInMemoryStream, fileSize);

            // Assert file is not committed/uploaded to box yet
            Assert.IsFalse(await DoesFileExistInFolder(UserClient, FolderId, remoteFileName));

            var allSessionParts = new List<BoxSessionPartInfo>();

            var boxSessionParts = await UserClient.FilesManager.GetSessionUploadedPartsAsync(listPartsUri, null, null, true);

            foreach (var sessionPart in boxSessionParts.Entries)
            {
                allSessionParts.Add(sessionPart);
            }
            var sessionPartsForCommit = new BoxSessionParts() { Parts = allSessionParts };

            var uploadedFile = await UserClient.FilesManager.CommitSessionAsync(commitUri, Helper.GetSha1Hash(fileInMemoryStream), sessionPartsForCommit);

            // Assert file is committed/uploaded to box after commit
            Assert.IsTrue(await DoesFileExistInFolder(UserClient, FolderId, remoteFileName));

            await DeleteFile(uploadedFile.Id);
        }

        [TestMethod]
        public async Task UploadNewVersionOfBigFileInSession_ShouldUploadNewVersionOfFile_WhenFileAlreadyExists()
        {
            var file = await CreateSmallFile();

            long fileSize = 20000000;
            MemoryStream fileInMemoryStream = CreateFileInMemoryStream(fileSize);

            var response = await UserClient.FilesManager.UploadNewVersionUsingSessionAsync(fileInMemoryStream, file.Id);

            Assert.AreEqual(file.Id, response.Id);
            Assert.AreNotEqual(file.FileVersion.Id, response.FileVersion.Id);
        }

        [TestMethod]
        public async Task ViewVersions_ShouldReturnCorrectVersionNumber_WhenFileVersionIsChangedByUpload()
        {
            var file = await CreateSmallFile();
            await CreateNewFileVersion(file.Id);

            var response = await UserClient.FilesManager.ViewVersionsAsync(file.Id, new List<string>() { BoxFileVersion.FieldVersionNumber });

            Assert.AreEqual("1", response.Entries[0].VersionNumber);

            await CreateNewFileVersion(file.Id);

            response = await UserClient.FilesManager.ViewVersionsAsync(file.Id, new List<string>() { BoxFileVersion.FieldVersionNumber });

            Assert.AreEqual("2", response.Entries[0].VersionNumber);
        }

        [TestMethod]
        public async Task ViewVersions_ShouldReturnCorrectVersionNumber_WhenPaginationUsed()
        {
            var file = await CreateSmallFile();
            await CreateNewFileVersion(file.Id);
            await CreateNewFileVersion(file.Id);

            var response = await UserClient.FilesManager.ViewVersionsAsync(file.Id, new List<string>() { BoxFileVersion.FieldVersionNumber }, 1, 1);

            Assert.AreEqual(1, response.Entries.Count);
            Assert.AreEqual("1", response.Entries[0].VersionNumber);
        }

        [TestMethod]
        public async Task ViewVersions_ShouldReturnCorrectVersionNumber_WhenAutoPaginationUsed()
        {
            var file = await CreateSmallFile();
            await CreateNewFileVersion(file.Id);
            await CreateNewFileVersion(file.Id);

            var response = await UserClient.FilesManager.ViewVersionsAsync(file.Id, new List<string>() { BoxFileVersion.FieldVersionNumber }, 0, 1, true);

            Assert.AreEqual("2", response.Entries[0].VersionNumber);
            Assert.AreEqual("1", response.Entries[1].VersionNumber);
        }

        [TestMethod]
        public async Task AddSharedLink_ForValidNewFile_ShouldCreateNewSharedLink()
        {
            var file = await CreateSmallFile();

            var sharedLinkRequest = new BoxSharedLinkRequest()
            {
                VanityName = GetShortUniqueName("SharedLink"),
                Access = BoxSharedLinkAccessType.open,
                Permissions = new BoxPermissionsRequest
                {
                    Download = true,
                    Edit = true,
                }
            };

            var response = await UserClient.FilesManager.CreateSharedLinkAsync(file.Id, sharedLinkRequest);

            Assert.AreEqual(file.Id, response.Id);
            Assert.AreEqual(BoxSharedLinkAccessType.open, response.SharedLink.Access);
            Assert.IsTrue(response.SharedLink.Permissions.CanDownload);
            Assert.IsTrue(response.SharedLink.Permissions.CanEdit);
        }

        private int GetNumberOfParts(long totalSize, long partSize)
        {
            if (partSize == 0)
            {
                throw new Exception("Part Size cannot be 0");
            }

            var numberOfParts = Convert.ToInt32(totalSize / partSize);
            if (totalSize % partSize != 0)
            {
                numberOfParts++;
            }
            return numberOfParts;
        }

        private async Task UploadPartsInSessionAsync(Uri uploadPartsUri, int numberOfParts, long partSize, Stream stream,
            long fileSize)
        {
            for (var i = 0; i < numberOfParts; i++)
            {
                var partOffset = partSize * i;
                Stream partFileStream = GetFilePart(stream, partSize, partOffset);
                var sha = Helper.GetSha1Hash(partFileStream);
                partFileStream.Position = 0;
                await UserClient.FilesManager.UploadPartAsync(uploadPartsUri, sha, partOffset, fileSize, partFileStream);
            }
        }

        private Stream GetFilePart(Stream stream, long partSize, long partOffset)
        {
            const int BufferSize = 4096;

            var buffer = new byte[BufferSize];
            stream.Position = partOffset;
            var partStream = new MemoryStream();
            int bytesRead;
            do
            {
                bytesRead = stream.Read(buffer, 0, 4096);
                if (bytesRead > 0)
                {
                    long bytesToWrite = bytesRead;
                    var shouldBreak = false;
                    if (partStream.Length + bytesRead >= partSize)
                    {
                        bytesToWrite = partSize - partStream.Length;
                        shouldBreak = true;
                    }

                    partStream.Write(buffer, 0, Convert.ToInt32(bytesToWrite));

                    if (shouldBreak)
                    {
                        break;
                    }
                }
            } while (bytesRead > 0);

            return partStream;
        }
    }
}
