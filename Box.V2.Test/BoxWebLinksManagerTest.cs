using System;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxWebLinkManagerTest : BoxResourceManagerTest
    {
        private readonly BoxWebLinksManager _webLinkManager;

        public BoxWebLinkManagerTest()
        {
            _webLinkManager = new BoxWebLinksManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task CreateWeblink_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
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
            var webLinksUri = new Uri(Constants.WebLinksEndpointString);
            Config.SetupGet(x => x.WebLinksEndpointUri).Returns(webLinksUri);
            Handler.Setup(h => h.ExecuteAsync<BoxWebLink>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWebLink>>(new BoxResponse<BoxWebLink>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var createWebLinkRequest = new BoxWebLinkRequest()
            {
                Url = new Uri("https://www.box.com"),
                Parent = new BoxRequestEntity()
                {
                    Id = "848123342"
                },
                Name = "Box Website!",
                Description = "Cloud Content Management"
            };
            BoxWebLink result = await _webLinkManager.CreateWebLinkAsync(createWebLinkRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(webLinksUri, boxRequest.AbsoluteUri.AbsoluteUri);
            BoxWebLinkRequest payload = JsonConvert.DeserializeObject<BoxWebLinkRequest>(boxRequest.Payload);
            Assert.AreEqual(new Uri("https://www.box.com"), payload.Url);
            Assert.AreEqual("848123342", payload.Parent.Id);
            Assert.AreEqual("Box Website!", payload.Name);
            Assert.AreEqual("Cloud Content Management", payload.Description);

            //Response check
            Assert.AreEqual("web_link", result.Type);
            Assert.AreEqual("6743065", result.Id);
            Assert.AreEqual(new Uri("https://www.box.com"), result.Url);
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
            var responseString = "";
            IBoxRequest boxRequest = null;
            var webLinksUri = new Uri(Constants.WebLinksEndpointString);
            Config.SetupGet(x => x.WebLinksEndpointUri).Returns(webLinksUri);
            Handler.Setup(h => h.ExecuteAsync<BoxWebLink>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWebLink>>(new BoxResponse<BoxWebLink>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var result = await _webLinkManager.DeleteWebLinkAsync("6743065");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(webLinksUri + "6743065", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task GetWeblink_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""web_link"",
                                        ""id"": ""6742981"",
                                        ""sequence_id"": ""0"",
                                        ""etag"": ""0"",
                                        ""name"": ""Box Website"",
                                        ""url"": ""https://www.box.com"",
                                        ""created_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""10523870"",
                                            ""name"": ""Ted Blosser"",
                                            ""login"": ""ted+demo@box.com""
                                        },
                                        ""created_at"": ""2015-05-07T14:31:16-07:00"",
                                        ""modified_at"": ""2015-05-07T14:31:16-07:00"",
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
            var webLinksUri = new Uri(Constants.WebLinksEndpointString);
            Config.SetupGet(x => x.WebLinksEndpointUri).Returns(webLinksUri);
            Handler.Setup(h => h.ExecuteAsync<BoxWebLink>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWebLink>>(new BoxResponse<BoxWebLink>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxWebLink result = await _webLinkManager.GetWebLinkAsync("6742981");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(webLinksUri + "6742981", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual("web_link", result.Type);
            Assert.AreEqual("6742981", result.Id);
            Assert.AreEqual(new Uri("https://www.box.com"), result.Url);
            Assert.AreEqual("Cloud Content Management", result.Description);
            Assert.AreEqual("0", result.PathCollection.Entries[0].Id);
            Assert.AreEqual("All Files", result.PathCollection.Entries[0].Name);
            Assert.AreEqual("848123342", result.PathCollection.Entries[1].Id);
            Assert.AreEqual("Documentation", result.PathCollection.Entries[1].Name);
        }

        [TestMethod]
        public async Task UpdateWeblink_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""web_link"",
                                        ""id"": ""6742981"",
                                        ""sequence_id"": ""2"",
                                        ""etag"": ""2"",
                                        ""name"": ""Box Marketing Web Page"",
                                        ""url"": ""https://www.box.com"",
                                        ""created_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""10523870"",
                                            ""name"": ""Ted Blosser"",
                                            ""login"": ""ted+demo@box.com""
                                        },
                                        ""created_at"": ""2015-05-07T14:31:16-07:00"",
                                        ""modified_at"": ""2015-05-07T15:45:04-07:00"",
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
            var webLinksUri = new Uri(Constants.WebLinksEndpointString);
            Config.SetupGet(x => x.WebLinksEndpointUri).Returns(webLinksUri);
            Handler.Setup(h => h.ExecuteAsync<BoxWebLink>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWebLink>>(new BoxResponse<BoxWebLink>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var updateWebLinkRequest = new BoxWebLinkRequest()
            {
                Name = "Box Marketing Web Page"
            };
            BoxWebLink result = await _webLinkManager.UpdateWebLinkAsync("6742981", updateWebLinkRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(webLinksUri + "6742981", boxRequest.AbsoluteUri.AbsoluteUri);
            BoxWebLinkRequest payload = JsonConvert.DeserializeObject<BoxWebLinkRequest>(boxRequest.Payload);
            Assert.AreEqual("Box Marketing Web Page", payload.Name);


            //Response check
            Assert.AreEqual("web_link", result.Type);
            Assert.AreEqual("6742981", result.Id);
            Assert.AreEqual(new Uri("https://www.box.com"), result.Url);
            Assert.AreEqual("Box Marketing Web Page", result.Name);
            Assert.AreEqual("Cloud Content Management", result.Description);
            Assert.AreEqual("0", result.PathCollection.Entries[0].Id);
            Assert.AreEqual("All Files", result.PathCollection.Entries[0].Name);
            Assert.AreEqual("848123342", result.PathCollection.Entries[1].Id);
            Assert.AreEqual("Documentation", result.PathCollection.Entries[1].Name);
            Assert.AreEqual("10523870", result.ModifiedBy.Id);
            Assert.AreEqual("10523870", result.OwnedBy.Id);
        }

        [TestMethod]
        public async Task Copy_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
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
            var webLinksUri = new Uri(Constants.WebLinksEndpointString);
            Config.SetupGet(x => x.WebLinksEndpointUri).Returns(webLinksUri);
            Handler.Setup(h => h.ExecuteAsync<BoxWebLink>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWebLink>>(new BoxResponse<BoxWebLink>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            var webLinkId = "11111";
            var destinationFolderId = "22222";

            /*** Act ***/
            BoxWebLink result = await _webLinkManager.CopyAsync(webLinkId, destinationFolderId);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/web_links/11111/copy"), boxRequest.AbsoluteUri);
            Assert.AreEqual("{\"parent\":{\"id\":\"22222\"}}", boxRequest.Payload);

            //Response check
            Assert.AreEqual("web_link", result.Type);
            Assert.AreEqual("6743065", result.Id);
            Assert.AreEqual(new Uri("https://www.box.com"), result.Url);
            Assert.AreEqual("Cloud Content Management", result.Description);
            Assert.AreEqual("0", result.PathCollection.Entries[0].Id);
            Assert.AreEqual("All Files", result.PathCollection.Entries[0].Name);
            Assert.AreEqual("848123342", result.PathCollection.Entries[1].Id);
            Assert.AreEqual("Documentation", result.PathCollection.Entries[1].Name);
        }

        [TestMethod]
        public async Task CreateWebLinkSharedLink_ValidResponse_ValidFile()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            var webLinksUri = new Uri(Constants.WebLinksEndpointString);
            Config.SetupGet(x => x.WebLinksEndpointUri).Returns(webLinksUri);
            Handler.Setup(h => h.ExecuteAsync<BoxWebLink>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWebLink>>(new BoxResponse<BoxWebLink>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxWebLinks/CreateWebLinkSharedLink200.json")
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            var sharedLink = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.collaborators,
                VanityName = "my-custom-vanity-name",
            };

            /*** Act ***/
            BoxWebLink w = await _webLinkManager.CreateSharedLinkAsync("12345", sharedLink);

            /*** Assert ***/
            Assert.AreEqual(new Uri("https://api.box.com/2.0/web_links/12345"), boxRequest.AbsoluteUri);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual("{\"shared_link\":{\"access\":\"collaborators\",\"vanity_name\":\"my-custom-vanity-name\"}}", boxRequest.Payload);

            Assert.AreEqual("5000948880", w.Id);
            Assert.AreEqual("3", w.SequenceId);
            Assert.AreEqual("3", w.ETag);
            Assert.AreEqual("https://www.box.com/s/rh935iit6ewrmw0unyul", w.SharedLink.Url);
            Assert.AreEqual("my-custom-vanity-name", w.SharedLink.VanityName);
        }

        [TestMethod]
        public async Task CreateWebLinkSharedLink_ShouldThrowArgumentException_WhenEditIsTrue()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxWebLink>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWebLink>>(new BoxResponse<BoxWebLink>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxWebLinks/CreateWebLinkSharedLink200.json")
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            var sharedLink = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.collaborators,
                VanityName = "my-custom-vanity-name",
                Permissions = new BoxPermissionsRequest
                {
                    Edit = true
                }
            };

            /*** Act && Assert ***/
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => { _ = await _webLinkManager.CreateSharedLinkAsync("12345", sharedLink); });
        }

        [TestMethod]
        public async Task DeleteWebLinkSharedLink_ValidResponse_ValidFile()
        {
            /*** Arrange ***/
            var responseString = "{ \"type\": \"web_link\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\", \"description\": \"a picture of tigers\", \"size\": 629644, \"path_collection\": { \"total_count\": 2, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" } ] }, \"created_at\": \"2012-12-12T10:55:30-08:00\", \"modified_at\": \"2012-12-12T11:04:26-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": null, \"parent\": { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" }, \"item_status\": \"active\" }";
            IBoxRequest boxRequest = null;
            var webLinksUri = new Uri(Constants.WebLinksEndpointString);
            Config.SetupGet(x => x.WebLinksEndpointUri).Returns(webLinksUri);
            Handler.Setup(h => h.ExecuteAsync<BoxWebLink>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWebLink>>(new BoxResponse<BoxWebLink>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxWebLink w = await _webLinkManager.DeleteSharedLinkAsync("12345");

            /*** Assert ***/
            Assert.AreEqual(new Uri("https://api.box.com/2.0/web_links/12345"), boxRequest.AbsoluteUri);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual("{\"shared_link\":null}", boxRequest.Payload);

            Assert.AreEqual("5000948880", w.Id);
            Assert.AreEqual("3", w.SequenceId);
            Assert.AreEqual("3", w.ETag);
            Assert.AreEqual("web_link", w.Type);
            Assert.IsNull(w.SharedLink);
        }
    }
}
