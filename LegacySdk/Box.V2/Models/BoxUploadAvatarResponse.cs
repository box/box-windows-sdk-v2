using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a upload avatar response
    /// </summary>
    public class BoxUploadAvatarResponse
    {
        public const string FieldPicUrls = "pic_urls";

        /// <summary>
        /// Property holding avatar Urls
        /// </summary>
        [JsonProperty(PropertyName = FieldPicUrls)]
        public virtual PicUrls PicUrls { get; private set; }
    }

    /// <summary>
    /// Box representation of a pic urls part of upload avatar response
    /// </summary>
    public class PicUrls
    {
        public const string FieldPreview = "preview";
        public const string FieldSmall = "small";
        public const string FieldLarge = "large";

        /// <summary>
        /// URL with the preview of Avatar
        /// </summary>
        [JsonProperty(PropertyName = FieldPreview)]
        public virtual string Preview { get; private set; }

        /// <summary>
        /// URL with the small representation of Avatar
        /// </summary>
        [JsonProperty(PropertyName = FieldSmall)]
        public virtual string Small { get; private set; }

        /// <summary>
        /// URL with the large representation of Avatar
        /// </summary>
        [JsonProperty(PropertyName = FieldLarge)]
        public virtual string Large { get; private set; }
    }
}
