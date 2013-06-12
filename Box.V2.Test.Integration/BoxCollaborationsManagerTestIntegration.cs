using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxCollaborationsManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        private const string folderId = "930744373";

        [TestMethod]
        public async Task CollaborationsWorkflow_LiveSession_ValidResponse()
        {
            // Test Add Collaboration
            BoxCollaborationRequest addRequest = new BoxCollaborationRequest(){
                Item = new BoxRequestEntity() {
                    Id = folderId,
                    Type = BoxType.folder
                },
                AccessibleBy = new BoxCollaborationUserRequest(){
                    Login = "btang@box.com"
                },
                Role = "viewer"
            };

            BoxCollaboration collab =  await _client.CollaborationsManager.AddCollaborationAsync(addRequest);

            Assert.AreEqual(folderId, collab.Item.Id);
            Assert.AreEqual(BoxCollaborationRoles.Viewer, collab.Role);

            // Test Edit Collaboration
            BoxCollaborationRequest editRequest = new BoxCollaborationRequest()
            {
                Id = collab.Id,
                Role = BoxCollaborationRoles.Editor 
            };

            BoxCollaboration editCollab = await _client.CollaborationsManager.EditCollaborationAsync(editRequest);

            Assert.AreEqual(collab.Id, editCollab.Id);
            Assert.AreEqual(BoxCollaborationRoles.Editor, editCollab.Role);

            // Test Remove Collaboration
            await _client.CollaborationsManager.RemoveCollaborationAsync(collab.Id);
        }
    }
}
