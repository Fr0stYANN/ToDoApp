using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Utilities;
using GraphQL;
using GraphQL.Types;
namespace ToDoListApp.GraphQlApi.Schema
{
    public class CategoriesSchema : GraphQL.Types.Schema
    {
        public CategoriesSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<CategoriesQuery>();
        }
    }
}
