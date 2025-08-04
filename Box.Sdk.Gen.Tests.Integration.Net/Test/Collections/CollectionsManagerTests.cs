using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class CollectionsManagerTests {
        public BoxClient client { get; }

        public CollectionsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCollections() {
            Collections collections = await client.Collections.GetCollectionsAsync();
            Collection favouriteCollection = await client.Collections.GetCollectionByIdAsync(collectionId: NullableUtils.Unwrap(NullableUtils.Unwrap(collections.Entries)[0].Id));
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(favouriteCollection.Type)) == "collection");
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(favouriteCollection.CollectionType)) == "favorites");
            ItemsOffsetPaginated collectionItems = await client.Collections.GetCollectionItemsAsync(collectionId: NullableUtils.Unwrap(favouriteCollection.Id));
            FolderFull folder = await client.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: Utils.GetUUID(), parent: new CreateFolderRequestBodyParentField(id: "0")));
            await client.Folders.UpdateFolderByIdAsync(folderId: folder.Id, requestBody: new UpdateFolderByIdRequestBody() { Collections = Array.AsReadOnly(new [] {new UpdateFolderByIdRequestBodyCollectionsField() { Id = favouriteCollection.Id }}) });
            ItemsOffsetPaginated collectionItemsAfterUpdate = await client.Collections.GetCollectionItemsAsync(collectionId: NullableUtils.Unwrap(favouriteCollection.Id));
            Assert.IsTrue(NullableUtils.Unwrap(collectionItemsAfterUpdate.TotalCount) > 0);
            await client.Folders.UpdateFolderByIdAsync(folderId: folder.Id, requestBody: new UpdateFolderByIdRequestBody() { Collections = Enumerable.Empty<UpdateFolderByIdRequestBodyCollectionsField>().ToList() });
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

    }
}