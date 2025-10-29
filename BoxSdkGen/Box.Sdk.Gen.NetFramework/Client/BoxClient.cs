using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class BoxClient {
        public IAuthentication Auth { get; set; }

        public NetworkSession NetworkSession { get; set; }

        public AuthorizationManager Authorization { get; set; }

        public FilesManager Files { get; set; }

        public TrashedFilesManager TrashedFiles { get; set; }

        public AppItemAssociationsManager AppItemAssociations { get; set; }

        public DownloadsManager Downloads { get; set; }

        public UploadsManager Uploads { get; set; }

        public ChunkedUploadsManager ChunkedUploads { get; set; }

        public ListCollaborationsManager ListCollaborations { get; set; }

        public CommentsManager Comments { get; set; }

        public TasksManager Tasks { get; set; }

        public FileVersionsManager FileVersions { get; set; }

        public FileMetadataManager FileMetadata { get; set; }

        public FileClassificationsManager FileClassifications { get; set; }

        public SkillsManager Skills { get; set; }

        public FileWatermarksManager FileWatermarks { get; set; }

        public FileRequestsManager FileRequests { get; set; }

        public FoldersManager Folders { get; set; }

        public TrashedFoldersManager TrashedFolders { get; set; }

        public FolderMetadataManager FolderMetadata { get; set; }

        public FolderClassificationsManager FolderClassifications { get; set; }

        public TrashedItemsManager TrashedItems { get; set; }

        public FolderWatermarksManager FolderWatermarks { get; set; }

        public FolderLocksManager FolderLocks { get; set; }

        public MetadataTemplatesManager MetadataTemplates { get; set; }

        public ClassificationsManager Classifications { get; set; }

        public MetadataCascadePoliciesManager MetadataCascadePolicies { get; set; }

        public SearchManager Search { get; set; }

        public UserCollaborationsManager UserCollaborations { get; set; }

        public TaskAssignmentsManager TaskAssignments { get; set; }

        public SharedLinksFilesManager SharedLinksFiles { get; set; }

        public SharedLinksFoldersManager SharedLinksFolders { get; set; }

        public WebLinksManager WebLinks { get; set; }

        public TrashedWebLinksManager TrashedWebLinks { get; set; }

        public SharedLinksWebLinksManager SharedLinksWebLinks { get; set; }

        public SharedLinksAppItemsManager SharedLinksAppItems { get; set; }

        public UsersManager Users { get; set; }

        public SessionTerminationManager SessionTermination { get; set; }

        public AvatarsManager Avatars { get; set; }

        public TransferManager Transfer { get; set; }

        public EmailAliasesManager EmailAliases { get; set; }

        public MembershipsManager Memberships { get; set; }

        public InvitesManager Invites { get; set; }

        public GroupsManager Groups { get; set; }

        public WebhooksManager Webhooks { get; set; }

        public EventsManager Events { get; set; }

        public CollectionsManager Collections { get; set; }

        public RecentItemsManager RecentItems { get; set; }

        public RetentionPoliciesManager RetentionPolicies { get; set; }

        public RetentionPolicyAssignmentsManager RetentionPolicyAssignments { get; set; }

        public LegalHoldPoliciesManager LegalHoldPolicies { get; set; }

        public LegalHoldPolicyAssignmentsManager LegalHoldPolicyAssignments { get; set; }

        public FileVersionRetentionsManager FileVersionRetentions { get; set; }

        public FileVersionLegalHoldsManager FileVersionLegalHolds { get; set; }

        public ShieldInformationBarriersManager ShieldInformationBarriers { get; set; }

        public ShieldInformationBarrierReportsManager ShieldInformationBarrierReports { get; set; }

        public ShieldInformationBarrierSegmentsManager ShieldInformationBarrierSegments { get; set; }

        public ShieldInformationBarrierSegmentMembersManager ShieldInformationBarrierSegmentMembers { get; set; }

        public ShieldInformationBarrierSegmentRestrictionsManager ShieldInformationBarrierSegmentRestrictions { get; set; }

        public DevicePinnersManager DevicePinners { get; set; }

        public TermsOfServicesManager TermsOfServices { get; set; }

        public TermsOfServiceUserStatusesManager TermsOfServiceUserStatuses { get; set; }

        public CollaborationAllowlistEntriesManager CollaborationAllowlistEntries { get; set; }

        public CollaborationAllowlistExemptTargetsManager CollaborationAllowlistExemptTargets { get; set; }

        public StoragePoliciesManager StoragePolicies { get; set; }

        public StoragePolicyAssignmentsManager StoragePolicyAssignments { get; set; }

        public ZipDownloadsManager ZipDownloads { get; set; }

        public SignRequestsManager SignRequests { get; set; }

        public WorkflowsManager Workflows { get; set; }

        public SignTemplatesManager SignTemplates { get; set; }

        public IntegrationMappingsManager IntegrationMappings { get; set; }

        public AiManager Ai { get; set; }

        public AiStudioManager AiStudio { get; set; }

        public DocgenTemplateManager DocgenTemplate { get; set; }

        public DocgenManager Docgen { get; set; }

        public EnterpriseConfigurationsManager EnterpriseConfigurations { get; set; }

        public HubsManager Hubs { get; set; }

        public HubCollaborationsManager HubCollaborations { get; set; }

        public HubItemsManager HubItems { get; set; }

        public ShieldListsManager ShieldLists { get; set; }

        public ArchivesManager Archives { get; set; }

        public ExternalUsersManager ExternalUsers { get; set; }

        public BoxClient(IAuthentication auth, NetworkSession networkSession = default) {
            Auth = auth;
            NetworkSession = networkSession ?? new NetworkSession(baseUrls: new BaseUrls());
            Authorization = new AuthorizationManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Files = new FilesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            TrashedFiles = new TrashedFilesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            AppItemAssociations = new AppItemAssociationsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Downloads = new DownloadsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Uploads = new UploadsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            ChunkedUploads = new ChunkedUploadsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            ListCollaborations = new ListCollaborationsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Comments = new CommentsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Tasks = new TasksManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            FileVersions = new FileVersionsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            FileMetadata = new FileMetadataManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            FileClassifications = new FileClassificationsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Skills = new SkillsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            FileWatermarks = new FileWatermarksManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            FileRequests = new FileRequestsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Folders = new FoldersManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            TrashedFolders = new TrashedFoldersManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            FolderMetadata = new FolderMetadataManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            FolderClassifications = new FolderClassificationsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            TrashedItems = new TrashedItemsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            FolderWatermarks = new FolderWatermarksManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            FolderLocks = new FolderLocksManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            MetadataTemplates = new MetadataTemplatesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Classifications = new ClassificationsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            MetadataCascadePolicies = new MetadataCascadePoliciesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Search = new SearchManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            UserCollaborations = new UserCollaborationsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            TaskAssignments = new TaskAssignmentsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            SharedLinksFiles = new SharedLinksFilesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            SharedLinksFolders = new SharedLinksFoldersManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            WebLinks = new WebLinksManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            TrashedWebLinks = new TrashedWebLinksManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            SharedLinksWebLinks = new SharedLinksWebLinksManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            SharedLinksAppItems = new SharedLinksAppItemsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Users = new UsersManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            SessionTermination = new SessionTerminationManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Avatars = new AvatarsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Transfer = new TransferManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            EmailAliases = new EmailAliasesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Memberships = new MembershipsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Invites = new InvitesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Groups = new GroupsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Webhooks = new WebhooksManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Events = new EventsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Collections = new CollectionsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            RecentItems = new RecentItemsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            RetentionPolicies = new RetentionPoliciesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            RetentionPolicyAssignments = new RetentionPolicyAssignmentsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            LegalHoldPolicies = new LegalHoldPoliciesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            LegalHoldPolicyAssignments = new LegalHoldPolicyAssignmentsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            FileVersionRetentions = new FileVersionRetentionsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            FileVersionLegalHolds = new FileVersionLegalHoldsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            ShieldInformationBarriers = new ShieldInformationBarriersManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            ShieldInformationBarrierReports = new ShieldInformationBarrierReportsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            ShieldInformationBarrierSegments = new ShieldInformationBarrierSegmentsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            ShieldInformationBarrierSegmentMembers = new ShieldInformationBarrierSegmentMembersManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            ShieldInformationBarrierSegmentRestrictions = new ShieldInformationBarrierSegmentRestrictionsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            DevicePinners = new DevicePinnersManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            TermsOfServices = new TermsOfServicesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            TermsOfServiceUserStatuses = new TermsOfServiceUserStatusesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            CollaborationAllowlistEntries = new CollaborationAllowlistEntriesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            CollaborationAllowlistExemptTargets = new CollaborationAllowlistExemptTargetsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            StoragePolicies = new StoragePoliciesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            StoragePolicyAssignments = new StoragePolicyAssignmentsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            ZipDownloads = new ZipDownloadsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            SignRequests = new SignRequestsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Workflows = new WorkflowsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            SignTemplates = new SignTemplatesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            IntegrationMappings = new IntegrationMappingsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Ai = new AiManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            AiStudio = new AiStudioManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            DocgenTemplate = new DocgenTemplateManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Docgen = new DocgenManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            EnterpriseConfigurations = new EnterpriseConfigurationsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Hubs = new HubsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            HubCollaborations = new HubCollaborationsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            HubItems = new HubItemsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            ShieldLists = new ShieldListsManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            Archives = new ArchivesManager(networkSession: this.NetworkSession) { Auth = this.Auth };
            ExternalUsers = new ExternalUsersManager(networkSession: this.NetworkSession) { Auth = this.Auth };
        }
        /// <summary>
        /// Make a custom http request using the client authentication and network session.
        /// </summary>
        /// <param name="fetchOptions">
        /// Options to be passed to the fetch call
        /// </param>
        public async System.Threading.Tasks.Task<FetchResponse> MakeRequestAsync(FetchOptions fetchOptions) {
            IAuthentication auth = fetchOptions.Auth == null ? this.Auth : NullableUtils.Unwrap(fetchOptions.Auth);
            NetworkSession networkSession = fetchOptions.NetworkSession == null ? this.NetworkSession : NullableUtils.Unwrap(fetchOptions.NetworkSession);
            FetchOptions enrichedFetchOptions = new FetchOptions(url: fetchOptions.Url, method: fetchOptions.Method, contentType: fetchOptions.ContentType, responseFormat: fetchOptions.ResponseFormat) { Auth = auth, NetworkSession = networkSession, Parameters = fetchOptions.Parameters, Headers = fetchOptions.Headers, Data = fetchOptions.Data, FileStream = fetchOptions.FileStream, MultipartData = fetchOptions.MultipartData, FollowRedirects = fetchOptions.FollowRedirects };
            return await networkSession.NetworkClient.FetchAsync(options: enrichedFetchOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a new client to impersonate user with the provided ID. All calls made with the new client will be made in context of the impersonated user, leaving the original client unmodified.
        /// </summary>
        /// <param name="userId">
        /// ID of an user to impersonate
        /// </param>
        public BoxClient WithAsUserHeader(string userId) {
            return new BoxClient(auth: this.Auth, networkSession: this.NetworkSession.WithAdditionalHeaders(additionalHeaders: new Dictionary<string, string>() { { "As-User", userId } }));
        }

        /// <summary>
        /// Create a new client with suppressed notifications. Calls made with the new client will not trigger email or webhook notifications
        /// </summary>
        public BoxClient WithSuppressedNotifications() {
            return new BoxClient(auth: this.Auth, networkSession: this.NetworkSession.WithAdditionalHeaders(additionalHeaders: new Dictionary<string, string>() { { "Box-Notifications", "off" } }));
        }

        /// <summary>
        /// Create a new client with a custom set of headers that will be included in every API call
        /// </summary>
        /// <param name="extraHeaders">
        /// Custom set of headers that will be included in every API call
        /// </param>
        public BoxClient WithExtraHeaders(Dictionary<string, string> extraHeaders = default) {
            extraHeaders = extraHeaders ?? new Dictionary<string, string>() {  };
            return new BoxClient(auth: this.Auth, networkSession: this.NetworkSession.WithAdditionalHeaders(additionalHeaders: extraHeaders));
        }

        /// <summary>
        /// Create a new client with a custom set of base urls that will be used for every API call
        /// </summary>
        /// <param name="baseUrls">
        /// Custom set of base urls that will be used for every API call
        /// </param>
        public BoxClient WithCustomBaseUrls(BaseUrls baseUrls) {
            return new BoxClient(auth: this.Auth, networkSession: this.NetworkSession.WithCustomBaseUrls(baseUrls: baseUrls));
        }

        /// <summary>
        /// Create a new client with a custom proxy that will be used for every API call
        /// </summary>
        /// <param name="config">
        /// 
        /// </param>
        public BoxClient WithProxy(ProxyConfig config) {
            return new BoxClient(auth: this.Auth, networkSession: this.NetworkSession.WithProxy(config: config));
        }

    }
}