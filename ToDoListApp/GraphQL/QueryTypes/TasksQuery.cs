using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using Task = BusinessLogic.Models.Task;
using BusinessLogic.Models;
using BusinessLogic.Interfaces;
using GraphQL;

namespace ToDoListApp.GraphQL
{
    public class TasksQuery : ObjectGraphType
    {
        public TasksQuery(ITaskRepository taskRepository)
        {
            Field<ListGraphType<TaskType>>(
                "completedTasks",
                resolve: context => taskRepository.GetCompletedTasks()
                );
            Field<ListGraphType<TaskType>>(
                "notCompletedTasks",
                resolve: context => taskRepository.GetNotCompletedTasks()
                );
            Field<TaskType>(
                "task",
                "Returns task by id",
                new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "TaskId", Description = "Task Id" }),
                resolve: context => taskRepository.GetTaskById(context.GetArgument("TaskId",int.MinValue)));
        }
    }
}
