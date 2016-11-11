using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxEventsManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task EnterpriseEvents_LiveSession()
        {
            var startDate = DateTime.Now.Subtract(TimeSpan.FromDays(7));
            var endDate = DateTime.Now;

            var events = await _client.EventsManager.EnterpriseEventsAsync(createdAfter: startDate, createdBefore: endDate);
            Assert.IsNotNull(events, "Failed to retrieve enterprise events");
            Assert.IsTrue(events.Entries.Count > 0, "Failed to retrieve enterprise events");
        }

        [TestMethod]
        public async Task UserEvents_LiveSession()
        {
            const string fileId = "16894943599";

            var startDate = DateTime.Now.Subtract(TimeSpan.FromDays(7));
            var endDate = DateTime.Now;

            var events = await _client.EventsManager.UserEventsAsync();
            Assert.IsNotNull(events.NextStreamPosition, "Failed to retrieve user next_stream_position");

            var fileLock = await _client.FilesManager.LockAsync(new BoxFileLockRequest() { Lock = new BoxFileLock() { IsDownloadPrevented = false } }, fileId);
            var result = await _client.FilesManager.UnLock(fileId);

            BoxEventCollection<BoxEnterpriseEvent> newEvents = null;
            bool keepChecking = true;
            int maxTimesToCheck = 10;
            while (keepChecking && maxTimesToCheck > 0)
            {
                newEvents = await _client.EventsManager.UserEventsAsync(streamPosition: events.NextStreamPosition);
                if (newEvents.Entries.Count > 0)
                {
                    keepChecking = false;
                }
                else
                {
                    maxTimesToCheck--;
                    Thread.Sleep(1000);
                }
            }

            var commentEvent = newEvents.Entries.SingleOrDefault(e => e.EventType == "LOCK_CREATE");
            Assert.IsNotNull(commentEvent, "Failed to retrieve user events");
        }
    }
}
