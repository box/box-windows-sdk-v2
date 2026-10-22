using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

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