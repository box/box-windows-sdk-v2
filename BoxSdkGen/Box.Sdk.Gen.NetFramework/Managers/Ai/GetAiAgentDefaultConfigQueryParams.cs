using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Managers {
    public class GetAiAgentDefaultConfigQueryParams {
        /// <summary>
        /// The mode to filter the agent config to return.
        /// </summary>
        public StringEnum<GetAiAgentDefaultConfigQueryParamsModeField> Mode { get; set; }

        /// <summary>
        /// The ISO language code to return the agent config for.
        /// If the language is not supported the default agent config is returned.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The model to return the default agent config for.
        /// </summary>
        public string Model { get; set; }

        public GetAiAgentDefaultConfigQueryParams(GetAiAgentDefaultConfigQueryParamsModeField mode) {
            Mode = mode;
        }
    }
}