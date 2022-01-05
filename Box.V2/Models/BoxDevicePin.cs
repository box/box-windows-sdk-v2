using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxDevicePin : BoxEntity
    {
        public const string FieldOwnedBy = "owned_by";
        public const string FieldProductName = "product_name";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";

        /// <summary>
        /// The user that the pin belongs to
        /// </summary>
        [JsonProperty(PropertyName = FieldOwnedBy)]
        public virtual BoxUser OwnedBy { get; private set; }

        /// <summary>
        /// The type of device being pinned
        /// </summary>
        [JsonProperty(PropertyName = FieldProductName)]
        public virtual string ProductName { get; set; }

        /// <summary>
        /// The time the device pin was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// The time the device pin was last modified
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public virtual DateTimeOffset? ModifiedAt { get; private set; }
    }
}
