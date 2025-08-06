using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum UpdateFolderByIdRequestBodySyncStateField {
        [Description("synced")]
        Synced,
        [Description("not_synced")]
        NotSynced,
        [Description("partially_synced")]
        PartiallySynced
    }
}