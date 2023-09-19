using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Exceptions;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Request;
using Box.V2.Services;
using Box.V2.Utility;
using Newtonsoft.Json.Linq;

namespace Box.V2.Managers
{
    /// <summary>
    /// File objects represent that metadata about individual files in Box, with attributes describing who created the file, 
    /// when it was last modified, and other information. 
    /// </summary>
    public class BoxFilesManager : BoxResourceManager, IBoxFilesManager
    {
        public BoxFilesManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Retrieves information about a file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="sharedLink">The shared link for this file</param>
        /// <param name="sharedLinkPassword">The password for the shared link (if required)</param>
        /// <returns>A full file object is returned if the ID is valid and if the user has access to the file.</returns>
        public async Task<BoxFile> GetInformationAsync(string id, IEnumerable<string> fields = null, string sharedLink = null, string sharedLinkPassword = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Param(ParamFields, fields);

            if (!string.IsNullOrEmpty(sharedLink))
            {
                var sharedLinkHeader = SharedLinkUtils.GetSharedLinkHeader(sharedLink, sharedLinkPassword);
                request.Header(sharedLinkHeader.Item1, sharedLinkHeader.Item2);
            }

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Returns the stream of the requested file.
        /// </summary>
        /// <param name="id">Id of the file to download.</param>
        /// <param name="versionId">The ID specific version of this file to download.</param>
        /// <param name="timeout">Optional timeout for response.</param>
        /// <param name="startOffsetInBytes">Starting byte of the chunk to download.</param>
        /// <param name="endOffsetInBytes">Ending byte of the chunk to download.</param>
        /// <param name="sharedLink">The shared link for this file</param>
        /// <param name="sharedLinkPassword">The password for the shared link (if required)</param>
        /// <returns>Stream of the requested file.</returns>
        public async Task<Stream> DownloadAsync(string id, string versionId = null, TimeSpan? timeout = null, long? startOffsetInBytes = null, long? endOffsetInBytes = null, string sharedLink = null, string sharedLinkPassword = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.ContentPathString, id)) { Timeout = timeout }
                .Param("version", versionId);

            if (startOffsetInBytes.HasValue && endOffsetInBytes.HasValue)
            {
                request = request.Header("Range", $"bytes={startOffsetInBytes}-{endOffsetInBytes}");
            }

            if (!string.IsNullOrEmpty(sharedLink))
            {
                var sharedLinkHeader = SharedLinkUtils.GetSharedLinkHeader(sharedLink, sharedLinkPassword);
                request.Header(sharedLinkHeader.Item1, sharedLinkHeader.Item2);
            }

            IBoxResponse<Stream> response = await ToResponseAsync<Stream>(request).ConfigureAwait(false);
            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves the temporary direct Uri to a file (valid for 15 minutes). This is typically used to send as a redirect to a browser to make the browser download the file directly from Box.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="versionId">Version of the file.</param>
        /// <returns></returns>
        public async Task<Uri> GetDownloadUriAsync(string id, string versionId = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.ContentPathString, id)) { FollowRedirect = false }
                .Param("version", versionId);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);
            var locationUri = response.Headers.Location;

            return locationUri;
        }

        /// <summary>
        /// Verify that a file will be accepted by Box before you send all the bytes over the wire.
        /// </summary>
        /// <remarks>
        /// Preflight checks verify all permissions as if the file was actually uploaded including:
        /// Folder upload permission
        /// File name collisions
        /// file size caps
        /// folder and file name restrictions*
        /// folder and account storage quota
        /// </remarks>
        /// <param name="preflightCheckRequest">BoxPreflightCheckRequest object.</param>
        /// <returns>Returns a BoxPreflightCheck object if successful, otherwise an error is thrown when any of the preflight conditions are not met.</returns>
        public async Task<BoxPreflightCheck> PreflightCheck(BoxPreflightCheckRequest preflightCheckRequest)
        {
            preflightCheckRequest.ThrowIfNull("preflightCheckRequest")
                .Name.ThrowIfNullOrWhiteSpace("preflightCheckRequest.Name");
            preflightCheckRequest.Parent.ThrowIfNull("preflightCheckRequest.Parent")
                .Id.ThrowIfNullOrWhiteSpace("preflightCheckRequest.Parent.Id");

            BoxRequest request = new BoxRequest(_config.FilesPreflightCheckUri)
                .Method(RequestMethod.Options);

            request.Payload = _converter.Serialize(preflightCheckRequest);
            request.ContentType = Constants.RequestParameters.ContentTypeJson;

            IBoxResponse<BoxPreflightCheck> response = await ToResponseAsync<BoxPreflightCheck>(request).ConfigureAwait(false);
            response.ResponseObject.Success = response.Status == ResponseStatus.Success;

            return response.ResponseObject;
        }

        /// <summary>
        /// Verify that a new version of a file will be accepted by Box before you send all the bytes over the wire.
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="preflightCheckRequest"></param>
        /// <returns></returns>
        public async Task<BoxPreflightCheck> PreflightCheckNewVersion(string fileId, BoxPreflightCheckRequest preflightCheckRequest)
        {
            if (preflightCheckRequest.Size <= 0)
                throw new ArgumentException("Size in bytes must be greater than zero (otherwise preflight check for new version would always succeed)", "sizeinBytes");

            BoxRequest request = new BoxRequest(new Uri(string.Format(Constants.FilesPreflightCheckNewVersionString, fileId)))
                .Method(RequestMethod.Options);

            request.Payload = _converter.Serialize(preflightCheckRequest);
            request.ContentType = Constants.RequestParameters.ContentTypeJson;

            IBoxResponse<BoxPreflightCheck> response = await ToResponseAsync<BoxPreflightCheck>(request).ConfigureAwait(false);
            response.ResponseObject.Success = response.Status == ResponseStatus.Success;

            return response.ResponseObject;
        }

        /// <summary>
        /// Uploads a provided file to the target parent folder.
        /// If the file already exists, an error will be thrown.
        /// A proper timeout should be provided for large uploads.
        /// </summary>
        /// <param name="fileRequest">BoxFileRequest object.</param>
        /// <param name="stream">Stream of uploading file.</param>
        /// <param name="fields">Fields which shall be returned in result.</param>
        /// <param name="timeout">Timeout for response.</param>
        /// <param name="contentMD5">The SHA1 hash of the file.</param>
        /// <param name="setStreamPositionToZero">Set position for input stream to 0.</param>
        /// <param name="uploadUri">Uri to use for upload. Default upload endpoint URI is used if not specified.</param>
        /// <returns>A full file object is returned inside of a collection if the ID is valid and if the update is successful.</returns>
        public async Task<BoxFile> UploadAsync(BoxFileRequest fileRequest, Stream stream, IEnumerable<string> fields = null,
                                                TimeSpan? timeout = null, byte[] contentMD5 = null,
                                                bool setStreamPositionToZero = true,
                                                Uri uploadUri = null)
        {
            stream.ThrowIfNull("stream");
            fileRequest.ThrowIfNull("fileRequest")
                .Name.ThrowIfNullOrWhiteSpace("filedRequest.Name");
            fileRequest.Parent.ThrowIfNull("fileRequest.Parent")
                .Id.ThrowIfNullOrWhiteSpace("fileRequest.Parent.Id");

            if (setStreamPositionToZero)
                stream.Position = 0;

            uploadUri = uploadUri ?? _config.FilesUploadEndpointUri;

            BoxMultiPartRequest request = new BoxMultiPartRequest(uploadUri) { Timeout = timeout }
                .Param(ParamFields, fields)
                .FormPart(new BoxStringFormPart()
                {
                    Name = "attributes",
                    Value = _converter.Serialize(fileRequest)
                })
                .FormPart(new BoxFileFormPart()
                {
                    Name = "file",
                    Value = stream,
                    FileName = fileRequest.Name
                });

            if (contentMD5 != null)
                request.Header(Constants.RequestParameters.ContentMD5, HexStringFromBytes(contentMD5));

            IBoxResponse<BoxCollection<BoxFile>> response = await ToResponseAsync<BoxCollection<BoxFile>>(request).ConfigureAwait(false);

            // We can only upload one file at a time, so return the first entry
            return response.ResponseObject.Entries.FirstOrDefault();
        }

        /// <summary>
        /// Create an upload session for uploading a new file.
        /// </summary>
        /// <param name="uploadSessionRequest">The upload session request.</param>
        /// <returns>The upload session.</returns>
        public async Task<BoxFileUploadSession> CreateUploadSessionAsync(BoxFileUploadSessionRequest uploadSessionRequest)
        {
            var uploadUri = _config.FilesUploadSessionEndpointUri;

            var request = new BoxRequest(uploadUri)
                .Method(RequestMethod.Post);

            request.Payload = _converter.Serialize(uploadSessionRequest);
            request.ContentType = Constants.RequestParameters.ContentTypeJson;

            IBoxResponse<BoxFileUploadSession> response = await ToResponseAsync<BoxFileUploadSession>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Create an upload session for uploading a new file version.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="uploadNewVersionSessionRequest">The upload session request for new file version.</param>
        /// <returns>The upload session for uploading new Box file version using session.</returns>
        public async Task<BoxFileUploadSession> CreateNewVersionUploadSessionAsync(string fileId,
            BoxFileUploadSessionRequest uploadNewVersionSessionRequest)
        {
            var uploadUri = new Uri(string.Format(Constants.FilesNewVersionUploadSessionEndpointString, fileId));

            var request = new BoxRequest(uploadUri)
                .Method(RequestMethod.Post);

            request.Payload = _converter.Serialize(uploadNewVersionSessionRequest);
            request.ContentType = Constants.RequestParameters.ContentTypeJson;

            IBoxResponse<BoxFileUploadSession> response = await ToResponseAsync<BoxFileUploadSession>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// This method is used to upload a new version of an existing file in a user’s account. Similar to regular file uploads, 
        /// these are performed as multipart form uploads. An optional If-Match header can be included to ensure that client only 
        /// overwrites the file if it knows about the latest version. The filename on Box will remain the same as the previous version.
        /// To update the file’s name, you can specify a new name for the file using the fileName parameter.
        /// A proper timeout should be provided for large uploads.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileId">Id of the file to upload a new version to.</param>
        /// <param name="stream">Stream of the uploading file.</param>
        /// <param name="etag">This ‘etag’ field of the file, which will be set in the If-Match header.</param>
        /// <param name="fields">Fields which shall be returned in result.</param>
        /// <param name="timeout">Optional timeout for response.</param>
        /// <param name="contentMD5">The SHA1 hash of the file.</param>
        /// <param name="setStreamPositionToZero">Set position for input stream to 0.</param>
        /// <param name="uploadUri">Optional url for uploading file.</param>
        /// <returns>A full file object is returned.</returns>
        public async Task<BoxFile> UploadNewVersionAsync(string fileName, string fileId, Stream stream,
                                                         string etag = null, IEnumerable<string> fields = null,
                                                         TimeSpan? timeout = null, byte[] contentMD5 = null,
                                                         bool setStreamPositionToZero = true,
                                                         Uri uploadUri = null, DateTimeOffset? contentModifiedTime = null)
        {
            fileId.ThrowIfNullOrWhiteSpace("fileId");
            stream.ThrowIfNull("stream");

            if (setStreamPositionToZero)
                stream.Position = 0;

            uploadUri = uploadUri ?? new Uri(string.Format(Constants.FilesNewVersionEndpointString, fileId));

            dynamic attributes = new JObject();
            if (fileName != null)
            {
                attributes.name = fileName;
            }
            if (contentModifiedTime.HasValue)
            {
                attributes.content_modified_at = contentModifiedTime.Value.ToUniversalTime().ToString(Constants.RFC3339DateFormat_UTC);
            }

            BoxMultiPartRequest request = new BoxMultiPartRequest(uploadUri) { Timeout = timeout }
                .Header(Constants.RequestParameters.IfMatch, etag)
                .Param(ParamFields, fields)
                .FormPart(new BoxStringFormPart()
                {
                    Name = "attributes",
                    Value = _converter.Serialize(attributes)
                })
                .FormPart(new BoxFileFormPart()
                {
                    Name = "filename",
                    Value = stream,
                    FileName = fileName
                });

            if (contentMD5 != null)
                request.Header(Constants.RequestParameters.ContentMD5, HexStringFromBytes(contentMD5));

            IBoxResponse<BoxCollection<BoxFile>> response = await ToResponseAsync<BoxCollection<BoxFile>>(request).ConfigureAwait(false);

            // We can only upload one file at a time, so return the first entry
            return response.ResponseObject.Entries.FirstOrDefault();
        }

        /// <summary>
        /// Upload a part of the file to the session.
        /// </summary>
        /// <param name="uploadPartUri">Upload Uri from Create Session which include SessionId</param>
        /// <param name="sha">The message digest of the part body, formatted as specified by RFC 3230.</param>
        /// <param name="partStartOffsetInBytes">Part begin offset in bytes.</param>
        /// <param name="sizeOfOriginalFileInBytes">Size of original file in bytes.</param>
        /// <param name="stream">The file part stream.</param>
        /// <param name="timeout">Timeout of the request.</param>
        /// <returns>The complete BoxUploadPartResponse object if success.</returns>
        public async Task<BoxUploadPartResponse> UploadPartAsync(Uri uploadPartUri, string sha, long partStartOffsetInBytes, long sizeOfOriginalFileInBytes, Stream stream, TimeSpan? timeout = null)
        {
            var request = new BoxBinaryRequest(uploadPartUri) { Timeout = timeout }
                .Method(RequestMethod.Put)
                .Header(Constants.RequestParameters.Digest, "sha=" + sha)
                .Header(Constants.RequestParameters.ContentRange, "bytes " + partStartOffsetInBytes + "-" + (partStartOffsetInBytes + stream.Length - 1) + "/" + sizeOfOriginalFileInBytes)
                .Part(new BoxFilePart()
                {
                    Value = stream
                });

            var response = await ToResponseAsync<BoxUploadPartResponse>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Commits a session after all individual file part uploads are complete.
        /// </summary>
        /// <param name="commitSessionUrl">Commit URL returned in the Create Session response.</param>
        /// <param name="sha">The message digest of the complete file, formatted as specified by RFC 3230.</param>
        /// <param name="sessionPartsInfo">Parts info for the uploaded parts.</param>
        /// <returns> The complete BoxFile object. </returns>
        public async Task<BoxFile> CommitSessionAsync(Uri commitSessionUrl, string sha, BoxSessionParts sessionPartsInfo)
        {
            return await CommitSessionInternalAsync(commitSessionUrl, sha, sessionPartsInfo).ConfigureAwait(false);
        }

        /// <summary>
        /// Commits a session after all individual new file version part uploads are complete.
        /// </summary>
        /// <param name="commitSessionUrl">Commit URL returned in the Create Session response.</param>
        /// <param name="sha">The message digest of the complete file, formatted as specified by RFC 3230.</param>
        /// <param name="sessionPartsInfo">Parts info for the uploaded parts.</param>
        /// <returns> The complete BoxFile object. </returns>
        public async Task<BoxFile> CommitFileVersionSessionAsync(Uri commitSessionUrl, string sha, BoxSessionParts sessionPartsInfo)
        {
            return await CommitSessionInternalAsync(commitSessionUrl, sha, sessionPartsInfo).ConfigureAwait(false);
        }

        private async Task<BoxFile> CommitSessionInternalAsync(Uri commitSessionUrl, string sha, BoxSessionParts sessionPartsInfo)
        {
            BoxRequest request = new BoxRequest(commitSessionUrl)
                .Method(RequestMethod.Post)
                .Header(Constants.RequestParameters.Digest, "sha=" + sha)
                .Payload(_converter.Serialize(sessionPartsInfo));

            request.ContentType = Constants.RequestParameters.ContentTypeJson;

            var response = await ToResponseAsync<BoxCollection<BoxFile>>(request).ConfigureAwait(false);

            // We can only commit one file at a time, so return the first entry
            return response.ResponseObject.Entries.FirstOrDefault();
        }

        /// <summary>
        /// Get a list of parts that were uploaded in a session.
        /// </summary>
        /// <param name="sessionPartsUri">The Url returned in the Create Session response.</param>
        /// <param name="offset">Zero-based index of first OffsetID of part to return.</param>
        /// <param name="limit">How many parts to return.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all; defaults to false.</param>
        /// <returns>Returns a list of file part information uploaded so far in the session.</returns>
        public async Task<BoxCollection<BoxSessionPartInfo>> GetSessionUploadedPartsAsync(Uri sessionPartsUri, int? offset = null, int? limit = null, bool autoPaginate = false)
        {
            BoxRequest request = new BoxRequest(sessionPartsUri)
               .Method(RequestMethod.Get);

            if (offset.HasValue)
            {
                request.Param("offset", offset.Value.ToString());
                // sessionPartsUri = sessionPartsUri.AppendQueryString("offset", offset.Value.ToString());
            }

            if (limit.HasValue)
            {
                request.Param("limit", limit.Value.ToString());
                // sessionPartsUri = sessionPartsUri.AppendQueryString("limit", limit.Value.ToString());
            }

            if (autoPaginate)
            {
                if (!limit.HasValue)
                {
                    limit = 100;
                    request.Param("limit", limit.ToString());
                }

                if (!offset.HasValue)
                {
                    request.Param("offset", "0");
                }

                return await AutoPaginateLimitOffset<BoxSessionPartInfo>(request, limit.Value).ConfigureAwait(false);
            }
            else
            {
                var response = await ToResponseAsync<BoxCollection<BoxSessionPartInfo>>(request).ConfigureAwait(false);

                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Gets the status of the upload session.
        /// </summary>
        /// <param name="sessionUploadStatusUri">The Url returned in the Create Session response.</param>
        /// <returns>Returns an object representing the status of the upload session.</returns>
        public async Task<BoxSessionUploadStatus> GetSessionUploadStatusAsync(Uri sessionUploadStatusUri)
        {
            BoxRequest request = new BoxRequest(sessionUploadStatusUri)
               .Method(RequestMethod.Get);

            IBoxResponse<BoxSessionUploadStatus> response = await ToResponseAsync<BoxSessionUploadStatus>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Upload a new large file version by splitting them up and uploads in a session.
        /// </summary>
        /// <param name="stream">The file stream.</param>
        /// <param name="fileId">Id of the remote file.</param>
        /// <param name="timeout">Timeout for subsequent UploadPart requests.</param>
        /// <param name="progress">Will report progress from 1 - 100.</param>
        /// <returns>The BoxFile object.</returns>
        public async Task<BoxFile> UploadNewVersionUsingSessionAsync(Stream stream, string fileId, string fileName = null, TimeSpan? timeout = null,
            IProgress<BoxProgress> progress = null)
        {
            // Create Upload Session
            var fileSize = stream.Length;
            var uploadNewVersionSessionRequest = new BoxFileUploadSessionRequest
            {
                FileSize = fileSize,
                FileName = fileName
            };

            var boxFileVersionUploadSession = await CreateNewVersionUploadSessionAsync(fileId, uploadNewVersionSessionRequest).ConfigureAwait(false);
            var response = await UploadSessionAsync(stream, boxFileVersionUploadSession, timeout, progress).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Upload a large file by splitting them up and uploads in a session.
        /// </summary>
        /// <param name="stream">The file stream.</param>
        /// <param name="fileName">Name of the remote file name.</param>
        /// <param name="folderId">Parent folder id.</param>
        /// <param name="timeout">Timeout for subsequent UploadPart requests.</param>
        /// <param name="progress">Will report progress from 1 - 100.</param>
        /// <returns>The complete BoxFile object.</returns>
        public async Task<BoxFile> UploadUsingSessionAsync(Stream stream, string fileName,
            string folderId, TimeSpan? timeout = null, IProgress<BoxProgress> progress = null)
        {
            // Create Upload Session
            var fileSize = stream.Length;
            var uploadSessionRequest = new BoxFileUploadSessionRequest
            {
                FileName = fileName,
                FileSize = fileSize,
                FolderId = folderId
            };

            var boxFileUploadSession = await CreateUploadSessionAsync(uploadSessionRequest).ConfigureAwait(false);
            var response = await UploadSessionAsync(stream, boxFileUploadSession, timeout, progress).ConfigureAwait(false);

            return response;
        }

        /// <summary>
        /// Using the upload session for new file upload and new file version upload, 
        /// upload by parts file/file version
        /// </summary>
        /// <param name="stream">The file stream.</param>
        /// <param name="uploadSession">BoxFileUpload session retrieved for uploading new file or uploading new file version</param>
        /// <param name="progress">Will report progress from 1 - 100.</param>
        /// <param name="callingMethod"> The calling function name used to determine which commit function to call.</param>
        /// <returns>The complete BoxFile object.</returns>
        private async Task<BoxFile> UploadSessionAsync(Stream stream, BoxFileUploadSession uploadFileSession,
            TimeSpan? timeout = null, IProgress<BoxProgress> progress = null, [CallerMemberName] string callingMethod = null)
        {
            var fileSize = stream.Length;
            // Parse upload session response
            var boxSessionEndpoint = uploadFileSession.SessionEndpoints;
            var uploadPartUri = new Uri(boxSessionEndpoint.UploadPart);
            var commitUri = new Uri(boxSessionEndpoint.Commit);
            var partSize = uploadFileSession.PartSize;
            if (long.TryParse(partSize, out var partSizeLong) == false)
            {
                throw new BoxCodingException("File part size is wrong!");
            }

            var numberOfParts = UploadUsingSessionInternal.GetNumberOfParts(fileSize,
                partSizeLong);

            // Full file sha1 for final commit
            var fullFileSha1 = await Task.Run(() =>
            {
                return Helper.GetSha1Hash(stream);
            });

            // Upload parts in session
            var allSessionParts = await UploadPartsInSessionAsync(uploadPartUri,
                numberOfParts, partSizeLong, stream,
                fileSize, timeout, progress).ConfigureAwait(false);

            var allSessionPartsList = allSessionParts.ToList();

            var sessionPartsForCommit = new BoxSessionParts
            {
                Parts = allSessionPartsList
            };

            // Commit, Retry 5 times with interval related to the total part number
            // Having debugged this -- retries do consistenly happen so we up the retries
            const int RetryCount = 5;
            var retryInterval = allSessionPartsList.Count * 100;

            // Depending on the calling function a different commit function will be used
            // to commit file upload parts
            if (callingMethod == "UploadUsingSessionAsync")
            {
                var fileResponse =
                await Retry.ExecuteAsync(
                    async () =>
                        await CommitSessionAsync(commitUri, fullFileSha1,
                            sessionPartsForCommit).ConfigureAwait(false),
                    TimeSpan.FromMilliseconds(retryInterval), RetryCount);
                return fileResponse;
            }
            else // This is to call the commit function for file version uploads
            {
                var versionResponse =
                await Retry.ExecuteAsync(
                    async () =>
                        await CommitFileVersionSessionAsync(commitUri, fullFileSha1,
                            sessionPartsForCommit).ConfigureAwait(false),
                        TimeSpan.FromMilliseconds(retryInterval), RetryCount);
                return versionResponse;
            }
        }

        private async Task<IEnumerable<BoxSessionPartInfo>> UploadPartsInSessionAsync(
            Uri uploadPartsUri, int numberOfParts, long partSize, Stream stream,
            long fileSize, TimeSpan? timeout = null, IProgress<BoxProgress> progress = null)
        {
            var maxTaskNum = Environment.ProcessorCount + 1;

            // Retry 5 times for 10 seconds
            const int RetryMaxCount = 5;
            const int RetryMaxInterval = 10;

            var ret = new List<BoxSessionPartInfo>();

            using (var concurrencySemaphore = new SemaphoreSlim(maxTaskNum))
            {
                var postTaskTasks = new List<Task>();
                var taskCompleted = 0;

                var tasks = new List<Task<BoxUploadPartResponse>>();
                for (var i = 0; i < numberOfParts; i++)
                {
                    await concurrencySemaphore.WaitAsync().ConfigureAwait(false);

                    // Split file as per part size
                    var partOffset = partSize * i;

                    // Retry
                    var uploadPartWithRetryTask = Retry.ExecuteAsync(async () =>
                    {
                        // Release the memory when done
                        using (var partFileStream = UploadUsingSessionInternal.GetFilePart(stream, partSize,
                                    partOffset))
                        {
                            var sha = Helper.GetSha1Hash(partFileStream);
                            partFileStream.Position = 0;
                            var uploadPartResponse = await UploadPartAsync(
                                uploadPartsUri, sha, partOffset, fileSize, partFileStream,
                                timeout).ConfigureAwait(false);

                            return uploadPartResponse;
                        }
                    }, TimeSpan.FromSeconds(RetryMaxInterval), RetryMaxCount);

                    // Have each task notify the Semaphore when it completes so that it decrements the number of tasks currently running.
                    postTaskTasks.Add(uploadPartWithRetryTask.ContinueWith(tsk =>
                        {
                            concurrencySemaphore.Release();
                            ++taskCompleted;
                            if (progress != null)
                            {
                                var boxProgress = new BoxProgress()
                                {
                                    progress = taskCompleted * 100 / numberOfParts
                                };

                                progress.Report(boxProgress);
                            }
                        }
                    ));

                    tasks.Add(uploadPartWithRetryTask);
                }

                var results = await Task.WhenAll(tasks).ConfigureAwait(false);
                ret.AddRange(results.Select(elem => elem.Part));
            }

            return ret;
        }

        private string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        /// <summary>
        /// If there are previous versions of this file, this method can be used to retrieve metadata about the older versions.
        /// <remarks>Versions are only tracked for Box users with premium accounts.</remarks>
        /// </summary>
        /// <param name="id">The file id.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="offset">Zero-based index of first OffsetID of part to return.</param>
        /// <param name="limit">How many parts to return.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all; defaults to false.</param>
        /// <returns>A collection of versions other than the main version of the file. If a file has no other versions, an empty collection will be returned.
        /// Note that if a file has a total of three versions, only the first two version will be returned.</returns>
        public async Task<BoxCollection<BoxFileVersion>> ViewVersionsAsync(string id, IEnumerable<string> fields = null, int? offset = null, int? limit = null, bool autoPaginate = false)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.VersionsPathString, id))
                .Param(ParamFields, fields);

            if (offset.HasValue)
            {
                request.Param("offset", offset.Value.ToString());
            }

            if (limit.HasValue)
            {
                request.Param("limit", limit.Value.ToString());
            }

            if (autoPaginate)
            {
                if (!limit.HasValue)
                {
                    limit = 100;
                    request.Param("limit", limit.ToString());
                }

                if (!offset.HasValue)
                {
                    request.Param("offset", "0");
                }

                return await AutoPaginateLimitOffset<BoxFileVersion>(request, limit.Value).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollection<BoxFileVersion>> response = await ToResponseAsync<BoxCollection<BoxFileVersion>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Used to update individual or multiple fields in the file object, including renaming the file, changing it’s description, 
        /// and creating a shared link for the file. To move a file, change the ID of its parent folder. An optional etag
        /// can be included to ensure that client only updates the file if it knows about the latest version.
        /// </summary>
        /// <param name="fileRequest">BoxFileRequest object.</param>
        /// <param name="etag">This ‘etag’ field of the file, which will be set in the If-Match header.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The complete BoxFile object.</returns>
        public async Task<BoxFile> UpdateInformationAsync(BoxFileRequest fileRequest, string etag = null, IEnumerable<string> fields = null)
        {
            fileRequest.ThrowIfNull("fileRequest")
                .Id.ThrowIfNullOrWhiteSpace("fileRequest.Id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, fileRequest.Id)
                .Method(RequestMethod.Put)
                .Header(Constants.RequestParameters.IfMatch, etag)
                .Param(ParamFields, fields);

            request.Payload = _converter.Serialize(fileRequest);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Discards a file to the trash. The etag of the file can be included as an ‘If-Match’ header to prevent race conditions.
        /// <remarks>Depending on the enterprise settings for this user, the item will either be immediately and permanently deleted from Box or moved to the trash.</remarks>
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="etag">This ‘etag’ field of the file, which will be set in the If-Match header.</param>
        /// <returns>True if file is deleted, false otherwise.</returns>
        public async Task<bool> DeleteAsync(string id, string etag = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Method(RequestMethod.Delete)
                .Header(Constants.RequestParameters.IfMatch, etag);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Abort the upload session and discard all data uploaded. This cannot be reversed.
        /// </summary>
        /// <param name="abortUri">The upload session abort url that aborts the session.</param>
        /// <returns>True if deletion success.</returns>
        public async Task<bool> DeleteUploadSessionAsync(Uri abortUri)
        {
            var request = new BoxRequest(abortUri)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxFileUploadSession> response = await ToResponseAsync<BoxFileUploadSession>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Used to create a copy of a file in another folder. The original version of the file will not be altered.
        /// </summary>
        /// <param name="fileRequest">BoxFileRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>
        /// A full file object is returned if the ID is valid and if the update is successful. 
        /// Errors can be thrown if the destination folder is invalid or if a file-name collision occurs. 
        /// </returns>
        public async Task<BoxFile> CopyAsync(BoxFileRequest fileRequest, IEnumerable<string> fields = null)
        {
            fileRequest.ThrowIfNull("fileRequest");
            fileRequest.Id.ThrowIfNullOrWhiteSpace("fileRequest.Id");
            fileRequest.Parent.ThrowIfNull("fileRequest.Parent")
                .Id.ThrowIfNullOrWhiteSpace("fileRequest.Parent.Id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.CopyPathString, fileRequest.Id))
                .Method(RequestMethod.Post)
                .Param(ParamFields, fields);

            fileRequest.Id = null; //file Id was used as a query parameter in this case
            request.Payload(_converter.Serialize(fileRequest));

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to create a shared link for a file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="sharedLinkRequest">BoxSharedLinkRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A full file object containing the updated shared link is returned
        /// if the ID is valid and if the update is successful.</returns>
        public async Task<BoxFile> CreateSharedLinkAsync(string id, BoxSharedLinkRequest sharedLinkRequest, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");
            sharedLinkRequest.ThrowIfNull("sharedLinkRequest");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Method(RequestMethod.Put)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(new BoxItemRequest() { SharedLink = sharedLinkRequest }));

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to delete the shared link for a file.
        /// </summary>
        /// <param name="id">The Id of the file to remove the shared link from.</param>
        /// <returns>A full file object with the shared link removed is returned
        /// if the ID is valid and if the update is successful.</returns>
        public async Task<BoxFile> DeleteSharedLinkAsync(string id, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Method(RequestMethod.Put)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(new BoxDeleteSharedLinkRequest()));

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Use this to get a list of all the collaborations on a file
        /// </summary>
        /// <param name="id">Id of the file</param>
        /// <param name="marker">Paging marker; use null to retrieve the first page of results</param>
        /// <param name="limit">Number of records to return per page</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <param name="autoPaginate">Whether to automatically gather the entire result set</param>
        /// <returns>Collection of the collaborations on a file</returns>
        public async Task<BoxCollectionMarkerBasedV2<BoxCollaboration>> GetCollaborationsCollectionAsync(string id, string marker = null, int? limit = null, IEnumerable<string> fields = null, bool autoPaginate = false)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.CollaborationsPathString, id))
                .Param(ParamFields, fields)
                .Param("limit", limit?.ToString())
                .Param("marker", marker);

            if (autoPaginate)
            {
                if (!limit.HasValue)
                {
                    limit = 100;
                    request.Param("limit", limit.ToString());
                }

                return await AutoPaginateMarkerV2<BoxCollaboration>(request, limit.Value).ConfigureAwait(false);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBasedV2<BoxCollaboration>> response = await ToResponseAsync<BoxCollectionMarkerBasedV2<BoxCollaboration>>(request).ConfigureAwait(false);

                return response.ResponseObject;
            }

        }

        /// <summary>
        /// Retrieves the comments on a particular file, if any exist.
        /// </summary>
        /// <param name="id">The Id of the item that the comments should be retrieved for.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A Collection of comment objects are returned. If there are no comments on the file, an empty comments array is returned.</returns>
        public async Task<BoxCollection<BoxComment>> GetCommentsAsync(string id, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.CommentsPathString, id))
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxComment>> response = await ToResponseAsync<BoxCollection<BoxComment>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves a thumbnail, or smaller image representation, of this file. Sizes of 32x32,
        /// 64x64, 128x128, and 256x256 can be returned in the .png format
        /// and sizes of 32x32, 94x94, 160x160, and 320x320 can be returned in the .jpg format.
        /// Thumbnails can be generated for the image and video file formats listed here.
        /// see <a href="http://community.box.com/t5/Managing-Your-Content/What-file-types-are-supported-by-Box-s-Content-Preview/ta-p/327"/>
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="minHeight">The minimum height of the thumbnail.</param>
        /// <param name="minWidth">The minimum width of the thumbnail.</param>
        /// <param name="maxHeight">The maximum height of the thumbnail.</param>
        /// <param name="maxWidth">The maximum width of the thumbnail.</param>
        /// <param name="handleRetry">Specifies whether the method handles retries. If true, then the method would retry the call if the HTTP response is 'Accepted'. The delay for the retry is determined 
        /// by the RetryAfter header, or if that header is not set, by the constant DefaultRetryDelay.</param>
        /// <param name="throttle">Whether the requests will be throttled. Recommended to be left true to prevent spamming the server.</param>
        /// <param name="extension">png or jpg with no "."</param>
        /// <returns>Contents of thumbnail as Stream.</returns>
        public async Task<Stream> GetThumbnailAsync(string id, int? minHeight = null, int? minWidth = null, int? maxHeight = null, int? maxWidth = null, bool throttle = true, bool handleRetry = true, string extension = "png")
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.ThumbnailPathExtensionString, id, extension))
                .Param("min_height", minHeight.ToString())
                .Param("min_width", minWidth.ToString())
                .Param("max_height", maxHeight.ToString())
                .Param("max_width", maxWidth.ToString());

            IBoxResponse<Stream> response = await ToResponseAsync<Stream>(request, throttle).ConfigureAwait(false);

            while (response.StatusCode == HttpStatusCode.Accepted && handleRetry)
            {
                await Task.Delay(GetTimeDelay(response.Headers));
                response = await ToResponseAsync<Stream>(request, throttle).ConfigureAwait(false);
            }

            return response.ResponseObject;
        }

        /// <summary>
        /// Gets a preview link (URI) for a file that is valid for 60 seconds.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <returns>Preview link (URI) for a file that is valid for 60 seconds.</returns>
        public async Task<Uri> GetPreviewLinkAsync(string id)
        {
            var fields = new List<string>() { "expiring_embed_link" };
            var file = await GetInformationAsync(id, fields).ConfigureAwait(false);
            return file.ExpiringEmbedLink.Url;
        }


        /// <summary>
        /// Return the time to wait until retrying the call. If no RetryAfter value is specified in the header, use default value;
        /// </summary>
        private int GetTimeDelay(HttpResponseHeaders headers)
        {
            return headers != null && headers.RetryAfter != null && int.TryParse(headers.RetryAfter.ToString(), out var timeToWait)
                ? timeToWait * 1000
                : Constants.DefaultRetryDelay;
        }

        /// <summary>
        /// Retrieves an item that has been moved to the trash.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The full item will be returned, including information about when the it was moved to the trash.</returns>
        public async Task<BoxFile> GetTrashedAsync(string id, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.TrashPathString, id))
                .Param(ParamFields, fields);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Restores an item that has been moved to the trash. Default behavior is to restore the item to the folder it was in before 
        /// it was moved to the trash. If that parent folder no longer exists or if there is now an item with the same name in that 
        /// parent folder, the new parent folder and/or new name will need to be included in the request.
        /// </summary>
        /// <param name="fileRequest">BoxFileRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The full item will be returned with a 201 Created status. By default it is restored to the parent folder it was in before it was trashed.</returns>
        public async Task<BoxFile> RestoreTrashedAsync(BoxFileRequest fileRequest, IEnumerable<string> fields = null)
        {
            fileRequest.ThrowIfNull("fileRequest")
                .Id.ThrowIfNullOrWhiteSpace("fileRequest.Id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, fileRequest.Id)
                .Method(RequestMethod.Post)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize(fileRequest));

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Permanently deletes an item that is in the trash. The item will no longer exist in Box. This action cannot be undone.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <returns>Returns true upon successful deletion, false otherwise.</returns>
        public async Task<bool> PurgeTrashedAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.TrashPathString, id))
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Gets a lock file object representation of the lock on the provided file Id (if a lock exists, otherwise returns null).
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <returns>BoxFileLock object.</returns>
        public async Task<BoxFileLock> GetLockAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Param(ParamFields, BoxFile.FieldLock);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject.Lock;
        }

        /// <summary>
        /// Used to update the lock information on the file (for example, ExpiresAt or IsDownloadPrevented.
        /// </summary>
        /// <param name="lockFileRequest">BoxFileLockRequest object.</param>
        /// <param name="id">Id of the file.</param>
        /// <returns>BoxFileLock object.</returns>
        public async Task<BoxFileLock> UpdateLockAsync(BoxFileLockRequest lockFileRequest, string id)
        {
            lockFileRequest.ThrowIfNull("lockFileRequest");
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Method(RequestMethod.Put)
                .Param(ParamFields, BoxFile.FieldLock);

            request.Payload = _converter.Serialize(lockFileRequest);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject.Lock;
        }

        /// <summary>
        /// Used to create a lock on the file.
        /// </summary>
        /// <param name="lockFileRequest">Request contains Lock object for setting of lock properties such as ExpiresAt - the time the lock expires, IsDownloadPrevented - whether or not the file can be downloaded while locked. </param>
        /// <param name="id">Id of the file.</param>
        /// <returns>Returns information about locked file</returns>
        public async Task<BoxFileLock> LockAsync(BoxFileLockRequest lockFileRequest, string id)
        {
            return await UpdateLockAsync(lockFileRequest, id).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove a lock
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> UnLock(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Method(RequestMethod.Put)
                .Payload("{\"lock\":null}");

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Retrieves all of the tasks for given file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A collection of task objects is returned. If there are no tasks, an empty collection will be returned.</returns>
        public async Task<BoxCollection<BoxTask>> GetFileTasks(string id, IEnumerable<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.TasksPathString, id))
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxTask>> response = await ToResponseAsync<BoxCollection<BoxTask>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to retrieve the watermark for a corresponding Box file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <returns>An object containing information about the watermark associated for this file. If the file does not have a watermark applied to it than return null</returns>
        public async Task<BoxWatermark> GetWatermarkAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.WatermarkPathString, id))
               .Method(RequestMethod.Get);

            IBoxResponse<BoxWatermarkResponse> response = await ToResponseAsync<BoxWatermarkResponse>(request).ConfigureAwait(false);
            return response.Status == ResponseStatus.Success ? response.ResponseObject.Watermark : null;
        }

        /// <summary>
        /// Used to apply or update the watermark for a corresponding Box file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="applyWatermarkRequest">BoxApplyWatermarkRequest object. Can be null, for using default values - imprint="default" </param>
        /// <returns>An object containing information about the watermark associated for this file.</returns>
        public async Task<BoxWatermark> ApplyWatermarkAsync(string id, BoxApplyWatermarkRequest applyWatermarkRequest = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            if (applyWatermarkRequest == null)
            {
                applyWatermarkRequest = new BoxApplyWatermarkRequest() { Watermark = new BoxWatermarkRequest() { Imprint = "default" } };
            }

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.WatermarkPathString, id))
               .Method(RequestMethod.Put)
               .Payload(_converter.Serialize(applyWatermarkRequest));

            IBoxResponse<BoxWatermarkResponse> response = await ToResponseAsync<BoxWatermarkResponse>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success ? response.ResponseObject.Watermark : null;
        }

        /// <summary>
        /// Used to remove the watermark for a corresponding Box file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <returns>True to confirm the watermark has been removed. If the file did not have a watermark applied to it, than False will be returned.</returns>
        public async Task<bool> RemoveWatermarkAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.WatermarkPathString, id))
               .Method(RequestMethod.Delete);

            IBoxResponse<BoxWatermarkResponse> response = await ToResponseAsync<BoxWatermarkResponse>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Discards a specific file version to the trash.
        /// </summary>
        /// <param name="id">Id of the file (Required).</param>
        /// <param name="versionId">Id of the version (Required).</param>
        /// <param name="etag">The etag of the file. This is in the ‘etag’ field of the file object.</param>
        /// <returns>True, if version is deleted.</returns>
        public async Task<bool> DeleteOldVersionAsync(string id, string versionId, string etag = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");
            versionId.ThrowIfNullOrWhiteSpace("versionId");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.DeleteOldVersionPathString, id, versionId))
                .Method(RequestMethod.Delete)
                .Header(Constants.RequestParameters.IfMatch, etag);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// If there are previous versions of this file, this method can be used to promote one of the older versions to the top of the stack. 
        /// This actually mints a copy of the old version and puts it on the top of the versions stack. 
        /// The file will have the exact same contents, the same SHA1/etag, and the same name as the promoted version. 
        /// Other properties such as comments do not get updated to their former values.
        /// </summary>
        /// <param name="id">Id of the file (Required).</param>
        /// <param name="versionId">Id of the version (Required).</param>
        /// <returns>The newly promoted file_version object is returned</returns>
        public async Task<BoxFileVersion> PromoteVersionAsync(string id, string versionId)
        {
            id.ThrowIfNullOrWhiteSpace("id");
            versionId.ThrowIfNullOrWhiteSpace("versionId");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.PromoteVersionPathString, id))
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(new BoxPromoteVersionRequest()
                {
                    Id = versionId
                }));

            IBoxResponse<BoxFileVersion> response = await ToResponseAsync<BoxFileVersion>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Representations are digital assets stored in Box. We can request the following representations: PDF, Extracted Text, Thumbnail,
        /// and Single Page depending on whether the file type is supported by passing in the corresponding x-rep-hints header. This will generate a 
        /// representation with a template_url. We will then have to either replace the {+asset_path} with <page_number>.png for single page or empty string
        /// for all other representation types.
        /// </summary>
        /// <param name="boxRepresentationRequest">Object of type BoxRepresentationRequest that contains Box file id, x-rep-hints</param>
        /// <returns>A full file object containing the updated representations template_url and state is returned.</returns>
        /// </summary>
        public async Task<BoxRepresentationCollection<BoxRepresentation>> GetRepresentationsAsync(BoxRepresentationRequest representationRequest)
        {
            representationRequest.ThrowIfNull("representationRequest")
                .FileId.ThrowIfNullOrWhiteSpace("representationRequest.FileId");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, representationRequest.FileId)
                .Method(RequestMethod.Get)
                .Header(Constants.RequestParameters.XRepHints, representationRequest.XRepHints)
                .Header(Constants.RequestParameters.SetContentDispositionType, representationRequest.SetContentDispositionType)
                .Header(Constants.RequestParameters.SetContentDispositionFilename, representationRequest.SetContentDispositionFilename)
                .Param(ParamFields, Constants.RequestParameters.RepresentationField);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            var retryCounter = 1;

            while (response.StatusCode == HttpStatusCode.Accepted && representationRequest.HandleRetry)
            {
                response = await CallWithRetryCheck(() => ToResponseAsync<BoxFile>(request),
                    $"Could not get valid File Representation status after {retryCounter} retries.",
                    retryCounter++)
                    .ConfigureAwait(false);
            }

            return response.ResponseObject.Representations;
        }

        /// <summary>
        /// Creates a zip and downloads it to a given Stream.
        /// </summary>
        /// <param name="zipRequest">Object of type BoxZipRequest that contains name and items.</param>
        /// <param name="output">The stream to where the zip file will be written.</param>
        /// <returns>The status of the download.</returns>
        /// </summary>
        public async Task<BoxZipDownloadStatus> DownloadZip(BoxZipRequest zipRequest, Stream output)
        {
            BoxZip createdZip = await CreateZip(zipRequest).ConfigureAwait(false);
            IBoxRequest downloadRequest = new BoxRequest(createdZip.DownloadUrl);
            IBoxResponse<Stream> streamResponse = await ToResponseAsync<Stream>(downloadRequest).ConfigureAwait(false);
            Stream fileStream = streamResponse.ResponseObject;

            // Default the buffer size to 4K.
            const int BufferSize = 4096;
            var buffer = new byte[BufferSize];
            int bytesRead;
            do
            {
                bytesRead = fileStream.Read(buffer, 0, BufferSize);
                if (bytesRead > 0)
                {
                    output.Write(buffer, 0, bytesRead);
                }
            } while (bytesRead > 0);

            BoxRequest downloadStatusRequest = new BoxRequest(createdZip.StatusUrl)
               .Method(RequestMethod.Get);
            IBoxResponse<BoxZipDownloadStatus> response = await ToResponseAsync<BoxZipDownloadStatus>(downloadStatusRequest).ConfigureAwait(false);
            BoxZipDownloadStatus finalResponse = response.ResponseObject;
            finalResponse.NameConflicts = createdZip.NameConflicts;
            return finalResponse;
        }

        /// <summary>
        /// Representations are digital assets stored in Box. We can request the following representations: PDF, Extracted Text, Thumbnail,
        /// and Single Page depending on whether the file type is supported by passing in the corresponding x-rep-hints header. This will generate a 
        /// representation with a template_url. We will then have to either replace the {+asset_path} with <page_number>.png for single page or empty string
        /// for all other representation types.
        /// </summary>
        /// <param name="boxRepresentationRequest">Object of type BoxRepresentationRequest that contains Box file id, x-rep-hints.</param>
        /// <returns>A stream over the representation contents.</returns>
        /// </summary>
        public async Task<Stream> GetRepresentationContentAsync(BoxRepresentationRequest representationRequest, string assetPath = "")
        {
            var reps = await GetRepresentationsAsync(representationRequest).ConfigureAwait(false);
            if (reps.Entries.Count == 0)
            {
                throw new BoxCodingException("Could not get requested representation!");
            }

            var repInfo = reps.Entries[0];
            IBoxRequest downloadRequest;
            IBoxResponse<Stream> response;
            switch (repInfo.Status.State)
            {
                case "success":
                case "viewable":
                    downloadRequest = new BoxRequest(new Uri(repInfo.Content.UrlTemplate.Replace("{+asset_path}", assetPath)));
                    response = await ToResponseAsync<Stream>(downloadRequest).ConfigureAwait(false);
                    return response.ResponseObject;
                case "error":
                    throw new BoxCodingException("Representation had error status");
                case "none":
                case "pending":
                    var urlTemplate = await PollRepresentationInfo(repInfo.Info.Url).ConfigureAwait(false);
                    downloadRequest = new BoxRequest(new Uri(urlTemplate.Replace("{+asset_path}", assetPath)));
                    response = await ToResponseAsync<Stream>(downloadRequest).ConfigureAwait(false);
                    return response.ResponseObject;
                default:
                    throw new BoxCodingException("Representation has unknown status");
            }

        }

        private async Task<BoxZip> CreateZip(BoxZipRequest zipRequest)
        {
            BoxRequest request = new BoxRequest(_config.ZipDownloadsEndpointUri)
               .Method(RequestMethod.Post)
               .Payload(_converter.Serialize(zipRequest));

            IBoxResponse<BoxZip> response = await ToResponseAsync<BoxZip>(request).ConfigureAwait(false);
            return response.ResponseObject;
        }

        private async Task<string> PollRepresentationInfo(string infoUrl, int retryCounter = 1)
        {
            var infoRequest = new BoxRequest(new Uri(infoUrl));
            IBoxResponse<BoxRepresentation> infoResponse = await ToResponseAsync<BoxRepresentation>(infoRequest).ConfigureAwait(false);
            var rep = infoResponse.ResponseObject;
            switch (rep.Status.State)
            {
                case "success":
                case "viewable":
                    return rep.Content.UrlTemplate;
                case "error":
                    throw new BoxCodingException("Representation had error status");
                case "none":
                case "pending":
                    return await CallWithRetryCheck(() => PollRepresentationInfo(infoUrl, ++retryCounter),
                        $"Could not get valid Representation status after {retryCounter} retries.",
                        retryCounter)
                        .ConfigureAwait(false);
                default:
                    throw new BoxCodingException("Representation has unknown status");
            }
        }

        private async Task<T> CallWithRetryCheck<T>(Func<Task<T>> action, string errorMessage, int retryCounter = 1) where T : class
        {
            if (retryCounter <= HttpRequestHandler.RetryLimit)
            {
                await Task.Delay(_config.RetryStrategy.GetRetryTimeout(retryCounter));
                return await action().ConfigureAwait(false);
            }
            else
            {
                throw new BoxCodingException(errorMessage);
            }
        }
    }

    internal static class UploadUsingSessionInternal
    {
        public static int GetNumberOfParts(long totalSize, long partSize)
        {
            if (partSize == 0)
            {
                throw new BoxCodingException("Part Size cannot be 0");
            }

            var numberOfParts = Convert.ToInt32(totalSize / partSize);
            if (totalSize % partSize != 0)
            {
                numberOfParts++;
            }
            return numberOfParts;
        }

        public static Stream GetFilePart(Stream stream, long partSize, long partOffset)
        {
            // Default the buffer size to 4K.
            const int BufferSize = 4096;

            var buffer = new byte[BufferSize];
            stream.Position = partOffset;
            var partStream = new MemoryStream();
            int bytesRead;
            do
            {
                bytesRead = stream.Read(buffer, 0, 4096);
                if (bytesRead > 0)
                {
                    long bytesToWrite = bytesRead;
                    var shouldBreak = false;
                    if (partStream.Length + bytesRead >= partSize)
                    {
                        bytesToWrite = partSize - partStream.Length;
                        shouldBreak = true;
                    }

                    partStream.Write(buffer, 0, Convert.ToInt32(bytesToWrite));

                    if (shouldBreak)
                    {
                        break;
                    }
                }
            } while (bytesRead > 0);

            return partStream;
        }
    }
}
