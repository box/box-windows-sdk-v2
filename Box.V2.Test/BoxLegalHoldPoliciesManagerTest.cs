using Box.V2.Config;
using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxLegalHoldPoliciesManagerTest : BoxResourceManagerTest
    {
        protected BoxLegalHoldPoliciesManager _legalHoldPoliciesManager;

        public BoxLegalHoldPoliciesManagerTest()
        {
            _legalHoldPoliciesManager = new BoxLegalHoldPoliciesManager(_config.Object, _service, _converter, _authRepository);
        }

        [TestMethod]
        public async Task GetLegalHoldPolicy_ValidResponse()
        {
            /*** Arrange ***/
            string responseString = @"{
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
            Uri legalHoldsPoliciesUri = new Uri(Constants.LegalHoldPoliciesEndpointString);
            _config.SetupGet(x => x.LegalHoldPoliciesEndpointUri).Returns(legalHoldsPoliciesUri);
            _handler.Setup(h => h.ExecuteAsync<BoxLegalHoldPolicy>(It.IsAny<IBoxRequest>()))
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
            Assert.AreEqual(DateTime.Parse("2016-05-18T10:28:45-07:00"), result.CreatedAt);
            Assert.AreEqual(DateTime.Parse("2016-05-18T11:25:59-07:00"), result.ModifiedAt);
            Assert.IsNull(result.DeletedAt);
            Assert.AreEqual(DateTime.Parse("2016-05-17T01:00:00-07:00"), result.FilterStartedAt);
            Assert.AreEqual(DateTime.Parse("2016-05-21T01:00:00-07:00"), result.FilterEndedAt);

        }

        [TestMethod]
        public async Task GetListLegalHoldPolicies_ValidResponse()
        {
            /*** Arrange ***/
            string responseString = @"{
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
            Uri legalHoldsPoliciesUri = new Uri(Constants.LegalHoldPoliciesEndpointString);
            _config.SetupGet(x => x.LegalHoldPoliciesEndpointUri).Returns(legalHoldsPoliciesUri);
            _handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxLegalHoldPolicy>>(It.IsAny<IBoxRequest>()))
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
            Assert.AreEqual(legalHoldsPoliciesUri + "?policy_name=pol", boxRequest.AbsoluteUri.AbsoluteUri);

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
            string responseString = @"{
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
            Uri legalHoldsPoliciesUri = new Uri(Constants.LegalHoldPoliciesEndpointString);
            _config.SetupGet(x => x.LegalHoldPoliciesEndpointUri).Returns(legalHoldsPoliciesUri);
            _handler.Setup(h => h.ExecuteAsync<BoxLegalHoldPolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxLegalHoldPolicy>>(new BoxResponse<BoxLegalHoldPolicy>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxLegalHoldPolicyRequest createRequest = new BoxLegalHoldPolicyRequest()
            {
                PolicyName = "Policy 3",
                Description = "postman created policy",
                FilterStartedAt = DateTime.Parse("2016-05-11T00:00:00-08:00"),
                FilterEndedAt = DateTime.Parse("2016-05-13T00:00:00-08:00")

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
            Assert.AreEqual(DateTime.Parse("2016-05-11T00:00:00-08:00"), payLoad.FilterStartedAt);
            Assert.AreEqual(DateTime.Parse("2016-05-13T00:00:00-08:00"), payLoad.FilterEndedAt);

            //Response check
            Assert.AreEqual("legal_hold_policy", result.Type);
            Assert.AreEqual("166921", result.Id);
            Assert.AreEqual("Policy 3", result.PolicyName);
            Assert.AreEqual("postman created policy", result.Description);
            Assert.IsNull(result.Status);
            Assert.AreEqual(DateTime.Parse("2016-05-18T16:18:49-07:00"), result.CreatedAt);
            Assert.AreEqual(DateTime.Parse("2016-05-18T16:18:49-07:00"), result.ModifiedAt);
            Assert.AreEqual(DateTime.Parse("2016-05-11T01:00:00-07:00"), result.FilterStartedAt);
            Assert.AreEqual(DateTime.Parse("2016-05-13T01:00:00-07:00"), result.FilterEndedAt);

        }

        [TestMethod]
        public async Task UpdateLegalHoldPolicy_ValidResponse()
        {
            /*** Arrange ***/
            string responseString = @"{
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
            Uri legalHoldsPoliciesUri = new Uri(Constants.LegalHoldPoliciesEndpointString);
            _config.SetupGet(x => x.LegalHoldPoliciesEndpointUri).Returns(legalHoldsPoliciesUri);
            _handler.Setup(h => h.ExecuteAsync<BoxLegalHoldPolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxLegalHoldPolicy>>(new BoxResponse<BoxLegalHoldPolicy>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxLegalHoldPolicyRequest updateRequest = new BoxLegalHoldPolicyRequest()
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
            Assert.AreEqual(DateTime.Parse("2016-05-18T16:18:49-07:00"), result.CreatedAt);
            Assert.AreEqual(DateTime.Parse("2016-05-18T16:20:47-07:00"), result.ModifiedAt);
            Assert.AreEqual(DateTime.Parse("2016-05-11T01:00:00-07:00"), result.FilterStartedAt);
            Assert.AreEqual(DateTime.Parse("2016-05-13T01:00:00-07:00"), result.FilterEndedAt);

        }

        [TestMethod]
        public async Task DeleteTask_TaskDeleted()
        {
            /*** Arrange ***/
            string responseString = "";
            IBoxRequest boxRequest = null;
            Uri legalHoldsPoliciesUri = new Uri(Constants.LegalHoldPoliciesEndpointString);
            _config.SetupGet(x => x.LegalHoldPoliciesEndpointUri).Returns(legalHoldsPoliciesUri);
            _handler.Setup(h => h.ExecuteAsync<BoxLegalHoldPolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxLegalHoldPolicy>>(new BoxResponse<BoxLegalHoldPolicy>()
                {
                    Status = ResponseStatus.Pending,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            bool result = await _legalHoldPoliciesManager.DeleteLegalHoldPolicyAsync("166921");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(legalHoldsPoliciesUri + "166921", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(true, result);
        }
    }
}
