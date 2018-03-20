using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    class BoxStoragePoliciesManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task AssignStoragePolicyAsync_LiveSession()
        {
            string userId = "235699372";
            string storagePolicyId = "26";
            var assignment = await _client.StoragePoliciesManager();
        }
    }
}
