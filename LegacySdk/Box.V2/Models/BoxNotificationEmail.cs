using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a notification email object.
    /// </summary>
    public class BoxNotificationEmail : BoxNotificationEmailField
    {
        public const string isConfirmed = "is_confirmed";

        [JsonProperty(PropertyName = isConfirmed)]
        public virtual bool? IsConfirmed { get; private set; }
    }
}
