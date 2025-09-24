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

        // For serialization
        private BoxSignRequestPrefillTag() { }

        /// <summary>
        /// Creates prefill tag with checkbox value.
        /// </summary>
        /// <param name="documentTagId">References the ID of a specific tag contained in a file of the sign request.</param>
        /// <param name="checkboxValue">Checkbox prefill value.</param>
        /// <returns>A prefill tag</returns>
        public BoxSignRequestPrefillTag(string documentTagId, bool checkboxValue)
        {
            DocumentTagId = documentTagId;
            CheckboxValue = checkboxValue;
        }

        /// <summary>
        /// Creates prefill tag with date value.
        /// </summary>
        /// <param name="documentTagId">References the ID of a specific tag contained in a file of the sign request.</param>
        /// <param name="dateValue">Date prefill value.</param>
        /// <returns>A prefill tag</returns>
        public BoxSignRequestPrefillTag(string documentTagId, DateTimeOffset dateValue)
        {
            DocumentTagId = documentTagId;
            DateValue = dateValue;
        }

        /// <summary>
        /// Creates prefill tag with text value.
        /// </summary>
        /// <param name="documentTagId">References the ID of a specific tag contained in a file of the sign request.</param>
        /// <param name="textValue">Text prefill value.</param>
        /// <returns>A prefill tag</returns>
        public BoxSignRequestPrefillTag(string documentTagId, string textValue)
        {
            DocumentTagId = documentTagId;
            TextValue = textValue;
        }

        /// <summary>
        /// Checkbox prefill value.
        /// </summary>
        [JsonProperty(PropertyName = FieldCheckboxValue)]
        public virtual bool? CheckboxValue { get; private set; }

        /// <summary>
        /// Date prefill value.
        /// </summary>
        [JsonProperty(PropertyName = FieldDateValue)]
        public virtual DateTimeOffset? DateValue { get; private set; }

        /// <summary>
        /// This references the ID of a specific tag contained in a file of the sign request.
        /// </summary>
        [JsonProperty(PropertyName = FieldDocumentTagId)]
        public virtual string DocumentTagId { get; private set; }

        /// <summary>
        /// Text prefill value.
        /// </summary>
        [JsonProperty(PropertyName = FieldTextValue)]
        public virtual string TextValue { get; private set; }
    }
}
