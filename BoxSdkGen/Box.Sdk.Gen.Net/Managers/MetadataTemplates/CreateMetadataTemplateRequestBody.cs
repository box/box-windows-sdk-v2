using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateMetadataTemplateRequestBody : ISerializable {
        /// <summary>
        /// The scope of the metadata template to create. Applications can
        /// only create templates for use within the authenticated user's
        /// enterprise.
        /// 
        /// This value needs to be set to `enterprise`, as `global` scopes can
        /// not be created by applications.
        /// </summary>
        [JsonPropertyName("scope")]
        public string Scope { get; }

        /// <summary>
        /// A unique identifier for the template. This identifier needs to be
        /// unique across the enterprise for which the metadata template is
        /// being created.
        /// 
        /// When not provided, the API will create a unique `templateKey`
        /// based on the value of the `displayName`.
        /// </summary>
        [JsonPropertyName("templateKey")]
        public string? TemplateKey { get; init; }

        /// <summary>
        /// The display name of the template.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; }

        /// <summary>
        /// Defines if this template is visible in the Box web app UI, or if
        /// it is purely intended for usage through the API.
        /// </summary>
        [JsonPropertyName("hidden")]
        public bool? Hidden { get; init; }

        /// <summary>
        /// An ordered list of template fields which are part of the template.
        /// Each field can be a regular text field, date field, number field,
        /// as well as a single or multi-select list.
        /// </summary>
        [JsonPropertyName("fields")]
        public IReadOnlyList<CreateMetadataTemplateRequestBodyFieldsField>? Fields { get; init; }

        /// <summary>
        /// Whether or not to copy any metadata attached to a file or folder
        /// when it is copied. By default, metadata is not copied along with a
        /// file or folder when it is copied.
        /// </summary>
        [JsonPropertyName("copyInstanceOnItemCopy")]
        public bool? CopyInstanceOnItemCopy { get; init; }

        public CreateMetadataTemplateRequestBody(string scope, string displayName) {
            Scope = scope;
            DisplayName = displayName;
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