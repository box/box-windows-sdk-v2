using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxStoragePoliciesManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task AssignStoragePolicyAsync_LiveSession()
        {
            _ = await Client.StoragePoliciesManager.GetAssignmentAsync("user_240097255");
        }
    }
}
