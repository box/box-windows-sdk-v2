using Box.V2.Config;
using Box.V2.JWTAuth;
using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Box.V2.Samples.Core.AppUser.Create
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
            var config = ConfigureBoxApi();
            var session = new BoxJWTAuth(config);

            // client with permissions to manage application users
            var adminToken = session.AdminToken();
            var client = session.AdminClient(adminToken);

            // TODO disabled it since it will try to delete the admin as well
            // await DeleteUsersAsync(client);
            var user = await CreateNewUser(client);
            Console.WriteLine("New app user created with Id = {0}", user.Id);

            // user client with access to user's data (folders, files, etc)
            var userToken = session.UserToken(user.Id);
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

        private static async Task DeleteUsersAsync(BoxClient client)
        {
            var users = await client.UsersManager.GetEnterpriseUsersAsync();
            var deletions = users.Entries.Select(x => client.UsersManager.DeleteEnterpriseUserAsync(x.Id, false, true));
            await Task.WhenAll(deletions);
        }

        private static Task<BoxUser> CreateNewUser(BoxClient client)
        {
            var userRequest = new BoxUserRequest
            {
                Name = "First user",
                IsPlatformAccessOnly = true // creating application specific user, not a Box.com user
            };
            return client.UsersManager.CreateEnterpriseUserAsync(userRequest);
        }

        private static BoxConfig ConfigureBoxApi()
        {
            // can be obtained from Box developer portal at application info page
            var clientId = "tksevv53giq7b79xpxgxi1m7yfa74c47";
            var clientSecret = "fq8xeB4PIAALKsHNe47pxV2sppEjPg4A";

            // RSA private key
            // Public key should be enabled for application
            var privateKey = File.ReadAllText("private_key.pem");

            // secret used to generate RSA keypair
            var rsaSecret = "temp1234";

            // can be obtained from administration console
            var enterpriseId = "753393";

            // can be obtained at application info page
            var publicKeyId = "ewbt0nyl";

            return new BoxConfig(clientId, clientSecret, enterpriseId, privateKey, rsaSecret, publicKeyId);
        }
    }
}
