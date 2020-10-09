using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace ConsoleAppDatc
{
    class Program
    {
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Console API Drive Application";
        
        static void Main(string[] args)
        {
            UserCredential credential;
            string filePath1 = "C:/Users/Sorin/Desktop/Orar.txt";
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            Console.WriteLine("Files:");
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("{0} ({1})", file.Name, file.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }
            Console.Read();
            
            if (System.IO.File.Exists(filePath1))  
            {  
               Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();  
               body.Name = System.IO.Path.GetFileName(filePath1);
               byte[] byteArray = System.IO.File.ReadAllBytes(filePath1);
               System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);  

               FilesResource.CreateMediaUpload request = service.Files.Create(body, stream,filePath1);  
               request.SupportsTeamDrives = true;   
               request.Upload();    
            }     
            else
            {
               System.Console.WriteLine("The file does not exist.");
            }
        }
    }
}
