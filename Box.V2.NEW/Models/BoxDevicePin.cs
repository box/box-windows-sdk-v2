using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public BoxUser OwnedBy { get; private set; }

        /// <summary>
        /// The type of device being pinned
        /// </summary>
        [JsonProperty(PropertyName = FieldProductName)]
        public string ProductName { get; set; }

        /// <summary>
        /// The time the device pin was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// The time the device pin was last modified
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public DateTime? ModifiedAt { get; private set; }
    }
}
