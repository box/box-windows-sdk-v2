using System.Threading.Tasks;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxCommentsManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task CommentsWorkflow_LiveSession_ValidResponse()
        {
            const string FileId = "16894947279";
            const string Message = "this is a test Comment";

            // Create a comment with message
            var addReq = new BoxCommentRequest()
            {
                Message = Message,
                Item = new BoxRequestEntity()
                {
                    Id = FileId,
                    Type = BoxType.file
                }
            };

            BoxComment c = await Client.CommentsManager.AddCommentAsync(addReq);

            Assert.AreEqual(FileId, c.Item.Id, "Comment was added to incorrect file");
            Assert.AreEqual(BoxType.file.ToString(), c.Item.Type, "Comment was not added to a file");
            Assert.AreEqual(BoxType.comment.ToString(), c.Type, "Returned object is not a comment");
            Assert.AreEqual(Message, c.Message, "Wrong comment added to file");

            // Create a comment with tagged message
            var messageWithTag = "this is an tagged @[215917383:DisplayName] test comment";

            var addReqWithTag = new BoxCommentRequest()
            {
                Message = messageWithTag,
                Item = new BoxRequestEntity()
                {
                    Id = FileId,
                    Type = BoxType.file
                }
            };

            BoxComment cWithTag = await Client.CommentsManager.AddCommentAsync(addReqWithTag);

            Assert.AreEqual(FileId, cWithTag.Item.Id, "Comment was added to incorrect file");
            Assert.AreEqual(BoxType.file.ToString(), cWithTag.Item.Type, "Comment was not added to a file");
            Assert.AreEqual(BoxType.comment.ToString(), cWithTag.Type, "Returned object is not a comment");
            Assert.AreEqual(messageWithTag, cWithTag.Message, "Wrong comment added to file");

            // Get comment details
            BoxComment cInfo = await Client.CommentsManager.GetInformationAsync(c.Id);

            Assert.AreEqual(c.Id, cInfo.Id, "two comment objects have different ids");
            Assert.AreEqual(BoxType.comment.ToString(), cInfo.Type, "returned object is not a comment");

            // Update the comment
            const string UpdateMessage = "this is an updated test comment";

            var updateReq = new BoxCommentRequest()
            {
                Message = UpdateMessage
            };

            BoxComment cUpdate = await Client.CommentsManager.UpdateAsync(c.Id, updateReq);

            Assert.AreEqual(c.Id, cUpdate.Id, "Wrong comment was updated");
            Assert.AreEqual(BoxType.comment.ToString(), cUpdate.Type, "returned type of update is not a comment");
            Assert.AreEqual(UpdateMessage, cUpdate.Message, "Comment was not updated with correct string");

            // Deleting a comment
            var success = await Client.CommentsManager.DeleteAsync(c.Id);

            Assert.AreEqual(true, success, "Unsuccessful comment delete");
        }
    }
}
