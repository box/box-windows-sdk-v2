using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class CreateWebhookCommand : CommandBase, IDisposableCommand
    {
        private readonly string _targetId;
        private readonly BoxType _targetType;
        private readonly string _address;
        private readonly List<string> _triggers;

        private string _webhookId;
        public BoxWebhook Webhook;

        public CreateWebhookCommand(string targetId, BoxType targetType, string address, List<string> triggers,
            CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.User) : base(scope, accessLevel)
        {
            _targetId = targetId;
            _targetType = targetType;
            _address = address;
            _triggers = triggers;
        }

        public async Task<string> Execute(IBoxClient client)
        {
            var webhookRequest = new BoxWebhookRequest()
            {
                Target = new BoxRequestEntity()
                {
                    Id = _targetId,
                    Type = _targetType
                },
                Address = _address,
                Triggers = _triggers,
            };

            Webhook = await client.WebhooksManager.CreateWebhookAsync(webhookRequest);
            _webhookId = Webhook.Id;

            return _webhookId;
        }

        public async Task Dispose(IBoxClient client)
        {
            await client.WebhooksManager.DeleteWebhookAsync(_webhookId);
        }
    }
}
