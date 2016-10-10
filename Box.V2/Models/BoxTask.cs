using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxTask : BoxItem
    {
        public const string FieldDueAt = "due_at";
        public const string FieldItem = "item";
        /// <summary>
        /// Date of task completion
        /// </summary>
        [JsonProperty(PropertyName = FieldDueAt)]
        public string DueAt { get; private set; }

        /// <summary>
        /// Mini file object. The file associated with this task
        /// </summary>
        [JsonProperty(PropertyName = FieldItem)]
        public BoxItem Item { get; private set; }
    }
}
