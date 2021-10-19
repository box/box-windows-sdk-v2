using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Models;
using Box.V2.Utility;

namespace Box.V2.Core.Sample
{
    public class Program
    {
        private static void Main()
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

            var config = new BoxConfigBuilder("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET", new Uri("http://boxsdk"))
                .Build();
            var client = new BoxClient(config, auth);

            var file = File.OpenRead(localFilePath);
            var fileRequest = new BoxFileRequest
            {
                Name = fileName,
                Parent = new BoxFolderRequest { Id = parentFolderId }
            };

            // Normal file upload
            // var bFile = await client.FilesManager.UploadAsync(fileRequest, file);

            // Supercharged filed upload with progress report, only works with file >= 50m.
            var progress = new Progress<BoxProgress>(val => { Console.WriteLine("{0}%", val.progress); });
            var bFile = await client.FilesManager.UploadUsingSessionAsync(file, fileName, parentFolderId, null, progress);

            Console.WriteLine("{0} uploaded to folder: {1} as file: {2}", localFilePath, parentFolderId, bFile.Id);
            Console.WriteLine("Time spend : {0} ms", timer.ElapsedMilliseconds);
        }
    }
}
