using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxFilesManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        private const string FileId = "7546361455";

        [TestMethod]
        public async Task Download_ValidRequest_ValidStream()
        {
            var test = await _client.FilesManager.DownloadBytesAsync(FileId);
        }

        [TestMethod]
        public async Task BatchDownload_ValidRequest_ValidResponse()
        {
            /*** Arrange ***/
            List<Task<byte[]>> tasks = new List<Task<byte[]>>();

            int size = 1420;
            int numTasks = 50;

            /*** Act ***/
            for (int i = 0; i < numTasks; ++i)
                 tasks.Add(_client.FilesManager.DownloadBytesAsync(FileId));

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
    }
}
