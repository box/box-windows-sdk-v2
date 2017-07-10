using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making file requests
    /// </summary>
    public class BoxFileRequest : BoxItemRequest
    {

        /// <summary>
        /// The time this file was created on the user’s machine.
        /// For more information about content times <see>http://developers.box.com/content-times/</see>
        /// NOTE: creation time MUST be the creation time from the file system. There will be issues with Sync otherwise
        /// </summary>
        [JsonProperty(PropertyName = "content_created_at")]
        public DateTime? ContentCreatedAt { get; set; }

        ///// <summary>
        ///// The time this file was last modified on the user’s machine.
        ///// For more information about content times <see>http://developers.box.com/content-times/"</see>
        ///// </summary>
        [JsonProperty(PropertyName = "content_modified_at")]
        public DateTime? ContentModifiedAt { get; set; }
    }
}
