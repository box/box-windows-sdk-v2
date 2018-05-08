using System.Threading.Tasks;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using Box.V2.Models.Request;
using static Box.V2.Config.Constants;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxStoragePoliciesManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task AssignStoragePolicyAsync_LiveSession()
        {
            var assignment = await _client.StoragePoliciesManager.GetAssignmentAsync("user_240097255");
        }
    }
}
