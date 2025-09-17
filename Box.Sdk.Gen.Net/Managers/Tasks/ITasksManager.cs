using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ITasksManager {
        /// <summary>
    /// Retrieves a list of all the tasks for a file. This
    /// endpoint does not support pagination.
    /// </summary>
    /// <param name="fileId">
    /// The unique identifier that represents a file.
    /// 
    /// The ID for any file can be determined
    /// by visiting a file in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/files/123`
    /// the `file_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of getFileTasks method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Tasks> GetFileTasksAsync(string fileId, GetFileTasksHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a single task on a file. This task is not assigned to any user and
    /// will need to be assigned separately.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createTask method
    /// </param>
    /// <param name="headers">
    /// Headers of createTask method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Task> CreateTaskAsync(CreateTaskRequestBody requestBody, CreateTaskHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves information about a specific task.
    /// </summary>
    /// <param name="taskId">
    /// The ID of the task.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of getTaskById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Task> GetTaskByIdAsync(string taskId, GetTaskByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a task. This can be used to update a task's configuration, or to
    /// update its completion state.
    /// </summary>
    /// <param name="taskId">
    /// The ID of the task.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateTaskById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateTaskById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Task> UpdateTaskByIdAsync(string taskId, UpdateTaskByIdRequestBody? requestBody = default, UpdateTaskByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Removes a task from a file.
    /// </summary>
    /// <param name="taskId">
    /// The ID of the task.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteTaskById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteTaskByIdAsync(string taskId, DeleteTaskByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}