using System.Linq;
using System.Threading.Tasks;
using Box.V2.Test.Integration.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxCollaborationWhitelistManagerIntegrationTest : TestInFolder
    {
        [TestMethod]
        public async Task GetAllCollaborationWhitelistExemptUsersAsync_ForExistingCollaborator_ShouldReturnWhitelistCollaborator()
        {
            var user = await CreateEnterpriseUser();
            var collaborationExempt = await AddCollaborationExempt(user.Id);

            var allUserWhitelistItems = await AdminClient.CollaborationWhitelistManager.GetAllCollaborationWhitelistExemptUsersAsync(10);

            Assert.IsTrue(allUserWhitelistItems.Entries.Any(x => x.Id == collaborationExempt.Id));
        }
    }
}
