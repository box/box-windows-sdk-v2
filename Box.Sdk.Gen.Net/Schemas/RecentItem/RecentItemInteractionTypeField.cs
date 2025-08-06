using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum RecentItemInteractionTypeField {
        [Description("item_preview")]
        ItemPreview,
        [Description("item_upload")]
        ItemUpload,
        [Description("item_comment")]
        ItemComment,
        [Description("item_open")]
        ItemOpen,
        [Description("item_modify")]
        ItemModify
    }
}