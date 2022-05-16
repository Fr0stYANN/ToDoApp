using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using BusinessLogic.Models;
namespace ToDoListApp.GraphQlApi.Schema
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType()
        {
            Field(c => c.CategoryId, type: typeof(IdGraphType));
            Field(c => c.CategoryName, type: typeof(StringGraphType));
        }
    }
}
