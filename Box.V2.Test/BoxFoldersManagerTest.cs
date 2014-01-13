﻿using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task GetFolderItems_ValidResponse_ValidFolder()
        {
            _handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxItem>>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxCollection<BoxItem>>>(new BoxResponse<BoxCollection<BoxItem>>()
                    {
                        Status = ResponseStatus.Success,
                        ContentString = "{\"total_count\":24,\"entries\":[{\"type\":\"folder\",\"id\":\"192429928\",\"sequence_id\":\"1\",\"etag\":\"1\",\"name\":\"Stephen Curry Three Pointers\"},{\"type\":\"file\",\"id\":\"818853862\",\"sequence_id\":\"0\",\"etag\":\"0\",\"name\":\"Warriors.jpg\"}],\"offset\":0,\"limit\":2,\"order\":[{\"by\":\"type\",\"direction\":\"ASC\"},{\"by\":\"name\",\"direction\":\"ASC\"}]}"
                    }));

            BoxCollection<BoxItem> items = await _foldersManager.GetFolderItemsAsync("0", 2);

            Assert.AreEqual(items.TotalCount, 24);
            Assert.AreEqual(items.Entries.Count, 2);
            Assert.AreEqual(items.Entries[0].Type, "folder");
            Assert.AreEqual(items.Entries[0].Id, "192429928");
            Assert.AreEqual(items.Entries[0].SequenceId, "1");
            Assert.AreEqual(items.Entries[0].ETag, "1");
            Assert.AreEqual(items.Entries[0].Name, "Stephen Curry Three Pointers");
            Assert.AreEqual(items.Entries[1].Type, "file");
            Assert.AreEqual(items.Entries[1].Id, "818853862");
            Assert.AreEqual(items.Entries[1].SequenceId, "0");
            Assert.AreEqual(items.Entries[1].ETag, "0");
            Assert.AreEqual(items.Entries[1].Name, "Warriors.jpg");
            Assert.AreEqual(items.Offset, 0);
            Assert.AreEqual(items.Limit, 2);
            //Assert.AreEqual(f.Offset, "0"); // Need to add property
            //Assert.AreEqual(f.Order[0].By, "type"); // Need to add property
            //Assert.AreEqual(f.Order[0].Direction, "ASC"); // Need to add property
            //Assert.AreEqual(f.Order[1].By, "name"); // Need to add property
            //Assert.AreEqual(f.Order[1].Direction, "ASC"); // Need to add property
        }

        [TestMethod]
        public async Task GetFolder_ValidResponse_ValidFolder()
        {
            _handler.Setup(h => h.ExecuteAsync<BoxFolder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxFolder>>(new BoxResponse<BoxFolder>()
                    {
                        Status = ResponseStatus.Success,
                        ContentString = "{ \"type\":\"folder\", \"id\":\"0\", \"sequence_id\":null, \"etag\":null, \"name\":\"All Files\", \"created_at\":null, \"modified_at\":null, \"description\":\"\", \"size\":61591428468, \"path_collection\":{ \"total_count\":0, \"entries\":[ ] }, \"created_by\":{ \"type\":\"user\", \"id\":\"189912110\", \"name\":\"Brian\", \"login\":\"brianytang@gmail.com\" }, \"modified_by\":{ \"type\":\"user\", \"id\":\"189912110\", \"name\":\"Brian\", \"login\":\"brianytang@gmail.com\" }, \"trashed_at\":null, \"purged_at\":null, \"content_created_at\":null, \"content_modified_at\":null, \"owned_by\":{ \"type\":\"user\", \"id\":\"189912110\", \"name\":\"Brian\", \"login\":\"brianytang@gmail.com\" }, \"shared_link\":null, \"folder_upload_email\":null, \"parent\":null, \"item_status\":\"active\", \"item_collection\":{ \"total_count\":10, \"entries\":[ { \"type\":\"folder\", \"id\":\"766352168\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"Books\" }, { \"type\":\"folder\", \"id\":\"869883498\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"bytLabs\" }, { \"type\":\"folder\", \"id\":\"767221958\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"Games\" }, { \"type\":\"folder\", \"id\":\"766174084\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"Mixes\" }, { \"type\":\"folder\", \"id\":\"57181304\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"Muzik\" }, { \"type\":\"folder\", \"id\":\"857305570\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"My\" }, { \"type\":\"folder\", \"id\":\"627316229\", \"sequence_id\":\"1\", \"etag\":\"1\", \"name\":\"My Music Folder\" }, { \"type\":\"folder\", \"id\":\"860155462\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"sample\" }, { \"type\":\"folder\", \"id\":\"775829294\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"Software\" }, { \"type\":\"folder\", \"id\":\"811565831\", \"sequence_id\":\"0\", \"etag\":\"0\", \"name\":\"Test\" } ], \"offset\":0, \"limit\":10, \"order\":[ { \"by\":\"type\", \"direction\":\"ASC\" }, { \"by\":\"name\", \"direction\":\"ASC\" } ] } }"
                    }));

            BoxFolder f = await _foldersManager.GetItemsAsync("0", 10);

            Assert.AreEqual(f.Id, "0");
            Assert.AreEqual(f.Type, "folder");
            Assert.IsNull(f.SequenceId);
            Assert.IsNull(f.ETag);
            Assert.IsNull(f.CreatedAt);
            Assert.IsNull(f.ModifiedAt);
            Assert.AreEqual(f.Description, "");
            Assert.AreEqual(f.Size, 61591428468);
            Assert.AreEqual(f.PathCollection.TotalCount, 0);
            Assert.AreEqual(f.PathCollection.Entries.Count, 0);
            Assert.AreEqual(f.CreatedBy.Type, "user");
            Assert.AreEqual(f.CreatedBy.Id, "189912110");
            Assert.AreEqual(f.CreatedBy.Name, "Brian");
            Assert.AreEqual(f.CreatedBy.Login, "brianytang@gmail.com");
            Assert.AreEqual(f.ModifiedBy.Type, "user");
            Assert.AreEqual(f.ModifiedBy.Id, "189912110");
            Assert.AreEqual(f.ModifiedBy.Name, "Brian");
            Assert.AreEqual(f.ModifiedBy.Login, "brianytang@gmail.com");
            //Assert.IsNull(f.TrashedAt); // Need to add property
            //Assert.IsNull(f.PurgedAt); // Need to add property
            //Assert.IsNull(f.ContentCreatedAt); // Need to add property
            //Assert.IsNull(f.ContentModifiedAt); // Need to add property
            Assert.AreEqual(f.OwnedBy.Type, "user");
            Assert.AreEqual(f.OwnedBy.Id, "189912110");
            Assert.AreEqual(f.OwnedBy.Name, "Brian");
            Assert.AreEqual(f.OwnedBy.Login, "brianytang@gmail.com");
            Assert.IsNull(f.SharedLink);
            Assert.IsNull(f.FolderUploadEmail);
            Assert.IsNull(f.Parent);
            Assert.AreEqual(f.ItemStatus, "active");
            Assert.AreEqual(f.Id, "0");
            Assert.AreEqual(f.Name, "All Files");
            Assert.AreEqual(f.ModifiedBy.Id, "189912110");
            Assert.AreEqual(f.ItemCollection.TotalCount, 10);
            Assert.AreEqual(f.ItemCollection.Entries.Count, 10);
            //Assert.AreEqual(f.Offset, "0"); // Need to add property
            //Assert.AreEqual(f.Order[0].By, "type"); // Need to add property
            //Assert.AreEqual(f.Order[0].Direction, "ASC"); // Need to add property
            //Assert.AreEqual(f.Order[1].By, "name"); // Need to add property
            //Assert.AreEqual(f.Order[1].Direction, "ASC"); // Need to add property
        }

        [TestMethod]
        public async Task CreateFolder_ValidResponse_ValidFolder()
        {
            _handler.Setup(h => h.ExecuteAsync<BoxFolder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxFolder>>(new BoxResponse<BoxFolder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\", \"created_at\": \"2012-12-12T10:53:43-08:00\", \"modified_at\": \"2012-12-12T11:15:04-08:00\", \"description\": \"Some pictures I took\", \"size\": 629644, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/vspke7y05sb214wjokpk\", \"download_url\": \"https://www.box.com/shared/static/vspke7y05sb214wjokpk\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"folder_upload_email\": { \"access\": \"open\", \"email\": \"upload.Picture.k13sdz1@u.box.com\" }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\", \"item_collection\": { \"total_count\": 0, \"entries\": [], \"offset\": 0, \"limit\": 100 } }"
                }));

            var folderReq = new BoxFolderRequest()
            {
                Name = "test",
                Parent = new BoxRequestEntity() { Id = "0" }
            };

            BoxFolder f = await _foldersManager.CreateAsync(folderReq);

            Assert.AreEqual(f.Type, "folder");
            Assert.AreEqual(f.Id, "11446498");
            Assert.AreEqual(f.SequenceId, "1");
            Assert.AreEqual(f.ETag, "1");
            Assert.AreEqual(f.Name, "Pictures");
            Assert.AreEqual(f.CreatedAt, DateTime.Parse("2012-12-12T10:53:43-08:00"));
            Assert.AreEqual(f.ModifiedAt, DateTime.Parse("2012-12-12T11:15:04-08:00"));
            Assert.AreEqual(f.Description, "Some pictures I took");
            Assert.AreEqual(f.Size, 629644);
            Assert.AreEqual(f.PathCollection.TotalCount, 1);
            Assert.AreEqual(f.PathCollection.Entries.Count, 1);
            Assert.AreEqual(f.PathCollection.Entries[0].Id, "0");
            Assert.IsNull(f.PathCollection.Entries[0].SequenceId);
            Assert.IsNull(f.PathCollection.Entries[0].ETag);
            Assert.AreEqual(f.PathCollection.Entries[0].Name, "All Files");
            Assert.AreEqual(f.CreatedBy.Type, "user");
            Assert.AreEqual(f.CreatedBy.Id, "17738362");
            Assert.AreEqual(f.CreatedBy.Name, "sean rose");
            Assert.AreEqual(f.CreatedBy.Login, "sean@box.com");
            Assert.AreEqual(f.ModifiedBy.Type, "user");
            Assert.AreEqual(f.ModifiedBy.Id, "17738362");
            Assert.AreEqual(f.ModifiedBy.Name, "sean rose");
            Assert.AreEqual(f.ModifiedBy.Login, "sean@box.com");
            Assert.AreEqual(f.OwnedBy.Type, "user");
            Assert.AreEqual(f.OwnedBy.Id, "17738362");
            Assert.AreEqual(f.OwnedBy.Name, "sean rose");
            Assert.AreEqual(f.OwnedBy.Login, "sean@box.com");
            Assert.AreEqual(f.SharedLink.Url, "https://www.box.com/s/vspke7y05sb214wjokpk");
            Assert.AreEqual(f.SharedLink.DownloadUrl, "https://www.box.com/shared/static/vspke7y05sb214wjokpk");
            Assert.AreEqual(f.SharedLink.VanityUrl, null);
            Assert.IsFalse(f.SharedLink.IsPasswordEnabled);
            Assert.IsNull(f.SharedLink.UnsharedAt);
            Assert.AreEqual(f.SharedLink.DownloadCount, 0);
            Assert.AreEqual(f.SharedLink.PreviewCount, 0);
            Assert.AreEqual(f.SharedLink.Access, BoxSharedLinkAccessType.open);
            Assert.IsTrue(f.SharedLink.Permissions.CanDownload);
            Assert.IsTrue(f.SharedLink.Permissions.CanPreview);
            Assert.AreEqual(f.FolderUploadEmail.Acesss, "open");
            Assert.AreEqual(f.FolderUploadEmail.Address, "upload.Picture.k13sdz1@u.box.com");
            Assert.AreEqual(f.Parent.Type, "folder");
            Assert.AreEqual(f.Parent.Id, "0");
            Assert.IsNull(f.Parent.SequenceId);
            Assert.IsNull(f.Parent.ETag);
            Assert.AreEqual(f.Parent.Name, "All Files");
            Assert.AreEqual(f.ItemStatus, "active");
            Assert.AreEqual(f.ItemCollection.TotalCount, 0);
            Assert.AreEqual(f.ItemCollection.Entries.Count, 0);
            //Assert.AreEqual(f.Offset, 0); // Need to add property
            //Assert.AreEqual(f.Limit, 100); // Need to add property
        }

        [TestMethod]
        public async Task GetFolderInformation_ValidResponse_ValidFolder()
        {
            _handler.Setup(h => h.ExecuteAsync<BoxFolder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxFolder>>(new BoxResponse<BoxFolder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\", \"created_at\": \"2012-12-12T10:53:43-08:00\", \"modified_at\": \"2012-12-12T11:15:04-08:00\", \"description\": \"Some pictures I took\", \"size\": 629644, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/vspke7y05sb214wjokpk\", \"download_url\": \"https://www.box.com/shared/static/vspke7y05sb214wjokpk\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"folder_upload_email\": { \"access\": \"open\", \"email\": \"upload.Picture.k13sdz1@u.box.com\" }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\", \"item_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\" } ], \"offset\": 0, \"limit\": 100 } }"
                }));

            BoxFolder f = await _foldersManager.GetInformationAsync("11446498");

            Assert.AreEqual(f.Type, "folder");
            Assert.AreEqual(f.Id, "11446498");
            Assert.AreEqual(f.SequenceId, "1");
            Assert.AreEqual(f.ETag, "1");
            Assert.AreEqual(f.Name, "Pictures");
            Assert.AreEqual(f.CreatedAt, DateTime.Parse("2012-12-12T10:53:43-08:00"));
            Assert.AreEqual(f.ModifiedAt, DateTime.Parse("2012-12-12T11:15:04-08:00"));
            Assert.AreEqual(f.Description, "Some pictures I took");
            Assert.AreEqual(f.Size, 629644);
            Assert.AreEqual(f.PathCollection.TotalCount, 1);
            Assert.AreEqual(f.PathCollection.Entries.Count, 1);
            Assert.AreEqual(f.PathCollection.Entries[0].Type, "folder");
            Assert.AreEqual(f.PathCollection.Entries[0].Id, "0");
            Assert.IsNull(f.PathCollection.Entries[0].SequenceId);
            Assert.IsNull(f.PathCollection.Entries[0].ETag);
            Assert.AreEqual(f.PathCollection.Entries[0].Name, "All Files");
            Assert.AreEqual(f.CreatedBy.Type, "user");
            Assert.AreEqual(f.CreatedBy.Id, "17738362");
            Assert.AreEqual(f.CreatedBy.Name, "sean rose");
            Assert.AreEqual(f.CreatedBy.Login, "sean@box.com");
            Assert.AreEqual(f.ModifiedBy.Type, "user");
            Assert.AreEqual(f.ModifiedBy.Id, "17738362");
            Assert.AreEqual(f.ModifiedBy.Name, "sean rose");
            Assert.AreEqual(f.ModifiedBy.Login, "sean@box.com");
            Assert.AreEqual(f.OwnedBy.Type, "user");
            Assert.AreEqual(f.OwnedBy.Id, "17738362");
            Assert.AreEqual(f.OwnedBy.Name, "sean rose");
            Assert.AreEqual(f.OwnedBy.Login, "sean@box.com");
            Assert.AreEqual(f.SharedLink.Url, "https://www.box.com/s/vspke7y05sb214wjokpk");
            Assert.AreEqual(f.SharedLink.DownloadUrl, "https://www.box.com/shared/static/vspke7y05sb214wjokpk");
            Assert.AreEqual(f.SharedLink.VanityUrl, null);
            Assert.IsFalse(f.SharedLink.IsPasswordEnabled);
            Assert.IsNull(f.SharedLink.UnsharedAt);
            Assert.AreEqual(f.SharedLink.DownloadCount, 0);
            Assert.AreEqual(f.SharedLink.PreviewCount, 0);
            Assert.AreEqual(f.SharedLink.Access, BoxSharedLinkAccessType.open);
            Assert.IsTrue(f.SharedLink.Permissions.CanDownload);
            Assert.IsTrue(f.SharedLink.Permissions.CanPreview);
            Assert.AreEqual(f.FolderUploadEmail.Acesss, "open");
            Assert.AreEqual(f.FolderUploadEmail.Address, "upload.Picture.k13sdz1@u.box.com");
            Assert.AreEqual(f.Parent.Type, "folder");
            Assert.AreEqual(f.Parent.Id, "0");
            Assert.IsNull(f.Parent.SequenceId);
            Assert.IsNull(f.Parent.ETag);
            Assert.AreEqual(f.Parent.Name, "All Files");
            Assert.AreEqual(f.ItemStatus, "active");
            Assert.AreEqual(f.ItemCollection.TotalCount, 1);
            Assert.AreEqual(f.ItemCollection.Entries.Count, 1);
            Assert.AreEqual(f.ItemCollection.Entries[0].Type, "file");
            Assert.AreEqual(f.ItemCollection.Entries[0].Id, "5000948880");
            Assert.AreEqual(f.ItemCollection.Entries[0].SequenceId, "3");
            Assert.AreEqual(f.ItemCollection.Entries[0].ETag, "3");
            //Assert.AreEqual(f.ItemCollection.Entries[0].Sha1, "134b65991ed521fcfe4724b7d814ab8ded5185dc"); // Need to add property
            Assert.AreEqual(f.ItemCollection.Entries[0].Name, "tigers.jpeg");
            //Assert.AreEqual(f.Offset, 0); // Need to add property
            //Assert.AreEqual(f.Limit, 100); // Need to add property


        }

        [TestMethod]
        public async Task CopyFolder_ValidResponse_ValidFolder()
        {
            /*** Arrange ***/
            _handler.Setup(h => h.ExecuteAsync<BoxFolder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxFolder>>(new BoxResponse<BoxFolder>()
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

            BoxFolder f = await _foldersManager.CopyAsync(folderReq);

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
            _handler.Setup(h => h.ExecuteAsync<BoxFolder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxFolder>>(new BoxResponse<BoxFolder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"New Folder Name!\", \"created_at\": \"2012-12-12T10:53:43-08:00\", \"modified_at\": \"2012-12-12T11:15:04-08:00\", \"description\": \"Some pictures I took\", \"size\": 629644, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/vspke7y05sb214wjokpk\", \"download_url\": \"https://www.box.com/shared/static/vspke7y05sb214wjokpk\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"folder_upload_email\": { \"access\": \"open\", \"email\": \"upload.Picture.k13sdz1@u.box.com\" }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\", \"item_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\" } ], \"offset\": 0, \"limit\": 100 } }"
                }));

            /*** Act ***/
            BoxFolderRequest folderReq = new BoxFolderRequest()
            {
                Id = "fakeId",
                Name = "New Folder Name!",
                FolderUploadEmail = new BoxEmailRequest() {  Acesss = "open" }
            };

            BoxFolder f = await _foldersManager.UpdateInformationAsync(folderReq);

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
            _handler.Setup(h => h.ExecuteAsync<BoxFolder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxFolder>>(new BoxResponse<BoxFolder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"type\": \"folder\", \"id\": \"11446498\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"Pictures\", \"created_at\": \"2012-12-12T10:53:43-08:00\", \"modified_at\": \"2012-12-12T11:15:04-08:00\", \"description\": \"Some pictures I took\", \"size\": 629644, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" } ] }, \"created_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"owned_by\": { \"type\": \"user\", \"id\": \"17738362\", \"name\": \"sean rose\", \"login\": \"sean@box.com\" }, \"shared_link\": { \"url\": \"https://www.box.com/s/vspke7y05sb214wjokpk\", \"download_url\": \"https://www.box.com/shared/static/vspke7y05sb214wjokpk\", \"vanity_url\": null, \"is_password_enabled\": false, \"unshared_at\": null, \"download_count\": 0, \"preview_count\": 0, \"access\": \"open\", \"permissions\": { \"can_download\": true, \"can_preview\": true } }, \"folder_upload_email\": { \"access\": \"open\", \"email\": \"upload.Picture.k13sdz1@u.box.com\" }, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"active\", \"item_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"file\", \"id\": \"5000948880\", \"sequence_id\": \"3\", \"etag\": \"3\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\", \"name\": \"tigers.jpeg\" } ], \"offset\": 0, \"limit\": 100 } }"
                }));

            /*** Act ***/
            BoxSharedLinkRequest sharedLink = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.collaborators
            };

            BoxFolder f = await _foldersManager.CreateSharedLinkAsync("0", sharedLink);

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
            _handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxCollaboration>>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxCollection<BoxCollaboration>>>(new BoxResponse<BoxCollection<BoxCollaboration>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"total_count\": 1, \"entries\": [ { \"type\": \"collaboration\", \"id\": \"14176246\", \"created_by\": { \"type\": \"user\", \"id\": \"4276790\", \"name\": \"David Lee\", \"login\": \"david@box.com\" }, \"created_at\": \"2011-11-29T12:56:35-08:00\", \"modified_at\": \"2012-09-11T15:12:32-07:00\", \"expires_at\": null, \"status\": \"accepted\", \"accessible_by\": { \"type\": \"user\", \"id\": \"755492\", \"name\": \"Simon Tan\", \"login\": \"simon@box.net\" }, \"role\": \"editor\", \"acknowledged_at\": \"2011-11-29T12:59:40-08:00\", \"item\": null } ] }"
                }));

            /*** Act ***/
            BoxCollection<BoxCollaboration> c = await _foldersManager.GetCollaborationsAsync("fakeId");
            BoxCollaboration collab = c.Entries.FirstOrDefault();

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
            _handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxItem>>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxCollection<BoxItem>>>(new BoxResponse<BoxCollection<BoxItem>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"total_count\": 49542, \"entries\": [ { \"type\": \"file\", \"id\": \"2701979016\", \"sequence_id\": \"1\", \"etag\": \"1\", \"sha1\": \"9d976863fc849f6061ecf9736710bd9c2bce488c\", \"name\": \"file Tue Jul 24 145436 2012KWPX5S.csv\" }, { \"type\": \"file\", \"id\": \"2698211586\", \"sequence_id\": \"1\", \"etag\": \"1\", \"sha1\": \"09b0e2e9760caf7448c702db34ea001f356f1197\", \"name\": \"file Tue Jul 24 010055 20129Z6GS3.csv\" } ], \"offset\": 0, \"limit\": 2 }"
                }));

            /*** Act ***/
            BoxCollection<BoxItem> i = await _foldersManager.GetTrashItemsAsync("fakeId", 10);
            BoxItem i1 = i.Entries.FirstOrDefault();
            BoxItem i2 = i.Entries.Skip(1).FirstOrDefault();

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
            _handler.Setup(h => h.ExecuteAsync<BoxFolder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxFolder>>(new BoxResponse<BoxFolder>()
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

            BoxFolder f = await _foldersManager.RestoreTrashedFolderAsync(folderReq);

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
            _handler.Setup(h => h.ExecuteAsync<BoxFolder>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<BoxFolder>>(new BoxResponse<BoxFolder>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{ \"type\": \"folder\", \"id\": \"588970022\", \"sequence_id\": \"1\", \"etag\": \"1\", \"name\": \"heloo world\", \"created_at\": \"2013-01-15T16:15:27-08:00\", \"modified_at\": \"2013-01-17T13:48:23-08:00\", \"description\": \"\", \"size\": 0, \"path_collection\": { \"total_count\": 1, \"entries\": [ { \"type\": \"folder\", \"id\": \"1\", \"sequence_id\": null, \"etag\": null, \"name\": \"Trash\" } ] }, \"created_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"modified_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"trashed_at\": \"2013-02-07T12:53:32-08:00\", \"purged_at\": \"2013-03-09T12:53:32-08:00\", \"content_created_at\": \"2013-01-15T16:15:27-08:00\", \"content_modified_at\": \"2013-01-17T13:48:23-08:00\", \"owned_by\": { \"type\": \"user\", \"id\": \"181757341\", \"name\": \"sean test\", \"login\": \"sean+test@box.com\" }, \"shared_link\": null, \"folder_upload_email\": null, \"parent\": { \"type\": \"folder\", \"id\": \"0\", \"sequence_id\": null, \"etag\": null, \"name\": \"All Files\" }, \"item_status\": \"trashed\" }"
                }));

            /***Act ***/
            BoxFolder f = await _foldersManager.GetTrashedFolderAsync("fakeId");

            /*** Assert ***/
            Assert.AreEqual("folder", f.Type);
            Assert.AreEqual("588970022", f.Id);
            Assert.AreEqual("1", f.SequenceId);
            Assert.AreEqual("1", f.ETag);
            Assert.AreEqual("heloo world", f.Name);
        }
    }
}
