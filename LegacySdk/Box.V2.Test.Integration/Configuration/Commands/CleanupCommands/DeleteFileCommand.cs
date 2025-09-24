using System.Threading.Tasks;

namespace Box.V2.Test.Integration.Configuration.Commands.CleanupCommands
{
    public class DeleteFileCommand : CommandBase, ICleanupCommand
    {
        private readonly string _fileId;

        public DeleteFileCommand(string fileId, CommandScope scope = CommandScope.Test) : base(scope)
        {
            _fileId = fileId;
        }

        public async Task Execute(IBoxClient client)
        {
            await client.FilesManager.DeleteAsync(_fileId);

            await client.FilesManager.PurgeTrashedAsync(_fileId);
        }
    }
}
