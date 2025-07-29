using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class AddShareLinkToFolderRequestBodySharedLinkPermissionsField : ISerializable {
        /// <summary>
        /// If the shared link allows for downloading of files.
        /// This can only be set when `access` is set to
        /// `open` or `company`.
        /// </summary>
        [JsonPropertyName("can_download")]
        public bool? CanDownload { get; init; }

        /// <summary>
        /// If the shared link allows for previewing of files.
        /// This value is always `true`. For shared links on folders
        /// this also applies to any items in the folder.
        /// </summary>
        [JsonPropertyName("can_preview")]
        public bool? CanPreview { get; init; }

        /// <summary>
        /// This value can only be `false` for items
        /// with a `type` of `folder`.
        /// </summary>
        [JsonPropertyName("can_edit")]
        public bool? CanEdit { get; init; }

        public AddShareLinkToFolderRequestBodySharedLinkPermissionsField() {
            
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