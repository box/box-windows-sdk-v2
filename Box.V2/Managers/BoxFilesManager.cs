using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using Box.V2.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    /// <summary>
    /// File objects represent that metadata about individual files in Box, with attributes describing who created the file, 
    /// when it was last modified, and other information. 
    /// </summary>
    public class BoxFilesManager : BoxResourceManager
    {
        public BoxFilesManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Retrieves information about a file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A full file object is returned if the ID is valid and if the user has access to the file.</returns>
        public async Task<BoxFile> GetInformationAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Param(ParamFields, fields);

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Returns the stream of the requested file.
        /// </summary>
        /// <param name="id">Id of the file to download.</param>
        /// <param name="versionId">The ID specific version of this file to download.</param>
        /// <param name="timeout">Optional timeout for response.</param>
        /// <returns>Stream of the requested file.</returns>
        public async Task<Stream> DownloadStreamAsync(string id, string versionId = null, TimeSpan? timeout = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.ContentPathString, id)) { Timeout = timeout }
                .Param("version", versionId);

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
        public async Task<BoxFile> UploadAsync(BoxFileRequest fileRequest, Stream stream, List<string> fields = null,
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

            uploadUri = uploadUri == null ? _config.FilesUploadEndpointUri : uploadUri;

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
                                                         string etag = null, List<string> fields = null,
                                                         TimeSpan? timeout = null, byte[] contentMD5 = null,
                                                         bool setStreamPositionToZero = true,
                                                         Uri uploadUri = null)
        {
            fileName.ThrowIfNullOrWhiteSpace("fileName");
            fileId.ThrowIfNullOrWhiteSpace("fileId");
            stream.ThrowIfNull("stream");

            if (setStreamPositionToZero)
                stream.Position = 0;

            uploadUri = uploadUri == null ? new Uri(string.Format(Constants.FilesNewVersionEndpointString, fileId)) : uploadUri;

            BoxMultiPartRequest request = new BoxMultiPartRequest(uploadUri) { Timeout = timeout }
                .Header(Constants.RequestParameters.IfMatch, etag)
                .Param(ParamFields, fields)
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

        private string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
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
        /// <returns>A collection of versions other than the main version of the file. If a file has no other versions, an empty collection will be returned.
        /// Note that if a file has a total of three versions, only the first two version will be returned.</returns>
        public async Task<BoxCollection<BoxFileVersion>> ViewVersionsAsync(string id, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.VersionsPathString, id))
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxFileVersion>> response = await ToResponseAsync<BoxCollection<BoxFileVersion>>(request).ConfigureAwait(false);

            return response.ResponseObject;
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
        public async Task<BoxFile> UpdateInformationAsync(BoxFileRequest fileRequest, string etag = null, List<string> fields = null)
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
        /// Used to create a copy of a file in another folder. The original version of the file will not be altered.
        /// </summary>
        /// <param name="fileRequest">BoxFileRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>
        /// A full file object is returned if the ID is valid and if the update is successful. 
        /// Errors can be thrown if the destination folder is invalid or if a file-name collision occurs. 
        /// </returns>
        public async Task<BoxFile> CopyAsync(BoxFileRequest fileRequest, List<string> fields = null)
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
        /// Used to create a shared link for this particular file. Please see here for more information on the permissions available for shared links. 
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="sharedLinkRequest">BoxSharedLinkRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A full file object containing the updated shared link is returned
        /// if the ID is valid and if the update is successful.</returns>
        public async Task<BoxFile> CreateSharedLinkAsync(string id, BoxSharedLinkRequest sharedLinkRequest, List<string> fields = null)
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
        /// Used to delete the shared link for this particular file.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BoxFile> DeleteSharedLinkAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, id)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize(new BoxDeleteSharedLinkRequest()));

            IBoxResponse<BoxFile> response = await ToResponseAsync<BoxFile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves the comments on a particular file, if any exist.
        /// </summary>
        /// <param name="id">The Id of the item that the comments should be retrieved for.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A Collection of comment objects are returned. If there are no comments on the file, an empty comments array is returned.</returns>
        public async Task<BoxCollection<BoxComment>> GetCommentsAsync(string id, List<string> fields = null)
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
        /// <see cref="http://community.box.com/t5/Managing-Your-Content/What-file-types-are-supported-by-Box-s-Content-Preview/ta-p/327"/>
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="minHeight">The minimum height of the thumbnail.</param>
        /// <param name="minWidth">The minimum width of the thumbnail.</param>
        /// <param name="maxHeight">The maximum height of the thumbnail.</param>
        /// <param name="maxWidth">The maximum width of the thumbnail.</param>
        /// <param name="handleRetry">Specifies whether the method handles retries. If true, then the method would retry the call if the HTTP response is 'Accepted'. The delay for the retry is determined 
        /// by the RetryAfter header, or if that header is not set, by the constant DefaultRetryDelay.</param>
        /// <param name="throttle">Whether the requests will be throttled. Recommended to be left true to prevent spamming the server.</param>
        /// <returns>Contents of thumbnail as Stream.</returns>
        public async Task<Stream> GetThumbnailAsync(string id, int? minHeight = null, int? minWidth = null, int? maxHeight = null, int? maxWidth = null, bool throttle = true, bool handleRetry = true)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.ThumbnailPathString, id))
                .Param("min_height", minHeight.ToString())
                .Param("min_width", minWidth.ToString())
                .Param("max_height", maxHeight.ToString())
                .Param("max_width", maxWidth.ToString());

            IBoxResponse<Stream> response = await ToResponseAsync<Stream>(request, throttle).ConfigureAwait(false);

            while (response.StatusCode == HttpStatusCode.Accepted && handleRetry)
            {
                await CrossPlatform.Delay(GetTimeDelay(response.Headers));
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
            var file = await GetInformationAsync(id, fields);
            return file.ExpiringEmbedLink.Url;
        }

        /// <summary>
        /// Gets the stream of a preview page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// /// <param name="handleRetry"></param>
        /// <returns>A PNG of the preview</returns>
        [Obsolete("Please use GetPreviewLinkAsync instead.  This functionality is not supported by Box.")]
        public async Task<Stream> GetPreviewAsync(string id, int page, bool handleRetry = true)
        {
            return (await GetPreviewResponseAsync(id, page, handleRetry: handleRetry)).ResponseObject;
        }

        /// <summary>
        /// Get the preview and return a BoxFilePreview response. 
        /// </summary>
        /// <param name="id">id of the file to return.</param>
        /// <param name="page">page number of the file.</param>
        /// <param name="handleRetry">specifies whether the method handles retries. If true, then the method would retry the call if the HTTP response is 'Accepted'. The delay for the retry is determined 
        /// by the RetryAfter header, or if that header is not set, by the constant DefaultRetryDelay.</param>
        /// <returns>BoxFilePreview that contains the stream, current page number and total number of pages in the file.</returns>
        [Obsolete("Please use GetPreviewLinkAsync instead.  This functionality is not supported by Box.")]
        public async Task<BoxFilePreview> GetFilePreviewAsync(string id, int page, int? maxWidth = null, int? minWidth = null, int? maxHeight = null, int? minHeight = null, bool handleRetry = true)
        {
            IBoxResponse<Stream> response = await GetPreviewResponseAsync(id, page, maxWidth, minWidth, maxHeight, minHeight, handleRetry);

            BoxFilePreview filePreview = new BoxFilePreview();
            filePreview.CurrentPage = page;
            filePreview.ReturnedStatusCode = response.StatusCode;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                filePreview.PreviewStream = response.ResponseObject;
                filePreview.TotalPages = response.BuildPagesCount();
            }

            return filePreview;
        }

        private async Task<IBoxResponse<Stream>> GetPreviewResponseAsync(string id, int page, int? maxWidth = null, int? minWidth = null, int? maxHeight = null, int? minHeight = null, bool handleRetry = true)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.FilesEndpointUri, string.Format(Constants.PreviewPathString, id))
                .Param("page", page.ToString())
                .Param("max_width", maxWidth.ToString())
                .Param("max_height", maxHeight.ToString())
                .Param("min_width", minWidth.ToString())
                .Param("min_height", minHeight.ToString());

            var response = await ToResponseAsync<Stream>(request).ConfigureAwait(false);

            while (response.StatusCode == HttpStatusCode.Accepted && handleRetry)
            {
                await CrossPlatform.Delay(GetTimeDelay(response.Headers));
                response = await ToResponseAsync<Stream>(request).ConfigureAwait(false);
            }

            return response;
        }

        /// <summary>
        /// Return the time to wait until retrying the call. If no RetryAfter value is specified in the header, use default value;
        /// </summary>
        private int GetTimeDelay(HttpResponseHeaders headers)
        {
            int timeToWait;
            if (headers != null && headers.RetryAfter != null && int.TryParse(headers.RetryAfter.ToString(), out timeToWait))
                return timeToWait * 1000;

            return Constants.DefaultRetryDelay;
        }

        /// <summary>
        /// Retrieves an item that has been moved to the trash.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The full item will be returned, including information about when the it was moved to the trash.</returns>
        public async Task<BoxFile> GetTrashedAsync(string id, List<string> fields = null)
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
        public async Task<BoxFile> RestoreTrashedAsync(BoxFileRequest fileRequest, List<string> fields = null)
        {
            fileRequest.ThrowIfNull("fileRequest")
                .Id.ThrowIfNullOrWhiteSpace("fileRequest.Id");
            fileRequest.Name.ThrowIfNullOrWhiteSpace("fileRequest.Name");

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
            return await UpdateLockAsync(lockFileRequest, id);
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
        public async Task<BoxCollection<BoxTask>> GetFileTasks(string id, List<string> fields = null)
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
            if (response.Status == ResponseStatus.Success)
            {
                return response.ResponseObject.Watermark;
            }
            else
            {
                return null;
            }
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

            if (response.Status == ResponseStatus.Success)
            {
                return response.ResponseObject.Watermark;
            }
            else
            {
                return null;
            }
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
    }
}
