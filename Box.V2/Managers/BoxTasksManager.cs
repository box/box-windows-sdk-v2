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
    }
}
