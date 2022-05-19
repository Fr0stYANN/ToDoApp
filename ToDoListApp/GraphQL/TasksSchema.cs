using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using GraphQL;
using GraphQL.Utilities;
namespace ToDoListApp.GraphQL
{
    public class TasksSchema : Schema
    {
        public TasksSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RootQueries>();
            Mutation = provider.GetRequiredService<RootMutations>();
        }
    }
}
