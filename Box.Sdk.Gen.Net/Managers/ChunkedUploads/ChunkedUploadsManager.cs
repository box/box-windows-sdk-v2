using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class ChunkedUploadsManager : IChunkedUploadsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public ChunkedUploadsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Creates an upload session for a new file.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createFileUploadSession method
        /// </param>
        /// <param name="headers">
        /// Headers of createFileUploadSession method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<UploadSession> CreateFileUploadSessionAsync(CreateFileUploadSessionRequestBody requestBody, CreateFileUploadSessionHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateFileUploadSessionHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.UploadUrl, "/2.0/files/upload_sessions"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<UploadSession>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates an upload session for an existing file.
        /// </summary>
        /// <param name="fileId">
        /// The unique identifier that represents a file.
        /// 
        /// The ID for any file can be determined
        /// by visiting a file in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/files/123`
        /// the `file_id` is `123`.
        /// Example: "12345"
        /// </param>
        /// <param name="requestBody">
        /// Request body of createFileUploadSessionForExistingFile method
        /// </param>
        /// <param name="headers">
        /// Headers of createFileUploadSessionForExistingFile method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<UploadSession> CreateFileUploadSessionForExistingFileAsync(string fileId, CreateFileUploadSessionForExistingFileRequestBody requestBody, CreateFileUploadSessionForExistingFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateFileUploadSessionForExistingFileHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.UploadUrl, "/2.0/files/", StringUtils.ToStringRepresentation(fileId), "/upload_sessions"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<UploadSession>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Using this method with urls provided in response when creating a new upload session is preferred to use over GetFileUploadSessionById method. 
        /// This allows to always upload your content to the closest Box data center and can significantly improve upload speed.
        ///  Return information about an upload session.
        /// 
        /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions) endpoint.
        /// </summary>
        /// <param name="url">
        /// URL of getFileUploadSessionById method
        /// </param>
        /// <param name="headers">
        /// Headers of getFileUploadSessionById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<UploadSession> GetFileUploadSessionByUrlAsync(string url, GetFileUploadSessionByUrlHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetFileUploadSessionByUrlHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: url, method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<UploadSession>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Return information about an upload session.
        /// 
        /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions) endpoint.
        /// </summary>
        /// <param name="uploadSessionId">
        /// The ID of the upload session.
        /// Example: "D5E3F7A"
        /// </param>
        /// <param name="headers">
        /// Headers of getFileUploadSessionById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<UploadSession> GetFileUploadSessionByIdAsync(string uploadSessionId, GetFileUploadSessionByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetFileUploadSessionByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.UploadUrl, "/2.0/files/upload_sessions/", StringUtils.ToStringRepresentation(uploadSessionId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<UploadSession>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Using this method with urls provided in response when creating a new upload session is preferred to use over UploadFilePart method. 
        /// This allows to always upload your content to the closest Box data center and can significantly improve upload speed.
        ///  Uploads a chunk of a file for an upload session.
        /// 
        /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
        /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
        /// </summary>
        /// <param name="url">
        /// URL of uploadFilePart method
        /// </param>
        /// <param name="requestBody">
        /// Request body of uploadFilePart method
        /// </param>
        /// <param name="headers">
        /// Headers of uploadFilePart method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<UploadedPart> UploadFilePartByUrlAsync(string url, System.IO.Stream requestBody, UploadFilePartByUrlHeaders headers, System.Threading.CancellationToken? cancellationToken = null) {
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "digest", StringUtils.ToStringRepresentation(headers.Digest) }, { "content-range", StringUtils.ToStringRepresentation(headers.ContentRange) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: url, method: "PUT", contentType: "application/octet-stream", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, FileStream = requestBody, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<UploadedPart>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Uploads a chunk of a file for an upload session.
        /// 
        /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
        /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
        /// </summary>
        /// <param name="uploadSessionId">
        /// The ID of the upload session.
        /// Example: "D5E3F7A"
        /// </param>
        /// <param name="requestBody">
        /// Request body of uploadFilePart method
        /// </param>
        /// <param name="headers">
        /// Headers of uploadFilePart method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<UploadedPart> UploadFilePartAsync(string uploadSessionId, System.IO.Stream requestBody, UploadFilePartHeaders headers, System.Threading.CancellationToken? cancellationToken = null) {
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "digest", StringUtils.ToStringRepresentation(headers.Digest) }, { "content-range", StringUtils.ToStringRepresentation(headers.ContentRange) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.UploadUrl, "/2.0/files/upload_sessions/", StringUtils.ToStringRepresentation(uploadSessionId)), method: "PUT", contentType: "application/octet-stream", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, FileStream = requestBody, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<UploadedPart>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Using this method with urls provided in response when creating a new upload session is preferred to use over DeleteFileUploadSessionById method. 
        /// This allows to always upload your content to the closest Box data center and can significantly improve upload speed.
        ///  Abort an upload session and discard all data uploaded.
        /// 
        /// This cannot be reversed.
        /// 
        /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
        /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
        /// </summary>
        /// <param name="url">
        /// URL of deleteFileUploadSessionById method
        /// </param>
        /// <param name="headers">
        /// Headers of deleteFileUploadSessionById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteFileUploadSessionByUrlAsync(string url, DeleteFileUploadSessionByUrlHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteFileUploadSessionByUrlHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: url, method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Abort an upload session and discard all data uploaded.
        /// 
        /// This cannot be reversed.
        /// 
        /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
        /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
        /// </summary>
        /// <param name="uploadSessionId">
        /// The ID of the upload session.
        /// Example: "D5E3F7A"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteFileUploadSessionById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteFileUploadSessionByIdAsync(string uploadSessionId, DeleteFileUploadSessionByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteFileUploadSessionByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.UploadUrl, "/2.0/files/upload_sessions/", StringUtils.ToStringRepresentation(uploadSessionId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Using this method with urls provided in response when creating a new upload session is preferred to use over GetFileUploadSessionParts method. 
        /// This allows to always upload your content to the closest Box data center and can significantly improve upload speed.
        ///  Return a list of the chunks uploaded to the upload session so far.
        /// 
        /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
        /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
        /// </summary>
        /// <param name="url">
        /// URL of getFileUploadSessionParts method
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getFileUploadSessionParts method
        /// </param>
        /// <param name="headers">
        /// Headers of getFileUploadSessionParts method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<UploadParts> GetFileUploadSessionPartsByUrlAsync(string url, GetFileUploadSessionPartsByUrlQueryParams? queryParams = default, GetFileUploadSessionPartsByUrlHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetFileUploadSessionPartsByUrlQueryParams();
            headers = headers ?? new GetFileUploadSessionPartsByUrlHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "offset", StringUtils.ToStringRepresentation(queryParams.Offset) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: url, method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<UploadParts>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Return a list of the chunks uploaded to the upload session so far.
        /// 
        /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
        /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
        /// </summary>
        /// <param name="uploadSessionId">
        /// The ID of the upload session.
        /// Example: "D5E3F7A"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getFileUploadSessionParts method
        /// </param>
        /// <param name="headers">
        /// Headers of getFileUploadSessionParts method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<UploadParts> GetFileUploadSessionPartsAsync(string uploadSessionId, GetFileUploadSessionPartsQueryParams? queryParams = default, GetFileUploadSessionPartsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetFileUploadSessionPartsQueryParams();
            headers = headers ?? new GetFileUploadSessionPartsHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "offset", StringUtils.ToStringRepresentation(queryParams.Offset) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.UploadUrl, "/2.0/files/upload_sessions/", StringUtils.ToStringRepresentation(uploadSessionId), "/parts"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<UploadParts>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Using this method with urls provided in response when creating a new upload session is preferred to use over CreateFileUploadSessionCommit method. 
        /// This allows to always upload your content to the closest Box data center and can significantly improve upload speed.
        ///  Close an upload session and create a file from the uploaded chunks.
        /// 
        /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
        /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
        /// </summary>
        /// <param name="url">
        /// URL of createFileUploadSessionCommit method
        /// </param>
        /// <param name="requestBody">
        /// Request body of createFileUploadSessionCommit method
        /// </param>
        /// <param name="headers">
        /// Headers of createFileUploadSessionCommit method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<Files?> CreateFileUploadSessionCommitByUrlAsync(string url, CreateFileUploadSessionCommitByUrlRequestBody requestBody, CreateFileUploadSessionCommitByUrlHeaders headers, System.Threading.CancellationToken? cancellationToken = null) {
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "digest", StringUtils.ToStringRepresentation(headers.Digest) }, { "if-match", StringUtils.ToStringRepresentation(headers.IfMatch) }, { "if-none-match", StringUtils.ToStringRepresentation(headers.IfNoneMatch) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: url, method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            if (StringUtils.ToStringRepresentation(response.Status) == "202") {
                return null;
            }
            return SimpleJsonSerializer.Deserialize<Files>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Close an upload session and create a file from the uploaded chunks.
        /// 
        /// The actual endpoint URL is returned by the [`Create upload session`](e://post-files-upload-sessions)
        /// and [`Get upload session`](e://get-files-upload-sessions-id) endpoints.
        /// </summary>
        /// <param name="uploadSessionId">
        /// The ID of the upload session.
        /// Example: "D5E3F7A"
        /// </param>
        /// <param name="requestBody">
        /// Request body of createFileUploadSessionCommit method
        /// </param>
        /// <param name="headers">
        /// Headers of createFileUploadSessionCommit method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<Files?> CreateFileUploadSessionCommitAsync(string uploadSessionId, CreateFileUploadSessionCommitRequestBody requestBody, CreateFileUploadSessionCommitHeaders headers, System.Threading.CancellationToken? cancellationToken = null) {
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "digest", StringUtils.ToStringRepresentation(headers.Digest) }, { "if-match", StringUtils.ToStringRepresentation(headers.IfMatch) }, { "if-none-match", StringUtils.ToStringRepresentation(headers.IfNoneMatch) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.UploadUrl, "/2.0/files/upload_sessions/", StringUtils.ToStringRepresentation(uploadSessionId), "/commit"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            if (StringUtils.ToStringRepresentation(response.Status) == "202") {
                return null;
            }
            return SimpleJsonSerializer.Deserialize<Files>(NullableUtils.Unwrap(response.Data));
        }

        internal async System.Threading.Tasks.Task<PartAccumulator> ReducerAsync(PartAccumulator acc, System.IO.Stream chunk) {
            long lastIndex = acc.LastIndex;
            IReadOnlyList<UploadPart> parts = acc.Parts;
            byte[] chunkBuffer = await Utils.ReadByteStreamAsync(byteStream: chunk).ConfigureAwait(false);
            Hash hash = new Hash(algorithm: HashName.Sha1);
            hash.UpdateHash(data: chunkBuffer);
            string sha1 = await hash.DigestHashAsync(encoding: "base64").ConfigureAwait(false);
            string digest = string.Concat("sha=", sha1);
            int chunkSize = Utils.BufferLength(buffer: chunkBuffer);
            long bytesStart = lastIndex + 1;
            long bytesEnd = lastIndex + chunkSize;
            string contentRange = string.Concat("bytes ", NullableUtils.Unwrap(StringUtils.ToStringRepresentation(bytesStart)), "-", NullableUtils.Unwrap(StringUtils.ToStringRepresentation(bytesEnd)), "/", NullableUtils.Unwrap(StringUtils.ToStringRepresentation(acc.FileSize)));
            UploadedPart uploadedPart = await this.UploadFilePartByUrlAsync(url: acc.UploadPartUrl, requestBody: Utils.GenerateByteStreamFromBuffer(buffer: chunkBuffer), headers: new UploadFilePartByUrlHeaders(digest: digest, contentRange: contentRange)).ConfigureAwait(false);
            UploadPart part = NullableUtils.Unwrap(uploadedPart.Part);
            string partSha1 = Utils.HexToBase64(value: NullableUtils.Unwrap(part.Sha1));
            if (!(partSha1 == sha1)) {
                throw new Exception(message: "Assertion failed");
            }
            if (!(NullableUtils.Unwrap(part.Size) == chunkSize)) {
                throw new Exception(message: "Assertion failed");
            }
            if (!(NullableUtils.Unwrap(part.Offset) == bytesStart)) {
                throw new Exception(message: "Assertion failed");
            }
            acc.FileHash.UpdateHash(data: chunkBuffer);
            return new PartAccumulator(lastIndex: bytesEnd, parts: parts.Concat(Array.AsReadOnly(new [] {part})).ToList(), fileSize: acc.FileSize, uploadPartUrl: acc.UploadPartUrl, fileHash: acc.FileHash);
        }

        /// <summary>
        /// Starts the process of chunk uploading a big file. Should return a File object representing uploaded file.
        /// </summary>
        /// <param name="file">
        /// The stream of the file to upload.
        /// </param>
        /// <param name="fileName">
        /// The name of the file, which will be used for storage in Box.
        /// </param>
        /// <param name="fileSize">
        /// The total size of the file for the chunked upload in bytes.
        /// </param>
        /// <param name="parentFolderId">
        /// The ID of the folder where the file should be uploaded.
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FileFull> UploadBigFileAsync(System.IO.Stream file, string fileName, long fileSize, string parentFolderId, System.Threading.CancellationToken? cancellationToken = null) {
            UploadSession uploadSession = await this.CreateFileUploadSessionAsync(requestBody: new CreateFileUploadSessionRequestBody(fileName: fileName, fileSize: fileSize, folderId: parentFolderId), headers: new CreateFileUploadSessionHeaders(), cancellationToken: cancellationToken).ConfigureAwait(false);
            string uploadPartUrl = NullableUtils.Unwrap(NullableUtils.Unwrap(uploadSession.SessionEndpoints).UploadPart);
            string commitUrl = NullableUtils.Unwrap(NullableUtils.Unwrap(uploadSession.SessionEndpoints).Commit);
            string listPartsUrl = NullableUtils.Unwrap(NullableUtils.Unwrap(uploadSession.SessionEndpoints).ListParts);
            long partSize = NullableUtils.Unwrap(uploadSession.PartSize);
            int totalParts = NullableUtils.Unwrap(uploadSession.TotalParts);
            if (!(partSize * totalParts >= fileSize)) {
                throw new Exception(message: "Assertion failed");
            }
            if (!(uploadSession.NumPartsProcessed == 0)) {
                throw new Exception(message: "Assertion failed");
            }
            Hash fileHash = new Hash(algorithm: HashName.Sha1);
            IEnumerable<System.IO.Stream> chunksIterator = Utils.IterateChunks(stream: file, chunkSize: partSize, fileSize: fileSize);
            PartAccumulator results = await Utils.ReduceIteratorAsync(iterator: chunksIterator, reducer: this.ReducerAsync, initialValue: new PartAccumulator(lastIndex: -1, parts: Enumerable.Empty<UploadPart>().ToList(), fileSize: fileSize, uploadPartUrl: uploadPartUrl, fileHash: fileHash)).ConfigureAwait(false);
            IReadOnlyList<UploadPart> parts = results.Parts;
            UploadParts processedSessionParts = await this.GetFileUploadSessionPartsByUrlAsync(url: listPartsUrl, queryParams: new GetFileUploadSessionPartsByUrlQueryParams(), headers: new GetFileUploadSessionPartsByUrlHeaders(), cancellationToken: cancellationToken).ConfigureAwait(false);
            if (!(NullableUtils.Unwrap(processedSessionParts.TotalCount) == totalParts)) {
                throw new Exception(message: "Assertion failed");
            }
            string sha1 = await fileHash.DigestHashAsync(encoding: "base64").ConfigureAwait(false);
            string digest = string.Concat("sha=", sha1);
            Files? committedSession = await this.CreateFileUploadSessionCommitByUrlAsync(url: commitUrl, requestBody: new CreateFileUploadSessionCommitByUrlRequestBody(parts: parts), headers: new CreateFileUploadSessionCommitByUrlHeaders(digest: digest), cancellationToken: cancellationToken).ConfigureAwait(false);
            return NullableUtils.Unwrap(NullableUtils.Unwrap(committedSession).Entries)[0];
        }

    }
}