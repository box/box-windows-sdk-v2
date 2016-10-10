using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Managers;
using Box.V2.Request;
using Box.V2.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Box.V2.Test
{
    public abstract class BoxResourceManagerTest 
    {

        protected IBoxConverter _converter;
        protected Mock<IRequestHandler> _handler;
        protected IBoxService _service;
        protected Mock<IBoxConfig> _config;
        protected AuthRepository _authRepository;

        protected Uri _baseUri = new Uri(Constants.BoxApiUriString);
		protected Uri _FoldersUri = new Uri(Constants.FoldersEndpointString);
        protected Uri _FilesUri = new Uri(Constants.FilesEndpointString);
        protected Uri _usersUri = new Uri(Constants.UserEndpointString);

        public BoxResourceManagerTest()
        {
            // Initial Setup
            _converter = new BoxJsonConverter();
            _handler = new Mock<IRequestHandler>();
            _service = new BoxService(_handler.Object);
            _config = new Mock<IBoxConfig>();
			_config.SetupGet(x => x.CollaborationsEndpointUri).Returns(new Uri(Constants.CollaborationsEndpointString));
            _config.SetupGet(x => x.FoldersEndpointUri).Returns(_FoldersUri);
            _config.SetupGet(x => x.FilesEndpointUri).Returns(_FilesUri);
            _config.SetupGet(x => x.UserEndpointUri).Returns(_usersUri);

            _authRepository = new AuthRepository(_config.Object, _service, _converter, new OAuthSession("fakeAccessToken", "fakeRefreshToken", 3600, "bearer"));
        }
		public static T CreateInstanceNonPublicConstructor<T>()
        {
            Type[] pTypes = new Type[0];

            ConstructorInfo[] c = typeof(T).GetConstructors
                (BindingFlags.NonPublic | BindingFlags.Instance
                );

            T inst =
                (T)c[0].Invoke(BindingFlags.NonPublic,
                               null,
                               null,
                               System.Threading.Thread.CurrentThread.CurrentCulture);
            return inst;
        }
    }
}
