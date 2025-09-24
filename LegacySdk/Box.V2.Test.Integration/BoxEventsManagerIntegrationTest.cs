using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Test.Integration.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxEventsManagerIntegrationTest : TestInFolder
    {
        //flaky test (sometimes too fast events fetch)
        [TestMethod]
        public async Task EnterpriseEventsStreamingAsync_ForNewFile_ShouldReturnUploadFileEvent()
        {
            var uploadedFile = await CreateSmallFile(FolderId);

            await Retry(async () =>
            {
                var events = await AdminClient.EventsManager.EnterpriseEventsStreamingAsync();
                BoxEnterpriseEvent uploadedFileEvent = null;
                while (events.ChunkSize == 500 || uploadedFileEvent == null)
                {
                    events = await AdminClient.EventsManager.EnterpriseEventsStreamingAsync(500, events.NextStreamPosition, new List<string>() { "UPLOAD" });
                    uploadedFileEvent = events.Entries.FirstOrDefault(x => x.Source?.Id == uploadedFile.Id);
                }
                Assert.IsNotNull(uploadedFileEvent);
                Assert.AreEqual("UPLOAD", uploadedFileEvent.EventType);
                Assert.AreEqual(uploadedFile.Id, uploadedFileEvent.Source.Id);
                Assert.AreEqual("file", uploadedFileEvent.Source.Type);
            });
        }
    }
}
