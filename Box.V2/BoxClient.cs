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

        public BoxClient(IBoxConfig boxConfig) : this(boxConfig, null) { }

        public BoxClient(IBoxConfig boxConfig, OAuthSession authSession)
        {
            _config = boxConfig;
            
            IResponseParser parser = new JsonResponseParser();
            IRequestHandler handler = new HttpRequestHandler();
            _service = new BoxService(parser, handler);

            Auth = new AuthRepository(_config, _service, authSession);

            InitManagers();
        }

        private void InitManagers()
        {
            FoldersManager = new BoxFoldersManager(_config, _service, Auth);
            FilesManager = new BoxFilesManager(_config, _service, Auth);
        }

        public BoxFilesManager FilesManager { get; private set; }
        public BoxFoldersManager FoldersManager { get; private set; }

        public AuthRepository Auth { get; set; }
    }
}
