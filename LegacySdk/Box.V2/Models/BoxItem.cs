using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of an item in box
    /// </summary>
    public class BoxItem : BoxEntity
    {
        public const string FieldSequence = "sequence_id";
        public const string FieldEtag = "etag";
        public const string FieldName = "name";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";
        public const string FieldTrashedAt = "trashed_at";
        public const string FieldDescription = "description";
        public const string FieldSize = "size";
        public const string FieldPathCollection = "path_collection";
        public const string FieldCreatedBy = "created_by";
        public const string FieldModifiedBy = "modified_by";
        public const string FieldOwnedBy = "owned_by";
        public const string FieldSharedLink = "shared_link";
        public const string FieldParent = "parent";
        public const string FieldItemStatus = "item_status";
        public const string FieldPermissions = "permissions";
        public const string FieldTags = "tags";

        /// <summary>
        /// A unique ID for use with the /events endpoint
        /// </summary>
        [JsonProperty(PropertyName = FieldSequence)]
        public virtual string SequenceId { get; private set; }

        /// <summary>
        /// A unique string identifying the version of this item
        /// </summary>
        [JsonProperty(PropertyName = FieldEtag)]
        public virtual string ETag { get; private set; }

        /// <summary>
        /// The name of the item
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public virtual string Name { get; private set; }

        /// <summary>
        /// The description of the item
        /// </summary>
        [JsonProperty(PropertyName = FieldDescription)]
        public virtual string Description { get; private set; }

        /// <summary>
        /// The folder size in bytes
        /// </summary>
        [JsonProperty(PropertyName = FieldSize)]
        public virtual long? Size { get; private set; }

        /// <summary>
        /// The path of folders to this item, starting at the root
        /// </summary>
        [JsonProperty(PropertyName = FieldPathCollection)]
        public virtual BoxCollection<BoxFolder> PathCollection { get; private set; }

        /// <summary>
        /// The time the item was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// The time the item or its contents were last modified
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public virtual DateTimeOffset? ModifiedAt { get; private set; }

        /// <summary>
        /// The time at which this item was put in the trash.
        /// </summary>
        [JsonProperty(PropertyName = FieldTrashedAt)]
        public virtual DateTimeOffset? TrashedAt { get; set; }

        /// <summary>
        /// The user who created this item
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public virtual BoxUser CreatedBy { get; private set; }

        /// <summary>
        /// The user who last modified this item
        /// mini user object
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedBy)]
        public virtual BoxUser ModifiedBy { get; private set; }

        /// <summary>
        /// The user who owns this item
        /// mini user object
        /// </summary>
        [JsonProperty(PropertyName = FieldOwnedBy)]
        public virtual BoxUser OwnedBy { get; private set; }

        /// <summary>
        /// The folder that contains this one
        /// </summary>
        [JsonProperty(PropertyName = FieldParent)]
        public virtual BoxFolder Parent { get; private set; }

        /// <summary>
        /// Whether this item is deleted or not
        /// </summary>
        [JsonProperty(PropertyName = FieldItemStatus)]
        public virtual string ItemStatus { get; private set; }

        /// <summary>
        /// The shared link for this item
        /// </summary>
        [JsonProperty(PropertyName = FieldSharedLink)]
        public virtual BoxSharedLink SharedLink { get; private set; }

        /// <summary>
        /// The tag for this item
        /// </summary>
        [JsonProperty(PropertyName = FieldTags)]
        public virtual string[] Tags { get; private set; }
    }
}
