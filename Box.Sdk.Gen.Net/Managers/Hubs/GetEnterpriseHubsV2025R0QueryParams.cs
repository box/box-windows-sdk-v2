using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class GetEnterpriseHubsV2025R0QueryParams {
        /// <summary>
        /// The query string to search for Box Hubs.
        /// </summary>
        public string? Query { get; init; }

        /// <summary>
        /// The field to sort results by.
        /// Possible values include `name`, `updated_at`,
        /// `last_accessed_at`, `view_count`, and `relevance`.
        /// Default is `relevance`.
        /// </summary>
        public string? Sort { get; init; }

        /// <summary>
        /// The direction to sort results in. This can be either in alphabetical ascending
        /// (`ASC`) or descending (`DESC`) order.
        /// </summary>
        public StringEnum<GetEnterpriseHubsV2025R0QueryParamsDirectionField>? Direction { get; init; }

        /// <summary>
        /// Defines the position marker at which to begin returning results. This is
        /// used when paginating using marker-based pagination.
        /// </summary>
        public string? Marker { get; init; }

        /// <summary>
        /// The maximum number of items to return per page.
        /// </summary>
        public long? Limit { get; init; }

        public GetEnterpriseHubsV2025R0QueryParams() {
            
        }
    }
}