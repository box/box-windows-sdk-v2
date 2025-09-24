using System;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
        public async Task CreateMetadataCascadePolicy_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = "{ \"id\": \"84113349-794d-445c-b93c-d8481b223434\", \"type\": \"metadata_cascade_policy\", \"owner_enterprise\": { \"type\": \"enterprise\", \"id\": \"11111\" }, \"parent\": { \"type\": \"folder\", \"id\": \"22222\" }, \"scope\": \"enterprise_11111\", \"templateKey\": \"testTemplate\" }";
            IBoxRequest boxRequest = null;
            var cascadePoliciesUri = new Uri(Constants.MetadataCascadePolicyEndpointString);
            Config.SetupGet(x => x.MetadataCascadePolicyUri).Returns(cascadePoliciesUri);
            Handler.Setup(h => h.ExecuteAsync<BoxMetadataCascadePolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxMetadataCascadePolicy>>(new BoxResponse<BoxMetadataCascadePolicy>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxMetadataCascadePolicy cascadePolicy = await _cascadePolicyManager.CreateCascadePolicyAsync("22222", "enterprise_11111", "templateKey");

            /*** Assert ***/
            var payload = boxRequest.Payload;
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(cascadePoliciesUri, boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.AreEqual("{\r\n  \"folder_id\": \"22222\",\r\n  \"scope\": \"enterprise_11111\",\r\n  \"templateKey\": \"templateKey\"\r\n}", payload);

            Assert.AreEqual("84113349-794d-445c-b93c-d8481b223434", cascadePolicy.Id);
            Assert.AreEqual("22222", cascadePolicy.Parent.Id);
            Assert.AreEqual("enterprise_11111", cascadePolicy.Scope);
            Assert.AreEqual("testTemplate", cascadePolicy.TemplateKey);
        }

        [TestMethod]
        public async Task GetMetadataCascadePolicy_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = "{ \"id\": \"84113349-794d-445c-b93c-d8481b223434\", \"type\": \"metadata_cascade_policy\", \"owner_enterprise\": { \"type\": \"enterprise\", \"id\": \"11111\" }, \"parent\": { \"type\": \"folder\", \"id\": \"22222\" }, \"scope\": \"enterprise_11111\", \"templateKey\": \"testTemplate\" }";
            IBoxRequest boxRequest = null;
            var cascadePoliciesUri = new Uri(Constants.MetadataCascadePolicyEndpointString);
            Config.SetupGet(x => x.MetadataCascadePolicyUri).Returns(cascadePoliciesUri);
            Handler.Setup(h => h.ExecuteAsync<BoxMetadataCascadePolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxMetadataCascadePolicy>>(new BoxResponse<BoxMetadataCascadePolicy>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxMetadataCascadePolicy cascadePolicy = await _cascadePolicyManager.GetCascadePolicyAsync("84113349-794d-445c-b93c-d8481b223434");

            /*** Assert ***/
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(cascadePoliciesUri + "84113349-794d-445c-b93c-d8481b223434", boxRequest.AbsoluteUri.AbsoluteUri);

            Assert.AreEqual("84113349-794d-445c-b93c-d8481b223434", cascadePolicy.Id);
            Assert.AreEqual("22222", cascadePolicy.Parent.Id);
            Assert.AreEqual("11111", cascadePolicy.OwnerEnterprise.Id);
            Assert.AreEqual("testTemplate", cascadePolicy.TemplateKey);
        }

        [TestMethod]
        public async Task DeleteMetadataCascadePolicy_ValidResponse()
        {
            var responseString = "";
            IBoxRequest boxRequest = null;
            var metadataCascadePolicyUri = new Uri(Constants.MetadataCascadePolicyEndpointString);
            Config.SetupGet(x => x.MetadataCascadePolicyUri).Returns(metadataCascadePolicyUri);
            Handler.Setup(h => h.ExecuteAsync<BoxMetadataCascadePolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxMetadataCascadePolicy>>(new BoxResponse<BoxMetadataCascadePolicy>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var result = await _cascadePolicyManager.DeleteCascadePolicyAsync("12345");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(metadataCascadePolicyUri + "12345", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task ForceApplyMetadataCascadePolicy_ValidResponse()
        {
            var responseString = "";
            IBoxRequest boxRequest = null;
            var metadataCascadePolicyUri = new Uri(Constants.MetadataCascadePolicyEndpointString);
            Config.SetupGet(x => x.MetadataCascadePolicyUri).Returns(metadataCascadePolicyUri);
            Handler.Setup(h => h.ExecuteAsync<BoxMetadataCascadePolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxMetadataCascadePolicy>>(new BoxResponse<BoxMetadataCascadePolicy>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var result = await _cascadePolicyManager.ForceApplyCascadePolicyAsync("12345", "none");

            /*** Assert ***/
            //Request check
            var payload = boxRequest.Payload;
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(metadataCascadePolicyUri + "12345/apply", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.AreEqual("{\r\n  \"conflict_resolution\": \"none\"\r\n}", payload);

            //Response check
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task GetAllMetadataCascadePolicies_ValidResponse()
        {
            var responseString = "{ \"limit\": 100, \"entries\": [ { \"id\": \"6fd4ff89-8fc1-42cf-8b29-1890dedd26d7\", \"type\": \"metadata_cascade_policy\", \"owner_enterprise\": { \"type\": \"enterprise\", \"id\": \"1111\" }, \"parent\": { \"type\": \"folder\", \"id\": \"2222\" }, \"scope\": \"enterprise_1111\", \"templateKey\": \"demo\" } ], \"next_marker\": null, \"prev_marker\": null }";
            IBoxRequest boxRequest = null;
            var metadataCascadePolicyUri = new Uri(Constants.MetadataCascadePolicyEndpointString);
            Config.SetupGet(x => x.MetadataCascadePolicyUri).Returns(metadataCascadePolicyUri);
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxMetadataCascadePolicy>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollectionMarkerBased<BoxMetadataCascadePolicy>>>(new BoxResponse<BoxCollectionMarkerBased<BoxMetadataCascadePolicy>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var metadataCascadePolicies = await _cascadePolicyManager.GetAllMetadataCascadePoliciesAsync("2222", "1111");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(metadataCascadePolicyUri + "?folder_id=2222&owner_enterprise_id=1111&limit=100", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual("6fd4ff89-8fc1-42cf-8b29-1890dedd26d7", metadataCascadePolicies.Entries[0].Id);
            Assert.AreEqual("metadata_cascade_policy", metadataCascadePolicies.Entries[0].Type);
            Assert.AreEqual("1111", metadataCascadePolicies.Entries[0].OwnerEnterprise.Id);
        }
    }
}
