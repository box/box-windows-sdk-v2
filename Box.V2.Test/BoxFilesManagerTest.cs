using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Box.V2.Exceptions;
using Box.V2.Managers;
using Box.V2.Models;
using Box.V2.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxFilesManagerTest : BoxResourceManagerTest
    {
        private readonly BoxFilesManager _filesManager;

        public BoxFilesManagerTest()
        {
            _filesManager = new BoxFilesManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task UploadNewVersionUsingSessionAsync_ValidResponse()
        {
            var fileInMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes("whatever"));

            /*** Arrange ***/
            var uploadSessionResponseString = "{ \"total_parts\": \"2\", \"part_size\": \"8388608\", \"session_endpoints\": { \"list_parts\": \"https://upload.box.com/api/2.0/files/upload_sessions/F971964745A5CD0C001BBE4E58196BFD/parts\", \"commit\": \"https://upload.box.com/api/2.0/files/upload_sessions/F971964745A5CD0C001BBE4E58196BFD/commit\", \"upload_part\": \"https://upload.box.com/api/2.0/files/upload_sessions/F971964745A5CD0C001BBE4E58196BFD\", \"status\": \"https://upload.box.com/api/2.0/files/upload_sessions/F971964745A5CD0C001BBE4E58196BFD\", \"abort\": \"https://upload.box.com/api/2.0/files/upload_sessions/F971964745A5CD0C001BBE4E58196BFD\" }, \"session_expires_at\": \"2017-04-18T01:45:15Z\", \"id\": \"F971964745A5CD0C001BBE4E58196BFD\", \"type\": \"upload_session\", \"num_parts_processed\": \"0\" }";
            Handler.Setup(h => h.ExecuteAsync<BoxFileUploadSession>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFileUploadSession>>(new BoxResponse<BoxFileUploadSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = uploadSessionResponseString
                }));


            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxFile>>(It.IsAny<IBoxRequest>()))
                 .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxFile>>>(new BoxResponse<BoxCollection<BoxFile>>()
                 {
                     Status = ResponseStatus.Success,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/UploadNewVersionUsingSession200.json")
                 }));

            var fakeStream = new Mock<System.IO.Stream>();

            BoxFile f = await _filesManager.UploadNewVersionUsingSessionAsync(fakeStream.Object, "fakeId", null, null, null);
            Assert.AreEqual("file", f.Type);
            Assert.AreEqual("5000948880", f.Id);
        }

        [TestMethod]
        public async Task GetCollaborationsCollectionAsync_ValidResponse_NextMarker()
        {
            var responseJSON = "{\"next_marker\":\"ZmlsZS0xLTE%3D\",\"previous_marker\":\"\",\"entries\":[{\"type\":\"collaboration\",\"id\":\"11111\",\"created_by\":{\"type\":\"user\",\"id\":\"33333\",\"name\":\"Test User\",\"login\":\"testuser@example.com\"},\"created_at\":\"2019-01-21T07:58:18-08:00\",\"modified_at\":\"2019-01-21T14:49:18-08:00\",\"expires_at\":null,\"status\":\"accepted\",\"accessible_by\":{\"type\":\"user\",\"id\":\"44444\",\"name\":\"Test User 2\",\"login\":\"testuser2@example.com\"},\"role\":\"editor\",\"acknowledged_at\":\"2019-01-21T07:58:18-08:00\",\"item\":{\"type\":\"file\",\"id\":\"22222\",\"file_version\":{\"type\":\"file_version\",\"id\":\"12345\",\"sha1\":\"96619397759a43a01537da34ea3e0bab86b22e9d\"},\"sequence_id\":\"26\",\"etag\":\"26\",\"sha1\":\"96619397759a43a01537da34ea3e0bab86b22e9d\",\"name\":\"Meeting Notes.boxnote\"}}]}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBasedV2<BoxCollaboration>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollectionMarkerBasedV2<BoxCollaboration>>>(new BoxResponse<BoxCollectionMarkerBasedV2<BoxCollaboration>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseJSON
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            var collabs = await _filesManager.GetCollaborationsCollectionAsync("22222", limit: 1);

            Assert.AreEqual("https://api.box.com/2.0/files/22222/collaborations?limit=1", boxRequest.AbsoluteUri.AbsoluteUri);

            Assert.AreEqual(1, collabs.Entries.Count);
            Assert.AreEqual("Test User", collabs.Entries[0].CreatedBy.Name);
            Assert.AreEqual("ZmlsZS0xLTE%3D", collabs.NextMarker);
        }

        [TestMethod]
        public async Task GetFileInformation_ValidResponse_ValidFile()
        {
            /*** Arrange ***/
            var responseString = @"{
                ""type"": ""file"",
                ""id"": ""5000948880"",
                ""sequence_id"": ""3"",
                ""etag"": ""3"",
                ""sha1"": ""134b65991ed521fcfe4724b7d814ab8ded5185dc"",
                ""name"": ""tigers.jpeg"",
                ""description"": ""a picture of tigers"",
                ""disposition_at"": ""2012-12-12T10:53:43-08:00"",
                ""size"": 629644,
                ""path_collection"": {
                    ""total_count"": 2,
                    ""entries"": [
                        {
                            ""type"": ""folder"",
                            ""id"": ""0"",
                            ""sequence_id"": null,
                            ""etag"": null,
                            ""name"": ""All Files""
                        },
                        {
                            ""type"": ""folder"",
                            ""id"": ""11446498"",
                            ""sequence_id"": ""1"",
                            ""etag"": ""1"",
                            ""name"": ""Pictures""
                        }
                    ]
                },
                ""created_at"": ""2012-12-12T10:55:30-08:00"",
                ""modified_at"": ""2012-12-12T11:04:26-08:00"",
                ""trashed_at"": null,
                ""purged_at"": null,
                ""content_created_at"": ""2013-02-04T16:57:52-08:00"",
                ""content_modified_at"": ""2013-02-04T16:57:52-08:00"",
                ""uploader_display_name"": ""sean rose"",
                ""created_by"": {
                    ""type"": ""user"",
                    ""id"": ""17738362"",
                    ""name"": ""sean rose"",
                    ""login"": ""sean@box.com""
                },
                ""modified_by"": {
                    ""type"": ""user"",
                    ""id"": ""17738362"",
                    ""name"": ""sean rose"",
                    ""login"": ""sean@box.com""
                },
                ""owned_by"": {
                    ""type"": ""user"",
                    ""id"": ""17738362"",
                    ""name"": ""sean rose"",
                    ""login"": ""sean@box.com""
                },
                ""shared_link"": {
                    ""url"": ""https://www.box.com/s/rh935iit6ewrmw0unyul"",
                    ""download_url"": ""https://www.box.com/shared/static/rh935iit6ewrmw0unyul.jpeg"",
                    ""vanity_url"": null,
                    ""is_password_enabled"": false,
                    ""unshared_at"": null,
                    ""download_count"": 0,
                    ""preview_count"": 0,
                    ""access"": ""open"",
                    ""permissions"": {
                        ""can_download"": true,
                        ""can_preview"": true
                    }
                },
                ""parent"": {
                    ""type"": ""folder"",
                    ""id"": ""11446498"",
                    ""sequence_id"": ""1"",
                    ""etag"": ""1"",
                    ""name"": ""Pictures"" },
                ""item_status"": ""active"",
                ""tags"": [ ""important"", ""needs review"" ],
                ""expires_at"": ""2020-11-03T22:00:00Z"",
                ""allowed_invitee_roles"": [ ""editor"" ],
                ""has_collaborations"": false,
                ""is_externally_owned"": false,
                ""classification"": {
                    ""name"": ""Top Secret"",
                    ""definition"": ""Content that should not be shared outside the company."",
                    ""color"": ""#FF0000""
                  }
            }";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
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
            Assert.AreEqual(FilesUri + "fakeId", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.IsNull(boxRequest.Payload);

            Assert.AreEqual("5000948880", f.Id);
            Assert.AreEqual("3", f.SequenceId);
            Assert.AreEqual("tigers.jpeg", f.Name);
            Assert.AreEqual("134b65991ed521fcfe4724b7d814ab8ded5185dc", f.Sha1);
            Assert.AreEqual(629644, f.Size);
            Assert.AreEqual("sean rose", f.UploaderDisplayName);
            Assert.AreEqual("https://www.box.com/s/rh935iit6ewrmw0unyul", f.SharedLink.Url);
            Assert.AreEqual("important", f.Tags[0]);
            Assert.AreEqual("needs review", f.Tags[1]);
            Assert.AreEqual("2020-11-03T22:00:00Z", f.ExpiresAt.Value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ssZ", DateTimeFormatInfo.InvariantInfo));
            Assert.AreEqual("2012-12-12T18:53:43Z", f.DispositionAt.Value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ssZ", DateTimeFormatInfo.InvariantInfo));
            Assert.AreEqual("editor", f.AllowedInviteeRoles.First());
            Assert.AreEqual("Top Secret", f.Classification.Name);
            Assert.AreEqual("Content that should not be shared outside the company.", f.Classification.Definition);
            Assert.AreEqual("#FF0000", f.Classification.Color);
            Assert.IsFalse(f.HasCollaborations.Value);
            Assert.IsFalse(f.IsExternallyOwned.Value);
        }

        [TestMethod]
        public async Task UploadFile_ValidResponse_ValidFile()
        {
            /*** Arrange ***/
            var responseString = "{ \"total_count\": 1, \"entries\": [ { \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\", \"description\": \"a picture of tigers\", \"size\": 629644, \"path_collection\": { \"total_count\": 2, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" } ] }, \"created_at\": \"2012-12-12T10:55:30-08:00\", \"modified_at\": \"2012-12-12T11:04:26-08:00\", \"trashed_at\": null, \"purged_at\": null, \"content_created_at\": \"2013-02-04T16:57:52-08:00\", \"content_modified_at\": \"2013-02-04T16:57:52-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": null, \"parent\": { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" }, \"item_status\": \"active\", \"tags\": [ \"important\", \"needs review\" ] } ] }";
            BoxMultiPartRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxFile>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxFile>>>(new BoxResponse<BoxCollection<BoxFile>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r as BoxMultiPartRequest);

            var createdAt = new DateTimeOffset(2016, 8, 27, 0, 0, 0, TimeSpan.Zero);
            var modifiedAt = new DateTimeOffset(2016, 8, 28, 0, 0, 0, TimeSpan.Zero);

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
            Assert.AreEqual(FilesUploadUri, boxRequest.AbsoluteUri.AbsoluteUri);
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
            var responseString = "{ \"total_count\": 1, \"entries\": [ { \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\", \"description\": \"a picture of tigers\", \"size\": 629644, \"path_collection\": { \"total_count\": 2, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" } ] }, \"created_at\": \"2012-12-12T10:55:30-08:00\", \"modified_at\": \"2012-12-12T11:04:26-08:00\", \"trashed_at\": null, \"purged_at\": null, \"content_created_at\": \"2013-02-04T16:57:52-08:00\", \"content_modified_at\": \"2013-02-04T16:57:52-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/rh935iit6ewrmw0unyul\", \"download_url\": \"https://www.box.com/shared/static/rh935iit6ewrmw0unyul.jpeg\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" }, \"item_status\": \"active\", \"tags\": [ \"important\", \"needs review\" ] } ] }";
            BoxMultiPartRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxFile>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxFile>>>(new BoxResponse<BoxCollection<BoxFile>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = (BoxMultiPartRequest)r);

            var fakeStream = new Mock<System.IO.Stream>();

            /*** Act ***/
            BoxFile f = await _filesManager.UploadNewVersionAsync("fakeFile", "0", fakeStream.Object, "1", contentModifiedTime: new DateTimeOffset(2020, 1, 1, 8, 0, 0, TimeSpan.Zero));
            var attrPart = (BoxStringFormPart)boxRequest.Parts[0];

            /*** Assert ***/
            Assert.AreEqual("attributes", attrPart.Name);
            Assert.AreEqual("{\"name\":\"fakeFile\",\"content_modified_at\":\"2020-01-01T08:00:00+00:00\"}", attrPart.Value);
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
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxFileVersion>>(It.Is<IBoxRequest>(r => "fields=version_number".Equals(r.GetQueryString()))))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxFileVersion>>>(new BoxResponse<BoxCollection<BoxFileVersion>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/ViewVersions200.json")
                }));

            /*** Act ***/
            BoxCollection<BoxFileVersion> c = await _filesManager.ViewVersionsAsync("0", new List<string>() { BoxFileVersion.FieldVersionNumber });

            /*** Assert ***/
            Assert.AreEqual(c.TotalCount, 1);
            Assert.AreEqual(c.Entries.Count, 1);
            BoxFileVersion f = c.Entries.First();
            Assert.AreEqual("file_version", f.Type);
            Assert.AreEqual("672259576", f.Id);
            Assert.AreEqual("359c6c1ed98081b9a69eb3513b9deced59c957f9", f.Sha1);
            Assert.AreEqual("Dragons.js", f.Name);
            Assert.AreEqual(DateTimeOffset.Parse("2012-08-20T10:20:30-07:00"), f.CreatedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2012-11-28T13:14:58-08:00"), f.ModifiedAt);
            Assert.AreEqual(92556, f.Size);
            Assert.AreEqual("user", f.ModifiedBy.Type);
            Assert.AreEqual("183732129", f.ModifiedBy.Id);
            Assert.AreEqual("sean rose", f.ModifiedBy.Name);
            Assert.AreEqual("sean+apitest@box.com", f.ModifiedBy.Login);
            Assert.AreEqual("1", f.VersionNumber);
        }

        [TestMethod]
        public async Task ViewVersionsWithOffsetAndLimit_ValidResponse_ValidFileVersions()
        {
            /*** Arrange ***/
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxFileVersion>>(It.Is<IBoxRequest>(r => "offset=100&limit=10".Equals(r.GetQueryString()))))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxFileVersion>>>(new BoxResponse<BoxCollection<BoxFileVersion>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/ViewVersions200.json")
                }));

            /*** Act ***/
            BoxCollection<BoxFileVersion> c = await _filesManager.ViewVersionsAsync("0", null, 100, 10);

            /*** Assert ***/
            Assert.AreEqual(c.TotalCount, 1);
            Assert.AreEqual(c.Entries.Count, 1);
            BoxFileVersion f = c.Entries.First();
            Assert.AreEqual("file_version", f.Type);
            Assert.AreEqual("672259576", f.Id);
            Assert.AreEqual("359c6c1ed98081b9a69eb3513b9deced59c957f9", f.Sha1);
            Assert.AreEqual("Dragons.js", f.Name);
            Assert.AreEqual(DateTimeOffset.Parse("2012-08-20T10:20:30-07:00"), f.CreatedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2012-11-28T13:14:58-08:00"), f.ModifiedAt);
            Assert.AreEqual(92556, f.Size);
            Assert.AreEqual("user", f.ModifiedBy.Type);
            Assert.AreEqual("183732129", f.ModifiedBy.Id);
            Assert.AreEqual("sean rose", f.ModifiedBy.Name);
            Assert.AreEqual("sean+apitest@box.com", f.ModifiedBy.Login);
            Assert.AreEqual("1", f.VersionNumber);
        }

        [TestMethod]
        public async Task UpdateFileInformation_ValidResponse_ValidFile()
        {
            var responseString = "{ \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"new name.jpg\", \"description\": \"a picture of tigers\", \"size\": 629644, \"path_collection\": { \"total_count\": 2, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" } ] }, \"created_at\": \"2012-12-12T10:55:30-08:00\", \"modified_at\": \"2012-12-12T11:04:26-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/rh935iit6ewrmw0unyul\", \"download_url\": \"https://www.box.com/shared/static/rh935iit6ewrmw0unyul.jpeg\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" }, \"item_status\": \"active\", \"tags\": [ \"important\", \"needs review\" ] }";
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            var request = new BoxFileRequest()
            {
                Id = "fakeId",
                DispositionAt = DateTimeOffset.Parse("2022-11-28T13:14:58-08:00")
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
            var responseString = "{ \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\", \"description\": \"a picture of tigers\", \"size\": 629644, \"path_collection\": { \"total_count\": 2, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" } ] }, \"created_at\": \"2012-12-12T10:55:30-08:00\", \"modified_at\": \"2012-12-12T11:04:26-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/rh935iit6ewrmw0unyul\", \"download_url\": \"https://www.box.com/shared/static/rh935iit6ewrmw0unyul.jpeg\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" }, \"item_status\": \"active\", \"tags\": [ \"important\", \"needs review\" ] }";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            var request = new BoxFileRequest()
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
            Assert.AreEqual(FilesUri + "5000948880/copy", boxRequest.AbsoluteUri.AbsoluteUri);
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
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/CreateFileSharedLink200.json")
                }));

            var sharedLink = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.collaborators,
                VanityName = "my-custom-vanity-name"
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
            Assert.AreEqual("my-custom-vanity-name", f.SharedLink.VanityName);
            Assert.AreEqual(true, f.SharedLink.Permissions.CanEdit);
        }

        [TestMethod]
        public async Task EditSharedLink_NullUnsharedAt_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = "{ \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\", \"description\": \"a picture of tigers\", \"size\": 629644, \"path_collection\": { \"total_count\": 2, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" } ] }, \"created_at\": \"2012-12-12T10:55:30-08:00\", \"modified_at\": \"2012-12-12T11:04:26-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/rh935iit6ewrmw0unyul\", \"download_url\": \"https://www.box.com/shared/static/rh935iit6ewrmw0unyul.jpeg\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" }, \"item_status\": \"active\" }";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            var sharedLink = new BoxSharedLinkRequest()
            {
                UnsharedAt = null
            };

            var fileRequest = new BoxFileRequest()
            {
                Id = "5000948880",
                SharedLink = sharedLink
            };

            /*** Act ***/
            BoxFile f = await _filesManager.UpdateInformationAsync(fileRequest);

            /*** Assert ***/
            Assert.AreEqual("{\"shared_link\":{\"access\":null,\"unshared_at\":null},\"id\":\"5000948880\"}", boxRequest.Payload);
            Assert.AreEqual("5000948880", f.Id);
            Assert.IsNull(f.SharedLink.UnsharedAt);
        }

        [TestMethod]
        public async Task EditSharedLink_UnsetUnsharedAt_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = "{ \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\", \"description\": \"a picture of tigers\", \"size\": 629644, \"path_collection\": { \"total_count\": 2, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" } ] }, \"created_at\": \"2012-12-12T10:55:30-08:00\", \"modified_at\": \"2012-12-12T11:04:26-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/rh935iit6ewrmw0unyul\", \"download_url\": \"https://www.box.com/shared/static/rh935iit6ewrmw0unyul.jpeg\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\" }, \"item_status\": \"active\" }";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            var sharedLink = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open
            };

            var fileRequest = new BoxFileRequest()
            {
                Id = "5000948880",
                SharedLink = sharedLink
            };

            /*** Act ***/
            BoxFile f = await _filesManager.UpdateInformationAsync(fileRequest);

            /*** Assert ***/
            Assert.AreEqual("{\"shared_link\":{\"access\":\"open\"},\"id\":\"5000948880\"}", boxRequest.Payload);
            Assert.AreEqual("5000948880", f.Id);
            Assert.IsNull(f.SharedLink.UnsharedAt);
        }

        [TestMethod]
        public async Task ViewFileComments_ValidResponse_ValidFile()
        {
            /*** Arrange ***/
            var responseString = "{ \"total_count\": 1, \"entries\": [ { \"type\": \"comment\", \"id\": \"191969\", \"is_reply_comment\": false, \"message\": \"These tigers are cool!\", \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"created_at\": \"2012-12-12T11:25:01-08:00\", \"item\": { \"id\": \"5000948880\", \"type\": \"file\" }, \"modified_at\": \"2012-12-12T11:25:01-08:00\" } ] }";
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxComment>>(It.IsAny<IBoxRequest>()))
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
            var responseString = "{ \"type\": \"file\", \"id\": \"5859258256\", \"sequence_id\": \"2\", \"etag\": \"2\", \"sha1\": \"4bd9e98652799fc57cf9423e13629c151152ce6c\", \"name\": \"Screenshot_1_30_13_6_37_PM.png\", \"description\": \"\", \"size\": 163265, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"1\", \"sequence_id\": null, \"etag\": null, \"name\": \"Trash\" } ] }, \"created_at\": \"2013-01-30T18:43:56-08:00\", \"modified_at\": \"2013-01-30T18:44:00-08:00\", \"trashed_at\": \"2013-02-07T10:49:34-08:00\", \"purged_at\": \"2013-03-09T10:49:34-08:00\", \"content_created_at\": \"2013-01-30T18:43:56-08:00\", \"content_modified_at\": \"2013-01-30T18:44:00-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"shared_link\": { \"url\": null, \"download_url\": null, \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"trashed\" }";
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
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
            var responseString = "{ \"type\": \"file\", \"id\": \"5859258256\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"4bd9e98652799fc57cf9423e13629c151152ce6c\", \"name\": \"Screenshot_1_30_13_6_37_PM.png\", \"description\": \"\", \"size\": 163265, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_at\": \"2013-01-30T18:43:56-08:00\", \"modified_at\": \"2013-02-07T10:56:58-08:00\", \"trashed_at\": null, \"purged_at\": null, \"content_created_at\": \"2013-01-30T18:43:56-08:00\", \"content_modified_at\": \"2013-02-07T10:56:58-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"shared_link\": { \"url\": \"https://seanrose.box.com/s/ebgti08mtmhbpb4vlp55\", \"download_url\": \"https://seanrose.box.com/shared/static/ebgti08mtmhbpb4vlp55.png\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 4, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\" }";
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            var fileReq = new BoxFileRequest()
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
            var responseString = "{ \"type\": \"file\", \"id\": \"5859258256\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"4bd9e98652799fc57cf9423e13629c151152ce6c\", \"name\": \"Screenshot_1_30_13_6_37_PM.png\", \"description\": \"\", \"size\": 163265, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_at\": \"2013-01-30T18:43:56-08:00\", \"modified_at\": \"2013-02-07T10:56:58-08:00\", \"trashed_at\": null, \"purged_at\": null, \"content_created_at\": \"2013-01-30T18:43:56-08:00\", \"content_modified_at\": \"2013-02-07T10:56:58-08:00\", \"created_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"shared_link\": { \"url\": \"https://seanrose.box.com/s/ebgti08mtmhbpb4vlp55\", \"download_url\": \"https://seanrose.box.com/shared/static/ebgti08mtmhbpb4vlp55.png\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 4, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\" }";
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));


            /*** Act ***/
            var success = await _filesManager.PurgeTrashedAsync("0");

            /*** Assert ***/
            Assert.AreEqual(true, success);
        }

        [TestMethod]
        public async Task GetLockFile_ValidResponse_Success()
        {
            /*** Arrange ***/
            var responseString = "{ \"type\": \"file\", \"id\": \"7435988481\", \"etag\": \"1\", \"lock\": { \"type\": \"lock\", \"id\": \"14516545\", \"created_by\": { \"type\": \"user\", \"id\": \"13130406\", \"name\": \"I don't know gmail\", \"login\": \"idontknow@gmail.com\" }, \"created_at\": \"2014-05-29T18:03:04-07:00\", \"expires_at\": \"2014-05-30T19:03:04-07:00\", \"is_download_prevented\": true } } ";

            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
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
            Assert.AreEqual(DateTimeOffset.Parse("2014-05-30T19:03:04-07:00"), fileLock.ExpiresAt);
            Assert.AreEqual(DateTimeOffset.Parse("2014-05-29T18:03:04-07:00"), fileLock.CreatedAt);
            Assert.IsNotNull(fileLock.CreatedBy);
            Assert.AreEqual("I don't know gmail", fileLock.CreatedBy.Name);
            Assert.AreEqual("idontknow@gmail.com", fileLock.CreatedBy.Login);

        }

        [TestMethod]
        public async Task UpdateFileLock_ValidResponse_ValidFile()
        {
            var responseString = "{ \"type\": \"file\", \"id\": \"7435988481\", \"etag\": \"1\", \"lock\": { \"type\": \"lock\", \"id\": \"14516545\", \"created_by\": { \"type\": \"user\", \"id\": \"13130406\", \"name\": \"I don't know gmail\", \"login\": \"idontknow@gmail.com\" }, \"created_at\": \"2014-05-29T18:03:04-07:00\", \"expires_at\": \"2014-05-30T19:03:04-07:00\", \"is_download_prevented\": false } } ";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                 .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var request = new BoxFileLockRequest
            {
                Lock = new BoxFileLock
                {
                    IsDownloadPrevented = false
                }
            };

            BoxFileLock fileLock = await _filesManager.UpdateLockAsync(request, "7435988481");

            /*** Assert ***/
            Assert.IsNotNull(fileLock);
            Assert.AreEqual(false, fileLock.IsDownloadPrevented);
            Assert.AreEqual(DateTimeOffset.Parse("2014-05-30T19:03:04-07:00"), fileLock.ExpiresAt);
            Assert.AreEqual(DateTimeOffset.Parse("2014-05-29T18:03:04-07:00"), fileLock.CreatedAt);
            Assert.IsNotNull(fileLock.CreatedBy);
            Assert.AreEqual("I don't know gmail", fileLock.CreatedBy.Name);
            Assert.AreEqual("idontknow@gmail.com", fileLock.CreatedBy.Login);
        }

        [TestMethod]
        public async Task FileUnLock_ValidResponse()
        {
            var responseString = "{ \"type\": \"file\", \"id\": \"7435988481\", \"etag\": \"1\" } ";
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            var unlocked = await _filesManager.UnLock("0");

            /*** Assert ***/
            Assert.IsTrue(unlocked);
        }

        [TestMethod]
        public async Task GetThumbnail_ValidResponse_ValidStream()
        {
            using (var thumb = new FileStream(string.Format(GetSaveFolderPath(), "thumb.png"), FileMode.OpenOrCreate))
            {
                /*** Arrange ***/

                Handler.Setup(h => h.ExecuteAsync<Stream>(It.IsAny<IBoxRequest>()))

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

        [TestMethod]
        public async Task GetThumbnail_ValidResponse_EndpointJpg()
        {
            using (var thumb = new FileStream(string.Format(GetSaveFolderPath(), "thumb.png"), FileMode.OpenOrCreate))
            {
                /*** Arrange ***/
                var endpoint = "";
                Handler.Setup(h => h.ExecuteAsync<Stream>(It.IsAny<IBoxRequest>()))
                    .Callback<IBoxRequest>(r => endpoint = r.AbsoluteUri.Segments.LastOrDefault())
                    .Returns(Task.FromResult<IBoxResponse<Stream>>(new BoxResponse<Stream>()
                    {
                        Status = ResponseStatus.Success,
                        ResponseObject = thumb
                    }));

                /*** Act ***/
                Stream result = await _filesManager.GetThumbnailAsync("34122832467", extension: "jpg");

                /*** Assert ***/
                Assert.IsNotNull(result, "Stream is Null");
                Assert.AreEqual("thumbnail.jpg", endpoint);

            }
        }

        private string GetSaveFolderPath()
        {
            var pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(pathUser, "Downloads") + "\\{0}";
        }

        [TestMethod]
        public async Task PreflightCheck_ValidResponse_ValidStatus()
        {
            /*** Arrange ***/
            var responseString = "";
            Handler.Setup(h => h.ExecuteAsync<BoxPreflightCheck>(It.IsAny<IBoxRequest>()))
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
            var responseString = "";
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            var result = await _filesManager.DeleteAsync("34122832467");

            /*** Assert ***/

            Assert.AreEqual(true, result);


        }

        [TestMethod]
        public async Task DeleteFile_ErrorResponse_Exception()
        {

            /*** Arrange ***/
            var headers = new HttpResponseMessage().Headers;
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                .Returns(Task<IBoxResponse<BoxFile>>.Factory.StartNew(() => new BoxResponse<BoxFile>()
                {
                    StatusCode = System.Net.HttpStatusCode.Forbidden,
                    Status = ResponseStatus.Forbidden,
                    Headers = headers,
                    ContentString = "{\"type\": \"error\", \"status\": 403, \"code\": \"forbidden_by_policy\", \"message\": \"Access denied by Shield policy\", \"request_id\": \"5hr712h2ip6deox0\"}"
                }));

            /*** Act ***/
            try
            {
                var result = await _filesManager.DeleteAsync("34122832467");

                Assert.Fail("Expected delete file throws when delete without permissions");
            }
            catch (BoxAPIException ex)
            {
                /*** Assert ***/
                Assert.AreEqual(System.Net.HttpStatusCode.Forbidden, ex.StatusCode);
                Assert.AreEqual("forbidden_by_policy", ex.ErrorCode);
                Assert.AreEqual("Access denied by Shield policy", ex.ErrorDescription);
                Assert.AreEqual("5hr712h2ip6deox0", ex.Error.RequestId);
                Assert.AreEqual("403", ex.Error.Status);
                Assert.AreEqual("forbidden_by_policy", ex.Error.Code);
                Assert.AreEqual("Access denied by Shield policy", ex.Error.Message);
            }
        }

        [TestMethod]
        public async Task Download_LargeOffset_ValidStream()
        {

            using (var exampleFile = new FileStream(string.Format(GetSaveFolderPath(), "example.png"), FileMode.OpenOrCreate))
            {
                /*** Arrange ***/
                var location = new Uri("http://dl.boxcloud.com");
                var headers = new HttpResponseMessage().Headers;
                headers.Location = location;
                Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))

                    .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                    {
                        Status = ResponseStatus.Success,
                        Headers = headers
                    }));
                IBoxRequest boxRequest = null;
                Handler.Setup(h => h.ExecuteAsync<Stream>(It.IsAny<IBoxRequest>()))

                   .Returns(Task.FromResult<IBoxResponse<Stream>>(new BoxResponse<Stream>()
                   {
                       Status = ResponseStatus.Success,
                       ResponseObject = exampleFile

                   }))
                   .Callback<IBoxRequest>(r => boxRequest = r); ;

                /*** Act ***/
                Stream result = await _filesManager.DownloadAsync("34122832467", startOffsetInBytes: 5_000_000_000, endOffsetInBytes: 6_000_000_000);

                /*** Assert ***/

                Assert.IsNotNull(result, "Stream is Null");

            }
        }

        [TestMethod]
        public async Task GetEmbedLink_ValidResponse_ValidEmbedLink()
        {
            /*** Arrange ***/
            var responseString = "{\"type\": \"file\",\"id\": \"34122832467\", \"etag\": \"1\", \"expiring_embed_link\": { \"url\": \"https://app.box.com/preview/expiring_embed/gvoct6FE!Qz2rDeyxCiHsYpvlnR7JJ0SCfFM2M4YiX9cIwrSo4LOYQgxyP3rzoYuMmXg96mTAidqjPuRH7HFXMWgXEEm5LTi1EDlfBocS-iRfHpc5ZeYrAZpA5B8C0Obzkr4bUoF6wGq8BZ1noN_txyZUU1nLDNuL_u0rsImWhPAZlvgt7662F9lZSQ8nw6zKaRWGyqmj06PnxewCx0EQD3padm6VYkfHE2N20gb5rw1D0a7aaRJZzEijb2ICLItqfMlZ5vBe7zGdEn3agDzZP7JlID3FYdPTITsegB10gKLgSp_AJJ9QAfDv8mzi0bGv1ZmAU1FoVLpGC0XI0UKy3N795rZBtjLlTNcuxapbHkUCoKcgdfmHEn5NRQ3tmw7hiBfnX8o-Au34ttW9ntPspdAQHL6xPzQC4OutWZDozsA5P9sGlI-sC3VC2-WXsbXSedemubVd5vWzpVZtKRlb0gpuXsnDPXnMxSH7_jT4KSLhC8b5kEMPNo33FjEJl5pwS_o_6K0awUdRpEQIxM9CC3pBUZK5ooAc5X5zxo_2FBr1xq1p_kSbt4TVnNeohiLIu38TQysSb7CMR7JRhDDZhMMwAUc0wdSszELgL053lJlPeoiaLA49rAGP_B3BVuwFAFEl696w7UMx5NKu1mA0IOn9pDebzbhTl5HuUvBAHROc1Ocjb28Svyotik1IkPIw_1R33ZyAMvEFyzIygqBj8WedQeSK38iXvF2UXvkAf9kevOdnpwsKYiJtcxeJhFm7LUVKDTufuzuGRw-T7cPtbg..\" } }";
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
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
            var responseString = "{\"total_count\": 1, \"entries\": [{\"type\": \"task\", \"id\": \"1786931\",\"item\": {\"type\": \"file\",\"id\": \"7026335894\", \"sequence_id\": \"6\", \"etag\": \"6\", \"sha1\": \"81cc829fb8366fcfc108aa6c5a9bde01a6a10c16\",\"name\": \"API - Persist On-Behalf-Of information.docx\" }, \"due_at\": null }   ] }";
            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxTask>>(It.IsAny<IBoxRequest>()))
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
            var responseString = @"{
                                          ""watermark"": {
                                            ""created_at"": ""2016-10-31T15:33:33-07:00"",
                                            ""modified_at"": ""2016-10-31T15:33:33-07:00""
                                          }
                                       }";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxWatermarkResponse>(It.IsAny<IBoxRequest>()))
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
            Assert.AreEqual(FilesUri + "5010739069/watermark", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(DateTimeOffset.Parse("2016-10-31T15:33:33-07:00"), result.CreatedAt.Value);
            Assert.AreEqual(DateTimeOffset.Parse("2016-10-31T15:33:33-07:00"), result.ModifiedAt.Value);
        }

        [TestMethod]
        public async Task ApplyWatermarkToFile_ValidResponse_ValidWatermark()
        {
            /*** Arrange ***/
            var responseString = @"{
                                          ""watermark"": {
                                            ""created_at"": ""2016-10-31T15:33:33-07:00"",
                                            ""modified_at"": ""2016-10-31T15:33:33-07:00""
                                          }
                                       }";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxWatermarkResponse>(It.IsAny<IBoxRequest>()))
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
            Assert.AreEqual(FilesUri + "5010739069/watermark", boxRequest.AbsoluteUri.AbsoluteUri);
            BoxApplyWatermarkRequest payload = JsonConvert.DeserializeObject<BoxApplyWatermarkRequest>(boxRequest.Payload);
            Assert.AreEqual("default", payload.Watermark.Imprint);

            //Response check
            Assert.AreEqual(DateTimeOffset.Parse("2016-10-31T15:33:33-07:00"), result.CreatedAt.Value);
            Assert.AreEqual(DateTimeOffset.Parse("2016-10-31T15:33:33-07:00"), result.ModifiedAt.Value);
        }

        [TestMethod]
        public async Task RemoveWatermarkFromFile_ValidResponse_RemovedWatermark()
        {
            /*** Arrange ***/
            var responseString = "";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxWatermarkResponse>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxWatermarkResponse>>(new BoxResponse<BoxWatermarkResponse>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var result = await _filesManager.RemoveWatermarkAsync("5010739069");

            /*** Assert ***/
            //Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(FilesUri + "5010739069/watermark", boxRequest.AbsoluteUri.AbsoluteUri);

            //Response check
            Assert.AreEqual(true, result);

        }

        [TestMethod]
        public async Task DeleteOldVersion_ValidReponse()
        {
            /*** Arrange ***/
            var responseString = "";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
           .Returns(() => Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
           {
               Status = ResponseStatus.Success,
               ContentString = responseString
           }))
           .Callback<IBoxRequest>(r => boxRequest = r);

            var result = await _filesManager.DeleteOldVersionAsync("fileid", "versionid");

            /*** Assert ***/

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(FilesUri + "fileid/versions/versionid", boxRequest.AbsoluteUri.AbsoluteUri);

            // Response check
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task PromoteVersion_ValidResponse()
        {
            /*** Arrange ***/
            var responseString = "{\"type\":\"file_version\",\"id\":\"871399\",\"sha1\":\"12039d6dd9a7e6eefc78846802e\",\"name\":\"Stark Family Lineage.doc\",\"size\":11,\"uploader_display_name\":\"Arya Stark\",\"created_at\":\"2013-11-20T13:20:50-08:00\",\"modified_at\":\"2013-11-20T13:26:48-08:00\",\"modified_by\":{\"type\":\"user\",\"id\":\"13711334\",\"name\":\"Eddard Stark\",\"login\":\"ned@winterfell.com\"}}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxFileVersion>(It.IsAny<IBoxRequest>()))
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
            Assert.AreEqual(FilesUri + "fileid/versions/current", boxRequest.AbsoluteUri.AbsoluteUri);
            BoxPromoteVersionRequest payload = JsonConvert.DeserializeObject<BoxPromoteVersionRequest>(boxRequest.Payload);
            Assert.AreEqual("871399", payload.Id);
            Assert.AreEqual("file_version", payload.Type);

            // Response check
            Assert.AreEqual("871399", result.Id);
            Assert.AreEqual("file_version", result.Type);
            Assert.AreEqual("Stark Family Lineage.doc", result.Name);
            Assert.AreEqual("Arya Stark", result.UploaderDisplayName);
            Assert.AreEqual(DateTimeOffset.Parse("2013-11-20T13:20:50-08:00"), result.CreatedAt);
            Assert.AreEqual(DateTimeOffset.Parse("2013-11-20T13:26:48-08:00"), result.ModifiedAt);
        }

        [TestMethod]
        public async Task DownloadZip_ValidResponse()
        {
            using (var exampleFile = new FileStream(string.Format(AppDomain.CurrentDomain.BaseDirectory + "/TestData/smalltest.pdf"), FileMode.OpenOrCreate))
            {
                /*** Arrange ***/
                var responseStringCreateZip = "{\"download_url\": \"https://api.box.com/zip_downloads/124hfiowk3fa8kmrwh/content\",\"status_url\": \"https://api.box.com/zip_downloads/124hfiowk3fa8kmrwh/status\",\"expires_at\": \"2018-04-25T11:00:18-07:00\", \"name_conflicts\":[[{\"id\":\"100\",\"type\":\"file\",\"original_name\":\"salary.pdf\",\"download_name\":\"aqc823.pdf\"},{\"id\":\"200\",\"type\": \"file\",\"original_name\":\"salary.pdf\",\"download_name\": \"aci23s.pdf\"}],[{\"id\":\"1000\",\"type\": \"folder\",\"original_name\":\"employees\",\"download_name\":\"3d366a_employees\"},{\"id\":\"2000\",\"type\": \"folder\",\"original_name\":\"employees\",\"download_name\": \"3aa6a7_employees\"}]]}";
                var responseStringDownloadStatus = "{\"total_file_count\": 20, \"downloaded_file_count\": 10, \"skipped_file_count\": 10, \"skipped_folder_count\": 10, \"state\": \"succeeded\"}";
                IBoxRequest boxRequest = null;
                IBoxRequest boxRequest2 = null;
                IBoxRequest boxRequest3 = null;
                Handler.Setup(h => h.ExecuteAsync<BoxZip>(It.IsAny<IBoxRequest>()))
                     .Returns(Task.FromResult<IBoxResponse<BoxZip>>(new BoxResponse<BoxZip>()
                     {
                         Status = ResponseStatus.Success,
                         ContentString = responseStringCreateZip
                     }))
                     .Callback<IBoxRequest>(r => boxRequest = r);

                Handler.Setup(h => h.ExecuteAsync<Stream>(It.IsAny<IBoxRequest>()))
                   .Returns(Task.FromResult<IBoxResponse<Stream>>(new BoxResponse<Stream>()
                   {
                       Status = ResponseStatus.Success,
                       ResponseObject = exampleFile

                   }))
                   .Callback<IBoxRequest>(r => boxRequest2 = r);

                Handler.Setup(h => h.ExecuteAsync<BoxZipDownloadStatus>(It.IsAny<IBoxRequest>()))
                     .Returns(Task.FromResult<IBoxResponse<BoxZipDownloadStatus>>(new BoxResponse<BoxZipDownloadStatus>()
                     {
                         Status = ResponseStatus.Success,
                         ContentString = responseStringDownloadStatus
                     }))
                     .Callback<IBoxRequest>(r => boxRequest3 = r);



                /*** Act ***/
                var request = new BoxZipRequest
                {
                    Name = "test",
                    Items = new List<BoxZipRequestItem>()
                };

                var file = new BoxZipRequestItem()
                {
                    Id = "466239504569",
                    Type = BoxZipItemType.file
                };
                var folder = new BoxZipRequestItem()
                {
                    Id = "466239504580",
                    Type = BoxZipItemType.folder
                };
                request.Items.Add(file);
                request.Items.Add(folder);
                Stream fs = new MemoryStream(100);

                BoxZipDownloadStatus status = await _filesManager.DownloadZip(request, fs);
                /*** Assert ***/

                // Request check
                Assert.IsNotNull(boxRequest);
                Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
                var payload = JObject.Parse(boxRequest.Payload);
                Assert.AreEqual("test", payload["name"]);
                var items = JArray.Parse(payload["items"].ToString());
                Assert.AreEqual("466239504569", items[0]["id"]);
                Assert.AreEqual("file", items[0]["type"]);

                // Reponse Check
                Assert.AreEqual(status.TotalFileCount, 20);
                Assert.AreEqual(status.State, BoxZipDownloadState.succeeded);
                Assert.AreEqual(status.NameConflicts[0].items[0].OriginalName, "salary.pdf");
                Assert.AreEqual(status.NameConflicts[0].items[1].OriginalName, "salary.pdf");
                Assert.AreEqual(status.NameConflicts[1].items[0].OriginalName, "employees");
                Assert.AreEqual(status.NameConflicts[1].items[1].OriginalName, "employees");
                Assert.AreNotEqual(fs.Length, 0);
            }
        }


        [TestMethod]
        [ExpectedException(typeof(BoxCodingException))]
        public async Task GetRepresentationContentAsync_ShouldThrowException_IfTooManyRetriesAndHandleRetryTrue()
        {
            Handler.SetupSequence(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                 .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                 {
                     Status = ResponseStatus.Success,
                     StatusCode = System.Net.HttpStatusCode.Accepted,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/GetRepresentationContentPending200.json")
                 }))
                 .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                 {
                     Status = ResponseStatus.Success,
                     StatusCode = System.Net.HttpStatusCode.Accepted,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/GetRepresentationContentPending200.json")
                 }))
                 .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                 {
                     Status = ResponseStatus.Success,
                     StatusCode = System.Net.HttpStatusCode.Accepted,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/GetRepresentationContentPending200.json")
                 }))
                 .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                 {
                     Status = ResponseStatus.Success,
                     StatusCode = System.Net.HttpStatusCode.Accepted,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/GetRepresentationContentPending200.json")
                 }))
                 .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                 {
                     Status = ResponseStatus.Success,
                     StatusCode = System.Net.HttpStatusCode.Accepted,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/GetRepresentationContentPending200.json")
                 }))
                 .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                 {
                     Status = ResponseStatus.Success,
                     StatusCode = System.Net.HttpStatusCode.Accepted,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/GetRepresentationContentPending200.json")
                 }));

            var repRequest = new BoxRepresentationRequest
            {
                FileId = "11111",
                XRepHints = $"[jpg?dimensions=320x320]",
                HandleRetry = true
            };

            var result = await _filesManager.GetRepresentationContentAsync(repRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(BoxCodingException))]
        public async Task GetRepresentationContentAsync_ShouldThrowException_IfTooManyRetries()
        {
            Handler.Setup(h => h.ExecuteAsync<BoxFile>(It.IsAny<IBoxRequest>()))
                 .Returns(Task.FromResult<IBoxResponse<BoxFile>>(new BoxResponse<BoxFile>()
                 {
                     Status = ResponseStatus.Success,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/GetRepresentationContentPending200.json")
                 }));

            Handler.SetupSequence(h => h.ExecuteAsync<BoxRepresentation>(It.IsAny<IBoxRequest>()))
                 .Returns(Task.FromResult<IBoxResponse<BoxRepresentation>>(new BoxResponse<BoxRepresentation>()
                 {
                     Status = ResponseStatus.Success,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/PollRepresentationPending200.json")
                 }))
                 .Returns(Task.FromResult<IBoxResponse<BoxRepresentation>>(new BoxResponse<BoxRepresentation>()
                 {
                     Status = ResponseStatus.Success,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/PollRepresentationPending200.json")
                 }))
                 .Returns(Task.FromResult<IBoxResponse<BoxRepresentation>>(new BoxResponse<BoxRepresentation>()
                 {
                     Status = ResponseStatus.Success,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/PollRepresentationPending200.json")
                 }))
                 .Returns(Task.FromResult<IBoxResponse<BoxRepresentation>>(new BoxResponse<BoxRepresentation>()
                 {
                     Status = ResponseStatus.Success,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/PollRepresentationPending200.json")
                 }))
                 .Returns(Task.FromResult<IBoxResponse<BoxRepresentation>>(new BoxResponse<BoxRepresentation>()
                 {
                     Status = ResponseStatus.Success,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/PollRepresentationPending200.json")
                 }))
                 .Returns(Task.FromResult<IBoxResponse<BoxRepresentation>>(new BoxResponse<BoxRepresentation>()
                 {
                     Status = ResponseStatus.Success,
                     ContentString = LoadFixtureFromJson("Fixtures/BoxFiles/PollRepresentationPending200.json")
                 }));

            var repRequest = new BoxRepresentationRequest
            {
                FileId = "11111",
                XRepHints = $"[jpg?dimensions=320x320]",
            };

            var result = await _filesManager.GetRepresentationContentAsync(repRequest);
        }
    }
}
