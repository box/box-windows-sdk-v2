using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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

        [TestMethod]
        public async Task LongPollUserEvents_LiveSession()
        {
            const string fileId = "16894943599";

            ConcurrentBag<BoxEnterpriseEvent> incomingEvents = new ConcurrentBag<BoxEnterpriseEvent>();

            //first we need to get the latest stream position
            var events = await _client.EventsManager.UserEventsAsync();

            CancellationTokenSource cancelSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            var t = await Task.Factory.StartNew(async (opts) =>
            {
                await _client.EventsManager.LongPollUserEvents(events.NextStreamPosition,
                    (newEvents) =>
                    {
                        //do something with incoming new events
                        //Debug.WriteLine("Received {0} new event(s)", newEvents.Entries.Count);
                        newEvents.Entries.ForEach(e => incomingEvents.Add(e));
                    }, cancelSource.Token, retryTimeoutOverride: 5);
            },
            TaskCreationOptions.LongRunning,
            cancelSource.Token);

            var tasks = new ConcurrentBag<Task>();
            tasks.Add(t);

            Thread.Sleep(1000);

            //make some events 
            var fileLock = await _client.FilesManager.LockAsync(new BoxFileLockRequest() { Lock = new BoxFileLock() { IsDownloadPrevented = false } }, fileId);
            var result = await _client.FilesManager.UnLock(fileId);

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
