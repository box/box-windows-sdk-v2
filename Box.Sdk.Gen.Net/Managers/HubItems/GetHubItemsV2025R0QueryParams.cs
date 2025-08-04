using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class GetHubItemsV2025R0QueryParams {
        /// <summary>
        /// The unique identifier that represent a hub.
        /// 
        /// The ID for any hub can be determined
        /// by visiting this hub in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/hubs/123`
        /// the `hub_id` is `123`.
        /// </summary>
        public string HubId { get; }

        /// <summary>
        /// Defines the position marker at which to begin returning results. This is
        /// used when paginating using marker-based pagination.
        /// 
        /// This requires `usemarker` to be set to `true`.
        /// </summary>
        public string? Marker { get; init; }

        /// <summary>
        /// The maximum number of items to return per page.
        /// </summary>
        public long? Limit { get; init; }

        public GetHubItemsV2025R0QueryParams(string hubId) {
            HubId = hubId;
        }
    }
}