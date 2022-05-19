using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
namespace ToDoListApp.GraphQL
{
    public class RootMutations : ObjectGraphType
    {
        public RootMutations()
        {
            Field<CategoryMutation>(
                "categoriesMutations",
                resolve: context => new { });
            Field<TaskMutation>(
                "tasksMutations",
                resolve: context => new { });
        }
    }
}
