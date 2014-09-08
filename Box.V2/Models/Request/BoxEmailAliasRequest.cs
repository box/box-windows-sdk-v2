using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// A class for adding email aliases
    /// </summary>
    public class BoxEmailAliasRequest : BoxRequestEntity
    {
        // Marked private as Type and ID are always returned with every response regardless of included Fields
        private const string FieldEmail = "email";

        /// <summary>
        /// The user to alias
        /// </summary>
        public BoxRequestEntity User { get; set; }

        /// <summary>
        /// The email alias to add
        /// </summary>
        [JsonProperty(PropertyName = FieldEmail)]
        public string Email { get; set; }
    }
}