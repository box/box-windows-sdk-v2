using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateSharedLinkOnWebLinkRequestBodySharedLinkField : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_ispasswordSet")]
        protected bool _isPasswordSet { get; set; }

        protected string? _password { get; set; }

        /// <summary>
        /// The level of access for the shared link. This can be
        /// restricted to anyone with the link (`open`), only people
        /// within the company (`company`) and only those who
        /// have been invited to the folder (`collaborators`).
        /// 
        /// If not set, this field defaults to the access level specified
        /// by the enterprise admin. To create a shared link with this
        /// default setting pass the `shared_link` object with
        /// no `access` field, for example `{ "shared_link": {} }`.
        /// 
        /// The `company` access level is only available to paid
        /// accounts.
        /// </summary>
        [JsonPropertyName("access")]
        [JsonConverter(typeof(StringEnumConverter<UpdateSharedLinkOnWebLinkRequestBodySharedLinkAccessField>))]
        public StringEnum<UpdateSharedLinkOnWebLinkRequestBodySharedLinkAccessField>? Access { get; init; }

        /// <summary>
        /// The password required to access the shared link. Set the
        /// password to `null` to remove it.
        /// Passwords must now be at least eight characters
        /// long and include a number, upper case letter, or
        /// a non-numeric or non-alphabetic character.
        /// A password can only be set when `access` is set to `open`.
        /// </summary>
        [JsonPropertyName("password")]
        public string? Password { get => _password; init { _password = value; _isPasswordSet = true; } }

        /// <summary>
        /// Defines a custom vanity name to use in the shared link URL,
        /// for example `https://app.box.com/v/my-shared-link`.
        /// 
        /// Custom URLs should not be used when sharing sensitive content
        /// as vanity URLs are a lot easier to guess than regular shared
        /// links.
        /// </summary>
        [JsonPropertyName("vanity_name")]
        public string? VanityName { get; init; }

        /// <summary>
        /// The timestamp at which this shared link will
        /// expire. This field can only be set by
        /// users with paid accounts. The value must be greater than the
        /// current date and time.
        /// </summary>
        [JsonPropertyName("unshared_at")]
        public System.DateTimeOffset? UnsharedAt { get; init; }

        [JsonPropertyName("permissions")]
        public UpdateSharedLinkOnWebLinkRequestBodySharedLinkPermissionsField? Permissions { get; init; }

        public UpdateSharedLinkOnWebLinkRequestBodySharedLinkField() {
            
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