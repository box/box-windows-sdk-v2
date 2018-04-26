﻿using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// There is an inconsistency in the events API where file sources have slightly different field names.
    /// </summary>
    public class BoxUserFolderCollaborationEventSource : BoxEntity
    {
        public const string FieldFolderId = "folder_id";
        public const string FieldFolderName = "folder_name";
        public const string FieldUserId = "user_id";
        public const string FieldUserName = "user_name";
        public const string FieldParent = "parent";

        /// <summary>
        /// The Id of the folder.
        /// </summary>
        [JsonProperty(PropertyName = FieldFolderId)]
        public override string Id { get; protected set; }

        public override string Type { get { return "folder"; } protected set { return; }}

        /// <summary>
        /// The name of the folder.
        /// </summary>
        [JsonProperty(PropertyName = FieldFolderName)]
        public string Name { get; private set; }

        /// <summary>
        /// The Id of the user.
        /// </summary>
        [JsonProperty(PropertyName = FieldUserId)]
        public string UserId { get; private set; }

        /// <summary>
        /// The name of the folder.
        /// </summary>
        [JsonProperty(PropertyName = FieldUserName)]
        public string UserName { get; private set; }

        /// <summary>
        /// The parent folder.
        /// </summary>
        [JsonProperty(PropertyName = FieldParent)]
        public BoxFolder Parent { get; private set; }
    }
}