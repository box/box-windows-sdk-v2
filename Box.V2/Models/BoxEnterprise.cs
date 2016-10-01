using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// Box mini representation of a enterprise
    /// </summary>
    public class BoxEnterprise : BoxEntity
    {
        public const string FieldName = "name";
        public const string FieldId = "id";

        /// <summary>
        /// The name of this enterprise
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public string Name { get; private set; }

        /// <summary>
        /// The id of this enterprise
        /// </summary>
        [JsonProperty(PropertyName = FieldId)]
        public string Id { get; private set; }
    }
}
