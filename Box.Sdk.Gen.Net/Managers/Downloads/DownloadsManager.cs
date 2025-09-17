using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class DownloadsManager : IDownloadsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public DownloadsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Returns the contents of a file in binary format.
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
        /// <param name="queryParams">
        /// Query parameters of downloadFile method
        /// </param>
        /// <param name="headers">
        /// Headers of downloadFile method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<string> GetDownloadFileUrlAsync(string fileId, GetDownloadFileUrlQueryParams? queryParams = default, GetDownloadFileUrlHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetDownloadFileUrlQueryParams();
            headers = headers ?? new GetDownloadFileUrlHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "version", StringUtils.ToStringRepresentation(queryParams.Version) }, { "access_token", StringUtils.ToStringRepresentation(queryParams.AccessTokenField) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "range", StringUtils.ToStringRepresentation(headers.Range) }, { "boxapi", StringUtils.ToStringRepresentation(headers.Boxapi) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/files/", StringUtils.ToStringRepresentation(fileId), "/content"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken, FollowRedirects = false }).ConfigureAwait(false);
            if (response.Headers.ContainsKey("location")) {
                return NullableUtils.Unwrap(response.Headers["location"]);
            }
            if (response.Headers.ContainsKey("Location")) {
                return NullableUtils.Unwrap(response.Headers["Location"]);
            }
            throw new BoxSdkException(message: "No location header in response");
        }

        /// <summary>
        /// Returns the contents of a file in binary format.
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
        /// <param name="queryParams">
        /// Query parameters of downloadFile method
        /// </param>
        /// <param name="headers">
        /// Headers of downloadFile method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<System.IO.Stream?> DownloadFileAsync(string fileId, DownloadFileQueryParams? queryParams = default, DownloadFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new DownloadFileQueryParams();
            headers = headers ?? new DownloadFileHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "version", StringUtils.ToStringRepresentation(queryParams.Version) }, { "access_token", StringUtils.ToStringRepresentation(queryParams.AccessTokenField) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "range", StringUtils.ToStringRepresentation(headers.Range) }, { "boxapi", StringUtils.ToStringRepresentation(headers.Boxapi) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/files/", StringUtils.ToStringRepresentation(fileId), "/content"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Binary) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            if (StringUtils.ToStringRepresentation(response.Status) == "202") {
                return null;
            }
            return response.Content;
        }

    }
}