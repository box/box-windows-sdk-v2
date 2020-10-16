using System;
using System.Collections.Generic;
using Box.V2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Box.V2.Converter
{
    internal class BoxZipConflictConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            List<BoxZipConflict> conflicts = new List<BoxZipConflict>();

            // Load JObject from stream
            JArray conflictsArray = JArray.Load(reader);
            foreach (JArray conflict in conflictsArray)
            {
                BoxZipConflict zipConflict = new BoxZipConflict();
                JObject conflictObject = new JObject();
                conflictObject.Add("items", conflict);
                // Populate the object properties
                serializer.Populate(conflictObject.CreateReader(), zipConflict);
                conflicts.Add(zipConflict);
            }

            return conflicts;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
