using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxEventsManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task EnterpriseEvents_LiveSession()
        {
            var startDate = Convert.ToDateTime("9/18/2017 11:51:12 PM");
            var endDate = Convert.ToDateTime("9/24/2017 11:51:12 PM");
            var expectedChunkSize = 107;

            var events = await Client.EventsManager.EnterpriseEventsAsync(createdAfter: startDate, createdBefore: endDate);
            var eventsMissingOneParam = await Client.EventsManager.EnterpriseEventsAsync(createdAfter: startDate, createdBefore: null);
            var eventsMissingBothParam = await Client.EventsManager.EnterpriseEventsAsync(createdAfter: null, createdBefore: null);

            Assert.IsNotNull(events, "Failed to retrieve enterprise events");
            Assert.AreEqual(events.ChunkSize, expectedChunkSize);
            Assert.IsTrue(events.Entries.Count > 0, "Failed to retrieve enterprise events");

            Assert.IsNotNull(eventsMissingOneParam, "Failed to retrieve enterprise events for missing createdBefore param");
            Assert.IsTrue(eventsMissingOneParam.Entries.Count > 0, "Failed to retrieve enterprise events with missing createdBefore param");

            Assert.IsNotNull(eventsMissingBothParam, "Failed to retrieve enterprise events for missing all params");
            Assert.IsTrue(eventsMissingBothParam.Entries.Count > 0, "Failed to retrieve enterprise events with missing all params");
        }

        [TestMethod]
        public async Task UserEvents_LiveSession()
        {
            const string FileId = "16894943599";

            var events = await Client.EventsManager.UserEventsAsync();
            Assert.IsNotNull(events.NextStreamPosition, "Failed to retrieve user next_stream_position");

            var fileLock = await Client.FilesManager.LockAsync(new BoxFileLockRequest() { Lock = new BoxFileLock() { IsDownloadPrevented = false } }, FileId);
            var result = await Client.FilesManager.UnLock(FileId);

            BoxEventCollection<BoxEnterpriseEvent> newEvents = null;
            var keepChecking = true;
            var maxTimesToCheck = 10;
            while (keepChecking && maxTimesToCheck > 0)
            {
                newEvents = await Client.EventsManager.UserEventsAsync(streamPosition: events.NextStreamPosition);
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

        [TestMethod]
        public async Task LongPollUserEvents_LiveSession()
        {
            const string FileId = "16894943599";

            var incomingEvents = new ConcurrentBag<BoxEnterpriseEvent>();

            //first we need to get the latest stream position
            var events = await Client.EventsManager.UserEventsAsync();

            var cancelSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            var t = await Task.Factory.StartNew(async (opts) =>
            {
                await Client.EventsManager.LongPollUserEvents(events.NextStreamPosition,
                    (newEvents) =>
                    {
                        //do something with incoming new events
                        //Debug.WriteLine("Received {0} new event(s)", newEvents.Entries.Count);
                        newEvents.Entries.ForEach(e => incomingEvents.Add(e));
                    }, cancelSource.Token, retryTimeoutOverride: 5);
            },
            TaskCreationOptions.LongRunning,
            cancelSource.Token);

            var tasks = new ConcurrentBag<Task>
            {
                t
            };

            Thread.Sleep(1000);

            //make some events 
            var fileLock = await Client.FilesManager.LockAsync(new BoxFileLockRequest() { Lock = new BoxFileLock() { IsDownloadPrevented = false } }, FileId);
            var result = await Client.FilesManager.UnLock(FileId);

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException e)
            {
                //Console.WriteLine("\nAggregateException thrown with the following inner exceptions:"); 
                foreach (var v in e.InnerExceptions)
                {
                    if (v is TaskCanceledException)
                    {
                        //Console.WriteLine("   TaskCanceledException: Task {0}", ((TaskCanceledException)v).Task.Id);
                        var num = incomingEvents.Count;
                        Assert.IsTrue(num >= 2, "Failed to get correct event count using long polling.");
                    }
                    else
                    {
                        //Console.WriteLine("   Exception: {0}", v.GetType().Name);
                        Assert.Fail("Failed to get events using long polling.");
                    }
                }
            }
            finally
            {
                cancelSource.Dispose();
            }
        }
    }
}
