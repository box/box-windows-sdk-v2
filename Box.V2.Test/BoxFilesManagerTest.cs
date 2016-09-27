﻿using Box.V2.Managers;
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
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxFile f = await _filesManager.GetInformationAsync("fakeId");

            /*** Assert ***/
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
            _handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxFile>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxFile>>>(new BoxResponse<BoxCollection<BoxFile>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            var fakeFileRequest = new BoxFileRequest()
            {
                Name = "test.txt",
                ContentCreatedAt = DateTime.Now,
                ContentModifiedAt = DateTime.Now,
                Parent = new BoxRequestEntity() { Id = "0" }
            };

            var fakeStream = new Mock<System.IO.Stream>();

            /*** Act ***/
            BoxFile f = await _filesManager.UploadAsync(fakeFileRequest, fakeStream.Object);

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
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));


            BoxFileRequest request = new BoxFileRequest()
            {
                Id = "5000948880",
                Name = "test",
                Parent = new BoxRequestEntity() { Id = "0" }
            };

            /*** Act ***/
            BoxFile f = await _filesManager.CopyAsync(request);

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
            _handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxFileLockRequest request = new BoxFileLockRequest();
            request.Lock = new BoxFileLock();
            request.Lock.IsDownloadPrevented = false;

            BoxFileLock fileLock = await _filesManager.UpdateLockAsync(request, "0");

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
    }
}
