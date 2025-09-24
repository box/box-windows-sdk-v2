using System;
using System.Threading.Tasks;
using Box.V2.Managers;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxFileRequestsManagerTest : BoxResourceManagerTest
    {
        private readonly BoxFileRequestsManager _fileRequestsManager;

        public BoxFileRequestsManagerTest()
        {
            _fileRequestsManager = new BoxFileRequestsManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task GetFileRequestById_Success()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxFileRequestObject>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFileRequestObject>>(new BoxResponse<BoxFileRequestObject>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxFileRequest/GetFileRequest200.json")
                }))
            .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxFileRequestObject response = await _fileRequestsManager.GetFileRequestByIdAsync("42037322");

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/file_requests/42037322"), boxRequest.AbsoluteUri);

            // Response check
            Assert.AreEqual("42037322", response.Id);
            Assert.AreEqual(DateTimeOffset.Parse("2020-09-28T10:53:43-08:00"), response.CreatedAt);
            Assert.AreEqual("ceo@example.com", response.CreatedBy.Login);
            Assert.AreEqual("Aaron Levie", response.CreatedBy.Name);
            Assert.AreEqual("Following documents are requested for your process", response.Description);
            Assert.AreEqual("1", response.Etag);
            Assert.AreEqual(DateTimeOffset.Parse("2020-09-28T10:53:43-08:00"), response.ExpiresAt);
            Assert.AreEqual("12345", response.Folder.Id);
            Assert.AreEqual("Contracts", response.Folder.Name);
            Assert.AreEqual(true, response.IsDescriptionRequired);
            Assert.AreEqual(true, response.IsEmailRequired);
            Assert.AreEqual(BoxFileRequestStatus.active, response.Status);
            Assert.AreEqual("Please upload documents", response.Title);
            Assert.AreEqual(DateTimeOffset.Parse("2020-09-28T10:53:43-08:00"), response.UpdatedAt);
            Assert.AreEqual("11446498", response.UpdatedBy.Id);
            Assert.AreEqual("ceo@example.com", response.UpdatedBy.Login);
            Assert.AreEqual("Aaron Levie", response.UpdatedBy.Name);
            Assert.AreEqual("/f/19e57f40ace247278a8e3d336678c64a", response.Url);
        }

        [TestMethod]
        public async Task CopyFileRequest_Success()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxFileRequestObject>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFileRequestObject>>(new BoxResponse<BoxFileRequestObject>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxFileRequest/CopyFileRequest200.json")
                }))
            .Callback<IBoxRequest>(r => boxRequest = r);

            var folder = new BoxRequestEntity()
            {
                Id = "44444",
                Type = BoxType.folder
            };
            var copyRequest = new BoxFileRequestCopyRequest
            {
                Folder = folder
            };

            /*** Act ***/
            BoxFileRequestObject response = await _fileRequestsManager.CopyFileRequestAsync("42037322", copyRequest);

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/file_requests/42037322/copy"), boxRequest.AbsoluteUri);

            // Response check
            Assert.AreEqual("42037322", response.Id);
            Assert.AreEqual(DateTimeOffset.Parse("2020-09-28T10:53:43-08:00"), response.CreatedAt);
            Assert.AreEqual("ceo@example.com", response.CreatedBy.Login);
            Assert.AreEqual("Aaron Levie", response.CreatedBy.Name);
            Assert.AreEqual("Following documents are requested for your process", response.Description);
            Assert.AreEqual("1", response.Etag);
            Assert.AreEqual(DateTimeOffset.Parse("2020-09-28T10:53:43-08:00"), response.ExpiresAt);
            Assert.AreEqual("44444", response.Folder.Id);
            Assert.AreEqual("Contracts2", response.Folder.Name);
            Assert.AreEqual(true, response.IsDescriptionRequired);
            Assert.AreEqual(true, response.IsEmailRequired);
            Assert.AreEqual(BoxFileRequestStatus.active, response.Status);
            Assert.AreEqual("Please upload documents", response.Title);
            Assert.AreEqual(DateTimeOffset.Parse("2020-09-28T10:53:43-08:00"), response.UpdatedAt);
            Assert.AreEqual("11446498", response.UpdatedBy.Id);
            Assert.AreEqual("ceo@example.com", response.UpdatedBy.Login);
            Assert.AreEqual("Aaron Levie", response.UpdatedBy.Name);
            Assert.AreEqual("/f/19e57f40ace247278a8e3d336678c64a", response.Url);
        }

        [TestMethod]
        public async Task UpdateFileRequest_Success()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxFileRequestObject>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxFileRequestObject>>(new BoxResponse<BoxFileRequestObject>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = LoadFixtureFromJson("Fixtures/BoxFileRequest/UpdateFileRequest200.json")
                }))
            .Callback<IBoxRequest>(r => boxRequest = r);

            var updateRequest = new BoxFileRequestUpdateRequest
            {
                Description = "New, updated description",
                ExpiresAt = DateTimeOffset.Parse("2021-09-28T10:53:43-08:00"),
                IsEmailRequired = false,
                Status = BoxFileRequestStatus.inactive
            };

            /*** Act ***/
            BoxFileRequestObject response = await _fileRequestsManager.UpdateFileRequestAsync("42037322", updateRequest);

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Put, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/file_requests/42037322"), boxRequest.AbsoluteUri);
            Assert.IsTrue(boxRequest.Payload.ContainsKeyValue("status", "inactive"));

            // Response check
            Assert.AreEqual("42037322", response.Id);
            Assert.AreEqual(DateTimeOffset.Parse("2020-09-28T10:53:43-08:00"), response.CreatedAt);
            Assert.AreEqual("ceo@example.com", response.CreatedBy.Login);
            Assert.AreEqual("Aaron Levie", response.CreatedBy.Name);
            Assert.AreEqual("New, updated description", response.Description);
            Assert.AreEqual("1", response.Etag);
            Assert.AreEqual(DateTimeOffset.Parse("2021-09-28T10:53:43-08:00"), response.ExpiresAt);
            Assert.AreEqual("12345", response.Folder.Id);
            Assert.AreEqual("Contracts", response.Folder.Name);
            Assert.AreEqual(true, response.IsDescriptionRequired);
            Assert.AreEqual(false, response.IsEmailRequired);
            Assert.AreEqual(BoxFileRequestStatus.inactive, response.Status);
            Assert.AreEqual("Please upload documents", response.Title);
            Assert.AreEqual(DateTimeOffset.Parse("2020-09-28T10:53:43-08:00"), response.UpdatedAt);
            Assert.AreEqual("11446498", response.UpdatedBy.Id);
            Assert.AreEqual("ceo@example.com", response.UpdatedBy.Login);
            Assert.AreEqual("Aaron Levie", response.UpdatedBy.Name);
            Assert.AreEqual("/f/19e57f40ace247278a8e3d336678c64a", response.Url);
        }

        [TestMethod]
        public async Task DeleteFileRequest_Success()
        {
            /*** Arrange ***/
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<object>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<object>>(new BoxResponse<object>()
                {
                    Status = ResponseStatus.Success,
                }))
            .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var response = await _fileRequestsManager.DeleteFileRequestAsync("42037322");

            /*** Assert ***/
            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Delete, boxRequest.Method);
            Assert.AreEqual(new Uri("https://api.box.com/2.0/file_requests/42037322"), boxRequest.AbsoluteUri);

            // Response check
            Assert.AreEqual(true, response);
        }
    }
}
