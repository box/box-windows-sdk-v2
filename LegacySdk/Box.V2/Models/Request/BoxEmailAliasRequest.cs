using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making email alias requests
    /// </summary>
    public class BoxEmailAliasRequest
    {
        private const string FieldEmail = "email";

        [JsonProperty(PropertyName = FieldEmail)]
        public string Email { get; set; }
    }
}
