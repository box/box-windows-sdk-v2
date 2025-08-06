using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetTermsOfServiceUserStatusesQueryParams {
        /// <summary>
        /// The ID of the terms of service.
        /// </summary>
        public string TosId { get; }

        /// <summary>
        /// Limits results to the given user ID.
        /// </summary>
        public string? UserId { get; init; }

        public GetTermsOfServiceUserStatusesQueryParams(string tosId) {
            TosId = tosId;
        }
    }
}