using System;
using System.Threading.Tasks;
using Box.V2.TransactionalAuth;
using Box.V2.Auth;
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

            Console.WriteLine("Enter the fileid, which the token has access to:");
            string fileId = Console.ReadLine();

            Console.WriteLine("Enter the folderId, which doesn't contain the fileid:");
            string folderId = Console.ReadLine();

            Task t = MainAsync(token, fileId, folderId);
            t.Wait();

            Console.WriteLine();
            Console.Write("Press return to exit...");
            Console.ReadLine();

        }

        private static async Task MainAsync(string token, string fileId, string folderId)
        {
            var auth = new OAuthSession(token, "YOUR_REFRESH_TOKEN", 3600, "bearer");

            var config = new BoxConfig(string.Empty, string.Empty, new Uri("http://boxsdk"));
            var client1 = new BoxClient(config, auth);
            var fileInfo = await client1.FilesManager.GetInformationAsync(fileId);
            Console.WriteLine(string.Format("File name is {0} ", fileInfo.Name));

            // var resource = string.Format("https://api.box.com/2.0/files/{0}", fileId);
            var resource = string.Format("https://api.box.com/2.0/folders/{0}", folderId);
            var boxTransactional = new BoxTransactionalAuth();
            var client2 = boxTransactional.GetClient(token, "root_readwrite", resource);
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

            // Check resource to be optional
            var client3 = boxTransactional.GetClient(token, "root_readwrite");
        }
    }
}
