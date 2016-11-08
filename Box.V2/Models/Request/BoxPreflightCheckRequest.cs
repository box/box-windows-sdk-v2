using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making preflight check requests
    /// 
    /// Required fields:
    /// name: The name of the file to be uploaded.
    /// parent.id: The parent folder id of this file.
    /// size: The size of the file in bytes. Specify 0 for unknown file-sizes.
    /// </summary>
    public class BoxPreflightCheckRequest : BoxItemRequest
    {
        /// <summary>
        /// The size of the file in bytes. Specify 0 for unknown file-sizes
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public long Size { get; set; }
    }
}
