using Box.V2.Auth;
using Box.V2.Contracts;
using Box.V2.Managers;
using Box.V2.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Test
{
    public abstract class BoxResourceManagerTest 
    {

        protected IResponseParser _parser;
        protected Mock<IRequestHandler> _handler;
        protected IBoxService _service;
        protected Mock<IBoxConfig> _config;
        protected BoxFoldersManager _foldersManager;
        protected AuthRepository _authRepository;

        protected Uri _baseUri = new Uri(Constants.BoxApiUriString);

        public BoxResourceManagerTest()
        {
            // Initial Setup
            _parser = new JsonResponseParser();
            _handler = new Mock<IRequestHandler>();
            _service = new BoxService(_parser, _handler.Object);
            _config = new Mock<IBoxConfig>();

            _authRepository = new AuthRepository(_config.Object, _service,
                new OAuthSession()
                {
                    AccessToken = "fakeAccessToken",
                    ExpiresIn = 3600,
                    RefreshToken = "fakeRefreshToken",
                    TokenType = "bearer"
                });
            _foldersManager = new BoxFoldersManager(_config.Object, _service, _authRepository);
        }
    }
}
