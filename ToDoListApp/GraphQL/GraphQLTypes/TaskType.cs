using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using BusinessLogic.Models;
using Task = BusinessLogic.Models.Task;
namespace ToDoListApp.GraphQL
{
    public class TaskType : ObjectGraphType<Task>
    {
        public TaskType()
        {
            Field(t => t.TaskId, type: typeof(IdGraphType));
            Field(t => t.TaskName, type: typeof(StringGraphType));
            Field(t => t.CategoryId, type: typeof(IntGraphType));
            Field(t => t.DoneDate, type:typeof(DateTimeGraphType));
            Field(t => t.DueDate, type: typeof(DateTimeGraphType));
            Field(t => t.IsDone, type: typeof(BooleanGraphType));
        }
    }
}
