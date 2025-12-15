using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetFileMetadataByIdQueryParams {
        /// <summary>
        /// Taxonomy field values are returned in `API view` by default, meaning 
        /// the value is represented with a taxonomy node identifier. 
        /// To retrieve the `Hydrated view`, where taxonomy values are represented 
        /// with the full taxonomy node information, set this parameter to `hydrated`. 
        /// This is the only supported value for this parameter.
        /// </summary>
        public string? View { get; init; }

        public GetFileMetadataByIdQueryParams() {
            
        }
    }
}