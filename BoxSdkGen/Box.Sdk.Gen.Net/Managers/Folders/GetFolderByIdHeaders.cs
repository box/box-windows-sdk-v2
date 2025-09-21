using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetFolderByIdHeaders {
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
        /// The URL, and optional password, for the shared link of this item.
        /// 
        /// This header can be used to access items that have not been
        /// explicitly shared with a user.
        /// 
        /// Use the format `shared_link=[link]` or if a password is required then
        /// use `shared_link=[link]&shared_link_password=[password]`.
        /// 
        /// This header can be used on the file or folder shared, as well as on any files
        /// or folders nested within the item.
        /// </summary>
        public string? Boxapi { get; init; }

        /// <summary>
        /// Extra headers that will be included in the HTTP request.
        /// </summary>
        public Dictionary<string, string?> ExtraHeaders { get; }

        public GetFolderByIdHeaders(Dictionary<string, string?>? extraHeaders = default) {
            ExtraHeaders = extraHeaders ?? new Dictionary<string, string?>() {  };
        }
    }
}