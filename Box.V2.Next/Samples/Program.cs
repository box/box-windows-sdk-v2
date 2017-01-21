using System;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.Auth;
using System.Diagnostics;
using Box.V2.Next.Managers;
using System.Collections.Generic;

namespace Box.V2.Next.Samples
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
            var accessToken = "U0k5GsJLoQkdd0hc4qNzsVTFVmC4bvgy";// Console.ReadLine();

            Console.WriteLine("Remote file id: ");
            var fileId = "47719821805"; // Console.ReadLine();

            var timer = Stopwatch.StartNew();

            var auth = new OAuthSession(accessToken, "YOUR_REFRESH_TOKEN", 3600, "bearer");

            var config = new BoxConfig("YOUR_CLIENT_ID", "YOUR_CLIENT_ID", new Uri("http://boxsdk"));
            var client = new BoxClient(config, auth);

            client.AddResourcePlugin<BoxFilesManagerV2>();

            var fileMgrV2 = client.ResourcePlugins.Get<BoxFilesManagerV2>();

            dynamic fileInfo = await fileMgrV2.GetInformationAsync(fileId, null, new List<string>(new string[] { "representations" }));

            var fid = fileInfo.id;
            var type = fileInfo.type;
            
            var repCount = fileInfo.representations.entries.Count;
            var rep0 = fileInfo.representations.entries[0];
            var content0 = rep0.content.url_template;

            Console.WriteLine(fid);
            Console.WriteLine(type);
            Console.WriteLine(content0);

            // var bFile = await client.FilesManager.UploadAsync(fileRequest, file);

            // Console.WriteLine("{0} uploaded to folder: {1} as file: {2}",localFilePath, parentFolderId, bFile.Id);
            Console.WriteLine("Time spend : {0} ms", timer.ElapsedMilliseconds);
        }
    }
}