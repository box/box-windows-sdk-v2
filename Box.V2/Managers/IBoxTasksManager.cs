using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    public interface IBoxTasksManager
    {
        /// <summary>
        /// Used to assign a task to a single user. There can be multiple assignments on a given task.
        /// </summary>
        /// <param name="taskAssignmentRequest">BoxTaskAssignmentRequest object.</param>
        /// <returns>A new task assignment object will be returned upon success.</returns>
        Task<BoxTaskAssignment> CreateTaskAssignmentAsync(BoxTaskAssignmentRequest taskAssignmentRequest);

        /// <summary>
        /// Used to update a task assignment.
        /// </summary>
        /// <param name="taskAssignmentUpdateRequest">BoxTaskAssignmentUpdateRequest object.</param>
        /// <returns>A new task assignment object will be returned upon success.</returns>
        Task<BoxTaskAssignment> UpdateTaskAssignmentAsync(BoxTaskAssignmentUpdateRequest taskAssignmentUpdateRequest);

        /// <summary>
        /// Fetches a specific task assignment.
        /// </summary>
        /// <param name="taskAssignmentId">Id of the task assignment.</param>
        /// <returns>The specified task assignment object will be returned upon success.</returns>
        Task<BoxTaskAssignment> GetTaskAssignmentAsync(string taskAssignmentId);

        /// <summary>
        /// Deletes a specific task assignment.
        /// </summary>
        /// <param name="taskAssignmentId">Id of the task assignment.</param>
        /// <returns>True will be returned upon success.</returns>
        Task<bool> DeleteTaskAssignmentAsync(string taskAssignmentId);

        /// <summary>
        /// Used to create a single task for single user on a single file.
        /// </summary>
        /// <param name="taskCreateRequest">BoxTaskCreateRequest object.</param>
        /// <returns>A new task object will be returned upon success.</returns>
        Task<BoxTask> CreateTaskAsync(BoxTaskCreateRequest taskCreateRequest);

        /// <summary>
        /// Updates a specific task.
        /// </summary>
        /// <param name="taskUpdateRequest">BoxTaskUpdateRequest object.</param>
        /// <returns>The updated task object will be returned upon success.</returns>
        Task<BoxTask> UpdateTaskAsync(BoxTaskUpdateRequest taskUpdateRequest);

        /// <summary>
        /// Permanently deletes a specific task.
        /// </summary>
        /// <param name="taskId">Id of the task.</param>
        /// <returns>True will be returned upon success.</returns>
        Task<bool> DeleteTaskAsync(string taskId);

        /// <summary>
        /// Fetches a specific task.
        /// </summary>
        /// <param name="taskId">Id of the task.</param>
        /// <returns>The specified task object will be returned upon success.</returns>
        Task<BoxTask> GetTaskAsync(string taskId);

        /// <summary>
        /// Retrieves all of the assignments for a given task.
        /// </summary>
        /// <param name="taskId">Id of the task.</param>
        /// <returns>A collection of task assignment mini objects will be returned upon success.</returns>
        Task<BoxCollection<BoxTaskAssignment>> GetAssignmentsAsync(string taskId);
    }
}
