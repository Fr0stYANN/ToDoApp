using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using Task = BusinessLogic.Models.Task;
using GraphQL.Types;
using BusinessLogic.Interfaces;
using ToDoListApp.GraphQL.InputTypes;

namespace ToDoListApp.GraphQL
{
    public class TaskMutation : ObjectGraphType
    {
        private readonly ITaskRepository taskRepository;
        public TaskMutation(IEnumerable<ITaskRepository> taskRepositories)
        {
            taskRepository = taskRepositories.Where(t => t.ProviderName == DataProvider.CurrentProvider).FirstOrDefault();
            Field<TaskType>(
                "createTask",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TaskInputType>> { Name = "task" }),
                resolve: context =>
                {
                    var task = context.GetArgument<Task>("task");
                    var taskId =  taskRepository.Create(task);
                    var createdTask =  taskRepository.GetTaskById(taskId);
                    return createdTask;
                }
                );
            Field<TaskType>(
                "editTask",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<EditTaskInputType>> { Name = "task" }),
                resolve: context =>
                {
                    var task = context.GetArgument<Task>("task");
                    var taskId = task.TaskId;
                    taskRepository.EditTask(taskId, task);
                    return task;
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
            Field<StringGraphType>(
                "changeDataProvider",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "providerName" }),
                resolve: context =>
                {
                    var providerName = context.GetArgument<string>("providerName");
                    var currentProvider = DataProvider.CurrentProvider;
                    DataProvider.ChangeProvider(providerName);
                    var provider = DataProvider.CurrentProvider;
                    return provider;
                }
                );
        }
    }
}
