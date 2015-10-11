using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.JWTAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Samples.JWTAuth
{
    class Program
    {
        static readonly string CLIENT_ID = ConfigurationManager.AppSettings["boxClientId"];
        static readonly string CLIENT_SECRET = ConfigurationManager.AppSettings["boxClientSecret"];
        static readonly string ENTERPRISE_ID = ConfigurationManager.AppSettings["boxEnterpriseId"];
        static readonly string JWT_PRIVATE_KEY_PASSWORD = ConfigurationManager.AppSettings["boxPrivateKeyPassword"];
        static readonly string JWT_PRIVATE_KEY = ConfigurationManager.AppSettings["boxPrivateKey"];
        static readonly string REFRESH_TOKEN = "NOT_NEEDED_BUT_MUST_BE_PRESENT";
        static readonly string DEVELOPER_TOKEN = "XIWZHey1xZy58zYv4CRnIZKRtcEnD8J5";

        static BoxClient adminClient;
        static BoxClient userClient;

        static void Main(string[] args)
        {
            //Task t = MainAsync();
            //t.Wait();

            string privateKey = "-----BEGIN RSA PRIVATE KEY-----\nProc-Type: 4,ENCRYPTED\nDEK-Info: AES-256-CBC,E2CB8D4AF59B64B82CFEA149C57EC941\n\nw15gE+eUiayZxy4f7VPcovw/DbIHhi9IP4N0yQcV5xsiYu3yAsjeITayUzTyt+mA\nOhx1H4wQQsBE6GdZIYhP9BXERZkNBOID047A9PDBQP1oE/7+4iQ49s00xKNJHMjX\nQNGVGBpGvk6VYUJvlDaZzOD7/FIph3s4ojzmDNrPquKhDrx9/tEmXUEAPfuGsrhJ\nzeZ5BzxiGThDVh2arQldc58yXzr4Bo7T+hRHg5g9QFWxzaKpQX+6sIz+JS79YEeT\n0XduMemsQ/Ga1EjUMgsnICs9nZeK9955L3WVZHOa7IUHw/lrd6jqdyBZQ6YHDH7J\nmAe2wva08tHUze0xu6P+DoszUzO2f4KBZBWD5sQIHXOFSyxaru0kEpf0tDmbhuxw\nFtClhJHo70xdnuoGqYwhStCfV0Iy/alh1dCUwbEDiXDNYgXMrtLiWzUFdV7Bl1Qw\nBgyWdbOi7Gtd1kEuko4nz37rD/rst3THtnmBDG/lbgc9B88gSzDZBBZ2h5nBW/NJ\nyySOKzglihh57xZnhbOu+Xg2TG1b1hEIw552OVstr7ARcw9/rt5o/Tw5mLnPP8Z/\nG1+/OtiSUuXY/j1Ro1woDmLyc1jLj25buuiY7h9QRCVaeYPQvzsEovGoPybFmXw9\ncDIGXIpMYxbymAor0ER2EeNdmQklGcoNznyDGh/UlkKtXJnXXgJNzLboMMoTAvJr\nXGhGZfsF1ae7i3fs6WLMW0U9ZogDqosa8FFnmTqcu5ZnbHq9iWRC6TESYIpMSiJ4\nCI3ndW+L2iljA868MyKjvt0T7nB+VsYB1eJKFH6gqyCyU0eK0UFEKfB7uL0X6fXZ\nNd1HuFHiyod0xK7kDfIIkZkUnEtw69xrcZmflWqdZfl6jZu70QTrCIsKss1I24KD\nt4yA537DAlRsuDE/ZaxyjRQQN/y8B0xutlnIohRzphHMy5VHcR7nQ9tMB/S6yZdj\nfjRKD7oYmqKN0IjGxtAB6t3u6tNusd3mzRLZiLuK9o7xy19W39SIKCxLaq3eICPY\nQCsZuJS6l52zdk+HFsfhZ8cYRqz6fPGXQXXGxrc5X10lxWCQQBU0Ar3eZeeis+iv\nMTSyjNbrpfersuRD0Kb4XB2flC/LRUEsf6iL8LGtz+W3CxBN+z34ZrqBgSRXcMHN\n5IY8H/G85ScmmqdEnpypak410t3pyxG09ITvY0NMlufn3Ro8JvMPUygtiuOQc9OE\nQyrc/8X5Mg67qQXNcsGgQxTnrHgyfxfNUKjdBrymL6NsPDA1hBbvT37VuQKrnz9u\nlaAW5o1arwt2RefDDBMCrXR9QMvPdwAe9FkVKyQNjdg12b6F6O6SFq60c/gmdRLO\nLblfY9J9gDor17OIgGXXHCNH0h5vpR7zhywhb5qPJpIbBl/mfZo2P8hMfynWgDE2\nqDW7KP1ZFp9RbnWyk1IcQKCCF0lubrntaJ/mzoV7OXBiVL3uKBlRmLVwglhz1mBA\nwYXZeIeuceCQD0GIMa3va8zoz4PaukOLygbIISy7ixkEvHl4mjmQqDeVyzlxcd5c\nD9qPggotLzU/c9GyYo0o5/WDmL4BH2OVgjaMtxPoG9bFJ8LQ1ycmNJ/I2tK+Lxvd\n-----END RSA PRIVATE KEY-----\n";
            string publicKeyId = "eq2emlah";

            var boxJWT = new BoxJWTAuth(ENTERPRISE_ID, CLIENT_ID, CLIENT_SECRET, privateKey, JWT_PRIVATE_KEY_PASSWORD, publicKeyId);

            var adminToken = boxJWT.EnterpriseToken();
            Console.WriteLine(adminToken);

            Console.WriteLine();
            Console.Write("Press return to exit...");
            Console.ReadLine();
        }

        static async Task MainAsync()
        {
            var config = new BoxConfig(CLIENT_ID, CLIENT_SECRET, new Uri("http://localhost"));
            var adminSession = new OAuthSession(DEVELOPER_TOKEN, REFRESH_TOKEN, 3600, "bearer");
            adminClient = new BoxClient(config, adminSession);

            //var items = await adminClient.FoldersManager.GetFolderItemsAsync("0", 500);
            var items = await adminClient.FoldersManager.GetFolderItemsAsync("0", 500);
            items.Entries.ForEach(i => Console.WriteLine(i.Name));
            Console.WriteLine();

            //var enterpriseToken = boxJWTHelper.GetEnterpriseToken();
            //var config = new BoxConfig(CLIENT_ID, CLIENT_SECRET, new Uri("http://localhost"));
            //var adminSession = new OAuthSession(enterpriseToken, REFRESH_TOKEN, 3600, "bearer");
            //adminClient = new BoxClient(config, adminSession);

            //string appUserId = boxJWTHelper.CreateAppUser("test user", enterpriseToken);
            //var userToken = boxJWTHelper.GetUserToken(appUserId);
            //var userSession = new OAuthSession(userToken, REFRESH_TOKEN, 3600, "bearer");
            //userClient = new BoxClient(config, userSession);

            //var items = await adminClient.FoldersManager.GetFolderItemsAsync("0", 100);
            //Console.WriteLine("Admin account root folder items:");
            //items.Entries.ForEach((i) =>
            //{
            //    Console.WriteLine("\t{0}", i.Name);
            //    if (i is BoxFile) { Console.WriteLine("\t{0}", BoxJWTHelper.EmbedUrl(i.Id, enterpriseToken)); }
            //});

            //var userDetails = await userClient.UsersManager.GetCurrentUserInformationAsync();
            //Console.WriteLine("\nApp User Details:");
            //Console.WriteLine("\tId: {0}", userDetails.Id);
            //Console.WriteLine("\tName: {0}", userDetails.Name);
            //Console.WriteLine("\tStatus: {0}", userDetails.Status);

            //boxJWTHelper.DeleteAppUser(appUserId, enterpriseToken);
        }
    }
}
