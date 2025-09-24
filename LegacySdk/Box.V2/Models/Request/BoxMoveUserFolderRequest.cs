using Newtonsoft.Json;

namespace Box.V2.Models.Request
{
    public class BoxMoveUserFolderRequest
    {
        public const string FieldOwnedBy = "owned_by";

        [JsonProperty(PropertyName = FieldOwnedBy)]
        public BoxUserRequest OwnedBy { get; set; }
    }
}
