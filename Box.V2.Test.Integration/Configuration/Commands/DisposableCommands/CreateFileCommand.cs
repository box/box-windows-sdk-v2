using System.IO;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class CreateFileCommand : CommandBase, IDisposableCommand
    {
        private readonly string _fileName;
        private readonly string _filePath;
        private readonly string _folderId;

        public string FileId;
        public BoxFile File;

        public CreateFileCommand(string fileName, string filePath, string folderId = "0", CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.User) : base(scope, accessLevel)
        {
            _fileName = fileName;
            _filePath = filePath;
            _folderId = folderId;
        }

        public async Task<string> Execute(IBoxClient client)
        {
            using (var fileStream = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                var requestParams = new BoxFileRequest()
                {
                    Name = _fileName,
                    Parent = new BoxRequestEntity() { Id = _folderId }
                };

                var response = await client.FilesManager.UploadAsync(requestParams, fileStream);
                File = response;
                FileId = File.Id;
                return FileId;
            }
        }

        public async Task Dispose(IBoxClient client)
        {
            await client.FilesManager.DeleteAsync(FileId);

            // for some reason file uploaded as admin cannot be purged from trash
            if(AccessLevel != CommandAccessLevel.Admin)
            {
                await client.FilesManager.PurgeTrashedAsync(FileId);
            }
        }
    }
}
