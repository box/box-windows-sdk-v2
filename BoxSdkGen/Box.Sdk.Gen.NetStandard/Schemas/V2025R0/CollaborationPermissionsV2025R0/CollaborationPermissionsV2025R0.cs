using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class CollaborationPermissionsV2025R0 : ISerializable {
        /// <summary>
        /// The co-owner role is enabled for collaboration.
        /// </summary>
        [JsonPropertyName("is_co_owner_role_enabled")]
        public bool? IsCoOwnerRoleEnabled { get; set; }

        /// <summary>
        /// The editor role is enabled for collaboration.
        /// </summary>
        [JsonPropertyName("is_editor_role_enabled")]
        public bool? IsEditorRoleEnabled { get; set; }

        /// <summary>
        /// The previewer role is enabled for collaboration.
        /// </summary>
        [JsonPropertyName("is_previewer_role_enabled")]
        public bool? IsPreviewerRoleEnabled { get; set; }

        /// <summary>
        /// The previewer uploader role is enabled for collaboration.
        /// </summary>
        [JsonPropertyName("is_previewer_uploader_role_enabled")]
        public bool? IsPreviewerUploaderRoleEnabled { get; set; }

        /// <summary>
        /// The uploader role is enabled for collaboration.
        /// </summary>
        [JsonPropertyName("is_uploader_role_enabled")]
        public bool? IsUploaderRoleEnabled { get; set; }

        /// <summary>
        /// The viewer role is enabled for collaboration.
        /// </summary>
        [JsonPropertyName("is_viewer_role_enabled")]
        public bool? IsViewerRoleEnabled { get; set; }

        /// <summary>
        /// The viewer uploader role is enabled for collaboration.
        /// </summary>
        [JsonPropertyName("is_viewer_uploader_role_enabled")]
        public bool? IsViewerUploaderRoleEnabled { get; set; }

        public CollaborationPermissionsV2025R0() {
            
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}