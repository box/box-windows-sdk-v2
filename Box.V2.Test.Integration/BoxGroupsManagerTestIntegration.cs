using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Box.V2.Models.Request;
using Box.V2.Exceptions;
using System.Linq;
using Box.V2.Models;

namespace Box.V2.Test.Integration
{
   
    [TestClass]
    public class BoxGroupsManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task GroupsWorkflow_ValidRequest_GetGroups()
        {
            // Get all groups and one individual group
            var allGroupsInit = await _client.GroupsManager.GetAllGroupsAsync();
            var oneGroup = await _client.GroupsManager.GetGroupAsync(allGroupsInit.Entries[0].Id);
            Assert.AreEqual<string>(allGroupsInit.Entries[0].Name, oneGroup.Name, "Did not retrieve the correct group");
            
            // Create a new group
            string groupName = GetUniqueName();

            BoxGroupRequest groupReq = new BoxGroupRequest()
            {
                Name = groupName,
            };

            var newGroup = await _client.GroupsManager.CreateAsync(groupReq);
            var allGroupsAfterAdd = await _client.GroupsManager.GetAllGroupsAsync();

            Assert.AreEqual(newGroup.Name, groupName, "New group does not have correct name");
            Assert.AreEqual(allGroupsInit.TotalCount + 1, allGroupsAfterAdd.TotalCount, "Number of groups after add is not correct");

            //Update the name of an existing group
            string updatedName = GetUniqueName();

            Assert.IsFalse(allGroupsInit.Entries.Any<BoxGroup>(x => x.Name.Equals(updatedName)), "A group with updatedName already exists");

            BoxGroupRequest updateRequest = new BoxGroupRequest()
            {
                Name = updatedName
            };

            var updatedGroup = await _client.GroupsManager.UpdateAsync(newGroup.Id, updateRequest);
            var allGroupsAfterUpdate = await _client.GroupsManager.GetAllGroupsAsync();

            Assert.AreEqual<string>(updatedGroup.Name, updatedName, "The group name was not updated correctly");
            Assert.AreEqual(1, allGroupsAfterUpdate.Entries.Count(x => x.Name.Equals(updatedName)), "The updated group name does not exist among all groups");
            Assert.IsFalse(allGroupsAfterUpdate.Entries.Any<BoxGroup>(x => x.Name.Equals(groupName)), "The old group name still exists among all groups");

            // Delete a group
            var delResult = await _client.GroupsManager.DeleteAsync(newGroup.Id);
            var allGroupsAfterDelete = await _client.GroupsManager.GetAllGroupsAsync();

            Assert.IsTrue(delResult, "Group was not deleted successfully");          
            Assert.AreEqual(allGroupsInit.TotalCount, allGroupsAfterDelete.TotalCount, "Number of groups after delete is not correct");
            Assert.IsFalse(allGroupsAfterDelete.Entries.Any<BoxGroup>(x => x.Id == newGroup.Id), "Deleted group still exists");
        }

        [TestMethod]
        public async Task GroupMembershipWorkflow_ValidRequest()
        {
            // Get current user
            var user = await _client.UsersManager.GetCurrentUserInformationAsync();

            // Get all the current memberships for this user
            var current_memberships = await _client.GroupsManager.GetAllGroupMembershipsForUserAsync(user.Id);

            // Create a new group
            string groupName = GetUniqueName();

            BoxGroupRequest groupReq = new BoxGroupRequest()
            {
                Name = groupName,
            };

            var newGroup = await _client.GroupsManager.CreateAsync(groupReq);

            // Create a membership
            BoxGroupMembershipRequest request = new BoxGroupMembershipRequest()
            {
                User = new BoxRequestEntity() { Id = user.Id },
                Group = new BoxGroupRequest() { Id = newGroup.Id }
            };

            var responseMembership = await _client.GroupsManager.AddMemberToGroupAsync(request);

            Assert.AreEqual<string>("group_membership", responseMembership.Type, "The type is not group_membership");
            Assert.AreEqual<string>("member", responseMembership.Role, "Membership role is not set correctly");
            Assert.AreEqual<string>(user.Id, responseMembership.User.Id, "User id not set correctly for membership");
            Assert.AreEqual<string>(newGroup.Id, responseMembership.Group.Id, "Group id not set correctly for membership");

            // Get the created group membership
            var membership = await _client.GroupsManager.GetGroupMembershipAsync(responseMembership.Id);
            Assert.AreEqual<string>("group_membership", membership.Type, "The type is not group_membership");
            Assert.AreEqual<string>("member", membership.Role, "Membership role is not set correctly");
            Assert.AreEqual<string>(user.Id, membership.User.Id, "User id not set correctly for membership");
            Assert.AreEqual<string>(newGroup.Id, membership.Group.Id, "Group id not set correctly for membership");

            // Update the group membership's role
            request = new BoxGroupMembershipRequest() { Role = "admin" };
            var updatedMembership = await _client.GroupsManager.UpdateGroupMembershipAsync(responseMembership.Id, request);
            Assert.AreEqual<string>("admin", updatedMembership.Role, "Membership role was not updated correctly");
            
            // Get all memberships for the given groups
            var memberships = await _client.GroupsManager.GetAllGroupMembershipsForGroupAsync(newGroup.Id);

            Assert.AreEqual<int>(1, memberships.Entries.Count, "Wrong count of memberships");
            Assert.AreEqual<int>(1, memberships.TotalCount, "Wrong total count of memberships");
            Assert.AreEqual<string>("group_membership", memberships.Entries[0].Type, "Wrong type");
            Assert.AreEqual<string>(newGroup.Id, memberships.Entries[0].Group.Id, "Wrong Group id");
            Assert.AreEqual<string>(user.Id, memberships.Entries[0].User.Id, "Wrong User id");

            // Get memberships for the user
            memberships = await _client.GroupsManager.GetAllGroupMembershipsForUserAsync(user.Id);

            Assert.AreEqual<int>(current_memberships.TotalCount + 1, memberships.TotalCount, "The total count of memberships for user did not increase");
            Assert.IsTrue(memberships.Entries.Exists(m => m.Id.Equals(membership.Id)), "Newly created group membership does not exist in this users list of memberships");

            // Delete the group membership
            bool success = await _client.GroupsManager.DeleteGroupMembershipAsync(membership.Id);
            memberships = await _client.GroupsManager.GetAllGroupMembershipsForGroupAsync(newGroup.Id);

            Assert.AreEqual<int>(0, memberships.Entries.Count, "Count should be 0");
            Assert.AreEqual<int>(0, memberships.TotalCount, "Total count should be 0");

            // Clean up - delete group
            var delResult = await _client.GroupsManager.DeleteAsync(newGroup.Id);
        }
    }
}
