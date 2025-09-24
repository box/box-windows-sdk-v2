using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models
{
    /// <summary>
    /// Additional information on which fields are required and which fields are not editable.
    /// </summary>
    public class BoxSignTemplateAdditionalInfo
    {
        public const string FieldNonEditable = "non_editable";
        public const string FieldRequired = "required";

        /// <summary>
        /// List of fields that are not editable.
        /// </summary>
        [JsonProperty(PropertyName = FieldNonEditable)]
        public virtual List<string> NonEditable { get; private set; }

        /// <summary>
        /// Required fields.
        /// </summary>
        [JsonProperty(PropertyName = FieldRequired)]
        public virtual BoxSignTemplateAdditionalInfoRequired Required { get; private set; }

    }

    public class BoxSignTemplateAdditionalInfoRequired
    {
        public const string FieldSigners = "signers";

        /// <summary>
        /// Required signer fields.
        /// </summary>
        [JsonProperty(PropertyName = FieldSigners)]
        public virtual List<List<string>> Signers { get; private set; }
    }
}
