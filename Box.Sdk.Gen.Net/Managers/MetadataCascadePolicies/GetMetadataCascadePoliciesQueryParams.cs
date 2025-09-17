using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetMetadataCascadePoliciesQueryParams {
        /// <summary>
        /// Specifies which folder to return policies for. This can not be used on the
        /// root folder with ID `0`.
        /// </summary>
        public string FolderId { get; }

        /// <summary>
        /// The ID of the enterprise ID for which to find metadata
        /// cascade policies. If not specified, it defaults to the
        /// current enterprise.
        /// </summary>
        public string? OwnerEnterpriseId { get; init; }

        /// <summary>
        /// Defines the position marker at which to begin returning results. This is
        /// used when paginating using marker-based pagination.
        /// 
        /// This requires `usemarker` to be set to `true`.
        /// </summary>
        public string? Marker { get; init; }

        /// <summary>
        /// The offset of the item at which to begin the response.
        /// 
        /// Queries with offset parameter value
        /// exceeding 10000 will be rejected
        /// with a 400 response.
        /// </summary>
        public long? Offset { get; init; }

        public GetMetadataCascadePoliciesQueryParams(string folderId) {
            FolderId = folderId;
        }
    }
}