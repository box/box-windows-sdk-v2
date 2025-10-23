using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class GetEnterpriseConfigurationByIdV2025R0QueryParams {
        /// <summary>
        /// The comma-delimited list of the enterprise configuration categories. 
        /// Allowed values: `security`, `content_and_sharing`, `user_settings`, `shield`.
        /// </summary>
        public string Categories { get; set; }

        public GetEnterpriseConfigurationByIdV2025R0QueryParams(string categories) {
            Categories = categories;
        }
    }
}