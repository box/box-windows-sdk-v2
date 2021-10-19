using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.Models;
using Box.V2.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public async Task RestoreFile_Valid_Response()
        {
            const string FileId = "238288183114";
            var fileRequest = new BoxFileRequest()
            {
                Id = FileId
            };
            _ = await Client.FilesManager.RestoreTrashedAsync(fileRequest);
        }

        [TestMethod]
        public async Task GetInformation_Fields_ValidResponse()
        {
            const string FileId = "16894947279";
            var file = await Client.FilesManager.GetInformationAsync(FileId, new List<string> { BoxFile.FieldName, BoxFile.FieldModifiedAt, BoxFile.FieldOwnedBy });

            Assert.AreEqual(FileId, file.Id, "Incorrect file id");
            Assert.IsNotNull(file.Name, "File Name is null");
            Assert.IsNotNull(file.ModifiedAt, "ModifiedAt field is null");
            Assert.IsNotNull(file.OwnedBy, "OwnedBy field is null");
        }

        [TestMethod]
        public async Task GetInformation_Fields_Metadata_ValidResponse()
        {
            const string FileId = "97274928659";
            var file = await Client.FilesManager.GetInformationAsync(FileId, fields: new List<string> { "metadata.enterprise_440385.testtemplate" });

            Assert.AreEqual(FileId, file.Id, "Incorrect file id");
            Assert.IsNotNull(file.Metadata, "Metadata is null");
            Assert.IsNotNull(file.Metadata["enterprise_440385"], "Scope could not be found");

            file = await Client.FilesManager.GetInformationAsync(FileId, fields: new List<string> { "metadata.enterprise.testtemplate" });

            Assert.AreEqual(FileId, file.Id, "Incorrect file id");
            Assert.IsNotNull(file.Metadata, "Metadata is null");
            Assert.IsNotNull(file.Metadata["enterprise"], "Scope could not be found");
        }

        [TestMethod]
        [TestCategory("CI-APP-USER")]
        [ExpectedException(typeof(TimeoutException))]
        public async Task Download_ValidRequest_Timeout()
        {
            var timeout = new TimeSpan(0, 0, 0, 0, 1); // 1ms timeout, should always cancel the request
            const string FileId = "16894947279";
            _ = await Client.FilesManager.DownloadStreamAsync(FileId, timeout: timeout);
        }

        [TestMethod]
        public async Task Download_ValidRequest_ValidStream()
        {
            const string FileId = "16894947279";
            var responseStream = await Client.FilesManager.DownloadStreamAsync(FileId);
            Assert.IsNotNull(responseStream, "Response stream is null");
        }

        [TestMethod]
        public async Task Download_ValidRequest_ValidStreamWithRange()
        {
            const string FileId = "16894947279";
            var responseStream = await Client.FilesManager.DownloadStreamAsync(FileId, null, null, 10, 20);
            Assert.IsNotNull(responseStream, "Response stream is null");

            using (var reader = new StreamReader(responseStream))
            {
                var value = reader.ReadToEnd();

                // Make sure it's the parts range
                Assert.AreEqual(11, value.Length);
            }
        }

        [TestMethod]
        public async Task GetThumbnail_ValidRequest_ValidThumbnail()
        {
            const string FileId = "102438629524";

            using (Stream stream = await Client.FilesManager.GetThumbnailAsync(FileId, 256, 256))
            using (var fs = new FileStream(string.Format(GetSaveFolderPath(), "thumb.png"), FileMode.OpenOrCreate))
            {
                Assert.IsNotNull(stream, "Stream is Null");

                await stream.CopyToAsync(fs);

                Assert.IsNotNull(fs, "FileStream is null");
            }
        }

        [TestMethod]
        public async Task GetSharedLink_ValidRequest_ValidSharedLink()
        {
            var imageFileId1 = "16894947279";

            var linkReq = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open
            };

            BoxFile fileLink = await Client.FilesManager.CreateSharedLinkAsync(imageFileId1, linkReq);
            Assert.AreEqual(BoxSharedLinkAccessType.open, fileLink.SharedLink.Access);
        }

        [TestMethod]
        public async Task GetRepresentations_ValidRequest_ValidRepresentation()
        {
            var representationsMissingHeader = await Client.FilesManager.GetRepresentationsAsync(new BoxRepresentationRequest()
            {
                FileId = "16894927933",
            });

            var representationsAllHeaders = await Client.FilesManager.GetRepresentationsAsync(new BoxRepresentationRequest()
            {
                FileId = "16894927933",
                XRepHints = Constants.RepresentationTypes.Pdf,
                SetContentDispositionFilename = "New Name",
                SetContentDispositionType = Constants.ContentDispositionTypes.Inline,
                HandleRetry = true
            });

            var representationsMultipleXRepHints = await Client.FilesManager.GetRepresentationsAsync(new BoxRepresentationRequest()
            {
                FileId = "16894927933",
                XRepHints = Constants.RepresentationTypes.ImageMedium
            });

            Assert.IsNotNull(representationsMissingHeader.Entries, "Failed to generate a representation for file");
            Assert.AreEqual("pdf", representationsAllHeaders.Entries[0].Representation);
            Assert.IsNotNull(representationsMultipleXRepHints.Entries[1], "Failed to generate second representation for file");
        }

        [TestMethod]
        public async Task Watermark_Files_CRUD()
        {
            const string FileId = "16894944949";

            var mylist = new List<string>(new string[] { "watermark_info" });
            var file = await Client.FilesManager.GetInformationAsync(FileId, mylist);
            Assert.IsFalse(file.WatermarkInfo.IsWatermarked);

            var watermark = await Client.FilesManager.ApplyWatermarkAsync(FileId);
            Assert.IsNotNull(watermark, "Failed to apply watermark to file");

            file = await Client.FilesManager.GetInformationAsync(FileId, mylist);
            Assert.IsTrue(file.WatermarkInfo.IsWatermarked);

            var fetchedWatermark = await Client.FilesManager.GetWatermarkAsync(FileId);
            Assert.IsNotNull(fetchedWatermark, "Failed to fetch watermark of file");

            var result = await Client.FilesManager.RemoveWatermarkAsync(FileId);
            Assert.IsTrue(result, "Failed to remove watermark from file");

        }

        [TestMethod]
        public async Task FileWorkflow_ValidRequest_ValidResponse()
        {
            //file Ids of the two files to download
            var imageFileId1 = "16894947279";
            var imageFileId2 = "16894946307";

            // paths to store the two downloaded files
            var dlPath1 = string.Format(GetSaveFolderPath(), "thumbnail1.png");
            var dlPath2 = string.Format(GetSaveFolderPath(), "thumbnail2.png");

            //download 2 files
            using (var fs = new FileStream(dlPath1, FileMode.OpenOrCreate))
            {
                Stream stream = await Client.FilesManager.DownloadStreamAsync(imageFileId1);
                await stream.CopyToAsync(fs);
            }

            using (var fs = new FileStream(dlPath2, FileMode.OpenOrCreate))
            {
                Stream stream = await Client.FilesManager.DownloadStreamAsync(imageFileId2);
                await stream.CopyToAsync(fs);
            }

            // File name to use to upload files
            var uploadFileName = "testUpload.png";

            // Upload file at dlPath1
            BoxFile file;
            using (var fs = new FileStream(dlPath1, FileMode.Open))
            {
                var req = new BoxFileRequest()
                {
                    Name = uploadFileName,
                    Parent = new BoxRequestEntity() { Id = "0" }
                };

                file = await Client.FilesManager.UploadAsync(req, fs);
            }

            Assert.AreEqual(uploadFileName, file.Name, "Incorrect file name");
            Assert.AreEqual("0", file.Parent.Id, "Incorrect destination folder");

            // Upload file at dlPath2 as new version of 'file'
            BoxFile newFileVersion;
            using (var fs = new FileStream(dlPath2, FileMode.Open))
            {
                newFileVersion = await Client.FilesManager.UploadNewVersionAsync(uploadFileName, file.Id, fs, file.ETag);
            }

            Assert.AreEqual(newFileVersion.Name, uploadFileName);
            Assert.AreEqual(newFileVersion.Parent.Id, "0");

            //View file versions (Requires upgraded acct)
            BoxCollection<BoxFileVersion> fileVersions = await Client.FilesManager.ViewVersionsAsync(newFileVersion.Id);

            Assert.AreEqual(fileVersions.TotalCount, 1, "Incorrect number of versions");

            foreach (var f in fileVersions.Entries)
            {
                Assert.AreEqual(uploadFileName, f.Name);
            }

            // Update the file name of a file
            var updateName = GetUniqueName();

            var updateReq = new BoxFileRequest()
            {
                Id = file.Id,
                Name = updateName
            };
            BoxFile fileUpdate = await Client.FilesManager.UpdateInformationAsync(updateReq);

            Assert.AreEqual(file.Id, fileUpdate.Id, "File Ids are not the same");
            Assert.AreEqual(updateName, fileUpdate.Name, "File Names are not the same");

            // Test create shared link
            var linkReq = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open
            };
            BoxFile fileWithLink = await Client.FilesManager.CreateSharedLinkAsync(newFileVersion.Id, linkReq);

            Assert.AreEqual(BoxSharedLinkAccessType.open, fileWithLink.SharedLink.Access, "Incorrect access for shared link");

            // Copy file in same folder
            var copyName = GetUniqueName();
            var copyReq = new BoxFileRequest()
            {
                Id = fileWithLink.Id,
                Name = copyName,
                Parent = new BoxRequestEntity() { Id = "0" }
            };
            BoxFile fileCopy = await Client.FilesManager.CopyAsync(copyReq);

            Assert.AreEqual(fileCopy.Name, copyName, "Incorrect Name for copied file");
            Assert.AreEqual(fileCopy.Parent.Id, "0", "Incorrect parent folder for copied file");

            // Test get file information
            _ = await Client.FilesManager.GetInformationAsync(fileWithLink.Id);

            Assert.AreEqual(updateName, fileWithLink.Name, "File name is incorrect");
            Assert.AreEqual("0", fileWithLink.Parent.Id, "Parent folder is incorrect");

            // Delete both files
            await Client.FilesManager.DeleteAsync(fileCopy.Id, fileCopy.ETag);
            await Client.FilesManager.DeleteAsync(fileWithLink.Id, fileWithLink.ETag);
        }

        [TestMethod]
        public async Task BatchGetInformation_ValidRequest_ValidResponse()
        {
            const string FileId = "16894947279";

            /*** Arrange ***/
            var tasks = new List<Task<BoxFile>>();

            var size = 574732;
            var numTasks = 5;

            /*** Act ***/
            for (var i = 0; i < numTasks; ++i)
            {
                tasks.Add(Client.FilesManager.GetInformationAsync(FileId));
            }

            await Task.WhenAll(tasks);

            /*** Assert ***/
            foreach (var t in tasks)
            {
                Assert.AreEqual(size, (await t).Size);
            }
        }

        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public void GetNumberOfParts_Utility_Function_CorrectPartNumber()
        {
            // This file size is expected to divide evenly with the partSize
            long fileSize = 209717000;
            // This file size is expected to have a small remainder after dividing with partSize
            long divisibleFileSize = 209715200;
            long partSize = 8388608;

            var numberOfPartsNoRemainder = UploadUsingSessionInternal.GetNumberOfParts(divisibleFileSize, partSize);
            var numberOfPartsWithRemainder = UploadUsingSessionInternal.GetNumberOfParts(fileSize, partSize);

            Assert.AreEqual(numberOfPartsNoRemainder, 25);
            Assert.AreEqual(numberOfPartsWithRemainder, 26);
        }

        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public async Task UploadFileInSession_AbortRequest_FileNotCommmited()
        {
            long fileSize = 50000000;
            MemoryStream fileInMemoryStream = GetBigFileInMemoryStream(fileSize);
            var remoteFileName = "UploadedUsingSession-" + DateTimeOffset.Now.TimeOfDay;
            var parentFolderId = "0";

            var boxFileUploadSessionRequest = new BoxFileUploadSessionRequest()
            {
                FolderId = parentFolderId,
                FileName = remoteFileName,
                FileSize = fileSize
            };
            // Create Upload Session
            BoxFileUploadSession boxFileUploadSession = await Client.FilesManager.CreateUploadSessionAsync(boxFileUploadSessionRequest);
            BoxSessionEndpoint boxSessionEndpoint = boxFileUploadSession.SessionEndpoints;
            var abortUri = new Uri(boxSessionEndpoint.Abort);
            var uploadPartUri = new Uri(boxSessionEndpoint.UploadPart);
            var partSize = boxFileUploadSession.PartSize;
            long.TryParse(partSize, out var partSizeLong);
            var numberOfParts = UploadUsingSessionInternal.GetNumberOfParts(fileSize, partSizeLong);

            // Upload parts in the session
            await UploadPartsInSessionAsync(uploadPartUri, numberOfParts, partSizeLong, fileInMemoryStream, fileSize);

            // Assert file is not committed/uploaded to box yet
            Assert.IsFalse(await DoesFileExistInFolder(parentFolderId, remoteFileName));

            // Abort
            await Client.FilesManager.DeleteUploadSessionAsync(abortUri);

            // Assert file is not committed/uploaded to box
            Assert.IsFalse(await DoesFileExistInFolder(parentFolderId, remoteFileName));
        }

        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public async Task UploadFileInSession_CommitSession_FilePresent()
        {
            long fileSize = 50000000;
            MemoryStream fileInMemoryStream = GetBigFileInMemoryStream(fileSize);
            var remoteFileName = "UploadedUsingSession-" + DateTimeOffset.Now.TimeOfDay;
            var parentFolderId = "0";

            var boxFileUploadSessionRequest = new BoxFileUploadSessionRequest()
            {
                FolderId = parentFolderId,
                FileName = remoteFileName,
                FileSize = fileSize
            };
            // Create Upload Session
            BoxFileUploadSession boxFileUploadSession = await Client.FilesManager.CreateUploadSessionAsync(boxFileUploadSessionRequest);
            BoxSessionEndpoint boxSessionEndpoint = boxFileUploadSession.SessionEndpoints;
            var listPartsUri = new Uri(boxSessionEndpoint.ListParts);
            var uploadPartUri = new Uri(boxSessionEndpoint.UploadPart);
            var commitUri = new Uri(boxSessionEndpoint.Commit);
            var partSize = boxFileUploadSession.PartSize;
            long.TryParse(partSize, out var partSizeLong);
            var numberOfParts = UploadUsingSessionInternal.GetNumberOfParts(fileSize, partSizeLong);

            // Upload parts in the session
            await UploadPartsInSessionAsync(uploadPartUri, numberOfParts, partSizeLong, fileInMemoryStream, fileSize);

            // Assert file is not committed/uploaded to box yet
            Assert.IsFalse(await DoesFileExistInFolder(parentFolderId, remoteFileName));

            // Get upload parts (1 by 1) for Integration testing purposes
            var allSessionParts = new List<BoxSessionPartInfo>();

            // var boxSessionParts = await _client.FilesManager.GetSessionUploadedPartsAsync(listPartsUri, 0, 2, true);
            var boxSessionParts = await Client.FilesManager.GetSessionUploadedPartsAsync(listPartsUri, null, null, true);

            foreach (var sessionPart in boxSessionParts.Entries)
            {
                allSessionParts.Add(sessionPart);
            }

            /* w/o autopaging
            var boxSessionParts = await _client.FilesManager.GetSessionUploadedPartsAsync(listPartsUri, 0, 1);
            allSessionParts.AddRange(boxSessionParts.Entries);

            while (allSessionParts.Count < boxSessionParts.TotalCount)
            {
                boxSessionParts = await _client.FilesManager.GetSessionUploadedPartsAsync(listPartsUri, allSessionParts.Count, 1);
                allSessionParts.AddRange(boxSessionParts.Entries);
            }
            */

            var sessionPartsForCommit = new BoxSessionParts() { Parts = allSessionParts };

            // Commit
            await Client.FilesManager.CommitSessionAsync(commitUri, Helper.GetSha1Hash(fileInMemoryStream), sessionPartsForCommit);

            // Assert file is committed/uploaded to box
            Assert.IsTrue(await DoesFileExistInFolder(parentFolderId, remoteFileName));

            // Delete file
            var fileId = await GetFileId(parentFolderId, remoteFileName);
            if (!string.IsNullOrWhiteSpace(fileId))
            {
                await Client.FilesManager.DeleteAsync(fileId);
            }

            // Assert file has been deleted from Box
            Assert.IsFalse(await DoesFileExistInFolder(parentFolderId, remoteFileName));
        }

        [TestMethod]
        public async Task UploadFileInSession_Utility_Function_FilePresent()
        {
            long fileSize = 50000000;
            MemoryStream fileInMemoryStream = GetBigFileInMemoryStream(fileSize);

            var remoteFileName = "UploadedUsingSession-" + DateTimeOffset.Now.TimeOfDay;
            var newRemoteFileName = "UploadNewVersionUsingSession-" + DateTimeOffset.Now.TimeOfDay;
            var parentFolderId = "0";

            var progressReported = false;

            var progress = new Progress<BoxProgress>(val =>
            {
                Debug.WriteLine("{0}%", val.progress);
                progressReported = true;
            });

            // Call Utility function
            await Client.FilesManager.UploadUsingSessionAsync(fileInMemoryStream, remoteFileName, parentFolderId,
                null, progress);

            // Assert file is committed/uploaded to box
            Assert.IsTrue(await DoesFileExistInFolder(parentFolderId, remoteFileName));
            var fileId = await GetFileId(parentFolderId, remoteFileName);

            // Using previously uploaded Box file, upload a new file version for that Box file
            var newBoxFile = await Client.FilesManager.UploadNewVersionUsingSessionAsync(fileInMemoryStream, fileId, newRemoteFileName,
                null, progress);

            Assert.IsNotNull(newBoxFile.FileVersion, "Did not successfully upload a new Box file version");
            Assert.IsTrue(progressReported);

            // Delete file
            if (!string.IsNullOrWhiteSpace(fileId))
            {
                await Client.FilesManager.DeleteAsync(fileId);
            }

            // Assert file has been deleted from Box
            Assert.IsFalse(await DoesFileExistInFolder(parentFolderId, remoteFileName));
        }

        [TestMethod]
        public async Task GetRepresentationContentAsync_E2E()
        {

            // Create stream from string content
            var assembly = Assembly.GetExecutingAssembly();
            _ = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            using (var fileStream = assembly.GetManifestResourceStream("Box.V2.Test.Integration.Properties.smalltestpdf.resources"))
            {
                var fileRequest = new BoxFileRequest();
                var parentFolder = new BoxRequestEntity
                {
                    Type = BoxType.folder,
                    Id = "0"
                };
                fileRequest.Parent = parentFolder;
                fileRequest.Name = DateTimeOffset.Now.Ticks + ".pdf";
                var file = await Client.FilesManager.UploadAsync(fileRequest, fileStream);

                var repRequest = new BoxRepresentationRequest
                {
                    FileId = file.Id,
                    XRepHints = "[png?dimensions=1024x1024]"
                };
                Stream assetStream = await Client.FilesManager.GetRepresentationContentAsync(repRequest, "1.png");

                // Delete the file when done
                await Client.FilesManager.DeleteAsync(file.Id);

                var memStream = new MemoryStream();
                await assetStream.CopyToAsync(memStream);
                var assetBytes = memStream.ToArray();

                Assert.IsTrue(assetBytes.Length > 4096, "Downlaoded asset contained " + assetBytes.Length + " but should contain more than 4 KB");
            }
        }

        #region Private functions

        private string GetSaveFolderPath()
        {
            var pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(pathUser, "Downloads") + "\\{0}";
        }

        private MemoryStream GetBigFileInMemoryStream(long fileSize)
        {
            // Create random data to write to the file.
            var dataArray = new byte[fileSize];
            new Random().NextBytes(dataArray);
            var memoryStream = new MemoryStream(dataArray);
            return memoryStream;
        }

        private async Task UploadPartsInSessionAsync(Uri uploadPartsUri, int numberOfParts, long partSize, Stream stream,
            long fileSize)
        {
            for (var i = 0; i < numberOfParts; i++)
            {
                // Split file as per part size
                var partOffset = partSize * i;
                Stream partFileStream = UploadUsingSessionInternal.GetFilePart(stream, partSize, partOffset);
                var sha = Helper.GetSha1Hash(partFileStream);
                partFileStream.Position = 0;
                await Client.FilesManager.UploadPartAsync(uploadPartsUri, sha, partOffset, fileSize, partFileStream);
            }
        }

        private async Task<bool> DoesFileExistInFolder(string folderId, string fileName)
        {
            // TODO: Paging
            BoxCollection<BoxItem> boxCollection = await Client.FoldersManager.GetFolderItemsAsync(folderId, 1000);
            return boxCollection.Entries.Any(item => item.Name == fileName);
        }

        private async Task<string> GetFileId(string folderId, string fileName)
        {
            // TODO: Paging
            BoxCollection<BoxItem> boxCollection = await Client.FoldersManager.GetFolderItemsAsync(folderId, 1000);
            return boxCollection.Entries.FirstOrDefault(item => item.Name == fileName)?.Id;
        }
        #endregion

        internal static class UploadUsingSessionInternal
        {
            public static int GetNumberOfParts(long totalSize, long partSize)
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

            public static Stream GetFilePart(Stream stream, long partSize, long partOffset)
            {
                // Default the buffer size to 4K.
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
}
