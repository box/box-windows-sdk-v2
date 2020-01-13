Collections
===========

Collections are used to store a custom user-defined set of items that may not
all be in the same folder.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Get a User's Collections](#get-a-users-collections)
- [Get the Items in a Collection](#get-the-items-in-a-collection)
- [Set an Item's Collections](#set-an-items-collections)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Get a User's Collections
------------------------

Get a list of all collections the user has defined by calling `CollectionsManager.GetCollectionsAsync()`.
A user always has a default collection called "Favorites" which they can add items to.

<!-- sample get_collections -->
```c#
BoxCollection<BoxCollectionItem> collections = await client.CollectionsManager.GetCollectionsAsync();
```

Get the Items in a Collection
-----------------------------

Get a list of the items in a collection by passing the ID of the collection to
`CollectionsManager.GetCollectionItemsAsync(string collectionId, int limit = 100, int offset = 0, IEnumerable<string> fields = null, bool autoPaginate = false)`.

<!-- sample get_collections_id_items -->
```c#
BoxCollection<BoxItem> items = await client.CollectionsManager.GetCollectionItemsAsync(id: "11111");
```

Set an Item's Collections
-------------------------

You can set the collections an item belongs to by calling
`CollectionsManager.CreateOrDeleteCollectionsForFolderAsync(string folderId, BoxCollectionsRequest collectionsRequest)`
or `CollectionsManager.CreateOrDeleteCollectionsForFileAsync(string fileId, BoxCollectionsRequest collectionsRequest)`.

<!-- sample put_files_id add_to_collection -->
```c#
// Put file 11111 into collection 22222
BoxCollectionsRequest requestParams = new BoxCollectionsRequest()
{
    Collections = new List<BoxRequestEntity>()
    {
        new BoxRequestEntity()
        {
            Id = "22222"
        }
    };
};
BoxFile file = await client.CollectionsManager.CreateOrDeleteCollectionsForFileAsync(fileId: "11111", requestParams);
```
