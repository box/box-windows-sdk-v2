using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxEntity
    {
        public const string FieldType = "type";
        public const string FieldId = "id";

        /// <summary>
        /// The folder’s ID
        /// </summary>
        [JsonProperty(PropertyName = FieldId)]
        public string Id { get; private set; }

        /// <summary>
        /// For file is 'file'
        /// For folders is ‘folder'
        /// For collaborations is 'collaboration'
        /// </summary>
        [JsonProperty(PropertyName = FieldType)]
        public string Type { get; private set; }
    }
}
