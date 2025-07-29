using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateFileByIdRequestBodyPermissionsField : ISerializable {
        /// <summary>
        /// Defines who is allowed to download this file. The possible
        /// values are either `open` for everyone or `company` for
        /// the other members of the user's enterprise.
        /// 
        /// This setting overrides the download permissions that are
        /// normally part of the `role` of a collaboration. When set to
        /// `company`, this essentially removes the download option for
        /// external users with `viewer` or `editor` a roles.
        /// </summary>
        [JsonPropertyName("can_download")]
        [JsonConverter(typeof(StringEnumConverter<UpdateFileByIdRequestBodyPermissionsCanDownloadField>))]
        public StringEnum<UpdateFileByIdRequestBodyPermissionsCanDownloadField>? CanDownload { get; init; }

        public UpdateFileByIdRequestBodyPermissionsField() {
            
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