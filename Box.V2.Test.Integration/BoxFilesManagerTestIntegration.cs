using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

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
        public async Task GetInformation_ValidRequest_ValidFile()
        {
            var f = await _client.FilesManager.GetInformationAsync(FileId);
            Assert.AreEqual(FileId, f.Id);
        }
    }
}
