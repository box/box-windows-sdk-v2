Search
======

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Search for Content](#search-for-content)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Search for Content
------------------

To get a list of items matching a search query, call the
```
SearchManager.SearchAsync(string keyword = null,
    int limit = 30,
    int offset = 0,
    IEnumerable<string> fields = null,
    string scope = null,
    IEnumerable<string> fileExtensions = null,
    DateTime? createdAtRangeFromDate = null,
    DateTime? createdAtRangeToDate = null,
    DateTime? updatedAtRangeFromDate = null,
    DateTime? updatedAtRangeToDate = null,
    int? sizeRangeLowerBoundBytes = null,
    int? sizeRangeUpperBoundBytes = null,
    IEnumerable<string> ownerUserIds = null,
    IEnumerable<string> ancestorFolderIds = null,
    IEnumerable<string> contentTypes = null,
    string type = null,
    string trashContent = null,
    List<BoxMetadataFilterRequest> mdFilters = null)
```
method.  There are many possible options for advanced search filtering, which are
documented in the [Search API Reference](https://docs.box.com/reference#searching-for-content).

```c#
// Search for PDF or Word documents matching "Meeting Notes"
BoxCollection<BoxItem> results = await client.SearchManager
    .SearchAsync("Meeting Notes", fileExtensions: new { "pdf", "docx" });
```

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
    .SearchAsync(fileExtensions: new { "pptx" }, mdFilters: metadataFilters);
```
