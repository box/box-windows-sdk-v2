using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxFileRequest : BoxRequestEntity
    {
        /// <summary>
        /// The folder that contains this file
        /// </summary>
        [JsonProperty(PropertyName = "parent")]
        public BoxRequestEntity Parent { get; set; }

        /// <summary>
        /// The name of the file 
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }


        /// <summary>
        /// The time this file was created on the user’s machine.
        /// <see cref="http://developers.box.com/content-times/"/>
        /// </summary>
        //[JsonProperty(PropertyName = "content_created_at")]
        //public DateTime? ContentCreatedAt { get; set; }

        ///// <summary>
        ///// The time this file was last modified on the user’s machine.
        ///// <see cref="http://developers.box.com/content-times/"/>
        ///// </summary>
        //[JsonProperty(PropertyName = "content_modified_at")]
        //public DateTime? ContentModifiedAt { get; set; }
    }
}
