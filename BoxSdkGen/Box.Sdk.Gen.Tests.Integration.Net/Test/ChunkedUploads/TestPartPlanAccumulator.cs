using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen {
    internal class TestPartPlanAccumulator {
        internal int LastIndex { get; }

        internal IReadOnlyList<UploadPartPlan> Parts { get; }

        internal long FileSize { get; }

        public TestPartPlanAccumulator(int lastIndex, IReadOnlyList<UploadPartPlan> parts, long fileSize) {
            LastIndex = lastIndex;
            Parts = parts;
            FileSize = fileSize;
        }
    }
}