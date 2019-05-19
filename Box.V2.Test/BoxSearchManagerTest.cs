using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxSearchManagerTest : BoxResourceManagerTest
    {
        protected BoxSearchManager _searchManager;

        public BoxSearchManagerTest()
        {
            _searchManager = new BoxSearchManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task SearchKeyword_ValidResponse_ValidResults()
        {
            /*** Arrange ***/
            string responseString = "{\"total_count\":4,\"entries\":[{\"type\":\"file\",\"id\":\"1874102965\",\"sequence_id\":\"0\",\"etag\":\"0\",\"sha1\":\"63a112a4567fb556f5269735102a2f24f2cbea56\",\"name\":\"football.jpg\",\"description\":\"\",\"size\":260271,\"path_collection\":{\"total_count\":1,\"entries\":[{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"}]},\"created_at\":\"2012-03-22T18:25:07-07:00\",\"modified_at\":\"2012-10-25T14:40:05-07:00\",\"created_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"modified_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"owned_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"shared_link\":null,\"parent\":{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"},\"item_status\":\"active\", \"sort\":\"modified_at\", \"direction\":\"ASC\"}],\"offset\":0,\"limit\":1}";
            IBoxRequest boxRequest = null;

            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxItem>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxItem>>>(new BoxResponse<BoxCollection<BoxItem>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCollection<BoxItem> results = await _searchManager.SearchAsync("fakeKeyword", 10, sort: "modified_at", direction: BoxSortDirection.ASC);
            var queryParams = boxRequest.Parameters;
            /*** Assert ***/
            Assert.AreEqual(4, results.TotalCount);
            Assert.AreEqual("modified_at", queryParams["sort"]);
            Assert.AreEqual("ASC", queryParams["direction"]);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task SearchWithDateRanges_ValidResponse_ValidResults()
        {
            /*** Arrange ***/
            string responseString = "{\"total_count\":4,\"entries\":[{\"type\":\"file\",\"id\":\"1874102965\",\"sequence_id\":\"0\",\"etag\":\"0\",\"sha1\":\"63a112a4567fb556f5269735102a2f24f2cbea56\",\"name\":\"football.jpg\",\"description\":\"\",\"size\":260271,\"path_collection\":{\"total_count\":1,\"entries\":[{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"}]},\"created_at\":\"2012-03-22T18:25:07-07:00\",\"modified_at\":\"2012-10-25T14:40:05-07:00\",\"created_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"modified_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"owned_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"shared_link\":null,\"parent\":{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"},\"item_status\":\"active\", \"sort\":\"modified_at\", \"direction\":\"ASC\"}],\"offset\":0,\"limit\":1}";
            IBoxRequest boxRequest = null;

            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxItem>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxItem>>>(new BoxResponse<BoxCollection<BoxItem>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var startDate = new DateTime(1988, 11, 18, 9, 30, 0, DateTimeKind.Utc);
            var endDate = new DateTime(2018, 11, 18, 9, 30, 0, DateTimeKind.Utc);
            var results = await _searchManager.SearchAsync("test", createdAtRangeFromDate: startDate, createdAtRangeToDate: endDate, updatedAtRangeFromDate: startDate, updatedAtRangeToDate: endDate);

            /*** Assert ***/
            Assert.AreEqual("query=test&created_at_range=1988-11-18T09%3A30%3A00Z%2C2018-11-18T09%3A30%3A00Z&updated_at_range=1988-11-18T09%3A30%3A00Z%2C2018-11-18T09%3A30%3A00Z&limit=30&offset=0", boxRequest.GetQueryString());
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task SearchWithOpenDateRanges_ValidResponse_ValidResults()
        {
            /*** Arrange ***/
            string responseString = "{\"total_count\":4,\"entries\":[{\"type\":\"file\",\"id\":\"1874102965\",\"sequence_id\":\"0\",\"etag\":\"0\",\"sha1\":\"63a112a4567fb556f5269735102a2f24f2cbea56\",\"name\":\"football.jpg\",\"description\":\"\",\"size\":260271,\"path_collection\":{\"total_count\":1,\"entries\":[{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"}]},\"created_at\":\"2012-03-22T18:25:07-07:00\",\"modified_at\":\"2012-10-25T14:40:05-07:00\",\"created_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"modified_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"owned_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"shared_link\":null,\"parent\":{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"},\"item_status\":\"active\", \"sort\":\"modified_at\", \"direction\":\"ASC\"}],\"offset\":0,\"limit\":1}";
            IBoxRequest boxRequest = null;

            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxItem>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxItem>>>(new BoxResponse<BoxCollection<BoxItem>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var startDate = new DateTime(1988, 11, 18, 9, 30, 0, DateTimeKind.Utc);
            var endDate = new DateTime(2018, 11, 18, 9, 30, 0, DateTimeKind.Utc);
            var results = await _searchManager.SearchAsync("test", createdAtRangeFromDate: startDate, updatedAtRangeToDate: endDate);

            /*** Assert ***/
            Assert.AreEqual("query=test&created_at_range=1988-11-18T09%3A30%3A00Z%2C&updated_at_range=%2C2018-11-18T09%3A30%3A00Z&limit=30&offset=0", boxRequest.GetQueryString());
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task Query_ValidResponse_ValidResults()
        {
            /*** Arrange ***/
            string responseString = "{\"total_count\":4,\"entries\":[{\"type\":\"file\",\"id\":\"1874102965\",\"sequence_id\":\"0\",\"etag\":\"0\",\"sha1\":\"63a112a4567fb556f5269735102a2f24f2cbea56\",\"name\":\"football.jpg\",\"description\":\"\",\"size\":260271,\"path_collection\":{\"total_count\":1,\"entries\":[{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"}]},\"created_at\":\"2012-03-22T18:25:07-07:00\",\"modified_at\":\"2012-10-25T14:40:05-07:00\",\"created_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"modified_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"owned_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"shared_link\":null,\"parent\":{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"},\"item_status\":\"active\", \"sort\":\"modified_at\", \"direction\":\"ASC\"}],\"offset\":0,\"limit\":1}";
            IBoxRequest boxRequest = null;

            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxItem>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxItem>>>(new BoxResponse<BoxCollection<BoxItem>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCollection<BoxItem> results = await _searchManager.QueryAsync("fakeKeyword", limit: 10, sort: "modified_at", direction: BoxSortDirection.ASC);
            var queryParams = boxRequest.Parameters;
            /*** Assert ***/
            Assert.AreEqual(4, results.TotalCount);
            Assert.AreEqual("modified_at", queryParams["sort"]);
            Assert.AreEqual("ASC", queryParams["direction"]);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task QueryWithDateRanges_ValidResponse_ValidResults()
        {
            /*** Arrange ***/
            string responseString = "{\"total_count\":4,\"entries\":[{\"type\":\"file\",\"id\":\"1874102965\",\"sequence_id\":\"0\",\"etag\":\"0\",\"sha1\":\"63a112a4567fb556f5269735102a2f24f2cbea56\",\"name\":\"football.jpg\",\"description\":\"\",\"size\":260271,\"path_collection\":{\"total_count\":1,\"entries\":[{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"}]},\"created_at\":\"2012-03-22T18:25:07-07:00\",\"modified_at\":\"2012-10-25T14:40:05-07:00\",\"created_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"modified_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"owned_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"shared_link\":null,\"parent\":{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"},\"item_status\":\"active\", \"sort\":\"modified_at\", \"direction\":\"ASC\"}],\"offset\":0,\"limit\":1}";
            IBoxRequest boxRequest = null;

            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxItem>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxItem>>>(new BoxResponse<BoxCollection<BoxItem>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var startDate = new DateTime(1988, 11, 18, 9, 30, 0, DateTimeKind.Utc);
            var endDate = new DateTime(2018, 11, 18, 9, 30, 0, DateTimeKind.Utc);
            var results = await _searchManager.QueryAsync("test", createdAfter: startDate, createdBefore: endDate, updatedAfter: startDate, updatedBefore: endDate);

            /*** Assert ***/
            Assert.AreEqual("query=test&created_at_range=1988-11-18T09%3A30%3A00Z%2C2018-11-18T09%3A30%3A00Z&updated_at_range=1988-11-18T09%3A30%3A00Z%2C2018-11-18T09%3A30%3A00Z&limit=30&offset=0", boxRequest.GetQueryString());
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task QueryWithOpenDateRanges_ValidResponse_ValidResults()
        {
            /*** Arrange ***/
            string responseString = "{\"total_count\":4,\"entries\":[{\"type\":\"file\",\"id\":\"1874102965\",\"sequence_id\":\"0\",\"etag\":\"0\",\"sha1\":\"63a112a4567fb556f5269735102a2f24f2cbea56\",\"name\":\"football.jpg\",\"description\":\"\",\"size\":260271,\"path_collection\":{\"total_count\":1,\"entries\":[{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"}]},\"created_at\":\"2012-03-22T18:25:07-07:00\",\"modified_at\":\"2012-10-25T14:40:05-07:00\",\"created_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"modified_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"owned_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"shared_link\":null,\"parent\":{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"},\"item_status\":\"active\", \"sort\":\"modified_at\", \"direction\":\"ASC\"}],\"offset\":0,\"limit\":1}";
            IBoxRequest boxRequest = null;

            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxItem>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxItem>>>(new BoxResponse<BoxCollection<BoxItem>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var startDate = new DateTime(1988, 11, 18, 9, 30, 0, DateTimeKind.Utc);
            var endDate = new DateTime(2018, 11, 18, 9, 30, 0, DateTimeKind.Utc);
            var results = await _searchManager.QueryAsync("test", createdAfter: startDate, updatedBefore: endDate);

            /*** Assert ***/
            Assert.AreEqual("query=test&created_at_range=1988-11-18T09%3A30%3A00Z%2C&updated_at_range=%2C2018-11-18T09%3A30%3A00Z&limit=30&offset=0", boxRequest.GetQueryString());
        }
    }
}
