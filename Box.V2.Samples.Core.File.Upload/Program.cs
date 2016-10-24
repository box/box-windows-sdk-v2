using System;
using System.IO;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.Models;
using Box.V2.Auth;

namespace Box.V2.Core.Sample
{
    public class Program
    {
        private static void Main(String[] args)
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
            var accessToken = "";
            Console.WriteLine("Access token: ");
            accessToken = Console.ReadLine();

            Console.WriteLine("Remote file name: ");
            var fileName = "";
            fileName = Console.ReadLine();

            Console.WriteLine("Local file path: ");
            var localFilePath = "";
            localFilePath = Console.ReadLine();

            Console.WriteLine("Parent folder Id: ");
            var parentFolderId = "";
            parentFolderId = Console.ReadLine();

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

            Console.WriteLine(localFilePath + " uploaded to folder: " + parentFolderId + " as file: " + bFile.Id);
            Console.ReadLine();
        }
    }
}