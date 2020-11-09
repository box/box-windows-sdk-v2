using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2;
using Box.V2.Exceptions;
using Newtonsoft.Json.Linq;
using System;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxMetadataManagerTest : BoxResourceManagerTest
    {
        private readonly BoxMetadataManager _metadataManager;

        public BoxMetadataManagerTest()
        {
            _metadataManager = new BoxMetadataManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task CreateFileMetadata_ValidResponse_ValidMetadata()
        {
            /*** Arrange ***/
            string responseString = @"{
                                            ""audience1"": ""internal"",
                                            ""documentType"": ""Q1 plans"",
                                            ""competitiveDocument"": ""no"",
                                            ""status"": ""active"",
                                            ""author"": ""Jones"",
                                            ""currentState"": ""proposal"",
                                            ""$type"": ""marketingCollateral-d086c908-2498-4d3e-8a1f-01e82bfc2abe"",
                                            ""$parent"": ""file_5010739061"",
                                            ""$id"": ""2094c584-68e1-475c-a581-534a4609594e"",
                                            ""$version"": 0,
                                            ""$typeVersion"": 0,
                                            ""$template"": ""marketingCollateral"",
                                            ""$scope"": ""enterprise_12345""
                                        }";

            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<Dictionary<string, object>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<Dictionary<string, object>>>(new BoxResponse<Dictionary<string, object>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            Dictionary<string, object> inputMetadata = new Dictionary<string, object>()
                                                        {
                                                            {"audience1", "internal"},
                                                            {"documentType", "Q1 plans"},
                                                            {"competitiveDocument", "no"},
                                                            {"status", "active"},
                                                            {"author", "Jones"},
                                                            {"currentState", "proposal"}
                                                        };
            Dictionary<string, object> metadata = await _metadataManager.CreateFileMetadataAsync("5010739061", inputMetadata, "enterprise", "marketingCollateral");

            /*** Assert ***/
            /***request***/
            Dictionary<string, object> payLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(boxRequest.Payload);
            foreach (string key in inputMetadata.Keys)
            {
                Assert.AreEqual(inputMetadata[key], payLoad[key]);
            }
            /***response***/
            Assert.AreEqual(inputMetadata["audience1"], metadata["audience1"]);
            Assert.AreEqual(inputMetadata["documentType"], metadata["documentType"]);
            Assert.AreEqual(inputMetadata["competitiveDocument"], metadata["competitiveDocument"]);
            Assert.AreEqual(inputMetadata["status"], metadata["status"]);
            Assert.AreEqual(inputMetadata["author"], metadata["author"]);
            Assert.AreEqual(inputMetadata["currentState"], metadata["currentState"]);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task GetFileMetadata_ValidResponse_ValidMetadata()
        {
            /*** Arrange ***/
            string responseString = @"{
                                        ""audience1"": ""internal"",
                                        ""documentType"": ""Q1 plans"",
                                        ""competitiveDocument"": ""no"",
                                        ""status"": ""active"",
                                        ""author"": ""Jones"",
                                        ""currentState"": ""proposal"",
                                        ""$type"": ""marketingCollateral-d086c908-2498-4d3e-8a1f-01e82bfc2abe"",
                                        ""$parent"": ""file_5010739061"",
                                        ""$id"": ""2094c584-68e1-475c-a581-534a4609594e"",
                                        ""$version"": 0,
                                        ""$typeVersion"": 0,
                                        ""$template"": ""marketingCollateral"",
                                        ""$scope"": ""enterprise_12345""
                                    }";

            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<Dictionary<string, object>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<Dictionary<string, object>>>(new BoxResponse<Dictionary<string, object>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            Dictionary<string, object> metadata = await _metadataManager.GetFileMetadataAsync("5010739061", "enterprise", "bandInfo");

            /*** Assert ***/
            /*** Request ***/
            Assert.AreEqual(string.Format("{0}/metadata/{1}/{2}", "5010739061", "enterprise", "bandInfo"), boxRequest.Path);
            /*** Response ***/
            Assert.AreEqual("internal", metadata["audience1"]);
            Assert.AreEqual("Q1 plans", metadata["documentType"]);
            Assert.AreEqual("no", metadata["competitiveDocument"]);
            Assert.AreEqual("active", metadata["status"]);
            Assert.AreEqual("Jones", metadata["author"]);
            Assert.AreEqual("proposal", metadata["currentState"]);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task GetAllFileMetadataTemplates_ValidResponse_ValidEntries()
        {
            /*** Arrange ***/
            string responseString = @"{
                                        ""entries"": [
                                            {
                                                ""currentDocumentStage"": ""Init"",
                                                ""$type"": ""documentFlow-452b4c9d-c3ad-4ac7-b1ad-9d5192f2fc5f"",
                                                ""$parent"": ""file_5010739061"",
                                                ""$id"": ""50ba0dba-0f89-4395-b867-3e057c1f6ed9"",
                                                ""$version"": 4,
                                                ""$typeVersion"": 2,
                                                ""needsApprovalFrom"": ""Smith"",
                                                ""$template"": ""documentFlow"",
                                                ""$scope"": ""enterprise_12345""
                                            },
                                            {
                                                ""$type"": ""productInfo-9d7b6993-b09e-4e52-b197-e42f0ea995b9"",
                                                ""$parent"": ""file_5010739061"",
                                                ""$id"": ""15d1014a-06c2-47ad-9916-014eab456194"",
                                                ""$version"": 2,
                                                ""$typeVersion"": 1,
                                                ""skuNumber"": 45334223,
                                                ""description"": ""Watch"",
                                                ""$template"": ""productInfo"",
                                                ""$scope"": ""enterprise_12345""
                                            },
                                            {
                                                ""Popularity"": ""25"",
                                                ""$type"": ""properties"",
                                                ""$parent"": ""file_5010739061"",
                                                ""$id"": ""b6f36cbc-fc7a-4eda-8889-130f350cc057"",
                                                ""$version"": 0,
                                                ""$typeVersion"": 2,
                                                ""$template"": ""properties"",
                                                ""$scope"": ""global""
                                            },

                                        ],
                                        ""limit"": 100
                                    }";

            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxMetadataTemplateCollection<Dictionary<string, object>>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxMetadataTemplateCollection<Dictionary<string, object>>>>(new BoxResponse<BoxMetadataTemplateCollection<Dictionary<string, object>>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxMetadataTemplateCollection<Dictionary<string, object>> result = await _metadataManager.GetAllFileMetadataTemplatesAsync("5010739061");

            /*** Request ***/
            Assert.AreEqual(string.Format("{0}/metadata", "5010739061"), boxRequest.Path);
            /*** Response ***/
            Assert.AreEqual("Init", result.Entries[0]["currentDocumentStage"]);
            Assert.AreEqual("50ba0dba-0f89-4395-b867-3e057c1f6ed9", result.Entries[0]["$id"]);
            Assert.AreEqual("file_5010739061", result.Entries[1]["$parent"]);
            Assert.AreEqual((long)2, result.Entries[1]["$version"]);
            Assert.AreEqual("25", result.Entries[2]["Popularity"]);
            Assert.AreEqual((long)2, result.Entries[2]["$typeVersion"]);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task UpdateFileMetadata_ValidResponse_ValidEntries()
        {
            /*** Arrange ***/
            string responseString = @"{
                                        ""audience1"": ""internal"",
                                        ""documentType"": ""Q1 plans"",
                                        ""status"": ""inactive"",
                                        ""author"": ""Jones"",
                                        ""$type"": ""marketingCollateral-d086c908-2498-4d3e-8a1f-01e82bfc2abe"",
                                        ""$parent"": ""file_5010739061"",
                                        ""$id"": ""2094c584-68e1-475c-a581-534a4609594e"",
                                        ""$version"": 1,
                                        ""$typeVersion"": 0,
                                        ""editor"": ""Jones"",
                                        ""previousState"": ""proposal"",
                                        ""currentState"": ""reviewed"",
                                        ""$template"": ""marketingCollateral"",
                                        ""$scope"": ""enterprise_12345""
                                    }";

            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<Dictionary<string, object>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<Dictionary<string, object>>>(new BoxResponse<Dictionary<string, object>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            List<BoxMetadataUpdate> updates = new List<BoxMetadataUpdate>()
            {
                new BoxMetadataUpdate()
                {
                    Op = MetadataUpdateOp.test,
                    Path = "/competitiveDocument",
                    Value = "no"
                },
                new BoxMetadataUpdate()
                {
                    Op = MetadataUpdateOp.remove,
                    Path = "/competitiveDocument"
                },
                new BoxMetadataUpdate()
                {
                    Op = MetadataUpdateOp.test,
                    Path = "/status",
                    Value = "active"
                },
                new BoxMetadataUpdate()
                {
                    Op = MetadataUpdateOp.replace,
                    Path = "/competitiveDocument",
                    Value = "inactive"
                },
                new BoxMetadataUpdate()
                {
                    Op = MetadataUpdateOp.test,
                    Path = "/author",
                    Value = "Jones"
                },
                new BoxMetadataUpdate()
                {
                    Op = MetadataUpdateOp.copy,
                    From="/author",
                    Path = "/editor"
                },
                new BoxMetadataUpdate()
                {
                    Op = MetadataUpdateOp.test,
                    Path = "/currentState",
                    Value = "proposal"
                },
                new BoxMetadataUpdate()
                {
                    Op = MetadataUpdateOp.move,
                    From = "/currentState",
                    Path = "/previousState"
                },
                new BoxMetadataUpdate()
                {
                    Op = MetadataUpdateOp.add,
                    Path = "/currentState",
                    Value = "reviewed"
                },
            };
            Dictionary<string, object> result = await _metadataManager.UpdateFileMetadataAsync("5010739061", updates, "enterprise", "marketingCollateral");

            /*** Assert ***/
            /***request***/
            List<BoxMetadataUpdate> payLoad = JsonConvert.DeserializeObject<List<BoxMetadataUpdate>>(boxRequest.Payload);
            for (int i = 0; i < payLoad.Count; i++)
            {
                Assert.AreEqual(updates[i].Op, payLoad[i].Op);
                Assert.AreEqual(updates[i].Path, payLoad[i].Path);
            }
            /***response***/
            Assert.AreEqual("internal", result["audience1"]);
            Assert.AreEqual("Q1 plans", result["documentType"]);
            Assert.AreEqual((long)1, result["$version"]);
            Assert.AreEqual("reviewed", result["currentState"]);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task SetFileMetadataAsync_Create_ValidResponse()
        {
            /*** Arrange ***/
            string responseString = @"{
                                        ""foo"": ""bar"",
                                        ""baz"": ""quux"",
                                        ""num"": 123,
                                        ""$type"": ""marketingCollateral-d086c908-2498-4d3e-8a1f-01e82bfc2abe"",
                                        ""$parent"": ""file_11111"",
                                        ""$id"": ""2094c584-68e1-475c-a581-534a4609594e"",
                                        ""$version"": 1,
                                        ""$typeVersion"": 0,
                                        ""$template"": ""marketingCollateral"",
                                        ""$scope"": ""enterprise_12345""
                                    }";

            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<Dictionary<string, object>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<Dictionary<string, object>>>(new BoxResponse<Dictionary<string, object>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            var md = new Dictionary<string, object>()
            {
                { "foo", "bar" },
                { "baz", "quux" },
                { "num", 123 }
            };

            /*** Act ***/
            var metadata = await _metadataManager.SetFileMetadataAsync("11111", md, "enterprise", "marketingCollateral");

            /*** Assert ***/
            Assert.AreEqual("bar", metadata["foo"]);
            Assert.AreEqual("quux", metadata["baz"]);
            Assert.AreEqual((System.Int64)123, metadata["num"]);

            Assert.AreEqual("https://api.box.com/2.0/files/11111/metadata/enterprise/marketingCollateral", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual("{\"foo\":\"bar\",\"baz\":\"quux\",\"num\":123}", boxRequest.Payload);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task SetFileMetadataAsync_Update_ValidResponse()
        {
            /*** Arrange ***/
            string responseString = @"{
                                        ""foo"": ""blargh"",
                                        ""baz"": ""quux"",
                                        ""num"": 456,
                                        ""$type"": ""marketingCollateral-d086c908-2498-4d3e-8a1f-01e82bfc2abe"",
                                        ""$parent"": ""file_11111"",
                                        ""$id"": ""2094c584-68e1-475c-a581-534a4609594e"",
                                        ""$version"": 1,
                                        ""$typeVersion"": 0,
                                        ""$template"": ""marketingCollateral"",
                                        ""$scope"": ""enterprise_12345""
                                    }";

            IBoxRequest boxRequest = null;
            var responses = new Queue<Task<IBoxResponse<Dictionary<string, object>>>>(new[]
            {
                Task.FromResult<IBoxResponse<Dictionary<string, object>>>(new BoxResponse<Dictionary<string, object>>()
                {
                    Status = ResponseStatus.Error,
                    StatusCode = System.Net.HttpStatusCode.Conflict,
                    ContentString = ""
                }),
                Task.FromResult<IBoxResponse<Dictionary<string, object>>>(new BoxResponse<Dictionary<string, object>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })
            });
            Handler.Setup(h => h.ExecuteAsync<Dictionary<string, object>>(It.IsAny<IBoxRequest>()))
                .Returns(() => responses.Dequeue())
                .Callback<IBoxRequest>(r => boxRequest = r);

            var md = new Dictionary<string, object>()
            {
                { "foo", "blargh" },
                { "num", 456 }
            };

            /*** Act ***/
            var metadata = await _metadataManager.SetFileMetadataAsync("11111", md, "enterprise", "marketingCollateral");

            /*** Assert ***/
            Assert.AreEqual("blargh", metadata["foo"]);
            Assert.AreEqual("quux", metadata["baz"]);
            Assert.AreEqual((System.Int64)456, metadata["num"]);

            Assert.AreEqual("https://api.box.com/2.0/files/11111/metadata/enterprise/marketingCollateral", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual("[{\"op\":\"add\",\"path\":\"/foo\",\"value\":\"blargh\"},{\"op\":\"add\",\"path\":\"/num\",\"value\":456}]", boxRequest.Payload);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task SetFileMetadataAsync_Create_Error()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<Dictionary<string, object>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<Dictionary<string, object>>>(new BoxResponse<Dictionary<string, object>>()
                {
                    Status = ResponseStatus.Error,
                    StatusCode = System.Net.HttpStatusCode.BadGateway,
                    ContentString = ""
                })).Callback<IBoxRequest>(r => boxRequest = r);

            var md = new Dictionary<string, object>()
            {
                { "foo", "bar" },
                { "baz", "quux" },
                { "num", 123 }
            };

            /*** Act ***/
            try
            {
                var metadata = await _metadataManager.SetFileMetadataAsync("11111", md, "enterprise", "marketingCollateral");

                Assert.Fail("Expected metadata set operation to throw when create operation throws with non-Conflict error");
            }
            catch (BoxException ex)
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadGateway, ex.StatusCode);
            }
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task SetFolderMetadataAsync_Create_ValidResponse()
        {
            /*** Arrange ***/
            string responseString = @"{
                                        ""foo"": ""bar"",
                                        ""baz"": ""quux"",
                                        ""num"": 123,
                                        ""$type"": ""marketingCollateral-d086c908-2498-4d3e-8a1f-01e82bfc2abe"",
                                        ""$parent"": ""folder_11111"",
                                        ""$id"": ""2094c584-68e1-475c-a581-534a4609594e"",
                                        ""$version"": 1,
                                        ""$typeVersion"": 0,
                                        ""$template"": ""marketingCollateral"",
                                        ""$scope"": ""enterprise_12345""
                                    }";

            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<Dictionary<string, object>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<Dictionary<string, object>>>(new BoxResponse<Dictionary<string, object>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            var md = new Dictionary<string, object>()
            {
                { "foo", "bar" },
                { "baz", "quux" },
                { "num", 123 }
            };

            /*** Act ***/
            var metadata = await _metadataManager.SetFolderMetadataAsync("11111", md, "enterprise", "marketingCollateral");

            /*** Assert ***/
            Assert.AreEqual("bar", metadata["foo"]);
            Assert.AreEqual("quux", metadata["baz"]);
            Assert.AreEqual((System.Int64)123, metadata["num"]);

            Assert.AreEqual("https://api.box.com/2.0/folders/11111/metadata/enterprise/marketingCollateral", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual("{\"foo\":\"bar\",\"baz\":\"quux\",\"num\":123}", boxRequest.Payload);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task SetFolderMetadataAsync_Update_ValidResponse()
        {
            /*** Arrange ***/
            string responseString = @"{
                                        ""foo"": ""blargh"",
                                        ""baz"": ""quux"",
                                        ""num"": 456,
                                        ""$type"": ""marketingCollateral-d086c908-2498-4d3e-8a1f-01e82bfc2abe"",
                                        ""$parent"": ""folder_11111"",
                                        ""$id"": ""2094c584-68e1-475c-a581-534a4609594e"",
                                        ""$version"": 1,
                                        ""$typeVersion"": 0,
                                        ""$template"": ""marketingCollateral"",
                                        ""$scope"": ""enterprise_12345""
                                    }";

            IBoxRequest boxRequest = null;
            var responses = new Queue<Task<IBoxResponse<Dictionary<string, object>>>>(new[]
            {
                Task.FromResult<IBoxResponse<Dictionary<string, object>>>(new BoxResponse<Dictionary<string, object>>()
                {
                    Status = ResponseStatus.Error,
                    StatusCode = System.Net.HttpStatusCode.Conflict,
                    ContentString = ""
                }),
                Task.FromResult<IBoxResponse<Dictionary<string, object>>>(new BoxResponse<Dictionary<string, object>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })
            });
            Handler.Setup(h => h.ExecuteAsync<Dictionary<string, object>>(It.IsAny<IBoxRequest>()))
                .Returns(() => responses.Dequeue())
                .Callback<IBoxRequest>(r => boxRequest = r);

            var md = new Dictionary<string, object>()
            {
                { "foo", "blargh" },
                { "num", 456 }
            };

            /*** Act ***/
            var metadata = await _metadataManager.SetFolderMetadataAsync("11111", md, "enterprise", "marketingCollateral");

            /*** Assert ***/
            Assert.AreEqual("blargh", metadata["foo"]);
            Assert.AreEqual("quux", metadata["baz"]);
            Assert.AreEqual((System.Int64)456, metadata["num"]);

            Assert.AreEqual("https://api.box.com/2.0/folders/11111/metadata/enterprise/marketingCollateral", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual("[{\"op\":\"add\",\"path\":\"/foo\",\"value\":\"blargh\"},{\"op\":\"add\",\"path\":\"/num\",\"value\":456}]", boxRequest.Payload);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task SetFolderMetadataAsync_Create_Error()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<Dictionary<string, object>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<Dictionary<string, object>>>(new BoxResponse<Dictionary<string, object>>()
                {
                    Status = ResponseStatus.Error,
                    StatusCode = System.Net.HttpStatusCode.BadGateway,
                    ContentString = ""
                })).Callback<IBoxRequest>(r => boxRequest = r);

            var md = new Dictionary<string, object>()
            {
                { "foo", "bar" },
                { "baz", "quux" },
                { "num", 123 }
            };

            /*** Act ***/
            try
            {
                var metadata = await _metadataManager.SetFolderMetadataAsync("11111", md, "enterprise", "marketingCollateral");

                Assert.Fail("Expected metadata set operation to throw when create operation throws with non-Conflict error");
            }
            catch (BoxException ex)
            {
                Assert.AreEqual(System.Net.HttpStatusCode.BadGateway, ex.StatusCode);
            }
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task ExecuteMetadataQuery_ValidResponse()
        {
            /*** Arrange ***/
            string responseString = "{\"entries\":[{\"item\":{\"type\":\"file\",\"id\":\"1617554169109\",\"file_version\":{\"type\":\"file_version\",\"id\":\"1451884469385\",\"sha1\":\"69888bb1bff455d1b2f8afea75ed1ff0b4879bf6\"},\"sequence_id\":\"0\",\"etag\":\"0\",\"sha1\":\"69888bb1bff455d1b2f8afea75ed1ff0b4879bf6\",\"name\":\"My Contract.docx\",\"description\":\"\",\"size\":25600,\"path_collection\":{\"total_count\":4,\"entries\":[{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"},{\"type\":\"folder\",\"id\":\"15017998644\",\"sequence_id\":\"0\",\"etag\":\"0\",\"name\":\"Contracts\"},{\"type\":\"folder\",\"id\":\"15286891196\",\"sequence_id\":\"1\",\"etag\":\"1\",\"name\":\"North America\"},{\"type\":\"folder\",\"id\":\"16125613433\",\"sequence_id\":\"0\",\"etag\":\"0\",\"name\":\"2017\"}]},\"created_at\":\"2017-04-20T12:55:27-07:00\",\"modified_at\":\"2017-04-20T12:55:27-07:00\",\"trashed_at\":null,\"purged_at\":null,\"content_created_at\":\"2017-01-06T17:59:01-08:00\",\"content_modified_at\":\"2017-01-06T17:59:01-08:00\",\"created_by\":{\"type\":\"user\",\"id\":\"193973366\",\"name\":\"Box Admin\",\"login\":\"admin@company.com\"},\"modified_by\":{\"type\":\"user\",\"id\":\"193973366\",\"name\":\"Box Admin\",\"login\":\"admin@company.com\"},\"owned_by\":{\"type\":\"user\",\"id\":\"193973366\",\"name\":\"Box Admin\",\"login\":\"admin@company.com\"},\"shared_link\":null,\"parent\":{\"type\":\"folder\",\"id\":\"16125613433\",\"sequence_id\":\"0\",\"etag\":\"0\",\"name\":\"2017\"},\"item_status\":\"active\"},\"metadata\":{\"enterprise_123456\":{\"someTemplate\":{\"$parent\":\"file_161753469109\",\"$version\":0,\"customerName\":\"Phoenix Corp\",\"$type\":\"someTemplate-3d5fcaca-f496-4bb6-9046-d25c37bc5594\",\"$typeVersion\":0,\"$id\":\"ba52e2cc-371d-4659-8d53-50f1ac642e35\",\"amount\":100,\"claimDate\":\"2016-04-10T00:00:00Z\",\"region\":\"West\",\"$typeScope\":\"enterprise_123456\"}}}}],\"next_marker\":\"AAAAAmVYB1FWec8GH6yWu2nwmanfMh07IyYInaa7DZDYjgO1H4KoLW29vPlLY173OKsci6h6xGh61gG73gnaxoS+o0BbI1/h6le6cikjlupVhASwJ2Cj0tOD9wlnrUMHHw3/ISf+uuACzrOMhN6d5fYrbidPzS6MdhJOejuYlvsg4tcBYzjauP3+VU51p77HFAIuObnJT0ff\"}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxMetadataQueryItem>>(It.IsAny<IBoxRequest>()))
                 .Returns(Task.FromResult<IBoxResponse<BoxCollectionMarkerBased<BoxMetadataQueryItem>>>(new BoxResponse<BoxCollectionMarkerBased<BoxMetadataQueryItem>>()
                 {
                     Status = ResponseStatus.Success,
                     ContentString = responseString
                 }))
                 .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var queryParams = new Dictionary<string, object>();
            queryParams.Add("arg", 100);
            List<BoxMetadataQueryOrderBy> orderByList = new List<BoxMetadataQueryOrderBy>();
            var orderBy = new BoxMetadataQueryOrderBy()
            {
                FieldKey = "amount",
                Direction = BoxSortDirection.ASC
            };
            orderByList.Add(orderBy);
            string marker = "q3f87oqf3qygou5t478g9gwrbul";
            BoxCollectionMarkerBased<BoxMetadataQueryItem> items = await _metadataManager.ExecuteMetadataQueryAsync(from: "enterprise_123456.someTemplate", query: "amount >= :arg", queryParameters: queryParams, ancestorFolderId: "5555", indexName: "amountAsc", orderBy: orderByList, marker: marker, autoPaginate: false);
            /*** Assert ***/

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(MetadataQueryUri, boxRequest.AbsoluteUri.AbsoluteUri);
            JObject payload = JObject.Parse(boxRequest.Payload);
            Assert.AreEqual("enterprise_123456.someTemplate", payload["from"]);
            Assert.AreEqual("amount >= :arg", payload["query"]);
            Assert.AreEqual(100, payload["query_params"]["arg"]);
            Assert.AreEqual("5555", payload["ancestor_folder_id"]);
            Assert.AreEqual("amountAsc", payload["use_index"]);
            JArray payloadOrderBy = JArray.Parse(payload["order_by"].ToString());
            Assert.AreEqual("amount", payloadOrderBy[0]["field_key"]);
            Assert.AreEqual("ASC", payloadOrderBy[0]["direction"]);
            Assert.AreEqual(marker, payload["marker"]);

            // Response check
            Assert.AreEqual(items.Entries[0].Item.Type, "file");
            Assert.AreEqual(items.Entries[0].Item.Id, "1617554169109");
            Assert.AreEqual(items.Entries[0].Item.Name, "My Contract.docx");
            Assert.AreEqual(items.Entries[0].Item.SequenceId, "0");
            Assert.AreEqual(items.Entries[0].Item.CreatedBy.Type, "user");
            Assert.AreEqual(items.Entries[0].Item.CreatedBy.Login, "admin@company.com");
            Assert.AreEqual(items.Entries[0].Item.Parent.Id, "16125613433");
            Assert.AreEqual(items.NextMarker, "AAAAAmVYB1FWec8GH6yWu2nwmanfMh07IyYInaa7DZDYjgO1H4KoLW29vPlLY173OKsci6h6xGh61gG73gnaxoS+o0BbI1/h6le6cikjlupVhASwJ2Cj0tOD9wlnrUMHHw3/ISf+uuACzrOMhN6d5fYrbidPzS6MdhJOejuYlvsg4tcBYzjauP3+VU51p77HFAIuObnJT0ff");
            var metadata = JObject.FromObject(items.Entries[0].Metadata["enterprise_123456"]);
            Assert.AreEqual(metadata["someTemplate"]["$parent"], "file_161753469109");
            Assert.AreEqual(metadata["someTemplate"]["customerName"], "Phoenix Corp");
            Assert.AreEqual(metadata["someTemplate"]["$typeVersion"], 0);
            Assert.AreEqual(metadata["someTemplate"]["region"], "West");
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task ExecuteMetadataQueryWithFields_ValidResponse()
        {
            /*** Arrange ***/
            string responseString = "{\"entries\":[{\"type\":\"file\",\"id\":\"1244738582\",\"etag\":\"1\",\"sha1\":\"012b5jdunwkfu438991344044\",\"name\":\"Very Important.docx\",\"metadata\":{\"enterprise_67890\":{\"catalogImages\":{\"$parent\":\"file_50347290\",\"$version\":2,\"$template\":\"catalogImages\",\"$scope\":\"enterprise_67890\",\"photographer\":\"Bob Dylan\"}}}},{\"type\":\"folder\",\"id\":\"124242482\",\"etag\":\"1\",\"sha1\":\"012b5ir8391344044\",\"name\":\"Also Important.docx\",\"metadata\":{\"enterprise_67890\":{\"catalogImages\":{\"$parent\":\"file_50427290\",\"$version\":2,\"$template\":\"catalogImages\",\"$scope\":\"enterprise_67890\",\"photographer\":\"Bob Dylan\"}}}}],\"limit\":2,\"next_marker\":\"0!WkeoDQ3mm5cI_RzSN--UOG1ICuw0gz3729kfhwuoagt54nbvqmgfhsygreh98nfu94344PpctrcgVa8AMIe7gRwSNBNloVR-XuGmfqTw\"}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxItem>>(It.IsAny<IBoxRequest>()))
                 .Returns(Task.FromResult<IBoxResponse<BoxCollectionMarkerBased<BoxItem>>>(new BoxResponse<BoxCollectionMarkerBased<BoxItem>>()
                 {
                     Status = ResponseStatus.Success,
                     ContentString = responseString
                 }))
                 .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var queryParams = new Dictionary<string, object>();
            queryParams.Add("arg", "Bob Dylan");
            List<string> fields = new List<string>();
            fields.Add("id");
            fields.Add("name");
            fields.Add("sha1");
            fields.Add("metadata.enterprise_240748.catalogImages.photographer");
            string marker = "q3f87oqf3qygou5t478g9gwrbul";
            BoxCollectionMarkerBased<BoxItem> items = await _metadataManager.ExecuteMetadataQueryAsync(from: "enterprise_67890.catalogImages", query: "photographer = :arg", fields: fields, queryParameters: queryParams, ancestorFolderId: "0", marker: marker, autoPaginate: false);
            /*** Assert ***/

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(MetadataQueryUri, boxRequest.AbsoluteUri.AbsoluteUri);
            JObject payload = JObject.Parse(boxRequest.Payload);
            Assert.AreEqual("enterprise_67890.catalogImages", payload["from"]);
            Assert.AreEqual("photographer = :arg", payload["query"]);
            Assert.AreEqual("0", payload["ancestor_folder_id"]);
            JArray payloadFields = JArray.Parse(payload["fields"].ToString());
            Assert.AreEqual("id", payloadFields[0]);
            Assert.AreEqual("name", payloadFields[1]);
            Assert.AreEqual("sha1", payloadFields[2]);
            Assert.AreEqual("metadata.enterprise_240748.catalogImages.photographer", payloadFields[3]);
            Assert.AreEqual(marker, payload["marker"]);

            // Response check
            Assert.AreEqual(items.Entries[0].Type, "file");
            Assert.AreEqual(items.Entries[0].Id, "1244738582");
            Assert.AreEqual(items.Entries[0].Name, "Very Important.docx");
            Assert.AreEqual(items.Entries[1].Type, "folder");
            Assert.AreEqual(items.Entries[1].Id, "124242482");
            Assert.AreEqual(items.Entries[1].Name, "Also Important.docx");
            BoxFile file = (BoxFile) items.Entries[0];
            Assert.AreEqual(file.Metadata["enterprise_67890"]["catalogImages"]["photographer"].Value, "Bob Dylan");
        }
    }
}
