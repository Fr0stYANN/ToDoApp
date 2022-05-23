using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using GraphQL;
using GraphQL.Utilities;
namespace ToDoListApp.GraphQL
{
    public class ToDoAppSchema : Schema
    {
        public ToDoAppSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RootQueries>();
            Mutation = provider.GetRequiredService<RootMutations>();
        }
    }
}
