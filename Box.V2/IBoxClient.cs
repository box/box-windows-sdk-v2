using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Managers;
using Box.V2.Plugins;

namespace Box.V2
{
    /// <summary>
    /// The central entrypoint for all SDK interaction. The BoxClient houses all of the API endpoints and are represented 
    /// as resource managers for each distinct endpoint
    /// </summary>
    public interface IBoxClient
    {
        /// <summary>
        /// Adds additional resource managers/endpoints to the BoxClient.
        /// This is meant to allow for the inclusion of beta APIs or unofficial endpoints
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IBoxClient AddResourcePlugin<T>() where T : BoxResourceManager;

        /// <summary>
        /// The configuration parameters used by the Box Service
        /// </summary>
        IBoxConfig Config { get; }

        /// <summary>
        /// The manager that represents the files endpoint
        /// </summary>
        IBoxFilesManager FilesManager { get; }

        /// <summary>
        /// The manager that represents the folders endpoint
        /// </summary>
        IBoxFoldersManager FoldersManager { get; }

        /// <summary>
        /// The manager that represents the comments endpoint
        /// </summary>
        IBoxCommentsManager CommentsManager { get; }

        /// <summary>
        /// The manager that represents the collaboration endpoint
        /// </summary>
        IBoxCollaborationsManager CollaborationsManager { get; }

        /// <summary>
        /// The manager that represents the search endpoint
        /// </summary>
        IBoxSearchManager SearchManager { get; }

        /// <summary>
        /// The manager that represents the events endpoint
        /// </summary>
        IBoxEventsManager EventsManager { get; }

        /// <summary>
        /// The manager that represents the users endpoint
        /// </summary>
        IBoxUsersManager UsersManager { get; }

        /// <summary>
        /// The manager that represents the groups endpoint
        /// </summary>
        IBoxGroupsManager GroupsManager { get; }

        /// <summary>
        /// The manager that represents the retention policies endpoint
        /// </summary>
        IBoxRetentionPoliciesManager RetentionPoliciesManager { get; }

        /// <summary>
        /// The manager that represents the file and folder metadata endpoint
        /// </summary>
        IBoxMetadataManager MetadataManager { get; }

        /// <summary>
        /// The manager that represents the webhooks V2 endpoint
        /// </summary>
        IBoxWebhooksManager WebhooksManager { get; }

        /// <summary>
        /// The manager that represents the recent items endpoint
        /// </summary>
        IBoxRecentItemsManager RecentItemsManager { get; }

        /// <summary>
        /// The manager that represents the tasks endpoint
        /// </summary>
        IBoxTasksManager TasksManager { get; }

        /// <summary>
        /// The manager that represents the legal hold policies endpoint
        /// </summary>
        IBoxLegalHoldPoliciesManager LegalHoldPoliciesManager { get; }

        /// <summary>
        /// The Auth repository that holds the auth session
        /// </summary>
        IAuthRepository Auth { get; }

        /// <summary>
        /// Allows resource managers to be registered and retrieved as plugins
        /// </summary>
        BoxResourcePlugins ResourcePlugins { get; }

        /// <summary>
        /// The manager that represents the shared items endpoint
        /// </summary>
        IBoxSharedItemsManager SharedItemsManager { get; }

        /// <summary>
        /// The manager that represents the collections endpoint
        /// </summary>
        IBoxCollectionsManager CollectionsManager { get; }

        /// <summary>
        /// The manager that represents the device pin endpoint
        /// </summary>
        IBoxDevicePinManager DevicePinManager { get; }

        /// <summary>
        /// The manager that represents the weblinks endpoint
        /// </summary>
        IBoxWebLinksManager WebLinksManager { get; }

        /// <summary>
        /// The manager that represents the collaboration whitelist endpoint
        /// </summary>
        IBoxCollaborationWhitelistManager CollaborationWhitelistManager { get; }

        /// <summary>
        /// The manager that represents the terms of service endpoint
        /// </summary>
        IBoxTermsOfServiceManager TermsOfServiceManager { get; }

        /// <summary>
        /// The manager that represents the metadata cascade policy endpoint
        /// </summary>
        IBoxMetadataCascadePolicyManager MetadataCascadePolicyManager { get; }

        /// <summary>
        /// The manager that represents the storage policies endpoint
        /// </summary>
        IBoxStoragePoliciesManager StoragePoliciesManager { get; }

        /// <summary>
        /// The manager that represents sign requests endpoints.
        /// </summary>
        IBoxSignRequestsManager SignRequestsManager { get; }

        /// <summary>
        /// The manager that represents all of the file requests endpoints.
        /// </summary>
        IBoxFileRequestsManager FileRequestsManager { get; }
    }
}
