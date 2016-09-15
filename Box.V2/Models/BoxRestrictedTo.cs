using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxRestrictedTo
    {
        /// <summary>
        /// Gets or sets the scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        [JsonProperty(PropertyName = "scope")]
        public string Scope
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
        [JsonProperty(PropertyName = "object")]
        public BoxEntity RestrictedEntity
        {
            get;
            set;
        }
    }
}
