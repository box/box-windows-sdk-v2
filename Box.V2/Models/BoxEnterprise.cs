﻿using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// Box mini representation of a enterprise
    /// </summary>
    class BoxEnterprise : BoxEntity
    {
        public const string FieldName = "name";

        /// <summary>
        /// The name of this enterprise
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public string Name { get; private set; }
    }
}
