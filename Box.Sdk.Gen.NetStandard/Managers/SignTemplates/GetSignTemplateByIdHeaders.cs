using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetSignTemplateByIdHeaders {
        /// <summary>
        /// Extra headers that will be included in the HTTP request.
        /// </summary>
        public Dictionary<string, string> ExtraHeaders { get; set; }

        public GetSignTemplateByIdHeaders(Dictionary<string, string> extraHeaders = default) {
            ExtraHeaders = extraHeaders ?? new Dictionary<string, string>() {  };
        }
    }
}