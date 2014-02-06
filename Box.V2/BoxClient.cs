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

        /// <summary>
        /// Instantiates a BoxClient with the provided config object
        /// </summary>
        /// <param name="boxConfig">The config object to be used</param>
        public BoxClient(IBoxConfig boxConfig) : this(boxConfig, null) { }

        /// <summary>
        /// Instantiates a BoxClient with the provided config object and auth session
        /// </summary>
        /// <param name="boxConfig">The config object to be used</param>
        /// <param name="authSession">A fully authenticated auth session</param>
        public BoxClient(IBoxConfig boxConfig, OAuthSession authSession)
        {
            Config = boxConfig;
            
            _handler = new HttpRequestHandler();
            _converter = new BoxJsonConverter();
            _service = new BoxService(_handler);
            Auth = new AuthRepository(Config, _service, _converter, authSession);

            InitManagers();
        }

        private void InitManagers()
        {
            // Init Resource Managers
            FoldersManager = new BoxFoldersManager(Config, _service, _converter, Auth);
            FilesManager = new BoxFilesManager(Config, _service, _converter, Auth);
            CommentsManager = new BoxCommentsManager(Config, _service, _converter, Auth);
            CollaborationsManager = new BoxCollaborationsManager(Config, _service, _converter, Auth);
            SearchManager = new BoxSearchManager(Config, _service, _converter, Auth);
            UsersManager = new BoxUsersManager(Config, _service, _converter, Auth);
            GroupsManager = new BoxGroupsManager(Config, _service, _converter, Auth);

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
            ResourcePlugins.Register<T>(() => (T)Activator.CreateInstance(typeof(T), Config, _service, _converter, Auth));
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
        /// The manager that represents the users endpoint
        /// </summary>
        public BoxUsersManager UsersManager { get; private set; }

        /// <summary>
        /// The manager that represents the groups endpoint
        /// </summary>
        public BoxGroupsManager GroupsManager { get; private set; }

        /// <summary>
        /// The Auth repository that holds the auth session
        /// </summary>
        public AuthRepository Auth { get; set; }

        /// <summary>
        /// Allows resource managers to be registered and retrieved as plugins
        /// </summary>
        public BoxResourcePlugins ResourcePlugins { get; private set; }

    }
}
