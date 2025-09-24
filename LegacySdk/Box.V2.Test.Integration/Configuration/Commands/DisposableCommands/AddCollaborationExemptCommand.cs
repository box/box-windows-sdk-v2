using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class AddCollaborationExemptCommand : CommandBase, IDisposableCommand
    {
        private readonly string _userId;

        public BoxCollaborationWhitelistTargetEntry WhitelistTargetEntry;

        public AddCollaborationExemptCommand(string userId, CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.Admin)
            : base(scope, accessLevel)
        {
            _userId = userId;
        }

        public async Task<string> Execute(IBoxClient client)
        {
            var whitelistTargetEntry = await client.CollaborationWhitelistManager.AddCollaborationWhitelistExemptUserAsync(_userId);

            WhitelistTargetEntry = whitelistTargetEntry;

            return WhitelistTargetEntry.Id;
        }

        public async Task Dispose(IBoxClient client)
        {
            await client.CollaborationWhitelistManager.DeleteCollaborationWhitelistExemptUserAsync(WhitelistTargetEntry.Id);
        }
    }
}

