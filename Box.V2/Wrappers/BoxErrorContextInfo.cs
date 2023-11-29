using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Box.V2
{
    /// <summary>
    /// A Box representation of a conflict error context.
    /// </summary>
    /// <typeparam name="T">The type conflict</typeparam>
    public class BoxConflictErrorContextInfo<T> where T : class
    {
        /// <summary>
        /// Gets or sets the conflicts.
        /// </summary>
        /// <value>The conflicts.</value>
        [JsonProperty(PropertyName = "conflicts")]
        //in case of copyFolder conflict object is returned instead of an array
        [JsonConverter(typeof(SingleOrCollectionConverter))]
        public Collection<T> Conflicts { get; set; }
    }

    /// <summary>
    /// A Box representation of a preflight check conflict error context.
    /// </summary>
    /// <typeparam name="T">The type conflict</typeparam>
    public class BoxPreflightCheckConflictErrorContextInfo<T> where T : class
    {
        /// <summary>
        /// Gets or sets the conflicts.
        /// </summary>
        /// <value>The conflicts.</value>
        [JsonProperty(PropertyName = "conflicts")]
        public T Conflict { get; set; }
    }


    internal class SingleOrCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jToken = serializer.Deserialize<JToken>(reader);

            return jToken is JArray ?
                jToken.ToObject(objectType, serializer) :
                new JArray(jToken).ToObject(objectType, serializer);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
