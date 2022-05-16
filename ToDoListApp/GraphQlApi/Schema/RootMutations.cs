using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
namespace ToDoListApp.GraphQlApi.Schema
{
    public class RootMutations : ObjectGraphType
    {
        public RootMutations()
        {
            Field<CategoryMutation>(
                "categoiesMutations",
                resolve: context => new { });
            Field<TaskMutation>(
                "tasksMutations",
                resolve: context => new { });
        }
    }
}
