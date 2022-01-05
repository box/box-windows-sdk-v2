using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.JWTAuth;
using Box.V2.Models;

namespace Box.V2.Samples.JWTAuth
{
    internal class Program
    {
        // modify the app.config file to reflect your Box app config values
        private static readonly string _clientId = ConfigurationManager.AppSettings["boxClientId"];
        private static readonly string _clientSecret = ConfigurationManager.AppSettings["boxClientSecret"];
        private static readonly string _enterpriseId = ConfigurationManager.AppSettings["boxEnterpriseId"];
        private static readonly string _jwtPrivateKeyPassword = ConfigurationManager.AppSettings["boxPrivateKeyPassword"];
        private static readonly string _jwtPublicKeyId = ConfigurationManager.AppSettings["boxPublicKeyId"];

        private static void Main()
        {
            Task t = MainAsync();
            t.Wait();

            Console.WriteLine();
            Console.Write("Press return to exit...");
            Console.ReadLine();
        }

        private static async Task MainAsync()
        {
            // rename the private_key.pem.example to private_key.pem and put your JWT private key in the file
            var privateKey = File.ReadAllText("private_key.pem.example");

            var boxConfig = new BoxConfigBuilder(_clientId, _clientSecret, _enterpriseId, privateKey, _jwtPrivateKeyPassword, _jwtPublicKeyId)
                .Build();
            var boxJWT = new BoxJWTAuth(boxConfig);

            var adminToken = await boxJWT.AdminTokenAsync();
            Console.WriteLine("Admin Token: " + adminToken);
            Console.WriteLine();

            var adminClient = boxJWT.AdminClient(adminToken);

            Console.WriteLine("Admin root folder items");
            var items = await adminClient.FoldersManager.GetFolderItemsAsync("0", 500);
            items.Entries.ForEach(i =>
            {
                Console.WriteLine("\t{0}", i.Name);
                //if (i.Type == "file")
                //{
                //    var previewLink = adminClient.FilesManager.GetPreviewLinkAsync(i.Id).Result;
                //    Console.WriteLine("\tPreview Link: {0}", previewLink.ToString());
                //    Console.WriteLine();
                //}   
            });
            Console.WriteLine();

            var userRequest = new BoxUserRequest() { Name = "test appuser", IsPlatformAccessOnly = true };
            var appUser = await adminClient.UsersManager.CreateEnterpriseUserAsync(userRequest);
            Console.WriteLine("Created App User");

            var userToken = await boxJWT.UserTokenAsync(appUser.Id);
            var userClient = boxJWT.UserClient(userToken, appUser.Id);

            var userDetails = await userClient.UsersManager.GetCurrentUserInformationAsync();
            Console.WriteLine("\nApp User Details:");
            Console.WriteLine("\tId: {0}", userDetails.Id);
            Console.WriteLine("\tName: {0}", userDetails.Name);
            Console.WriteLine("\tStatus: {0}", userDetails.Status);
            Console.WriteLine();

            await adminClient.UsersManager.DeleteEnterpriseUserAsync(appUser.Id, false, true);
            Console.WriteLine("Deleted App User");
        }
    }
}

