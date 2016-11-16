using System;
using System.Threading.Tasks;
using Box.V2.TransactionalAuth;
using Box.V2.Config;
using Box.V2.Exceptions;

namespace Box.V2.Samples.TransactionalAuth
{
    /// <summary>
    /// Test program for token exchange.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main program method.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Enter token:");
            string token = Console.ReadLine();

            Console.WriteLine("Enter client Id:");
            string clientId = Console.ReadLine();

            Console.WriteLine("Enter the fileId, which the token has access to:");
            string fileId = Console.ReadLine();

            Console.WriteLine("Enter the folderId, which doesn't contain the fileId:");
            string folderId = Console.ReadLine();

            Task t = MainAsync(token, clientId, fileId, folderId);
            t.Wait();

            Console.WriteLine();
            Console.Write("Press return to exit...");
            Console.ReadLine();
        }

        private static async Task MainAsync(string token, string clientId, string fileId, string folderId)
        {
            var scope = "annotation";

            var config = new BoxConfig(token);
            var boxTransactional = new BoxTransactionalAuth(config);
            var client1 = boxTransactional.GetClient();
            var fileInfo = await client1.FilesManager.GetInformationAsync(fileId);
            Console.WriteLine(string.Format("File name is {0} ", fileInfo.Name));

            // var resource = string.Format("https://api.box.com/2.0/files/{0}", fileId);
            var resource = string.Format("https://api.box.com/2.0/folders/{0}", folderId);

            // actor token creation
            var actorToken = BoxTransactionalAuth.ConstructActorToken("123", "name", clientId);
            var userToken = boxTransactional.TokenExchange(scope, null, actorToken);

            // check resource token creation
            var resourceToken = boxTransactional.TokenExchange(scope, resource);
            var client2 = new BoxTransactionalAuth(new BoxConfig(resourceToken)).GetClient();
            try
            {
                await client2.FilesManager.GetInformationAsync(fileId);
            }
            catch (BoxException exp)
            {
                // The new token does not have access to the file any more
                Console.WriteLine("Permission denied!");
                Console.WriteLine(exp);
            }

            // Can still access the folder
            var folderInfo = await client2.FoldersManager.GetInformationAsync(folderId);
            Console.WriteLine(folderInfo.Name);

            // Check resource to be optional
            var scopedToken = boxTransactional.TokenExchange(scope);
        }
    }
}
