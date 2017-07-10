using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of email alias
    /// </summary>
    public class BoxEmailAlias : BoxEntity
    {
        public const string FieldIsConfirmed = "is_confirmed";
        public const string FieldEmail = "email";

        /// <summary>
        /// The available access
        /// </summary>
        [JsonProperty(PropertyName = FieldIsConfirmed)]
        public bool IsConfirmed { get; private set; }

        /// <summary>
        /// The email address
        /// </summary>
        [JsonProperty(PropertyName = FieldEmail)]
        public string Email { get; private set; }
    }
}
