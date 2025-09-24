using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// The sole purpose of this class is to allow passing null as shared_link property in order to delete a shared link
    /// </summary>
    public class BoxDeleteSharedLinkRequest : BoxRequestEntity
    {
        /// <summary>
        /// Allow passing of null to remove the shared link
        /// </summary>
        [JsonProperty(PropertyName = "shared_link", NullValueHandling = NullValueHandling.Include)]
        public BoxSharedLinkRequest SharedLink { get; set; }
    }

}
