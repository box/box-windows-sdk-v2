using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Models;
using Box.V2.Extensions;
using Box.V2.Services;
using Box.V2.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents the events endpoint
    /// </summary>
    public class BoxEventsManager : BoxResourceManager
    {
        public const string ENTERPRISE_EVENTS_STREAM_TYPE = "admin_logs";
        public readonly LRUCache<string,bool> USER_EVENTS_DEDUPE_CACHE = new LRUCache<string, bool>(1000);

        public BoxEventsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Retrieve a chunk of Enterprise Events.  You must be using a token that is scoped to admin level in order to use this endpoint.
        /// </summary>
        /// <param name="limit">Limits the number of events returned (defaults to 500).</param>
        /// <param name="streamPosition">The starting position for fetching the events. This is used in combination with the limit to determine which events to return to the caller. Use the results from the next_stream_position of your last call to get the next set of events.</param>
        /// <param name="eventTypes">Events to filter by.</param>
        /// <param name="createdAfter">A lower bound on the timestamp of the events returned.</param>
        /// <param name="createdBefore">An upper bound on the timestamp of the events returned.</param>
        /// <returns></returns>
        public async Task<BoxEventCollection<BoxEnterpriseEvent>> EnterpriseEventsAsync(int limit = 500,
                                                                        string streamPosition = null,
                                                                        IEnumerable<string> eventTypes = null,
                                                                        DateTime? createdAfter = null,
                                                                        DateTime? createdBefore = null)
        {
            var createdAfterString = createdAfter.HasValue ? createdAfter.Value.ToUniversalTime().ToString(Constants.RFC3339DateFormat_UTC) : null;
            var createdBeforeString = createdBefore.HasValue ? createdBefore.Value.ToUniversalTime().ToString(Constants.RFC3339DateFormat_UTC) : null;

            BoxRequest request = new BoxRequest(_config.EventsUri)
                .Param("stream_type", ENTERPRISE_EVENTS_STREAM_TYPE)
                .Param("limit", limit.ToString())
                .Param("stream_position", streamPosition)
                .Param("event_type", eventTypes)
                .Param("created_after", createdAfterString)
                .Param("created_before", createdBeforeString);

            IBoxResponse<BoxEventCollection<BoxEnterpriseEvent>> response = await ToResponseAsync<BoxEventCollection<BoxEnterpriseEvent>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Use this to get events for a given user.
        /// </summary>
        /// <param name="limit">Limits the number of events returned (defaults to 500).</param>
        /// <param name="streamType">Restricts the types of events returned: all returns all events; changes returns events that may cause file tree changes such as file updates or collaborations; sync returns events that may cause file tree changes only for synced folders.</param>
        /// <param name="streamPosition">The location in the event stream from which you want to start receiving events. You can specify the special value 'now' to get 0 events and the latest stream_position value. Defaults to 'now'.</param>
        /// <param name="dedupeEvents">Whether or not to automatically de-duplicate events as they are received. Defaults to true.</param>
        /// <returns></returns>
        public async Task<BoxEventCollection<BoxEnterpriseEvent>> UserEventsAsync(int limit = 500, 
                                                                                  UserEventsStreamType streamType = UserEventsStreamType.all,
                                                                                  string streamPosition = "now", 
                                                                                  bool dedupeEvents = true)
        {
            BoxRequest request = new BoxRequest(_config.EventsUri)
                .Param("stream_type", streamType.ToString())
                .Param("limit", limit.ToString())
                .Param("stream_position", streamPosition);

            IBoxResponse<BoxEventCollection<BoxEnterpriseEvent>> response = await ToResponseAsync<BoxEventCollection<BoxEnterpriseEvent>>(request).ConfigureAwait(false);

            if (dedupeEvents)
            {
                List<BoxEnterpriseEvent> filteredEvents = new List<BoxEnterpriseEvent>();
                foreach (var e in response.ResponseObject.Entries)
                {
                    bool notUsed = true;
                    if (!USER_EVENTS_DEDUPE_CACHE.TryGetValue(e.EventId, out notUsed))
                    {
                        USER_EVENTS_DEDUPE_CACHE.Add(e.EventId, true);
                        filteredEvents.Add(e);
                    }
                }

                response.ResponseObject.Entries = filteredEvents;
            }   

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to get real-time notification of activity in a Box account.
        /// </summary>
        /// <param name="streamPosition">The location in the event stream from which you want to start receiving events.</param>
        /// <param name="newEventsCallback">Method to invoke when new events are received.</param>
        /// <param name="cancellationToken">Used to request that the long polling process terminate.</param>
        /// <param name="streamType">Restricts the types of events returned: all returns all events; changes returns events that may cause file tree changes such as file updates or collaborations; sync returns events that may cause file tree changes only for synced folders.</param>
        /// <param name="dedupeEvents">Whether or not to automatically de-duplicate events as they are received. Defaults to true.</param>
        /// <param name="retryTimeoutOverride">Used to override the retry timeout value returned from the long polling OPTIONS request.</param>
        /// <returns></returns>
        public async Task LongPollUserEvents(string streamPosition,
                                             Action<BoxEventCollection<BoxEnterpriseEvent>> newEventsCallback,
                                             CancellationToken cancellationToken,
                                             UserEventsStreamType streamType = UserEventsStreamType.all, 
                                             bool dedupeEvents = true,
                                             int? retryTimeoutOverride = null)
        {
            const string NEW_CHANGE_MESSAGE = "new_change";

            string nextStreamPosition = streamPosition;

            while (true)
            {
                BoxRequest optionsRequest = new BoxRequest(_config.EventsUri)
               .Param("stream_type", streamType.ToString())
               .Method(RequestMethod.Options);

                IBoxResponse<BoxLongPollInfoCollection<BoxLongPollInfo>> optionsResponse = await ToResponseAsync<BoxLongPollInfoCollection<BoxLongPollInfo>>(optionsRequest).ConfigureAwait(false);
                var longPollInfo = optionsResponse.ResponseObject.Entries[0];
                var numRetries = Int32.Parse(longPollInfo.MaxRetries);

                bool pollAgain = true;
                do
                { 
                    if (cancellationToken.IsCancellationRequested)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }      
                    try
                    {
                        var timeout = retryTimeoutOverride.HasValue ? retryTimeoutOverride.Value : longPollInfo.RetryTimeout;
                        BoxRequest pollRequest = new BoxRequest(longPollInfo.Url) { Timeout = TimeSpan.FromSeconds(timeout) };
                        IBoxResponse<BoxLongPollMessage> pollResponse = await ToResponseAsync<BoxLongPollMessage>(pollRequest).ConfigureAwait(false);

                        var message = pollResponse.ResponseObject.Message;
                        if (message == NEW_CHANGE_MESSAGE)
                        {
                            BoxEventCollection<BoxEnterpriseEvent> newEvents = null;
                            do
                            {
                                newEvents = await UserEventsAsync(streamType: streamType, streamPosition: nextStreamPosition, dedupeEvents: dedupeEvents).ConfigureAwait(false);
                                nextStreamPosition = newEvents.NextStreamPosition;
                                if (newEvents.Entries.Count > 0)
                                {
                                    newEventsCallback.Invoke(newEvents);
                                }
                            } while (newEvents.Entries.Count > 0);
                        }
                    }
                    catch
                    {
                        //Most likely request timed out.
                        //If we've reached maximum number of retries then bounce all the way back to the OPTIONS request.
                        pollAgain = numRetries-- > 0;
                    }
                } while (pollAgain);                         
            } 
        }
    }
}
