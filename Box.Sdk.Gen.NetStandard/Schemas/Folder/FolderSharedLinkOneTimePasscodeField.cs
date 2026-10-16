using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FolderSharedLinkOneTimePasscodeField : ISerializable {
        /// <summary>
        /// The current OTP enforcement mode for the shared link.
        /// 
        /// * `none` - OTP enforcement is turned off. The shared link is accessible
        ///   without any one-time-passcode verification.
        /// * `open` - Any anonymous recipient can request a one-time-passcode to
        ///   any email address and access the shared link after verifying it.
        ///   Collaborators and registered Box users access the link without
        ///   being prompted.
        /// * `allowlist` - Anonymous recipients can request a one-time-passcode
        ///   only when they enter an email that matches an entry on the
        ///   `allowlist`.
        /// </summary>
        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        /// <summary>
        /// The list of email and domain entries that are permitted to access the
        /// shared link when `mode` is `allowlist`. Anonymous users whose
        /// email matches an entry can request and verify a one-time-passcode.
        /// 
        /// Because the entries carry recipient email addresses and the identity
        /// of the user who added them, this field is only returned to users
        /// with owner, co-owner, or editor access on the item. For viewer
        /// collaborators and below the field is omitted.
        /// 
        /// An allowlist contains at most 1000 entries, matching the limit
        /// enforced when setting the allowlist on a shared link.
        /// </summary>
        [JsonPropertyName("allowlist")]
        public IReadOnlyList<SharedLinkOtpAllowlistEntry> Allowlist { get; set; }

        public FolderSharedLinkOneTimePasscodeField(string mode) {
            Mode = mode;
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