using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxFilesManagerTest : BoxResourceManagerTest
    {
        protected BoxFilesManager _filesManager;

        public BoxFilesManagerTest()
        {
            _filesManager = new BoxFilesManager(_config.Object, _service, _converter, _authRepository);
        }

        [TestMethod]
        public async Task GetFileInformation_ValidResponse_ValidFile()
        {
            /*** Arrange ***/
            string responseString = "{ \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\", \"description\": \"a picture of tigers\", \"size\": 629644, \"path_collection\": { \"total_count\": 2, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" } ] }, \"created_at\": \"2012-12-12T10:55:30-08:00\", \"modified_at\": \"2012-12-12T11:04:26-08:00\", \"trashed_at\": null, \"purged_at\": null, \"content_created_at\": \"2013-02-04T16:57:52-08:00\", \"content_modified_at\": \"2013-02-04T16:57:52-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/rh935iit6ewrmw0unyul\", \"download_url\": \"https://www.box.com/shared/static/rh935iit6ewrmw0unyul.jpeg\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" }, \"item_status\": \"active\", \"tags\": [ \"important\", \"needs review\" ] }";
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                })).Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxFile f = await _filesManager.GetInformationAsync("fakeId");

            /*** Assert ***/
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(_FilesUri + "fakeId", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.IsNull(boxRequest.Payload);

            Assert.AreEqual("5000948880", f.Id);
            Assert.AreEqual("3", f.SequenceId);
            Assert.AreEqual("tigers.jpeg", f.Name);
            Assert.AreEqual("134b65991ed521fcfe4724b7d814ab8ded5185dc", f.Sha1);
            Assert.AreEqual(629644, f.Size);
            Assert.AreEqual("https://www.box.com/s/rh935iit6ewrmw0unyul", f.SharedLink.Url);
            Assert.AreEqual("important", f.Tags[0]);
            Assert.AreEqual("needs review", f.Tags[1]);
        }

        [TestMethod]
        public async Task UploadFile_ValidResponse_ValidFile()
        {
            /*** Arrange ***/
            string responseString = "{ \"total_count\": 1, \"entries\": [ { \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\", \"description\": \"a picture of tigers\", \"size\": 629644, \"path_collection\": { \"total_count\": 2, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" } ] }, \"created_at\": \"2012-12-12T10:55:30-08:00\", \"modified_at\": \"2012-12-12T11:04:26-08:00\", \"trashed_at\": null, \"purged_at\": null, \"content_created_at\": \"2013-02-04T16:57:52-08:00\", \"content_modified_at\": \"2013-02-04T16:57:52-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": null, \"parent\": { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" }, \"item_status\": \"active\", \"tags\": [ \"important\", \"needs review\" ] } ] }";
            BoxMultiPartRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxFile>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxFile>>>(new BoxResponse<BoxCollection<BoxFile>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r as BoxMultiPartRequest);

            var createdAt = new DateTime(2016, 8, 27);
            var modifiedAt = new DateTime(2016, 8, 28);

            var fakeFileRequest = new BoxFileRequest()
            {
                Name = "test.txt",
                ContentCreatedAt = createdAt,
                ContentModifiedAt = modifiedAt,
                Parent = new BoxRequestEntity() { Id = "0" }
            };

            var createdAtString = createdAt.ToString("yyyy-MM-ddTHH:mm:sszzz");
            var modifiedAtString = modifiedAt.ToString("yyyy-MM-ddTHH:mm:sszzz");

            var fakeStream = new Mock<System.IO.Stream>();

            /*** Act ***/
            BoxFile f = await _filesManager.UploadAsync(fakeFileRequest, fakeStream.Object, null, null, new byte[] { 0, 1, 2, 3, 4 });

            /*** Assert ***/
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(_FilesUploadUri, boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.AreEqual(2, boxRequest.Parts.Count);
            Assert.AreEqual("attributes", boxRequest.Parts[0].Name);
            Assert.IsNotNull(boxRequest.Parts[0] as BoxStringFormPart);
            Assert.IsTrue(AreJsonStringsEqual(
                "{\"content_created_at\":\"" + createdAtString + "\",\"content_modified_at\":\"" + modifiedAtString + "\",\"parent\":{\"id\":\"0\"},\"name\":\"test.txt\"}",
                (boxRequest.Parts[0] as BoxStringFormPart).Value));
            Assert.AreEqual("file", boxRequest.Parts[1].Name);
            Assert.IsNotNull(boxRequest.Parts[1] as BoxFileFormPart);
            Assert.AreEqual("test.txt", (boxRequest.Parts[1] as BoxFileFormPart).FileName);
            Assert.IsTrue(object.ReferenceEquals(fakeStream.Object, (boxRequest.Parts[1] as BoxFileFormPart).Value));
            Assert.IsTrue(boxRequest.HttpHeaders.ContainsKey("Content-MD5"));
            Assert.AreEqual(HexStringFromBytes(new byte[] { 0, 1, 2, 3, 4 }), boxRequest.HttpHeaders["Content-MD5"]);

            Assert.AreEqual("5000948880", f.Id);
            Assert.AreEqual("3", f.SequenceId);
            Assert.AreEqual("tigers.jpeg", f.Name);
            Assert.AreEqual("134b65991ed521fcfe4724b7d814ab8ded5185dc", f.Sha1);
            Assert.AreEqual(629644, f.Size);
            Assert.AreEqual("important", f.Tags[0]);
            Assert.AreEqual("needs review", f.Tags[1]);
        }

        [TestMethod]
        public async Task UploadNewVersion_ValidResponse_ValidFile()
        {
            /*** Arrange ***/
            string responseString = "{ \"total_count\": 1, \"entries\": [ { \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\", \"description\": \"a picture of tigers\", \"size\": 629644, \"path_collection\": { \"total_count\": 2, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" } ] }, \"created_at\": \"2012-12-12T10:55:30-08:00\", \"modified_at\": \"2012-12-12T11:04:26-08:00\", \"trashed_at\": null, \"purged_at\": null, \"content_created_at\": \"2013-02-04T16:57:52-08:00\", \"content_modified_at\": \"2013-02-04T16:57:52-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/rh935iit6ewrmw0unyul\", \"download_url\": \"https://www.box.com/shared/static/rh935iit6ewrmw0unyul.jpeg\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" }, \"item_status\": \"active\", \"tags\": [ \"important\", \"needs review\" ] } ] }";
            _handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxFile>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxFile>>>(new BoxResponse<BoxCollection<BoxFile>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            var fakeStream = new Mock<System.IO.Stream>();

            /*** Act ***/
            BoxFile f = await _filesManager.UploadNewVersionAsync("fakeFile", "0", fakeStream.Object, "1");

            /*** Assert ***/
            Assert.AreEqual("5000948880", f.Id);
            Assert.AreEqual("3", f.SequenceId);
            Assert.AreEqual("tigers.jpeg", f.Name);
            Assert.AreEqual("134b65991ed521fcfe4724b7d814ab8ded5185dc", f.Sha1);
            Assert.AreEqual(629644, f.Size);
            Assert.AreEqual("important", f.Tags[0]);
            Assert.AreEqual("needs review", f.Tags[1]);
        }

        [TestMethod]
        public async Task ViewVersions_ValidResponse_ValidFileVersions()
        {
            /*** Arrange ***/
            string responseString = "{ \"total_count\": 1, \"entries\": [ { \"type\": \"file_version\", \"id\": \"672259576\", \"sha1\": \"359c6c1ed98081b9a69eb3513b9deced59c957f9\", \"name\": \"Dragons.js\", \"size\": 92556, \"created_at\": \"2012-08-20T10:20:30-07:00\", \"modified_at\": \"2012-11-28T13:14:58-08:00\", \"modified_by\": { \"type\": \"user\", \"id\": \"183732129\", \"name\": \"sean rose\", \"login\": \"sean+apitest@box.com\" } } ] }";
            _handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxFileVersion>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxFileVersion>>>(new BoxResponse<BoxCollection<BoxFileVersion>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxCollection<BoxFileVersion> c = await _filesManager.ViewVersionsAsync("0");

            /*** Assert ***/
            Assert.AreEqual(c.TotalCount, 1);
            Assert.AreEqual(c.Entries.Count, 1);
            BoxFileVersion f = c.Entries.First();
            Assert.AreEqual("file_version", f.Type);
            Assert.AreEqual("672259576", f.Id);
            Assert.AreEqual("359c6c1ed98081b9a69eb3513b9deced59c957f9", f.Sha1);
            Assert.AreEqual("Dragons.js", f.Name);
            Assert.AreEqual(DateTime.Parse("2012-08-20T10:20:30-07:00"), f.CreatedAt);
            Assert.AreEqual(DateTime.Parse("2012-11-28T13:14:58-08:00"), f.ModifiedAt);
            Assert.AreEqual(92556, f.Size);
            Assert.AreEqual("user", f.ModifiedBy.Type);
            Assert.AreEqual("183732129", f.ModifiedBy.Id);
            Assert.AreEqual("sean rose", f.ModifiedBy.Name);
            Assert.AreEqual("sean+apitest@box.com", f.ModifiedBy.Login);
        }

        [TestMethod]
        public async Task UpdateFileInformation_ValidResponse_ValidFile()
        {
            string responseString = "{ \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"new name.jpg\", \"description\": \"a picture of tigers\", \"size\": 629644, \"path_collection\": { \"total_count\": 2, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" } ] }, \"created_at\": \"2012-12-12T10:55:30-08:00\", \"modified_at\": \"2012-12-12T11:04:26-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/rh935iit6ewrmw0unyul\", \"download_url\": \"https://www.box.com/shared/static/rh935iit6ewrmw0unyul.jpeg\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" }, \"item_status\": \"active\", \"tags\": [ \"important\", \"needs review\" ] }";
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxFileRequest request = new BoxFileRequest()
            {
                Id = "fakeId"
            };

            BoxFile f = await _filesManager.UpdateInformationAsync(request);

            /*** Assert ***/

            Assert.AreEqual("5000948880", f.Id);
            Assert.AreEqual("3", f.SequenceId);
            Assert.AreEqual("3", f.ETag);
            Assert.AreEqual("134b65991ed521fcfe4724b7d814ab8ded5185dc", f.Sha1);
            Assert.AreEqual("file", f.Type);
            Assert.AreEqual("sean rose", f.CreatedBy.Name);
            Assert.AreEqual("sean@box.com", f.CreatedBy.Login);
            Assert.AreEqual("user", f.CreatedBy.Type);
            Assert.AreEqual("17738362", f.CreatedBy.Id);
            Assert.AreEqual("important", f.Tags[0]);
            Assert.AreEqual("needs review", f.Tags[1]);
        }

        [TestMethod]
        public async Task CopyFile_ValidResponse_ValidFile()
        {
            /*** Arrange ***/
            string responseString = "{ \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\", \"description\": \"a picture of tigers\", \"size\": 629644, \"path_collection\": { \"total_count\": 2, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" } ] }, \"created_at\": \"2012-12-12T10:55:30-08:00\", \"modified_at\": \"2012-12-12T11:04:26-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/rh935iit6ewrmw0unyul\", \"download_url\": \"https://www.box.com/shared/static/rh935iit6ewrmw0unyul.jpeg\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" }, \"item_status\": \"active\", \"tags\": [ \"important\", \"needs review\" ] }";
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            BoxFileRequest request = new BoxFileRequest()
            {
                Id = "5000948880",
                Name = "test",
                Parent = new BoxRequestEntity() { Id = "0" }
            };


            /*** Act ***/
            BoxFile f = await _filesManager.CopyAsync(request);


            /*** Assert ***/
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(_FilesUri + "5000948880/copy", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.IsTrue(AreJsonStringsEqual("{\"parent\":{\"id\":\"0\"},\"name\":\"test\"}", boxRequest.Payload));

            Assert.AreEqual("5000948880", f.Id);
            Assert.AreEqual("3", f.SequenceId);
            Assert.AreEqual("3", f.ETag);
            Assert.AreEqual("134b65991ed521fcfe4724b7d814ab8ded5185dc", f.Sha1);
            Assert.AreEqual("file", f.Type);
            Assert.AreEqual("sean rose", f.CreatedBy.Name);
            Assert.AreEqual("sean@box.com", f.CreatedBy.Login);
            Assert.AreEqual("user", f.CreatedBy.Type);
            Assert.AreEqual("17738362", f.CreatedBy.Id);
            Assert.AreEqual("important", f.Tags[0]);
            Assert.AreEqual("needs review", f.Tags[1]);
        }

        [TestMethod]
        public async Task CreateFileSharedLink_ValidResponse_ValidFile()
        {
            /*** Arrange ***/
            string responseString = "{ \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\", \"description\": \"a picture of tigers\", \"size\": 629644, \"path_collection\": { \"total_count\": 2, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" } ] }, \"created_at\": \"2012-12-12T10:55:30-08:00\", \"modified_at\": \"2012-12-12T11:04:26-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/rh935iit6ewrmw0unyul\", \"download_url\": \"https://www.box.com/shared/static/rh935iit6ewrmw0unyul.jpeg\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" }, \"item_status\": \"active\" }";
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            BoxSharedLinkRequest sharedLink = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.collaborators
            };

            /*** Act ***/
            BoxFile f = await _filesManager.CreateSharedLinkAsync("0", sharedLink);

            /*** Assert ***/
            Assert.AreEqual("5000948880", f.Id);
            Assert.AreEqual("3", f.SequenceId);
            Assert.AreEqual("3", f.ETag);
            Assert.AreEqual("134b65991ed521fcfe4724b7d814ab8ded5185dc", f.Sha1);
            Assert.AreEqual("file", f.Type);
            Assert.AreEqual("sean rose", f.CreatedBy.Name);
            Assert.AreEqual("sean@box.com", f.CreatedBy.Login);
            Assert.AreEqual("user", f.CreatedBy.Type);
            Assert.AreEqual("17738362", f.CreatedBy.Id);
        }

        [TestMethod]
        public async Task ViewFileComments_ValidResponse_ValidFile()
        {
            /*** Arrange ***/
            string responseString = "{ \"total_count\": 1, \"entries\": [ { \"type\": \"comment\", \"id\": \"191969\", \"is_reply_comment\": false, \"message\": \"These tigers are cool!\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"created_at\": \"2012-12-12T11:25:01-08:00\", \"item\": { \"id\": \"5000948880\", \"type\": \"file\" }, \"modified_at\": \"2012-12-12T11:25:01-08:00\" } ] }";
            _handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxComment>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxComment>>>(new BoxResponse<BoxCollection<BoxComment>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxCollection<BoxComment> c = await _filesManager.GetCommentsAsync("0");
            BoxComment comment = c.Entries.FirstOrDefault();

            /*** Assert ***/
            Assert.AreEqual(1, c.TotalCount);
            Assert.AreEqual("191969", comment.Id);
            Assert.AreEqual(false, comment.IsReplyComment);
            Assert.AreEqual("These tigers are cool!", comment.Message);
            Assert.AreEqual("user", comment.CreatedBy.Type);
            Assert.AreEqual("17738362", comment.CreatedBy.Id);
            Assert.AreEqual("sean rose", comment.CreatedBy.Name);
            Assert.AreEqual("sean@box.com", comment.CreatedBy.Login);
        }

        [TestMethod]
        public async Task GetTrashedFile_ValidResponse_ValidFile()
        {
            /*** Arrange ***/
            string responseString = "{ \"type\": \"file\", \"id\": \"5859258256\", \"sequence_id\": \"2\", \"etag\": \"2\", \"sha1\": \"4bd9e98652799fc57cf9423e13629c151152ce6c\", \"name\": \"Screenshot_1_30_13_6_37_PM.png\", \"description\": \"\", \"size\": 163265, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"1\", \"sequence_id\": null, \"etag\": null, \"name\": \"Trash\" } ] }, \"created_at\": \"2013-01-30T18:43:56-08:00\", \"modified_at\": \"2013-01-30T18:44:00-08:00\", \"trashed_at\": \"2013-02-07T10:49:34-08:00\", \"purged_at\": \"2013-03-09T10:49:34-08:00\", \"content_created_at\": \"2013-01-30T18:43:56-08:00\", \"content_modified_at\": \"2013-01-30T18:44:00-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"shared_link\": { \"url\": null, \"download_url\": null, \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"trashed\" }";
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxFile f = await _filesManager.GetTrashedAsync("0");

            /*** Assert ***/
            Assert.AreEqual("5859258256", f.Id);
            Assert.AreEqual("2", f.SequenceId);
            Assert.AreEqual("2", f.ETag);
            Assert.AreEqual("4bd9e98652799fc57cf9423e13629c151152ce6c", f.Sha1);
            Assert.AreEqual("file", f.Type);
        }

        [TestMethod]
        public async Task RestoreTrashedFile_ValidResponse_ValidFile()
        {
            /*** Arrange ***/
            string responseString = "{ \"type\": \"file\", \"id\": \"5859258256\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"4bd9e98652799fc57cf9423e13629c151152ce6c\", \"name\": \"Screenshot_1_30_13_6_37_PM.png\", \"description\": \"\", \"size\": 163265, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_at\": \"2013-01-30T18:43:56-08:00\", \"modified_at\": \"2013-02-07T10:56:58-08:00\", \"trashed_at\": null, \"purged_at\": null, \"content_created_at\": \"2013-01-30T18:43:56-08:00\", \"content_modified_at\": \"2013-02-07T10:56:58-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"shared_link\": { \"url\": \"https://seanrose.box.com/s/ebgti08mtmhbpb4vlp55\", \"download_url\": \"https://seanrose.box.com/shared/static/ebgti08mtmhbpb4vlp55.png\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 4, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\" }";
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            BoxFileRequest fileReq = new BoxFileRequest()
            {
                Id = "0",
                Name = "test"
            };

            /*** Act ***/
            BoxFile f = await _filesManager.RestoreTrashedAsync(fileReq);

            /*** Assert ***/
            Assert.AreEqual("5859258256", f.Id);
            Assert.AreEqual("3", f.SequenceId);
            Assert.AreEqual("3", f.ETag);
            Assert.AreEqual("4bd9e98652799fc57cf9423e13629c151152ce6c", f.Sha1);
            Assert.AreEqual("file", f.Type);


        }

        [TestMethod]
        public async Task PurgeTrashedFile_ValidResponse_Success()
        {
            /*** Arrange ***/
            string responseString = "{ \"type\": \"file\", \"id\": \"5859258256\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"4bd9e98652799fc57cf9423e13629c151152ce6c\", \"name\": \"Screenshot_1_30_13_6_37_PM.png\", \"description\": \"\", \"size\": 163265, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_at\": \"2013-01-30T18:43:56-08:00\", \"modified_at\": \"2013-02-07T10:56:58-08:00\", \"trashed_at\": null, \"purged_at\": null, \"content_created_at\": \"2013-01-30T18:43:56-08:00\", \"content_modified_at\": \"2013-02-07T10:56:58-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"shared_link\": { \"url\": \"https://seanrose.box.com/s/ebgti08mtmhbpb4vlp55\", \"download_url\": \"https://seanrose.box.com/shared/static/ebgti08mtmhbpb4vlp55.png\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 4, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\" }";
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));


            /*** Act ***/
            bool success = await _filesManager.PurgeTrashedAsync("0");

            /*** Assert ***/
            Assert.AreEqual(true, success);
        }

        [TestMethod]
        public async Task GetLockFile_ValidResponse_Success()
        {
            /*** Arrange ***/
            string responseString = "{ \"type\": \"file\", \"id\": \"7435988481\", \"etag\": \"1\", \"lock\": { \"type\": \"lock\", \"id\": \"14516545\", \"created_by\": { \"type\": \"user\", \"id\": \"13130406\", \"name\": \"I don't know gmail\", \"login\": \"idontknow@gmail.com\" }, \"created_at\": \"2014-05-29T18:03:04-07:00\", \"expires_at\": \"2014-05-30T19:03:04-07:00\", \"is_download_prevented\": true } } ";

            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));


            /*** Act ***/
            BoxFileLock fileLock = await _filesManager.GetLockAsync("0");

            /*** Assert ***/
            Assert.IsNotNull(fileLock);
            Assert.AreEqual(true, fileLock.IsDownloadPrevented);
            Assert.AreEqual(DateTime.Parse("2014-05-30T19:03:04-07:00"), fileLock.ExpiresAt);
            Assert.AreEqual(DateTime.Parse("2014-05-29T18:03:04-07:00"), fileLock.CreatedAt);
            Assert.IsNotNull(fileLock.CreatedBy);
            Assert.AreEqual("I don't know gmail", fileLock.CreatedBy.Name);
            Assert.AreEqual("idontknow@gmail.com", fileLock.CreatedBy.Login);

        }

        [TestMethod]
        public async Task UpdateFileLock_ValidResponse_ValidFile()
        {
            string responseString = "{ \"type\": \"file\", \"id\": \"7435988481\", \"etag\": \"1\", \"lock\": { \"type\": \"lock\", \"id\": \"14516545\", \"created_by\": { \"type\": \"user\", \"id\": \"13130406\", \"name\": \"I don't know gmail\", \"login\": \"idontknow@gmail.com\" }, \"created_at\": \"2014-05-29T18:03:04-07:00\", \"expires_at\": \"2014-05-30T19:03:04-07:00\", \"is_download_prevented\": false } } ";
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                 .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxFileLockRequest request = new BoxFileLockRequest();
            request.Lock = new BoxFileLock();
            request.Lock.IsDownloadPrevented = false;

            BoxFileLock fileLock = await _filesManager.UpdateLockAsync(request, "7435988481");

            /*** Assert ***/
            Assert.IsNotNull(fileLock);
            Assert.AreEqual(false, fileLock.IsDownloadPrevented);
            Assert.AreEqual(DateTime.Parse("2014-05-30T19:03:04-07:00"), fileLock.ExpiresAt);
            Assert.AreEqual(DateTime.Parse("2014-05-29T18:03:04-07:00"), fileLock.CreatedAt);
            Assert.IsNotNull(fileLock.CreatedBy);
            Assert.AreEqual("I don't know gmail", fileLock.CreatedBy.Name);
            Assert.AreEqual("idontknow@gmail.com", fileLock.CreatedBy.Login);
        }

        [TestMethod]
        public async Task FileUnLock_ValidResponse()
        {
            string responseString = "{ \"type\": \"file\", \"id\": \"7435988481\", \"etag\": \"1\" } ";
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            bool unlocked = await _filesManager.UnLock("0");

            /*** Assert ***/
            Assert.IsTrue(unlocked);
        }

        [TestMethod]
        public async Task GetThumbnail_ValidResponse_ValidStream()
        {
            using (FileStream thumb = new FileStream(string.Format(getSaveFolderPath(), "thumb.png"), FileMode.OpenOrCreate))
            {
                /*** Arrange ***/

                _handler.Setup(h => h.ExecuteAsync<Stream>(It.IsAny<IBoxRequest>()))

                    .Returns(Task.FromResult<IBoxResponse<Stream>>(new BoxResponse<Stream>()
                    {
                        Status = ResponseStatus.Success,
                        ResponseObject = thumb

                    }));

                /*** Act ***/
                Stream result = await _filesManager.GetThumbnailAsync("34122832467");

                /*** Assert ***/

                Assert.IsNotNull(result, "Stream is Null");

            }
        }
        private string getSaveFolderPath()
        {
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(pathUser, "Downloads") + "\\{0}";
        }
        [TestMethod]
        public async Task PreflightCheck_ValidResponse_ValidStatus()
        {
            /*** Arrange ***/
            string responseString = "";
            _handler.Setup(h => h.ExecuteAsync<BoxPreflightCheck>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxPreflightCheck>>(new BoxResponse<BoxPreflightCheck>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString,
                    ResponseObject = new BoxPreflightCheck()
                }));

            /*** Act ***/
            BoxPreflightCheck result = await _filesManager.PreflightCheck(new BoxPreflightCheckRequest()
            {
                Name = "Wolves owners.ppt",
                Parent = new BoxRequestEntity()
                {
                    Id = "1523432"
                },
                Size = 15243
            });

            /*** Assert ***/

            Assert.AreEqual(true, result.Success);


        }
        [TestMethod]
        public async Task DeleteFile_ValidResponse_FileDeleted()
        {
            /*** Arrange ***/
            string responseString = "";
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            bool result = await _filesManager.DeleteAsync("34122832467");

            /*** Assert ***/

            Assert.AreEqual(true, result);


        }
        [TestMethod]
        public async Task DownloadStream_ValidResponse_ValidStream()
        {

            using (FileStream exampleFile = new FileStream(string.Format(getSaveFolderPath(), "example.png"), FileMode.OpenOrCreate))
            {
                /*** Arrange ***/
                Uri location = new Uri("http://dl.boxcloud.com");
                HttpResponseHeaders headers = CreateInstanceNonPublicConstructor<HttpResponseHeaders>();
                headers.Location = location;
                _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))

                    .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                    {
                        Status = ResponseStatus.Success,
                        Headers = headers

                    }));
                IBoxRequest boxRequest = null;
                _handler.Setup(h => h.ExecuteAsync<Stream>(It.IsAny<IBoxRequest>()))

                   .Returns(Task.FromResult<IBoxResponse<Stream>>(new BoxResponse<Stream>()
                   {
                       Status = ResponseStatus.Success,
                       ResponseObject = exampleFile

                   }))
                   .Callback<IBoxRequest>(r => boxRequest = r); ;

                /*** Act ***/
                Stream result = await _filesManager.DownloadStreamAsync("34122832467");

                /*** Assert ***/

                Assert.IsNotNull(result, "Stream is Null");

            }
        }

        [TestMethod]
        public async Task GetEmbedLink_ValidResponse_ValidEmbedLink()
        {
            /*** Arrange ***/
            string responseString = "{\"type\": \"file\",\"id\": \"34122832467\", \"etag\": \"1\", \"expiring_embed_link\": { \"url\": \"https://app.box.com/preview/expiring_embed/gvoct6FE!Qz2rDeyxCiHsYpvlnR7JJ0SCfFM2M4YiX9cIwrSo4LOYQgxyP3rzoYuMmXg96mTAidqjPuRH7HFXMWgXEEm5LTi1EDlfBocS-iRfHpc5ZeYrAZpA5B8C0Obzkr4bUoF6wGq8BZ1noN_txyZUU1nLDNuL_u0rsImWhPAZlvgt7662F9lZSQ8nw6zKaRWGyqmj06PnxewCx0EQD3padm6VYkfHE2N20gb5rw1D0a7aaRJZzEijb2ICLItqfMlZ5vBe7zGdEn3agDzZP7JlID3FYdPTITsegB10gKLgSp_AJJ9QAfDv8mzi0bGv1ZmAU1FoVLpGC0XI0UKy3N795rZBtjLlTNcuxapbHkUCoKcgdfmHEn5NRQ3tmw7hiBfnX8o-Au34ttW9ntPspdAQHL6xPzQC4OutWZDozsA5P9sGlI-sC3VC2-WXsbXSedemubVd5vWzpVZtKRlb0gpuXsnDPXnMxSH7_jT4KSLhC8b5kEMPNo33FjEJl5pwS_o_6K0awUdRpEQIxM9CC3pBUZK5ooAc5X5zxo_2FBr1xq1p_kSbt4TVnNeohiLIu38TQysSb7CMR7JRhDDZhMMwAUc0wdSszELgL053lJlPeoiaLA49rAGP_B3BVuwFAFEl696w7UMx5NKu1mA0IOn9pDebzbhTl5HuUvBAHROc1Ocjb28Svyotik1IkPIw_1R33ZyAMvEFyzIygqBj8WedQeSK38iXvF2UXvkAf9kevOdnpwsKYiJtcxeJhFm7LUVKDTufuzuGRw-T7cPtbg..\" } }";
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            Uri embedLinkUrl = await _filesManager.GetPreviewLinkAsync("fakeId");

            /*** Assert ***/

            Assert.IsNotNull(embedLinkUrl);


        }
        [TestMethod]
        public async Task GetFileTasks_ValidResponse_ValidTasks()
        {
            /*** Arrange ***/
            string responseString = "{\"total_count\": 1, \"entries\": [{\"type\": \"task\", \"id\": \"1786931\",\"item\": {\"type\": \"file\",\"id\": \"7026335894\", \"sequence_id\": \"6\", \"etag\": \"6\", \"sha1\": \"81cc829fb8366fcfc108aa6c5a9bde01a6a10c16\",\"name\": \"API - Persist On-Behalf-Of information.docx\" }, \"due_at\": null }   ] }";
            _handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxTask>>(It.IsAny<IBoxRequest>()))
                 .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxTask>>>(new BoxResponse<BoxCollection<BoxTask>>()
                 {
                     Status = ResponseStatus.Success,
                     ContentString = responseString
                 }));

            /*** Act ***/
            BoxCollection<BoxTask> tasks = await _filesManager.GetFileTasks("fakeId");

            /*** Assert ***/

            BoxTask task = tasks.Entries.FirstOrDefault();

            /*** Assert ***/
            Assert.AreEqual(1, tasks.TotalCount);
            Assert.AreEqual("1786931", task.Id);
            Assert.AreEqual("task", task.Type);
            Assert.AreEqual("API - Persist On-Behalf-Of information.docx", task.Item.Name);
            Assert.AreEqual("7026335894", task.Item.Id);



        }

        [TestMethod]
        public async Task GetWatermarkForFile_ValidResponse_ValidWatermark()
        {
            /*** Arrange ***/
            string responseString = @"{
                                          ""watermark"": {
                                            ""created_at"": ""2016-10-31T15:33:33-07:00"",
                                            ""modified_at"": ""2016-10-31T15:33:33-07:00""
                                          }
                                       }";
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<BoxWatermarkResponse>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWatermarkResponse>>(new BoxResponse<BoxWatermarkResponse>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxWatermark result = await _filesManager.GetWatermarkAsync("5010739069");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(_FilesUri + "5010739069/watermark", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(DateTime.Parse("2016-10-31T15:33:33-07:00"), result.CreatedAt.Value);
            Assert.AreEqual(DateTime.Parse("2016-10-31T15:33:33-07:00"), result.ModifiedAt.Value);
        }

        [TestMethod]
        public async Task ApplyWatermarkToFile_ValidResponse_ValidWatermark()
        {
            /*** Arrange ***/
            string responseString = @"{
                                          ""watermark"": {
                                            ""created_at"": ""2016-10-31T15:33:33-07:00"",
                                            ""modified_at"": ""2016-10-31T15:33:33-07:00""
                                          }
                                       }";
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<BoxWatermarkResponse>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWatermarkResponse>>(new BoxResponse<BoxWatermarkResponse>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxWatermark result = await _filesManager.ApplyWatermarkAsync("5010739069");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(_FilesUri + "5010739069/watermark", boxRequest.AbsoluteUri.AbsoluteUri);
            BoxApplyWatermarkRequest payload = JsonConvert.DeserializeObject<BoxApplyWatermarkRequest>(boxRequest.Payload);
            Assert.AreEqual("default", payload.Watermark.Imprint);

            //Response check
            Assert.AreEqual(DateTime.Parse("2016-10-31T15:33:33-07:00"), result.CreatedAt.Value);
            Assert.AreEqual(DateTime.Parse("2016-10-31T15:33:33-07:00"), result.ModifiedAt.Value);
        }

        [TestMethod]
        public async Task RemoveWatermarkFromFile_ValidResponse_RemovedWatermark()
        {
            /*** Arrange ***/
            string responseString = "";
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<BoxWatermarkResponse>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWatermarkResponse>>(new BoxResponse<BoxWatermarkResponse>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            bool result = await _filesManager.RemoveWatermarkAsync("5010739069");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(_FilesUri + "5010739069/watermark", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(true, result);
           
        }
        [TestMethod]
        public async Task DeleteOldVersion_ValidReponse()
        {
            /*** Arrange ***/
            string responseString = "";
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
           .Returns(() => Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
           {
               Status = ResponseStatus.Success,
               ContentString = responseString
           }))
           .Callback<IBoxRequest>(r => boxRequest = r);

            bool result = await _filesManager.DeleteOldVersionAsync("fileid", "versionid");

            /*** Assert ***/

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(_FilesUri + "fileid/versions/versionid", boxRequest.AbsoluteUri.AbsoluteUri);

            // Response check
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task PromoteVersion_ValidResponse()
        {
            /*** Arrange ***/
            string responseString = "{\"type\":\"file_version\",\"id\":\"871399\",\"sha1\":\"12039d6dd9a7e6eefc78846802e\",\"name\":\"Stark Family Lineage.doc\",\"size\":11,\"created_at\":\"2013-11-20T13:20:50-08:00\",\"modified_at\":\"2013-11-20T13:26:48-08:00\",\"modified_by\":{\"type\":\"user\",\"id\":\"13711334\",\"name\":\"Eddard Stark\",\"login\":\"ned@winterfell.com\"}}";
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<BoxFileVersion>(It.IsAny<IBoxRequest>()))
                 .Returns(Task.FromResult<IBoxResponse<BoxFileVersion>>(new BoxResponse<BoxFileVersion>()
                 {
                     Status = ResponseStatus.Success,
                     ContentString = responseString
                 }))
                 .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxFileVersion result = await _filesManager.PromoteVersionAsync("fileid", "871399");

            /*** Assert ***/

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(_FilesUri + "fileid/versions/current", boxRequest.AbsoluteUri.AbsoluteUri);
            BoxPromoteVersionRequest payload = JsonConvert.DeserializeObject<BoxPromoteVersionRequest>(boxRequest.Payload);
            Assert.AreEqual("871399", payload.Id);
            Assert.AreEqual("file_version", payload.Type);

            // Response check
            Assert.AreEqual("871399", result.Id);
            Assert.AreEqual("file_version", result.Type);
            Assert.AreEqual("Stark Family Lineage.doc", result.Name);
            Assert.AreEqual(DateTime.Parse("2013-11-20T13:20:50-08:00"), result.CreatedAt);
            Assert.AreEqual(DateTime.Parse("2013-11-20T13:26:48-08:00"), result.ModifiedAt);
        }
    }
}
