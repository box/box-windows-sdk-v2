# Events

The event feed provides a way for an application to subscribe to any actions performed by any user, users, or service in an enterprise.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [User Events](#user-events)
  - [Get Events Manually](#deduplicating-events)
- [Enterprise (Admin) Events](#enterprise-admin-events)
  - [Historical Querying](#historical-querying)
  - [Live Monitoring](#live-monitoring)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## User Events

User events provides a low latency stream of events relevant to the currently authenticated user.

### Get Events Manually

The SDK provides an `BoxEventsManager` class and
`UserEventsAsync(int limit, UserEventsStreamType streamType, string streamPosition, bool dedupeEvents)`. By default, this
will fetch the first available events chronologically. You can pass a specific `stream_position` to get events from a
particular time. 

<!-- sample get_events -->
```c#
BoxEventCollection<BoxEnterpriseEvent> events = await client.EventsManager.UserEventsAsync();
```

If you want to progress within a stream you can use position parameter:
```c#
BoxEventCollection<BoxEnterpriseEvent> events = await client.EventsManager.UserEventsAsync(20);
string nextStreamPosition = events.NextStreamPosition;
// process revieved events
BoxEventCollection<BoxEnterpriseEvent> events2 = await client.EventsManager
    .UserEventsAsync(20, UserEventsStreamType.all, nextStreamPosition); // get events from the next position
// process revieved events
```

## Enterprise (Admin) Events

Enterprise events provide an event feed for all users and content in an enterprise Box instance.

### Historical Querying

The Box API provides an `BoxEventsManager` class and
`EnterpriseEventsAsync(int limit, string streamPosition, IEnumerable<string> eventTypes,  DateTimeOffset? createdAfter, DateTimeOffset? createdBefore = null)` method
that reads from the `admin-logs` stream and returns an `BoxEventCollection<BoxEnterpriseEvent>`. The emphasis for this stream is on completeness over latency,
which means that Box will deliver admin events in chronological order and without duplicates,
but with higher latency. You can specify start and end time/dates. This method
will only work with an API connection for an enterprise admin account or service account with a manage enterprise properties.

<!-- sample get_events enterprise -->
```c#
// get the last two hours of unfiltered enterprise events
var createdAfter = DateTimeOffset.UtcNow.AddHours(-2);
var createdBefore = DateTimeOffset.UtcNow;
BoxEventCollection<BoxEnterpriseEvent> events = await client.EventsManager.EnterpriseEventsAsync(500, null, null, createdAfter, createdBefore);
```

You can also filter events by type.

<!-- sample get_events enterprise_filter -->
```c#
// filter events by type
var eventTypestoFilter = new List<string>() { "UPLOAD" };
BoxEventCollection<BoxEnterpriseEvent> events = await client.EventsManager.EnterpriseEventsAsync(500, null, eventTypestoFilter);
```

If you want to progress within a stream you can use position parameter:
```c#
BoxEventCollection<BoxEnterpriseEvent> events = await client.EventsManager.EnterpriseEventsAsync(20);
string nextStreamPosition = events.NextStreamPosition;
// process revieved events
BoxEventCollection<BoxEnterpriseEvent> events2 = await client.EventsManager
    .EnterpriseEventsAsync(20, nextStreamPosition); // get events from the next position
// process revieved events
```

### Live Monitoring
To monitor recent events that have been generated within Box across the enterprise use
`EnterpriseEventsStreamingAsync(int limit, string streamPosition, IEnumerable<string> eventTypes)`,
method that reads from the `admin-logs-streaming` stream and returns an `BoxEventCollection<BoxEnterpriseEvent>`.
The emphasis for this feed is on low latency rather than chronological accuracy, which means that Box may return
events more than once and out of chronological order. Events are returned via the API around 12 seconds after they
are processed by Box (the 12 seconds buffer ensures that new events are not written after your cursor position).
Only two weeks of events are available via this feed, and you cannot set start and end time/dates. This method
will only work with an API connection for an enterprise admin account or service account with a manage enterprise properties.

<!-- sample get_events enterprise_stream -->
```c#
BoxEventCollection<BoxEnterpriseEvent> events = await client.EventsManager.EnterpriseEventsStreamingAsync();
```

You can limit number of events returned.
```c#
// get first 20 events
int limit = 20
BoxEventCollection<BoxEnterpriseEvent> events = await client.EventsManager.EnterpriseEventsStreamingAsync(limit);
```

<!-- sample get_events enterprise_stream_filter -->
You can also filter events by type.
```c#
// filter events by type
var eventTypestoFilter = new List<string>() { "UPLOAD" };
BoxEventCollection<BoxEnterpriseEvent> events = await client.EventsManager.EnterpriseEventsStreamingAsync(500, null, eventTypestoFilter);
```

If you want to progress within a stream you can use position parameter:
```c#
BoxEventCollection<BoxEnterpriseEvent> events = await client.EventsManager.EnterpriseEventsStreamingAsync(20);
string nextStreamPosition = events.NextStreamPosition;
// process revieved events
BoxEventCollection<BoxEnterpriseEvent> events2 = await client.EventsManager
    .EnterpriseEventsStreamingAsync(20, nextStreamPosition); // get events from the next position
// process revieved events
```
If you have the next stream position, and make a subsequent call, the API will return immediately
even when there are no events, the next stream position will be returned.
If you have a stream position that is older than two weeks than API will return no events and next
stream position.
