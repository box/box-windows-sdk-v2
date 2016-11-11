using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Models;
using Box.V2.Extensions;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents the events endpoint
    /// </summary>
    public class BoxEventsManager : BoxResourceManager
    {
        public const string ENTERPRISE_EVENTS_STREAM_TYPE = "admin_logs";

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
                                                                        List<string> eventTypes = null,
                                                                        DateTime? createdAfter = null,
                                                                        DateTime? createdBefore = null)
        {
            var createdAfterString = createdAfter.HasValue ? createdAfter.Value.ToString(Constants.RFC3339DateFormat) : null;
            var createdBeforeString = createdBefore.HasValue ? createdBefore.Value.ToString(Constants.RFC3339DateFormat) : null;

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
        /// <returns></returns>
        public async Task<BoxEventCollection<BoxEnterpriseEvent>> UserEventsAsync(int limit = 500, UserEventsStreamType streamType = UserEventsStreamType.all, string streamPosition = "now")
        {
            BoxRequest request = new BoxRequest(_config.EventsUri)
                .Param("stream_type", streamType.ToString())
                .Param("limit", limit.ToString())
                .Param("stream_position", streamPosition);

            IBoxResponse<BoxEventCollection<BoxEnterpriseEvent>> response = await ToResponseAsync<BoxEventCollection<BoxEnterpriseEvent>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }


    }
}
