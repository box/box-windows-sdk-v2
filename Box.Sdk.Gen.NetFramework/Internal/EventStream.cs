using Box.Sdk.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Internal
{
  /// <summary>
  /// EventStream fetches events from the Box API using long polling to receive real-time updates.
  /// This class provides a callback-based API for streaming events.
  /// 
  /// Example usage:
  /// <code>
  /// var stream = eventsManager.GetEventStream();
  /// var cts = new CancellationTokenSource();
  /// await stream.StartAsync(
  ///     onEvent: (eventItem) => Console.WriteLine($"Event: {eventItem.EventType}"),
  ///     onError: (ex) => Console.WriteLine($"Error: {ex.Message}"),
  ///     cancellationToken: cts.Token
  /// );
  /// </code>
  /// </summary>
  public class EventStream
  {
    private readonly EventsManager _eventsManager;
    private readonly GetEventStreamQueryParams _queryParams;
    private string _streamPosition;
    private RealtimeServer _longPollingInfo;
    private int _longPollingRetries = 0;
    private readonly int _deduplicationFilterSize = 1000;
    private readonly Dictionary<string, bool> _dedupHash = new Dictionary<string, bool>();

    /// <summary>
    /// The headers to include in the request.
    /// </summary>
    public GetEventStreamHeaders HeadersInput { get; set; }

    /// <summary>
    /// Initializes a new instance of the EventStream class.
    /// </summary>
    /// <param name="eventsManager">The EventsManager instance which provides relevant methods to fetch events.</param>
    /// <param name="queryParams">The query parameters to use for fetching events.</param>
    public EventStream(EventsManager eventsManager, GetEventStreamQueryParams queryParams)
    {
      _eventsManager = eventsManager ?? throw new ArgumentNullException(nameof(eventsManager));
      _queryParams = queryParams ?? throw new ArgumentNullException(nameof(queryParams));
      _streamPosition = queryParams.StreamPosition ?? "now";
      HeadersInput = new GetEventStreamHeaders();
    }

    /// <summary>
    /// Starts streaming events asynchronously from the Box API using long polling.
    /// This method will continue to invoke the callback with events until the cancellation token is triggered.
    /// </summary>
    /// <param name="onEvent">Callback invoked for each event received.</param>
    /// <param name="onError">Optional callback invoked when an error occurs.</param>
    /// <param name="cancellationToken">Token used for request cancellation.</param>
    /// <returns>A task that completes when the stream is stopped.</returns>
    public async System.Threading.Tasks.Task StartAsync(
        Action<Event> onEvent,
        Action<Exception> onError = null,
        CancellationToken cancellationToken = default)
    {

      if (onEvent == null)
      {
        throw new ArgumentNullException(nameof(onEvent));
      }

      // Fetch and invoke callback with current events before starting long polling
      try
      {
        var initialEvents = await FetchEventsAsync(cancellationToken).ConfigureAwait(false);
        foreach (var eventItem in initialEvents)
        {
          onEvent(eventItem);
        }
      }
      catch (Exception ex)
      {
        if (onError != null)
        {
          onError(ex);
        }
      }

      while (!cancellationToken.IsCancellationRequested)
      {
        try
        {
          // Get long polling info if we don't have it yet or need to refresh
          if (_longPollingInfo == null)
          {
            await GetLongPollInfoAsync(cancellationToken).ConfigureAwait(false);
            if (cancellationToken.IsCancellationRequested)
            {
              break;
            }
          }

          // Perform long poll
          var shouldFetchEvents = await DoLongPollAsync(cancellationToken).ConfigureAwait(false);
          if (cancellationToken.IsCancellationRequested)
          {
            break;
          }

          // If long poll indicates new changes, fetch and invoke callback with events
          if (shouldFetchEvents)
          {
            var newEvents = await FetchEventsAsync(cancellationToken).ConfigureAwait(false);
            foreach (var eventItem in newEvents)
            {
              onEvent(eventItem);
            }
          }
        }
        catch (OperationCanceledException)
        {
          break;
        }
        catch (Exception ex)
        {
          if (onError != null)
          {
            onError(ex);
          }
          // Wait before retrying
          await System.Threading.Tasks.Task.Delay(1000, cancellationToken).ConfigureAwait(false);
        }
      }
    }

    /// <summary>
    /// Gets the long polling information from the Box API.
    /// </summary>
    private async System.Threading.Tasks.Task GetLongPollInfoAsync(CancellationToken cancellationToken)
    {
      var info = await _eventsManager.GetEventsWithLongPollingAsync(
          headers: null,
          cancellationToken: cancellationToken
      ).ConfigureAwait(false);

      var server = info.Entries?.FirstOrDefault(entry => entry.Type == "realtime_server");
      if (server == null)
      {
        throw new BoxSdkException("No realtime server found in the response.");
      }

      _longPollingInfo = server;
      _longPollingRetries = 0;
    }

    /// <summary>
    /// Performs a long poll request to the realtime server.
    /// Returns true if events should be fetched, false otherwise.
    /// </summary>
    private async System.Threading.Tasks.Task<bool> DoLongPollAsync(CancellationToken cancellationToken)
    {
      // Check if we need to refresh long polling info
      var maxRetries = int.TryParse(_longPollingInfo?.MaxRetries ?? "10", out var parsed) ? parsed : 10;
      if (_longPollingInfo == null || _longPollingRetries > maxRetries)
      {
        await GetLongPollInfoAsync(cancellationToken).ConfigureAwait(false);
        return false;
      }

      _longPollingRetries++;

      var longPollUrl = _longPollingInfo?.Url;
      var separator = longPollUrl?.Contains("?") == true ? "&" : "?";
      var longPollWithStreamPosition = $"{longPollUrl}{separator}stream_position={_streamPosition}";

      // Convert GetEventStreamHeaders to GetEventsHeaders for network call
      var headers = new GetEventsHeaders(extraHeaders: HeadersInput?.ExtraHeaders);
      var headersMap = Utils.PrepareParams(map: headers.ExtraHeaders);

      var response = await _eventsManager.NetworkSession.NetworkClient.FetchAsync(
          options: new FetchOptions(
              url: longPollWithStreamPosition,
              method: "GET",
              responseFormat: ResponseFormat.Json
          )
          {
            Headers = headersMap,
            Auth = _eventsManager.Auth,
            NetworkSession = _eventsManager.NetworkSession,
            CancellationToken = cancellationToken
          }
      ).ConfigureAwait(false);

      if (response.Data != null)
      {
        var message = SimpleJsonSerializer.Deserialize<LongPollMessage>(response.Data);

        if (message?.Message?.ToLowerInvariant() == "new_change")
        {
          return true; // Fetch events
        }
        else if (message?.Message?.ToLowerInvariant() == "reconnect")
        {
          _longPollingInfo = null; // Force refresh of long poll info
          return false;
        }
      }

      return false;
    }

    /// <summary>
    /// Fetches events from the Box API and returns them after deduplication.
    /// </summary>
    private async System.Threading.Tasks.Task<List<Event>> FetchEventsAsync(CancellationToken cancellationToken)
    {
      List<Event> eventsToYield = new List<Event>();

      try
      {
        // Build query params with current stream position
        // Convert GetEventStreamQueryParams types to GetEventsQueryParams types using string values
        var queryParams = new GetEventsQueryParams()
        {
          StreamType = _queryParams.StreamType != null ? new StringEnum<GetEventsQueryParamsStreamTypeField>(_queryParams.StreamType.StringValue) : null,
          StreamPosition = _streamPosition,
          Limit = _queryParams.Limit,
          EventType = _queryParams.EventType != null
            ? _queryParams.EventType.Select(et => new StringEnum<GetEventsQueryParamsEventTypeField>(et.StringValue)).ToList()
            : null,
          CreatedAfter = _queryParams.CreatedAfter,
          CreatedBefore = _queryParams.CreatedBefore
        };

        // Convert GetEventStreamHeaders to GetEventsHeaders
        var headers = new GetEventsHeaders(extraHeaders: HeadersInput?.ExtraHeaders);

        var events = await _eventsManager.GetEventsAsync(
            queryParams: queryParams,
            headers: headers,
            cancellationToken: cancellationToken
        ).ConfigureAwait(false);

        // Update stream position
        if (events.NextStreamPosition != null)
        {
          _streamPosition = events.NextStreamPosition.StringVal ?? events.NextStreamPosition.LongVal?.ToString() ?? "now";
        }

        // Collect events after deduplication
        if (events.Entries != null)
        {
          foreach (var entry in events.Entries)
          {
            if (!string.IsNullOrEmpty(entry.EventId))
            {
              // Skip if we've already seen this event
              if (_dedupHash.ContainsKey(entry.EventId))
              {
                continue;
              }
              _dedupHash[entry.EventId] = true;
            }

            eventsToYield.Add(entry);
          }

          // Clean up deduplication hash if it gets too large
          if (_dedupHash.Count >= _deduplicationFilterSize)
          {
            var eventIds = new HashSet<string>(events.Entries
                .Where(e => !string.IsNullOrEmpty(e.EventId))
                .Select(e => e.EventId));

            var keysToRemove = _dedupHash.Keys
                .Where(key => !eventIds.Contains(key))
                .ToList();

            foreach (var key in keysToRemove)
            {
              _dedupHash.Remove(key);
            }
          }
        }
      }
      catch (OperationCanceledException)
      {
        throw;
      }
      catch (Exception)
      {
        // On error, wait a bit before the next fetch attempt
        await System.Threading.Tasks.Task.Delay(1000, cancellationToken).ConfigureAwait(false);
      }

      return eventsToYield;
    }

    /// <summary>
    /// Internal class to deserialize long polling messages.
    /// </summary>
    internal class LongPollMessage : ISerializable
    {
      [System.Text.Json.Serialization.JsonPropertyName("source")]
      public string Source { get; set; }

      [System.Text.Json.Serialization.JsonPropertyName("version")]
      public int Version { get; set; }

      [System.Text.Json.Serialization.JsonPropertyName("message")]
      public string Message { get; set; }

      internal string RawJson { get; set; } = default;

      void ISerializable.SetJson(string json)
      {
        RawJson = json;
      }

      string ISerializable.GetJson()
      {
        return RawJson;
      }
    }
  }
}
