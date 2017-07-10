using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Box.V2.Models;

namespace Box.V2
{
    /// <summary>
    /// A Box representation of a conflict error context.
    /// </summary>
    /// <typeparam name="T">The type conflict</typeparam>
    public class BoxConflictErrorContextInfo<T> where T : class
    {
        /// <summary>
        /// Gets or sets the conflicts.
        /// </summary>
        /// <value>The conflicts.</value>
        [JsonProperty(PropertyName = "conflicts")]
        public Collection<T> Conflicts { get; set; }
    }

    /// <summary>
    /// A Box representation of a preflight check conflict error context.
    /// </summary>
    /// <typeparam name="T">The type conflict</typeparam>
    public class BoxPreflightCheckConflictErrorContextInfo<T> where T : class
    {
        /// <summary>
        /// Gets or sets the conflicts.
        /// </summary>
        /// <value>The conflicts.</value>
        [JsonProperty(PropertyName = "conflicts")]
        public T Conflict { get; set; }
    }
}
