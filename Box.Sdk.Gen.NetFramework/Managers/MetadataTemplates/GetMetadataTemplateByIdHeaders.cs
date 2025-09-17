using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetMetadataTemplateByIdHeaders {
        /// <summary>
        /// Extra headers that will be included in the HTTP request.
        /// </summary>
        public Dictionary<string, string> ExtraHeaders { get; set; }

        public GetMetadataTemplateByIdHeaders(Dictionary<string, string> extraHeaders = default) {
            ExtraHeaders = extraHeaders ?? new Dictionary<string, string>() {  };
        }
    }
}