using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Box.V2
{
    /// <summary>
    /// A Box representation of an error context.
    /// </summary>
    /// <typeparam name="TModel">The type of the t model.</typeparam>
    public class BoxErrorContextInfo<TModel> where TModel : class
    {
        /// <summary>
        /// Gets or sets the conflicts.
        /// </summary>
        /// <value>The conflicts.</value>
        [JsonProperty(PropertyName = "conflicts")]
        public Collection<TModel> Conflicts { get; set; }
    }
}
