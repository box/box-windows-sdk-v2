using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Managers;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxSignTemplatesManagerTest : BoxResourceManagerTest
    {
        private readonly BoxSignTemplatesManager _signTemplatesManager;

        public BoxSignTemplatesManagerTest()
        {
            _signTemplatesManager = new BoxSignTemplatesManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task GetSignTemplateById_Success()
        {
            /** Arrange **/
            const string signTemplateId = "93153068-5420-467b-b8ef-8e54bfb7be42";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxSignTemplate>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxSignTemplate>>(new BoxResponse<BoxSignTemplate>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxSignTemplate/GetSignTemplate200.json")
                }))
            .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxSignTemplate response = await _signTemplatesManager.GetSignTemplateByIdAsync(signTemplateId);

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/sign_templates/93153068-5420-467b-b8ef-8e54bfb7be42"), boxRequest.AbsoluteUri);

            // Response check
            Assert.AreEqual(signTemplateId, response.Id);
            Assert.AreEqual("requirements-dev.pdf", response.Name);
            Assert.AreEqual("Please sign this document.\n\nKind regards", response.EmailMessage);
            Assert.AreEqual("Someone (user00@box.com) has requested your signature on a document", response.EmailSubject);
            Assert.AreEqual("1234567890", response.ParentFolder.Id);
            Assert.AreEqual(1, response.SourceFiles.Count);
            Assert.AreEqual("1234567890", response.SourceFiles[0].Id);
            Assert.AreEqual("https://app.box.com/sign/ready-sign-link/59917816-c12b-4ef6-8f1d-aaaaaaa", response.ReadySignLink.Url);
        }

        [TestMethod]
        public async Task GetSignTemplates_Success()
        {
            /** Arrange **/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxSignTemplate>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollectionMarkerBased<BoxSignTemplate>>>(new BoxResponse<BoxCollectionMarkerBased<BoxSignTemplate>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxSignTemplate/GetAllSignTemplates200.json")
                }))
            .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCollectionMarkerBased<BoxSignTemplate> response = await _signTemplatesManager.GetSignTemplatesAsync(1000, "JV9IRGZmieiBasejOG9yDCRNgd2ymoZIbjsxbJMjIs3kioVii");

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/sign_templates?limit=1000&marker=JV9IRGZmieiBasejOG9yDCRNgd2ymoZIbjsxbJMjIs3kioVii"), boxRequest.AbsoluteUri);

            // Response check
            Assert.AreEqual(1, response.Entries.Count);
            Assert.AreEqual("93153068-5420-467b-b8ef-8e54bfb7be42", response.Entries[0].Id);
        }
    }
}
