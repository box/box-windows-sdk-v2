using System;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class CreateWebLinkCommand : CommandBase, IDisposableCommand
    {
        private readonly string _name;
        private readonly string _parentFolderId;

        private string _webLinkId;
        public BoxWebLink WebLink;

        public CreateWebLinkCommand(string name, string parentFolderId,
            CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.User) : base(scope, accessLevel)
        {
            _name = name;
            _parentFolderId = parentFolderId;
        }

        public async Task<string> Execute(IBoxClient client)
        {
            var webLinkRequest = new BoxWebLinkRequest()
            {
                Url = new Uri("http://www.box.com"),
                Name = _name,
                Description = "A weblink to Box.com",
                Parent = new BoxRequestEntity() { Id = _parentFolderId }
            };

            WebLink = await client.WebLinksManager.CreateWebLinkAsync(webLinkRequest);
            _webLinkId = WebLink.Id;

            return _webLinkId;
        }

        public async Task Dispose(IBoxClient client)
        {
            await client.WebLinksManager.DeleteWebLinkAsync(_webLinkId);
        }
    }
}
