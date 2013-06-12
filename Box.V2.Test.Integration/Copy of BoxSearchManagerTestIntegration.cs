using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxSearchManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        private const string fileId = "7869094982";

        [TestMethod]
        public async Task CommentsWorkflow_LiveSession_ValidResponse()
        {

            // Test adding a new comment
            const string message = "this is a test";

            BoxCommentRequest addReq = new BoxCommentRequest()
            {
                Message = message,
                Item = new BoxRequestEntity()
                {
                    Id = fileId,
                    Type = BoxType.file
                }
            };
            
            BoxComment c = await _client.CommentsManager.AddCommentAsync(addReq);

            Assert.AreEqual(fileId, c.Item.Id);
            Assert.AreEqual(BoxType.file.ToString(), c.Item.Type);
            Assert.AreEqual(BoxType.comment.ToString(), c.Type);
            Assert.AreEqual(message, c.Message);


            // Test getting the comment information
            BoxComment cInfo = await _client.CommentsManager.GetInformationAsync(c.Id);

            Assert.AreEqual(c.Id, cInfo.Id);
            Assert.AreEqual(BoxType.comment.ToString(), cInfo.Type);


            // Test updating a message
            const string updateMessage = "this is an updated test comment";

            BoxCommentRequest updateReq = new BoxCommentRequest()
            {
                Message = updateMessage
            };

            BoxComment cUpdate = await _client.CommentsManager.UpdateAsync(c.Id, updateReq);

            Assert.AreEqual(c.Id, cUpdate.Id);
            Assert.AreEqual(BoxType.comment.ToString(), cUpdate.Type);
            Assert.AreEqual(updateMessage, cUpdate.Message);

            // Test deleting a comment
            bool success = await _client.CommentsManager.DeleteAsync(c.Id);

            Assert.AreEqual(true, success);
        }
    }
}
