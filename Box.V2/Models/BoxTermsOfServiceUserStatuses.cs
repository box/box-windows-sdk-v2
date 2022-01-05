using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxTermsOfServiceUserStatuses : BoxEntity
    {
        public const string FieldTos = "tos";
        public const string FieldUser = "user";
        public const string FieldIsAccepted = "is_accepted";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";

        /// <summary>
        /// The Terms of Service object
        /// </summary>
        [JsonProperty(PropertyName = FieldTos)]
        public virtual BoxEntity TermsOfService { get; set; }

        /// <summary>
        /// The Box user this Terms of Service is associated with
        /// </summary>
        [JsonProperty(PropertyName = FieldUser)]
        public virtual BoxUser User { get; set; }

        /// <summary>
        /// The acceptance status of the Terms of Service object
        /// </summary>
        [JsonProperty(PropertyName = FieldIsAccepted)]
        public virtual bool IsAccepted { get; set; }

        /// <summary>
        /// The time this terms of service was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// The time this terms of service was modified
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public virtual DateTimeOffset? ModifiedAt { get; private set; }
    }
}
