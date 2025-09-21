using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class CreateDocgenTemplateV2025R0Headers {
        /// <summary>
        /// Version header.
        /// </summary>
        public StringEnum<BoxVersionHeaderV2025R0> BoxVersion { get; }

        /// <summary>
        /// Extra headers that will be included in the HTTP request.
        /// </summary>
        public Dictionary<string, string?> ExtraHeaders { get; }

        public CreateDocgenTemplateV2025R0Headers(BoxVersionHeaderV2025R0 boxVersion = BoxVersionHeaderV2025R0._20250, Dictionary<string, string?>? extraHeaders = default) {
            BoxVersion = boxVersion;
            ExtraHeaders = extraHeaders ?? new Dictionary<string, string?>() {  };
        }
    }
}