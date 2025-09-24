using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxRestrictedTo
    {
        private const string FieldScope = "scope";
        private const string FieldObject = "object";

        /// <summary>
        /// Gets or sets the scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        [JsonProperty(PropertyName = FieldScope)]
        public virtual string Scope
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets restricted entity.
        /// </summary>
        /// <value>
        /// The restricted entity.
        /// </value>
        [JsonProperty(PropertyName = FieldObject)]
        public virtual BoxItem RestrictedEntity
        {
            get;
            set;
        }
    }
}
