using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
