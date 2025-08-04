using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetSlackIntegrationMappingQueryParams {
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
        /// Mapped item type, for which the mapping should be returned.
        /// </summary>
        public StringEnum<GetSlackIntegrationMappingQueryParamsPartnerItemTypeField>? PartnerItemType { get; init; }

        /// <summary>
        /// ID of the mapped item,
        /// for which the mapping should be returned.
        /// </summary>
        public string? PartnerItemId { get; init; }

        /// <summary>
        /// Box item ID, for which the mappings should be returned.
        /// </summary>
        public string? BoxItemId { get; init; }

        /// <summary>
        /// Box item type, for
        /// which the mappings should be returned.
        /// </summary>
        public StringEnum<GetSlackIntegrationMappingQueryParamsBoxItemTypeField>? BoxItemType { get; init; }

        /// <summary>
        /// Whether the mapping has been manually created.
        /// </summary>
        public bool? IsManuallyCreated { get; init; }

        public GetSlackIntegrationMappingQueryParams() {
            
        }
    }
}