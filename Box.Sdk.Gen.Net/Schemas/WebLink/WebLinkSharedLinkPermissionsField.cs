using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class WebLinkSharedLinkPermissionsField : ISerializable {
        /// <summary>
        /// Defines if the shared link allows for the item to be downloaded. For
        /// shared links on folders, this also applies to any items in the folder.
        /// 
        /// This value can be set to `true` when the effective access level is
        /// set to `open` or `company`, not `collaborators`.
        /// </summary>
        [JsonPropertyName("can_download")]
        public bool CanDownload { get; }

        /// <summary>
        /// Defines if the shared link allows for the item to be previewed.
        /// 
        /// This value is always `true`. For shared links on folders this also
        /// applies to any items in the folder.
        /// </summary>
        [JsonPropertyName("can_preview")]
        public bool CanPreview { get; }

        /// <summary>
        /// Defines if the shared link allows for the item to be edited.
        /// 
        /// This value can only be `true` if `can_download` is also `true` and if
        /// the item has a type of `file`.
        /// </summary>
        [JsonPropertyName("can_edit")]
        public bool CanEdit { get; }

        public WebLinkSharedLinkPermissionsField(bool canDownload, bool canPreview, bool canEdit) {
            CanDownload = canDownload;
            CanPreview = canPreview;
            CanEdit = canEdit;
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