using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxWatermark
    {
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";

        /// <summary>
        /// The time this watermark was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// The time this watermark was modified
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public virtual DateTimeOffset? ModifiedAt { get; private set; }
    }
}
