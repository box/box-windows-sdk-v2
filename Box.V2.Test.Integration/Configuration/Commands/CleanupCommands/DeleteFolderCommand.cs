using System.Threading.Tasks;

namespace Box.V2.Test.Integration.Configuration.Commands.CleanupCommands
{
    public class DeleteFolderCommand : CommandBase, ICleanupCommand
    {
        private readonly string _folderId;

        public DeleteFolderCommand(string folderId, CommandScope scope = CommandScope.Test) : base(scope)
        {
            _folderId = folderId;
        }

        public async Task Execute(IBoxClient client)
        {
            await client.FoldersManager.DeleteAsync(_folderId, true);

            await client.FoldersManager.PurgeTrashedFolderAsync(_folderId);
        }
    }
}
