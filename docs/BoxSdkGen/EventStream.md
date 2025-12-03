# Event Stream

The Event Stream class utilizes long-polling to receive real-time events from Box. The SDK provides an easy way to set up and manage the event stream. The .NET SDK offers two implementations depending on your target framework:

- **.NET 6.0+**: Uses `IAsyncEnumerable` for async stream iteration
- **.NET Standard 2.0**: Uses callback-based API with `Action` delegates

Both implementations provide the same core functionality with automatic deduplication and retry logic.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Event Stream](#event-stream)
  - [Listening to the Event Stream](#listening-to-the-event-stream)
    - [Using Async Streams (.NET 6.0+)](#using-async-streams-net-60)
    - [Using Callbacks (.NET Standard 2.0)](#using-callbacks-net-standard-20)
  - [Stopping the Event Stream](#stopping-the-event-stream)
  - [Deduplication](#deduplication)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Listening to the Event Stream

### Using Async Streams (.NET 6.0+)

When the `EventStream` is started, it begins long-polling asynchronously. Events received from the API are then yielded to the caller through an `IAsyncEnumerable<Event>`.

```csharp
using Box.Sdk.Gen;

var stream = client.Events.GetEventStream();

await foreach (var eventItem in stream.StreamEventsAsync())
{
    Console.WriteLine($"Received event: ID={eventItem.EventId}, Type={eventItem.EventType}, CreatedAt={eventItem.CreatedAt}");
}
```

### Using Callbacks (.NET Standard 2.0)

For .NET Standard 2.0 compatibility, the SDK provides a callback-based API where you provide an `Action<Event>` delegate that gets invoked for each event received.

```csharp
using Box.Sdk.Gen;
using System.Threading;

var stream = client.Events.GetEventStream();
var cts = new CancellationTokenSource();

await stream.StartAsync(
    onEvent: (eventItem) =>
    {
        Console.WriteLine($"Received event: ID={eventItem.EventId}, Type={eventItem.EventType}, CreatedAt={eventItem.CreatedAt}");
    },
    onError: (exception) =>
    {
        Console.WriteLine($"Error occurred: {exception.Message}");
    },
    cancellationToken: cts.Token
);
```

## Stopping the Event Stream

To stop the event stream, use a `CancellationToken`:

**.NET 6.0+ (Async Streams):**

```csharp
var cts = new CancellationTokenSource();

// Cancel after 30 seconds
cts.CancelAfter(TimeSpan.FromSeconds(30));

await foreach (var eventItem in stream.StreamEventsAsync(cts.Token))
{
    Console.WriteLine($"Event: {eventItem.EventType}");
}
```

**.NET Standard 2.0 (Callbacks):**

```csharp
var cts = new CancellationTokenSource();

var streamTask = stream.StartAsync(
    onEvent: (eventItem) => Console.WriteLine($"Event: {eventItem.EventType}"),
    cancellationToken: cts.Token
);

// Stop the stream when needed
cts.Cancel();
await streamTask;
```

## Deduplication

The `EventStream` class automatically deduplicates events based on their `EventId`. This means that if the same event is received multiple times, it will only be emitted once to the listeners.
