using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using Box.V2.Models.Request;
using System;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxTasksManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task TasksWorkflow_ValidRequest()
        {
            // Create a task
            var task = await _client.TasksManager.CreateTaskAsync(new BoxTaskCreateRequest()
            {
                Item = new BoxRequestEntity() {
                    Id = "100699285359",
                    Type = BoxType.file
                },
                Message = "REVIEW PLZ K THX",
                DueAt = DateTime.Now.AddDays(30)
            });

            // Creat task assignment
            var taskAssignment = await _client.TasksManager.CreateTaskAssignmentAsync(new BoxTaskAssignmentRequest()
            {
                Task = new BoxTaskRequest()
                {
                    Id = task.Id
                },
                AssignTo = new BoxAssignmentRequest()
                {
                    Id = "215917383"
                }
            });

            // Get task
            var gTask = await _client.TasksManager.GetTaskAsync(task.Id);
            Assert.AreEqual(gTask.Message, task.Message, "Task does not have the same message");

            // Get task assignment
            var gTaskAssignment = await _client.TasksManager.GetTaskAssignmentAsync(taskAssignment.Id);
            Assert.AreEqual(taskAssignment.AssignedTo.Id, gTaskAssignment.AssignedTo.Id, "Task does not have the same message");

            // Update task
            var uTask = await _client.TasksManager.UpdateTaskAsync(new BoxTaskUpdateRequest() {
                Id = task.Id,
                Message = "PLZ"
            });
            Assert.AreEqual(uTask.Message, "PLZ", "Task message does not update!");

            // Update task assignment
            var uTaskAssignment = await _client.TasksManager.UpdateTaskAssignmentAsync(new BoxTaskAssignmentUpdateRequest() {
                Id = taskAssignment.Id,
                Message = "TA Update MSG"
            });
            Assert.AreEqual(uTaskAssignment.Message, "TA Update MSG", "Task assignment message does not update!");

            // Get task assignments
            var taskAssignments = await _client.TasksManager.GetAssignmentsAsync(task.Id);
            Assert.AreEqual(1, taskAssignments.Entries.Count, "Task assignmnet number are incorrect!");
            Assert.AreEqual(taskAssignments.Entries[0].Id, uTaskAssignment.Id, "Task assignment id are incorrect!");

            // Delete task assignment 
            await _client.TasksManager.DeleteTaskAssignmentAsync(taskAssignment.Id);

            // Delete task
            await _client.TasksManager.DeleteTaskAsync(task.Id);
        }
    }
}
