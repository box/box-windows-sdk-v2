Tasks
=====

Tasks enable file-centric workflows in Box. User can create tasks on files and assign them to collaborators on Box.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Get a Task's Information](#get-a-tasks-information)
- [Get Tasks on a File](#get-tasks-on-a-file)
- [Create a Task](#create-a-task)
- [Update a Task](#update-a-task)
- [Delete a Task](#delete-a-task)
- [Get Assignments for a Task](#get-assignments-for-a-task)
- [Assign Task](#assign-task)
- [Get Task Assignment](#get-task-assignment)
- [Update Task Assignment](#update-task-assignment)
- [Remove Task Assignment](#remove-task-assignment)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Get a Task's Information
------------------------

To get a task information call `TasksManager.GetTaskAsync(string taskId)` with the ID of the task.

<!-- sample get_tasks_id -->
```c#
BoxTask task = await client.TasksManager.GetTaskAsync("11111");
```

Get Tasks on a File
-------------------

Calling the `FilesManager.GetFileTasks(string id, IEnumerable<string> fields = null)`
method will retrieve all of the tasks for the given file.

<!-- sample get_files_id_tasks -->
```c#
BoxCollection<BoxTask> tasks = await client.FilesManager.FilesManager.GetFileTasks("11111");
```

Create a Task
-------------

To create a task, call `TasksManager.CreateTaskAsync(BoxTaskCreateRequest taskCreateRequest)` with the
parameters of the task.


<!-- sample post_tasks -->
```c#
var taskParams = new BoxTaskCreateRequest()
{
    Item = new BoxRequestEntity()
    {
        Type = BoxType.file,
        Id = "11111"
    },
    Message = "Please review!"
};
BoxTask task = await client.TasksManager.CreateTaskAsync(taskParams);
```

Update a Task
-------------

To update a task, call
`TasksManager.UpdateTaskAsync(BoxTaskUpdateRequest taskUpdateRequest)`.

<!-- sample put_tasks_id -->
```c#
var updates = new BoxTaskUpdateRequest()
{
    Id = "22222",
    Message = "Could you please review this?"
};
BoxTask updatedTask = await client.TasksManager.UpdateTaskAsync(updates);
```

Delete a Task
-------------

To delete a task, call the `TasksManager.DeleteTaskAsync(string taskId)` method with the ID of the task to be deleted.

<!-- sample delete_tasks_id -->
```c#
await client.TasksManager.DeleteTaskAsync("11111");
```

Get Assignments for a Task
--------------------------

To get a list of assignments for a task, which associate the task to users who
must complete it, call `TasksManager.GetAssignmentsAsync(string taskId)` with the ID of the task.

<!-- sample get_tasks_id_assignments -->
```c#
BoxCollection<BoxTaskAssignment> assignments = await client.TasksManager
    .GetAssignmentsAsync(taskId: "11111");
```

Assign Task
-----------

To assign a task to a user, call
`TasksManager.CreateTaskAssignmentAsync(BoxTaskAssignmentRequest taskAssignmentRequest)`
with the ID of the task to assign and either the ID or login email address of the
user to whom the task should be assigned.

<!-- sample post_task_assignments -->
```c#
// Assign task 11111 to user 22222
var assignmentParams = new BoxTaskAssignmentRequest()
{
    Task = new BoxTaskRequest()
    {
        Id = "11111"
    },
    AssignTo = new BoxAssignmentRequest()
    {
        Id = "22222"
    }
};
BoxTaskAssignment assignment = await client.TasksManager.CreateTaskAssignmentAsync(assignmentParams);
```

```c#
// Assign task 11111 to user with login user@example.com
var assignmentParams = new BoxTaskAssignmentRequest()
{
    Task = new BoxTaskRequest()
    {
        Id = "11111"
    },
    AssignTo = new BoxAssignmentRequest()
    {
        Login = "user@example.com"
    }
};
BoxTaskAssignment assignment = await client.TasksManager.CreateTaskAssignmentAsync(assignmentParams);
```

Get Task Assignment
-------------------

To retrieve information about a specific task assignment, call the
`TasksManager.GetTaskAssignmentAsync(string taskAssignmentId)`
method with the ID of the assignment to get.

<!-- sample get_task_assignments_id -->
```c#
BoxTaskAssignment assignment = await client.TasksManager.GetTaskAssignmentAsync("12345");
```

Update Task Assignment
----------------------

To update a task assignment, call the
`TasksManager.UpdateTaskAssignmentAsync(BoxTaskAssignmentUpdateRequest taskAssignmentUpdateRequest)`
method.  This can be used to resolve or complete a task.

Updating the resolution state:
<!-- sample put_task_assignments_id -->
```c#
var requestParams = new BoxTaskAssignmentUpdateRequest()
{
    Id = "12345",
    ResolutionState = ResolutionStateType.approved
};
BoxTaskAssignment updatedAssignment = await client.TasksManager.UpdateTaskAssignmentAsync(requestParams);
```

Updating the message:
<!-- sample put_task_assignments_id message -->
```c#
var requestParams = new BoxTaskAssignmentUpdateRequest()
{
    Id = "12345",
    Message = "Updated message"
};
BoxTaskAssignment updatedAssignment = await client.TasksManager.UpdateTaskAssignmentAsync(requestParams);
```

Remove Task Assignment
----------------------

To delete a task assignment, effectively unassigning a user from the task, call the
`TasksManager.DeleteTaskAssignmentAsync(string taskAssignmentId)`
method with the ID of the assignment to remove.

<!-- sample delete_task_assignments_id -->
```c#
await client.TasksManager.DeleteTaskAssignmentAsync("12345");
```
