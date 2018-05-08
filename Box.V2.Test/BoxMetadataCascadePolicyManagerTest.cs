using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxMetadataCascadePolicyManagerTest : BoxResourceManagerTest
    {
        private readonly BoxMetadataCascadePolicyManager _cascadePolicyManager;

        public BoxMetadataCascadePolicyManagerTest()
        {
            _cascadePolicyManager = new BoxMetadataCascadePolicyManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task CreateMetadataCascadePolicy_ValidResponse()
        {
            /*** Arrange ***/
            string responseString = "{ \"id\": \"84113349-794d-445c-b93c-d8481b223434\", \"type\": \"metadata_cascade_policy\", \"owner_enterprise\": { \"type\": \"enterprise\", \"id\": \"11111\" }, \"parent\": { \"type\": \"folder\", \"id\": \"22222\" }, \"scope\": \"enterprise_11111\", \"templateKey\": \"testTemplate\" }";
            Handler.Setup(h => h.ExecuteAsync<BoxMetadataCascadePolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxMetadataCascadePolicy>>(new BoxResponse<BoxMetadataCascadePolicy>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxMetadataCascadePolicy cascadePolicy = await _cascadePolicyManager.CreateCascadePolicyAsync("22222", "enterprise_11111", "templateKey");

            /*** Assert ***/
            Assert.AreEqual("84113349-794d-445c-b93c-d8481b223434", cascadePolicy.Id);
            Assert.AreEqual("22222", cascadePolicy.Parent.Id);
            Assert.AreEqual("enterprise_11111", cascadePolicy.Scope);
            Assert.AreEqual("templateTest", cascadePolicy.TemplateKey);
        }
    }
}
