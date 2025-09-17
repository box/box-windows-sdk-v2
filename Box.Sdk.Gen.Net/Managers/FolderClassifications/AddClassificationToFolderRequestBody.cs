using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class AddClassificationToFolderRequestBody : ISerializable {
        /// <summary>
        /// The name of the classification to apply to this folder.
        /// 
        /// To list the available classifications in an enterprise,
        /// use the classification API to retrieve the
        /// [classification template](e://get_metadata_templates_enterprise_securityClassification-6VMVochwUWo_schema)
        /// which lists all available classification keys.
        /// </summary>
        [JsonPropertyName("Box__Security__Classification__Key")]
        public string? BoxSecurityClassificationKey { get; init; }

        public AddClassificationToFolderRequestBody() {
            
        }
        internal string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}