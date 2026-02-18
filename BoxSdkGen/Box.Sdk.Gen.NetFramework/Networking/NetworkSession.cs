using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen
{
    /// <summary>
    /// NetworkSession holding parameters for the network connection like retry strategy.
    /// </summary>
    public class NetworkSession
    {
        /// <summary>
        /// Additional headers, which are appended to each API request
        /// </summary>
        public Dictionary<string, string> AdditionalHeaders { get; set; }

        /// <summary>
        /// Custom base urls.
        /// </summary>
        public BaseUrls BaseUrls { get; } = new BaseUrls();

        /// <summary>
        /// IRetryStrategy used when retrying http/s request.
        /// </summary>
        public IRetryStrategy RetryStrategy { get; set; } = new BoxRetryStrategy();

        /// <summary>
        /// Proxy configuration
        /// </summary>
        public ProxyConfig proxyConfig { get; set; }

        /// <summary>
        /// Network client used to make API calls.
        /// </summary>
        public INetworkClient NetworkClient { get; set; } = new BoxNetworkClient();

        /// <summary>
        /// Timeout configuration used for each API call.
        /// </summary>
        public TimeoutConfig TimeoutConfig { get; set; }

        /// <summary>
        /// Data sanitizer used to sanitize sensitive data for logging.
        /// </summary>
        public DataSanitizer DataSanitizer { get; set; } = new DataSanitizer();

        public NetworkSession(Dictionary<string, string> additionalHeaders = default, BaseUrls baseUrls = null)
        {
            AdditionalHeaders = additionalHeaders ?? new Dictionary<string, string>();
            BaseUrls = baseUrls ?? new BaseUrls();
        }

        /// <summary>
        /// Generate a fresh network session by duplicating the existing configuration and network parameters,
        /// while also including a custom network client to be used for each API call.
        /// </summary>
        public NetworkSession WithNetworkClient(INetworkClient networkClient)
        {
            return new NetworkSession(this.AdditionalHeaders, this.BaseUrls) { RetryStrategy = this.RetryStrategy, proxyConfig = this.proxyConfig, NetworkClient = networkClient, TimeoutConfig = this.TimeoutConfig, DataSanitizer = this.DataSanitizer };
        }

        /// <summary>
        /// Generate a fresh network session by duplicating the existing configuration and network parameters,
        /// while also including additional headers to be attached to every API call.
        /// </summary>
        /// <param name="additionalHeaders">
        /// Map of headers, which are appended to each API request
        /// </param>
        public NetworkSession WithAdditionalHeaders(Dictionary<string, string> additionalHeaders)
        {
            return new NetworkSession(DictionaryUtils.MergeDictionaries(this.AdditionalHeaders, additionalHeaders), this.BaseUrls) { RetryStrategy = this.RetryStrategy, proxyConfig = this.proxyConfig, NetworkClient = this.NetworkClient, TimeoutConfig = this.TimeoutConfig, DataSanitizer = this.DataSanitizer };
        }

        /// <summary>
        /// Generate a fresh network session by duplicating the existing configuration and network parameters,
        /// while using custom base urls for the API calls.
        /// </summary>
        /// <param name="baseUrls">
        /// Custom base urls.
        /// </param>
        public NetworkSession WithCustomBaseUrls(BaseUrls baseUrls)
        {
            return new NetworkSession(this.AdditionalHeaders, baseUrls) { RetryStrategy = this.RetryStrategy, proxyConfig = this.proxyConfig, NetworkClient = this.NetworkClient, TimeoutConfig = this.TimeoutConfig, DataSanitizer = this.DataSanitizer };
        }

        /// <summary>
        /// Generate a fresh network session by duplicating the existing configuration and network parameters,
        /// while also including a proxy to be used for each API call.
        /// </summary>
        /// <param name="proxyConfig">
        /// Proxy configuration.
        /// </param>
        public NetworkSession WithProxy(ProxyConfig config)
        {
            return new NetworkSession(this.AdditionalHeaders, this.BaseUrls) { RetryStrategy = this.RetryStrategy, proxyConfig = config, NetworkClient = this.NetworkClient, TimeoutConfig = this.TimeoutConfig, DataSanitizer = this.DataSanitizer };
        }

        /// <summary>
        /// Generate a fresh network session by duplicating the existing configuration and network parameters,
        /// while also including a timeout configuration to be used for each API call.
        /// </summary>
        /// <param name="config">
        /// Timeout configuration.
        /// </param>
        public NetworkSession WithTimeoutConfig(TimeoutConfig timeoutConfig)
        {
            return new NetworkSession(this.AdditionalHeaders, this.BaseUrls) { RetryStrategy = this.RetryStrategy, proxyConfig = this.proxyConfig, NetworkClient = this.NetworkClient, TimeoutConfig = timeoutConfig, DataSanitizer = this.DataSanitizer };
        }

        /// <summary>
        /// Generate a fresh network session by duplicating the existing configuration and network parameters,
        /// while also including a data sanitizer to be used to sanitize sensitive data for logging.
        public NetworkSession WithDataSanitizer(DataSanitizer dataSanitizer)
        {
            return new NetworkSession(this.AdditionalHeaders, this.BaseUrls) { RetryStrategy = this.RetryStrategy, proxyConfig = this.proxyConfig, NetworkClient = this.NetworkClient, TimeoutConfig = this.TimeoutConfig, DataSanitizer = dataSanitizer };
        }
    }
}
