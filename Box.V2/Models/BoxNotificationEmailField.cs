using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// This is a request representation for Notification Email
    /// </summary>
    public class BoxNotificationEmailField
    {
        public const string email = "email";

        [JsonProperty(PropertyName = email)]
        public virtual string Email { get; set; }
    }
}
