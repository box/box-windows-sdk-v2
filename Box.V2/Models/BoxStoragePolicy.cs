using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxStoragePolicy : BoxEntity
    {
        public const string FieldName = "name";

        /// <summary>
        /// The name of the Box Storage Policy.
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public virtual string Name { get; private set; }
    }
}
