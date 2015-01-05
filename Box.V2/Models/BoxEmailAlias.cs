using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// An email alias for a Box user's primary login
    /// </summary>
    public class BoxEmailAlias : BoxEntity
    {
        // Marked private as Type and ID are always returned with every response regardless of included Fields
        private const string FieldIsConfirmed = "is_confirmed";
        private const string FieldEmail = "email";

        /// <summary>
        /// Whether this alias has been confirmed by the user
        /// </summary>
        [JsonProperty(PropertyName = FieldIsConfirmed)]
        public bool IsConfirmed { get; private set; }

        /// <summary>
        /// The email address linked to this alias
        /// </summary>
        [JsonProperty(PropertyName = FieldEmail)]
        public string Email { get; private set; }
    }
}
