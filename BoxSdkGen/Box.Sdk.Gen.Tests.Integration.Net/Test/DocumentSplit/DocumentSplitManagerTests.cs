using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class DocumentSplitManagerTests {
        public BoxClient client { get; }

        public DocumentSplitManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateDocumentSplitV2026R0() {
            FileFull file = await new CommonsManager().UploadNewFileAsync();
            FolderFull folder = await new CommonsManager().CreateNewFolderAsync();
            string externalId = Utils.GetUUID();
            DocumentSplitCreateResponseV2026R0 createdDocumentSplit = await client.DocumentSplit.CreateDocumentSplitV2026R0Async(requestBody: new DocumentSplitCreateRequestV2026R0(sourceFileId: file.Id, destinationFolderId: folder.Id, inputSource: DocumentSplitCreateRequestV2026R0InputSourceField.Api, splitInput: new DocumentSplitCreateRequestManualV2026R0(splitType: DocumentSplitCreateRequestManualV2026R0SplitTypeField.Manual, splitDefinition: Array.AsReadOnly(new [] {new DocumentSplitPageRangeV2026R0(start: 1, end: 1)}))) { ExternalId = externalId });
            Assert.IsTrue(createdDocumentSplit.Id != "");
            Assert.IsTrue(StringUtils.ToStringRepresentation(createdDocumentSplit.Type?.Value) == "document_split");
            DocumentSplitV2026R0 documentSplit = await client.DocumentSplit.GetDocumentSplitByIdV2026R0Async(documentSplitId: createdDocumentSplit.Id);
            Assert.IsTrue(documentSplit.Id == createdDocumentSplit.Id);
            Assert.IsTrue(StringUtils.ToStringRepresentation(documentSplit.Type?.Value) == "document_split");
            Assert.IsTrue(documentSplit.SourceFile.Id == file.Id);
            Assert.IsTrue(documentSplit.DestinationFolder.Id == folder.Id);
            Assert.IsTrue(StringUtils.ToStringRepresentation(documentSplit.InputSource) == "api");
            Assert.IsTrue(StringUtils.ToStringRepresentation(documentSplit.Status?.Value) != "");
            Assert.IsTrue(documentSplit.ExternalId == externalId);
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id, queryParams: new DeleteFolderByIdQueryParams() { Recursive = true });
            await client.Files.DeleteFileByIdAsync(fileId: file.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetDocumentSplitsV2026R0() {
            DocumentSplitsV2026R0 documentSplits = await client.DocumentSplit.GetDocumentSplitsV2026R0Async(queryParams: new GetDocumentSplitsV2026R0QueryParams() { Limit = 10L });
            Assert.IsTrue(NullableUtils.Unwrap(documentSplits.Entries).Count >= 0);
        }

    }
}