using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxFile : BoxItem
    {
        /// <summary>
        /// The sha1 hash of this file
        /// </summary>
        [JsonProperty(PropertyName = "sha1")]
        public string Sha1 { get; private set; }

        /// <summary>
        /// When this file was last moved to the trash
        /// </summary>
        [JsonProperty(PropertyName = "trashed_at")]
        public DateTime? TrashedAt { get; private set; }

        /// <summary>
        /// When this file will be permanently deleted
        /// </summary>
        [JsonProperty(PropertyName = "purged_at")]
        public DateTime? PurgedAt { get; private set; }

        /// <summary>
        /// When the content of this file was created
        /// <see cref="http://developers.box.com/content-times/"/> 
        /// </summary>
        [JsonProperty(PropertyName = "content_created_at")]
        public DateTime? ContentCreatedAt { get; private set; }

        /// <summary>
        /// When the content of this file was last modified
        /// <see cref="http://developers.box.com/content-times/"/>
        /// </summary>
        [JsonProperty(PropertyName = "content_modified_at")]
        public DateTime? ContentModifiedAt { get; private set; }
    }
}
