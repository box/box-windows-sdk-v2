using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Box.V2.Models.Request;
using Box.V2.Exceptions;
using Box.V2.Config;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxSearchManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task SearchKeyword_LiveSession_ValidResponse()
        {
            const string keyword = "IMG";
            const int numResults = 13;
            const int numFiles = 12;
            const int numFolders = 1;

            BoxCollection<BoxItem> results = await _client.SearchManager.SearchAsync(keyword, 200);

            Assert.IsNotNull(results, "Search results are null");
            Assert.AreEqual(numResults, results.Entries.Count, "Incorrect number of search results");
            Assert.IsTrue(results.Entries.Count(item => item is BoxFolder) == numFolders, "Incorrect number of folders in search results");
            Assert.IsTrue(results.Entries.Count(item => item is BoxFile) == numFiles, "Incorrect number of files in search results");
        }

        [TestMethod]
        public async Task SearchKeyword_LiveSession_EmptyResult()
        {
            const string keyword = "NonExistentKeyWord";

            BoxCollection<BoxItem> results = await _client.SearchManager.SearchAsync(keyword, 200);

            Assert.IsNotNull(results, "Search results are null");
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results");
        }

        [TestMethod]
        public async Task SearchAdvanced_LiveSession()
        {
            const string keyword = "IMG";

            //search using an extension that should return results
            BoxCollection<BoxItem> results = await _client.SearchManager.SearchAsync(keyword, 200, fileExtensions: new List<string>() { "jpg" });
            Assert.AreEqual(12, results.Entries.Count, "Incorrect number of search results using extension");

            //search using an extension that should not return results
            results = await _client.SearchManager.SearchAsync(keyword, 200, fileExtensions:new List<string>() { "pdf" });
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using extension");

            //search using a created at daterange that should return results
            var start = new DateTime(2014, 5, 1);
            var end = new DateTime(2014, 5, 30);
            results = await _client.SearchManager.SearchAsync(keyword, 200, createdAtRangeFromDate: start, createdAtRangeToDate: end);
            Assert.AreEqual(13, results.Entries.Count, "Incorrect number of search results using created at date range");

            //search using a created at daterange that should not return results
            start = new DateTime(2014, 6, 1);
            end = new DateTime(2014, 6, 30);
            results = await _client.SearchManager.SearchAsync(keyword, 200, createdAtRangeFromDate: start, createdAtRangeToDate: end);
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using created at date range");

            //search using a updated at daterange that should return results
            start = new DateTime(2014, 5, 1);
            end = new DateTime(2014, 5, 30);
            results = await _client.SearchManager.SearchAsync(keyword, 200, updatedAtRangeFromDate: start, updatedAtRangeToDate: end);
            Assert.AreEqual(12, results.Entries.Count, "Incorrect number of search results using updated at date range");

            //search using a size range that should return results
            var minBytes = 150000;
            var maxBytes = 400000;
            results = await _client.SearchManager.SearchAsync(keyword, 200, sizeRangeLowerBoundBytes: minBytes, sizeRangeUpperBoundBytes: maxBytes);
            Assert.AreEqual(2, results.Entries.Count, "Incorrect number of search results using size range");

            //search using a size range that should not return results
            minBytes = 40000000;
            maxBytes = 50000000;
            results = await _client.SearchManager.SearchAsync(keyword, 200, sizeRangeLowerBoundBytes: minBytes, sizeRangeUpperBoundBytes: maxBytes);
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using size range");

            //search using an owner Id that should return results
            var ownerId = "215917383";
            results = await _client.SearchManager.SearchAsync(keyword, 200, ownerUserIds: new List<string>() { ownerId });
            Assert.AreEqual(13, results.Entries.Count, "Incorrect number of search results using owner id");

            //search using an owner Id that should not return results
            ownerId = "1";
            results = await _client.SearchManager.SearchAsync(keyword, 200, ownerUserIds: new List<string>() { ownerId });
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using owner id");

            //search using an ancestor folder Id that should return subset of results
            var ancestorFolderId = "1927308583";
            results = await _client.SearchManager.SearchAsync(keyword, 200, ancestorFolderIds: new List<string>() { ancestorFolderId });
            Assert.AreEqual(6, results.Entries.Count, "Incorrect number of search results using ancestor folder id");

            //search using a content type that should return subset of results
            var contentType = "file_content";
            results = await _client.SearchManager.SearchAsync(keyword, 200, contentTypes: new List<string>() { contentType });
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using ancestor folder id");

            //search using a type that should return files only
            var type = "file";
            results = await _client.SearchManager.SearchAsync(keyword, 200, type: type);
            Assert.AreEqual(12, results.Entries.Count, "Incorrect number of search results using type");

            //search using a type that should return folders only
            type = "folder";
            results = await _client.SearchManager.SearchAsync(keyword, 200, type: type);
            Assert.AreEqual(1, results.Entries.Count, "Incorrect number of search results using type");

            //search using a type that should return web links only
            type = "web_link";
            results = await _client.SearchManager.SearchAsync(keyword, 200, type: type);
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using type");

            //search trashed content only
            results = await _client.SearchManager.SearchAsync(keyword, 200, trashContent: "trashed_only");
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results using trashed_only");

            //search non-trashed content only
            results = await _client.SearchManager.SearchAsync(keyword, 200, trashContent: "non_trashed_only");
            Assert.AreEqual(13, results.Entries.Count, "Incorrect number of search results using non_trashed_only");
        }

        [TestMethod]
        public async Task SearchMetadata_LiveSession()
        {


            var filter = new
            {
                attr1 = "blah",
                attr2 = new { gt = 5, lt = 5 },
                attr3 = new { gt = new DateTime(2016, 10, 1), lt = new DateTime(2016, 11, 5) },
                attr4 = "value1"
            };

            var mdFilter = new BoxMetadataFilterRequest()
            {
                TemplateKey = "testtemplate",
                Scope = "enterprise",
                Filters = filter
            };

            var results = await _client.SearchManager.SearchAsync(mdFilters: new List<BoxMetadataFilterRequest>() { mdFilter });
            Assert.AreEqual(1, results.Entries.Count, "Incorrect number of search results using metadata search");
        }
    }
}
