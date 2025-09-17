using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class FindAppItemForSharedLinkHeaders {
        /// <summary>
        /// A header containing the shared link and optional password for the
        /// shared link.
        /// 
        /// The format for this header is `shared_link=[link]&shared_link_password=[password]`.
        /// </summary>
        public string Boxapi { get; }

        /// <summary>
        /// Extra headers that will be included in the HTTP request.
        /// </summary>
        public Dictionary<string, string?> ExtraHeaders { get; }

        public FindAppItemForSharedLinkHeaders(string boxapi, Dictionary<string, string?>? extraHeaders = default) {
            Boxapi = boxapi;
            ExtraHeaders = extraHeaders ?? new Dictionary<string, string?>() {  };
        }
    }
}