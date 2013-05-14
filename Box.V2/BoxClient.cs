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
        private IAuthRepository _auth;

        public BoxClient(IBoxConfig boxConfig) : this(boxConfig, null) { }

        public BoxClient(IBoxConfig boxConfig, OAuthSession authSession)
        {
            _config = boxConfig;
            
            IResponseParser parser = new JsonResponseParser();
            IRequestHandler handler = new HttpRequestHandler();
            _service = new BoxService(parser, handler);

            Auth = new AuthRepository(_config, _service);

            InitManagers();
        }

        private void InitManagers()
        {
            BoxFoldersManager = new BoxFoldersManager(_config, _service, _auth);
            BoxFilesManager = new BoxFilesManager(_config, _service, _auth);
        }

        public BoxFilesManager BoxFilesManager { get; private set; }
        public BoxFoldersManager BoxFoldersManager { get; private set; }

        public AuthRepository Auth { get; set; }
    }
}
