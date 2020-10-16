using System;
using System.Net;

namespace Box.V2.Config
{
    public interface IBoxConfig
    {
        Uri BoxApiHostUri { get; set; }
        Uri BoxApiUri { get; set; }
        Uri BoxUploadApiUri { get; set; }

        string ClientId { get; }
        string ConsumerKey { get; }
        string ClientSecret { get; }
        Uri RedirectUri { get; }

        string EnterpriseId { get; }
        string JWTPrivateKey { get; }
        string JWTPrivateKeyPassword { get; }
        string JWTPublicKeyId { get; }

        string DeviceId { get; set; }
        string DeviceName { get; set; }
        string UserAgent { get; set; }

        /// <summary>
        /// Sends compressed responses from Box for faster response times
        /// </summary>
        CompressionType? AcceptEncoding { get; }

        Uri AuthCodeBaseUri { get; }
        Uri AuthCodeUri { get; }
        Uri FoldersEndpointUri { get; }
        Uri FilesEndpointUri { get; }
        Uri FilesUploadEndpointUri { get; }

        /// <summary>
        /// Upload session
        /// </summary>
        Uri FilesUploadSessionEndpointUri { get; }

        Uri FilesPreflightCheckUri { get; }
        //Uri FilesPreflightCheckNewVersionUri { get; }
        Uri CommentsEndpointUri { get; }
        Uri SearchEndpointUri { get; }
        Uri UserEndpointUri { get; }
        Uri InviteEndpointUri { get; }
        Uri CollaborationsEndpointUri { get; }
        Uri GroupsEndpointUri { get; }
        Uri GroupMembershipEndpointUri { get; }
        Uri RetentionPoliciesEndpointUri { get; }
        Uri RetentionPolicyAssignmentsUri { get; }
        Uri FileVersionRetentionsUri { get; }
        Uri EventsUri { get; }
        Uri MetadataTemplatesUri { get; }
        Uri MetadataQueryUri { get; }
        Uri CreateMetadataTemplateUri { get; }
        Uri WebhooksUri { get; }
        Uri RecentItemsUri { get; }
        Uri EnterprisesUri { get; }
        Uri DevicePinUri { get; }
        Uri CollaborationWhitelistEntryUri { get; }
        Uri CollaborationWhitelistTargetEntryUri { get; }
        Uri TermsOfServicesUri { get; }
        Uri TermsOfServiceUserStatusesUri { get; }
        Uri MetadataCascadePolicyUri { get; }
        Uri StoragePoliciesUri { get; }
        Uri StoragePolicyAssignmentsUri { get; }
        Uri StoragePolicyAssignmentsForTargetUri { get; }

        /// <summary>
        /// Gets the shared items endpoint URI.
        /// </summary>
        /// <value>
        /// The shared items endpoint URI.
        /// </value>
        Uri SharedItemsUri { get; }

        /// <summary>
        /// Gets the task assignments endpoint URI.
        /// </summary>
        /// <value>
        /// The task assignments endpoint URI.
        /// </value>
        Uri TaskAssignmentsEndpointUri { get; }

        /// <summary>
        /// Gets the tasks endpoint URI.
        /// </summary>
        Uri TasksEndpointUri { get; }

        /// <summary>
        /// Gets the collections endpoint URI.
        /// </summary>
        /// <value>
        /// The collections endpoint URI.
        /// </value>
        Uri CollectionsEndpointUri { get; }
        /// <summary>
        /// Gets the web links endpoint URI.
        /// </summary>
        Uri WebLinksEndpointUri { get; }
        /// <summary>
        /// Gets the legal hold policies endpoint URI.
        /// </summary>
        Uri LegalHoldPoliciesEndpointUri { get; }
        /// <summary>
        /// Gets the legal hold policy assignments endpoint URI.
        /// </summary>
        Uri LegalHoldPolicyAssignmentsEndpointUri { get; }
        /// <summary>
        /// Gets the file viersion legal holds endpoint URI.
        /// </summary>
        Uri FileVersionLegalHoldsEndpointUri { get; }
        /// <summary>
        /// Gets the zip downloads endpoint URI.
        /// </summary>
        Uri ZipDownloadsEndpointUri { get; }

        /// <summary>
        /// The web proxy for HttpRequestHandler
        /// </summary>
        IWebProxy WebProxy { get; set; }
    }
}
