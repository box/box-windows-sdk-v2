using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public string Name { get; private set; }

        [JsonProperty(PropertyName = FieldCollectionType)]
        public string CollectionType { get; private set; }
    }
}
