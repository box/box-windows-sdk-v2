using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateFileUploadSessionCommitByUrlRequestBody : ISerializable {
        /// <summary>
        /// The list details for the uploaded parts.
        /// 
        /// Each entry can include `sha1` or `sha512`. Include `sha512`
        /// on every entry or on none of them. A request where only
        /// some entries include `sha512` is rejected.
        /// 
        /// Include parts that the server already has, returned as
        /// `hits` by the [`Plan upload session`](https://developer.box.com/reference/post-files-upload-sessions-id-plan)
        /// endpoint. Use the `part_id`, `offset`, and `size` from the
        /// plan response. If the other entries include `sha512`,
        /// include the `sha512` value from the plan response.
        /// Otherwise, leave the hash out of these entries.
        /// </summary>
        [JsonPropertyName("parts")]
        public IReadOnlyList<UploadPart> Parts { get; set; }

        public CreateFileUploadSessionCommitByUrlRequestBody(IReadOnlyList<UploadPart> parts) {
            Parts = parts;
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