using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a file
    /// </summary>
    public class BoxFile : BoxItem
    {
        public const string FieldSha1 = "sha1";
        public const string FieldTrashedAt = "trashed_at";
        public const string FieldPurgedAt = "purged_at";
        public const string FieldContentCreatedAt = "content_created_at";
        public const string FieldContentModifiedAt = "content_modified_at";
        public const string FieldVersionNumber = "version_number";
        public const string FieldCommentCount = "comment_count";

        /// <summary>
        /// The sha1 hash of this file
        /// </summary>
        [JsonProperty(PropertyName = FieldSha1)]
        public string Sha1 { get; private set; }

        /// <summary>
        /// When this file was last moved to the trash
        /// </summary>
        [JsonProperty(PropertyName = FieldTrashedAt)]
        public DateTime? TrashedAt { get; private set; }

        /// <summary>
        /// When this file will be permanently deleted
        /// </summary>
        [JsonProperty(PropertyName = FieldPurgedAt)]
        public DateTime? PurgedAt { get; private set; }

        /// <summary>
        /// When the content of this file was created
        /// For more information about content times <see>http://developers.box.com/content-times/</see>
        /// </summary>
        [JsonProperty(PropertyName = FieldContentCreatedAt)]
        public DateTime? ContentCreatedAt { get; private set; }

        /// <summary>
        /// When the content of this file was last modified
        /// For more information about content times <see>http://developers.box.com/content-times/</see>
        /// </summary>
        [JsonProperty(PropertyName = FieldContentModifiedAt)]
        public DateTime? ContentModifiedAt { get; private set; }

        /// <summary>
        /// The version of the file
        /// </summary>
        [JsonProperty(PropertyName = FieldVersionNumber)]
        public string VersionNumber { get; private set; }

        /// <summary>
        /// The number of comments on a file
        /// </summary>
        [JsonProperty(PropertyName = FieldCommentCount)]
        public int CommentCount { get; private set; }

        /// <summary>
        /// The available permissions on this file
        /// </summary>
        [JsonProperty(PropertyName = FieldPermissions)]
        public BoxFilePermission Permissions { get; protected set; }
    }
}
