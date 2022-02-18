using System.Collections.Generic;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class ApplyMetadataCommand : CommandBase, IDisposableCommand
    {
        private readonly string _templateKey;
        private readonly string _fileId;
        private readonly Dictionary<string, object> _metadata;

        public ApplyMetadataCommand(string templateKey, string fileId, Dictionary<string, object> metadata,
            CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.User) : base(scope, accessLevel)
        {
            _templateKey = templateKey;
            _fileId = fileId;
            _metadata = metadata;
        }

        public async Task<string> Execute(IBoxClient client)
        {
            await client.MetadataManager.CreateFileMetadataAsync(_fileId, _metadata, "enterprise", _templateKey);

            return _templateKey;
        }

        public async Task Dispose(IBoxClient client)
        {
            await client.MetadataManager.DeleteFileMetadataAsync(_fileId, "enterprise", _templateKey);
        }
    }
}
