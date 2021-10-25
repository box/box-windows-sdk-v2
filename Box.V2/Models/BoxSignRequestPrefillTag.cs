using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// When a document contains sign related tags in the content, you can prefill them using this prefill_tags by referencing the 'id' of the tag as the external_id field of the prefill tag.
    /// </summary>
    public class BoxSignRequestPrefillTag
    {
        public const string FieldCheckboxValue = "checkbox_value";
        public const string FieldDateValue = "date_value";
        public const string FieldDocumentTagId = "document_tag_id";
        public const string FieldTextValue = "text_value";

        /// <summary>
        /// Checkbox prefill value.
        /// </summary>
        [JsonProperty(PropertyName = FieldCheckboxValue)]
        public virtual bool? CheckboxValue { get; set; }

        /// <summary>
        /// Date prefill value.
        /// </summary>
        [JsonProperty(PropertyName = FieldDateValue)]
        public virtual DateTimeOffset? DateValue { get; set; }

        /// <summary>
        /// This references the ID of a specific tag contained in a file of the sign request.
        /// </summary>
        [JsonProperty(PropertyName = FieldDocumentTagId)]
        public virtual string DocumentTagId { get; set; }

        /// <summary>
        /// Text prefill value.
        /// </summary>
        [JsonProperty(PropertyName = FieldTextValue)]
        public virtual string TextValue { get; set; }
    }
}
