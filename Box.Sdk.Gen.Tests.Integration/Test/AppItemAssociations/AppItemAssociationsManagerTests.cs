using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class AppItemAssociationsManagerTests {
        [RetryableTest]
        public async System.Threading.Tasks.Task TestListFileAppItemAssocations() {
            BoxClient client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
            string fileId = Utils.GetEnvVar(name: "APP_ITEM_ASSOCIATION_FILE_ID");
            AppItemAssociations fileAppItemAssociations = await client.AppItemAssociations.GetFileAppItemAssociationsAsync(fileId: fileId);
            Assert.IsTrue(NullableUtils.Unwrap(fileAppItemAssociations.Entries).Count == 1);
            AppItemAssociation association = NullableUtils.Unwrap(fileAppItemAssociations.Entries)[0];
            Assert.IsTrue(association.Id != "");
            Assert.IsTrue(StringUtils.ToStringRepresentation(association.AppItem.ApplicationType) == "hubs");
            Assert.IsTrue(StringUtils.ToStringRepresentation(association.AppItem.Type?.Value) == "app_item");
            FileFull file = await client.Files.GetFileByIdAsync(fileId: fileId, queryParams: new GetFileByIdQueryParams() { Fields = Array.AsReadOnly(new [] {"is_associated_with_app_item"}) });
            Assert.IsTrue(NullableUtils.Unwrap(file.IsAssociatedWithAppItem) == true);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestListFolderAppItemAssocations() {
            BoxClient client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
            string folderId = Utils.GetEnvVar(name: "APP_ITEM_ASSOCIATION_FOLDER_ID");
            AppItemAssociations folderAppItemAssociations = await client.AppItemAssociations.GetFolderAppItemAssociationsAsync(folderId: folderId);
            Assert.IsTrue(NullableUtils.Unwrap(folderAppItemAssociations.Entries).Count == 1);
            AppItemAssociation association = NullableUtils.Unwrap(folderAppItemAssociations.Entries)[0];
            Assert.IsTrue(association.Id != "");
            Assert.IsTrue(StringUtils.ToStringRepresentation(association.AppItem.ApplicationType) == "hubs");
            Assert.IsTrue(StringUtils.ToStringRepresentation(association.AppItem.Type?.Value) == "app_item");
            FolderFull folder = await client.Folders.GetFolderByIdAsync(folderId: folderId, queryParams: new GetFolderByIdQueryParams() { Fields = Array.AsReadOnly(new [] {"is_associated_with_app_item"}) });
            Assert.IsTrue(NullableUtils.Unwrap(folder.IsAssociatedWithAppItem) == true);
        }

    }
}