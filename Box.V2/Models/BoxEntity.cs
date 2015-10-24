using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Represents the base class for most Box model objects
    /// </summary>
    public class BoxEntity
    {
        // Marked private as Type and ID are always returned with every response regardless of included Fields
        private const string FieldType = "type";
        private const string FieldId = "id";

        /// <summary>
        /// The folder’s ID
        /// </summary>
        [JsonProperty(PropertyName = FieldId)]
        public string Id { get; private set; }

        /// <summary>
        /// For file is 'file'
        /// For folders is ‘folder'
        /// For collaborations is 'collaboration'
        /// For enterprise is 'enterprise'
        /// </summary>
        [JsonProperty(PropertyName = FieldType)]
        public string Type { get; private set; }
    }
}
