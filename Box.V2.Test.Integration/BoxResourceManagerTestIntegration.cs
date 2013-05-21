using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Auth;
using Box.V2.Services;
using Box.V2.Contracts;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public abstract class BoxResourceManagerTestIntegration
    {

        public const string ClientId = "pweqblqwil7cpmvgu45jaokt3qw77wbo";
        public const string ConsumerKey = "hdivvq08t2gnj19zssp6xqmovjp42u2g";
        public const string ClientSecret = "dTrKxu2JYDeYIyQKSKLDf57HVlWjvU10";
        public const string RedirectUri = "http://localhost";

        protected OAuthSession _auth;
        protected BoxClient _client;
        protected IBoxConfig _config;
        protected IRequestHandler _handler;
        protected IResponseParser _parser;

        public BoxResourceManagerTestIntegration()
        {
            _auth = new OAuthSession()
            {
                AccessToken = "Xznde1gh1HkfeZK6fKZ0Bs3VJ4zPu5vb",
                RefreshToken = "ZrRzZh5ikbrYeQeTUILOf33SlJM2zVNp1H3mdRKAYupfTuqPLtpq0yNVkEvSfJRy",
                TokenType = "bearer",
                ExpiresIn = 3600
            };

            _handler = new HttpRequestHandler();
            _parser = new JsonResponseParser();
            _config = new BoxConfig(ClientId, ClientSecret, RedirectUri);
            _client = new BoxClient(_config, _auth);
        }



    }
}
