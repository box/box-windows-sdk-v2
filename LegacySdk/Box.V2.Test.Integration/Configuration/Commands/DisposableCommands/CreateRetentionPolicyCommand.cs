using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;

namespace Box.V2.Test.Integration.Configuration.Commands.DisposableCommands
{
    public class CreateRetentionPolicyCommand : CommandBase, IDisposableCommand
    {
        private readonly string _policyName;
        private readonly string _folderId;

        public string PolicyId;
        public BoxRetentionPolicy Policy;

        public CreateRetentionPolicyCommand(string folderId, string policyName, CommandScope scope = CommandScope.Test, CommandAccessLevel accessLevel = CommandAccessLevel.Admin) : base(scope, accessLevel)
        {
            _policyName = policyName;
            _folderId = folderId;
        }

        public async Task<string> Execute(IBoxClient client)
        {
            var retentionPolicyRequest = new BoxRetentionPolicyRequest
            {
                PolicyName = _policyName,
                PolicyType = "finite",
                RetentionLength = 1,
                DispositionAction = DispositionAction.permanently_delete.ToString(),
                RetentionType = BoxRetentionType.modifiable
            };
            var response = await client.RetentionPoliciesManager.CreateRetentionPolicyAsync(retentionPolicyRequest);
            Policy = response;
            PolicyId = Policy.Id;

            if (_folderId != null)
            {
                var assignmentRequest = new BoxRetentionPolicyAssignmentRequest()
                {
                    PolicyId = PolicyId,
                    AssignTo = new BoxRequestEntity()
                    {
                        Type = BoxType.folder,
                        Id = _folderId
                    }
                };

                await client.RetentionPoliciesManager.CreateRetentionPolicyAssignmentAsync(assignmentRequest);
            }

            return PolicyId;
        }

        public async Task Dispose(IBoxClient client)
        {
            var retentionPolicyRequest = new BoxRetentionPolicyRequest
            {
                Status = "retired"
            };

            await client.RetentionPoliciesManager.UpdateRetentionPolicyAsync(PolicyId, retentionPolicyRequest);
        }
    }
}
