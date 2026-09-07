using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class ChunkedUploadsManagerTests {
        public BoxClient client { get; }

        public ChunkedUploadsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        internal async System.Threading.Tasks.Task<TestPartAccumulator> ReducerByIdAsync(TestPartAccumulator acc, System.IO.Stream chunk) {
            int lastIndex = acc.LastIndex;
            IReadOnlyList<UploadPart> parts = acc.Parts;
            byte[] chunkBuffer = await Utils.ReadByteStreamAsync(byteStream: chunk);
            Hash hash = new Hash(algorithm: HashName.Sha1);
            hash.UpdateHash(data: chunkBuffer);
            string sha1 = await hash.DigestHashAsync(encoding: "base64");
            string digest = string.Concat("sha=", sha1);
            int chunkSize = Utils.BufferLength(buffer: chunkBuffer);
            int bytesStart = lastIndex + 1;
            int bytesEnd = lastIndex + chunkSize;
            string contentRange = string.Concat("bytes ", NullableUtils.Unwrap(StringUtils.ToStringRepresentation(bytesStart)), "-", NullableUtils.Unwrap(StringUtils.ToStringRepresentation(bytesEnd)), "/", NullableUtils.Unwrap(StringUtils.ToStringRepresentation(acc.FileSize)));
            UploadedPart uploadedPart = await client.ChunkedUploads.UploadFilePartAsync(uploadSessionId: acc.UploadSessionId, requestBody: Utils.GenerateByteStreamFromBuffer(buffer: chunkBuffer), headers: new UploadFilePartHeaders(digest: digest, contentRange: contentRange));
            UploadPart part = NullableUtils.Unwrap(uploadedPart.Part);
            string partSha1 = Utils.HexToBase64(value: NullableUtils.Unwrap(part.Sha1));
            Assert.IsTrue(partSha1 == sha1);
            Assert.IsTrue(NullableUtils.Unwrap(part.Size) == chunkSize);
            Assert.IsTrue(NullableUtils.Unwrap(part.Offset) == bytesStart);
            acc.FileHash.UpdateHash(data: chunkBuffer);
            return new TestPartAccumulator(lastIndex: bytesEnd, parts: parts.Concat(Array.AsReadOnly(new [] {part})).ToList(), fileSize: acc.FileSize, uploadSessionId: acc.UploadSessionId, fileHash: acc.FileHash);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestChunkedManualProcessById() {
            int fileSize = 20 * 1024 * 1024;
            System.IO.Stream fileByteStream = Utils.GenerateByteStream(size: fileSize);
            string fileName = Utils.GetUUID();
            const string parentFolderId = "0";
            UploadSession uploadSession = await client.ChunkedUploads.CreateFileUploadSessionAsync(requestBody: new CreateFileUploadSessionRequestBody(fileName: fileName, fileSize: (long)(fileSize), folderId: parentFolderId));
            string uploadSessionId = NullableUtils.Unwrap(uploadSession.Id);
            long partSize = NullableUtils.Unwrap(uploadSession.PartSize);
            int totalParts = NullableUtils.Unwrap(uploadSession.TotalParts);
            Assert.IsTrue(partSize * (long)(totalParts) >= fileSize);
            Assert.IsTrue(uploadSession.NumPartsProcessed == 0);
            Hash fileHash = new Hash(algorithm: HashName.Sha1);
            IEnumerable<System.IO.Stream> chunksIterator = Utils.IterateChunks(stream: fileByteStream, chunkSize: partSize, fileSize: (long)(fileSize));
            TestPartAccumulator results = await Utils.ReduceIteratorAsync(iterator: chunksIterator, reducer: ReducerByIdAsync, initialValue: new TestPartAccumulator(lastIndex: -1, parts: Enumerable.Empty<UploadPart>().ToList(), fileSize: (long)(fileSize), uploadSessionId: uploadSessionId, fileHash: fileHash));
            IReadOnlyList<UploadPart> parts = results.Parts;
            UploadParts processedSessionParts = await client.ChunkedUploads.GetFileUploadSessionPartsAsync(uploadSessionId: uploadSessionId);
            Assert.IsTrue(NullableUtils.Unwrap(processedSessionParts.TotalCount) == totalParts);
            UploadSession processedSession = await client.ChunkedUploads.GetFileUploadSessionByIdAsync(uploadSessionId: uploadSessionId);
            Assert.IsTrue(NullableUtils.Unwrap(processedSession.Id) == uploadSessionId);
            string sha1 = await fileHash.DigestHashAsync(encoding: "base64");
            string digest = string.Concat("sha=", sha1);
            Files? committedSession = await client.ChunkedUploads.CreateFileUploadSessionCommitAsync(uploadSessionId: uploadSessionId, requestBody: new CreateFileUploadSessionCommitRequestBody(parts: parts), headers: new CreateFileUploadSessionCommitHeaders(digest: digest));
            Assert.IsTrue(NullableUtils.Unwrap(NullableUtils.Unwrap(NullableUtils.Unwrap(committedSession).Entries)[0].Name) == fileName);
            await client.ChunkedUploads.DeleteFileUploadSessionByIdAsync(uploadSessionId: uploadSessionId);
        }

        internal async System.Threading.Tasks.Task<TestPartAccumulator> ReducerByUrlAsync(TestPartAccumulator acc, System.IO.Stream chunk) {
            int lastIndex = acc.LastIndex;
            IReadOnlyList<UploadPart> parts = acc.Parts;
            byte[] chunkBuffer = await Utils.ReadByteStreamAsync(byteStream: chunk);
            Hash hash = new Hash(algorithm: HashName.Sha1);
            hash.UpdateHash(data: chunkBuffer);
            string sha1 = await hash.DigestHashAsync(encoding: "base64");
            string digest = string.Concat("sha=", sha1);
            int chunkSize = Utils.BufferLength(buffer: chunkBuffer);
            int bytesStart = lastIndex + 1;
            int bytesEnd = lastIndex + chunkSize;
            string contentRange = string.Concat("bytes ", NullableUtils.Unwrap(StringUtils.ToStringRepresentation(bytesStart)), "-", NullableUtils.Unwrap(StringUtils.ToStringRepresentation(bytesEnd)), "/", NullableUtils.Unwrap(StringUtils.ToStringRepresentation(acc.FileSize)));
            UploadedPart uploadedPart = await client.ChunkedUploads.UploadFilePartByUrlAsync(url: acc.UploadPartUrl, requestBody: Utils.GenerateByteStreamFromBuffer(buffer: chunkBuffer), headers: new UploadFilePartByUrlHeaders(digest: digest, contentRange: contentRange));
            UploadPart part = NullableUtils.Unwrap(uploadedPart.Part);
            string partSha1 = Utils.HexToBase64(value: NullableUtils.Unwrap(part.Sha1));
            Assert.IsTrue(partSha1 == sha1);
            Assert.IsTrue(NullableUtils.Unwrap(part.Size) == chunkSize);
            Assert.IsTrue(NullableUtils.Unwrap(part.Offset) == bytesStart);
            acc.FileHash.UpdateHash(data: chunkBuffer);
            return new TestPartAccumulator(lastIndex: bytesEnd, parts: parts.Concat(Array.AsReadOnly(new [] {part})).ToList(), fileSize: acc.FileSize, uploadPartUrl: acc.UploadPartUrl, fileHash: acc.FileHash);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestChunkedManualProcessByUrl() {
            int fileSize = 20 * 1024 * 1024;
            System.IO.Stream fileByteStream = Utils.GenerateByteStream(size: fileSize);
            string fileName = Utils.GetUUID();
            const string parentFolderId = "0";
            UploadSession uploadSession = await client.ChunkedUploads.CreateFileUploadSessionAsync(requestBody: new CreateFileUploadSessionRequestBody(fileName: fileName, fileSize: (long)(fileSize), folderId: parentFolderId));
            string uploadPartUrl = NullableUtils.Unwrap(NullableUtils.Unwrap(uploadSession.SessionEndpoints).UploadPart);
            string commitUrl = NullableUtils.Unwrap(NullableUtils.Unwrap(uploadSession.SessionEndpoints).Commit);
            string listPartsUrl = NullableUtils.Unwrap(NullableUtils.Unwrap(uploadSession.SessionEndpoints).ListParts);
            string statusUrl = NullableUtils.Unwrap(NullableUtils.Unwrap(uploadSession.SessionEndpoints).Status);
            string abortUrl = NullableUtils.Unwrap(NullableUtils.Unwrap(uploadSession.SessionEndpoints).Abort);
            string uploadSessionId = NullableUtils.Unwrap(uploadSession.Id);
            long partSize = NullableUtils.Unwrap(uploadSession.PartSize);
            int totalParts = NullableUtils.Unwrap(uploadSession.TotalParts);
            Assert.IsTrue(partSize * (long)(totalParts) >= fileSize);
            Assert.IsTrue(uploadSession.NumPartsProcessed == 0);
            Hash fileHash = new Hash(algorithm: HashName.Sha1);
            IEnumerable<System.IO.Stream> chunksIterator = Utils.IterateChunks(stream: fileByteStream, chunkSize: partSize, fileSize: (long)(fileSize));
            TestPartAccumulator results = await Utils.ReduceIteratorAsync(iterator: chunksIterator, reducer: ReducerByUrlAsync, initialValue: new TestPartAccumulator(lastIndex: -1, parts: Enumerable.Empty<UploadPart>().ToList(), fileSize: (long)(fileSize), uploadPartUrl: uploadPartUrl, fileHash: fileHash));
            IReadOnlyList<UploadPart> parts = results.Parts;
            UploadParts processedSessionParts = await client.ChunkedUploads.GetFileUploadSessionPartsByUrlAsync(url: listPartsUrl);
            Assert.IsTrue(NullableUtils.Unwrap(processedSessionParts.TotalCount) == totalParts);
            UploadSession processedSession = await client.ChunkedUploads.GetFileUploadSessionByUrlAsync(url: statusUrl);
            Assert.IsTrue(NullableUtils.Unwrap(processedSession.Id) == uploadSessionId);
            string sha1 = await fileHash.DigestHashAsync(encoding: "base64");
            string digest = string.Concat("sha=", sha1);
            Files? committedSession = await client.ChunkedUploads.CreateFileUploadSessionCommitByUrlAsync(url: commitUrl, requestBody: new CreateFileUploadSessionCommitByUrlRequestBody(parts: parts), headers: new CreateFileUploadSessionCommitByUrlHeaders(digest: digest));
            Assert.IsTrue(NullableUtils.Unwrap(NullableUtils.Unwrap(NullableUtils.Unwrap(committedSession).Entries)[0].Name) == fileName);
            await client.ChunkedUploads.DeleteFileUploadSessionByUrlAsync(url: abortUrl);
        }

        internal async System.Threading.Tasks.Task<TestPartPlanAccumulator> ReducerForUploadSessionPlanAsync(TestPartPlanAccumulator acc, System.IO.Stream chunk) {
            int lastIndex = acc.LastIndex;
            IReadOnlyList<UploadPartPlan> parts = acc.Parts;
            byte[] chunkBuffer = await Utils.ReadByteStreamAsync(byteStream: chunk);
            Hash hash = new Hash(algorithm: HashName.Sha512);
            hash.UpdateHash(data: chunkBuffer);
            string sha512 = await hash.DigestHashAsync(encoding: "hex");
            int chunkSize = Utils.BufferLength(buffer: chunkBuffer);
            int bytesStart = lastIndex + 1;
            int bytesEnd = lastIndex + chunkSize;
            UploadPartPlan part = new UploadPartPlan(offset: (long)(bytesStart), size: (long)(chunkSize), sha512: sha512);
            return new TestPartPlanAccumulator(lastIndex: bytesEnd, parts: parts.Concat(Array.AsReadOnly(new [] {part})).ToList(), fileSize: acc.FileSize);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestUploadSessionPlan() {
            int fileSize = 20 * 1024 * 1024;
            string fileName = Utils.GetUUID();
            const string parentFolderId = "0";
            System.IO.Stream fileContentStream = Utils.GenerateByteStream(size: fileSize);
            byte[] fileBuffer = await Utils.ReadByteStreamAsync(byteStream: fileContentStream);
            File uploadedFile = await client.ChunkedUploads.UploadBigFileAsync(file: Utils.GenerateByteStreamFromBuffer(buffer: fileBuffer), fileName: fileName, fileSize: (long)(fileSize), parentFolderId: parentFolderId);
            await Utils.DelayInSecondsAsync(seconds: 5);
            UploadSession uploadSession = await client.ChunkedUploads.CreateFileUploadSessionForExistingFileAsync(fileId: uploadedFile.Id, requestBody: new CreateFileUploadSessionForExistingFileRequestBody(fileSize: (long)(fileSize)));
            string uploadSessionId = NullableUtils.Unwrap(uploadSession.Id);
            string planUrl = NullableUtils.Unwrap(NullableUtils.Unwrap(uploadSession.SessionEndpoints).Plan);
            long partSize = NullableUtils.Unwrap(uploadSession.PartSize);
            int totalParts = NullableUtils.Unwrap(uploadSession.TotalParts);
            IEnumerable<System.IO.Stream> chunksIterator = Utils.IterateChunks(stream: Utils.GenerateByteStreamFromBuffer(buffer: fileBuffer), chunkSize: partSize, fileSize: (long)(fileSize));
            TestPartPlanAccumulator results = await Utils.ReduceIteratorAsync(iterator: chunksIterator, reducer: ReducerForUploadSessionPlanAsync, initialValue: new TestPartPlanAccumulator(lastIndex: -1, parts: Enumerable.Empty<UploadPartPlan>().ToList(), fileSize: (long)(fileSize)));
            IReadOnlyList<UploadPartPlan> parts = results.Parts;
            UploadSessionPlanResponse plan = await client.ChunkedUploads.CreateFileUploadSessionPlanByUrlAsync(url: planUrl, requestBody: new UploadSessionPlanRequest(parts: parts));
            Assert.IsTrue(plan.UploadSessionId == uploadSessionId);
            Assert.IsTrue(plan.Hits.Count == totalParts);
            Assert.IsTrue(plan.Misses.Count == 0);
            UploadPartPlan firstPart = parts.ElementAt(0);
            UploadPartPlanHit firstHit = plan.Hits.ElementAt(0);
            Assert.IsTrue(firstHit.Offset == firstPart.Offset);
            Assert.IsTrue(firstHit.Size == firstPart.Size);
            Assert.IsTrue(firstHit.Sha512 == firstPart.Sha512);
            Assert.IsTrue(firstHit.PartId != "");
            await client.ChunkedUploads.DeleteFileUploadSessionByIdAsync(uploadSessionId: uploadSessionId);
            await client.Files.DeleteFileByIdAsync(fileId: uploadedFile.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestChunkedUploadConvenienceMethod() {
            int fileSize = 20 * 1024 * 1024;
            System.IO.Stream fileByteStream = Utils.GenerateByteStream(size: fileSize);
            string fileName = Utils.GetUUID();
            const string parentFolderId = "0";
            File uploadedFile = await client.ChunkedUploads.UploadBigFileAsync(file: fileByteStream, fileName: fileName, fileSize: (long)(fileSize), parentFolderId: parentFolderId);
            Assert.IsTrue(NullableUtils.Unwrap(uploadedFile.Name) == fileName);
            Assert.IsTrue(NullableUtils.Unwrap(uploadedFile.Size) == fileSize);
            Assert.IsTrue(NullableUtils.Unwrap(uploadedFile.Parent).Id == parentFolderId);
            await client.Files.DeleteFileByIdAsync(fileId: uploadedFile.Id);
        }

    }
}