using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxFilesManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task Download_ValidRequest_ValidStream()
        {
            var test = await _client.FilesManager.DownloadAsync("7546361455");
        }
    }
}
