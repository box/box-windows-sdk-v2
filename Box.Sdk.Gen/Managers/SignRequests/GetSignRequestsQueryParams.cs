using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetSignRequestsQueryParams {
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

        /// <summary>
        /// A list of sender emails to filter the signature requests by sender.
        /// If provided, `shared_requests` must be set to `true`.
        /// </summary>
        public IReadOnlyList<string>? Senders { get; init; }

        /// <summary>
        /// If set to `true`, only includes requests that user is not an owner,
        /// but user is a collaborator. Collaborator access is determined by the
        /// user access level of the sign files of the request.
        /// Default is `false`. Must be set to `true` if `senders` are provided.
        /// </summary>
        public bool? SharedRequests { get; init; }

        public GetSignRequestsQueryParams() {
            
        }
    }
}