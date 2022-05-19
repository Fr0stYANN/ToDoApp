using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
namespace ToDoListApp.GraphQL
{
    public class CategoryInputType : InputObjectGraphType
    {
        public CategoryInputType()
        {
            Name = "categoryInput";
            Field<IdGraphType>("categoryName");
        }
    }
}
