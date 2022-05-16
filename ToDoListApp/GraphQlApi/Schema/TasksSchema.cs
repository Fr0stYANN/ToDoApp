using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using GraphQL;
using GraphQL.Utilities;
namespace ToDoListApp.GraphQlApi.Schema
{
    public class TasksSchema : GraphQL.Types.Schema
    {
        public TasksSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<TasksQuery>();
        }
    }
}
