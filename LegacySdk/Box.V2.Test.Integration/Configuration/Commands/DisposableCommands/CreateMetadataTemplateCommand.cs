using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class CreateMetadataTemplateCommand : CommandBase, IDisposableCommand
    {
        private readonly List<BoxMetadataTemplateField> _metadataFields;

        public string TemplateKey;
        public BoxMetadataTemplate MetadataTemplate; 

        public CreateMetadataTemplateCommand(string templateKey, List<BoxMetadataTemplateField> metadataFields,
            CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.Admin) : base(scope, accessLevel)
        {
            TemplateKey = templateKey;
            _metadataFields = metadataFields;
        }

        public async Task<string> Execute(IBoxClient client)
        {
            var metadataTemplate = new BoxMetadataTemplate
            {
                DisplayName = TemplateKey,
                TemplateKey = TemplateKey,
                Scope = "enterprise",
                Fields = _metadataFields
            };

            var response = await client.MetadataManager.CreateMetadataTemplate(metadataTemplate);
            MetadataTemplate = response;

            return response.Id;
        }

        public async Task Dispose(IBoxClient client)
        {
            await client.MetadataManager.DeleteMetadataTemplate("enterprise", TemplateKey);
        }
    }
}
