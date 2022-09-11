using System;
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
            };
            try
            {
                var response = await client.RetentionPoliciesManager.CreateRetentionPolicyAsync(retentionPolicyRequest);
                Policy = response;
            }
            catch
            {
                // TODO: 12-09-2022, @mcong
                // There is an error on backend side, which will return 409 status code "conflict"
                // but retention policy still created.
                // Delete this try-catch after the issue is fixed.
                var policies = await client.RetentionPoliciesManager.GetRetentionPoliciesAsync(_policyName);
                if (policies.Entries.Count == 1)
                {
                    // Retention policy already created.
                    var policy = policies.Entries[0];
                    var response = await client.RetentionPoliciesManager.GetRetentionPolicyAsync(policy.Id);
                    Policy = response;
                }

            }
            PolicyId = Policy.Id;

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

            return PolicyId;
        }

        public async Task Dispose(IBoxClient client)
        {
            var retentionPolicyRequest = new BoxRetentionPolicyRequest
            {
                Status = "retired"
            };
            try
            {
                await client.RetentionPoliciesManager.UpdateRetentionPolicyAsync(PolicyId, retentionPolicyRequest);
            }
            catch
            {
                // TODO: 12-09-2022, @mcong
                // There is an error on backend side, which will return 500 status code "Internal Server Error"
                // but retention policy still updated.
                // Delete this try-catch after the issue is fixed.
            }
        }
    }
}
