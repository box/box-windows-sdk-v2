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
    public class BoxEventsManager : BoxResourceManager
    {
        public const string ENTERPRISE_EVENTS_STREAM_TYPE = "admin_logs";

        public BoxEventsManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null)
            : base(config, service, converter, auth, asUser) { }

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
    }
}
