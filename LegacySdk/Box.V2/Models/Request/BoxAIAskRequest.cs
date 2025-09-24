using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models
{
    /// <summary>
    /// Model used to send request to Box AI Ask API
    /// </summary>
    public class BoxAIAskRequest
    {
        public const string FieldItems = "items";
        public const string FieldMode = "mode";
        public const string FieldPrompt = "prompt";

        /// <summary>
        /// The items to be processed by the LLM, often files.
        /// Note: Box AI handles documents with text representations up to 1MB in size, or a maximum of 25 files, whichever comes first.
        /// If the file size exceeds 1MB, the first 1MB of text representation will be processed.
        /// If you set mode parameter to single_item_qa, the items array can have one element only.
        /// </summary>
        [JsonProperty(PropertyName = FieldItems)]
        public List<BoxAIAskItem> Items { get; set; }

        /// <summary>
        /// The mode specifies if this request is for a single or multiple items.If you select single_item_qa the items array can have one element only.
        /// Selecting multiple_item_qa allows you to provide up to 25 items.
        /// </summary>
        [JsonProperty(PropertyName = FieldMode)]
        [JsonConverter(typeof(StringEnumConverter))]
        public BoxAIAskMode Mode { get; set; }

        /// <summary>
        /// The prompt provided by the client to be answered by the LLM.The prompt's length is limited to 10000 characters.
        /// </summary>
        [JsonProperty(PropertyName = FieldPrompt)]
        public string Prompt { get; set; }
    }

    /// <summary>
    /// The items to be processed by the LLM, often files.
    /// Note: Box AI handles documents with text representations up to 1MB in size, or a maximum of 25 files, whichever comes first.
    /// If the file size exceeds 1MB, the first 1MB of text representation will be processed.
    /// If you set mode parameter to single_item_qa, the items array can have one element only.
    /// </summary>
    public class BoxAIAskItem
    {
        public const string FieldId = "id";
        public const string FieldType = "type";
        public const string FieldContent = "content";

        /// <summary>
        /// The id of the item.
        /// </summary>
        [JsonProperty(PropertyName = FieldId)]
        public string Id { get; set; }

        /// <summary>
        /// The type of the item. Value is always file.
        /// </summary>
        [JsonProperty(PropertyName = FieldType)]
        public string Type => "file";

        /// <summary>
        /// The content of the item, often the text representation.
        /// </summary>
        [JsonProperty(PropertyName = FieldContent)]
        public string Content { get; set; }
    }

    /// <summary>
    /// The mode specifies if this request is for a single or multiple items. If you select single_item_qa the items array can have one element only.
    /// Selecting multiple_item_qa allows you to provide up to 25 items.
    /// </summary>
    public enum BoxAIAskMode
    {
        multiple_item_qa,
        single_item_qa
    }
}
