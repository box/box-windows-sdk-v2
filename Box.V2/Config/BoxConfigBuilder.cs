using System;
using System.IO;
using System.Net;
using Box.V2.Utility;

namespace Box.V2.Config
{
    /// <summary>
    /// Builder used to instantiate new BoxConfig. It follows FluentBuilder pattern and can be used by chaining methods.
    /// </summary>
    public class BoxConfigBuilder
    {
        /// <summary>
        /// Instantiates a BoxConfigBuilder with all of the standard defaults
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"></param>
        /// <returns>BoxConfigBuilder instance.</returns>
        public BoxConfigBuilder(string clientId, string clientSecret, Uri redirectUri)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
            UserAgent = BoxConfig.DefaultUserAgent;
        }

        /// <summary>
        /// Instantiates a BoxConfigBuilder for use with JWT authentication
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="enterpriseId"></param>
        /// <param name="jwtPrivateKey"></param>
        /// <param name="jwtPrivateKeyPassword"></param>
        /// <param name="jwtPublicKeyId"></param>
        /// <returns>BoxConfigBuilder instance.</returns>
        public BoxConfigBuilder(string clientId, string clientSecret, string enterpriseId,
            string jwtPrivateKey, string jwtPrivateKeyPassword, string jwtPublicKeyId)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            EnterpriseId = enterpriseId;
            JWTPrivateKey = jwtPrivateKey;
            JWTPrivateKeyPassword = jwtPrivateKeyPassword;
            JWTPublicKeyId = jwtPublicKeyId;
            UserAgent = BoxConfig.DefaultUserAgent;
        }

        /// <summary>
        /// Instantiates a BoxConfigBuilder for use with CCG authentication
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns>BoxConfigBuilder instance.</returns>
        public BoxConfigBuilder(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            UserAgent = BoxConfig.DefaultUserAgent;
        }

        /// <summary>
        /// Create BoxConfigBuilder from json file.
        /// </summary>
        /// <param name="jsonFile">json file stream.</param>
        /// <returns>BoxConfigBuilder instance.</returns>
        public static BoxConfigBuilder CreateFromJsonFile(Stream jsonFile)
        {
            var config = BoxConfig.CreateFromJsonFile(jsonFile);
            var configBuilder = new BoxConfigBuilder();
            RewritePropertiesToBuilder(configBuilder, config);
            return configBuilder;
        }

        /// <summary>
        /// Create BoxConfigBuilder from json string
        /// </summary>
        /// <param name="jsonString">json string.</param>
        /// <returns>BoxConfigBuilder instance.</returns>
        public static BoxConfigBuilder CreateFromJsonString(string jsonString)
        {
            var config = BoxConfig.CreateFromJsonString(jsonString);
            var configBuilder = new BoxConfigBuilder();
            RewritePropertiesToBuilder(configBuilder, config);
            return configBuilder;
        }

        private static void RewritePropertiesToBuilder(BoxConfigBuilder configBuilder, IBoxConfig config)
        {
            configBuilder.ClientId = config.ClientId;
            configBuilder.ClientSecret = config.ClientSecret;
            configBuilder.JWTPrivateKey = config.JWTPrivateKey;
            configBuilder.JWTPrivateKeyPassword = config.JWTPrivateKeyPassword;
            configBuilder.JWTPublicKeyId = config.JWTPublicKeyId;
            configBuilder.EnterpriseId = config.EnterpriseId;
        }

        private BoxConfigBuilder() { }

        /// <summary>
        /// Create IBoxConfig from the builder.
        /// </summary>
        /// <returns>IBoxConfig instance.</returns>
        public IBoxConfig Build()
        {
            return new BoxConfig(this);
        }

        /// <summary>
        /// Sets user agent.
        /// </summary>
        /// <param name="userAgent">User agent.</param>
        /// <returns>this BoxConfigBuilder object for chaining</returns>
        public BoxConfigBuilder SetUserAgent(string userAgent)
        {
            UserAgent = userAgent;
            return this;
        }

        /// <summary>
        /// Sets BoxAPI host uri.
        /// </summary>
        /// <param name="boxApiHostUri">BoxAPI host uri.</param>
        /// <returns>this BoxConfigBuilder object for chaining</returns>
        public BoxConfigBuilder SetBoxApiHostUri(Uri boxApiHostUri)
        {
            BoxApiHostUri = EnsureEndsWithSlash(boxApiHostUri);
            return this;
        }

        /// <summary>
        /// Sets BoxAPI account host uri.
        /// </summary>
        /// <param name="boxAccountApiHostUri">BoxAPI account host uri.</param>
        /// <returns>this BoxConfigBuilder object for chaining</returns>
        public BoxConfigBuilder SetBoxAccountApiHostUri(Uri boxAccountApiHostUri)
        {
            BoxAccountApiHostUri = EnsureEndsWithSlash(boxAccountApiHostUri);
            return this;
        }

        /// <summary>
        /// Sets BoxAPI upload uri.
        /// </summary>
        /// <param name="boxUploadApiUri">BoxAPI upload uri.</param>
        /// <returns>this BoxConfigBuilder object for chaining</returns>
        public BoxConfigBuilder SetBoxUploadApiUri(Uri boxUploadApiUri)
        {
            BoxUploadApiUri = EnsureEndsWithSlash(boxUploadApiUri);
            return this;
        }

        /// <summary>
        /// Sets redirect uri.
        /// </summary>
        /// <param name="redirectUri">Redirect uri.</param>
        /// <returns>this BoxConfigBuilder object for chaining</returns>
        public BoxConfigBuilder SetRedirectUri(Uri redirectUri)
        {
            RedirectUri = redirectUri;
            return this;
        }

        /// <summary>
        /// Sets device id.
        /// </summary>
        /// <param name="deviceId">Device id.</param>
        /// <returns>this BoxConfigBuilder object for chaining</returns>
        public BoxConfigBuilder SetDeviceId(string deviceId)
        {
            DeviceId = deviceId;
            return this;
        }

        /// <summary>
        /// Sets device name.
        /// </summary>
        /// <param name="deviceName">Device name.</param>
        /// <returns>this BoxConfigBuilder object for chaining</returns>
        public BoxConfigBuilder SetDeviceName(string deviceName)
        {
            DeviceName = deviceName;
            return this;
        }

        /// <summary>
        /// Sets acceptEncoding.
        /// </summary>
        /// <param name="acceptEncoding">AcceptEncoding.</param>
        /// <returns>this BoxConfigBuilder object for chaining</returns>
        public BoxConfigBuilder SetAcceptEncoding(CompressionType? acceptEncoding)
        {
            AcceptEncoding = acceptEncoding;
            return this;
        }

        /// <summary>
        /// Sets web proxy for HttpRequestHandler.
        /// </summary>
        /// <param name="webProxy">Web proxy for HttpRequestHandler.</param>
        /// <returns>this BoxConfigBuilder object for chaining</returns>
        public BoxConfigBuilder SetWebProxy(IWebProxy webProxy)
        {
            WebProxy = webProxy;
            return this;
        }

        /// <summary>
        /// Sets connection timeout for HttpRequestHandler.
        /// </summary>
        /// <param name="timeout">Connection timeout for HttpRequestHandler.</param>
        /// <returns>this BoxConfigBuilder object for chaining</returns>
        public BoxConfigBuilder SetTimeout(TimeSpan timeout)
        {
            Timeout = timeout;
            return this;
        }

        /// <summary>
        /// Sets enterprise id.
        /// </summary>
        /// <param name="enterpriseId">Enteprise id.</param>
        /// <returns>this BoxConfigBuilder object for chaining</returns>
        public BoxConfigBuilder SetEnterpriseId(string enterpriseId)
        {
            EnterpriseId = enterpriseId;
            return this;
        }

        /// <summary>
        /// Sets retry strategy.
        /// </summary>
        /// <param name="enterpriseId">Retry strategy.</param>
        /// <returns>this BoxConfigBuilder object for chaining</returns>
        public BoxConfigBuilder SetRetryStrategy(IRetryStrategy retryStrategy)
        {
            RetryStrategy = retryStrategy;
            return this;
        }

        /// <summary>
        /// Sets audience claim used in JWT tokens.
        /// </summary>
        /// <param name="jwtAudience">Audience claim value</param>
        /// <returns>this BoxConfigBuilder object for chaining</returns>
        public BoxConfigBuilder SetJWTAudience(string jwtAudience)
        {
            _jwtAudience = jwtAudience;
            return this;
        }

        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string EnterpriseId { get; private set; }
        public string JWTPrivateKey { get; private set; }
        public string JWTPrivateKeyPassword { get; private set; }
        public string JWTPublicKeyId { get; private set; }
        public string UserAgent { get; private set; }

        public Uri BoxApiHostUri { get; private set; } = new Uri(Constants.BoxApiHostUriString);
        public Uri BoxAccountApiHostUri { get; private set; } = new Uri(Constants.BoxAccountApiHostUriString);
        public Uri BoxUploadApiUri { get; private set; } = new Uri(new Uri(Constants.BoxUploadApiUriWithoutVersionString), Constants.BoxApiCurrentVersionUriString);

        private Uri _boxApiUri;
        public Uri BoxApiUri
        {
            get { return _boxApiUri ?? new Uri(BoxApiHostUri, Constants.BoxApiCurrentVersionUriString); }
            private set { _boxApiUri = value; }
        }

        public Uri RedirectUri { get; private set; }
        public string DeviceId { get; private set; }
        public string DeviceName { get; private set; }

        /// <summary>
        /// Sends compressed responses from Box for faster response times
        /// </summary>
        public CompressionType? AcceptEncoding { get; private set; }

        /// <summary>
        /// The web proxy for HttpRequestHandler
        /// </summary>
        public IWebProxy WebProxy { get; private set; }

        /// <summary>
        /// Timeout for the connection
        /// </summary>
        public TimeSpan? Timeout { get; private set; }

        /// <summary>
        /// Retry strategy for failed requests
        /// </summary>
        public IRetryStrategy RetryStrategy { get; private set; } = new ExponentialBackoff();

        private string _jwtAudience;

        /// <summary>
        /// Audience claim for JWT token. 
        /// </summary>
        public string JWTAudience
        {
            get { return _jwtAudience ?? Constants.BoxAuthTokenApiUriString; }
            private set { _jwtAudience = value; }
        }

        private Uri EnsureEndsWithSlash(Uri uri)
        {
            return uri.ToString().EndsWith("/") ? uri : new Uri($"{uri}{"/"}");
        }
    }
}
