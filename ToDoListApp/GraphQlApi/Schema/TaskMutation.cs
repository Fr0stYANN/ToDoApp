using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using Task = BusinessLogic.Models.Task;
using GraphQL.Types;
using BusinessLogic.Interfaces;
namespace ToDoListApp.GraphQlApi.Schema
{
    public class TaskMutation : ObjectGraphType
    {
        public TaskMutation(ITaskRepository taskRepository)
        {
            Field<TaskType>(
                "createTask",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TaskInputType>> { Name = "task" }),
                resolve: context =>
                {
                    var task = context.GetArgument<Task>("task");
                    return taskRepository.Create(task);
                }
                );
            Field<TaskType>(
                "updateTask",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "taskId" }),
                resolve: context =>
                {
                    var taskId = context.GetArgument<int>("taskId");
                    taskRepository.Update(taskId, DateTime.Now);
                    return taskRepository.GetTaskById(taskId);
                }
                );
            Field<StringGraphType>(
                "deleteTask",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "taskId" }),
                resolve: context =>
                {
                    var taskId = context.GetArgument<int>("taskId");
                    var task = taskRepository.GetTaskById(taskId);
                    taskRepository.Delete(taskId);
                    return $"task with {taskId} has been deleted";
                }
                );
        }
    }
}
