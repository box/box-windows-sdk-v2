using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class EventsManagerTests {
        public BoxClient client { get; }

        public EventsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestEvents() {
            Events events = await client.Events.GetEventsAsync();
            Assert.IsTrue(NullableUtils.Unwrap(events.Entries).Count > 0);
            Event firstEvent = NullableUtils.Unwrap(events.Entries)[0];
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(firstEvent.CreatedBy).Type) == "user");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(firstEvent.EventType)) != "");
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestEventUpload() {
            Events events = await client.Events.GetEventsAsync(queryParams: new GetEventsQueryParams() { StreamType = GetEventsQueryParamsStreamTypeField.AdminLogs, EventType = Array.AsReadOnly(new [] {new StringEnum<GetEventsQueryParamsEventTypeField>(GetEventsQueryParamsEventTypeField.Upload)}) });
            Assert.IsTrue(NullableUtils.Unwrap(events.Entries).Count > 0);
            Event firstEvent = NullableUtils.Unwrap(events.Entries)[0];
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(firstEvent.EventType)) == "UPLOAD");
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestEventDeleteUser() {
            Events events = await client.Events.GetEventsAsync(queryParams: new GetEventsQueryParams() { StreamType = GetEventsQueryParamsStreamTypeField.AdminLogs, EventType = Array.AsReadOnly(new [] {new StringEnum<GetEventsQueryParamsEventTypeField>(GetEventsQueryParamsEventTypeField.DeleteUser)}) });
            Assert.IsTrue(NullableUtils.Unwrap(events.Entries).Count > 0);
            Event firstEvent = NullableUtils.Unwrap(events.Entries)[0];
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(firstEvent.EventType)) == "DELETE_USER");
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestEventSourceFileOrFolder() {
            Events events = await client.Events.GetEventsAsync(queryParams: new GetEventsQueryParams() { StreamType = GetEventsQueryParamsStreamTypeField.Changes });
            Assert.IsTrue(NullableUtils.Unwrap(events.Entries).Count > 0);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetEventsWithLongPolling() {
            RealtimeServers servers = await client.Events.GetEventsWithLongPollingAsync();
            Assert.IsTrue(NullableUtils.Unwrap(servers.Entries).Count > 0);
            RealtimeServer server = NullableUtils.Unwrap(servers.Entries)[0];
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(server.Type)) == "realtime_server");
            Assert.IsTrue(NullableUtils.Unwrap(server.Url) != "");
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetEventsWithDateFilters() {
            long currentEpochTimeInSeconds = Utils.GetEpochTimeInSeconds();
            long epochTimeInSecondsAWeekAgo = currentEpochTimeInSeconds - 7 * 24 * 60 * 60;
            System.DateTimeOffset createdAfterDate = Utils.EpochSecondsToDateTime(seconds: epochTimeInSecondsAWeekAgo);
            System.DateTimeOffset createdBeforeDate = Utils.EpochSecondsToDateTime(seconds: currentEpochTimeInSeconds);
            Events servers = await client.Events.GetEventsAsync(queryParams: new GetEventsQueryParams() { StreamType = GetEventsQueryParamsStreamTypeField.AdminLogs, Limit = 1, CreatedAfter = createdAfterDate, CreatedBefore = createdBeforeDate });
            Assert.IsTrue(NullableUtils.Unwrap(servers.Entries).Count == 1);
        }

    }
}