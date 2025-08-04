using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class ZipDownloadsManager : IZipDownloadsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public ZipDownloadsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Creates a request to download multiple files and folders as a single `zip`
        /// archive file. This API does not return the archive but instead performs all
        /// the checks to ensure that the user has access to all the items, and then
        /// returns a `download_url` and a `status_url` that can be used to download the
        /// archive.
        /// 
        /// The limit for an archive is either the Account's upload limit or
        /// 10,000 files, whichever is met first.
        /// 
        /// **Note**: Downloading a large file can be
        /// affected by various
        /// factors such as distance, network latency,
        /// bandwidth, and congestion, as well as packet loss
        /// ratio and current server load.
        /// For these reasons we recommend that a maximum ZIP archive
        /// total size does not exceed 25GB.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createZipDownload method
        /// </param>
        /// <param name="headers">
        /// Headers of createZipDownload method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ZipDownload> CreateZipDownloadAsync(ZipDownloadRequest requestBody, CreateZipDownloadHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateZipDownloadHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/zip_downloads"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ZipDownload>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Returns the contents of a `zip` archive in binary format. This URL does not
        /// require any form of authentication and could be used in a user's browser to
        /// download the archive to a user's device.
        /// 
        /// By default, this URL is only valid for a few seconds from the creation of
        /// the request for this archive. Once a download has started it can not be
        /// stopped and resumed, instead a new request for a zip archive would need to
        /// be created.
        /// 
        /// The URL of this endpoint should not be considered as fixed. Instead, use
        /// the [Create zip download](e://post_zip_downloads) API to request to create a
        /// `zip` archive, and then follow the `download_url` field in the response to
        /// this endpoint.
        /// </summary>
        /// <param name="downloadUrl">
        /// The URL that can be used to download created `zip` archive.
        ///  Example: `https://dl.boxcloud.com/2.0/zip_downloads/29l00nfxDyHOt7RphI9zT_w==nDnZEDjY2S8iEWWCHEEiptFxwoWojjlibZjJ6geuE5xnXENDTPxzgbks_yY=/content`
        /// </param>
        /// <param name="headers">
        /// Headers of getZipDownloadContent method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<System.IO.Stream> GetZipDownloadContentAsync(string downloadUrl, GetZipDownloadContentHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetZipDownloadContentHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: downloadUrl, method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Binary) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return NullableUtils.Unwrap(response.Content);
        }

        /// <summary>
        /// Returns the download status of a `zip` archive, allowing an application to
        /// inspect the progress of the download as well as the number of items that
        /// might have been skipped.
        /// 
        /// This endpoint can only be accessed once the download has started.
        /// Subsequently this endpoint is valid for 12 hours from the start of the
        /// download.
        /// 
        /// The URL of this endpoint should not be considered as fixed. Instead, use
        /// the [Create zip download](e://post_zip_downloads) API to request to create a
        /// `zip` archive, and then follow the `status_url` field in the response to
        /// this endpoint.
        /// </summary>
        /// <param name="statusUrl">
        /// The URL that can be used to get the status of the `zip` archive being downloaded.
        ///  Example: `https://dl.boxcloud.com/2.0/zip_downloads/29l00nfxDyHOt7RphI9zT_w==nDnZEDjY2S8iEWWCHEEiptFxwoWojjlibZjJ6geuE5xnXENDTPxzgbks_yY=/status`
        /// </param>
        /// <param name="headers">
        /// Headers of getZipDownloadStatus method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ZipDownloadStatus> GetZipDownloadStatusAsync(string statusUrl, GetZipDownloadStatusHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetZipDownloadStatusHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: statusUrl, method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ZipDownloadStatus>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a zip and downloads its content
        /// </summary>
        /// <param name="requestBody">
        /// Zip download request body
        /// </param>
        /// <param name="headers">
        /// Headers of zip download method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<System.IO.Stream> DownloadZipAsync(ZipDownloadRequest requestBody, DownloadZipHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DownloadZipHeaders();
            ZipDownload zipDownloadSession = await this.CreateZipDownloadAsync(requestBody: new ZipDownloadRequest(items: requestBody.Items) { DownloadFileName = requestBody.DownloadFileName }, headers: new CreateZipDownloadHeaders(extraHeaders: headers.ExtraHeaders), cancellationToken: cancellationToken).ConfigureAwait(false);
            return await this.GetZipDownloadContentAsync(downloadUrl: NullableUtils.Unwrap(zipDownloadSession.DownloadUrl), headers: new GetZipDownloadContentHeaders(extraHeaders: headers.ExtraHeaders), cancellationToken: cancellationToken).ConfigureAwait(false);
        }

    }
}