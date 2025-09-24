using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxAssignmentCounts
    {
        public const string FieldUser = "user";
        public const string FieldFolder = "folder";
        public const string FieldFile = "file";
        public const string FileVersion = "file_version";

        /// <summary>
        /// Gets the count of users.
        /// </summary>
        [JsonProperty(PropertyName = FieldUser)]
        public virtual int User { get; private set; }

        /// <summary>
        /// Gets the count of folders.
        /// </summary>
        [JsonProperty(PropertyName = FieldFolder)]
        public virtual int Folder { get; private set; }

        /// <summary>
        /// Gets the count of files.
        /// </summary>
        [JsonProperty(PropertyName = FieldFile)]
        public virtual int File { get; private set; }

        /// <summary>
        /// Gets the count of versions.
        /// </summary>
        [JsonProperty(PropertyName = FileVersion)]
        public virtual int Version { get; private set; }
    }
}
