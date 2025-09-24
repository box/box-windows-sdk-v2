using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxCollectionItem : BoxEntity
    {
        private const string FieldName = "name";
        private const string FieldCollectionType = "collection_type";
        /// <summary>
        /// The name of the item
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public virtual string Name { get; private set; }

        [JsonProperty(PropertyName = FieldCollectionType)]
        public virtual string CollectionType { get; private set; }
    }
}
