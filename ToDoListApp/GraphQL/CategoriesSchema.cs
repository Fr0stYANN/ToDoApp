using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Utilities;
using GraphQL;
using GraphQL.Types;
namespace ToDoListApp.GraphQL
{
    public class CategoriesSchema : Schema
    {
        public CategoriesSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<CategoriesQuery>();
        }
    }
}
