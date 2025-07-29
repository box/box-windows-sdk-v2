using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ITaskAssignmentsManager {
        /// <summary>
    /// Lists all of the assignments for a given task.
    /// </summary>
    /// <param name="taskId">
    /// The ID of the task.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of getTaskAssignments method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TaskAssignments> GetTaskAssignmentsAsync(string taskId, GetTaskAssignmentsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Assigns a task to a user.
    /// 
    /// A task can be assigned to more than one user by creating multiple
    /// assignments.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createTaskAssignment method
    /// </param>
    /// <param name="headers">
    /// Headers of createTaskAssignment method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TaskAssignment> CreateTaskAssignmentAsync(CreateTaskAssignmentRequestBody requestBody, CreateTaskAssignmentHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves information about a task assignment.
    /// </summary>
    /// <param name="taskAssignmentId">
    /// The ID of the task assignment.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of getTaskAssignmentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TaskAssignment> GetTaskAssignmentByIdAsync(string taskAssignmentId, GetTaskAssignmentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a task assignment. This endpoint can be
    /// used to update the state of a task assigned to a user.
    /// </summary>
    /// <param name="taskAssignmentId">
    /// The ID of the task assignment.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateTaskAssignmentById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateTaskAssignmentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<TaskAssignment> UpdateTaskAssignmentByIdAsync(string taskAssignmentId, UpdateTaskAssignmentByIdRequestBody? requestBody = default, UpdateTaskAssignmentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a specific task assignment.
    /// </summary>
    /// <param name="taskAssignmentId">
    /// The ID of the task assignment.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteTaskAssignmentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteTaskAssignmentByIdAsync(string taskAssignmentId, DeleteTaskAssignmentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}