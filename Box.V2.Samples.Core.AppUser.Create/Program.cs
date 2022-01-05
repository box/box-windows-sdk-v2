using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.JWTAuth;
using Box.V2.Models;

namespace Box.V2.Samples.Core.AppUser.Create
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
            var config = ConfigureBoxApi();

            var session = new BoxJWTAuth(config);

            // client with permissions to manage application users
            var adminToken = await session.AdminTokenAsync();
            var client = session.AdminClient(adminToken);

            var user = await CreateNewUser(client);
            Console.WriteLine("New app user created with Id = {0}", user.Id);

            // user client with access to user's data (folders, files, etc)
            var userToken = await session.UserTokenAsync(user.Id);
            var userClient = session.UserClient(userToken, user.Id);

            // root folder has id = 0
            var newFolder = await CreateNewFolder(userClient);
            Console.WriteLine("New folder created with Id = {0}", newFolder.Id);

            var timer = Stopwatch.StartNew();
            var file = File.OpenRead("box_logo.png");
            var uploaded = await UploadFile(newFolder, userClient, file);
            Console.WriteLine("New file uploaded with Id = {0} in {1} ms", uploaded.Id, timer.ElapsedMilliseconds);
        }

        private static Task<BoxFile> UploadFile(BoxFolder newFolder, BoxClient userClient, Stream file)
        {
            var fileRequest = new BoxFileRequest
            {
                Name = "logo.png",
                Parent = new BoxFolderRequest { Id = newFolder.Id }
            };
            return userClient.FilesManager.UploadAsync(fileRequest, file);
        }

        private static Task<BoxFolder> CreateNewFolder(BoxClient userClient)
        {
            var folderRequest = new BoxFolderRequest
            {
                Description = "Test folder",
                Name = "Misc",
                Parent = new BoxFolderRequest { Id = "0" }
            };
            return userClient.FoldersManager.CreateAsync(folderRequest);
        }

        private static Task<BoxUser> CreateNewUser(BoxClient client)
        {
            var userRequest = new BoxUserRequest
            {
                Name = "First user",
                IsPlatformAccessOnly = true, // creating application specific user, not a Box.com user
                // ExternalAppUserId = "yhu-au1" // Optional, set unique external app user id
            };
            return client.UsersManager.CreateEnterpriseUserAsync(userRequest);
        }

        private static IBoxConfig ConfigureBoxApi()
        {
            IBoxConfig config = null;
            using (var fs = new FileStream(@"YOUR_JSON_FILE_HERE", FileMode.Open))
            {
                config = BoxConfigBuilder.CreateFromJsonFile(fs).Build();
            }

            return config;
        }
    }
}
