using System;
using System.Threading.Tasks;
using Box.V2.Managers;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxSearchManagerTest : BoxResourceManagerTest
    {
        protected BoxSearchManager SearchManager;

        public BoxSearchManagerTest()
        {
            SearchManager = new BoxSearchManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task SearchKeyword_ValidResponse_ValidResults()
        {
            /*** Arrange ***/
            var responseString = "{\"total_count\":4,\"entries\":[{\"type\":\"file\",\"id\":\"1874102965\",\"sequence_id\":\"0\",\"etag\":\"0\",\"sha1\":\"63a112a4567fb556f5269735102a2f24f2cbea56\",\"name\":\"football.jpg\",\"description\":\"\",\"size\":260271,\"path_collection\":{\"total_count\":1,\"entries\":[{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"}]},\"created_at\":\"2012-03-22T18:25:07-07:00\",\"modified_at\":\"2012-10-25T14:40:05-07:00\",\"created_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"modified_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"owned_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"shared_link\":null,\"parent\":{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"},\"item_status\":\"active\", \"sort\":\"modified_at\", \"direction\":\"ASC\"}],\"offset\":0,\"limit\":1}";
            IBoxRequest boxRequest = null;

            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxItem>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxItem>>>(new BoxResponse<BoxCollection<BoxItem>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCollection<BoxItem> results = await SearchManager.QueryAsync("fakeKeyword", limit: 10, sort: "modified_at", direction: BoxSortDirection.ASC);
            var queryParams = boxRequest.Parameters;
            /*** Assert ***/
            Assert.AreEqual(4, results.TotalCount);
            Assert.AreEqual("modified_at", queryParams["sort"]);
            Assert.AreEqual("ASC", queryParams["direction"]);
        }

        [TestMethod]
        public async Task Query_ValidResponse_ValidResults()
        {
            /*** Arrange ***/
            var responseString = "{\"total_count\":4,\"entries\":[{\"type\":\"file\",\"id\":\"1874102965\",\"sequence_id\":\"0\",\"etag\":\"0\",\"sha1\":\"63a112a4567fb556f5269735102a2f24f2cbea56\",\"name\":\"football.jpg\",\"description\":\"\",\"size\":260271,\"path_collection\":{\"total_count\":1,\"entries\":[{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"}]},\"created_at\":\"2012-03-22T18:25:07-07:00\",\"modified_at\":\"2012-10-25T14:40:05-07:00\",\"created_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"modified_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"owned_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"shared_link\":null,\"parent\":{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"},\"item_status\":\"active\", \"sort\":\"modified_at\", \"direction\":\"ASC\"}],\"offset\":0,\"limit\":1}";
            IBoxRequest boxRequest = null;

            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxItem>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxItem>>>(new BoxResponse<BoxCollection<BoxItem>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCollection<BoxItem> results = await SearchManager.QueryAsync("fakeKeyword", limit: 10, sort: "modified_at", direction: BoxSortDirection.ASC);
            var queryParams = boxRequest.Parameters;
            /*** Assert ***/
            Assert.AreEqual(4, results.TotalCount);
            Assert.AreEqual("modified_at", queryParams["sort"]);
            Assert.AreEqual("ASC", queryParams["direction"]);
        }

        [TestMethod]
        public async Task QueryWithDateRanges_ValidResponse_ValidResults()
        {
            /*** Arrange ***/
            var responseString = "{\"total_count\":4,\"entries\":[{\"type\":\"file\",\"id\":\"1874102965\",\"sequence_id\":\"0\",\"etag\":\"0\",\"sha1\":\"63a112a4567fb556f5269735102a2f24f2cbea56\",\"name\":\"football.jpg\",\"description\":\"\",\"size\":260271,\"path_collection\":{\"total_count\":1,\"entries\":[{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"}]},\"created_at\":\"2012-03-22T18:25:07-07:00\",\"modified_at\":\"2012-10-25T14:40:05-07:00\",\"created_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"modified_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"owned_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"shared_link\":null,\"parent\":{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"},\"item_status\":\"active\", \"sort\":\"modified_at\", \"direction\":\"ASC\"}],\"offset\":0,\"limit\":1}";
            IBoxRequest boxRequest = null;

            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxItem>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxItem>>>(new BoxResponse<BoxCollection<BoxItem>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var startDate = new DateTimeOffset(1988, 11, 18, 9, 30, 0, TimeSpan.Zero);
            var endDate = new DateTimeOffset(2018, 11, 18, 9, 30, 0, TimeSpan.Zero);
            var results = await SearchManager.QueryAsync("test", createdAfter: startDate, createdBefore: endDate, updatedAfter: startDate, updatedBefore: endDate);

            /*** Assert ***/
            Assert.AreEqual("query=test&created_at_range=1988-11-18T09%3A30%3A00%2B00%3A00%2C2018-11-18T09%3A30%3A00%2B00%3A00&updated_at_range=1988-11-18T09%3A30%3A00%2B00%3A00%2C2018-11-18T09%3A30%3A00%2B00%3A00&limit=30&offset=0", boxRequest.GetQueryString());
        }

        [TestMethod]

        public async Task QueryWithOpenDateRanges_ValidResponse_ValidResults()
        {
            /*** Arrange ***/
            var responseString = "{\"total_count\":4,\"entries\":[{\"type\":\"file\",\"id\":\"1874102965\",\"sequence_id\":\"0\",\"etag\":\"0\",\"sha1\":\"63a112a4567fb556f5269735102a2f24f2cbea56\",\"name\":\"football.jpg\",\"description\":\"\",\"size\":260271,\"path_collection\":{\"total_count\":1,\"entries\":[{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"}]},\"created_at\":\"2012-03-22T18:25:07-07:00\",\"modified_at\":\"2012-10-25T14:40:05-07:00\",\"created_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"modified_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"owned_by\":{\"type\":\"user\",\"id\":\"175065494\",\"name\":\"Andrew Luck\",\"login\":\"aluck@colts.com\"},\"shared_link\":null,\"parent\":{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"},\"item_status\":\"active\", \"sort\":\"modified_at\", \"direction\":\"ASC\"}],\"offset\":0,\"limit\":1}";
            IBoxRequest boxRequest = null;

            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxItem>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxItem>>>(new BoxResponse<BoxCollection<BoxItem>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            var startDate = new DateTimeOffset(1988, 11, 18, 9, 30, 0, TimeSpan.Zero);
            var endDate = new DateTimeOffset(2018, 11, 18, 9, 30, 0, TimeSpan.Zero);
            var results = await SearchManager.QueryAsync("test", createdAfter: startDate, updatedBefore: endDate);

            /*** Assert ***/
            Assert.AreEqual("query=test&created_at_range=1988-11-18T09%3A30%3A00%2B00%3A00%2C&updated_at_range=%2C2018-11-18T09%3A30%3A00%2B00%3A00&limit=30&offset=0", boxRequest.GetQueryString());
        }

        [TestMethod]
        public async Task QueryWithSharedLinks_ValidResponse_ValidResults()
        {
            /*** Arrange ***/
            var responseString = "{\"entries\":[{\"accessible_via_shared_link\":\"https://www.box.com/s/vspke7y05sb214wjokpk\",\"item\":{\"type\":\"file\",\"id\":\"11111\",\"file_version\":{\"type\":\"file_version\",\"id\":\"111110\",\"sha1\":\"97cc02de7c356f94e3beeb1e0c63f78a6edb01fd\"},\"sequence_id\":\"9\",\"etag\":\"9\",\"sha1\":\"97cc02de7c356f94e3beeb1e0c63f78a6edb01fd\",\"name\":\"test file.txt\",\"description\":\"\",\"size\":16,\"path_collection\":{\"total_count\":1,\"entries\":[{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"}]},\"created_at\":\"2016-12-07T15:53:59-08:00\",\"modified_at\":\"2018-04-24T15:08:58-07:00\",\"trashed_at\":null,\"purged_at\":null,\"content_created_at\":\"2016-12-07T15:53:59-08:00\",\"content_modified_at\":\"2016-12-07T15:59:32-08:00\",\"created_by\":{\"type\":\"user\",\"id\":\"33333\",\"name\":\"Test User\",\"login\":\"testuser@example.com\"},\"modified_by\":{\"type\":\"user\",\"id\":\"33333\",\"name\":\"Test User\",\"login\":\"testuser@example.com\"},\"owned_by\":{\"type\":\"user\",\"id\":\"33333\",\"name\":\"Test User\",\"login\":\"testuser@example.com\"},\"shared_link\":null,\"parent\":{\"type\":\"folder\",\"id\":\"0\",\"sequence_id\":null,\"etag\":null,\"name\":\"All Files\"},\"item_status\":\"active\"},\"type\":\"search_result\"}],\"limit\":30,\"offset\":0,\"total_count\":1}";
            IBoxRequest boxRequest = null;

            Handler.Setup(h => h.ExecuteAsync<BoxCollection<BoxSearchResult>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollection<BoxSearchResult>>>(new BoxResponse<BoxCollection<BoxSearchResult>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCollection<BoxSearchResult> results = await SearchManager.QueryAsyncWithSharedLinks("test");

            /*** Assert ***/
            Assert.AreEqual("query=test&limit=30&offset=0&include_recent_shared_links=true", boxRequest.GetQueryString());
            Assert.AreEqual(1, results.TotalCount);
            Assert.AreEqual("file", results.Entries[0].Item.Type);
            Assert.AreEqual("11111", results.Entries[0].Item.Id);
        }
    }
}
