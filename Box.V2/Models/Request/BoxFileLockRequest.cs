using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making file lock requests
    /// </summary>
    public class BoxFileLockRequest
    {
        /// <summary>
        /// The lock object
        /// </summary>
        [JsonProperty(PropertyName = "lock")]
        public BoxFileLock Lock { get; set; }
    }
}
