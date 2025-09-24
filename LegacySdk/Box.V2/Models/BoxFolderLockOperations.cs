using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a operations that have been locked on a folder lock in box
    /// </summary>
    public class BoxFolderLockOperations
    {
        public const string FieldDelete = "delete";
        public const string FieldMove = "move";

        /// <summary>
        /// Whether deleting the folder is restricted
        /// </summary>
        [JsonProperty(PropertyName = FieldDelete)]
        public virtual bool Delete { get; private set; }

        /// <summary>
        /// Whether deleting the folder is restricted
        /// </summary>
        [JsonProperty(PropertyName = FieldMove)]
        public virtual bool Move { get; private set; }
    }
}
