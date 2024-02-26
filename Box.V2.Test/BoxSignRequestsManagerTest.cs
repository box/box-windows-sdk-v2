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
    public class BoxSignRequestsManagerTest : BoxResourceManagerTest
    {
        private readonly BoxSignRequestsManager _signRequestsManager;

        public BoxSignRequestsManagerTest()
        {
            _signRequestsManager = new BoxSignRequestsManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task CreateSignRequest_RequiredParams_Success()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxSignRequest>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxSignRequest>>(new BoxResponse<BoxSignRequest>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxSignRequest/CreateSignRequest200.json")
                }))
                .Callback<IBoxRequest>(r => boxRequest = r); ;

            var sourceFiles = new List<BoxSignRequestCreateSourceFile>
            {
                new BoxSignRequestCreateSourceFile()
                {
                    Id = "12345"
                }
            };

            var signers = new List<BoxSignRequestSignerCreate>
            {
                new BoxSignRequestSignerCreate()
                {
                    Email = "example@gmail.com",
                    Role = BoxSignRequestSignerRole.signer
                },
                new BoxSignRequestSignerCreate()
                {
                    Email = "example@gmail.com",
                    Role = BoxSignRequestSignerRole.signer
                },
            };

            var parentFolder = new BoxRequestEntity()
            {
                Id = "12345",
                Type = BoxType.folder
            };

            var request = new BoxSignRequestCreateRequest
            {
                SourceFiles = sourceFiles,
                Signers = signers,
                ParentFolder = parentFolder,
            };

            /*** Act ***/
            var response = await _signRequestsManager.CreateSignRequestAsync(request);

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/sign_requests"), boxRequest.AbsoluteUri);
            Assert.IsTrue(boxRequest.Payload.ContainsKeyValue("signers[0].role", "signer"));

            // Response check
            Assert.AreEqual(1, response.SourceFiles.Count);
            Assert.AreEqual("12345", response.SourceFiles[0].Id);
            Assert.AreEqual(2, response.Signers.Count);
            Assert.AreEqual("example@gmail.com", response.Signers[0].Email);
            Assert.AreEqual("12345", response.ParentFolder.Id);
            Assert.AreEqual(1, response.Signers[0].Inputs.Count);
            Assert.IsTrue(response.Signers[0].Inputs[0].CheckboxValue.Value);
            Assert.AreEqual(BoxSignRequestSingerInputContentType.checkbox, response.Signers[0].Inputs[0].ContentType);
        }

        [TestMethod]
        public async Task CreateSignRequest_OptionalParams_Success()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxSignRequest>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxSignRequest>>(new BoxResponse<BoxSignRequest>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxSignRequest/CreateSignRequest200.json")
                }))
                .Callback<IBoxRequest>(r => boxRequest = r); ;

            var sourceFiles = new List<BoxSignRequestCreateSourceFile>
            {
                new BoxSignRequestCreateSourceFile()
                {
                    Id = "12345"
                }
            };

            var signers = new List<BoxSignRequestSignerCreate>
            {
                new BoxSignRequestSignerCreate()
                {
                    Email = "example@gmail.com",
                    Role = BoxSignRequestSignerRole.signer,
                    RedirectUrl = new Uri("https://box.com/redirect_url_signer_1"),
                    DeclinedRedirectUrl = new Uri("https://box.com/declined_redirect_url_signer_1"),
                    LoginRequired = false,
                    Password = "abcdefg",
                    SignerGroupId = "SignerGroup",
                    VerificationPhoneNumber = "1234567890",
                }, new BoxSignRequestSignerCreate()
                {
                    Email = "other-example@gmail.com",
                    Role = BoxSignRequestSignerRole.signer,
                    RedirectUrl = new Uri("https://box.com/redirect_url_signer_1"),
                    DeclinedRedirectUrl = new Uri("https://box.com/declined_redirect_url_signer_1"),
                    SignerGroupId = "SignerGroup",
                    VerificationPhoneNumber = "1234567890",
                }
            };

            var parentFolder = new BoxRequestEntity()
            {
                Id = "12345",
                Type = BoxType.folder
            };

            var request = new BoxSignRequestCreateRequest
            {
                IsDocumentPreparationNeeded = true,
                AreRemindersEnabled = true,
                AreTextSignaturesEnabled = true,
                DaysValid = 2,
                EmailMessage = "Hello! Please sign the document below",
                EmailSubject = "Sign Request from Acme",
                ExternalId = "123",
                SourceFiles = sourceFiles,
                Signers = signers,
                ParentFolder = parentFolder,
                RedirectUrl = new Uri("https://box.com/redirect_url"),
                DeclinedRedirectUrl = new Uri("https://box.com/declined_redirect_url"),
                PrefillTags = new List<BoxSignRequestPrefillTag>
                {
                    new BoxSignRequestPrefillTag
                    (
                        "1234",
                        "text"
                    )
                },
                TemplateId = "12345",
            };

            /*** Act ***/
            var response = await _signRequestsManager.CreateSignRequestAsync(request);

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/sign_requests"), boxRequest.AbsoluteUri);

            // Response check
            Assert.AreEqual(1, response.SourceFiles.Count);
            Assert.AreEqual("12345", response.SourceFiles[0].Id);
            Assert.AreEqual(2, response.Signers.Count);
            Assert.AreEqual("example@gmail.com", response.Signers[0].Email);
            Assert.AreEqual("https://box.com/redirect_url_signer_1", response.Signers[0].RedirectUrl.ToString());
            Assert.AreEqual("https://box.com/declined_redirect_url_signer_1", response.Signers[0].DeclinedRedirectUrl.ToString());
            Assert.AreEqual("https://app.box.com/embed/sign/document/bf7aaac6/", response.Signers[0].IframeableEmbedUrl);
            Assert.AreEqual(1, response.Signers[0].Inputs.Count);
            Assert.IsTrue(response.Signers[0].Inputs[0].CheckboxValue.Value);
            Assert.AreEqual(BoxSignRequestSingerInputContentType.checkbox, response.Signers[0].Inputs[0].ContentType);
            Assert.AreEqual("12345", response.ParentFolder.Id);
            Assert.IsTrue(response.IsDocumentPreparationNeeded);
            Assert.IsTrue(response.AreRemindersEnabled);
            Assert.IsTrue(response.AreTextSignaturesEnabled);
            Assert.AreEqual(2, response.DaysValid);
            Assert.AreEqual("Hello! Please sign the document below", response.EmailMessage);
            Assert.AreEqual("Sign Request from Acme", response.EmailSubject);
            Assert.AreEqual("123", response.ExternalId);
            Assert.AreEqual(1, response.PrefillTags.Count);
            Assert.AreEqual("1234", response.PrefillTags[0].DocumentTagId);
            Assert.AreEqual("text", response.PrefillTags[0].TextValue);
            Assert.AreEqual(DateTimeOffset.Parse("2021-04-26T08:12:13.982Z"), response.PrefillTags[0].DateValue);
            Assert.AreEqual("https://box.com/redirect_url", response.RedirectUrl.ToString());
            Assert.AreEqual("https://box.com/declined_redirect_url", response.DeclinedRedirectUrl.ToString());
            Assert.AreEqual("12345", response.TemplateId);
            Assert.AreEqual("cd4ff89-8fc1-42cf-8b29-1890dedd26d7", response.Signers[0].SignerGroupId);
            Assert.AreEqual("1234567890", response.Signers[0].VerificationPhoneNumber);
            Assert.AreEqual("cd4ff89-8fc1-42cf-8b29-1890dedd26d7", response.Signers[1].SignerGroupId);
            Assert.AreEqual("1234567890", response.Signers[1].VerificationPhoneNumber);
            Assert.IsFalse(response.Signers[0].LoginRequired);
            Assert.AreEqual("abcdefg", response.Signers[0].Password);
            Assert.IsFalse(response.Signers[1].LoginRequired);
            Assert.AreEqual("abcdefg", response.Signers[1].Password);
        }

        [TestMethod]
        public async Task GetSignRequest_Success()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxSignRequest>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollectionMarkerBased<BoxSignRequest>>>(new BoxResponse<BoxCollectionMarkerBased<BoxSignRequest>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxSignRequest/GetAllSignRequests200.json")
                }))
            .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCollectionMarkerBased<BoxSignRequest> response = await _signRequestsManager.GetSignRequestsAsync(1000, "JV9IRGZmieiBasejOG9yDCRNgd2ymoZIbjsxbJMjIs3kioVii");

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/sign_requests?limit=1000&marker=JV9IRGZmieiBasejOG9yDCRNgd2ymoZIbjsxbJMjIs3kioVii"), boxRequest.AbsoluteUri);

            // Response check
            Assert.AreEqual(1, response.Entries[0].SourceFiles.Count);
            Assert.AreEqual("12345", response.Entries[0].SourceFiles[0].Id);
            Assert.AreEqual(1, response.Entries[0].Signers.Count);
            Assert.AreEqual("example@gmail.com", response.Entries[0].Signers[0].Email);
            Assert.AreEqual("https://box.com/redirect_url_signer_1", response.Entries[0].Signers[0].RedirectUrl.ToString());
            Assert.AreEqual("https://box.com/declined_redirect_url_signer_1", response.Entries[0].Signers[0].DeclinedRedirectUrl.ToString());
            Assert.AreEqual("https://app.box.com/embed/sign/document/bf7aaac6/", response.Entries[0].Signers[0].IframeableEmbedUrl);
            Assert.AreEqual("12345", response.Entries[0].ParentFolder.Id);
            Assert.IsTrue(response.Entries[0].IsDocumentPreparationNeeded);
            Assert.IsTrue(response.Entries[0].AreRemindersEnabled);
            Assert.IsTrue(response.Entries[0].AreTextSignaturesEnabled);
            Assert.AreEqual(2, response.Entries[0].DaysValid);
            Assert.AreEqual("Hello! Please sign the document below", response.Entries[0].EmailMessage);
            Assert.AreEqual("Sign Request from Acme", response.Entries[0].EmailSubject);
            Assert.AreEqual("123", response.Entries[0].ExternalId);
            Assert.AreEqual(1, response.Entries[0].PrefillTags.Count);
            Assert.AreEqual("1234", response.Entries[0].PrefillTags[0].DocumentTagId);
            Assert.AreEqual("text", response.Entries[0].PrefillTags[0].TextValue);
            Assert.IsTrue(response.Entries[0].PrefillTags[0].CheckboxValue.Value);
            Assert.AreEqual(DateTimeOffset.Parse("2021-04-26T08:12:13.982Z"), response.Entries[0].PrefillTags[0].DateValue);
            Assert.AreEqual("https://box.com/redirect_url", response.Entries[0].RedirectUrl.ToString());
            Assert.AreEqual("https://box.com/declined_redirect_url", response.Entries[0].DeclinedRedirectUrl.ToString());
        }

        [TestMethod]
        public async Task GetSignRequestById_Success()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxSignRequest>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxSignRequest>>(new BoxResponse<BoxSignRequest>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxSignRequest/GetSignRequest200.json")
                }))
            .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxSignRequest response = await _signRequestsManager.GetSignRequestByIdAsync("12345");

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/sign_requests/12345"), boxRequest.AbsoluteUri);

            // Response check
            Assert.AreEqual(1, response.SourceFiles.Count);
            Assert.AreEqual("12345", response.SourceFiles[0].Id);
            Assert.AreEqual(1, response.Signers.Count);
            Assert.AreEqual("example@gmail.com", response.Signers[0].Email);
            Assert.AreEqual("https://box.com/redirect_url_signer_1", response.Signers[0].RedirectUrl.ToString());
            Assert.AreEqual("https://box.com/declined_redirect_url_signer_1", response.Signers[0].DeclinedRedirectUrl.ToString());
            Assert.AreEqual("https://app.box.com/embed/sign/document/bf7aaac6/", response.Signers[0].IframeableEmbedUrl);
            Assert.AreEqual(1, response.Signers[0].Inputs.Count);
            Assert.IsTrue(response.Signers[0].Inputs[0].CheckboxValue.Value);
            Assert.AreEqual(BoxSignRequestSingerInputContentType.checkbox, response.Signers[0].Inputs[0].ContentType);
            Assert.AreEqual("12345", response.ParentFolder.Id);
            Assert.IsTrue(response.IsDocumentPreparationNeeded);
            Assert.IsTrue(response.AreRemindersEnabled);
            Assert.IsTrue(response.AreTextSignaturesEnabled);
            Assert.AreEqual(2, response.DaysValid);
            Assert.AreEqual("Hello! Please sign the document below", response.EmailMessage);
            Assert.AreEqual("Sign Request from Acme", response.EmailSubject);
            Assert.AreEqual("123", response.ExternalId);
            Assert.AreEqual(1, response.PrefillTags.Count);
            Assert.AreEqual("1234", response.PrefillTags[0].DocumentTagId);
            Assert.AreEqual("text", response.PrefillTags[0].TextValue);
            Assert.IsTrue(response.PrefillTags[0].CheckboxValue.Value);
            Assert.AreEqual(DateTimeOffset.Parse("2021-04-26T08:12:13.982Z"), response.PrefillTags[0].DateValue);
            Assert.AreEqual("https://box.com/redirect_url", response.RedirectUrl.ToString());
            Assert.AreEqual("https://box.com/declined_redirect_url", response.DeclinedRedirectUrl.ToString());
        }

        [TestMethod]
        public async Task CancelSignRequest_Success()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxSignRequest>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxSignRequest>>(new BoxResponse<BoxSignRequest>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxSignRequest/CancelSignRequest200.json")
                }))
            .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxSignRequest response = await _signRequestsManager.CancelSignRequestAsync("12345");

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/sign_requests/12345/cancel"), boxRequest.AbsoluteUri);

            // Response check
            Assert.AreEqual("12345", response.Id);
            Assert.AreEqual(BoxSignRequestStatus.cancelled, response.Status);
        }

        [TestMethod]
        public async Task ResendSignRequest_Success()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<object>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<object>>(new BoxResponse<object>()
                {
                    Status = ResponseStatus.Success,
                }))
            .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            await _signRequestsManager.ResendSignRequestAsync("12345");

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/sign_requests/12345/resend"), boxRequest.AbsoluteUri);
        }
    }
}
