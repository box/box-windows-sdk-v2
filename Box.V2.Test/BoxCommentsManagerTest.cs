using System.Threading.Tasks;
using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxCommentsManagerTest : BoxResourceManagerTest
    {
        private readonly BoxCommentsManager _commentsManager;

        public BoxCommentsManagerTest()
        {
            _commentsManager = new BoxCommentsManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task AddComment_ValidResponse_ValidComment()
        {
            /*** Arrange ***/
            var responseString = "{\"type\":\"comment\",\"id\":\"191969\",\"is_reply_comment\":false,\"message\":\"These tigers are cool!\",\"created_by\":{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\"},\"created_at\":\"2012-12-12T11:25:01-08:00\",\"item\":{\"id\":\"5000948880\",\"type\":\"file\"},\"modified_at\":\"2012-12-12T11:25:01-08:00\"}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxComment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxComment>>(new BoxResponse<BoxComment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var request = new BoxCommentRequest()
            {
                Item = new BoxRequestEntity()
                {
                    Id = "fakeId",
                    Type = BoxType.file
                },
                Message = "fake message"
            };
            BoxComment c = await _commentsManager.AddCommentAsync(request);

            /*** Assert ***/
            /*** Request ***/
            BoxCommentRequest payLoad = JsonConvert.DeserializeObject<BoxCommentRequest>(boxRequest.Payload);
            Assert.AreEqual(request.Message, payLoad.Message);
            Assert.AreEqual(request.Item.Id, payLoad.Item.Id);
            Assert.AreEqual(request.Item.Type, payLoad.Item.Type);
            /*** Response***/
            Assert.AreEqual("comment", c.Type);
            Assert.AreEqual("191969", c.Id);
            Assert.AreEqual("These tigers are cool!", c.Message);
        }

        [TestMethod]
        public async Task GetCommentInformation_ValidResponse_ValidComment()
        {
            /*** Arrange ***/
            var responseString = "{\"type\":\"comment\",\"id\":\"191969\",\"is_reply_comment\":false,\"message\":\"These tigers are cool!\",\"created_by\":{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\"},\"created_at\":\"2012-12-12T11:25:01-08:00\",\"item\":{\"id\":\"5000948880\",\"type\":\"file\"},\"modified_at\":\"2012-12-12T11:25:01-08:00\"}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxComment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxComment>>(new BoxResponse<BoxComment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxComment c = await _commentsManager.GetInformationAsync("fakeId");

            /*** Assert ***/
            /*** Request ***/
            Assert.AreEqual("fakeId", boxRequest.Path);
            /*** Response ***/
            Assert.AreEqual("comment", c.Type);
            Assert.AreEqual("191969", c.Id);
            Assert.AreEqual("These tigers are cool!", c.Message);
        }

        [TestMethod]
        public async Task UpdateComment_ValidResponse_ValidComment()
        {
            /*** Arrange ***/
            var responseString = "{\"type\":\"comment\",\"id\":\"191969\",\"is_reply_comment\":false,\"message\":\"These tigers are cool!\",\"created_by\":{\"type\":\"user\",\"id\":\"17738362\",\"name\":\"sean rose\",\"login\":\"sean@box.com\"},\"created_at\":\"2012-12-12T11:25:01-08:00\",\"item\":{\"id\":\"5000948880\",\"type\":\"file\"},\"modified_at\":\"2012-12-12T11:25:01-08:00\"}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxComment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxComment>>(new BoxResponse<BoxComment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var request = new BoxCommentRequest()
            {
                Message = "fakeMessage"
            };

            BoxComment c = await _commentsManager.UpdateAsync("fakeId", request);

            /*** Assert ***/
            /*** Request ***/
            BoxCommentRequest payLoad = JsonConvert.DeserializeObject<BoxCommentRequest>(boxRequest.Payload);
            Assert.AreEqual(request.Message, payLoad.Message);
            /** Response ***/
            Assert.AreEqual("comment", c.Type);
            Assert.AreEqual("191969", c.Id);
            Assert.AreEqual("These tigers are cool!", c.Message);
        }

        [TestMethod]
        public async Task DeleteComment_ValidResponse_CommentDeleted()
        {
            /*** Arrange ***/
            var responseString = "";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxComment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxComment>>(new BoxResponse<BoxComment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var result = await _commentsManager.DeleteAsync("191969");

            /*** Assert ***/
            /*** Request ***/
            Assert.AreEqual("191969", boxRequest.Path);
            /*** Response ***/
            Assert.AreEqual(true, result);
        }
    }
}
