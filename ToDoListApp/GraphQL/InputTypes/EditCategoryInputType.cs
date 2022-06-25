using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.GraphQL.InputTypes
{
    public class EditCategoryInputType : InputObjectGraphType
    {
        public EditCategoryInputType()
        {
            Name = "editCategoryInput";
            Field<IntGraphType>("categoryId");
            Field<StringGraphType>("categoryName");
        }
    }
}
