using System;
using System.Net;
using Box.V2.Config;
using Box.V2.JWTAuth;
using Box.V2.Request;

namespace Box.V2.Samples.Core.HttpProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            var boxConfig = BoxConfigBuilder.CreateFromJsonString(GetConfigJson())
                // Set web proxy
                .SetWebProxy(new BoxHttpProxy())
                .Build();

            var boxJWT = new BoxJWTAuth(boxConfig);

            var adminToken = boxJWT.AdminTokenAsync().Result; 
            var adminClient = boxJWT.AdminClient(adminToken);

            var items = adminClient.FoldersManager.GetFolderItemsAsync("0", 500).Result;
        }

        private static string GetConfigJson()
        {
            return @"<box app settings json >";
        }
    }

    public class BoxHttpProxy : IWebProxy
    {
        public Uri GetProxy(Uri destination)
        {
            return new Uri("http://localhost:8888");
        }

        public bool IsBypassed(Uri host)
        {
            return false;
        }

        public ICredentials Credentials { get; set; }
    }
}
