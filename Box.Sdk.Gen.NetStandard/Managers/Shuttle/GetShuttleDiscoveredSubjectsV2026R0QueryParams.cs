using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class GetShuttleDiscoveredSubjectsV2026R0QueryParams {
        /// <summary>
        /// The unique identifier of the shuttle job whose discovered subjects to list.
        /// </summary>
        public string JobId { get; set; }

        /// <summary>
        /// Defines the position marker at which to begin returning results. This is
        /// used when paginating using marker-based pagination.
        /// 
        /// This requires `usemarker` to be set to `true`.
        /// </summary>
        public string Marker { get; set; }

        /// <summary>
        /// The maximum number of items to return per page.
        /// </summary>
        public long? Limit { get; set; }

        public GetShuttleDiscoveredSubjectsV2026R0QueryParams(string jobId) {
            JobId = jobId;
        }
    }
}