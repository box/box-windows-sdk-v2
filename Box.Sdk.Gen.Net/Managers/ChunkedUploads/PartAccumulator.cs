using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    internal class PartAccumulator {
        internal long LastIndex { get; }

        internal IReadOnlyList<UploadPart> Parts { get; }

        internal long FileSize { get; }

        internal string UploadPartUrl { get; }

        internal Hash FileHash { get; }

        internal string PlanUrl { get; }

        public PartAccumulator(long lastIndex, IReadOnlyList<UploadPart> parts, long fileSize, string uploadPartUrl, Hash fileHash, string planUrl = "") {
            LastIndex = lastIndex;
            Parts = parts;
            FileSize = fileSize;
            UploadPartUrl = uploadPartUrl;
            FileHash = fileHash;
            PlanUrl = planUrl;
        }
    }
}