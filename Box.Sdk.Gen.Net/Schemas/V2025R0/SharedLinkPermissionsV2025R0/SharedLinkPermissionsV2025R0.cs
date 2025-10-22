using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class SharedLinkPermissionsV2025R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isshared_links_optionSet")]
        protected bool _isSharedLinksOptionSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isdefault_shared_link_typeSet")]
        protected bool _isDefaultSharedLinkTypeSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isnotes_shared_link_optionSet")]
        protected bool _isNotesSharedLinkOptionSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isdefault_notes_shared_link_typeSet")]
        protected bool _isDefaultNotesSharedLinkTypeSet { get; set; }

        protected string? _sharedLinksOption { get; set; }

        protected string? _defaultSharedLinkType { get; set; }

        protected string? _notesSharedLinkOption { get; set; }

        protected string? _defaultNotesSharedLinkType { get; set; }

        /// <summary>
        /// The selected option for shared links permissions.
        /// </summary>
        [JsonPropertyName("shared_links_option")]
        public string? SharedLinksOption { get => _sharedLinksOption; init { _sharedLinksOption = value; _isSharedLinksOptionSet = true; } }

        /// <summary>
        /// The default shared link type.
        /// </summary>
        [JsonPropertyName("default_shared_link_type")]
        public string? DefaultSharedLinkType { get => _defaultSharedLinkType; init { _defaultSharedLinkType = value; _isDefaultSharedLinkTypeSet = true; } }

        /// <summary>
        /// The selected option for notes shared links permissions.
        /// </summary>
        [JsonPropertyName("notes_shared_link_option")]
        public string? NotesSharedLinkOption { get => _notesSharedLinkOption; init { _notesSharedLinkOption = value; _isNotesSharedLinkOptionSet = true; } }

        /// <summary>
        /// The default notes shared link type.
        /// </summary>
        [JsonPropertyName("default_notes_shared_link_type")]
        public string? DefaultNotesSharedLinkType { get => _defaultNotesSharedLinkType; init { _defaultNotesSharedLinkType = value; _isDefaultNotesSharedLinkTypeSet = true; } }

        public SharedLinkPermissionsV2025R0() {
            
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