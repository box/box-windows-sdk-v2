using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a folder lock in box
    /// </summary>
    public class BoxFolderLock : BoxEntity
    {
        public const string FieldCreatedAt = "created_at";
        public const string FieldCreatedBy = "created_by";
        public const string FieldFolder = "folder";
        public const string FieldLockType = "lock_type";
        public const string FieldLockedOperations = "locked_operations";

        /// <summary>
        /// The time the item was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// The user who created this item
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public BoxUser CreatedBy { get; private set; }

        /// <summary>
        /// The user who created this item
        /// </summary>
        [JsonProperty(PropertyName = FieldFolder)]
        public BoxFolder Folder { get; private set; }

        /// <summary>
        /// The lock type
        /// </summary>
        [JsonProperty(PropertyName = FieldLockType)]
        public string LockType { get; private set; }

        /// <summary>
        /// The operations locked by a folder lock
        /// </summary>
        [JsonProperty(PropertyName = FieldLockedOperations)]
        public BoxFolderLockOperations LockedOperations { get; private set; }
    }
}
