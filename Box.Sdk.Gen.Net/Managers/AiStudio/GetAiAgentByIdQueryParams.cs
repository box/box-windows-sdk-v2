using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetAiAgentByIdQueryParams {
        /// <summary>
        /// The fields to return in the response.
        /// </summary>
        public IReadOnlyList<string>? Fields { get; init; }

        public GetAiAgentByIdQueryParams() {
            
        }
    }
}