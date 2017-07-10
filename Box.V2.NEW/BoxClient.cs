using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Managers;
using Box.V2.Plugins;
using Box.V2.Request;
using Box.V2.Services;
using System;
using System.Collections.Generic;

namespace Box.V2
{
    /// <summary>
    /// The central entrypoint for all SDK interaction. The BoxClient houses all of the API endpoints and are represented 
    /// as resource managers for each distinct endpoint
    /// </summary>
    public class BoxClient
    {
        protected readonly IBoxService _service;
        protected readonly IBoxConverter _converter;
        protected readonly IRequestHandler _handler;
        protected readonly string _asUser;
        protected readonly bool? _suppressNotifications;

        /// <summary>
        /// Instantiates a BoxClient with the provided config object
        /// </summary>
        /// <param name="boxConfig">The config object to be used</param>
        /// <param name="asUser">The user ID to set as the 'As-User' header parameter; used to make calls in the context of a user using an admin token</param>
        /// <param name="suppressNotifications">Whether or not to suppress both email and webhook notifications. Typically used for administrative API calls. Your application must have “Manage an Enterprise” scope, and the user making the API calls is a co-admin with the correct "Edit settings for your company" permission.</param>
        public BoxClient(IBoxConfig boxConfig, string asUser = null, bool? suppressNotifications = null)
        {
            Config = boxConfig;

            _asUser = asUser;
            _suppressNotifications = suppressNotifications;

            _handler = new HttpRequestHandler();
            _converter = new BoxJsonConverter();
            _service = new BoxService(_handler);
            Auth = new AuthRepository(Config, _service, _converter, null);

            InitManagers();
        }

        /// <summary>
        /// Instantiates a BoxClient with the provided config object and auth session
        /// </summary>
        /// <param name="boxConfig">The config object to be used</param>
        /// <param name="authSession">A fully authenticated auth session</param>
        /// <param name="asUser">The user ID to set as the 'As-User' header parameter; used to make calls in the context of a user using an admin token</param>
        /// <param name="suppressNotifications">Whether or not to suppress both email and webhook notifications. Typically used for administrative API calls. Your application must have “Manage an Enterprise” scope, and the user making the API calls is a co-admin with the correct "Edit settings for your company" permission.</param>
        public BoxClient(IBoxConfig boxConfig, OAuthSession authSession, string asUser = null, bool? suppressNotifications = null)
        {
            Config = boxConfig;

            _asUser = asUser;
            _suppressNotifications = suppressNotifications;

            _handler = new HttpRequestHandler();
            _converter = new BoxJsonConverter();
            _service = new BoxService(_handler);
            Auth = new AuthRepository(Config, _service, _converter, authSession);

            InitManagers();
        }

        /// <summary>
        /// Instantiates a BoxClient that uses JWT authentication
        /// </summary>
        /// <param name="boxConfig">The config object to be used</param>
        /// <param name="authRepository">An IAuthRepository that knows how to retrieve new tokens using JWT</param>
        /// <param name="asUser">The user ID to set as the 'As-User' header parameter; used to make calls in the context of a user using an admin token</param>
        /// <param name="suppressNotifications">Whether or not to suppress both email and webhook notifications. Typically used for administrative API calls. Your application must have “Manage an Enterprise” scope, and the user making the API calls is a co-admin with the correct "Edit settings for your company" permission.</param>
        public BoxClient(IBoxConfig boxConfig, IAuthRepository authRepository, string asUser = null, bool? suppressNotifications = null)
        {
            Config = boxConfig;

            _asUser = asUser;
            _suppressNotifications = suppressNotifications;

            _handler = new HttpRequestHandler();
            _converter = new BoxJsonConverter();
            _service = new BoxService(_handler);
            Auth = authRepository;

            InitManagers();
        }

        /// <summary>
        /// Initializes a new BoxClient with the provided config, converter, service and auth objects.
        /// </summary>
        /// <param name="boxConfig">The config object to use</param>
        /// <param name="boxConverter">The box converter object to use</param>
        /// <param name="requestHandler">The box request handler to use</param>
        /// <param name="boxService">The box service to use</param>
        /// <param name="auth">The auth repository object to use</param>
        /// <param name="asUser">The user ID to set as the 'As-User' header parameter; used to make calls in the context of a user using an admin token</param>
        /// <param name="suppressNotifications">Whether or not to suppress both email and webhook notifications. Typically used for administrative API calls. Your application must have “Manage an Enterprise” scope, and the user making the API calls is a co-admin with the correct "Edit settings for your company" permission.</param>
        public BoxClient(IBoxConfig boxConfig, IBoxConverter boxConverter, IRequestHandler requestHandler, IBoxService boxService, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
        {
            Config = boxConfig;

            _asUser = asUser;
            _suppressNotifications = suppressNotifications;

            _handler = requestHandler;
            _converter = boxConverter;
            _service = boxService;
            Auth = auth;

            InitManagers();
        }

        private void InitManagers()
        {
            // Init Resource Managers
            FoldersManager = new BoxFoldersManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            FilesManager = new BoxFilesManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            CommentsManager = new BoxCommentsManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            CollaborationsManager = new BoxCollaborationsManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            SearchManager = new BoxSearchManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            EventsManager = new BoxEventsManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            UsersManager = new BoxUsersManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            GroupsManager = new BoxGroupsManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            RetentionPoliciesManager = new BoxRetentionPoliciesManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            MetadataManager = new BoxMetadataManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            WebhooksManager = new BoxWebhooksManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            TasksManager = new BoxTasksManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            SharedItemsManager = new BoxSharedItemsManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            CollectionsManager = new BoxCollectionsManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            DevicePinManager = new BoxDevicePinManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            WebLinksManager = new BoxWebLinksManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);
            LegalHoldPoliciesManager = new BoxLegalHoldPoliciesManager(Config, _service, _converter, Auth, _asUser, _suppressNotifications);

            // Init Resource Plugins Manager
            ResourcePlugins = new BoxResourcePlugins();
        }

        /// <summary>
        /// Adds additional resource managers/endpoints to the BoxClient.
        /// This is meant to allow for the inclusion of beta APIs or unofficial endpoints
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public BoxClient AddResourcePlugin<T>() where T : BoxResourceManager
        {
            ResourcePlugins.Register<T>(() => (T)Activator.CreateInstance(typeof(T), Config, _service, _converter, Auth, _asUser, _suppressNotifications));
            return this;
        }

        /// <summary>
        /// The configuration parameters used by the Box Service
        /// </summary>
        public IBoxConfig Config { get; private set; }

        /// <summary>
        /// The manager that represents the files endpoint
        /// </summary>
        public BoxFilesManager FilesManager { get; private set; }
        
        /// <summary>
        /// The manager that represents the folders endpoint
        /// </summary>
        public BoxFoldersManager FoldersManager { get; private set; }

        /// <summary>
        /// The manager that represents the comments endpoint
        /// </summary>
        public BoxCommentsManager CommentsManager { get; private set; }

        /// <summary>
        /// The manager that represents the collaboration endpoint
        /// </summary>
        public BoxCollaborationsManager CollaborationsManager { get; private set; }

        /// <summary>
        /// The manager that represents the search endpoint
        /// </summary>
        public BoxSearchManager SearchManager { get; private set; }

        /// <summary>
        /// The manager that represents the events endpoint
        /// </summary>
        public BoxEventsManager EventsManager { get; private set; }

        /// <summary>
        /// The manager that represents the users endpoint
        /// </summary>
        public BoxUsersManager UsersManager { get; private set; }

        /// <summary>
        /// The manager that represents the groups endpoint
        /// </summary>
        public BoxGroupsManager GroupsManager { get; private set; }

        /// <summary>
        /// The manager that represents the retention policies endpoint
        /// </summary>
        public BoxRetentionPoliciesManager RetentionPoliciesManager { get; private set; }

        /// <summary>
        /// The manager that represents the file and folder metadata endpoint
        /// </summary>
        public BoxMetadataManager MetadataManager { get; private set; }

        /// <summary>
        /// The manager that represents the webhooks V2 endpoint
        /// </summary>
        public BoxWebhooksManager WebhooksManager { get; private set; }

        /// <summary>
        /// The manager that represents the tasks endpoint
        /// </summary>
        public BoxTasksManager TasksManager { get; private set; }

        /// <summary>
        /// The manager that represents the legal hold policies endpoint
        /// </summary>
        public BoxLegalHoldPoliciesManager LegalHoldPoliciesManager { get; private set; }

        /// <summary>
        /// The Auth repository that holds the auth session
        /// </summary>
        public IAuthRepository Auth { get; private set; }

        /// <summary>
        /// Allows resource managers to be registered and retrieved as plugins
        /// </summary>
        public BoxResourcePlugins ResourcePlugins { get; private set; }

        /// <summary>
        /// The manager that represents the shared items endpoint
        /// </summary>
        public BoxSharedItemsManager SharedItemsManager { get; private set; }

        /// <summary>
        /// The manager that represents the collections endpoint
        /// </summary>
        public BoxCollectionsManager CollectionsManager { get; private set; }

        /// <summary>
        /// The manager that represents the device pin endpoint
        /// </summary>
        public BoxDevicePinManager DevicePinManager { get; private set; }

        /// <summary>
        /// The manager that represents the weblinks endpoint
        /// </summary>
        public BoxWebLinksManager WebLinksManager { get; private set; }

    }
}
