using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetFileVersionLegalHoldsQueryParams {
        /// <summary>
        /// The ID of the legal hold policy to get the file version legal
        /// holds for.
        /// </summary>
        public string PolicyId { get; }

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

        public GetFileVersionLegalHoldsQueryParams(string policyId) {
            PolicyId = policyId;
        }
    }
}