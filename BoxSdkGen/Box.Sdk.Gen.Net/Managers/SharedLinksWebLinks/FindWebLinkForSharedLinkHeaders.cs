using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class FindWebLinkForSharedLinkHeaders {
        /// <summary>
        /// Ensures an item is only returned if it has changed.
        /// 
        /// Pass in the item's last observed `etag` value
        /// into this header and the endpoint will fail
        /// with a `304 Not Modified` if the item has not
        /// changed since.
        /// </summary>
        public string? IfNoneMatch { get; init; }

        /// <summary>
        /// A header containing the shared link and optional password for the
        /// shared link.
        /// 
        /// The format for this header is as follows:
        /// 
        /// `shared_link=[link]&shared_link_password=[password]`.
        /// </summary>
        public string Boxapi { get; }

        /// <summary>
        /// Extra headers that will be included in the HTTP request.
        /// </summary>
        public Dictionary<string, string?> ExtraHeaders { get; }

        public FindWebLinkForSharedLinkHeaders(string boxapi, Dictionary<string, string?>? extraHeaders = default) {
            Boxapi = boxapi;
            ExtraHeaders = extraHeaders ?? new Dictionary<string, string?>() {  };
        }
    }
}