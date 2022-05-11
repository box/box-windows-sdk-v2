using System.IO;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class CreateNewFileVersion : CommandBase, INonDisposableCommand
    {
        private readonly string _fileName;
        private readonly string _filePath;
        private readonly string _fileId;

        public string FileId;
        public BoxFile File;

        public CreateNewFileVersion(string fileName, string filePath, string fileId, CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.User) : base(scope, accessLevel)
        {
            _fileName = fileName;
            _filePath = filePath;
            _fileId = fileId;
        }

        public async Task<string> Execute(IBoxClient client)
        {
            using (var fileStream = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                var response = await client.FilesManager.UploadNewVersionAsync(_fileName, _fileId, fileStream);
                File = response;
                FileId = File.Id;
                return FileId;
            }
        }
    }
}
