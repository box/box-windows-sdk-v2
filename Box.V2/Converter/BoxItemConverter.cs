using Box.V2.Config;
using Box.V2.Models;
using Box.V2.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Box.V2.Converter
{
    internal class BoxItemConverter : JsonCreationConverter<BoxEntity>
    {
        const string ItemType = "type";
        const string EventSourceItemType = "item_type";
        const string WatermarkType = "watermark";

        protected override BoxEntity Create(Type objectType, JObject jObject)
        {
            if (FieldExists(ItemType, jObject))
            {
                switch (jObject[ItemType].ToString())
                {
                    case Constants.TypeFile:
                        return new BoxFile();
                    case Constants.TypeFolder:
                        return new BoxFolder();
                    case Constants.TypeWebLink:
                        return new BoxWebLink();
                    case Constants.TypeRetentionPolicy:
                        return new BoxRetentionPolicy();
                    case Constants.TypeRetentionPolicyAssignment:
                        return new BoxRetentionPolicyAssignment();
                    case Constants.TypeFileVersionRetention:
                        return new BoxFileVersionRetention();
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
                    case Constants.TypeLock:
                        return new BoxFileLock();
                    case Constants.TypeInvite:
                        return new BoxUserInvite();
                    case Constants.TypeWebhook:
                        return new BoxWebhook();
                    case Constants.TypeTask:
                        return new BoxTask();
                    case Constants.TypeEmailAlias:
                        return new BoxEmailAlias();
                    case Constants.TypeTaskAssignment:
                        return new BoxTaskAssignment();
                    case Constants.TypeCollection:
                        return new BoxCollectionItem();
                    case Constants.TypeDevicePin:
                        return new BoxDevicePin();
                    case Constants.TypeLegalHoldPolicy:
                        return new BoxLegalHoldPolicy();
                    case Constants.TypeLegalHoldPolicyAssignment:
                        return new BoxLegalHoldPolicyAssignment();
                }
            }
            //There is an inconsistency in the events API where file sources have slightly different field names
            else if (FieldExists(EventSourceItemType, jObject))
            {
                switch (jObject[EventSourceItemType].ToString())
                {
                    case Constants.TypeFile:
                        return new BoxFileEventSource();
                }
            }
            else if (FieldExists(WatermarkType, jObject))
            {
                return new BoxWatermarkResponse();
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
            return CrossPlatform.CanConvert<T>(objectType);
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
