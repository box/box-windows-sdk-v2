using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System.Collections.ObjectModel;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetMetadataTaxonomyNodesQueryParams {
        /// <summary>
        /// Filters results by taxonomy level. Multiple values can be provided. 
        /// Results include nodes that match any of the specified values.
        /// </summary>
        public IReadOnlyList<long>? Level { get; init; }

        /// <summary>
        /// Node identifier of a direct parent node. Multiple values can be provided. 
        /// Results include nodes that match any of the specified values.
        /// </summary>
        public IReadOnlyList<string>? Parent { get; init; }

        /// <summary>
        /// Node identifier of any ancestor node. Multiple values can be provided. 
        /// Results include nodes that match any of the specified values.
        /// </summary>
        public IReadOnlyList<string>? Ancestor { get; init; }

        /// <summary>
        /// Query text to search for the taxonomy nodes.
        /// </summary>
        public string? Query { get; init; }

        /// <summary>
        /// When set to `true` this provides the total number of nodes that matched the query. 
        /// The response will compute counts of up to 10,000 elements. Defaults to `false`.
        /// </summary>
        public bool? IncludeTotalResultCount { get; init; }

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

        public GetMetadataTaxonomyNodesQueryParams() {
            
        }
    }
}