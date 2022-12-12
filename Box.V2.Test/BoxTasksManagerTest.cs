using System;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.Managers;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxTasksManagerTest : BoxResourceManagerTest
    {
        private readonly BoxTasksManager _tasksManager;

        public BoxTasksManagerTest()
        {
            _tasksManager = new BoxTasksManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task CreateTaskAssignment_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""task_assignment"",
                                        ""id"": ""2698512"",
                                        ""item"": {
                                            ""type"": ""file"",
                                            ""id"": ""8018809384"",
                                            ""sequence_id"": ""0"",
                                            ""etag"": ""0"",
                                            ""sha1"": ""7840095ee096ee8297676a138d4e316eabb3ec96"",
                                            ""name"": ""scrumworksToTrello.js""
                                        },
                                        ""assigned_to"": {
                                            ""type"": ""user"",
                                            ""id"": ""1992432"",
                                            ""name"": ""rhaegar@box.com"",
                                            ""login"": ""rhaegar@box.com""
                                        },
                                        ""message"": null,
                                        ""completed_at"": null,
                                        ""assigned_at"": ""2013-05-10T11:43:41-07:00"",
                                        ""reminded_at"": null,
                                        ""status"": ""incomplete"",
                                        ""assigned_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""11993747"",
                                            ""name"": ""sean"",
                                            ""login"": ""sean@box.com""
                                        }
                                    }";
            IBoxRequest boxRequest = null;
            var taskAssignmentsUri = new Uri(Constants.TaskAssignmentsEndpointString);
            Config.SetupGet(x => x.TaskAssignmentsEndpointUri).Returns(taskAssignmentsUri);
            Handler.Setup(h => h.ExecuteAsync<BoxTaskAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxTaskAssignment>>(new BoxResponse<BoxTaskAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var taskAssignmentRequest = new BoxTaskAssignmentRequest()
            {
                Task = new BoxTaskRequest()
                {
                    Id = "1992432"
                },
                AssignTo = new BoxAssignmentRequest()
                {
                    Id = "1992432"
                }
            };
            BoxTaskAssignment result = await _tasksManager.CreateTaskAssignmentAsync(taskAssignmentRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(taskAssignmentsUri, boxRequest.AbsoluteUri.AbsoluteUri);
            BoxTaskAssignmentRequest payload = JsonConvert.DeserializeObject<BoxTaskAssignmentRequest>(boxRequest.Payload);
            Assert.AreEqual(taskAssignmentRequest.Task.Id, payload.Task.Id);
            Assert.AreEqual(taskAssignmentRequest.Task.Type, payload.Task.Type);
            Assert.AreEqual(taskAssignmentRequest.AssignTo.Id, payload.AssignTo.Id);
            Assert.AreEqual(taskAssignmentRequest.AssignTo.Login, payload.AssignTo.Login);

            //Response check
            Assert.AreEqual("2698512", result.Id);
            Assert.AreEqual("task_assignment", result.Type);
            Assert.AreEqual("8018809384", result.Item.Id);
            Assert.AreEqual("file", result.Item.Type);
            Assert.AreEqual("0", result.Item.ETag);
            Assert.AreEqual("incomplete", result.Status);
            Assert.AreEqual("sean@box.com", result.AssignedBy.Login);
            Assert.AreEqual("11993747", result.AssignedBy.Id);
            Assert.AreEqual("rhaegar@box.com", result.AssignedTo.Login);
            Assert.AreEqual("1992432", result.AssignedTo.Id);
        }

        [TestMethod]
        public async Task UpdateTaskAssignment_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                            ""type"": ""task_assignment"",
                                            ""id"": ""2698512"",
                                            ""item"": {
                                                ""type"": ""file"",
                                                ""id"": ""8018809384"",
                                                ""sequence_id"": ""0"",
                                                ""etag"": ""0"",
                                                ""sha1"": ""7840095ee096ee8297676a138d4e316eabb3ec96"",
                                                ""name"": ""scrumworksToTrello.js""
                                            },
                                            ""assigned_to"": {
                                                ""type"": ""user"",
                                                ""id"": ""1992432"",
                                                ""name"": ""rhaegar@box.com"",
                                                ""login"": ""rhaegar@box.com""
                                            },
                                            ""message"": ""hello!!!"",
                                            ""completed_at"": null,
                                            ""assigned_at"": ""2013-05-10T11:43:41-07:00"",
                                            ""reminded_at"": null,
                                            ""status"": ""incomplete"",
                                            ""assigned_by"": {
                                                ""type"": ""user"",
                                                ""id"": ""11993747"",
                                                ""name"": ""sean"",
                                                ""login"": ""sean@box.com""
                                            }
                                        }";
            IBoxRequest boxRequest = null;
            var taskAssignmentsUri = new Uri(Constants.TaskAssignmentsEndpointString);
            Config.SetupGet(x => x.TaskAssignmentsEndpointUri).Returns(taskAssignmentsUri);
            Handler.Setup(h => h.ExecuteAsync<BoxTaskAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxTaskAssignment>>(new BoxResponse<BoxTaskAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var taskAssignmentUpdateRequest = new BoxTaskAssignmentUpdateRequest()
            {
                Id = "2698512",
                Message = "hello!!!"
            };
            BoxTaskAssignment result = await _tasksManager.UpdateTaskAssignmentAsync(taskAssignmentUpdateRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(taskAssignmentsUri + taskAssignmentUpdateRequest.Id, boxRequest.AbsoluteUri.AbsoluteUri);
            BoxTaskAssignmentUpdateRequest payload = JsonConvert.DeserializeObject<BoxTaskAssignmentUpdateRequest>(boxRequest.Payload);
            Assert.AreEqual(taskAssignmentUpdateRequest.Id, payload.Id);
            Assert.AreEqual(taskAssignmentUpdateRequest.Message, payload.Message);

            //Response check
            Assert.AreEqual("2698512", result.Id);
            Assert.AreEqual("task_assignment", result.Type);
            Assert.AreEqual("hello!!!", result.Message);
            Assert.AreEqual("8018809384", result.Item.Id);
            Assert.AreEqual("file", result.Item.Type);
            Assert.AreEqual("0", result.Item.ETag);
            Assert.AreEqual("incomplete", result.Status);
            Assert.AreEqual("sean@box.com", result.AssignedBy.Login);
            Assert.AreEqual("11993747", result.AssignedBy.Id);
            Assert.AreEqual("rhaegar@box.com", result.AssignedTo.Login);
            Assert.AreEqual("1992432", result.AssignedTo.Id);
        }

        [TestMethod]
        public async Task GetTaskAssignment_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""task_assignment"",
                                        ""id"": ""2698512"",
                                        ""item"": {
                                            ""type"": ""file"",
                                            ""id"": ""8018809384"",
                                            ""sequence_id"": ""0"",
                                            ""etag"": ""0"",
                                            ""sha1"": ""7840095ee096ee8297676a138d4e316eabb3ec96"",
                                            ""name"": ""scrumworksToTrello.js""
                                        },
                                        ""assigned_to"": {
                                            ""type"": ""user"",
                                            ""id"": ""1992432"",
                                            ""name"": ""rhaegar@box.com"",
                                            ""login"": ""rhaegar@box.com""
                                        },
                                        ""message"": null,
                                        ""completed_at"": null,
                                        ""assigned_at"": ""2013-05-10T11:43:41-07:00"",
                                        ""reminded_at"": null,
                                        ""status"": ""incomplete"",
                                        ""assigned_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""11993747"",
                                            ""name"": ""sean"",
                                            ""login"": ""sean@box.com""
                                        }
                                    }";
            IBoxRequest boxRequest = null;
            var taskAssignmentsUri = new Uri(Constants.TaskAssignmentsEndpointString);
            Config.SetupGet(x => x.TaskAssignmentsEndpointUri).Returns(taskAssignmentsUri);
            Handler.Setup(h => h.ExecuteAsync<BoxTaskAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxTaskAssignment>>(new BoxResponse<BoxTaskAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/

            BoxTaskAssignment result = await _tasksManager.GetTaskAssignmentAsync("2698512");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(taskAssignmentsUri + "2698512", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual("2698512", result.Id);
            Assert.AreEqual("task_assignment", result.Type);
            Assert.AreEqual("8018809384", result.Item.Id);
            Assert.AreEqual("file", result.Item.Type);
            Assert.AreEqual("0", result.Item.ETag);
            Assert.AreEqual("incomplete", result.Status);
            Assert.AreEqual("sean@box.com", result.AssignedBy.Login);
            Assert.AreEqual("11993747", result.AssignedBy.Id);
            Assert.AreEqual("rhaegar@box.com", result.AssignedTo.Login);
            Assert.AreEqual("1992432", result.AssignedTo.Id);
        }

        [TestMethod]
        public async Task GetTaskAssignment_TranslatedStatus()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""task_assignment"",
                                        ""id"": ""12345"",
                                        ""item"": {
                                            ""type"": ""file"",
                                            ""id"": ""11111"",
                                            ""sequence_id"": ""0"",
                                            ""etag"": ""0"",
                                            ""sha1"": ""7840095ee096ee8297676a138d4e316eabb3ec96"",
                                            ""name"": ""script.js""
                                        },
                                        ""assigned_to"": {
                                            ""type"": ""user"",
                                            ""id"": ""22222"",
                                            ""name"": ""rhaegar@example.com"",
                                            ""login"": ""rhaegar@example.com""
                                        },
                                        ""message"": null,
                                        ""completed_at"": null,
                                        ""assigned_at"": ""2013-05-10T11:43:41-07:00"",
                                        ""reminded_at"": null,
                                        ""resolution_state"": ""incomplete"",
                                        ""status"": ""未完了"",
                                        ""assigned_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""33333"",
                                            ""name"": ""sean"",
                                            ""login"": ""sean@example.com""
                                        }
                                    }";

            var taskAssignmentsUri = new Uri(Constants.TaskAssignmentsEndpointString);
            Config.SetupGet(x => x.TaskAssignmentsEndpointUri).Returns(taskAssignmentsUri);
            Handler.Setup(h => h.ExecuteAsync<BoxTaskAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxTaskAssignment>>(new BoxResponse<BoxTaskAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/

            BoxTaskAssignment result = await _tasksManager.GetTaskAssignmentAsync("12345");

            /*** Assert ***/

            Assert.AreEqual("incomplete", result.LocalizedStatus);
            Assert.AreEqual("未完了", result.Status);
            Assert.AreEqual(ResolutionStateType.incomplete, result.ResolutionState);
        }

        [TestMethod]
        public async Task DeleteTaskAssignment_TaskAssignmentDeleted()
        {
            /*** Arrange ***/
            var responseString = "";
            IBoxRequest boxRequest = null;
            var taskAssignmentsUri = new Uri(Constants.TaskAssignmentsEndpointString);
            Config.SetupGet(x => x.TaskAssignmentsEndpointUri).Returns(taskAssignmentsUri);
            Handler.Setup(h => h.ExecuteAsync<BoxTaskAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxTaskAssignment>>(new BoxResponse<BoxTaskAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var result = await _tasksManager.DeleteTaskAssignmentAsync("2698512");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(taskAssignmentsUri + "2698512", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(true, result);


        }

        [TestMethod]
        public async Task CreateTask_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""task"",
                                        ""id"": ""1839355"",
                                        ""item"": {
                                            ""type"": ""file"",
                                            ""id"": ""7287087200"",
                                            ""sequence_id"": ""0"",
                                            ""etag"": ""0"",
                                            ""sha1"": ""0bbd79a105c504f99573e3799756debba4c760cd"",
                                            ""name"": ""box-logo.png""
                                        },
                                        ""due_at"": ""2014-04-03T11:09:43-07:00"",
                                        ""action"": ""review"",
                                        ""message"": ""REVIEW PLZ K THX"",
                                        ""task_assignment_collection"": {
                                            ""total_count"": 0,
                                            ""entries"": []
                                        },
                                        ""is_completed"": false,
                                        ""created_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""11993747"",
                                            ""name"": ""sean"",
                                            ""login"": ""sean@box.com""
                                        },
                                        ""created_at"": ""2013-04-03T11:12:54-07:00"",
                                        ""completion_rule"": ""all_assignees""
                                    }";
            IBoxRequest boxRequest = null;
            var tasksUri = new Uri(Constants.TasksEndpointString);
            Config.SetupGet(x => x.TasksEndpointUri).Returns(tasksUri);
            Handler.Setup(h => h.ExecuteAsync<BoxTask>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxTask>>(new BoxResponse<BoxTask>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var taskCreateRequest = new BoxTaskCreateRequest()
            {
                Item = new BoxRequestEntity()
                {
                    Id = "7287087200",
                    Type = BoxType.file
                },
                Message = "REVIEW PLZ K THX"
            };
            BoxTask result = await _tasksManager.CreateTaskAsync(taskCreateRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(tasksUri, boxRequest.AbsoluteUri.AbsoluteUri);
            BoxTaskCreateRequest payload = JsonConvert.DeserializeObject<BoxTaskCreateRequest>(boxRequest.Payload);
            Assert.AreEqual(taskCreateRequest.Item.Id, payload.Item.Id);
            Assert.AreEqual(taskCreateRequest.Item.Type, payload.Item.Type);
            Assert.AreEqual(taskCreateRequest.Message, payload.Message);
            Assert.IsNull(payload.CompletionRule);

            //Response check
            Assert.AreEqual("1839355", result.Id);
            Assert.AreEqual("task", result.Type);
            Assert.AreEqual("7287087200", result.Item.Id);
            Assert.AreEqual("file", result.Item.Type);
            Assert.AreEqual("0", result.Item.ETag);
            Assert.AreEqual("REVIEW PLZ K THX", result.Message);
            Assert.AreEqual(false, result.IsCompleted);
            Assert.AreEqual("11993747", result.CreatedBy.Id);
            Assert.AreEqual("sean@box.com", result.CreatedBy.Login);
            Assert.AreEqual(0, result.TaskAssignments.TotalCount);
            Assert.AreEqual(BoxCompletionRule.all_assignees, result.CompletionRule);
        }

        [TestMethod]
        public async Task CreateTask_WithCompletionRule()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""task"",
                                        ""id"": ""1839355"",
                                        ""item"": {
                                            ""type"": ""file"",
                                            ""id"": ""7287087200"",
                                            ""sequence_id"": ""0"",
                                            ""etag"": ""0"",
                                            ""sha1"": ""0bbd79a105c504f99573e3799756debba4c760cd"",
                                            ""name"": ""box-logo.png""
                                        },
                                        ""due_at"": ""2014-04-03T11:09:43-07:00"",
                                        ""action"": ""review"",
                                        ""message"": ""REVIEW PLZ K THX"",
                                        ""task_assignment_collection"": {
                                            ""total_count"": 0,
                                            ""entries"": []
                                        },
                                        ""is_completed"": false,
                                        ""created_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""11993747"",
                                            ""name"": ""sean"",
                                            ""login"": ""sean@box.com""
                                        },
                                        ""created_at"": ""2013-04-03T11:12:54-07:00"",
                                        ""completion_rule"": ""any_assignee""
                                    }";
            IBoxRequest boxRequest = null;
            var tasksUri = new Uri(Constants.TasksEndpointString);
            Config.SetupGet(x => x.TasksEndpointUri).Returns(tasksUri);
            Handler.Setup(h => h.ExecuteAsync<BoxTask>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxTask>>(new BoxResponse<BoxTask>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var taskCreateRequest = new BoxTaskCreateRequest()
            {
                Item = new BoxRequestEntity()
                {
                    Id = "7287087200",
                    Type = BoxType.file
                },
                Message = "REVIEW PLZ K THX",
                CompletionRule = BoxCompletionRule.any_assignee
            };
            BoxTask result = await _tasksManager.CreateTaskAsync(taskCreateRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(tasksUri, boxRequest.AbsoluteUri.AbsoluteUri);
            BoxTaskCreateRequest payload = JsonConvert.DeserializeObject<BoxTaskCreateRequest>(boxRequest.Payload);
            Assert.AreEqual(taskCreateRequest.Item.Id, payload.Item.Id);
            Assert.AreEqual(taskCreateRequest.Item.Type, payload.Item.Type);
            Assert.AreEqual(taskCreateRequest.Message, payload.Message);
            Assert.AreEqual(taskCreateRequest.CompletionRule, payload.CompletionRule);
            Assert.IsTrue(boxRequest.Payload.ContainsKeyValue("completion_rule", "any_assignee"));

            //Response check
            Assert.AreEqual(BoxCompletionRule.any_assignee, result.CompletionRule);
        }

        [TestMethod]
        public async Task UpdateTask_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""task"",
                                        ""id"": ""1839355"",
                                        ""item"": {
                                            ""type"": ""file"",
                                            ""id"": ""7287087200"",
                                            ""sequence_id"": ""0"",
                                            ""etag"": ""0"",
                                            ""sha1"": ""0bbd79a105c504f99573e3799756debba4c760cd"",
                                            ""name"": ""box-logo.png""
                                        },
                                        ""due_at"": ""2014-04-03T11:09:43-07:00"",
                                        ""action"": ""review"",
                                        ""message"": ""REVIEW PLZ K THX"",
                                        ""task_assignment_collection"": {
                                            ""total_count"": 0,
                                            ""entries"": []
                                        },
                                        ""is_completed"": false,
                                        ""created_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""11993747"",
                                            ""name"": ""sean"",
                                            ""login"": ""sean@box.com""
                                        },
                                        ""created_at"": ""2013-04-03T11:12:54-07:00""
                                    }";
            IBoxRequest boxRequest = null;
            var tasksUri = new Uri(Constants.TasksEndpointString);
            Config.SetupGet(x => x.TasksEndpointUri).Returns(tasksUri);
            Handler.Setup(h => h.ExecuteAsync<BoxTask>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxTask>>(new BoxResponse<BoxTask>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var taskUpdateRequest = new BoxTaskUpdateRequest()
            {
                Id = "1839355",
                Message = "REVIEW PLZ K THX"
            };
            BoxTask result = await _tasksManager.UpdateTaskAsync(taskUpdateRequest);

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(tasksUri + "1839355", boxRequest.AbsoluteUri.AbsoluteUri);
            BoxTaskUpdateRequest payload = JsonConvert.DeserializeObject<BoxTaskUpdateRequest>(boxRequest.Payload);
            Assert.AreEqual(taskUpdateRequest.Message, payload.Message);

            //Response check
            Assert.AreEqual("1839355", result.Id);
            Assert.AreEqual("task", result.Type);
            Assert.AreEqual("7287087200", result.Item.Id);
            Assert.AreEqual("file", result.Item.Type);
            Assert.AreEqual("0", result.Item.ETag);
            Assert.AreEqual("REVIEW PLZ K THX", result.Message);
            Assert.AreEqual(false, result.IsCompleted);
            Assert.AreEqual("11993747", result.CreatedBy.Id);
            Assert.AreEqual("sean@box.com", result.CreatedBy.Login);
            Assert.AreEqual(0, result.TaskAssignments.TotalCount);

        }

        [TestMethod]
        public async Task DeleteTask_TaskDeleted()
        {
            /*** Arrange ***/
            var responseString = "";
            IBoxRequest boxRequest = null;
            var tasksUri = new Uri(Constants.TasksEndpointString);
            Config.SetupGet(x => x.TasksEndpointUri).Returns(tasksUri);
            Handler.Setup(h => h.ExecuteAsync<BoxTaskAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxTaskAssignment>>(new BoxResponse<BoxTaskAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var result = await _tasksManager.DeleteTaskAsync("1839355");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(tasksUri + "1839355", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(true, result);


        }

        [TestMethod]

        public async Task GetTask_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""type"": ""task"",
                                        ""id"": ""1839355"",
                                        ""item"": {
                                            ""type"": ""file"",
                                            ""id"": ""7287087200"",
                                            ""sequence_id"": ""0"",
                                            ""etag"": ""0"",
                                            ""sha1"": ""0bbd79a105c504f99573e3799756debba4c760cd"",
                                            ""name"": ""box-logo.png""
                                        },
                                        ""due_at"": ""2014-04-03T11:09:43-07:00"",
                                        ""action"": ""review"",
                                        ""message"": ""REVIEW PLZ K THX"",
                                        ""task_assignment_collection"": {
                                            ""total_count"": 0,
                                            ""entries"": []
                                        },
                                        ""is_completed"": false,
                                        ""created_by"": {
                                            ""type"": ""user"",
                                            ""id"": ""11993747"",
                                            ""name"": ""sean"",
                                            ""login"": ""sean@box.com""
                                        },
                                        ""created_at"": ""2013-04-03T11:12:54-07:00""
                                    }";
            IBoxRequest boxRequest = null;
            var tasksUri = new Uri(Constants.TasksEndpointString);
            Config.SetupGet(x => x.TasksEndpointUri).Returns(tasksUri);
            Handler.Setup(h => h.ExecuteAsync<BoxTask>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxTask>>(new BoxResponse<BoxTask>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            BoxTask result = await _tasksManager.GetTaskAsync("1839355");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(tasksUri + "1839355", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual("1839355", result.Id);
            Assert.AreEqual("task", result.Type);
            Assert.AreEqual("7287087200", result.Item.Id);
            Assert.AreEqual("file", result.Item.Type);
            Assert.AreEqual("0", result.Item.ETag);
            Assert.AreEqual("REVIEW PLZ K THX", result.Message);
            Assert.AreEqual(false, result.IsCompleted);
            Assert.AreEqual("11993747", result.CreatedBy.Id);
            Assert.AreEqual("sean@box.com", result.CreatedBy.Login);
            Assert.AreEqual(0, result.TaskAssignments.TotalCount);

        }

        [TestMethod]
        public async Task GetAssignments_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = @"{
                                        ""total_count"": 1,
                                        ""entries"": [
                                            {
                                                ""type"": ""task_assignment"",
                                                ""id"": ""2485961"",
                                                ""item"": {
                                                    ""type"": ""file"",
                                                    ""id"": ""7287087200"",
                                                    ""sequence_id"": ""0"",
                                                    ""etag"": ""0"",
                                                    ""sha1"": ""0bbd79a105c504f99573e3799756debba4c760cd"",
                                                    ""name"": ""box-logo.png""
                                                },
                                                ""assigned_to"": {
                                                    ""type"": ""user"",
                                                    ""id"": ""193425559"",
                                                    ""name"": ""Rhaegar Targaryen"",
                                                    ""login"": ""rhaegar@box.com""
                                                }
                                            }
                                        ]
                                    }";
            IBoxRequest boxRequest = null;
            var tasksUri = new Uri(Constants.TasksEndpointString);
            Config.SetupGet(x => x.TasksEndpointUri).Returns(tasksUri);
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxTaskAssignment>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxTaskAssignment>>>(new BoxResponse<BoxCollection<BoxTaskAssignment>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            BoxCollection<BoxTaskAssignment> result = await _tasksManager.GetAssignmentsAsync("1839355");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(tasksUri + string.Format(Constants.TaskAssignmentsPathString, "1839355"), boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(1, result.TotalCount);
            Assert.AreEqual("2485961", result.Entries[0].Id);
            Assert.AreEqual("193425559", result.Entries[0].AssignedTo.Id);
            Assert.AreEqual("rhaegar@box.com", result.Entries[0].AssignedTo.Login);
            Assert.AreEqual("task_assignment", result.Entries[0].Type);
            Assert.AreEqual("file", result.Entries[0].Item.Type);
            Assert.AreEqual("7287087200", result.Entries[0].Item.Id);
            Assert.AreEqual("box-logo.png", result.Entries[0].Item.Name);
        }

        [TestMethod]
        public async Task ThrowIfNull_WorksWithNullable_NoValue()
        {
            var responseString = "{\"type\":\"task\",\"id\":\"1874102965\"}";
            Handler.Setup(h => h.ExecuteAsync<BoxTask>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxTask>>(new BoxResponse<BoxTask>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            var taskRequest = new BoxTaskCreateRequest
            {
                Item = new BoxRequestEntity
                {
                    Id = "1234"
                }
            };
            try
            {
                BoxTask task = await _tasksManager.CreateTaskAsync(taskRequest);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("taskCreateRequest.Item.Type", ex.ParamName);
                return;
            }

            Assert.Fail("Exception should have been thrown");
        }
    }
}
