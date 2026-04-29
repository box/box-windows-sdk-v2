using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class CreateAutomateWorkflowStartV2026R0Headers {
        /// <summary>
        /// Version header.
        /// </summary>
        public StringEnum<BoxVersionHeaderV2026R0> BoxVersion { get; set; }

        /// <summary>
        /// Extra headers that will be included in the HTTP request.
        /// </summary>
        public Dictionary<string, string> ExtraHeaders { get; set; }

        public CreateAutomateWorkflowStartV2026R0Headers(BoxVersionHeaderV2026R0 boxVersion = BoxVersionHeaderV2026R0._20260, Dictionary<string, string> extraHeaders = default) {
            BoxVersion = boxVersion;
            ExtraHeaders = extraHeaders ?? new Dictionary<string, string>() {  };
        }
    }
}