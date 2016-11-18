using System;
using System.IO;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.Models;
using Box.V2.Auth;
using System.Diagnostics;

namespace Box.V2.Core.Sample
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                new Program().ExecuteMainAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task ExecuteMainAsync()
        {
            Console.WriteLine("Access token: ");
            var accessToken = Console.ReadLine();

            Console.WriteLine("Remote file name: ");
            var fileName = Console.ReadLine();

            Console.WriteLine("Local file path: ");
            var localFilePath = Console.ReadLine();

            Console.WriteLine("Parent folder Id: ");
            var parentFolderId = Console.ReadLine();

            var timer = Stopwatch.StartNew();

            var auth = new OAuthSession(accessToken, "YOUR_REFRESH_TOKEN", 3600, "bearer");

            var config = new BoxConfig("YOUR_CLIENT_ID", "YOUR_CLIENT_ID", new Uri("http://boxsdk"));
            var client = new BoxClient(config, auth);

            var file = File.OpenRead(localFilePath);
            var fileRequest = new BoxFileRequest
            {
                Name = fileName,
                Parent = new BoxFolderRequest { Id = parentFolderId }
            };

            var bFile = await client.FilesManager.UploadAsync(fileRequest, file);

            Console.WriteLine("{0} uploaded to folder: {1} as file: {2}",localFilePath, parentFolderId, bFile.Id);
            Console.WriteLine("Time spend : {0} ms", timer.ElapsedMilliseconds);
        }
    }
}