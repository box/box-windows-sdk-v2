using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen {
    internal class TestPartAccumulator {
        internal int LastIndex { get; set; }

        internal IReadOnlyList<UploadPart> Parts { get; set; }

        internal long FileSize { get; set; }

        internal string UploadPartUrl { get; set; }

        internal string UploadSessionId { get; set; }

        internal Hash FileHash { get; set; }

        public TestPartAccumulator(int lastIndex, IReadOnlyList<UploadPart> parts, long fileSize, Hash fileHash, string uploadPartUrl = "", string uploadSessionId = "") {
            LastIndex = lastIndex;
            Parts = parts;
            FileSize = fileSize;
            UploadPartUrl = uploadPartUrl;
            UploadSessionId = uploadSessionId;
            FileHash = fileHash;
        }
    }
}