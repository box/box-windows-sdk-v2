using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box mini representation of a enterprise
    /// </summary>
    public class BoxEnterprise : BoxEntity
    {
        public const string FieldName = "name";

        /// <summary>
        /// The name of this enterprise
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public virtual string Name { get; private set; }
    }
}
