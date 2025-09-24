using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents the events endpoint
    /// </summary>
    public interface IBoxEventsManager
    {
        /// <summary>
        /// Retrieves up to a year's events for all users in the enterprise. High latency. You must be using a token that is scoped to admin level in order to use this endpoint.
        /// </summary>
        /// <param name="limit">Limits the number of events returned (defaults to 500).</param>
        /// <param name="streamPosition">The starting position for fetching the events. This is used in combination with the limit to determine which events to return to the caller. Use the results from the next_stream_position of your last call to get the next set of events.</param>
        /// <param name="eventTypes">Events to filter by.</param>
        /// <param name="createdAfter">A lower bound on the timestamp of the events returned.</param>
        /// <param name="createdBefore">An upper bound on the timestamp of the events returned.</param>
        /// <returns></returns>
        Task<BoxEventCollection<BoxEnterpriseEvent>> EnterpriseEventsAsync(int limit = 500,
            string streamPosition = null,
            IEnumerable<string> eventTypes = null,
            DateTimeOffset? createdAfter = null,
            DateTimeOffset? createdBefore = null);

        /// <summary>
        /// Use this to get events for a given user.
        /// </summary>
        /// <param name="limit">Limits the number of events returned (defaults to 500).</param>
        /// <param name="streamType">Restricts the types of events returned: all returns all events; changes returns events that may cause file tree changes such as file updates or collaborations; sync returns events that may cause file tree changes only for synced folders.</param>
        /// <param name="streamPosition">The location in the event stream from which you want to start receiving events. You can specify the special value 'now' to get 0 events and the latest stream_position value. Defaults to 'now'.</param>
        /// <param name="dedupeEvents">Whether or not to automatically de-duplicate events as they are received. Defaults to true.</param>
        /// <returns></returns>
        Task<BoxEventCollection<BoxEnterpriseEvent>> UserEventsAsync(int limit = 500,
            UserEventsStreamType streamType = UserEventsStreamType.all,
            string streamPosition = "now",
            bool dedupeEvents = true);

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
        Task LongPollUserEvents(string streamPosition,
            Action<BoxEventCollection<BoxEnterpriseEvent>> newEventsCallback,
            CancellationToken cancellationToken,
            UserEventsStreamType streamType = UserEventsStreamType.all,
            bool dedupeEvents = true,
            int? retryTimeoutOverride = null);

        /// <summary>
        /// Retrieves up to a two weeks's events for all users in the enterprise. Low latency. You must be using a token that is scoped to admin level in order to use this endpoint.
        /// </summary>
        /// <param name="limit">Limits the number of events returned (defaults to 500).</param>
        /// <param name="streamPosition">The starting position for fetching the events. This is used in combination with the limit to determine which events to return to the caller. Use the results from the next_stream_position of your last call to get the next set of events.</param>
        /// <param name="eventTypes">Events to filter by.</param>
        /// <returns></returns>
        Task<BoxEventCollection<BoxEnterpriseEvent>> EnterpriseEventsStreamingAsync(int limit = 500,
            string streamPosition = null,
            IEnumerable<string> eventTypes = null);
    }
}
