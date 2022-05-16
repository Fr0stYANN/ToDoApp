using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
namespace ToDoListApp.GraphQlApi.Schema
{
    public class TaskInputType : InputObjectGraphType
    {
        public TaskInputType()
        {
            Name = "taskInput";
            Field<StringGraphType>("taskName");
            Field<IntGraphType>("categoryId");
            Field<DateTimeGraphType>("dueDate");
        }
    }
}
