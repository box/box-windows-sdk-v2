using Box.V2.Config;
using Box.V2.TransactionalAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Box.V2.Samples.TransactionalAuth
{
    /// <summary>
    /// Test program
    /// </summary>
    class Program
    {
        static readonly string PRIMARY_TOKEN = ConfigurationManager.AppSettings["boxPrimaryToken"];
        static readonly string SECONDARY_TOKEN = ConfigurationManager.AppSettings["boxSecondaryToken"];
        /// <summary>
        /// Main program method.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            Console.Write("Use primary token:");
            ConsoleKeyInfo usePT = Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("Enter fileid:");
            string fileId = Console.ReadLine();
            Task t = MainAsync(fileId, usePT.Key == ConsoleKey.Y);
            t.Wait();

            Console.WriteLine();
            Console.Write("Press return to exit...");
            Console.ReadLine();

        }
        private static async Task MainAsync(string fileId, bool usePrimaryToken)
        {

            string resource = string.Format("https://api.box.com/2.0/files/{0}", fileId);
            var boxTransactional = new BoxTransactionalAuth();
            string token = usePrimaryToken ? PRIMARY_TOKEN : SECONDARY_TOKEN;
            var client = boxTransactional.GetClient(token, resource);
            var fileInfo = await client.FilesManager.GetInformationAsync(fileId);
            if (fileInfo.Id == fileId)
            {
                Console.WriteLine(string.Format("File name is {0} ", fileInfo.Name));
            }




        }
    }
}
