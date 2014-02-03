using Box.V2.Config;
using Box.V2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Converter
{
    internal class BoxItemConverter : JsonCreationConverter<BoxEntity>
    {
        const string ItemType = "type";

        protected override BoxEntity Create(Type objectType, JObject jObject)
        {
            if (FieldExists(ItemType, jObject))
            {
                switch(jObject[ItemType].ToString())
                {
                    case Constants.TypeFile:
                        return new BoxFile();
                    case Constants.TypeFolder:
                        return new BoxFolder();
                    case Constants.TypeWebLink:
                        return new BoxWebLink();
                    case Constants.TypeComment:
                        return new BoxComment();
                    case Constants.TypeFileVersion:
                        return new BoxFileVersion();
                    case Constants.TypeGroup:
                        return new BoxGroup();
                    case Constants.TypeGroupMembership:
                        return new BoxGroupMembership();
                    case Constants.TypeUser:
                        return new BoxUser();
                    case Constants.TypeEnterprise:
                        return new BoxEnterprise();
                    case Constants.TypeCollaboration:
                        return new BoxCollaboration();
                }
            }
            return new BoxEntity();
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }
    
    internal abstract class JsonCreationConverter<T> : JsonConverter
    {
        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jObject">contents of JSON object that will be deserialized</param>
        /// <returns></returns>
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Null check so that the JObject.Load doesn't throw an exception
            if (reader.TokenType == JsonToken.Null)
                return null;

            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            T target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
