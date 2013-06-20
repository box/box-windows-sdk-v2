using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxFileRequest : BoxItemRequest
    {

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
