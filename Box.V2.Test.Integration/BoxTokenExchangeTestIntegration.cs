using Box.V2.Auth;
using Box.V2.Auth.Token;
using Box.V2.Config;
using Box.V2.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxTokenExchangeTestIntegration : BoxResourceManagerTestIntegration
    {
        private static BoxClient CreateClientByToken(string token)
        {
            var auth = new OAuthSession(token, "YOUR_REFRESH_TOKEN", 3600, "bearer");

            var config = new BoxConfig(string.Empty, string.Empty, new Uri("http://boxsdk"));
            var client = new BoxClient(config, auth);

            return client;
        }

        [TestMethod]
        public async Task TokenExchange_LiveSession()
        {
            var token = _client.Auth.Session.AccessToken;
            var fileId = "16894965489";
            var folderId = "1927307787";

            var client = CreateClientByToken(token);
            var fileInfo = await client.FilesManager.GetInformationAsync(fileId);

            // var resource = string.Format("https://api.box.com/2.0/files/{0}", fileId);
            var resource = string.Format("https://api.box.com/2.0/folders/{0}", folderId);

            var scopes = new List<string> { "item_preview", "item_delete" };
            var tokenExchange = new TokenExchange(token, scopes);

            // Check resource to be optional
            var token1 = tokenExchange.Exchange();
            var client1 = CreateClientByToken(token1);

            // Should be able to access the file
            var file1 = await client1.FilesManager.GetInformationAsync(fileId);
            Assert.IsNotNull(file1.Id);

            // Set resource
            tokenExchange.SetResource(resource);
            var token2 = tokenExchange.Exchange();
            var client2 = CreateClientByToken(token2);
            try
            {
                await client2.FilesManager.GetInformationAsync(fileId);
                Assert.Fail();
            }
            catch (BoxException exp)
            {
                // The new token does not have access to the file any more.
            }

            // Can still access the folder.
            var folderInfo = await client2.FoldersManager.GetInformationAsync(folderId);
            Assert.IsNotNull(folderInfo.Name);
        }
    }
}
