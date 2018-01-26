using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxCollaborationsManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task CollaborationsOnFolderWorkflow_LiveSession_ValidResponse()
        {
            const string folderId = "1927307787";

            // Add Collaboration
            BoxCollaborationRequest addRequest = new BoxCollaborationRequest() {
                Item = new BoxRequestEntity()
                {
                    Id = folderId,
                    Type = BoxType.folder
                },
                AccessibleBy = new BoxCollaborationUserRequest()
                {
                    Login = "BoxWinUser@box.com"
                },
                Role = "viewer"
            };

            BoxCollaboration collab =  await _client.CollaborationsManager.AddCollaborationAsync(addRequest, notify: false);

            Assert.AreEqual(folderId, collab.Item.Id, "Folder and collaboration folder id do not match");
            Assert.AreEqual(BoxCollaborationRoles.Viewer, collab.Role, "Incorrect collaboration role");

            // Edit Collaboration
            BoxCollaborationRequest editRequest = new BoxCollaborationRequest()
            {
                Id = collab.Id,
                Role = BoxCollaborationRoles.Editor,
                CanViewPath = true
            };

            BoxCollaboration editCollab = await _client.CollaborationsManager.EditCollaborationAsync(editRequest);

            Assert.AreEqual(collab.Id, editCollab.Id, "Id of original collaboration and updated collaboration do not match");
            Assert.AreEqual(BoxCollaborationRoles.Editor, editCollab.Role, "Incorrect updated role");

            // get existing collaboration
            var existingCollab = await _client.CollaborationsManager.GetCollaborationAsync(collab.Id, fields: new List<string>() { "can_view_path" });
            Assert.IsTrue(existingCollab.CanViewPath.Value, "failed to retrieve existing collab with specific fields");

            // test getting list of collaborations on folder
            var collabs = await _client.FoldersManager.GetCollaborationsAsync(folderId);
            Assert.AreEqual(4, collabs.Entries.Count, "Failed to get correct number of folder collabs.");

            // Test Remove Collaboration
            bool success = await _client.CollaborationsManager.RemoveCollaborationAsync(collab.Id);

            Assert.IsTrue(success, "Collaboration deletion was unsucessful");
        }

        // Test to add collaboration by Box User ID and Box Group ID. 
        [TestMethod]
        public async Task AddGroupCollaboration_File_Fields_ValidResponse()
        {
            const string fileId = "238288183114";
            const string groupId = "176708848";
            const string userId = "349294186";

            // Add Group Collaboration
            BoxCollaborationRequest addGroupRequest = new BoxCollaborationRequest()
            {
                Item = new BoxRequestEntity()
                {
                    Id = fileId,
                    Type = BoxType.file
                },
                AccessibleBy = new BoxCollaborationUserRequest()
                {
                    Type = BoxType.group,
                    Id = groupId
                },
                Role = "viewer"
            };

            //Add User Collaboration
            BoxCollaborationRequest addUserRequest = new BoxCollaborationRequest()
            {
                Item = new BoxRequestEntity()
                {
                    Id = fileId,
                    Type = BoxType.file
                },
                AccessibleBy = new BoxCollaborationUserRequest()
                {
                    Type = BoxType.user,
                    Id = userId
                },
                Role = "editor"
            };
            var groupFileCollaboration = await _client.CollaborationsManager.AddCollaborationAsync(addGroupRequest, notify: false);
            var userFileCollaboration = await _client.CollaborationsManager.AddCollaborationAsync(addUserRequest, notify: false);
        }

        [TestMethod]
        public async Task CollaborationsOnFileWorkflow_LiveSession_ValidResponse()
        {
            const string fileId = "100699285359";

            // Add Collaboration
            BoxCollaborationRequest addRequest = new BoxCollaborationRequest()
            {
                Item = new BoxRequestEntity()
                {
                    Id = fileId,
                    Type = BoxType.file
                },
                AccessibleBy = new BoxCollaborationUserRequest()
                {
                    Login = "BoxWinUser@box.com"
                },
                Role = "viewer"
            };

            BoxCollaboration collab = await _client.CollaborationsManager.AddCollaborationAsync(addRequest, notify: false);

            Assert.AreEqual(fileId, collab.Item.Id, "File and collaboration file id do not match");
            Assert.AreEqual(BoxCollaborationRoles.Viewer, collab.Role, "Incorrect collaboration role");

            
            // TODO: Edit Collaboration
            BoxCollaborationRequest editRequest = new BoxCollaborationRequest()
            {
                Id = collab.Id,
                Role = BoxCollaborationRoles.Editor
            };

            BoxCollaboration editCollab = await _client.CollaborationsManager.EditCollaborationAsync(editRequest);

            Assert.AreEqual(collab.Id, editCollab.Id, "Id of original collaboration and updated collaboration do not match");
            Assert.AreEqual(BoxCollaborationRoles.Editor, editCollab.Role, "Incorrect updated role");

            // get existing collaboration
            var existingCollab = await _client.CollaborationsManager.GetCollaborationAsync(collab.Id);
            Assert.IsTrue(existingCollab.Item.Id == fileId, "failed to retrieve existing collab");

            // test getting list of collaborations on file
            var collabs = await _client.FilesManager.GetCollaborationsAsync(fileId);
            Assert.AreEqual(4, collabs.Entries.Count, "Failed to get correct number of file collabs.");      

            // Test Remove Collaboration
            bool success = await _client.CollaborationsManager.RemoveCollaborationAsync(collab.Id);

            Assert.IsTrue(success, "Collaboration deletion was unsucessful");
        }
    }
}
