using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.GraphQL.InputTypes
{
    public class EditTaskInputType : InputObjectGraphType
    {
        public EditTaskInputType()
        {
            Name = "editTaskInput";
            Field<IntGraphType>("taskId");
            Field<StringGraphType>("taskName");
            Field<DateTimeGraphType>("dueDate");
            Field<DateTimeGraphType>("doneDate");
            Field<IntGraphType>("categoryId");
            Field<BooleanGraphType>("isDone");
        }
    }
}
