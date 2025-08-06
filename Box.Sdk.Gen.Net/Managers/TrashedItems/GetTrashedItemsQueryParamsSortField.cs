using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum GetTrashedItemsQueryParamsSortField {
        [Description("name")]
        Name,
        [Description("date")]
        Date,
        [Description("size")]
        Size
    }
}