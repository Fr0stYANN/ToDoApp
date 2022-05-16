using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
namespace ToDoListApp.GraphQlApi.Schema
{
    public class RootQueries : ObjectGraphType
    {
        public RootQueries()
        {
            Field<CategoriesQuery>(
            "categories",
            resolve: context => new { });
            Field<TasksQuery>(
            "tasks",
            resolve: context => new { });
        }
    }
}
