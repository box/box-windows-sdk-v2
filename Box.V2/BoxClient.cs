using Box.V2.Auth;
using Box.V2.Managers;
using Box.V2.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box.V2.Services;

namespace Box.V2
{
    public class BoxClient
    {
        private IBoxConfig _config;
        private IBoxService _service;
        IBoxConverter _converter;

        public BoxClient(IBoxConfig boxConfig) : this(boxConfig, null) { }

        public BoxClient(IBoxConfig boxConfig, OAuthSession authSession)
        {
            _config = boxConfig;
            
            IRequestHandler handler = new HttpRequestHandler();
            _converter = new BoxJsonConverter();

            _service = new BoxService(handler);

            Auth = new AuthRepository(_config, _service, _converter, authSession);

            InitManagers();
        }

        private void InitManagers()
        {
            FoldersManager = new BoxFoldersManager(_config, _service, _converter, Auth);
            FilesManager = new BoxFilesManager(_config, _service, _converter, Auth);
            CommentsManager = new BoxCommentsManager(_config, _service, _converter, Auth);
        }

        public BoxFilesManager FilesManager { get; private set; }
        public BoxFoldersManager FoldersManager { get; private set; }
        public BoxCommentsManager CommentsManager { get; private set; }

        public AuthRepository Auth { get; set; }
    }
}
