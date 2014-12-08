using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DropboxRestAPI;

namespace Sample
{
    class Program
    {
        private static void Main()
        {
            Run().Wait();
        }

        private static async Task Run()
        {
            var options = new Options
                {
                    ClientId = "...",
                    ClientSecret = "...",
                    RedirectUri = "..."
                };

            // Initialize a new Client (without an AccessToken)
            var client = new Client(options);

            // Get the OAuth Request Url
            var authRequestUrl = await client.Core.OAuth2.AuthorizeAsync("code");

            // TODO: Navigate to authRequestUrl using the browser, and retrieve the Authorization Code from the response
            var authCode = "...";

            // Exchange the Authorization Code with Access/Refresh tokens
            var token = await client.Core.OAuth2.TokenAsync(authCode);

            // Get account info
            var accountInfo = await client.Core.Accounts.AccountInfoAsync();
            Console.WriteLine("Uid: " + accountInfo.uid);
            Console.WriteLine("Display_name: " + accountInfo.display_name);
            Console.WriteLine("Email: " + accountInfo.email);

            // Get root folder without content
            var rootFolder = await client.Core.Metadata.MetadataAsync("/", list: false);
            Console.WriteLine("Root Folder: {0} (Id: {1})", rootFolder.Name, rootFolder.path);

            // Get root folder with content
            rootFolder = await client.Core.Metadata.MetadataAsync("/", list: true);
            foreach (var folder in rootFolder.contents)
            {
                Console.WriteLine(" -> {0}: {1} (Id: {2})", 
                    folder.is_dir ? "Folder" : "File", folder.Name, folder.path);
            }

            // Initialize a new Client (with an AccessToken)
            var client2 = new Client(options);

            // Create a new folder
            var newFolder = await client2.Core.FileOperations.CreateFolderAsync("/New Folder");

            // Find a file in the root folder
            var file = rootFolder.contents.FirstOrDefault(x => x.is_dir == false);

            // Download a file
            var tempFile = Path.GetTempFileName();
            using (var fileStream = System.IO.File.OpenWrite(tempFile))
            {
                await client2.Core.Metadata.FilesAsync(file.path, fileStream);
            }

            //Upload the downloaded file to the new folder

            using (var fileStream = System.IO.File.OpenRead(tempFile))
            {
                var uploadedFile =await client2.Core.Metadata.FilesPutAsync(fileStream, newFolder.path + "/" + file.Name);
            }

            // Search file based on name
            var searchResults = await client2.Core.Metadata.SearchAsync("/", file.Name);
            foreach (var searchResult in searchResults)
            {
                Console.WriteLine("Found: " + searchResult.path);
            }
        }
    }
}
