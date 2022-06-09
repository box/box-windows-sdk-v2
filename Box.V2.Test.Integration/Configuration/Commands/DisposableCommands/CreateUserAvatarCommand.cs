using System.IO;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class CreateUserAvatarCommand : CommandBase, IDisposableCommand
    {
        private readonly string _userId;
        private readonly string _filePath;

        public BoxUploadAvatarResponse Response;

        public CreateUserAvatarCommand(string userId, string filePath,
            CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.Admin) : base(scope, accessLevel)
        {
            _userId = userId;
            _filePath = filePath;
        }

        public async Task<string> Execute(IBoxClient client)
        {
            using (var fileStream = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                Response = await client.UsersManager.AddOrUpdateUserAvatarAsync(_userId, fileStream);
            }

            return "0";
        }

        public async Task Dispose(IBoxClient client)
        {
            await client.UsersManager.DeleteUserAvatarAsync(_userId);
        }
    }
}
