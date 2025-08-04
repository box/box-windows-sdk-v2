using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum UpdateMetadataTemplateRequestBodyOpField {
        [Description("editTemplate")]
        EditTemplate,
        [Description("addField")]
        AddField,
        [Description("reorderFields")]
        ReorderFields,
        [Description("addEnumOption")]
        AddEnumOption,
        [Description("reorderEnumOptions")]
        ReorderEnumOptions,
        [Description("reorderMultiSelectOptions")]
        ReorderMultiSelectOptions,
        [Description("addMultiSelectOption")]
        AddMultiSelectOption,
        [Description("editField")]
        EditField,
        [Description("removeField")]
        RemoveField,
        [Description("editEnumOption")]
        EditEnumOption,
        [Description("removeEnumOption")]
        RemoveEnumOption,
        [Description("editMultiSelectOption")]
        EditMultiSelectOption,
        [Description("removeMultiSelectOption")]
        RemoveMultiSelectOption
    }
}