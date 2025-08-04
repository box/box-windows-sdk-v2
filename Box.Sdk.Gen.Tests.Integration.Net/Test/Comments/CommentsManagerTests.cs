using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class CommentsManagerTests {
        public BoxClient client { get; }

        public CommentsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestComments() {
            const int fileSize = 256;
            string fileName = Utils.GetUUID();
            System.IO.Stream fileByteStream = Utils.GenerateByteStream(size: fileSize);
            const string parentId = "0";
            Files uploadedFiles = await client.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: fileName, parent: new UploadFileRequestBodyAttributesParentField(id: parentId)), file: fileByteStream));
            string fileId = NullableUtils.Unwrap(uploadedFiles.Entries)[0].Id;
            Comments comments = await client.Comments.GetFileCommentsAsync(fileId: fileId);
            Assert.IsTrue(comments.TotalCount == 0);
            const string message = "Hello there!";
            CommentFull newComment = await client.Comments.CreateCommentAsync(requestBody: new CreateCommentRequestBody(message: message, item: new CreateCommentRequestBodyItemField(id: fileId, type: CreateCommentRequestBodyItemTypeField.File)));
            Assert.IsTrue(newComment.Message == message);
            Assert.IsTrue(newComment.IsReplyComment == false);
            Assert.IsTrue(NullableUtils.Unwrap(newComment.Item).Id == fileId);
            CommentFull newReplyComment = await client.Comments.CreateCommentAsync(requestBody: new CreateCommentRequestBody(message: message, item: new CreateCommentRequestBodyItemField(id: NullableUtils.Unwrap(newComment.Id), type: CreateCommentRequestBodyItemTypeField.Comment)));
            Assert.IsTrue(newReplyComment.Message == message);
            Assert.IsTrue(newReplyComment.IsReplyComment == true);
            const string newMessage = "Hi!";
            await client.Comments.UpdateCommentByIdAsync(commentId: NullableUtils.Unwrap(newReplyComment.Id), requestBody: new UpdateCommentByIdRequestBody() { Message = newMessage });
            Comments newComments = await client.Comments.GetFileCommentsAsync(fileId: fileId);
            Assert.IsTrue(newComments.TotalCount == 2);
            Assert.IsTrue(NullableUtils.Unwrap(newComments.Entries)[1].Message == newMessage);
            CommentFull receivedComment = await client.Comments.GetCommentByIdAsync(commentId: NullableUtils.Unwrap(newComment.Id));
            Assert.IsTrue(NullableUtils.Unwrap(receivedComment.Message) == NullableUtils.Unwrap(newComment.Message));
            await client.Comments.DeleteCommentByIdAsync(commentId: NullableUtils.Unwrap(newComment.Id));
            await Assert.That.IsExceptionAsync(async() => await client.Comments.GetCommentByIdAsync(commentId: NullableUtils.Unwrap(newComment.Id)));
            await client.Files.DeleteFileByIdAsync(fileId: fileId);
        }

    }
}