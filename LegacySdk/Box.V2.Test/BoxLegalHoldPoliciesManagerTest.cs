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
    public class BoxLegalHoldPoliciesManagerTest : BoxResourceManagerTest
    {
        private readonly BoxLegalHoldPoliciesManager _legalHoldPoliciesManager;

        public BoxLegalHoldPoliciesManagerTest()
        {
            _legalHoldPoliciesManager = new BoxLegalHoldPoliciesManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task GetLegalHoldPolicy_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                          ""type"": ""legal_hold_policy"",
                                          ""id"": ""166757"",
                                          ""policy_name"": ""Policy 4"",
                                          ""description"": ""Postman created policy"",
                                          ""status"": ""active"",
                                          ""assignment_counts"": {
                                            ""user"": 1,
                                            ""folder"": 0,
                                            ""file"": 0,
                                            ""file_version"": 0
                                          },
                                          ""created_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""2030388321"",
                                            ""name"": ""Steve Boxuser"",
                                            ""login"": ""steve@box.com""
                                          },
                                          ""created_at"": ""2016-05-18T10:28:45-07:00"",
                                          ""modified_at"": ""2016-05-18T11:25:59-07:00"",
                                          ""deleted_at"": null,
                                          ""filter_started_at"": ""2016-05-17T01:00:00-07:00"",
                                          ""filter_ended_at"": ""2016-05-21T01:00:00-07:00""
                                        }";
            IBoxRequest boxRequest = null;
            var legalHoldsPoliciesUri = new Uri(Constants.LegalHoldPoliciesEndpointString);
            Config.SetupGet(x => x.LegalHoldPoliciesEndpointUri).Returns(legalHoldsPoliciesUri);
            Handler.Setup(h => h.ExecuteAsync<BoxLegalHoldPolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxLegalHoldPolicy>>(new BoxResponse<BoxLegalHoldPolicy>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxLegalHoldPolicy result = await _legalHoldPoliciesManager.GetLegalHoldPolicyAsync("166757");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(legalHoldsPoliciesUri + "166757", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual("legal_hold_policy", result.Type);
            Assert.AreEqual("166757", result.Id);
            Assert.AreEqual("Policy 4", result.PolicyName);
            Assert.AreEqual("Postman created policy", result.Description);
            Assert.AreEqual("active", result.Status);
            Assert.AreEqual(1, result.AssignmentCounts.User);
            Assert.AreEqual(0, result.AssignmentCounts.Folder);
            Assert.AreEqual(0, result.AssignmentCounts.File);
            Assert.AreEqual(0, result.AssignmentCounts.Version);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-18T10:28:45-07:00"), result.CreatedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-18T11:25:59-07:00"), result.ModifiedAt);
            Assert.IsNull(result.DeletedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-17T01:00:00-07:00"), result.FilterStartedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-21T01:00:00-07:00"), result.FilterEndedAt);

        }

        [TestMethod]
        public async Task GetListLegalHoldPolicies_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                          ""entries"": [
                                            {
                                              ""type"": ""legal_hold_policy"",
                                              ""id"": ""166877"",
                                              ""policy_name"": ""Policy 1""
                                            },
                                            {
                                              ""type"": ""legal_hold_policy"",
                                              ""id"": ""166881"",
                                              ""policy_name"": ""Policy 2""
                                            }
                                          ],
                                          ""limit"": 3,
                                          ""order"": [
                                            {
                                              ""by"": ""policy_name"",
                                              ""direction"": ""ASC""
                                            }
                                          ]
                                        }";
            IBoxRequest boxRequest = null;
            var legalHoldsPoliciesUri = new Uri(Constants.LegalHoldPoliciesEndpointString);
            Config.SetupGet(x => x.LegalHoldPoliciesEndpointUri).Returns(legalHoldsPoliciesUri);
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxLegalHoldPolicy>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollectionMarkerBased<BoxLegalHoldPolicy>>>(new BoxResponse<BoxCollectionMarkerBased<BoxLegalHoldPolicy>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCollectionMarkerBased<BoxLegalHoldPolicy> result = await _legalHoldPoliciesManager.GetListLegalHoldPoliciesAsync("pol");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(legalHoldsPoliciesUri + "?policy_name=pol&limit=100", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(2, result.Entries.Count);
            Assert.AreEqual(3, result.Limit);
            Assert.AreEqual(BoxSortBy.policy_name, result.Order[0].By);
            Assert.AreEqual(BoxSortDirection.ASC, result.Order[0].Direction);
            Assert.AreEqual("legal_hold_policy", result.Entries[0].Type);
            Assert.AreEqual("166877", result.Entries[0].Id);
            Assert.AreEqual("Policy 1", result.Entries[0].PolicyName);
            Assert.AreEqual("legal_hold_policy", result.Entries[1].Type);
            Assert.AreEqual("166881", result.Entries[1].Id);
            Assert.AreEqual("Policy 2", result.Entries[1].PolicyName);

        }

        [TestMethod]
        public async Task CreateLegalHoldPolicy_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                          ""type"": ""legal_hold_policy"",
                                          ""id"": ""166921"",
                                          ""policy_name"": ""Policy 3"",
                                          ""description"": ""postman created policy"",
                                          ""created_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""2030388321"",
                                            ""name"": ""Ryan Churchill"",
                                            ""login"": ""rchurchill+deventerprise@box.com""
                                          },
                                          ""created_at"": ""2016-05-18T16:18:49-07:00"",
                                          ""modified_at"": ""2016-05-18T16:18:49-07:00"",
                                          ""deleted_at"": null,
                                          ""filter_started_at"": ""2016-05-11T01:00:00-07:00"",
                                          ""filter_ended_at"": ""2016-05-13T01:00:00-07:00""
                                        }";
            IBoxRequest boxRequest = null;
            var legalHoldsPoliciesUri = new Uri(Constants.LegalHoldPoliciesEndpointString);
            Config.SetupGet(x => x.LegalHoldPoliciesEndpointUri).Returns(legalHoldsPoliciesUri);
            Handler.Setup(h => h.ExecuteAsync<BoxLegalHoldPolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxLegalHoldPolicy>>(new BoxResponse<BoxLegalHoldPolicy>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var createRequest = new BoxLegalHoldPolicyRequest()
            {
                PolicyName = "Policy 3",
                Description = "postman created policy",
                FilterStartedAt = DateTimeOffset.Parse("2016-05-11T00:00:00-08:00"),
                FilterEndedAt = DateTimeOffset.Parse("2016-05-13T00:00:00-08:00")

            };
            BoxLegalHoldPolicy result = await _legalHoldPoliciesManager.CreateLegalHoldPolicyAsync(createRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(legalHoldsPoliciesUri, boxRequest.AbsoluteUri.AbsoluteUri);
            BoxLegalHoldPolicyRequest payLoad = JsonConvert.DeserializeObject<BoxLegalHoldPolicyRequest>(boxRequest.Payload);
            Assert.AreEqual("Policy 3", payLoad.PolicyName);
            Assert.AreEqual("postman created policy", payLoad.Description);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-11T00:00:00-08:00"), payLoad.FilterStartedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-13T00:00:00-08:00"), payLoad.FilterEndedAt);

            //Response check
            Assert.AreEqual("legal_hold_policy", result.Type);
            Assert.AreEqual("166921", result.Id);
            Assert.AreEqual("Policy 3", result.PolicyName);
            Assert.AreEqual("postman created policy", result.Description);
            Assert.IsNull(result.Status);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-18T16:18:49-07:00"), result.CreatedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-18T16:18:49-07:00"), result.ModifiedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-11T01:00:00-07:00"), result.FilterStartedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-13T01:00:00-07:00"), result.FilterEndedAt);

        }

        [TestMethod]
        public async Task UpdateLegalHoldPolicy_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                          ""type"": ""legal_hold_policy"",
                                          ""id"": ""166921"",
                                          ""policy_name"": ""New Policy 3"",
                                          ""description"": ""Policy 3 New Description"",
                                          ""created_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""2030388321"",
                                            ""name"": ""Ryan Churchill"",
                                            ""login"": ""rchurchill+deventerprise@box.com""
                                          },
                                          ""created_at"": ""2016-05-18T16:18:49-07:00"",
                                          ""modified_at"": ""2016-05-18T16:20:47-07:00"",
                                          ""deleted_at"": null,
                                          ""filter_started_at"": ""2016-05-11T01:00:00-07:00"",
                                          ""filter_ended_at"": ""2016-05-13T01:00:00-07:00""
                                        }";
            IBoxRequest boxRequest = null;
            var legalHoldsPoliciesUri = new Uri(Constants.LegalHoldPoliciesEndpointString);
            Config.SetupGet(x => x.LegalHoldPoliciesEndpointUri).Returns(legalHoldsPoliciesUri);
            Handler.Setup(h => h.ExecuteAsync<BoxLegalHoldPolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxLegalHoldPolicy>>(new BoxResponse<BoxLegalHoldPolicy>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var updateRequest = new BoxLegalHoldPolicyRequest()
            {
                PolicyName = "New Policy 3",
                Description = "Policy 3 New Description"
            };
            BoxLegalHoldPolicy result = await _legalHoldPoliciesManager.UpdateLegalHoldPolicyAsync("166921", updateRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(legalHoldsPoliciesUri + "166921", boxRequest.AbsoluteUri.AbsoluteUri);
            BoxLegalHoldPolicyRequest payLoad = JsonConvert.DeserializeObject<BoxLegalHoldPolicyRequest>(boxRequest.Payload);
            Assert.AreEqual("New Policy 3", payLoad.PolicyName);
            Assert.AreEqual("Policy 3 New Description", payLoad.Description);

            //Response check
            Assert.AreEqual("legal_hold_policy", result.Type);
            Assert.AreEqual("166921", result.Id);
            Assert.AreEqual("New Policy 3", result.PolicyName);
            Assert.AreEqual("Policy 3 New Description", result.Description);
            Assert.IsNull(result.Status);
            Assert.AreEqual("2030388321", result.CreatedBy.Id);
            Assert.AreEqual("Ryan Churchill", result.CreatedBy.Name);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-18T16:18:49-07:00"), result.CreatedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-18T16:20:47-07:00"), result.ModifiedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-11T01:00:00-07:00"), result.FilterStartedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-13T01:00:00-07:00"), result.FilterEndedAt);

        }

        [TestMethod]
        public async Task DeleteTask_TaskDeleted()
        {
            /*** Arrange ***/
            var responseString = "";
            IBoxRequest boxRequest = null;
            var legalHoldsPoliciesUri = new Uri(Constants.LegalHoldPoliciesEndpointString);
            Config.SetupGet(x => x.LegalHoldPoliciesEndpointUri).Returns(legalHoldsPoliciesUri);
            Handler.Setup(h => h.ExecuteAsync<BoxLegalHoldPolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxLegalHoldPolicy>>(new BoxResponse<BoxLegalHoldPolicy>()
                {
                    Status = ResponseStatus.Pending,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var result = await _legalHoldPoliciesManager.DeleteLegalHoldPolicyAsync("166921");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(legalHoldsPoliciesUri + "166921", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task GetAssignment_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                          ""type"": ""legal_hold_policy_assignment"",
                                          ""id"": ""255473"",
                                          ""legal_hold_policy"": {
                                            ""type"": ""legal_hold_policy"",
                                            ""id"": ""166757"",
                                            ""policy_name"": ""Bug Bash 5-12 Policy 3 updated""
                                          },
                                          ""assigned_to"": {
                                            ""type"": ""user"",
                                            ""id"": ""2030388321""
                                          },
                                          ""assigned_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""2030388321"",
                                            ""name"": ""Steve Boxuser"",
                                            ""login"": ""sboxuser@box.com""
                                          },
                                          ""assigned_at"": ""2016-05-18T10:32:19-07:00"",
                                          ""deleted_at"": null
                                        }";
            IBoxRequest boxRequest = null;
            var legalHoldPolicyAssignmentUri = new Uri(Constants.LegalHoldPolicyAssignmentsEndpointString);
            Config.SetupGet(x => x.LegalHoldPolicyAssignmentsEndpointUri).Returns(legalHoldPolicyAssignmentUri);
            Handler.Setup(h => h.ExecuteAsync<BoxLegalHoldPolicyAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxLegalHoldPolicyAssignment>>(new BoxResponse<BoxLegalHoldPolicyAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxLegalHoldPolicyAssignment result = await _legalHoldPoliciesManager.GetAssignmentAsync("255473");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(legalHoldPolicyAssignmentUri + "255473", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual("legal_hold_policy_assignment", result.Type);
            Assert.AreEqual("255473", result.Id);
            Assert.AreEqual("legal_hold_policy", result.LegalHoldPolicy.Type);
            Assert.AreEqual("166757", result.LegalHoldPolicy.Id);
            Assert.AreEqual("Bug Bash 5-12 Policy 3 updated", result.LegalHoldPolicy.PolicyName);
            Assert.AreEqual("user", result.AssignedTo.Type);
            Assert.AreEqual("2030388321", result.AssignedTo.Id);
            Assert.AreEqual("user", result.AssignedBy.Type);
            Assert.AreEqual("2030388321", result.AssignedBy.Id);
            Assert.AreEqual("Steve Boxuser", result.AssignedBy.Name);
            Assert.AreEqual("sboxuser@box.com", result.AssignedBy.Login);
            Assert.IsNull(result.DeletedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-18T10:32:19-07:00"), result.AssignedAt);


        }

        [TestMethod]
        public async Task GetListAssignments_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                          ""entries"": [
                                            {
                                              ""type"": ""legal_hold_policy_assignment"",
                                              ""id"": ""255473""
                                            }
                                          ],
                                          ""limit"": 100,
                                          ""order"": [
                                            {
                                              ""by"": ""retention_policy_id, retention_policy_object_id"",
                                              ""direction"": ""ASC""
                                            }
                                          ]
                                        }";
            IBoxRequest boxRequest = null;
            var legalHoldsPoliciesUri = new Uri(Constants.LegalHoldPoliciesEndpointString);
            Config.SetupGet(x => x.LegalHoldPoliciesEndpointUri).Returns(legalHoldsPoliciesUri);
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxLegalHoldPolicyAssignment>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollectionMarkerBased<BoxLegalHoldPolicyAssignment>>>(new BoxResponse<BoxCollectionMarkerBased<BoxLegalHoldPolicyAssignment>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var result = await _legalHoldPoliciesManager.GetAssignmentsAsync("123456");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(legalHoldsPoliciesUri + "123456/assignments?limit=100", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(1, result.Entries.Count);
            Assert.AreEqual(100, result.Limit);

            //Not using for now until order is corrected to be two entries instead of a comma-separated list in one entry
            //Assert.AreEqual(BoxSortBy.retention_policy_object_id, result.Order[0].By);

            Assert.AreEqual(BoxSortDirection.ASC, result.Order[0].Direction);
            Assert.AreEqual("legal_hold_policy_assignment", result.Entries[0].Type);
            Assert.AreEqual("255473", result.Entries[0].Id);


        }

        [TestMethod]
        public async Task CreateNewAssignment_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                          ""type"": ""legal_hold_policy_assignment"",
                                          ""id"": ""255613"",
                                          ""legal_hold_policy"": {
                                            ""type"": ""legal_hold_policy"",
                                            ""id"": ""166757"",
                                            ""policy_name"": ""Bug Bash 5-12 Policy 3 updated""
                                          },
                                          ""assigned_to"": {
                                            ""type"": ""file"",
                                            ""id"": ""5025127885""
                                          },
                                          ""assigned_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""2030388321"",
                                            ""name"": ""Steve Boxuser"",
                                            ""login"": ""sboxuser@box.com""
                                          },
                                          ""assigned_at"": ""2016-05-18T17:38:03-07:00"",
                                          ""deleted_at"": null
                                        }";
            IBoxRequest boxRequest = null;
            var legalHoldPolicyAssignmentUri = new Uri(Constants.LegalHoldPolicyAssignmentsEndpointString);
            Config.SetupGet(x => x.LegalHoldPolicyAssignmentsEndpointUri).Returns(legalHoldPolicyAssignmentUri);
            Handler.Setup(h => h.ExecuteAsync<BoxLegalHoldPolicyAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxLegalHoldPolicyAssignment>>(new BoxResponse<BoxLegalHoldPolicyAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var createRequest = new BoxLegalHoldPolicyAssignmentRequest()
            {
                PolicyId = "166757",
                AssignTo = new BoxRequestEntity()
                {
                    Id = "5025127885",
                    Type = BoxType.file
                }
            };
            BoxLegalHoldPolicyAssignment result = await _legalHoldPoliciesManager.CreateAssignmentAsync(createRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(legalHoldPolicyAssignmentUri, boxRequest.AbsoluteUri.AbsoluteUri);
            BoxLegalHoldPolicyAssignmentRequest payLoad = JsonConvert.DeserializeObject<BoxLegalHoldPolicyAssignmentRequest>(boxRequest.Payload);
            Assert.AreEqual("166757", payLoad.PolicyId);
            Assert.AreEqual("5025127885", payLoad.AssignTo.Id);
            Assert.AreEqual(BoxType.file, payLoad.AssignTo.Type);

            //Response check
            Assert.AreEqual("legal_hold_policy_assignment", result.Type);
            Assert.AreEqual("255613", result.Id);
            Assert.AreEqual("legal_hold_policy", result.LegalHoldPolicy.Type);
            Assert.AreEqual("166757", result.LegalHoldPolicy.Id);
            Assert.AreEqual("Bug Bash 5-12 Policy 3 updated", result.LegalHoldPolicy.PolicyName);
            Assert.AreEqual("file", result.AssignedTo.Type);
            Assert.AreEqual("5025127885", result.AssignedTo.Id);
            Assert.AreEqual("user", result.AssignedBy.Type);
            Assert.AreEqual("2030388321", result.AssignedBy.Id);
            Assert.AreEqual("Steve Boxuser", result.AssignedBy.Name);
            Assert.AreEqual("sboxuser@box.com", result.AssignedBy.Login);
            Assert.IsNull(result.DeletedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2016-05-18T17:38:03-07:00"), result.AssignedAt);


        }
    }
}
