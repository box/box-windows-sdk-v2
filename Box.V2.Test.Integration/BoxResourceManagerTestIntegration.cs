﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Auth;
using Box.V2.Services;
using System.Threading.Tasks;
using Box.V2.Request;
using Box.V2.Config;
using Box.V2.Converter;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public abstract class BoxResourceManagerTestIntegration
    {
        public const string ClientId = "5uzq9vy86dsefo8rpmd60ol1i0hu54qr";
        public const string ClientSecret = "EPAjy3QrBczECOYDt2kXKBmjmJwcx6OE";

        public Uri RedirectUri = new Uri("http://boxsdk");

        protected OAuthSession _auth;
        protected BoxClient _client;
        protected IBoxConfig _config;
        protected IRequestHandler _handler;
        protected IBoxConverter _parser;

        public BoxResourceManagerTestIntegration()
        {
            _auth = new OAuthSession("vLiSG2rfGC65ibHGTvGN3rAkuBYqMF6W", "YOUR_REFRESH_TOKEN", 3600, "bearer");

            _handler = new HttpRequestHandler();
            _parser = new BoxJsonConverter();
            _config = new BoxConfig(ClientId, ClientSecret, RedirectUri);
            _client = new BoxClient(_config, _auth);
        }

        protected string GetUniqueName()
        {
            return string.Format("test{0}", Guid.NewGuid().ToString());
        }

        #region Test Properties

        //private string _testFolderId = "0";
        //public string TestFolderId
        //{
        //    get { return _testFolderId; }
        //    set { _testFolderId = value; }
        //}

        //private string _testFileId = "7869094982";
        //public string TestFileId
        //{
        //    get { return _testFileId; }
        //    set { _testFileId = value; }
        //}


        #endregion
    }
}
