using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    public class BoxTasksManager : BoxResourceManager
    {
        public BoxTasksManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        /// <summary>
        /// Used to assign a task to a single user. There can be multiple assignments on a given task.
        /// </summary>
        /// <param name="taskAssignmentRequest">The task assignment request.
        /// taskAssignmentRequest.Task.Id - Id of the task,
        /// taskAssignmentRequest.AssignTo.Id - The Id of the user this assignment is for,
        /// taskAssignmentRequest.AssignTo.Login - The login email address for the user this assignment is for.
        /// </param>
        /// <returns>A new task assignment object will be returned upon success.</returns>
        public async Task<BoxTaskAssignment> CreateTaskAssignmentAsync(BoxTaskAssignmentRequest taskAssignmentRequest)
        {
            taskAssignmentRequest.ThrowIfNull("taskAssignmentRequest")
                .Task.ThrowIfNull("taskAssignmentRequest.Task")
                .Id.ThrowIfNullOrWhiteSpace("taskAssignmentRequest.Task.Id");
            taskAssignmentRequest.AssignTo.ThrowIfNull("taskAssignmentRequest.AssignTo");
            if (string.IsNullOrEmpty(taskAssignmentRequest.AssignTo.Login) && string.IsNullOrEmpty(taskAssignmentRequest.AssignTo.Id))
            {
                throw new ArgumentException("At least one of Id or Login is required in this object.", "taskAssignmentRequest.AssignTo");
            }


            BoxRequest request = new BoxRequest(_config.TaskAssignmentsEndpointUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(taskAssignmentRequest));

            IBoxResponse<BoxTaskAssignment> response = await ToResponseAsync<BoxTaskAssignment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to update a task assignment.
        /// </summary>
        /// <param name="taskAssignmentUpdateRequest">The task assignment update request.
        /// taskAssignmentUpdateRequest.Id - Id of the task assignment,
        /// taskAssignmentUpdateRequest.Message - A message from the assignee about this task,
        /// taskAssignmentUpdateRequest.ResolutionState - Can be completed, incomplete, approved, or rejected.
        /// </param>
        /// <returns>A new task assignment object will be returned upon success.</returns>
        public async Task<BoxTaskAssignment> UpdateTaskAssignmentAsync(BoxTaskAssignmentUpdateRequest taskAssignmentUpdateRequest)
        {
            taskAssignmentUpdateRequest.ThrowIfNull("taskAssignmentUpdateRequest")
                .Id.ThrowIfNull("taskAssignmentUpdateRequest.Id");

            BoxRequest request = new BoxRequest(_config.TaskAssignmentsEndpointUri, taskAssignmentUpdateRequest.Id)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize(taskAssignmentUpdateRequest));

            IBoxResponse<BoxTaskAssignment> response = await ToResponseAsync<BoxTaskAssignment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Fetches a specific task assignment.
        /// </summary>
        /// <param name="taskAssignmentId">Id of the task assignment.</param>
        /// <returns>The specified task assignment object will be returned upon success.</returns>
        public async Task<BoxTaskAssignment> GetTaskAssignmentAsync(string taskAssignmentId)
        {
            taskAssignmentId.ThrowIfNull("taskAssignmentId");

            BoxRequest request = new BoxRequest(_config.TaskAssignmentsEndpointUri, taskAssignmentId)
                .Method(RequestMethod.Get);

            IBoxResponse<BoxTaskAssignment> response = await ToResponseAsync<BoxTaskAssignment>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Deletes a specific task assignment.
        /// </summary>
        /// <param name="taskAssignmentId">Id of the task assignment.</param>
        /// <returns>True will be returned upon success.</returns>
        public async Task<bool> DeleteTaskAssignmentAsync(string taskAssignmentId)
        {
            taskAssignmentId.ThrowIfNull("taskAssignmentId");

            BoxRequest request = new BoxRequest(_config.TaskAssignmentsEndpointUri, taskAssignmentId)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxTaskAssignment> response = await ToResponseAsync<BoxTaskAssignment>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Used to create a single task for single user on a single file.
        /// </summary>
        /// <param name="taskCreateRequest">The task create request.
        /// taskCreateRequest.Item.Id - The ID of the item this task is for,
        /// taskCreateRequest.Item.Type - The type of the item this task is for (currently only file is supported),
        /// taskCreateRequest.Message - An optional message to include with the task,
        /// taskCreateRequest.DueAt - The day at which this task is due.
        /// </param>
        /// <returns>A new task object will be returned upon success.</returns>
        public async Task<BoxTask> CreateTaskAsync(BoxTaskCreateRequest taskCreateRequest)
        {
            taskCreateRequest.ThrowIfNull("taskCreateRequest")
                .Item.ThrowIfNull("taskCreateRequest.Item")
                .Id.ThrowIfNullOrWhiteSpace("taskCreateRequest.Item.Id");
            taskCreateRequest.Item.Type.ThrowIfNull("taskCreateRequest.Item.Type");
            if (taskCreateRequest.Item.Type != BoxType.file)
            {
                throw new ArgumentException("Currently only file is supported", "taskCreateRequest.Item.Type");
            }
            BoxRequest request = new BoxRequest(_config.TasksEndpointUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(taskCreateRequest));

            IBoxResponse<BoxTask> response = await ToResponseAsync<BoxTask>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Updates a specific task.
        /// </summary>
        /// <param name="taskId">Id of the task.</param>
        /// <param name="taskUpdateRequest">The task update request.
        /// taskUpdateRequest.Action - The action the task assignee will be prompted to do. Can be review.,
        /// taskUpdateRequest.Message - An optional message to include with the task,
        /// taskUpdateRequest.DueAt - The day at which this task is due.
        /// </param>
        /// <returns>The updated task object will be returned upon success.</returns>
        public async Task<BoxTask> UpdateTaskAsync(string taskId, BoxTaskUpdateRequest taskUpdateRequest)
        {
            taskUpdateRequest.ThrowIfNull("taskUpdateRequest");

            BoxRequest request = new BoxRequest(_config.TasksEndpointUri, taskId)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(taskUpdateRequest));

            IBoxResponse<BoxTask> response = await ToResponseAsync<BoxTask>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Permanently deletes a specific task..
        /// </summary>
        /// <param name="taskId">Id of the task.</param>
        /// <returns>True will be returned upon success.</returns>
        public async Task<bool> DeleteTaskAsync(string taskId)
        {
            taskId.ThrowIfNull("taskId");

            BoxRequest request = new BoxRequest(_config.TasksEndpointUri, taskId)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxTaskAssignment> response = await ToResponseAsync<BoxTaskAssignment>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Fetches a specific task.
        /// </summary>
        /// <param name="taskId">Id of the task.</param>
        /// <returns>The specified task object will be returned upon success.</returns>
        public async Task<BoxTask> GetTaskAsync(string taskId)
        {
            taskId.ThrowIfNull("taskId");

            BoxRequest request = new BoxRequest(_config.TasksEndpointUri, taskId)
                .Method(RequestMethod.Get);

            IBoxResponse<BoxTask> response = await ToResponseAsync<BoxTask>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieves all of the assignments for a given task.
        /// </summary>
        /// <param name="taskId">Id of the task.</param>
        /// <returns>A collection of task assignment mini objects will be returned upon success.</returns>
        public async Task<BoxCollection<BoxTaskAssignment>> GetAssignmentsAsync(string taskId)
        {
            taskId.ThrowIfNull("taskId");

            BoxRequest request = new BoxRequest(_config.TasksEndpointUri, string.Format(Constants.TaskAssignmentsPathString, taskId))
                .Method(RequestMethod.Get);

            IBoxResponse<BoxCollection<BoxTaskAssignment>> response = await ToResponseAsync<BoxCollection<BoxTaskAssignment>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

    }
}
