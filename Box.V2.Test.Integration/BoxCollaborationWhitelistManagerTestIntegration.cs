using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxCollaborationWhitelistManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task CreateDomainWhitelist_LiveSession_ValidResponse()
        {
            var whiteList = await _client.CollaborationWhitelistManager.AddCollaborationWhitelistEntryAsync("test1.com", "both");
           
        }
    }
}
