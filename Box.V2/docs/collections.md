Collections
===========

Collections are used to store a custom user-defined set of items that may not
all be in the same folder.

Get a User's Collections
------------------------

Get a list of all collections the user has defined by calling `CollectionsManager.GetCollectionsAsync()`.
A user always has a default collection called "Favorites" which they can add items to.

```c#
BoxCollection<BoxCollectionItem> collections = await client.CollectionsManager.GetCollectionsAsync();
```

Get the Items in a Collection
-----------------------------

Get a list of the items in a collection by passing the ID of the collection to
`CollectionsManager.GetCollectionItemsAsync(string collectionId, int limit = 100, int offset = 0, IEnumerable<string> fields = null, bool autoPaginate = false)`.

```c#
BoxCollection<BoxItem> items = await client.CollectionsManager.GetCollectionItemsAsync(id: "11111");
```

Set an Item's Collections
-------------------------

You can set the collections an item belongs to by calling
`CollectionsManager.CreateOrDeleteCollectionsForFolderAsync(string folderId, BoxCollectionsRequest collectionsRequest)`
or `CollectionsManager.CreateOrDeleteCollectionsForFileAsync(string fileId, BoxCollectionsRequest collectionsRequest)`.

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
