Search
======

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Search for Content](#search-for-content)
    - [Search for Content with Shared Link Items](#search-for-content-with-shared-link-items)
    - [Metadata Search](#metadata-search)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Search for Content
------------------

To get a list of items matching a search query, call the
```
SearchManager.QueryAsync(string query,
    string scope = null,
    IEnumerable<string> fileExtensions = null,
    DateTime? createdAfter = null,
    DateTime? createdBefore = null,
    DateTime? updatedAfter = null,
    DateTime? updatedBefore = null,
    long? sizeLowerBound = null,
    long? sizeUpperBound = null,
    IEnumerable<string> ownerUserIds = null,
    IEnumerable<string> ancestorFolderIds = null,
    IEnumerable<string> contentTypes = null,
    string type = null,
    string trashContent = null,
    List<BoxMetadataFilterRequest> mdFilters = null,
    int limit = 30,
    int offset = 0,
    IEnumerable<string> fields = null,
    string sort = null,
    BoxSortDirection? direction = null)
```
method.  There are many possible options for advanced search filtering, which are
documented in the [Search API Reference](https://developer.box.com/en/guides/search/).
For most types of searches, a query string is required; for example, it is not possible to
search for all files created after a certain date through the Search API.

<!-- sample get_search -->
```c#
// Search for PDF or Word documents matching "Meeting Notes"
BoxCollection<BoxItem> results = await client.SearchManager
    .QueryAsync("Meeting Notes", fileExtensions: new { "pdf", "docx" });
```

## Search for Content with Shared Link Items

To get a list of items matching a search query, including items that a user might have accessed recently through a shared link, call the
```
SearchManager.QueryAsyncWithSharedLinks(string query,
    string scope = null,
    IEnumerable<string> fileExtensions = null,
    DateTime? createdAfter = null,
    DateTime? createdBefore = null,
    DateTime? updatedAfter = null,
    DateTime? updatedBefore = null,
    long? sizeLowerBound = null,
    long? sizeUpperBound = null,
    IEnumerable<string> ownerUserIds = null,
    IEnumerable<string> ancestorFolderIds = null,
    IEnumerable<string> contentTypes = null,
    string type = null,
    string trashContent = null,
    List<BoxMetadataFilterRequest> mdFilters = null,
    int limit = 30,
    int offset = 0,
    IEnumerable<string> fields = null,
    string sort = null,
    BoxSortDirection? direction = null)
```
method.  There are many possible options for advanced search filtering, which are
documented in the [Search API Reference](https://developer.box.com/en/guides/search/).
For most types of searches, a query string is required.

```c#
// Search for PDF or Word documents matching "Meeting Notes"
BoxCollection<BoxSearchResult> results = await client.SearchManager
    .QueryAsyncWithSharedLinks("Meeting Notes", fileExtensions: new { "pdf", "docx" });
```

### Metadata Search

When searching on metadata values, a search query string is not required and
can be set to `null`.

```c#
// Search for all Powerpoint presentations with the TopSecret metadata applied
var metadataFilters = new List<BoxMetadataFilterRequest>()
{
    new BoxMetadataFilterRequest()
    {
        TemplateKey = "TopSecret",
        Scope = "enterprise"
    }
};
BoxCollection<BoxItem> results = await client.SearchManager
    .QueryAsync(null, fileExtensions: new { "pptx" }, mdFilters: metadataFilters);
```
