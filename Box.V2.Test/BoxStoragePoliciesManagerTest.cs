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
    public class BoxStoragePoliciesManagerTest : BoxResourceManagerTest
    {
        private readonly BoxStoragePoliciesManager _storagePoliciesManager;

        public BoxStoragePoliciesManagerTest()
        {
            _storagePoliciesManager = new BoxStoragePoliciesManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task GetStoragePolicy_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""storage_policy"",
                                        ""id"": ""2698512"",
                                        ""name"": ""AWS Frankfurt / AWS Dublin with in region Uploads/Downloads/Previews""
                                    }";
            IBoxRequest boxRequest = null;
            var storagePoliciesUri = new Uri(Constants.StoragePoliciesEndpointString);
            Config.SetupGet(x => x.StoragePoliciesUri).Returns(storagePoliciesUri);
            Handler.Setup(h => h.ExecuteAsync<BoxStoragePolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxStoragePolicy>>(new BoxResponse<BoxStoragePolicy>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxStoragePolicy result = await _storagePoliciesManager.GetStoragePolicyAsync("1234");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(storagePoliciesUri + "1234", boxRequest.AbsoluteUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetStoragePolicies_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""next_marker"": null,
                                        ""limit"": 1000,
                                        ""entries"": [
                                            {
                                                ""type"": ""storage_policy"",
                                                ""id"": ""1234"",
                                                ""name"": ""AWS Montreal / AWS Dublin""
                                            },
                                            {
                                                ""type"": ""storage_policy"",
                                                ""id"": ""5678"",
                                                ""name"": ""AWS Frankfurt / AWS Dublin with in region Uploads/Downloads/Previews""
                                            }
                                        ]
                                    }";

            IBoxRequest boxRequest = null;
            var storagePoliciesUri = new Uri(Constants.StoragePoliciesEndpointString);
            Config.SetupGet(x => x.StoragePoliciesUri).Returns(storagePoliciesUri);
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxStoragePolicy>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollectionMarkerBased<BoxStoragePolicy>>>(new BoxResponse<BoxCollectionMarkerBased<BoxStoragePolicy>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCollectionMarkerBased<BoxStoragePolicy> result = await _storagePoliciesManager.GetListStoragePoliciesAsync();

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(storagePoliciesUri + "?limit=100", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual("1234", result.Entries[0].Id);
            Assert.AreEqual("AWS Montreal / AWS Dublin", result.Entries[0].Name);
            Assert.AreEqual("5678", result.Entries[1].Id);
            Assert.AreEqual("AWS Frankfurt / AWS Dublin with in region Uploads/Downloads/Previews", result.Entries[1].Name);

        }

        [TestMethod]
        public async Task CreateAssignment_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""storage_policy_assignment"",
                                        ""id"": ""user_5678"",
                                        ""storage_policy"": {
                                            ""type"": ""storage_policy"",
                                            ""id"": ""1234"",
                                        },
                                        ""assigned_to"": {
                                            ""type"": ""user"",
                                            ""id"": ""5678"",
                                        }
                                    }";

            IBoxRequest boxRequest = null;
            var storagePolicyAssignmentsForTargetUri = new Uri(Constants.StoragePolicyAssignmentsForTargetEndpointString);
            Config.SetupGet(x => x.StoragePolicyAssignmentsForTargetUri).Returns(storagePolicyAssignmentsForTargetUri);
            Handler.Setup(h => h.ExecuteAsync<BoxStoragePolicyAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxStoragePolicyAssignment>>(new BoxResponse<BoxStoragePolicyAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxStoragePolicyAssignment result = await _storagePoliciesManager.CreateAssignmentAsync("5678", "1234");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(storagePolicyAssignmentsForTargetUri, boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual("storage_policy_assignment", result.Type);
            Assert.AreEqual("user_5678", result.Id);
            Assert.IsNotNull(result.BoxStoragePolicy);
            Assert.IsNotNull(result.AssignedTo);
        }

        [TestMethod]
        public async Task GetAssignment_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""entries"": [
                                            {
                                                ""type"": ""storage_policy_assignment"",
                                                ""id"": ""user_5678"",
                                                ""storage_policy"": {
                                                    ""type"": ""storage_policy"",
                                                    ""id"": ""1234"",
                                                },
                                                ""assigned_to"": {
                                                    ""type"": ""user"",
                                                    ""id"": ""5678"",
                                                }
                                            }
                                        ]
                                    }";

            IBoxRequest boxRequest = null;
            var storagePolicyAssignmentsForTargetUri = new Uri(Constants.StoragePolicyAssignmentsForTargetEndpointString);
            Config.SetupGet(x => x.StoragePolicyAssignmentsForTargetUri).Returns(storagePolicyAssignmentsForTargetUri);
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxStoragePolicyAssignment>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollectionMarkerBased<BoxStoragePolicyAssignment>>>(new BoxResponse<BoxCollectionMarkerBased<BoxStoragePolicyAssignment>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxStoragePolicyAssignment result = await _storagePoliciesManager.GetAssignmentForTargetAsync("5678");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(storagePolicyAssignmentsForTargetUri + "?resolved_for_type=user&resolved_for_id=5678", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual("storage_policy_assignment", result.Type);
            Assert.AreEqual("user_5678", result.Id);
            Assert.IsNotNull(result.BoxStoragePolicy);
            Assert.IsNotNull(result.AssignedTo);
        }

        [TestMethod]
        public async Task GetAssignmentWithID_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""storage_policy_assignment"",
                                        ""id"": ""user_5678"",
                                        ""storage_policy"": {
                                            ""type"": ""storage_policy"",
                                            ""id"": ""1234"",
                                        },
                                        ""assigned_to"": {
                                            ""type"": ""user"",
                                            ""id"": ""5678"",
                                        }
                                    }";

            IBoxRequest boxRequest = null;
            var storagePolicyAssignmentsUri = new Uri(Constants.StoragePolicyAssignmentsEndpointString);
            Config.SetupGet(x => x.StoragePolicyAssignmentsUri).Returns(storagePolicyAssignmentsUri);
            Handler.Setup(h => h.ExecuteAsync<BoxStoragePolicyAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxStoragePolicyAssignment>>(new BoxResponse<BoxStoragePolicyAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxStoragePolicyAssignment result = await _storagePoliciesManager.GetAssignmentAsync("user_5678");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(storagePolicyAssignmentsUri + "user_5678", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual("storage_policy_assignment", result.Type);
            Assert.AreEqual("user_5678", result.Id);
        }

        [TestMethod]
        public async Task UpdateAssignment_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""storage_policy_assignment"",
                                        ""id"": ""user_5678"",
                                        ""storage_policy"": {
                                            ""type"": ""storage_policy"",
                                            ""id"": ""1111"",
                                        },
                                        ""assigned_to"": {
                                            ""type"": ""user"",
                                            ""id"": ""5678"",
                                        }
                                    }";

            IBoxRequest boxRequest = null;
            var storagePolicyAssignmentsUri = new Uri(Constants.StoragePolicyAssignmentsEndpointString);
            Config.SetupGet(x => x.StoragePolicyAssignmentsUri).Returns(storagePolicyAssignmentsUri);
            Handler.Setup(h => h.ExecuteAsync<BoxStoragePolicyAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxStoragePolicyAssignment>>(new BoxResponse<BoxStoragePolicyAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxStoragePolicyAssignment result = await _storagePoliciesManager.UpdateStoragePolicyAssignment("1111", "user+5678");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(storagePolicyAssignmentsUri + "1111", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual("1111", result.BoxStoragePolicy.Id);
        }

        [TestMethod]
        public async Task DeleteAssignment_ValidResponse()
        {
            var responseString = "";
            IBoxRequest boxRequest = null;
            var storagePolicyAssignmentsUri = new Uri(Constants.StoragePolicyAssignmentsEndpointString);
            Config.SetupGet(x => x.StoragePolicyAssignmentsUri).Returns(storagePolicyAssignmentsUri);
            Handler.Setup(h => h.ExecuteAsync<BoxStoragePolicyAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxStoragePolicyAssignment>>(new BoxResponse<BoxStoragePolicyAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var result = await _storagePoliciesManager.DeleteAssignmentAsync("user_5678");

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(storagePolicyAssignmentsUri + "user_5678", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task Assign_SameStoragePolicy_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""next_marker"": null,
                                        ""limit"": 1000,
                                        ""entries"": [
                                            {
                                                ""type"": ""storage_policy_assignment"",
                                                ""id"": ""user_1234"",
                                                ""storage_policy"": {
                                                    ""type"": ""storage_policy"",
                                                    ""id"": ""5678""
                                                },
                                                ""assigned_to"": {
                                                    ""type"": ""enterprise"",
                                                    ""id"": ""1111""
                                                }
                                            }
                                        ]
                                    }";

            IBoxRequest boxRequest = null;
            var storagePolicyAssignmentsUri = new Uri(Constants.StoragePolicyAssignmentsEndpointString);
            Config.SetupGet(x => x.StoragePolicyAssignmentsUri).Returns(storagePolicyAssignmentsUri);
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxStoragePolicyAssignment>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollectionMarkerBased<BoxStoragePolicyAssignment>>>(new BoxResponse<BoxCollectionMarkerBased<BoxStoragePolicyAssignment>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var result = await _storagePoliciesManager.AssignAsync("1234", "5678");
            //Response check
            Assert.AreEqual("storage_policy_assignment", result.Type);
            Assert.AreEqual("user_1234", result.Id);
        }

        [TestMethod]
        public async Task Assign_DifferentStoragePolicy_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""next_marker"": null,
                                        ""limit"": 1000,
                                        ""entries"": [
                                            {
                                                ""type"": ""storage_policy_assignment"",
                                                ""id"": ""user_5678"",
                                                ""storage_policy"": {
                                                    ""type"": ""storage_policy"",
                                                    ""id"": ""5678""
                                                },
                                                ""assigned_to"": {
                                                    ""type"": ""enterprise"",
                                                    ""id"": ""1111""
                                                }
                                            }
                                        ]
                                    }";

            IBoxRequest boxRequest = null;
            var storagePolicyAssignmentsUri = new Uri(Constants.StoragePolicyAssignmentsEndpointString);
            Config.SetupGet(x => x.StoragePolicyAssignmentsUri).Returns(storagePolicyAssignmentsUri);
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxStoragePolicyAssignment>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollectionMarkerBased<BoxStoragePolicyAssignment>>>(new BoxResponse<BoxCollectionMarkerBased<BoxStoragePolicyAssignment>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Arrange ***/
            var putResponseString = @"{
                                        ""type"": ""storage_policy_assignment"",
                                        ""id"": ""user_7777"",
                                        ""storage_policy"": {
                                            ""type"": ""storage_policy"",
                                            ""id"": ""1111"",
                                        },
                                        ""assigned_to"": {
                                            ""type"": ""user"",
                                            ""id"": ""7777"",
                                        }
                                    }";


            IBoxRequest putBoxRequest = null;
            Config.SetupGet(x => x.StoragePolicyAssignmentsUri).Returns(storagePolicyAssignmentsUri);
            Handler.Setup(h => h.ExecuteAsync<BoxStoragePolicyAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxStoragePolicyAssignment>>(new BoxResponse<BoxStoragePolicyAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = putResponseString
                }))
                .Callback<IBoxRequest>(r => putBoxRequest = r);

            /*** Act ***/
            var result = await _storagePoliciesManager.AssignAsync("1111", "7777");

            //Response check
            Assert.AreEqual("storage_policy_assignment", result.Type);
            Assert.AreEqual("user_7777", result.Id);
            Assert.AreEqual("storage_policy", result.BoxStoragePolicy.Type);
            Assert.AreEqual("1111", result.BoxStoragePolicy.Id);
            Assert.AreEqual("user", result.AssignedTo.Type);
            Assert.AreEqual("7777", result.AssignedTo.Id);
        }
    }
}
