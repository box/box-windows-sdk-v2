using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetLegalHoldPolicyAssignmentsQueryParams {
        /// <summary>
        /// The ID of the legal hold policy.
        /// </summary>
        public string PolicyId { get; }

        /// <summary>
        /// Filters the results by the type of item the
        /// policy was applied to.
        /// </summary>
        public StringEnum<GetLegalHoldPolicyAssignmentsQueryParamsAssignToTypeField>? AssignToType { get; init; }

        /// <summary>
        /// Filters the results by the ID of item the
        /// policy was applied to.
        /// </summary>
        public string? AssignToId { get; init; }

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

        public GetLegalHoldPolicyAssignmentsQueryParams(string policyId) {
            PolicyId = policyId;
        }
    }
}