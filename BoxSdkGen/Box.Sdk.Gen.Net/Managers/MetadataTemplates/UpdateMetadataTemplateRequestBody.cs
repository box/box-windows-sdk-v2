using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateMetadataTemplateRequestBody : ISerializable {
        /// <summary>
        /// The type of change to perform on the template. Some
        /// of these are hazardous as they will change existing templates.
        /// </summary>
        [JsonPropertyName("op")]
        [JsonConverter(typeof(StringEnumConverter<UpdateMetadataTemplateRequestBodyOpField>))]
        public StringEnum<UpdateMetadataTemplateRequestBodyOpField> Op { get; }

        /// <summary>
        /// The data for the operation. This will vary depending on the
        /// operation being performed.
        /// </summary>
        [JsonPropertyName("data")]
        public Dictionary<string, object>? Data { get; init; }

        /// <summary>
        /// For operations that affect a single field this defines the key of
        /// the field that is affected.
        /// </summary>
        [JsonPropertyName("fieldKey")]
        public string? FieldKey { get; init; }

        /// <summary>
        /// For operations that affect multiple fields this defines the keys
        /// of the fields that are affected.
        /// </summary>
        [JsonPropertyName("fieldKeys")]
        public IReadOnlyList<string>? FieldKeys { get; init; }

        /// <summary>
        /// For operations that affect a single `enum` option this defines
        /// the key of the option that is affected.
        /// </summary>
        [JsonPropertyName("enumOptionKey")]
        public string? EnumOptionKey { get; init; }

        /// <summary>
        /// For operations that affect multiple `enum` options this defines
        /// the keys of the options that are affected.
        /// </summary>
        [JsonPropertyName("enumOptionKeys")]
        public IReadOnlyList<string>? EnumOptionKeys { get; init; }

        /// <summary>
        /// For operations that affect a single multi select option this
        /// defines the key of the option that is affected.
        /// </summary>
        [JsonPropertyName("multiSelectOptionKey")]
        public string? MultiSelectOptionKey { get; init; }

        /// <summary>
        /// For operations that affect multiple multi select options this
        /// defines the keys of the options that are affected.
        /// </summary>
        [JsonPropertyName("multiSelectOptionKeys")]
        public IReadOnlyList<string>? MultiSelectOptionKeys { get; init; }

        public UpdateMetadataTemplateRequestBody(UpdateMetadataTemplateRequestBodyOpField op) {
            Op = op;
        }
        
        [JsonConstructorAttribute]
        internal UpdateMetadataTemplateRequestBody(StringEnum<UpdateMetadataTemplateRequestBodyOpField> op) {
            Op = op;
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