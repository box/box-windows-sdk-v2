using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Managers;
using Box.V2.Request;
using Box.V2.Services;
using System;

namespace Box.V2
{
    /// <summary>
    /// The central entrypoint for all SDK interaction. The BoxClient houses all of the API endpoints and are represented 
    /// as resource managers for each distinct endpoint
    /// </summary>
    public class BoxClient
    {
        private IBoxConfig _config;
        private IBoxService _service;
        IBoxConverter _converter;

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
            _config = boxConfig;
            
            IRequestHandler handler = new HttpRequestHandler();
            _converter = new BoxJsonConverter();

            _service = new BoxService(handler);

            Auth = new AuthRepository(_config, _service, _converter, authSession);

            InitManagers();
        }

        /// <summary>
        /// The Service that makes the HTTP requests
        /// </summary>
        protected IBoxService Service 
        {
            get { return _service; }
            set { _service = value; }
        }

        /// <summary>
        /// The converter used to convert Json into Box objects
        /// </summary>
        protected IBoxConverter Converter
        {
            get { return _converter; }
            set { _converter = value; }
        }
        /// <summary>
        /// The config that holds the config values.
        /// </summary>
        protected IBoxConfig Config
        {
            get { return _config; }
            set { _config = value; }
        }

        private void InitManagers()
        {
            FoldersManager = new BoxFoldersManager(_config, _service, _converter, Auth);
            FilesManager = new BoxFilesManager(_config, _service, _converter, Auth);
            CommentsManager = new BoxCommentsManager(_config, _service, _converter, Auth);
            CollaborationsManager = new BoxCollaborationsManager(_config, _service, _converter, Auth);
            SearchManager = new BoxSearchManager(_config, _service, _converter, Auth);
            UsersManager = new BoxUsersManager(_config, _service, _converter, Auth);
            GroupsManager = new BoxGroupsManager(_config, _service, _converter, Auth);
        }
        
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
    }
}
