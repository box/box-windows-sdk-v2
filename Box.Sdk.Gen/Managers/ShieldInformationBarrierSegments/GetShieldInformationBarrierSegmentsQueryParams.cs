using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetShieldInformationBarrierSegmentsQueryParams {
        /// <summary>
        /// The ID of the shield information barrier.
        /// </summary>
        public string ShieldInformationBarrierId { get; }

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

        public GetShieldInformationBarrierSegmentsQueryParams(string shieldInformationBarrierId) {
            ShieldInformationBarrierId = shieldInformationBarrierId;
        }
    }
}