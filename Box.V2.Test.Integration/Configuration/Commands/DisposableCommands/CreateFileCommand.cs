using System.IO;
using System.Text;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class CreateFileCommand : CommandBase, IDisposableCommand
    {
        private readonly string _fileName;
        private readonly string _filePath;
        private readonly string _folderId;
        private readonly string _content;
        private readonly bool _isFileStream;

        public string FileId;
        public BoxFile File;

        public CreateFileCommand(string fileName, string filePath, string folderId = "0", CommandScope scope = CommandScope.Test,
            CommandAccessLevel accessLevel = CommandAccessLevel.User, string content = "") : base(scope, accessLevel)
        {
            _fileName = fileName;
            _filePath = filePath;
            _folderId = folderId;
            _content = content;
            if (!string.IsNullOrEmpty(_filePath) && !string.IsNullOrEmpty(_content))
            {
                throw new System.Exception("You can't have both filePath and content filled");
            }
            _isFileStream = !string.IsNullOrEmpty(_filePath);
        }

        public async Task<string> Execute(IBoxClient client)
        {
            if (_isFileStream)
            {
                using (var fileStream = new FileStream(_filePath, FileMode.OpenOrCreate))
                {
                    return await UploadFileAsync(client, fileStream);
                }
            }

            var byteArray = Encoding.UTF8.GetBytes(_content);
            using (var memoryStream = new MemoryStream(byteArray))
            {
                return await UploadFileAsync(client, memoryStream);
            }
        }

        private async Task<string> UploadFileAsync(IBoxClient client, Stream stream)
        {
            var requestParams = new BoxFileRequest()
            {
                Name = _fileName,
                Parent = new BoxRequestEntity() { Id = _folderId }
            };

            var response = await client.FilesManager.UploadAsync(requestParams, stream);
            File = response;
            FileId = File.Id;
            return FileId;
        }

        public async Task Dispose(IBoxClient client)
        {
            await client.FilesManager.DeleteAsync(FileId);

            // for some reason file uploaded as admin cannot be purged from trash
            if (AccessLevel != CommandAccessLevel.Admin)
            {
                await client.FilesManager.PurgeTrashedAsync(FileId);
            }
        }
    }
}
