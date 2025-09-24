using System;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.Managers;
using Box.V2.Models;
using Box.V2.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxTermsOfServiceManagerTest : BoxResourceManagerTest
    {
        private readonly BoxTermsOfServiceManager _termsOfServiceManager;

        public BoxTermsOfServiceManagerTest()
        {
            _termsOfServiceManager = new BoxTermsOfServiceManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task CreateTermsOfServiceUserStatus_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""terms_of_service_user_status"",
                                        ""id"": ""11446498"",
                                        ""created_at"": ""2012-12-12T10:53:43-08:00"",
                                        ""is_accepted"": ""true"",
                                        ""modified_at"": ""2012-12-12T10:53:43-08:00"",
                                        ""tos"": {
                                            ""type"": ""terms_of_service"",
                                            ""id"": ""11446498"",
                                        },
                                        ""user"": {
                                            ""type"": ""user"",
                                            ""id"": ""24446498"",
                                            ""name"": ""Aaron Levie"",
                                            ""login"": ""ceo@box.com""
                                        },
                                    }";
            IBoxRequest boxRequest = null;
            var tosUri = new Uri(Constants.BoxApiUriString + Constants.TermsOfServiceUserStatusesString);
            Config.SetupGet(x => x.TermsOfServiceUserStatusesUri).Returns(tosUri);
            Handler.Setup(h => h.ExecuteAsync<BoxTermsOfServiceUserStatuses>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxTermsOfServiceUserStatuses>>(new BoxResponse<BoxTermsOfServiceUserStatuses>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var createStatusRequest = new BoxTermsOfServiceUserStatusCreateRequest()
            {
                TermsOfService = new BoxRequestEntity()
                {
                    Id = "11446498",
                    Type = BoxType.terms_of_service
                },
                User = new BoxRequestEntity()
                {
                    Id = "24446498",
                    Type = BoxType.user
                },
                IsAccepted = true
            };
            BoxTermsOfServiceUserStatuses result =
                await _termsOfServiceManager.CreateBoxTermsOfServiceUserStatusesAsync(createStatusRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(tosUri, boxRequest.AbsoluteUri.AbsoluteUri);
            BoxTermsOfServiceUserStatusCreateRequest payload = JsonConvert.DeserializeObject<BoxTermsOfServiceUserStatusCreateRequest>(boxRequest.Payload);
            Assert.AreEqual(createStatusRequest.TermsOfService.Id, payload.TermsOfService.Id);
            Assert.AreEqual(createStatusRequest.TermsOfService.Type, payload.TermsOfService.Type);
            Assert.AreEqual(createStatusRequest.User.Id, payload.User.Id);
            Assert.AreEqual(createStatusRequest.User.Type, payload.User.Type);
            Assert.AreEqual(createStatusRequest.IsAccepted, payload.IsAccepted);

            //Response check
            Assert.AreEqual("11446498", result.Id);
            Assert.AreEqual("terms_of_service_user_status", result.Type);
            Assert.AreEqual(true, result.IsAccepted);
            Assert.AreEqual("11446498", result.TermsOfService.Id);
            Assert.AreEqual("24446498", result.User.Id);
            Assert.AreEqual("Aaron Levie", result.User.Name);
        }
    }
}
