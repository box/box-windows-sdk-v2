using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxCollaborationsManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task CollaborationsOnFolderWorkflow_LiveSession_ValidResponse()
        {
            const string FolderId = "1927307787";

            // Add Collaboration
            var addRequest = new BoxCollaborationRequest()
            {
                Item = new BoxRequestEntity()
                {
                    Id = FolderId,
                    Type = BoxType.folder
                },
                AccessibleBy = new BoxCollaborationUserRequest()
                {
                    Login = "BoxWinUser@box.com"
                },
                Role = "viewer"
            };

            BoxCollaboration collab = await Client.CollaborationsManager.AddCollaborationAsync(addRequest, notify: false);

            Assert.AreEqual(FolderId, collab.Item.Id, "Folder and collaboration folder id do not match");
            Assert.AreEqual(BoxCollaborationRoles.Viewer, collab.Role, "Incorrect collaboration role");

            // Edit Collaboration
            var editRequest = new BoxCollaborationRequest()
            {
                Id = collab.Id,
                Role = BoxCollaborationRoles.Editor,
                CanViewPath = true
            };

            BoxCollaboration editCollab = await Client.CollaborationsManager.EditCollaborationAsync(editRequest);

            Assert.AreEqual(collab.Id, editCollab.Id, "Id of original collaboration and updated collaboration do not match");
            Assert.AreEqual(BoxCollaborationRoles.Editor, editCollab.Role, "Incorrect updated role");

            // get existing collaboration
            var existingCollab = await Client.CollaborationsManager.GetCollaborationAsync(collab.Id, fields: new List<string>() { "can_view_path" });
            Assert.IsTrue(existingCollab.CanViewPath.Value, "failed to retrieve existing collab with specific fields");

            // test getting list of collaborations on folder
            var collabs = await Client.FoldersManager.GetCollaborationsAsync(FolderId);
            Assert.AreEqual(4, collabs.Entries.Count, "Failed to get correct number of folder collabs.");

            // Test Remove Collaboration
            var success = await Client.CollaborationsManager.RemoveCollaborationAsync(collab.Id);

            Assert.IsTrue(success, "Collaboration deletion was unsucessful");
        }

        // Test to add collaboration by Box User ID and Box Group ID. 
        [TestMethod]
        public async Task AddGroupCollaboration_File_Fields_ValidResponse()
        {
            const string FileId = "238288183114";
            const string GroupId = "176708848";
            const string UserId = "349294186";

            // Add Group Collaboration
            var addGroupRequest = new BoxCollaborationRequest()
            {
                Item = new BoxRequestEntity()
                {
                    Id = FileId,
                    Type = BoxType.file
                },
                AccessibleBy = new BoxCollaborationUserRequest()
                {
                    Type = BoxType.group,
                    Id = GroupId
                },
                Role = "viewer"
            };

            //Add User Collaboration
            var addUserRequest = new BoxCollaborationRequest()
            {
                Item = new BoxRequestEntity()
                {
                    Id = FileId,
                    Type = BoxType.file
                },
                AccessibleBy = new BoxCollaborationUserRequest()
                {
                    Type = BoxType.user,
                    Id = UserId
                },
                Role = "editor"
            };
            _ = await Client.CollaborationsManager.AddCollaborationAsync(addGroupRequest, notify: false);
            _ = await Client.CollaborationsManager.AddCollaborationAsync(addUserRequest, notify: false);
        }

        [TestMethod]
        public async Task CollaborationsOnFileWorkflow_LiveSession_ValidResponse()
        {
            const string FileId = "100699285359";

            // Add Collaboration
            var addRequest = new BoxCollaborationRequest()
            {
                Item = new BoxRequestEntity()
                {
                    Id = FileId,
                    Type = BoxType.file
                },
                AccessibleBy = new BoxCollaborationUserRequest()
                {
                    Login = "BoxWinUser@box.com"
                },
                Role = "viewer"
            };

            BoxCollaboration collab = await Client.CollaborationsManager.AddCollaborationAsync(addRequest, notify: false);

            Assert.AreEqual(FileId, collab.Item.Id, "File and collaboration file id do not match");
            Assert.AreEqual(BoxCollaborationRoles.Viewer, collab.Role, "Incorrect collaboration role");


            // TODO: Edit Collaboration
            var editRequest = new BoxCollaborationRequest()
            {
                Id = collab.Id,
                Role = BoxCollaborationRoles.Editor
            };

            BoxCollaboration editCollab = await Client.CollaborationsManager.EditCollaborationAsync(editRequest);

            Assert.AreEqual(collab.Id, editCollab.Id, "Id of original collaboration and updated collaboration do not match");
            Assert.AreEqual(BoxCollaborationRoles.Editor, editCollab.Role, "Incorrect updated role");

            // get existing collaboration
            var existingCollab = await Client.CollaborationsManager.GetCollaborationAsync(collab.Id);
            Assert.IsTrue(existingCollab.Item.Id == FileId, "failed to retrieve existing collab");

            // test getting list of collaborations on file
            var collabs = await Client.FilesManager.GetCollaborationsAsync(FileId);
            Assert.AreEqual(4, collabs.Entries.Count, "Failed to get correct number of file collabs.");

            // Test Remove Collaboration
            var success = await Client.CollaborationsManager.RemoveCollaborationAsync(collab.Id);

            Assert.IsTrue(success, "Collaboration deletion was unsucessful");
        }
    }
}
