using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Represents a conflict that occurs between items that have the same name.
    /// </summary>
    public class BoxZipConflict
    {
        /// <summary>
        /// The items that have a conflict
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public virtual List<BoxZipConflictItem> items { get; private set; }
    }
}

