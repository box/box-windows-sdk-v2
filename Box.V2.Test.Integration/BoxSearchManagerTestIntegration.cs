using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxSearchManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task SearchKeyword_LiveSession_ValidResponse()
        {
            const string Keyword = "IMG";
            const int NumResults = 13;
            const int NumFiles = 12;
            const int NumFolders = 1;

            BoxCollection<BoxItem> results = await Client.SearchManager.SearchAsync(Keyword, 200);

            Assert.IsNotNull(results, "Search results are null");
            Assert.AreEqual(NumResults, results.Entries.Count, "Incorrect number of search results");
            Assert.IsTrue(results.Entries.Count(item => item is BoxFolder) == NumFolders, "Incorrect number of folders in search results");
            Assert.IsTrue(results.Entries.Count(item => item is BoxFile) == NumFiles, "Incorrect number of files in search results");
        }

        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public async Task SearchKeyword_LiveSession_EmptyResult()
        {
            const string Keyword = "NonExistentKeyWord";

            BoxCollection<BoxItem> results = await Client.SearchManager.SearchAsync(Keyword, 200);

            Assert.IsNotNull(results, "Search results are null");
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results");
        }

        [TestMethod]
        public async Task SearchAdvanced_LiveSession()
        {
            const string Keyword = "IMG";

            //search using an extension that should return results
            BoxCollection<BoxItem> results = await Client.SearchManager.SearchAsync(Keyword, 200, fileExtensions: new List<string>() { "jpg" });
            Assert.AreEqual(12, results.Entries.Count, "Incorrect number of search results using extension");

            //search using an extension that should not return results
            results = await Client.SearchManager.SearchAsync(Keyword, 200, fileExtensions: new List<string>() { "pdf" });
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using extension");

            //search using a created at daterange that should return results
            var start = new DateTimeOffset(2014, 5, 1, 0, 0, 0, TimeSpan.Zero);
            var end = new DateTimeOffset(2014, 5, 30, 0, 0, 0, TimeSpan.Zero);
            results = await Client.SearchManager.SearchAsync(Keyword, 200, createdAtRangeFromDate: start, createdAtRangeToDate: end);
            Assert.AreEqual(13, results.Entries.Count, "Incorrect number of search results using created at date range");

            //search using a created at daterange that should not return results
            start = new DateTimeOffset(2014, 6, 1, 0, 0, 0, TimeSpan.Zero);
            end = new DateTimeOffset(2014, 6, 30, 0, 0, 0, TimeSpan.Zero);
            results = await Client.SearchManager.SearchAsync(Keyword, 200, createdAtRangeFromDate: start, createdAtRangeToDate: end);
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using created at date range");

            //search using a updated at daterange that should return results
            start = new DateTimeOffset(2014, 5, 1, 0, 0, 0, TimeSpan.Zero);
            end = new DateTimeOffset(2014, 5, 30, 0, 0, 0, TimeSpan.Zero);
            results = await Client.SearchManager.SearchAsync(Keyword, 200, updatedAtRangeFromDate: start, updatedAtRangeToDate: end);
            Assert.AreEqual(12, results.Entries.Count, "Incorrect number of search results using updated at date range");

            //search using a size range that should return results
            var minBytes = 150000;
            var maxBytes = 400000;
            results = await Client.SearchManager.SearchAsync(Keyword, 200, sizeRangeLowerBoundBytes: minBytes, sizeRangeUpperBoundBytes: maxBytes);
            Assert.AreEqual(2, results.Entries.Count, "Incorrect number of search results using size range");

            //search using a size range that should not return results
            minBytes = 40000000;
            maxBytes = 50000000;
            results = await Client.SearchManager.SearchAsync(Keyword, 200, sizeRangeLowerBoundBytes: minBytes, sizeRangeUpperBoundBytes: maxBytes);
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using size range");

            //search using an owner Id that should return results
            var ownerId = "215917383";
            results = await Client.SearchManager.SearchAsync(Keyword, 200, ownerUserIds: new List<string>() { ownerId });
            Assert.AreEqual(13, results.Entries.Count, "Incorrect number of search results using owner id");

            //search using an owner Id that should not return results
            ownerId = "1";
            results = await Client.SearchManager.SearchAsync(Keyword, 200, ownerUserIds: new List<string>() { ownerId });
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using owner id");

            //search using an ancestor folder Id that should return subset of results
            var ancestorFolderId = "1927308583";
            results = await Client.SearchManager.SearchAsync(Keyword, 200, ancestorFolderIds: new List<string>() { ancestorFolderId });
            Assert.AreEqual(6, results.Entries.Count, "Incorrect number of search results using ancestor folder id");

            //search using a content type that should return subset of results
            var contentType = "file_content";
            results = await Client.SearchManager.SearchAsync(Keyword, 200, contentTypes: new List<string>() { contentType });
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using ancestor folder id");

            //search using a type that should return files only
            var type = "file";
            results = await Client.SearchManager.SearchAsync(Keyword, 200, type: type);
            Assert.AreEqual(12, results.Entries.Count, "Incorrect number of search results using type");

            //search using a type that should return folders only
            type = "folder";
            results = await Client.SearchManager.SearchAsync(Keyword, 200, type: type);
            Assert.AreEqual(1, results.Entries.Count, "Incorrect number of search results using type");

            //search using a type that should return web links only
            type = "web_link";
            results = await Client.SearchManager.SearchAsync(Keyword, 200, type: type);
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using type");

            //search trashed content only
            results = await Client.SearchManager.SearchAsync(Keyword, 200, trashContent: "trashed_only");
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using trashed_only");

            //search non-trashed content only
            results = await Client.SearchManager.SearchAsync(Keyword, 200, trashContent: "non_trashed_only");
            Assert.AreEqual(13, results.Entries.Count, "Incorrect number of search results using non_trashed_only");
        }

        [TestMethod]
        public async Task SearchMetadata_LiveSession()
        {


            var filter = new
            {
                attr1 = "blah",
                attr2 = new { gt = 5, lt = 5 },
                attr3 = new
                {
                    gt = new DateTimeOffset(2016, 10, 1, 0, 0, 0, TimeSpan.Zero),
                    lt = new DateTimeOffset(2016, 11, 5, 0, 0, 0, TimeSpan.Zero)
                },
                attr4 = "value1"
            };

            var mdFilter = new BoxMetadataFilterRequest()
            {
                TemplateKey = "testtemplate",
                Scope = "enterprise",
                Filters = filter
            };

            var results = await Client.SearchManager.SearchAsync(mdFilters: new List<BoxMetadataFilterRequest>() { mdFilter });
            Assert.AreEqual(1, results.Entries.Count, "Incorrect number of search results using metadata search");
        }
    }
}
