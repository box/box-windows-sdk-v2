using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of an application.
    /// </summary>
    public class BoxApplication : BoxEntity
    {
        public const string FieldName = "name";
        public const string FieldApiKey = "api_key";

        /// <summary>
        /// The name of this application.
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public virtual String Name { get; private set; }

        /// <summary>
        /// The API key of this application.
        /// </summary>
        [JsonProperty(PropertyName = FieldApiKey)]
        public virtual String ApiKey { get; private set; }
    }
}
