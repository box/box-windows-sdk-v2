using System.Threading.Tasks;
using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxCollaborationsManagerTest : BoxResourceManagerTest
    {
        private readonly BoxCollaborationsManager _collaborationsManager;

        public BoxCollaborationsManagerTest()
        {
            _collaborationsManager = new BoxCollaborationsManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task AddCollaboration_ValidResponse_ValidCollaboration()
        {
            /*** Arrange ***/
            var responseString = "{\"type\":\"collaboration\",\"id\":\"791293\",\"created_by\":{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\"},\"created_at\":\"2012-12-12T10:54:37-08:00\",\"modified_at\":\"2012-12-12T11:30:43-08:00\",\"expires_at\":null,\"status\":\"accepted\",\"accessible_by\":{\"type\":\"user\",\"id\":\"18203124\",\"name\":\"sean\",\"login\":\"sean+test@box.com\"},\"role\":\"editor\",\"acknowledged_at\":\"2012-12-12T11:30:43-08:00\",\"item\":{\"type\":\"folder\",\"id\":\"11446500\",\"sequence_id\":\"0\",\"etag\":\"0\",\"name\":\"Shared Pictures\"}}";
            Handler.Setup(h => h.ExecuteAsync<BoxCollaboration>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollaboration>>(new BoxResponse<BoxCollaboration>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            var request = new BoxCollaborationRequest()
            {
                Item = new BoxRequestEntity()
                {
                    Id = "fakeId",
                    Type = BoxType.file
                },
                AccessibleBy = new BoxCollaborationUserRequest()
                {
                    Id = "fakeId",
                    Login = "fakeLogin"
                },
                Role = "fakeRole"
            };

            BoxCollaboration collab = await _collaborationsManager.AddCollaborationAsync(request);

            /*** Assert ***/
            Assert.AreEqual("791293", collab.Id);
            Assert.AreEqual("collaboration", collab.Type);
            Assert.AreEqual("user", collab.CreatedBy.Type);
            Assert.AreEqual("17738362", collab.CreatedBy.Id);
            Assert.AreEqual("sean@box.com", collab.CreatedBy.Login);
        }

        [TestMethod]
        public async Task EditCollaboration_ValidResponse_ValidCollaboration()
        {
            /*** Arrange ***/
            var responseString = "{\"type\":\"collaboration\",\"id\":\"791293\",\"created_by\":{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\"},\"created_at\":\"2012-12-12T10:54:37-08:00\",\"modified_at\":\"2012-12-12T11:30:43-08:00\",\"expires_at\":null,\"status\":\"accepted\",\"accessible_by\":{\"type\":\"user\",\"id\":\"18203124\",\"name\":\"sean\",\"login\":\"sean+test@box.com\"},\"role\":\"viewer\",\"acknowledged_at\":\"2012-12-12T11:30:43-08:00\",\"item\":{\"type\":\"folder\",\"id\":\"11446500\",\"sequence_id\":\"0\",\"etag\":\"0\",\"name\":\"Shared Pictures\"}}";
            Handler.Setup(h => h.ExecuteAsync<BoxCollaboration>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollaboration>>(new BoxResponse<BoxCollaboration>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            var request = new BoxCollaborationRequest()
            {
                Id = "fakeId"
            };

            BoxCollaboration collab = await _collaborationsManager.EditCollaborationAsync(request);

            /*** Assert ***/
            Assert.AreEqual("791293", collab.Id);
            Assert.AreEqual("collaboration", collab.Type);
            Assert.AreEqual("user", collab.CreatedBy.Type);
            Assert.AreEqual("17738362", collab.CreatedBy.Id);
            Assert.AreEqual("sean@box.com", collab.CreatedBy.Login);
        }

        [TestMethod]
        public async Task GetCollaboration_ValidResponse_ValidCollaboration()
        {
            /*** Arrange ***/
            var responseString = "{\"type\":\"collaboration\",\"id\":\"791293\",\"created_by\":{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\"},\"created_at\":\"2012-12-12T10:54:37-08:00\",\"modified_at\":\"2012-12-12T11:30:43-08:00\",\"expires_at\":null,\"status\":\"accepted\",\"accessible_by\":{\"type\":\"user\",\"id\":\"18203124\",\"name\":\"sean\",\"login\":\"sean+test@box.com\"},\"role\":\"editor\",\"acknowledged_at\":\"2012-12-12T11:30:43-08:00\",\"item\":{\"type\":\"folder\",\"id\":\"11446500\",\"sequence_id\":\"0\",\"etag\":\"0\",\"name\":\"Shared Pictures\"}}";
            Handler.Setup(h => h.ExecuteAsync<BoxCollaboration>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollaboration>>(new BoxResponse<BoxCollaboration>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxCollaboration collab = await _collaborationsManager.GetCollaborationAsync("fakeId");

            /*** Assert ***/
            Assert.AreEqual("791293", collab.Id);
            Assert.AreEqual("collaboration", collab.Type);
            Assert.AreEqual("user", collab.CreatedBy.Type);
            Assert.AreEqual("17738362", collab.CreatedBy.Id);
            Assert.AreEqual("sean@box.com", collab.CreatedBy.Login);
        }

        [TestMethod]
        public async Task GetPendingCollaboration_ValidResponse_ValidEntries()
        {
            /*** Arrange ***/
            var responseString = @"{
                    ""total_count"": 1,
                    ""entries"": [
                        {
                            ""type"": ""collaboration"",
                            ""id"": ""27513888"",
                            ""created_by"": {
                                ""type"": ""user"",
                                ""id"": ""11993747"",
                                ""name"": ""sean"",
                                ""login"": ""sean@box.com""
                            },
                            ""created_at"": ""2012-10-17T23:14:42-07:00"",
                            ""modified_at"": ""2012-10-17T23:14:42-07:00"",
                            ""expires_at"": null,
                            ""status"": ""Pending"",
                            ""accessible_by"": {
                                ""type"": ""user"",
                                ""id"": ""181216415"",
                                ""name"": ""sean rose"",
                                ""login"": ""sean+awesome@box.com""
                            },
                            ""role"": ""Editor"",
                            ""acknowledged_at"": null,
                            ""invite_email"": ""collab@example.com"",
                            ""item"": null
                        }
                    ]
                }";

            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxCollaboration>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxCollaboration>>>(new BoxResponse<BoxCollection<BoxCollaboration>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);



            /*** Act ***/
            BoxCollection<BoxCollaboration> collaborations = await _collaborationsManager.GetPendingCollaborationAsync();

            /*** Assert ***/
            Assert.AreEqual(1, collaborations.TotalCount);
            Assert.AreEqual("27513888", collaborations.Entries[0].Id);
            Assert.AreEqual("collaboration", collaborations.Entries[0].Type);
            Assert.AreEqual("user", collaborations.Entries[0].AccessibleBy.Type);
            Assert.AreEqual("181216415", collaborations.Entries[0].AccessibleBy.Id);
            Assert.AreEqual("collab@example.com", collaborations.Entries[0].InviteEmail);
        }

        [TestMethod]
        public async Task DeleteCollaboration_ValidResponse_CollaborationDeleted()
        {
            /*** Arrange ***/
            var responseString = "";
            Handler.Setup(h => h.ExecuteAsync<BoxCollaboration>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollaboration>>(new BoxResponse<BoxCollaboration>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            var result = await _collaborationsManager.RemoveCollaborationAsync("34122832467");

            /*** Assert ***/

            Assert.AreEqual(true, result);
        }

    }

}
