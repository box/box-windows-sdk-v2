using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class GetAiSessionsV2026R0QueryParams {
        /// <summary>
        /// Restricts the list to AI sessions with this origin. The only supported
        /// value is `api`.
        /// </summary>
        public StringEnum<GetAiSessionsV2026R0QueryParamsSourceField> Source { get; set; }

        /// <summary>
        /// The position marker at which to begin returning results, as returned in
        /// `next_marker` by the previous page. Omit it for the first page. All other
        /// parameters must match the request that returned the marker.
        /// </summary>
        public string Marker { get; set; }

        /// <summary>
        /// The field to sort results by. Possible values are `created_at`,
        /// `updated_at`, and `name`. Default is `updated_at`, which advances on each
        /// turn of session activity.
        /// </summary>
        public StringEnum<GetAiSessionsV2026R0QueryParamsSortField> Sort { get; set; }

        /// <summary>
        /// The direction to sort results in, either ascending (`asc`) or
        /// descending (`desc`). Default is `desc`.
        /// </summary>
        public StringEnum<GetAiSessionsV2026R0QueryParamsDirectionField> Direction { get; set; }

        /// <summary>
        /// The maximum number of AI sessions to return per page. The maximum is
        /// `100`; larger values are treated as `100`. The server may return fewer
        /// entries than requested.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// A comma-separated list of OAuth client IDs. Restricts the list to AI
        /// sessions created by any of these applications. Client IDs that match no
        /// application are ignored, so the response is an empty list if none of them
        /// match. Empty entries are ignored, and an empty value is treated as if the
        /// parameter were not provided.
        /// </summary>
        public IReadOnlyList<string> ClientIds { get; set; }

        public GetAiSessionsV2026R0QueryParams(GetAiSessionsV2026R0QueryParamsSourceField source) {
            Source = source;
        }
    }
}