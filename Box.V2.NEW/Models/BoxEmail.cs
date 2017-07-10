using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of an email
    /// </summary>
    public class BoxEmail
    {
        public const string FieldAccess = "access";
        public const string FieldEmail = "email";

        /// <summary>
        /// The available access
        /// </summary>
        [JsonProperty(PropertyName = FieldAccess)]
        public string Acesss { get; private set; }

        /// <summary>
        /// The email address
        /// </summary>
        [JsonProperty(PropertyName = FieldEmail)]
        public string Address { get; private set; }
    }
}
