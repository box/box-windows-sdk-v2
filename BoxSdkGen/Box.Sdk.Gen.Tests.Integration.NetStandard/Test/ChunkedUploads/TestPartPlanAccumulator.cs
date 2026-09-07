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
        internal int LastIndex { get; set; }

        internal IReadOnlyList<UploadPartPlan> Parts { get; set; }

        internal long FileSize { get; set; }

        public TestPartPlanAccumulator(int lastIndex, IReadOnlyList<UploadPartPlan> parts, long fileSize) {
            LastIndex = lastIndex;
            Parts = parts;
            FileSize = fileSize;
        }
    }
}