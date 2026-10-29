using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class GetShuttlePathEntriesV2026R0QueryParams {
        /// <summary>
        /// The unique identifier of the shuttle job whose path entries to list.
        /// </summary>
        public string JobId { get; }

        /// <summary>
        /// The side of the migration to browse.
        /// </summary>
        public StringEnum<GetShuttlePathEntriesV2026R0QueryParamsSideField> Side { get; }

        /// <summary>
        /// The human-readable parent path whose children are returned. This can
        /// be used with or without `path_id`. Omit both, or pass `/` as `path`,
        /// to list root entries.
        /// </summary>
        public string? Path { get; init; }

        /// <summary>
        /// The `path_id` value of the parent entry whose children are
        /// returned. This can be used with or without `path`. It distinguishes
        /// paths with identical names on systems that support stable identifiers.
        /// </summary>
        public string? PathId { get; init; }

        /// <summary>
        /// The email of the account whose path entries are returned. This is a
        /// system-native identifier and is not always an RFC 5322 address.
        /// </summary>
        public string? AccountEmail { get; init; }

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

        public GetShuttlePathEntriesV2026R0QueryParams(string jobId, GetShuttlePathEntriesV2026R0QueryParamsSideField side) {
            JobId = jobId;
            Side = side;
        }
    }
}