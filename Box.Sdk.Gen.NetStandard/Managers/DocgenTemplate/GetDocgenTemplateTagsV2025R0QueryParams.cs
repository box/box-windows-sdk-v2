using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class GetDocgenTemplateTagsV2025R0QueryParams {
        /// <summary>
        /// Id of template version.
        /// </summary>
        public string TemplateVersionId { get; set; }

        /// <summary>
        /// Defines the position marker at which to begin returning results. This is
        /// used when paginating using marker-based pagination.
        /// 
        /// This requires `usemarker` to be set to `true`.
        /// </summary>
        public string Marker { get; set; }

        /// <summary>
        /// The maximum number of items to return per page.
        /// </summary>
        public long? Limit { get; set; }

        public GetDocgenTemplateTagsV2025R0QueryParams() {
            
        }
    }
}