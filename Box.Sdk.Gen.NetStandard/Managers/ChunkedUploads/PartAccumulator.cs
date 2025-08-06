using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    internal class PartAccumulator {
        internal long LastIndex { get; set; }

        internal IReadOnlyList<UploadPart> Parts { get; set; }

        internal long FileSize { get; set; }

        internal string UploadPartUrl { get; set; }

        internal Hash FileHash { get; set; }

        public PartAccumulator(long lastIndex, IReadOnlyList<UploadPart> parts, long fileSize, string uploadPartUrl, Hash fileHash) {
            LastIndex = lastIndex;
            Parts = parts;
            FileSize = fileSize;
            UploadPartUrl = uploadPartUrl;
            FileHash = fileHash;
        }
    }
}