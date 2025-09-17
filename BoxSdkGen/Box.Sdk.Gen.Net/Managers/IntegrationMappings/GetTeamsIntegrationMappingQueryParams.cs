using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetTeamsIntegrationMappingQueryParams {
        /// <summary>
        /// Mapped item type, for which the mapping should be returned.
        /// </summary>
        public StringEnum<GetTeamsIntegrationMappingQueryParamsPartnerItemTypeField>? PartnerItemType { get; init; }

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
        public StringEnum<GetTeamsIntegrationMappingQueryParamsBoxItemTypeField>? BoxItemType { get; init; }

        public GetTeamsIntegrationMappingQueryParams() {
            
        }
    }
}