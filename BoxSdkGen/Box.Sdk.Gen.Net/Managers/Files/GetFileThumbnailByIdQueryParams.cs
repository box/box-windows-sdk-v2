using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetFileThumbnailByIdQueryParams {
        /// <summary>
        /// The minimum height of the thumbnail.
        /// </summary>
        public long? MinHeight { get; init; }

        /// <summary>
        /// The minimum width of the thumbnail.
        /// </summary>
        public long? MinWidth { get; init; }

        /// <summary>
        /// The maximum height of the thumbnail.
        /// </summary>
        public long? MaxHeight { get; init; }

        /// <summary>
        /// The maximum width of the thumbnail.
        /// </summary>
        public long? MaxWidth { get; init; }

        public GetFileThumbnailByIdQueryParams() {
            
        }
    }
}