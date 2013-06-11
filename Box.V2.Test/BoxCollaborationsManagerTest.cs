using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxCollaborationsManagerTest : BoxResourceManagerTest
    {
        protected BoxCollaborationsManager _collaborationsManager;

        public BoxCollaborationsManagerTest()
        {
            _collaborationsManager = new BoxCollaborationsManager(_config.Object, _service, _converter, _authRepository);
        }

        [TestMethod]
        public async Task AddCollaboration_ValidResponse_ValidCollaboration()
        {
            /*** Arrange ***/
            string responseString = "{\"type\":\"collaboration\",\"id\":\"791293\",\"created_by\":{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\"},\"created_at\":\"2012-12-12T10:54:37-08:00\",\"modified_at\":\"2012-12-12T11:30:43-08:00\",\"expires_at\":null,\"status\":\"accepted\",\"accessible_by\":{\"type\":\"user\",\"id\":\"18203124\",\"name\":\"sean\",\"login\":\"sean+test@box.com\"},\"role\":\"editor\",\"acknowledged_at\":\"2012-12-12T11:30:43-08:00\",\"item\":{\"type\":\"folder\",\"id\":\"11446500\",\"sequence_id\":\"0\",\"etag\":\"0\",\"name\":\"Shared Pictures\"}}";
            _handler.Setup(h => h.ExecuteAsync<BoxCollaboration>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollaboration>>(new BoxResponse<BoxCollaboration>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxCollaborationRequest request = new BoxCollaborationRequest()
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
            string responseString = "{\"type\":\"collaboration\",\"id\":\"791293\",\"created_by\":{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\"},\"created_at\":\"2012-12-12T10:54:37-08:00\",\"modified_at\":\"2012-12-12T11:30:43-08:00\",\"expires_at\":null,\"status\":\"accepted\",\"accessible_by\":{\"type\":\"user\",\"id\":\"18203124\",\"name\":\"sean\",\"login\":\"sean+test@box.com\"},\"role\":\"viewer\",\"acknowledged_at\":\"2012-12-12T11:30:43-08:00\",\"item\":{\"type\":\"folder\",\"id\":\"11446500\",\"sequence_id\":\"0\",\"etag\":\"0\",\"name\":\"Shared Pictures\"}}";
            _handler.Setup(h => h.ExecuteAsync<BoxCollaboration>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollaboration>>(new BoxResponse<BoxCollaboration>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxCollaborationRequest request = new BoxCollaborationRequest()
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
            string responseString = "{\"type\":\"collaboration\",\"id\":\"791293\",\"created_by\":{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\"},\"created_at\":\"2012-12-12T10:54:37-08:00\",\"modified_at\":\"2012-12-12T11:30:43-08:00\",\"expires_at\":null,\"status\":\"accepted\",\"accessible_by\":{\"type\":\"user\",\"id\":\"18203124\",\"name\":\"sean\",\"login\":\"sean+test@box.com\"},\"role\":\"editor\",\"acknowledged_at\":\"2012-12-12T11:30:43-08:00\",\"item\":{\"type\":\"folder\",\"id\":\"11446500\",\"sequence_id\":\"0\",\"etag\":\"0\",\"name\":\"Shared Pictures\"}}";
            _handler.Setup(h => h.ExecuteAsync<BoxCollaboration>(It.IsAny<IBoxRequest>()))
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



    }
}