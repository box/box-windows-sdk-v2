using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetCollectionsQueryParams {
        /// <summary>
        /// A comma-separated list of attributes to include in the
        /// response. This can be used to request fields that are
        /// not normally returned in a standard response.
        /// 
        /// Be aware that specifying this parameter will have the
        /// effect that none of the standard fields are returned in
        /// the response unless explicitly specified, instead only
        /// fields for the mini representation are returned, additional
        /// to the fields requested.
        /// </summary>
        public IReadOnlyList<string>? Fields { get; init; }

        /// <summary>
        /// The offset of the item at which to begin the response.
        /// 
        /// Queries with offset parameter value
        /// exceeding 10000 will be rejected
        /// with a 400 response.
        /// </summary>
        public long? Offset { get; init; }

        /// <summary>
        /// The maximum number of items to return per page.
        /// </summary>
        public long? Limit { get; init; }

        public GetCollectionsQueryParams() {
            
        }
    }
}