using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Utilities;
using GraphQL.Types;
using BusinessLogic.Models;
using BusinessLogic.Interfaces;
namespace ToDoListApp.GraphQL
{
    public class CategoriesQuery : ObjectGraphType
    {
        public CategoriesQuery(ICategoryRepository categoryRepository)
        {
            Field<ListGraphType<CategoryType>>(
                 "categories",
                  resolve: context => categoryRepository.GetCategories());
        }
    }
}
