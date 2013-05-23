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
        protected IBoxConverter _parser;

        public BoxResourceManagerTestIntegration()
        {


            _auth = new OAuthSession("C7bEFjfhZaD8GGlS50QAsFfylhUyPMv8", "aPgnuziyrZ23r9hUOOl4mCUboy7q3pAntRdcGuy5r6hkEE34tuuSHuZaLfXpZHBY", 3600, "bearer");

            _handler = new HttpRequestHandler();
            _parser = new BoxJsonConverter();
            _config = new BoxConfig(ClientId, ClientSecret, RedirectUri);
            _client = new BoxClient(_config, _auth);
        }
    }
}
