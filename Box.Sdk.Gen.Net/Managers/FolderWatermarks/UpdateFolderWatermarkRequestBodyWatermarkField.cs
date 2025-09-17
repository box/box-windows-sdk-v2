using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateFolderWatermarkRequestBodyWatermarkField : ISerializable {
        /// <summary>
        /// The type of watermark to apply.
        /// 
        /// Currently only supports one option.
        /// </summary>
        [JsonPropertyName("imprint")]
        [JsonConverter(typeof(StringEnumConverter<UpdateFolderWatermarkRequestBodyWatermarkImprintField>))]
        public StringEnum<UpdateFolderWatermarkRequestBodyWatermarkImprintField> Imprint { get; }

        public UpdateFolderWatermarkRequestBodyWatermarkField(UpdateFolderWatermarkRequestBodyWatermarkImprintField imprint = UpdateFolderWatermarkRequestBodyWatermarkImprintField.Default) {
            Imprint = imprint;
        }
        
        [JsonConstructorAttribute]
        internal UpdateFolderWatermarkRequestBodyWatermarkField(StringEnum<UpdateFolderWatermarkRequestBodyWatermarkImprintField> imprint) {
            Imprint = UpdateFolderWatermarkRequestBodyWatermarkImprintField.Default;
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