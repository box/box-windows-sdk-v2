using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Box.V2.Services;
using Box.V2.Contracts;
using Box.V2.Auth;
using Box.V2.Models;
using System.Threading.Tasks;
using Box.V2.Managers;
using System.Linq;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxFoldersManagerTest : BoxResourceManagerTest
    {
        protected BoxFoldersManager _foldersManager;

        public BoxFoldersManagerTest()
        {
            _foldersManager = new BoxFoldersManager(_config.Object, _service, _converter, _authRepository);
        }

        [TestMethod]
        public async Task GetFolder_ValidResponse_ValidFolder()
        {
            _handler.Setup(h => h.ExecuteAsync<Folder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<Folder>>(new BoxResponse<Folder>()
                    {
                        Status = ResponseStatus.Success,
                        //ContentString = "{ \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\", \"created_at\": \"2012-12-12T10:53:43-08:00\", \"modified_at\": \"2012-12-12T11:15:04-08:00\", \"description\": \"Some pictures I took\", \"size\": 629644, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/vspke7y05sb214wjokpk\", \"download_url\": \"https://www.box.com/shared/static/vspke7y05sb214wjokpk\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"folder_upload_email\": { \"access\": \"open\", \"email\": \"upload.Picture.k13sdz1@u.box.com\" }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\", \"item_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\" } ], \"offset\": 0, \"limit\": 100 } }"
                        ContentString = "{ \"type\":\"folder\", \"id\":\"0\", \"sequence_id\":null, \"etag\":null, \"name\":\"All Files\", \"created_at\":null, \"modified_at\":null, \"description\":\"\", \"size\":61591428468, \"path_collection\":{ \"total_count\":0, \"entries\":[ ] }, \"created_by\":{ \"type\":\"user\", \"id\":\"189912110\", \"name\":\"Brian\", \"login\":\"brianytang@gmail.com\" }, \"modified_by\":{ \"type\":\"user\", \"id\":\"189912110\", \"name\":\"Brian\", \"login\":\"brianytang@gmail.com\" }, \"trashed_at\":null, \"purged_at\":null, \"content_created_at\":null, \"content_modified_at\":null, \"owned_by\":{ \"type\":\"user\", \"id\":\"189912110\", \"name\":\"Brian\", \"login\":\"brianytang@gmail.com\" }, \"shared_link\":null, \"folder_upload_email\":null, \"parent\":null, \"item_status\":\"active\", \"item_collection\":{ \"total_count\":10, \"entries\":[ { \"type\":\"folder\", \"id\":\"766352168\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"Books\" }, { \"type\":\"folder\", \"id\":\"869883498\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"bytLabs\" }, { \"type\":\"folder\", \"id\":\"767221958\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"Games\" }, { \"type\":\"folder\", \"id\":\"766174084\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"Mixes\" }, { \"type\":\"folder\", \"id\":\"57181304\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"Muzik\" }, { \"type\":\"folder\", \"id\":\"857305570\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"My\" }, { \"type\":\"folder\", \"id\":\"627316229\", \"sequence_id\":\"1\", \"etag\":\"1\", \"name\":\"My Music Folder\" }, { \"type\":\"folder\", \"id\":\"860155462\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"sample\" }, { \"type\":\"folder\", \"id\":\"775829294\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"Software\" }, { \"type\":\"folder\", \"id\":\"811565831\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"Test\" } ], \"offset\":0, \"limit\":10, \"order\":[ { \"by\":\"type\", \"direction\":\"ASC\" }, { \"by\":\"name\", \"direction\":\"ASC\" } ] } }"
                    }));

            Folder f = await _foldersManager.GetItemsAsync("0", 10);
        }

        [TestMethod]
        public async Task CreateFolder_ValidResponse_ValidFolder()
        {
            _handler.Setup(h => h.ExecuteAsync<Folder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<Folder>>(new BoxResponse<Folder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\", \"created_at\": \"2012-12-12T10:53:43-08:00\", \"modified_at\": \"2012-12-12T11:15:04-08:00\", \"description\": \"Some pictures I took\", \"size\": 629644, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/vspke7y05sb214wjokpk\", \"download_url\": \"https://www.box.com/shared/static/vspke7y05sb214wjokpk\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"folder_upload_email\": { \"access\": \"open\", \"email\": \"upload.Picture.k13sdz1@u.box.com\" }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\", \"item_collection\": { \"total_count\": 0, \"entries\": [], \"offset\": 0, \"limit\": 100 } }"
                }));

            var folderReq = new BoxFolderRequest()
            {
                Name = "test",
                Parent = new BoxRequestEntity() { Id = "0" }
            };

            Folder f = await _foldersManager.CreateAsync(folderReq);
        }

        [TestMethod]
        public async Task GetFolderInformation_ValidResponse_ValidFolder()
        {
            _handler.Setup(h => h.ExecuteAsync<Folder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<Folder>>(new BoxResponse<Folder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\", \"created_at\": \"2012-12-12T10:53:43-08:00\", \"modified_at\": \"2012-12-12T11:15:04-08:00\", \"description\": \"Some pictures I took\", \"size\": 629644, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/vspke7y05sb214wjokpk\", \"download_url\": \"https://www.box.com/shared/static/vspke7y05sb214wjokpk\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"folder_upload_email\": { \"access\": \"open\", \"email\": \"upload.Picture.k13sdz1@u.box.com\" }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\", \"item_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\" } ], \"offset\": 0, \"limit\": 100 } }"
                }));

            Folder f = await _foldersManager.GetInformationAsync("fakeId");
        }

        [TestMethod]
        public async Task CopyFolder_ValidResponse_ValidFolder()
        {
            /*** Arrange ***/
            _handler.Setup(h => h.ExecuteAsync<Folder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<Folder>>(new BoxResponse<Folder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\", \"created_at\": \"2012-12-12T10:53:43-08:00\", \"modified_at\": \"2012-12-12T11:15:04-08:00\", \"description\": \"Some pictures I took\", \"size\": 629644, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/vspke7y05sb214wjokpk\", \"download_url\": \"https://www.box.com/shared/static/vspke7y05sb214wjokpk\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"folder_upload_email\": { \"access\": \"open\", \"email\": \"upload.Picture.k13sdz1@u.box.com\" }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\", \"item_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\" } ], \"offset\": 0, \"limit\": 100 } }"
                }));

            /*** Act ***/
            BoxFolderRequest folderReq = new BoxFolderRequest()
            {
                Id = "fakeId",
                Parent = new BoxRequestEntity() { Id = "fakeId" }
            };

            Folder f = await _foldersManager.CopyAsync(folderReq);

            /*** Assert ***/
            Assert.AreEqual("folder", f.Type);
            Assert.AreEqual("11446498", f.Id);
            Assert.AreEqual("1", f.SequenceId);
            Assert.AreEqual("1", f.ETag);
            Assert.AreEqual("Pictures", f.Name);
        }

        [TestMethod]
        public async Task UpdateFolderInformation_ValidResponse_ValidFolder()
        {
            /*** Arrange ***/
            _handler.Setup(h => h.ExecuteAsync<Folder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<Folder>>(new BoxResponse<Folder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"New Folder Name!\", \"created_at\": \"2012-12-12T10:53:43-08:00\", \"modified_at\": \"2012-12-12T11:15:04-08:00\", \"description\": \"Some pictures I took\", \"size\": 629644, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/vspke7y05sb214wjokpk\", \"download_url\": \"https://www.box.com/shared/static/vspke7y05sb214wjokpk\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"folder_upload_email\": { \"access\": \"open\", \"email\": \"upload.Picture.k13sdz1@u.box.com\" }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\", \"item_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\" } ], \"offset\": 0, \"limit\": 100 } }"
                }));

            /*** Act ***/
            BoxFolderRequest folderReq = new BoxFolderRequest()
            {
                Id = "fakeId",
                Name = "New Folder Name!"
            };

            Folder f = await _foldersManager.UpdateInformationAsync(folderReq);

            /*** Assert ***/
            Assert.AreEqual("folder", f.Type);
            Assert.AreEqual("11446498", f.Id);
            Assert.AreEqual("1", f.SequenceId);
            Assert.AreEqual("1", f.ETag);
            Assert.AreEqual("New Folder Name!", f.Name);

        }

        [TestMethod]
        public async Task CreateFolderSharedLink_ValidResponse_ValidFolder()
        {
            /*** Arrange ***/
            _handler.Setup(h => h.ExecuteAsync<Folder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<Folder>>(new BoxResponse<Folder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\", \"created_at\": \"2012-12-12T10:53:43-08:00\", \"modified_at\": \"2012-12-12T11:15:04-08:00\", \"description\": \"Some pictures I took\", \"size\": 629644, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/vspke7y05sb214wjokpk\", \"download_url\": \"https://www.box.com/shared/static/vspke7y05sb214wjokpk\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"folder_upload_email\": { \"access\": \"open\", \"email\": \"upload.Picture.k13sdz1@u.box.com\" }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\", \"item_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\" } ], \"offset\": 0, \"limit\": 100 } }"
                }));

            /*** Act ***/
            BoxSharedLinkRequest sharedLink = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.collaborators
            };

            Folder f = await _foldersManager.CreateSharedLinkAsync("0", sharedLink);

            /*** Assert ***/
            Assert.AreEqual("folder", f.Type);
            Assert.AreEqual("11446498", f.Id);
            Assert.AreEqual("1", f.SequenceId);
            Assert.AreEqual("1", f.ETag);
            Assert.AreEqual("Pictures", f.Name);
        }

        [TestMethod]
        public async Task GetFolderCollaborators_ValidResponse_ValidCollaborators()
        {
            /*** Arrange ***/
            _handler.Setup(h => h.ExecuteAsync<Collection<Collaboration>>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<Collection<Collaboration>>>(new BoxResponse<Collection<Collaboration>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"total_count\": 1, \"entries\": [ { \"type\": \"collaboration\", \"id\": \"14176246\", \"created_by\": { \"type\": \"user\", \"id\": \"4276790\", \"name\": \"David Lee\", \"login\": \"david@box.com\" }, \"created_at\": \"2011-11-29T12:56:35-08:00\", \"modified_at\": \"2012-09-11T15:12:32-07:00\", \"expires_at\": null, \"status\": \"accepted\", \"accessible_by\": { \"type\": \"user\", \"id\": \"755492\", \"name\": \"Simon Tan\", \"login\": \"simon@box.net\" }, \"role\": \"editor\", \"acknowledged_at\": \"2011-11-29T12:59:40-08:00\", \"item\": null } ] }"
                }));

            /*** Act ***/
            Collection<Collaboration> c = await _foldersManager.GetCollaborationsAsync("fakeId");
            Collaboration collab = c.Entries.FirstOrDefault();

            /*** Assert ***/
            Assert.AreEqual(1, c.TotalCount);
            Assert.AreEqual("collaboration", collab.Type);
            Assert.AreEqual("14176246", collab.Id);
            Assert.AreEqual("David Lee", collab.CreatedBy.Name);
            Assert.AreEqual("david@box.com", collab.CreatedBy.Login);
            Assert.AreEqual("Simon Tan", collab.AccessibleBy.Name);
            Assert.AreEqual("simon@box.net", collab.AccessibleBy.Login);
        }

        [TestMethod]
        public async Task GetTrashedItems_ValidResponse_ValidFiles()
        {
            /*** Arrange ***/
            _handler.Setup(h => h.ExecuteAsync<Collection<Item>>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<Collection<Item>>>(new BoxResponse<Collection<Item>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"total_count\": 49542, \"entries\": [ { \"type\": \"file\", \"id\": \"2701979016\", \"sequence_id\": \"1\", \"etag\": \"1\", \"sha1\": \"9d976863fc849f6061ecf9736710bd9c2bce488c\", \"name\": \"file Tue Jul 24 145436 2012KWPX5S.csv\" }, { \"type\": \"file\", \"id\": \"2698211586\", \"sequence_id\": \"1\", \"etag\": \"1\", \"sha1\": \"09b0e2e9760caf7448c702db34ea001f356f1197\", \"name\": \"file Tue Jul 24 010055 20129Z6GS3.csv\" } ], \"offset\": 0, \"limit\": 2 }"
                }));

            /*** Act ***/
            Collection<Item> i = await _foldersManager.GetTrashItemsAsync("fakeId", 10);
            Item i1 = i.Entries.FirstOrDefault();
            Item i2 = i.Entries.Skip(1).FirstOrDefault();

            /*** Assert ***/
            Assert.AreEqual(49542, i.TotalCount);
            Assert.AreEqual("file", i1.Type);
            Assert.AreEqual("2701979016", i1.Id);
            Assert.AreEqual("1", i1.SequenceId);
            Assert.AreEqual("file Tue Jul 24 145436 2012KWPX5S.csv", i1.Name);
            Assert.AreEqual("1", i1.ETag);
            Assert.AreEqual("file", i2.Type);
            Assert.AreEqual("2698211586", i2.Id);
            Assert.AreEqual("1", i2.SequenceId);
            Assert.AreEqual("1", i1.ETag);
            Assert.AreEqual("file Tue Jul 24 010055 20129Z6GS3.csv", i2.Name);

        }

        [TestMethod]
        public async Task RestoreTrashedFolder_ValidResponse_ValidFolder()
        {
            /*** Arrange ***/
            _handler.Setup(h => h.ExecuteAsync<Folder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<Folder>>(new BoxResponse<Folder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"type\": \"folder\", \"id\": \"588970022\", \"sequence_id\": \"2\", \"etag\": \"2\", \"name\": \"heloo world\", \"created_at\": \"2013-01-15T16:15:27-08:00\", \"modified_at\": \"2013-02-07T13:26:00-08:00\", \"description\": \"\", \"size\": 0, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"trashed_at\": null, \"purged_at\": null, \"content_created_at\": \"2013-01-15T16:15:27-08:00\", \"content_modified_at\": \"2013-02-07T13:26:00-08:00\", \"owned_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"shared_link\": null, \"folder_upload_email\": null, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\" }"
                }));

            /*** Act ***/
            BoxFolderRequest folderReq = new BoxFolderRequest()
            {
                Id = "fakeId",
                Parent = new BoxRequestEntity() { Id = "fakeId" }
            };

            Folder f = await _foldersManager.RestoreTrashedFolderAsync(folderReq);

            /*** Assert ***/

            Assert.AreEqual("folder", f.Type);
            Assert.AreEqual("588970022", f.Id);
            Assert.AreEqual("2", f.SequenceId);
            Assert.AreEqual("2", f.ETag);
            Assert.AreEqual("heloo world", f.Name);
        }

        [TestMethod]
        public async Task GetTrashedFolder_ValidResponse_ValidFolder()
        {
            /*** Arrange ***/
            _handler.Setup(h => h.ExecuteAsync<Folder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<Folder>>(new BoxResponse<Folder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"type\": \"folder\", \"id\": \"588970022\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"heloo world\", \"created_at\": \"2013-01-15T16:15:27-08:00\", \"modified_at\": \"2013-01-17T13:48:23-08:00\", \"description\": \"\", \"size\": 0, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"1\", \"sequence_id\": null, \"etag\": null, \"name\": \"Trash\" } ] }, \"created_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"trashed_at\": \"2013-02-07T12:53:32-08:00\", \"purged_at\": \"2013-03-09T12:53:32-08:00\", \"content_created_at\": \"2013-01-15T16:15:27-08:00\", \"content_modified_at\": \"2013-01-17T13:48:23-08:00\", \"owned_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"shared_link\": null, \"folder_upload_email\": null, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"trashed\" }"
                }));

            /***Act ***/
            Folder f = await _foldersManager.GetTrashedFolderAsync("fakeId");

            /*** Assert ***/
            Assert.AreEqual("folder", f.Type);
            Assert.AreEqual("588970022", f.Id);
            Assert.AreEqual("1", f.SequenceId);
            Assert.AreEqual("1", f.ETag);
            Assert.AreEqual("heloo world", f.Name);
        }
    }
}
