using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class CreateFolderCommand : CommandBase, IDisposableCommand
    {
        private readonly string _folderName;
        private readonly string _parentId;

        public string FolderId;
        public BoxFolder Folder;

        public CreateFolderCommand(string folderName, string parentId = "0", CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.User) : base(scope, accessLevel)
        {
            _folderName = folderName;
            _parentId = parentId;
        }

        public async Task<string> Execute(IBoxClient client)
        {
            var createFolderRequest = new BoxFolderRequest
            {
                Name = _folderName,
                Parent = new BoxRequestEntity
                {
                    Id = _parentId
                }
            };
            Folder = await client.FoldersManager.CreateAsync(createFolderRequest);
            FolderId = Folder.Id;
            return FolderId;
        }

        public async Task Dispose(IBoxClient client)
        {
            await client.FoldersManager.DeleteAsync(FolderId, true);

            await client.FoldersManager.PurgeTrashedFolderAsync(FolderId);
        }
    }
}
