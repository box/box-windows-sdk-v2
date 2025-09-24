using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class CreateSignRequestCommand : CommandBase, IDisposableCommand
    {
        private readonly string _signerEmail;
        private readonly string _fileId;
        private readonly string _folderId;

        public BoxSignRequest SignRequest;

        public CreateSignRequestCommand(string signerEmail, string fileId, string folderId = "0", CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.User) : base(scope, accessLevel)
        {
            _signerEmail = signerEmail;
            _fileId = fileId;
            _folderId = folderId;
        }

        public async Task<string> Execute(IBoxClient client)
        {
            var signRequestCreateRequest = new BoxSignRequestCreateRequest()
            {
                SourceFiles = new List<BoxSignRequestCreateSourceFile>()
                {
                    new BoxSignRequestCreateSourceFile()
                    {
                        Id = _fileId
                    }
                },
                Signers = new List<BoxSignRequestSignerCreate>()
                {
                    new BoxSignRequestSignerCreate()
                    {
                        Email = "sdk_integration_test@boxdemo.com",
                        RedirectUrl = new Uri("https://www.box.com/redirect_url_signer_1"),
                        DeclinedRedirectUrl = new Uri("https://www.box.com/declined_redirect_url_singer_1")
                    }
                },
                ParentFolder = new BoxRequestEntity()
                {
                    Id = _folderId,
                    Type = BoxType.folder
                },
                RedirectUrl = new Uri("https://www.box.com/redirect_url"),
                DeclinedRedirectUrl = new Uri("https://www.box.com/declined_redirect_url")
            };
            SignRequest = await client.SignRequestsManager.CreateSignRequestAsync(signRequestCreateRequest);
            return SignRequest.Id;
        }

        public async Task Dispose(IBoxClient client)
        {
            await client.SignRequestsManager.CancelSignRequestAsync(SignRequest.Id);
        }
    }
}
