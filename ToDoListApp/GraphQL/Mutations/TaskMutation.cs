using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using Task = BusinessLogic.Models.Task;
using GraphQL.Types;
using BusinessLogic.Interfaces;
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
            Field<StringGraphType>(
                "changeDataProvider",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "providerName" }),
                resolve: context =>
                {
                    var providerName = context.GetArgument<string>("providerName");
                    var currentProvider = DataProvider.CurrentProvider;
                    DataProvider.ChangeProvider(providerName);
                    TaskMutation taskMutation = new TaskMutation(taskRepositories);
                    return $"current data provider has been changed from {currentProvider} to {providerName}";
                }
                );
        }
    }
}
