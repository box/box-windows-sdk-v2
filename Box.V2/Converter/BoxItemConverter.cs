using System;
using Box.V2.Config;
using Box.V2.Models;
using Box.V2.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Box.V2.Converter
{
    internal class BoxItemConverter : JsonCreationConverter<BoxEntity>
    {
        protected const string ItemType = "type";
        private const string EventSourceItemType = "item_type";
        private const string WatermarkType = "watermark";
        private const string GroupId = "group_id";
        private const string UserId = "user_id";
        private const string FolderId = "folder_id";
        private const string FileId = "file_id";

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
                    case Constants.TypeUploadSession:
                        return new BoxFileUploadSession();
                    case Constants.TypeRecentItem:
                        return new BoxRecentItem();
                    case Constants.TypeCollabWhitelistEntry:
                        return new BoxCollaborationWhitelistEntry();
                    case Constants.TypeCollabWhitelistTargetEntry:
                        return new BoxCollaborationWhitelistTargetEntry();
                    case Constants.TypeMetadataTemplate:
                        return new BoxMetadataTemplate();
                    case Constants.TypeTermsOfService:
                        return new BoxTermsOfService();
                    case Constants.TypeTermsOfServiceUserStatuses:
                        return new BoxTermsOfServiceUserStatuses();
                    case Constants.TypeMetadataCascadePolicy:
                        return new BoxMetadataCascadePolicy();
                    case Constants.TypeStoragePolicy:
                        return new BoxStoragePolicy();
                    case Constants.TypeStoragePolicyAssignment:
                        return new BoxStoragePolicyAssignment();
                    case Constants.TypeApplication:
                        return new BoxApplication();
                    case Constants.TypeFolderLock:
                        return new BoxFolderLock();
                    case Constants.TypeSignRequest:
                        return new BoxSignRequest();
                    case Constants.TypeSignTemplate:
                        return new BoxSignTemplate();
                    case Constants.TypeFileRequest:
                        return new BoxFileRequestObject();
                }
            }
            //There is an inconsistency in the events API where file sources have slightly different field names
            else if (FieldExists(EventSourceItemType, jObject))
            {
                switch (jObject[EventSourceItemType].ToString())
                {
                    case Constants.TypeFile:
                        return new BoxFileEventSource();
                    case Constants.TypeFolder:
                        return new BoxFolderEventSource();
                    case Constants.TypeWebLink:
                        return new BoxWebLinkEventSource();
                }
            }
            else if (FieldExists(WatermarkType, jObject))
            {
                return new BoxWatermarkResponse();
            }
            else if (FieldExists(GroupId, jObject))
            {
                if (FieldExists(FolderId, jObject))
                {
                    return new BoxGroupFolderCollaborationEventSource();
                }
                else if (FieldExists(FileId, jObject))
                {
                    return new BoxGroupFileCollaborationEventSource();
                }
                else
                {
                    return new BoxGroupEventSource();
                }
            }
            else if (FieldExists(UserId, jObject) && FieldExists(FileId, jObject))
            {
                return new BoxUserFileCollaborationEventSource();
            }
            else if (FieldExists(UserId, jObject) && FieldExists(FolderId, jObject))
            {
                return new BoxUserFolderCollaborationEventSource();
            }

            return new BoxEntity();
        }

        protected bool FieldExists(string fieldName, JObject jObject)
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
            var jObject = JObject.Load(reader);
            // The notification_email field for the user object is an object when a value exists and is an empty array when it isn't. The code below converts the empty array to a null value to avoid deserialization issues.  
            if (jObject["type"] != null && jObject["type"].ToString() == "user" && jObject["notification_email"] != null && jObject["notification_email"].Type == JTokenType.Array)
            {
                jObject["notification_email"] = null;
            }

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
