using System;
using System.IO;
using System.Net;
using System.Text;
using Box.V2.Utility;
using Newtonsoft.Json.Linq;

namespace Box.V2.Config
{
    public sealed class BoxConfig : IBoxConfig
    {
        internal static string DefaultUserAgent = "Box Windows SDK v" + AssemblyInfo.NuGetVersion;

        /// <summary>
        /// Instantiates a Box config with all of the standard defaults
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"></param>
        public BoxConfig(string clientId, string clientSecret, Uri redirectUri)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
            UserAgent = DefaultUserAgent;
        }

        /// <summary>
        /// Instantiates a Box config for use with JWT authentication
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="enterpriseId"></param>
        /// <param name="jwtPrivateKey"></param>
        /// <param name="jwtPrivateKeyPassword"></param>
        /// <param name="jwtPublicKeyId"></param>
        public BoxConfig(string clientId, string clientSecret, string enterpriseId,
            string jwtPrivateKey, string jwtPrivateKeyPassword, string jwtPublicKeyId)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            EnterpriseId = enterpriseId;
            JWTPrivateKey = jwtPrivateKey;
            JWTPrivateKeyPassword = jwtPrivateKeyPassword;
            JWTPublicKeyId = jwtPublicKeyId;
            UserAgent = DefaultUserAgent;
        }

        public BoxConfig(BoxConfigBuilder builder)
        {
            ClientId = builder.ClientId;
            ClientSecret = builder.ClientSecret;
            EnterpriseId = builder.EnterpriseId;
            JWTPrivateKey = builder.JWTPrivateKey;
            JWTPrivateKeyPassword = builder.JWTPrivateKeyPassword;
            JWTPublicKeyId = builder.JWTPublicKeyId;
            UserAgent = builder.UserAgent;
            BoxApiHostUri = builder.BoxApiHostUri;
            BoxAccountApiHostUri = builder.BoxAccountApiHostUri;
            BoxApiUri = builder.BoxApiUri;
            BoxUploadApiUri = builder.BoxUploadApiUri;
            RedirectUri = builder.RedirectUri;
            DeviceId = builder.DeviceId;
            DeviceName = builder.DeviceName;
            AcceptEncoding = builder.AcceptEncoding;
            WebProxy = builder.WebProxy;
            Timeout = builder.Timeout;
            RetryStrategy = builder.RetryStrategy;
            JWTAudience = builder.JWTAudience;
        }

        /// <summary>
        /// Create BoxConfig from json file.
        /// </summary>
        /// <param name="jsonFile">json file stream.</param>
        /// <returns>IBoxConfig instance.</returns>
        public static IBoxConfig CreateFromJsonFile(Stream jsonFile)
        {
            var jsonString = string.Empty;

            using (var reader = new StreamReader(jsonFile, Encoding.UTF8))
            {
                jsonString = reader.ReadToEnd();
            }

            return CreateFromJsonString(jsonString);
        }

        /// <summary>
        /// Create BoxConfig from json string
        /// </summary>
        /// <param name="jsonString">json string.</param>
        /// <returns>IBoxConfig instance.</returns>
        public static IBoxConfig CreateFromJsonString(string jsonString)
        {
            var json = JObject.Parse(jsonString);

            string clientId = null;
            string clientSecret = null;
            string enterpriseId = null;
            string privateKey = null;
            string rsaSecret = null;
            string publicKeyId = null;

            if (json["boxAppSettings"] != null)
            {
                var boxAppSettings = json["boxAppSettings"];

                if (boxAppSettings["clientID"] != null)
                {
                    clientId = boxAppSettings["clientID"].ToString();
                }

                if (boxAppSettings["clientSecret"] != null)
                {
                    clientSecret = boxAppSettings["clientSecret"].ToString();
                }

                if (boxAppSettings["appAuth"] != null)
                {
                    var appAuth = boxAppSettings["appAuth"];

                    if (appAuth["privateKey"] != null)
                    {
                        privateKey = appAuth["privateKey"].ToString();
                    }

                    if (appAuth["passphrase"] != null)
                    {
                        rsaSecret = appAuth["passphrase"].ToString();
                    }

                    if (appAuth["publicKeyID"] != null)
                    {
                        publicKeyId = appAuth["publicKeyID"].ToString();
                    }
                }
            }

            if (json["enterpriseID"] != null)
            {
                enterpriseId = json["enterpriseID"].ToString();
            }

            return new BoxConfig(clientId, clientSecret, enterpriseId, privateKey, rsaSecret, publicKeyId);
        }

        public Uri BoxApiHostUri { get; private set; } = new Uri(Constants.BoxApiHostUriString);
        public Uri BoxAccountApiHostUri { get; private set; } = new Uri(Constants.BoxAccountApiHostUriString);
        public Uri BoxUploadApiUri { get; private set; } = new Uri(new Uri(Constants.BoxUploadApiUriWithoutVersionString), Constants.BoxApiCurrentVersionUriString);

        private Uri _boxApiUri;
        public Uri BoxApiUri
        {
            get { return _boxApiUri ?? new Uri(BoxApiHostUri, Constants.BoxApiCurrentVersionUriString); }
            private set { _boxApiUri = value; }
        }

        private string _jwtAudience;

        /// <summary>
        /// Audience claim for JWT token. 
        /// </summary>
        public string JWTAudience
        {
            get { return _jwtAudience ?? Constants.BoxAuthTokenApiUriString; }
            private set { _jwtAudience = value; }
        }

        public string ClientId { get; private set; }
        public string ConsumerKey { get; private set; }
        public string ClientSecret { get; private set; }
        public Uri RedirectUri { get; private set; }

        public string EnterpriseId { get; private set; }
        public string JWTPrivateKey { get; private set; }
        public string JWTPrivateKeyPassword { get; private set; }
        public string JWTPublicKeyId { get; private set; }

        public string DeviceId { get; private set; }
        public string DeviceName { get; private set; }
        public string UserAgent { get; private set; }

        /// <summary>
        /// Sends compressed responses from Box for faster response times
        /// </summary>
        public CompressionType? AcceptEncoding { get; private set; }

        public Uri AuthCodeBaseUri { get { return new Uri(BoxAccountApiHostUri, Constants.AuthCodeString); } }
        public Uri AuthCodeUri { get { return new Uri(AuthCodeBaseUri, string.Format("?response_type=code&client_id={0}&redirect_uri={1}", ClientId, RedirectUri)); } }
        public Uri FoldersEndpointUri { get { return new Uri(BoxApiUri, Constants.FoldersString); } }
        public Uri TermsOfServicesUri { get { return new Uri(BoxApiUri, Constants.TermsOfServicesString); } }
        public Uri TermsOfServiceUserStatusesUri { get { return new Uri(BoxApiUri, Constants.TermsOfServiceUserStatusesString); } }
        public Uri FilesEndpointUri { get { return new Uri(BoxApiUri, Constants.FilesString); } }
        public Uri FilesUploadEndpointUri { get { return new Uri(BoxUploadApiUri, Constants.FilesUploadString); } }

        /// <summary>
        /// Upload session
        /// </summary>
        public Uri FilesUploadSessionEndpointUri { get { return new Uri(BoxUploadApiUri, Constants.FilesUploadSessionString); } }
        public Uri FilesPreflightCheckUri { get { return new Uri(BoxApiUri, Constants.FilesUploadString); } }
        public Uri CommentsEndpointUri { get { return new Uri(BoxApiUri, Constants.CommentsString); } }
        public Uri SearchEndpointUri { get { return new Uri(BoxApiUri, Constants.SearchString); } }
        public Uri UserEndpointUri { get { return new Uri(BoxApiUri, Constants.UserString); } }
        public Uri InviteEndpointUri { get { return new Uri(BoxApiUri, Constants.InviteString); } }
        public Uri CollaborationsEndpointUri { get { return new Uri(BoxApiUri, Constants.CollaborationsString); } }
        public Uri GroupsEndpointUri { get { return new Uri(BoxApiUri, Constants.GroupsString); } }
        public Uri GroupMembershipEndpointUri { get { return new Uri(BoxApiUri, Constants.GroupMembershipString); } }
        public Uri RetentionPoliciesEndpointUri { get { return new Uri(BoxApiUri, Constants.RetentionPoliciesString); } }
        public Uri RetentionPolicyAssignmentsUri { get { return new Uri(BoxApiUri, Constants.RetentionPolicyAssignmentsString); } }
        public Uri FileVersionRetentionsUri { get { return new Uri(BoxApiUri, Constants.FileVersionRetentionsString); } }
        public Uri EventsUri { get { return new Uri(BoxApiUri, Constants.EventsString); } }
        public Uri MetadataTemplatesUri { get { return new Uri(BoxApiUri, Constants.MetadataTemplatesString); } }
        public Uri CreateMetadataTemplateUri { get { return new Uri(BoxApiUri, Constants.CreateMetadataTemplateString); } }
        public Uri MetadataQueryUri { get { return new Uri(BoxApiUri, Constants.MetadataQueryString); } }
        public Uri WebhooksUri { get { return new Uri(BoxApiUri, Constants.WebhooksString); } }
        public Uri RecentItemsUri { get { return new Uri(BoxApiUri, Constants.RecentItemsString); } }
        public Uri EnterprisesUri { get { return new Uri(BoxApiUri, Constants.EnterprisesString); } }
        public Uri DevicePinUri { get { return new Uri(BoxApiUri, Constants.DevicePinString); } }
        public Uri CollaborationWhitelistEntryUri { get { return new Uri(BoxApiUri, Constants.CollaborationWhitelistEntryString); } }
        public Uri CollaborationWhitelistTargetEntryUri { get { return new Uri(BoxApiUri, Constants.CollaborationWhitelistTargetEntryString); } }
        public Uri MetadataCascadePolicyUri { get { return new Uri(BoxApiUri, Constants.MetadataCascadePoliciesString); } }
        public Uri StoragePoliciesUri { get { return new Uri(BoxApiUri, Constants.StoragePoliciesString); } }
        public Uri StoragePolicyAssignmentsUri { get { return new Uri(BoxApiUri, Constants.StoragePolicyAssignmentsString); } }
        public Uri StoragePolicyAssignmentsForTargetUri { get { return new Uri(BoxApiUri, Constants.StoragePolicyAssignmentsForTargetString); } }

        /// <summary>
        /// Gets the shared items endpoint URI.
        /// </summary>
        /// <value>
        /// The shared items endpoint URI.
        /// </value>
        public Uri SharedItemsUri { get { return new Uri(BoxApiUri, Constants.SharedItemsString); } }

        /// <summary>
        /// Gets the task assignments endpoint URI.
        /// </summary>
        /// <value>
        /// The task assignments endpoint URI.
        /// </value>
        public Uri TaskAssignmentsEndpointUri { get { return new Uri(BoxApiUri, Constants.TaskAssignmentsString); } }

        /// <summary>
        /// Gets the tasks endpoint URI.
        /// </summary>
        public Uri TasksEndpointUri { get { return new Uri(BoxApiUri, Constants.TasksString); } }

        /// <summary>
        /// Gets the collections endpoint URI.
        /// </summary>
        /// <value>
        /// The collections endpoint URI.
        /// </value>
        public Uri CollectionsEndpointUri { get { return new Uri(BoxApiUri, Constants.CollectionsString); } }
        /// <summary>
        /// Gets the web links endpoint URI.
        /// </summary>
        public Uri WebLinksEndpointUri { get { return new Uri(BoxApiUri, Constants.WebLinksString); } }
        /// <summary>
        /// Gets the legal hold policies endpoint URI.
        /// </summary>
        public Uri LegalHoldPoliciesEndpointUri { get { return new Uri(BoxApiUri, Constants.LegalHoldPoliciesString); } }
        /// <summary>
        /// Gets the legal hold policies endpoint URI.
        /// </summary>
        public Uri LegalHoldPolicyAssignmentsEndpointUri { get { return new Uri(BoxApiUri, Constants.LegalHoldPolicyAssignmentsString); } }
        /// <summary>
        /// Gets the file viersion legal holds endpoint URI.
        /// </summary>
        public Uri FileVersionLegalHoldsEndpointUri { get { return new Uri(BoxApiUri, Constants.FileVersionLegalHoldsString); } }
        /// <summary>
        /// Gets the zip downloads endpoint URI.
        /// </summary>
        public Uri ZipDownloadsEndpointUri { get { return new Uri(BoxApiUri, Constants.ZipDownloadsString); } }
        /// <summary>
        /// Gets the folder locks endpoint URI.
        /// </summary>
        public Uri FolderLocksEndpointUri { get { return new Uri(BoxApiUri, Constants.FolderLocksString); } }
        /// <summary>
        /// Gets the sign requests endpoint URI.
        /// </summary>
        public Uri SignRequestsEndpointUri { get { return new Uri(BoxApiUri, Constants.SignRequestsString); } }
        /// <summary>
        /// Gets the sign requests endpoint URI.
        /// </summary>
        public Uri SignRequestsEndpointWithPathUri { get { return new Uri(BoxApiUri, Constants.SignRequestsWithPathString); } }
        /// <summary>
        /// Gets the sign templates endpoint URI.
        /// </summary>
        public Uri SignTemplatesEndpointUri { get { return new Uri(BoxApiUri, Constants.SignTemplatesString); } }
        /// <summary>
        /// Gets the sign templates endpoint URI with path.
        /// </summary>
        public Uri SignTemplatesEndpointWithPathUri { get { return new Uri(BoxApiUri, Constants.SignTemplatesWithPathString); } }
        /// <summary>
        /// Gets the file requests endpoint URI.
        /// </summary>
        public Uri FileRequestsEndpointWithPathUri { get { return new Uri(BoxApiUri, Constants.FileRequestsWithPathString); } }
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
    }

    public enum CompressionType
    {
        gzip,
        deflate
    }
}
