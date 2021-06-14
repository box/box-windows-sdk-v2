using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxTermsOfService : BoxEntity
    {
        public const string FieldStatus = "status";
        public const string FieldEnterprise = "enterprise";
        public const string FieldTosType = "tos_type";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";
        public const string FieldText = "text";

        /// <summary>
        /// The status of the terms of service object
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public virtual String Status { get; set; }

        /// <summary>
        /// The enterprise the terms of service object is associated with
        /// </summary>
        [JsonProperty(PropertyName = FieldEnterprise)]
        public virtual BoxEnterprise Enterprise { get; set; }

        /// <summary>
        /// The type of the terms of service object
        /// </summary>
        [JsonProperty(PropertyName = FieldTosType)]
        public virtual String TosType { get; set; }

        /// <summary>
        /// The text description of the terms of service object
        /// </summary>
        [JsonProperty(PropertyName = FieldText)]
        public virtual String Text { get; set; }

        /// <summary>
        /// The time this terms of service was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// The time this terms of service was modified
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public virtual DateTime? ModifiedAt { get; private set; }
    }
}
