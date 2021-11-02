using System;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Auth.Token;
using Box.V2.Config;
using Box.V2.Exceptions;

namespace Box.V2.Samples.TransactionalAuth
{
    /// <summary>
    /// Test program for token exchange.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main program method.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main()
        {
            Console.WriteLine("Enter token:");
            var token = Console.ReadLine();

            Console.WriteLine("Enter the fileId, which the token has access to:");
            var fileId = Console.ReadLine();

            Console.WriteLine("Enter the folderId, which doesn't contain the fileId:");
            var folderId = Console.ReadLine();

            Task t = MainAsync(token, fileId, folderId);
            t.Wait();

            Console.WriteLine();
            Console.Write("Press return to exit...");
            Console.ReadLine();
        }

        private static BoxClient CreateClientByToken(string token)
        {
            var auth = new OAuthSession(token, "YOUR_REFRESH_TOKEN", 3600, "bearer");

            var config = new BoxConfigBuilder(string.Empty, string.Empty, new Uri("http://boxsdk"))
                .Build();
            var client = new BoxClient(config, auth);

            return client;
        }

        private static async Task MainAsync(string token, string fileId, string folderId)
        {
            var client = CreateClientByToken(token);
            var fileInfo = await client.FilesManager.GetInformationAsync(fileId);
            Console.WriteLine(string.Format("File name is {0} ", fileInfo.Name));

            // var resource = string.Format("https://api.box.com/2.0/files/{0}", fileId);
            var resource = string.Format("https://api.box.com/2.0/folders/{0}", folderId);

            var scope = "root_readwrite";
            var tokenExchange = new TokenExchange(token, scope);

            // Check resource to be optional
            var token1 = await tokenExchange.ExchangeAsync();
            _ = CreateClientByToken(token1);

            // Set resource
            tokenExchange.SetResource(resource);
            var token2 = await tokenExchange.ExchangeAsync();
            var client2 = CreateClientByToken(token2);
            try
            {
                await client2.FilesManager.GetInformationAsync(fileId);
            }
            catch (BoxException exp)
            {
                // The new token does not have access to the file any more.
                Console.WriteLine("Permission denied!");
                Console.WriteLine(exp);
            }

            // Can still access the folder.
            var folderInfo = await client2.FoldersManager.GetInformationAsync(folderId);
            Console.WriteLine(folderInfo.Name);

            /*
            // Set ActorToken
            var actorTokenBuilder = new ActorTokenBuilder("FAKE_USER_ID", "YOUR_CLIENT_ID");
            var actorToken = actorTokenBuilder.build();

            tokenExchange.setActorToken(actorToken);
            var tokenWAT1 = tokenExchange.exchange();

            // Set ActorToken w/ user name
            actorTokenBuilder.setUserName("uname");
            actorToken = actorTokenBuilder.build();

            tokenExchange.setActorToken(actorToken);
            var tokenWAT2 = tokenExchange.exchange();
            */
        }
    }
}
