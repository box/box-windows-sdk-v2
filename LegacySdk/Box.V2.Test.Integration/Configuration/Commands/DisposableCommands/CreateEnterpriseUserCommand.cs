using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class CreateEnterpriseUserCommand : CommandBase, IDisposableCommand
    {
        private string _id;
        private readonly string _name;
        private readonly string _externalAppUserId;

        public BoxUser BoxUser;

        public CreateEnterpriseUserCommand(string name, string externalAppUserId,
            CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.Admin) : base(scope, accessLevel)
        {
            _name = name;
            _externalAppUserId = externalAppUserId;
        }

        public async Task<string> Execute(IBoxClient client)
        {
            var userRequest = new BoxUserRequest
            {
                Name = _name,
                IsPlatformAccessOnly = true,
                ExternalAppUserId = _externalAppUserId
            };

            BoxUser = await client.UsersManager.CreateEnterpriseUserAsync(userRequest);
            _id = BoxUser.Id;

            return BoxUser.Id;
        }

        public async Task Dispose(IBoxClient client)
        {
            await client.UsersManager.DeleteEnterpriseUserAsync(_id, false, true);
        }
    }
}
