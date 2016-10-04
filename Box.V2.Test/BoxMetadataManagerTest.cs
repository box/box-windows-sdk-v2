using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxMetadataManagerTest : BoxResourceManagerTest
    {
        protected BoxMetadataManager _metadataManager;

        public BoxMetadataManagerTest()
        {
            _metadataManager = new BoxMetadataManager(_config.Object, _service, _converter, _authRepository);
        }

        [TestMethod]
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
            _handler.Setup(h => h.ExecuteAsync<Dictionary<string, object>>(It.IsAny<IBoxRequest>()))
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
            _handler.Setup(h => h.ExecuteAsync<Dictionary<string, object>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<Dictionary<string, object>>>(new BoxResponse<Dictionary<string, object>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            Dictionary<string, object> metadata = await _metadataManager.GetFileMetadataAsync("5010739061", "enterprise", "bandInfo");

            /*** Assert ***/
            /*** Request ***/
            Assert.AreEqual(string.Format("{0}/metadata/{1}/{2}","5010739061", "enterprise", "bandInfo"), boxRequest.Path);
            /*** Response ***/
            Assert.AreEqual("internal", metadata["audience1"]);
            Assert.AreEqual("Q1 plans", metadata["documentType"]);
            Assert.AreEqual("no", metadata["competitiveDocument"]);
            Assert.AreEqual("active", metadata["status"]);
            Assert.AreEqual("Jones", metadata["author"]);
            Assert.AreEqual("proposal", metadata["currentState"]);
        }

        [TestMethod]
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
            _handler.Setup(h => h.ExecuteAsync<BoxMetadataTemplateCollection<Dictionary<string, object>>>(It.IsAny<IBoxRequest>()))
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
            _handler.Setup(h => h.ExecuteAsync<Dictionary<string, object>>(It.IsAny<IBoxRequest>()))
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
    }
}
