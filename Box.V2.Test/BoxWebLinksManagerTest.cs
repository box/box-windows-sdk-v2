using Box.V2.Config;
using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxWebLinkManagerTest : BoxResourceManagerTest
    {
        protected BoxWebLinksManager _webLinkManager;

        public BoxWebLinkManagerTest()
        {
            _webLinkManager = new BoxWebLinksManager(_config.Object, _service, _converter, _authRepository);
        }

        [TestMethod]
        public async Task CreateWeblink_ValidResponse()
        {
            /*** Arrange ***/
            string responseString = @"{
                                        ""type"": ""web_link"",
                                        ""id"": ""6743065"",
                                        ""sequence_id"": ""0"",
                                        ""etag"": ""0"",
                                        ""name"": ""Box Website!"",
                                        ""url"": ""https://www.box.com"",
                                        ""created_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""10523870"",
                                            ""name"": ""Ted Blosser"",
                                            ""login"": ""ted+demo@box.com""
                                        },
                                        ""created_at"": ""2015-05-07T15:00:01-07:00"",
                                        ""modified_at"": ""2015-05-07T15:00:01-07:00"",
                                        ""parent"": {
                                            ""type"": ""folder"",
                                            ""id"": ""848123342"",
                                            ""sequence_id"": ""1"",
                                            ""etag"": ""1"",
                                            ""name"": ""Documentation""
                                        },
                                        ""description"": ""Cloud Content Management"",
                                        ""item_status"": ""active"",
                                        ""trashed_at"": null,
                                        ""purged_at"": null,
                                        ""shared_link"": null,
                                        ""path_collection"": {
                                            ""total_count"": 2,
                                            ""entries"": [
                                                {
                                                    ""type"": ""folder"",
                                                    ""id"": ""0"",
                                                    ""sequence_id"": null,
                                                    ""etag"": null,
                                                    ""name"": ""All Files""
                                                },
                                                {
                                                    ""type"": ""folder"",
                                                    ""id"": ""848123342"",
                                                    ""sequence_id"": ""1"",
                                                    ""etag"": ""1"",
                                                    ""name"": ""Documentation""
                                                }
                                            ]
                                        },
                                        ""modified_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""10523870"",
                                            ""name"": ""Ted Blosser"",
                                            ""login"": ""ted+demo@box.com""
                                        },
                                        ""owned_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""10523870"",
                                            ""name"": ""Ted Blosser"",
                                            ""login"": ""ted+demo@box.com""
                                        }
                                    }";
            IBoxRequest boxRequest = null;
            Uri webLinksUri = new Uri(Constants.WebLinksEndpointString);
            _config.SetupGet(x => x.WebLinksEndpointUri).Returns(webLinksUri);
            _handler.Setup(h => h.ExecuteAsync<BoxWebLink>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWebLink>>(new BoxResponse<BoxWebLink>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCreateWebLinkRequest createWebLinkRequest = new BoxCreateWebLinkRequest()
            {
                Url = "https://www.box.com",
                Parent = new BoxRequestEntity()
                {
                    Id = "848123342"
                },
                Name = "Box Website!",
                Description = "Cloud Content Management"
            };
            BoxWebLink result = await _webLinkManager.CreateWeblinkAsync(createWebLinkRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(webLinksUri, boxRequest.AbsoluteUri.AbsoluteUri);
            BoxCreateWebLinkRequest payload = JsonConvert.DeserializeObject<BoxCreateWebLinkRequest>(boxRequest.Payload);
            Assert.AreEqual("https://www.box.com", payload.Url);
            Assert.AreEqual("848123342", payload.Parent.Id);
            Assert.AreEqual("Box Website!", payload.Name);
            Assert.AreEqual("Cloud Content Management", payload.Description);

            //Response check
            Assert.AreEqual("web_link", result.Type);
            Assert.AreEqual("6743065", result.Id);
            Assert.AreEqual("https://www.box.com", result.Url);
            Assert.AreEqual("Cloud Content Management", result.Description);
            Assert.AreEqual("0", result.PathCollection.Entries[0].Id);
            Assert.AreEqual("All Files", result.PathCollection.Entries[0].Name);
            Assert.AreEqual("848123342", result.PathCollection.Entries[1].Id);
            Assert.AreEqual("Documentation", result.PathCollection.Entries[1].Name);
        }

        [TestMethod]
        public async Task DeleteWeblink_ValidResponse()
        {
            /*** Arrange ***/
            string responseString = "";
            IBoxRequest boxRequest = null;
            Uri webLinksUri = new Uri(Constants.WebLinksEndpointString);
            _config.SetupGet(x => x.WebLinksEndpointUri).Returns(webLinksUri);
            _handler.Setup(h => h.ExecuteAsync<BoxWebLink>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWebLink>>(new BoxResponse<BoxWebLink>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            bool result = await _webLinkManager.DeleteWeblinkAsync("6743065");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(webLinksUri + "6743065", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(true, result);
        }
    }
}
