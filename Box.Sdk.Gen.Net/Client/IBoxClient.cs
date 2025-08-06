using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public interface IBoxClient {
        public IAuthentication Auth { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public NetworkSession NetworkSession { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IAuthorizationManager Authorization { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IFilesManager Files { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ITrashedFilesManager TrashedFiles { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IAppItemAssociationsManager AppItemAssociations { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IDownloadsManager Downloads { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IUploadsManager Uploads { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IChunkedUploadsManager ChunkedUploads { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IListCollaborationsManager ListCollaborations { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ICommentsManager Comments { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ITasksManager Tasks { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IFileVersionsManager FileVersions { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IFileMetadataManager FileMetadata { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IFileClassificationsManager FileClassifications { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ISkillsManager Skills { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IFileWatermarksManager FileWatermarks { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IFileRequestsManager FileRequests { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IFoldersManager Folders { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ITrashedFoldersManager TrashedFolders { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IFolderMetadataManager FolderMetadata { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IFolderClassificationsManager FolderClassifications { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ITrashedItemsManager TrashedItems { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IFolderWatermarksManager FolderWatermarks { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IFolderLocksManager FolderLocks { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IMetadataTemplatesManager MetadataTemplates { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IClassificationsManager Classifications { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IMetadataCascadePoliciesManager MetadataCascadePolicies { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ISearchManager Search { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IUserCollaborationsManager UserCollaborations { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ITaskAssignmentsManager TaskAssignments { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ISharedLinksFilesManager SharedLinksFiles { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ISharedLinksFoldersManager SharedLinksFolders { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IWebLinksManager WebLinks { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ITrashedWebLinksManager TrashedWebLinks { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ISharedLinksWebLinksManager SharedLinksWebLinks { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ISharedLinksAppItemsManager SharedLinksAppItems { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IUsersManager Users { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ISessionTerminationManager SessionTermination { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IAvatarsManager Avatars { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ITransferManager Transfer { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IEmailAliasesManager EmailAliases { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IMembershipsManager Memberships { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IInvitesManager Invites { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IGroupsManager Groups { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IWebhooksManager Webhooks { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IEventsManager Events { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ICollectionsManager Collections { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IRecentItemsManager RecentItems { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IRetentionPoliciesManager RetentionPolicies { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IRetentionPolicyAssignmentsManager RetentionPolicyAssignments { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ILegalHoldPoliciesManager LegalHoldPolicies { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ILegalHoldPolicyAssignmentsManager LegalHoldPolicyAssignments { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IFileVersionRetentionsManager FileVersionRetentions { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IFileVersionLegalHoldsManager FileVersionLegalHolds { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IShieldInformationBarriersManager ShieldInformationBarriers { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IShieldInformationBarrierReportsManager ShieldInformationBarrierReports { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IShieldInformationBarrierSegmentsManager ShieldInformationBarrierSegments { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IShieldInformationBarrierSegmentMembersManager ShieldInformationBarrierSegmentMembers { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IShieldInformationBarrierSegmentRestrictionsManager ShieldInformationBarrierSegmentRestrictions { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IDevicePinnersManager DevicePinners { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ITermsOfServicesManager TermsOfServices { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ITermsOfServiceUserStatusesManager TermsOfServiceUserStatuses { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ICollaborationAllowlistEntriesManager CollaborationAllowlistEntries { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ICollaborationAllowlistExemptTargetsManager CollaborationAllowlistExemptTargets { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IStoragePoliciesManager StoragePolicies { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IStoragePolicyAssignmentsManager StoragePolicyAssignments { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IZipDownloadsManager ZipDownloads { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ISignRequestsManager SignRequests { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IWorkflowsManager Workflows { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public ISignTemplatesManager SignTemplates { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IIntegrationMappingsManager IntegrationMappings { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IAiManager Ai { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IAiStudioManager AiStudio { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IDocgenTemplateManager DocgenTemplate { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IDocgenManager Docgen { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IHubsManager Hubs { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IHubCollaborationsManager HubCollaborations { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IHubItemsManager HubItems { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IShieldListsManager ShieldLists { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

        public IArchivesManager Archives { get => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it."); }

    }
}