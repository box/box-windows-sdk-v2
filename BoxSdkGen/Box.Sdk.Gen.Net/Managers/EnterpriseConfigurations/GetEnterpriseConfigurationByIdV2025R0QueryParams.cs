using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class GetEnterpriseConfigurationByIdV2025R0QueryParams {
        /// <summary>
        /// A comma-separated list of the enterprise configuration categories.
        /// Allowed values: `security`, `content_and_sharing`, `user_settings`, `shield`.
        /// </summary>
        public IReadOnlyList<string> Categories { get; }

        public GetEnterpriseConfigurationByIdV2025R0QueryParams(IReadOnlyList<string> categories) {
            Categories = categories;
        }
    }
}