using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Managers {
    public enum SearchForContentQueryParamsTrashContentField {
        [Description("non_trashed_only")]
        NonTrashedOnly,
        [Description("trashed_only")]
        TrashedOnly,
        [Description("all_items")]
        AllItems
    }
}