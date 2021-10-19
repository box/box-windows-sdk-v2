using System;
using Newtonsoft.Json;

namespace Box.V2.Models.Request
{
    public class BoxFileVersionRetentionRequest
    {
        public BoxFileVersionRetentionRequest()
        {
            Limit = 100;
        }

        /// <summary>
        /// A file id to filter the file version retentions by.
        /// </summary>
        [JsonProperty(PropertyName = "file_id")]
        public string FileId { get; set; }

        /// <summary>
        /// A file version id to filter the file version retentions by.
        /// </summary>
        [JsonProperty(PropertyName = "file_version_id")]
        public string FileVersionId { get; set; }

        /// <summary>
        /// A policy id to filter the file version retentions by.
        /// </summary>
        [JsonProperty(PropertyName = "policy_id")]
        public string PolicyId { get; set; }

        /// <summary>
        /// The disposition action of the retention policy. This action can be permanently_delete, which will cause the content retained by the policy to be permanently deleted, or remove_retention, which will lift the retention policy from the content, allowing it to be deleted by users, once the retention policy time period has passed.
        /// </summary>
        [JsonProperty(PropertyName = "disposition_action")]
        public string DispositionAction { get; set; }

        [JsonProperty(PropertyName = "disposition_before")]
        public DateTimeOffset? DispositionBefore { get; set; }

        [JsonProperty(PropertyName = "disposition_after")]
        public DateTimeOffset? DispositionAfter { get; set; }

        /// <summary>
        /// The maximum number of items to return in a page
        /// </summary>
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Base 64 encoded string that represents where the paging should being. It should be left blank to begin paging.
        /// </summary>
        [JsonProperty(PropertyName = "marker")]
        public string Marker { get; set; }
    }
}
