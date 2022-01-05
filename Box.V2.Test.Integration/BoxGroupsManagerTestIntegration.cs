using System.Linq;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Box.V2.Config.Constants;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxGroupsManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task GroupsWorkflow_ValidRequest_GetGroups()
        {
            // Get all groups and one individual group
            var allGroupsInit = await Client.GroupsManager.GetAllGroupsAsync();
            var oneGroup = await Client.GroupsManager.GetGroupAsync(allGroupsInit.Entries[0].Id);
            Assert.AreEqual(allGroupsInit.Entries[0].Name, oneGroup.Name, "Did not retrieve the correct group");

            // Create a new group
            var groupName = GetUniqueName();

            var groupReq = new BoxGroupRequest()
            {
                Name = groupName,
                InvitabilityLevel = RequestParameters.AdminsOnly,
                MemberViewabilityLevel = RequestParameters.AdminsOnly
            };

            var newGroup = await Client.GroupsManager.CreateAsync(groupReq);
            var allGroupsAfterAdd = await Client.GroupsManager.GetAllGroupsAsync(limit: 3, autoPaginate: true);

            Assert.AreEqual(newGroup.Name, groupName, "New group does not have correct name");
            Assert.AreEqual(allGroupsInit.TotalCount + 1, allGroupsAfterAdd.TotalCount, "Number of groups after add is not correct");

            //Update the name of an existing group
            var updatedName = GetUniqueName();

            Assert.IsFalse(allGroupsInit.Entries.Any(x => x.Name.Equals(updatedName)), "A group with updatedName already exists");

            var updateRequest = new BoxGroupRequest()
            {
                Name = updatedName
            };

            var updatedGroup = await Client.GroupsManager.UpdateAsync(newGroup.Id, updateRequest);
            var allGroupsAfterUpdate = await Client.GroupsManager.GetAllGroupsAsync();

            Assert.AreEqual(updatedGroup.Name, updatedName, "The group name was not updated correctly");
            Assert.AreEqual(1, allGroupsAfterUpdate.Entries.Count(x => x.Name.Equals(updatedName)), "The updated group name does not exist among all groups");
            Assert.IsFalse(allGroupsAfterUpdate.Entries.Any(x => x.Name.Equals(groupName)), "The old group name still exists among all groups");

            // Delete a group
            var delResult = await Client.GroupsManager.DeleteAsync(newGroup.Id);
            var allGroupsAfterDelete = await Client.GroupsManager.GetAllGroupsAsync();

            Assert.IsTrue(delResult, "Group was not deleted successfully");
            Assert.AreEqual(allGroupsInit.TotalCount, allGroupsAfterDelete.TotalCount, "Number of groups after delete is not correct");
            Assert.IsFalse(allGroupsAfterDelete.Entries.Any(x => x.Id == newGroup.Id), "Deleted group still exists");
        }

        [TestMethod]
        public async Task GroupMembershipWorkflow_ValidRequest()
        {
            // Get current user
            var user = await Client.UsersManager.GetCurrentUserInformationAsync();

            // Get all the current memberships for this user
            var current_memberships = await Client.GroupsManager.GetAllGroupMembershipsForUserAsync(user.Id);

            // Create a new group
            var groupName = GetUniqueName();

            var groupReq = new BoxGroupRequest()
            {
                Name = groupName,
            };

            var newGroup = await Client.GroupsManager.CreateAsync(groupReq);

            // Create a membership
            var request = new BoxGroupMembershipRequest()
            {
                User = new BoxRequestEntity() { Id = user.Id },
                Group = new BoxGroupRequest() { Id = newGroup.Id }
            };

            var responseMembership = await Client.GroupsManager.AddMemberToGroupAsync(request);

            Assert.AreEqual("group_membership", responseMembership.Type, "The type is not group_membership");
            Assert.AreEqual("member", responseMembership.Role, "Membership role is not set correctly");
            Assert.AreEqual(user.Id, responseMembership.User.Id, "User id not set correctly for membership");
            Assert.AreEqual(newGroup.Id, responseMembership.Group.Id, "Group id not set correctly for membership");

            // Get the created group membership
            var membership = await Client.GroupsManager.GetGroupMembershipAsync(responseMembership.Id);
            Assert.AreEqual("group_membership", membership.Type, "The type is not group_membership");
            Assert.AreEqual("member", membership.Role, "Membership role is not set correctly");
            Assert.AreEqual(user.Id, membership.User.Id, "User id not set correctly for membership");
            Assert.AreEqual(newGroup.Id, membership.Group.Id, "Group id not set correctly for membership");

            // Update the group membership's role
            request = new BoxGroupMembershipRequest() { Role = "admin" };
            var updatedMembership = await Client.GroupsManager.UpdateGroupMembershipAsync(responseMembership.Id, request);
            Assert.AreEqual("admin", updatedMembership.Role, "Membership role was not updated correctly");

            // Get all memberships for the given groups
            var memberships = await Client.GroupsManager.GetAllGroupMembershipsForGroupAsync(newGroup.Id);

            Assert.AreEqual(1, memberships.Entries.Count, "Wrong count of memberships");
            Assert.AreEqual(1, memberships.TotalCount, "Wrong total count of memberships");
            Assert.AreEqual("group_membership", memberships.Entries[0].Type, "Wrong type");
            Assert.AreEqual(newGroup.Id, memberships.Entries[0].Group.Id, "Wrong Group id");
            Assert.AreEqual(user.Id, memberships.Entries[0].User.Id, "Wrong User id");

            // Add this group to a folder
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
                    Type = BoxType.group,
                    Id = newGroup.Id
                },
                Role = "viewer"
            };

            BoxCollaboration collab = await Client.CollaborationsManager.AddCollaborationAsync(addRequest, notify: false);

            Assert.AreEqual(FolderId, collab.Item.Id, "Folder and collaboration folder id do not match");
            Assert.AreEqual(BoxCollaborationRoles.Viewer, collab.Role, "Incorrect collaboration role");

            // Get all collaborations for the give group
            var collabs = await Client.GroupsManager.GetCollaborationsForGroupAsync(newGroup.Id);
            Assert.AreEqual(1, collabs.Entries.Count, "Wrong count of collaborations");
            Assert.AreEqual(1, collabs.TotalCount, "Wrong total count of collaborations");

            collab = collabs.Entries[0];
            Assert.AreEqual<string>(newGroup.Id, collab.AccessibleBy.Id, "Wrong Group Id");
            Assert.AreEqual<string>("viewer", collab.Role, "Wrong Role Type");

            // Get memberships for the user
            memberships = await Client.GroupsManager.GetAllGroupMembershipsForUserAsync(user.Id);

            Assert.AreEqual(current_memberships.TotalCount + 1, memberships.TotalCount, "The total count of memberships for user did not increase");
            Assert.IsTrue(memberships.Entries.Exists(m => m.Id.Equals(membership.Id)), "Newly created group membership does not exist in this users list of memberships");

            // Delete the group membership
            var success = await Client.GroupsManager.DeleteGroupMembershipAsync(membership.Id);
            memberships = await Client.GroupsManager.GetAllGroupMembershipsForGroupAsync(newGroup.Id);

            Assert.AreEqual(0, memberships.Entries.Count, "Count should be 0");
            Assert.AreEqual(0, memberships.TotalCount, "Total count should be 0");

            // Clean up - delete group
            var delResult = await Client.GroupsManager.DeleteAsync(newGroup.Id);
        }
    }
}
