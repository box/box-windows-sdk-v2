using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetAiAgentsQueryParams {
        /// <summary>
        /// The mode to filter the agent config to return. Possible values are: `ask`, `text_gen`, and `extract`.
        /// </summary>
        public IReadOnlyList<string>? Mode { get; init; }

        /// <summary>
        /// The fields to return in the response.
        /// </summary>
        public IReadOnlyList<string>? Fields { get; init; }

        /// <summary>
        /// The state of the agents to return. Possible values are: `enabled`, `disabled` and `enabled_for_selected_users`.
        /// </summary>
        public IReadOnlyList<string>? AgentState { get; init; }

        /// <summary>
        /// Whether to include the Box default agents in the response.
        /// </summary>
        public bool? IncludeBoxDefault { get; init; }

        /// <summary>
        /// Defines the position marker at which to begin returning results.
        /// </summary>
        public string? Marker { get; init; }

        /// <summary>
        /// The maximum number of items to return per page.
        /// </summary>
        public long? Limit { get; init; }

        public GetAiAgentsQueryParams() {
            
        }
    }
}