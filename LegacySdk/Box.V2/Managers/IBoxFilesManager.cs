using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Utility;

namespace Box.V2.Managers
{
    /// <summary>
    /// File objects represent that metadata about individual files in Box, with attributes describing who created the file, 
    /// when it was last modified, and other information. 
    /// </summary>
    public interface IBoxFilesManager
    {
        /// <summary>
        /// Retrieves information about a file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <param name="sharedLink">The shared link for this file</param>
        /// <param name="sharedLinkPassword">The password for the shared link (if required)</param>
        /// <returns>A full file object is returned if the ID is valid and if the user has access to the file.</returns>
        Task<BoxFile> GetInformationAsync(string id, IEnumerable<string> fields = null, string sharedLink = null, string sharedLinkPassword = null);

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
        Task<Stream> DownloadAsync(string id, string versionId = null, TimeSpan? timeout = null, long? startOffsetInBytes = null, long? endOffsetInBytes = null, string sharedLink = null, string sharedLinkPassword = null);

        /// <summary>
        /// Retrieves the temporary direct Uri to a file (valid for 15 minutes). This is typically used to send as a redirect to a browser to make the browser download the file directly from Box.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="versionId">Version of the file.</param>
        /// <returns></returns>
        Task<Uri> GetDownloadUriAsync(string id, string versionId = null);

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
        Task<BoxPreflightCheck> PreflightCheck(BoxPreflightCheckRequest preflightCheckRequest);

        /// <summary>
        /// Verify that a new version of a file will be accepted by Box before you send all the bytes over the wire.
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="preflightCheckRequest"></param>
        /// <returns></returns>
        Task<BoxPreflightCheck> PreflightCheckNewVersion(string fileId, BoxPreflightCheckRequest preflightCheckRequest);

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
        Task<BoxFile> UploadAsync(BoxFileRequest fileRequest, Stream stream, IEnumerable<string> fields = null,
            TimeSpan? timeout = null, byte[] contentMD5 = null,
            bool setStreamPositionToZero = true,
            Uri uploadUri = null);

        /// <summary>
        /// Create an upload session for uploading a new file.
        /// </summary>
        /// <param name="uploadSessionRequest">The upload session request.</param>
        /// <returns>The upload session.</returns>
        Task<BoxFileUploadSession> CreateUploadSessionAsync(BoxFileUploadSessionRequest uploadSessionRequest);

        /// <summary>
        /// Create an upload session for uploading a new file version.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="uploadNewVersionSessionRequest">The upload session request for new file version.</param>
        /// <returns>The upload session for uploading new Box file version using session.</returns>
        Task<BoxFileUploadSession> CreateNewVersionUploadSessionAsync(string fileId,
            BoxFileUploadSessionRequest uploadNewVersionSessionRequest);

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
        Task<BoxFile> UploadNewVersionAsync(string fileName, string fileId, Stream stream,
            string etag = null, IEnumerable<string> fields = null,
            TimeSpan? timeout = null, byte[] contentMD5 = null,
            bool setStreamPositionToZero = true,
            Uri uploadUri = null, DateTimeOffset? contentModifiedTime = null);

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
        Task<BoxUploadPartResponse> UploadPartAsync(Uri uploadPartUri, string sha, long partStartOffsetInBytes, long sizeOfOriginalFileInBytes, Stream stream, TimeSpan? timeout = null);

        /// <summary>
        /// Commits a session after all individual file part uploads are complete.
        /// </summary>
        /// <param name="commitSessionUrl">Commit URL returned in the Create Session response.</param>
        /// <param name="sha">The message digest of the complete file, formatted as specified by RFC 3230.</param>
        /// <param name="sessionPartsInfo">Parts info for the uploaded parts.</param>
        /// <returns> The complete BoxFile object. </returns>
        Task<BoxFile> CommitSessionAsync(Uri commitSessionUrl, string sha, BoxSessionParts sessionPartsInfo);

        /// <summary>
        /// Commits a session after all individual new file version part uploads are complete.
        /// </summary>
        /// <param name="commitSessionUrl">Commit URL returned in the Create Session response.</param>
        /// <param name="sha">The message digest of the complete file, formatted as specified by RFC 3230.</param>
        /// <param name="sessionPartsInfo">Parts info for the uploaded parts.</param>
        /// <returns> The complete BoxFile object. </returns>
        Task<BoxFile> CommitFileVersionSessionAsync(Uri commitSessionUrl, string sha, BoxSessionParts sessionPartsInfo);

        /// <summary>
        /// Get a list of parts that were uploaded in a session.
        /// </summary>
        /// <param name="sessionPartsUri">The Url returned in the Create Session response.</param>
        /// <param name="offset">Zero-based index of first OffsetID of part to return.</param>
        /// <param name="limit">How many parts to return.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all; defaults to false.</param>
        /// <returns>Returns a list of file part information uploaded so far in the session.</returns>
        Task<BoxCollection<BoxSessionPartInfo>> GetSessionUploadedPartsAsync(Uri sessionPartsUri, int? offset = null, int? limit = null, bool autoPaginate = false);

        /// <summary>
        /// Gets the status of the upload session.
        /// </summary>
        /// <param name="sessionUploadStatusUri">The Url returned in the Create Session response.</param>
        /// <returns>Returns an object representing the status of the upload session.</returns>
        Task<BoxSessionUploadStatus> GetSessionUploadStatusAsync(Uri sessionUploadStatusUri);

        /// <summary>
        /// Upload a new large file version by splitting them up and uploads in a session.
        /// </summary>
        /// <param name="stream">The file stream.</param>
        /// <param name="fileId">Id of the remote file.</param>
        /// <param name="timeout">Timeout for subsequent UploadPart requests.</param>
        /// <param name="progress">Will report progress from 1 - 100.</param>
        /// <returns>The BoxFile object.</returns>
        Task<BoxFile> UploadNewVersionUsingSessionAsync(Stream stream, string fileId, string fileName = null, TimeSpan? timeout = null,
            IProgress<BoxProgress> progress = null);

        /// <summary>
        /// Upload a large file by splitting them up and uploads in a session.
        /// </summary>
        /// <param name="stream">The file stream.</param>
        /// <param name="fileName">Name of the remote file name.</param>
        /// <param name="folderId">Parent folder id.</param>
        /// <param name="timeout">Timeout for subsequent UploadPart requests.</param>
        /// <param name="progress">Will report progress from 1 - 100.</param>
        /// <returns>The complete BoxFile object.</returns>
        Task<BoxFile> UploadUsingSessionAsync(Stream stream, string fileName,
            string folderId, TimeSpan? timeout = null, IProgress<BoxProgress> progress = null);

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
        Task<BoxCollection<BoxFileVersion>> ViewVersionsAsync(string id, IEnumerable<string> fields = null, int? offset = null, int? limit = null, bool autoPaginate = false);

        /// <summary>
        /// Used to update individual or multiple fields in the file object, including renaming the file, changing it’s description, 
        /// and creating a shared link for the file. To move a file, change the ID of its parent folder. An optional etag
        /// can be included to ensure that client only updates the file if it knows about the latest version.
        /// </summary>
        /// <param name="fileRequest">BoxFileRequest object.</param>
        /// <param name="etag">This ‘etag’ field of the file, which will be set in the If-Match header.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The complete BoxFile object.</returns>
        Task<BoxFile> UpdateInformationAsync(BoxFileRequest fileRequest, string etag = null, IEnumerable<string> fields = null);

        /// <summary>
        /// Discards a file to the trash. The etag of the file can be included as an ‘If-Match’ header to prevent race conditions.
        /// <remarks>Depending on the enterprise settings for this user, the item will either be immediately and permanently deleted from Box or moved to the trash.</remarks>
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="etag">This ‘etag’ field of the file, which will be set in the If-Match header.</param>
        /// <returns>True if file is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(string id, string etag = null);

        /// <summary>
        /// Abort the upload session and discard all data uploaded. This cannot be reversed.
        /// </summary>
        /// <param name="abortUri">The upload session abort url that aborts the session.</param>
        /// <returns>True if deletion success.</returns>
        Task<bool> DeleteUploadSessionAsync(Uri abortUri);

        /// <summary>
        /// Used to create a copy of a file in another folder. The original version of the file will not be altered.
        /// </summary>
        /// <param name="fileRequest">BoxFileRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>
        /// A full file object is returned if the ID is valid and if the update is successful. 
        /// Errors can be thrown if the destination folder is invalid or if a file-name collision occurs. 
        /// </returns>
        Task<BoxFile> CopyAsync(BoxFileRequest fileRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to create a shared link for a file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="sharedLinkRequest">BoxSharedLinkRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A full file object containing the updated shared link is returned
        /// if the ID is valid and if the update is successful.</returns>
        Task<BoxFile> CreateSharedLinkAsync(string id, BoxSharedLinkRequest sharedLinkRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to delete the shared link for a file.
        /// </summary>
        /// <param name="id">The Id of the file to remove the shared link from.</param>
        /// <returns>A full file object with the shared link removed is returned
        /// if the ID is valid and if the update is successful.</returns>
        Task<BoxFile> DeleteSharedLinkAsync(string id, IEnumerable<string> fields = null);

        /// <summary>
        /// Use this to get a list of all the collaborations on a file
        /// </summary>
        /// <param name="id">Id of the file</param>
        /// <param name="marker">Paging marker; use null to retrieve the first page of results</param>
        /// <param name="limit">Number of records to return per page</param>
        /// <param name="fields">Attribute(s) to include in the response</param>
        /// <param name="autoPaginate">Whether to automatically gather the entire result set</param>
        /// <returns>Collection of the collaborations on a file</returns>
        Task<BoxCollectionMarkerBasedV2<BoxCollaboration>> GetCollaborationsCollectionAsync(string id, string marker = null, int? limit = null, IEnumerable<string> fields = null, bool autoPaginate = false);

        /// <summary>
        /// Retrieves the comments on a particular file, if any exist.
        /// </summary>
        /// <param name="id">The Id of the item that the comments should be retrieved for.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A Collection of comment objects are returned. If there are no comments on the file, an empty comments array is returned.</returns>
        Task<BoxCollection<BoxComment>> GetCommentsAsync(string id, IEnumerable<string> fields = null);

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
        Task<Stream> GetThumbnailAsync(string id, int? minHeight = null, int? minWidth = null, int? maxHeight = null, int? maxWidth = null, bool throttle = true, bool handleRetry = true, string extension = "png");

        /// <summary>
        /// Gets a preview link (URI) for a file that is valid for 60 seconds.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <returns>Preview link (URI) for a file that is valid for 60 seconds.</returns>
        Task<Uri> GetPreviewLinkAsync(string id);

        /// <summary>
        /// Retrieves an item that has been moved to the trash.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The full item will be returned, including information about when the it was moved to the trash.</returns>
        Task<BoxFile> GetTrashedAsync(string id, IEnumerable<string> fields = null);

        /// <summary>
        /// Restores an item that has been moved to the trash. Default behavior is to restore the item to the folder it was in before 
        /// it was moved to the trash. If that parent folder no longer exists or if there is now an item with the same name in that 
        /// parent folder, the new parent folder and/or new name will need to be included in the request.
        /// </summary>
        /// <param name="fileRequest">BoxFileRequest object.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>The full item will be returned with a 201 Created status. By default it is restored to the parent folder it was in before it was trashed.</returns>
        Task<BoxFile> RestoreTrashedAsync(BoxFileRequest fileRequest, IEnumerable<string> fields = null);

        /// <summary>
        /// Permanently deletes an item that is in the trash. The item will no longer exist in Box. This action cannot be undone.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <returns>Returns true upon successful deletion, false otherwise.</returns>
        Task<bool> PurgeTrashedAsync(string id);

        /// <summary>
        /// Gets a lock file object representation of the lock on the provided file Id (if a lock exists, otherwise returns null).
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <returns>BoxFileLock object.</returns>
        Task<BoxFileLock> GetLockAsync(string id);

        /// <summary>
        /// Used to update the lock information on the file (for example, ExpiresAt or IsDownloadPrevented.
        /// </summary>
        /// <param name="lockFileRequest">BoxFileLockRequest object.</param>
        /// <param name="id">Id of the file.</param>
        /// <returns>BoxFileLock object.</returns>
        Task<BoxFileLock> UpdateLockAsync(BoxFileLockRequest lockFileRequest, string id);

        /// <summary>
        /// Used to create a lock on the file.
        /// </summary>
        /// <param name="lockFileRequest">Request contains Lock object for setting of lock properties such as ExpiresAt - the time the lock expires, IsDownloadPrevented - whether or not the file can be downloaded while locked. </param>
        /// <param name="id">Id of the file.</param>
        /// <returns>Returns information about locked file</returns>
        Task<BoxFileLock> LockAsync(BoxFileLockRequest lockFileRequest, string id);

        /// <summary>
        /// Remove a lock
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> UnLock(string id);

        /// <summary>
        /// Retrieves all of the tasks for given file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="fields">Attribute(s) to include in the response.</param>
        /// <returns>A collection of task objects is returned. If there are no tasks, an empty collection will be returned.</returns>
        Task<BoxCollection<BoxTask>> GetFileTasks(string id, IEnumerable<string> fields = null);

        /// <summary>
        /// Used to retrieve the watermark for a corresponding Box file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <returns>An object containing information about the watermark associated for this file. If the file does not have a watermark applied to it than return null</returns>
        Task<BoxWatermark> GetWatermarkAsync(string id);

        /// <summary>
        /// Used to apply or update the watermark for a corresponding Box file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <param name="applyWatermarkRequest">BoxApplyWatermarkRequest object. Can be null, for using default values - imprint="default" </param>
        /// <returns>An object containing information about the watermark associated for this file.</returns>
        Task<BoxWatermark> ApplyWatermarkAsync(string id, BoxApplyWatermarkRequest applyWatermarkRequest = null);

        /// <summary>
        /// Used to remove the watermark for a corresponding Box file.
        /// </summary>
        /// <param name="id">Id of the file.</param>
        /// <returns>True to confirm the watermark has been removed. If the file did not have a watermark applied to it, than False will be returned.</returns>
        Task<bool> RemoveWatermarkAsync(string id);

        /// <summary>
        /// Discards a specific file version to the trash.
        /// </summary>
        /// <param name="id">Id of the file (Required).</param>
        /// <param name="versionId">Id of the version (Required).</param>
        /// <param name="etag">The etag of the file. This is in the ‘etag’ field of the file object.</param>
        /// <returns>True, if version is deleted.</returns>
        Task<bool> DeleteOldVersionAsync(string id, string versionId, string etag = null);

        /// <summary>
        /// If there are previous versions of this file, this method can be used to promote one of the older versions to the top of the stack. 
        /// This actually mints a copy of the old version and puts it on the top of the versions stack. 
        /// The file will have the exact same contents, the same SHA1/etag, and the same name as the promoted version. 
        /// Other properties such as comments do not get updated to their former values.
        /// </summary>
        /// <param name="id">Id of the file (Required).</param>
        /// <param name="versionId">Id of the version (Required).</param>
        /// <returns>The newly promoted file_version object is returned</returns>
        Task<BoxFileVersion> PromoteVersionAsync(string id, string versionId);

        /// <summary>
        /// Representations are digital assets stored in Box. We can request the following representations: PDF, Extracted Text, Thumbnail,
        /// and Single Page depending on whether the file type is supported by passing in the corresponding x-rep-hints header. This will generate a 
        /// representation with a template_url. We will then have to either replace the {+asset_path} with <page_number>.png for single page or empty string
        /// for all other representation types.
        /// </summary>
        /// <param name="boxRepresentationRequest">Object of type BoxRepresentationRequest that contains Box file id, x-rep-hints</param>
        /// <returns>A full file object containing the updated representations template_url and state is returned.</returns>
        /// </summary>
        Task<BoxRepresentationCollection<BoxRepresentation>> GetRepresentationsAsync(BoxRepresentationRequest representationRequest);

        /// <summary>
        /// Representations are digital assets stored in Box. We can request the following representations: PDF, Extracted Text, Thumbnail,
        /// and Single Page depending on whether the file type is supported by passing in the corresponding x-rep-hints header. This will generate a 
        /// representation with a template_url. We will then have to either replace the {+asset_path} with <page_number>.png for single page or empty string
        /// for all other representation types.
        /// </summary>
        /// <param name="boxRepresentationRequest">Object of type BoxRepresentationRequest that contains Box file id, x-rep-hints.</param>
        /// <returns>A stream over the representation contents.</returns>
        /// </summary>
        Task<Stream> GetRepresentationContentAsync(BoxRepresentationRequest representationRequest, string assetPath = "");

        /// <summary>
        /// Creates a zip and downloads it to a given Stream.
        /// </summary>
        /// <param name="zipRequest">Object of type BoxZipRequest that contains name and items.</param>
        /// <param name="output">The stream to where the zip file will be written.</param>
        /// <returns>The status of the download.</returns>
        /// </summary>
        Task<BoxZipDownloadStatus> DownloadZip(BoxZipRequest zipRequest, Stream output);
    }
}
