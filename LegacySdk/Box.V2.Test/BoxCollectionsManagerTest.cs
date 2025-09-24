using System;
using System.Collections.Generic;
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
    public class BoxCollectionsManagerTest : BoxResourceManagerTest
    {
        private readonly BoxCollectionsManager _collectionsManager;

        public BoxCollectionsManagerTest()
        {
            _collectionsManager = new BoxCollectionsManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task CreateOrDeleteCollectionsForFolder_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                            ""type"": ""folder"",
                                            ""id"": ""11446498"",
                                            ""sequence_id"": ""1"",
                                            ""etag"": ""1"",
                                            ""name"": ""New Folder Name!"",
                                            ""created_at"": ""2012-12-12T10:53:43-08:00"",
                                            ""modified_at"": ""2012-12-12T11:15:04-08:00"",
                                            ""description"": ""Some pictures I took"",
                                            ""size"": 629644,
                                            ""path_collection"": {
                                                ""total_count"": 1,
                                                ""entries"": [
                                                    {
                                                        ""type"": ""folder"",
                                                        ""id"": ""0"",
                                                        ""sequence_id"": null,
                                                        ""etag"": null,
                                                        ""name"": ""All Files""
                                                    }
                                                ]
                                            },
                                            ""created_by"": {
                                                ""type"": ""user"",
                                                ""id"": ""17738362"",
                                                ""name"": ""sean rose"",
                                                ""login"": ""sean@box.com""
                                            },
                                            ""modified_by"": {
                                                ""type"": ""user"",
                                                ""id"": ""17738362"",
                                                ""name"": ""sean rose"",
                                                ""login"": ""sean@box.com""
                                            },
                                            ""owned_by"": {
                                                ""type"": ""user"",
                                                ""id"": ""17738362"",
                                                ""name"": ""sean rose"",
                                                ""login"": ""sean@box.com""
                                            },
                                            ""shared_link"": {
                                                ""url"": ""https://www.box.com/s/vspke7y05sb214wjokpk"",
                                                ""download_url"": null,
                                                ""vanity_url"": null,
                                                ""is_password_enabled"": false,
                                                ""unshared_at"": null,
                                                ""download_count"": 0,
                                                ""preview_count"": 0,
                                                ""access"": ""open"",
                                                ""permissions"": {
                                                    ""can_download"": true,
                                                    ""can_preview"": true
                                                }
                                            },
                                            ""folder_upload_email"": {
                                                ""access"": ""open"",
                                                ""email"": ""upload.Picture.k13sdz1@u.box.com""
                                            },
                                            ""parent"": {
                                                ""type"": ""folder"",
                                                ""id"": ""0"",
                                                ""sequence_id"": null,
                                                ""etag"": null,
                                                ""name"": ""All Files""
                                            },
                                            ""item_status"": ""active"",
                                            ""item_collection"": {
                                                ""total_count"": 1,
                                                ""entries"": [
                                                    {
                                                        ""type"": ""file"",
                                                        ""id"": ""5000948880"",
                                                        ""sequence_id"": ""3"",
                                                        ""etag"": ""3"",
                                                        ""sha1"": ""134b65991ed521fcfe4724b7d814ab8ded5185dc"",
                                                        ""name"": ""tigers.jpeg""
                                                    }
                                                ],
                                                ""offset"": 0,
                                                ""limit"": 100
                                            }
                                        }";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxFolder>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFolder>>(new BoxResponse<BoxFolder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var collectionsRequest = new BoxCollectionsRequest()
            {
                Collections = new List<BoxRequestEntity>()
                {
                    new BoxRequestEntity()
                        {
                            Id="123"
                        }
                }
            };
            BoxFolder result = await _collectionsManager.CreateOrDeleteCollectionsForFolderAsync("11446498", collectionsRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(FoldersUri + "11446498", boxRequest.AbsoluteUri.AbsoluteUri);
            BoxCollectionsRequest payload = JsonConvert.DeserializeObject<BoxCollectionsRequest>(boxRequest.Payload);
            Assert.AreEqual(collectionsRequest.Collections[0].Id, payload.Collections[0].Id);

            //Response check
            Assert.AreEqual("11446498", result.Id);
            Assert.AreEqual(1, result.PathCollection.TotalCount);
            Assert.AreEqual("https://www.box.com/s/vspke7y05sb214wjokpk", result.SharedLink.Url);
            Assert.AreEqual("17738362", result.CreatedBy.Id);

        }

        [TestMethod]
        public async Task CreateOrDeleteCollectionsForFile_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""file"",
                                        ""id"": ""5000948880"",
                                        ""file_version"": {
                                            ""type"": ""file_version"",
                                            ""id"": ""26261748416"",
                                            ""sha1"": ""134b65991ed521fcfe4724b7d814ab8ded5185dc""
                                        },
                                        ""sequence_id"": ""3"",
                                        ""etag"": ""3"",
                                        ""sha1"": ""134b65991ed521fcfe4724b7d814ab8ded5185dc"",
                                        ""name"": ""tigers.jpeg"",
                                        ""description"": ""a picture of tigers"",
                                        ""size"": 629644,
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
                                                    ""id"": ""11446498"",
                                                    ""sequence_id"": ""1"",
                                                    ""etag"": ""1"",
                                                    ""name"": ""Pictures""
                                                }
                                            ]
                                        },
                                        ""created_at"": ""2012-12-12T10:55:30-08:00"",
                                        ""modified_at"": ""2012-12-12T11:04:26-08:00"",
                                        ""created_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""17738362"",
                                            ""name"": ""sean rose"",
                                            ""login"": ""sean@box.com""
                                        },
                                        ""modified_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""17738362"",
                                            ""name"": ""sean rose"",
                                            ""login"": ""sean@box.com""
                                        },
                                        ""owned_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""17738362"",
                                            ""name"": ""sean rose"",
                                            ""login"": ""sean@box.com""
                                        },
                                        ""shared_link"": {
                                            ""url"": ""https://www.box.com/s/rh935iit6ewrmw0unyul"",
                                            ""download_url"": ""https://www.box.com/shared/static/rh935iit6ewrmw0unyul.jpeg"",
                                            ""vanity_url"": null,
                                            ""is_password_enabled"": false,
                                            ""unshared_at"": null,
                                            ""download_count"": 0,
                                            ""preview_count"": 0,
                                            ""access"": ""open"",
                                            ""permissions"": {
                                                ""can_download"": true,
                                                ""can_preview"": true
                                            }
                                        },
                                        ""parent"": {
                                            ""type"": ""folder"",
                                            ""id"": ""11446498"",
                                            ""sequence_id"": ""1"",
                                            ""etag"": ""1"",
                                            ""name"": ""Pictures""
                                        },
                                        ""item_status"": ""active""
                                    }";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var collectionsRequest = new BoxCollectionsRequest()
            {
                Collections = new List<BoxRequestEntity>()
                {
                    new BoxRequestEntity()
                        {
                            Id="123"
                        }
                }
            };
            BoxFile result = await _collectionsManager.CreateOrDeleteCollectionsForFileAsync("5000948880", collectionsRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(FilesUri + "5000948880", boxRequest.AbsoluteUri.AbsoluteUri);
            BoxCollectionsRequest payload = JsonConvert.DeserializeObject<BoxCollectionsRequest>(boxRequest.Payload);
            Assert.AreEqual(collectionsRequest.Collections[0].Id, payload.Collections[0].Id);

            //Response check
            Assert.AreEqual("5000948880", result.Id);
            Assert.AreEqual(2, result.PathCollection.TotalCount);
            Assert.AreEqual("https://www.box.com/s/rh935iit6ewrmw0unyul", result.SharedLink.Url);
            Assert.AreEqual("17738362", result.CreatedBy.Id);

        }

        [TestMethod]
        public async Task GetCollections_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                            ""total_count"": 1,
                                            ""entries"": [
                                                {
                                                    ""type"": ""collection"",
                                                    ""id"": ""405151"",
                                                    ""name"": ""Favorites"",
                                                    ""collection_type"": ""favorites""
                                                }
                                            ],
                                            ""limit"": 100,
                                            ""offset"": 0
                                        }";
            IBoxRequest boxRequest = null;
            var collectionUri = new Uri(Constants.CollectionsEndpointString);
            Config.SetupGet(x => x.CollectionsEndpointUri).Returns(collectionUri);
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxCollectionItem>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxCollectionItem>>>(new BoxResponse<BoxCollection<BoxCollectionItem>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/

            BoxCollection<BoxCollectionItem> result = await _collectionsManager.GetCollectionsAsync();

            /*** Assert ***/

            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(collectionUri, boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(1, result.TotalCount);
            Assert.IsNotNull(result.Entries);
            Assert.AreEqual(1, result.Entries.Count);
            Assert.AreEqual("collection", result.Entries[0].Type);
            Assert.AreEqual("405151", result.Entries[0].Id);
            Assert.AreEqual("Favorites", result.Entries[0].Name);
            Assert.AreEqual("favorites", result.Entries[0].CollectionType);
            Assert.AreEqual(100, result.Limit);
            Assert.AreEqual(0, result.Offset);

        }

        [TestMethod]
        public async Task GetCollectionItems_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                            ""total_count"": 24,
                                            ""entries"": [
                                                {
                                                    ""type"": ""folder"",
                                                    ""id"": ""192429928"",
                                                    ""sequence_id"": ""1"",
                                                    ""etag"": ""1"",
                                                    ""name"": ""Stephen Curry Three Pointers""
                                                },
                                                {
                                                    ""type"": ""file"",
                                                    ""id"": ""818853862"",
                                                    ""sequence_id"": ""0"",
                                                    ""etag"": ""0"",
                                                    ""name"": ""Warriors.jpg""
                                                }
                                            ],
                                            ""offset"": 0,
                                            ""limit"": 2
                                        }";
            IBoxRequest boxRequest = null;
            var collectionUri = new Uri(Constants.CollectionsEndpointString);
            Config.SetupGet(x => x.CollectionsEndpointUri).Returns(collectionUri);
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxItem>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxItem>>>(new BoxResponse<BoxCollection<BoxItem>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/

            BoxCollection<BoxItem> result = await _collectionsManager.GetCollectionItemsAsync("405151", 2);

            /*** Assert ***/

            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(collectionUri + "405151/items?limit=2&offset=0", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(24, result.TotalCount);
            Assert.IsNotNull(result.Entries);
            Assert.AreEqual(2, result.Entries.Count);
            Assert.AreEqual("folder", result.Entries[0].Type);
            Assert.AreEqual("192429928", result.Entries[0].Id);
            Assert.AreEqual("1", result.Entries[0].ETag);
            Assert.AreEqual("Stephen Curry Three Pointers", result.Entries[0].Name);
            Assert.AreEqual("file", result.Entries[1].Type);
            Assert.AreEqual("818853862", result.Entries[1].Id);
            Assert.AreEqual("0", result.Entries[1].ETag);
            Assert.AreEqual("Warriors.jpg", result.Entries[1].Name);
            Assert.AreEqual(2, result.Limit);
            Assert.AreEqual(0, result.Offset);

        }
    }
}
