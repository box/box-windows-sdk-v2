using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxCommentsManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task CommentsWorkflow_LiveSession_ValidResponse()
        {
            const string fileId = "16894947279";
            const string message = "this is a test Comment";

            // Create a comment with message
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

            Assert.AreEqual(fileId, c.Item.Id, "Comment was added to incorrect file");
            Assert.AreEqual(BoxType.file.ToString(), c.Item.Type, "Comment was not added to a file");
            Assert.AreEqual(BoxType.comment.ToString(), c.Type, "Returned object is not a comment");
            Assert.AreEqual(message, c.Message, "Wrong comment added to file");

            // Create a comment with tagged message
            var messageWithTag = "this is an tagged @[215917383:DisplayName] test comment";

            BoxCommentRequest addReqWithTag = new BoxCommentRequest()
            {
                Message = messageWithTag,
                Item = new BoxRequestEntity()
                {
                    Id = fileId,
                    Type = BoxType.file
                }
            };

            BoxComment cWithTag = await _client.CommentsManager.AddCommentAsync(addReqWithTag);

            Assert.AreEqual(fileId, cWithTag.Item.Id, "Comment was added to incorrect file");
            Assert.AreEqual(BoxType.file.ToString(), cWithTag.Item.Type, "Comment was not added to a file");
            Assert.AreEqual(BoxType.comment.ToString(), cWithTag.Type, "Returned object is not a comment");
            Assert.AreEqual(messageWithTag, cWithTag.Message, "Wrong comment added to file");

            // Get comment details
            BoxComment cInfo = await _client.CommentsManager.GetInformationAsync(c.Id);

            Assert.AreEqual(c.Id, cInfo.Id, "two comment objects have different ids");
            Assert.AreEqual(BoxType.comment.ToString(), cInfo.Type, "returned object is not a comment");
            
            // Update the comment
            const string updateMessage = "this is an updated test comment";

            BoxCommentRequest updateReq = new BoxCommentRequest()
            {
                Message = updateMessage
            };

            BoxComment cUpdate = await _client.CommentsManager.UpdateAsync(c.Id, updateReq);

            Assert.AreEqual(c.Id, cUpdate.Id, "Wrong comment was updated");
            Assert.AreEqual(BoxType.comment.ToString(), cUpdate.Type, "returned type of update is not a comment");
            Assert.AreEqual(updateMessage, cUpdate.Message, "Comment was not updated with correct string");

            // Deleting a comment
            bool success = await _client.CommentsManager.DeleteAsync(c.Id);

            Assert.AreEqual(true, success, "Unsuccessful comment delete");
        }
    }
}
