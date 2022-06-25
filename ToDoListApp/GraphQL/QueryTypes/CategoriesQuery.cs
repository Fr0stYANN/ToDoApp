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
        private readonly ICategoryRepository categoryRepository;
        public CategoriesQuery(IEnumerable<ICategoryRepository> categoryRepositories)
        {
            categoryRepository = categoryRepositories.Where(t => t.ProviderName == DataProvider.CurrentProvider).FirstOrDefault();
            Field<ListGraphType<CategoryType>>(
                 "categories",
                  resolve: context => categoryRepository.GetCategories());
            Field<CategoryType>(
                "category",
                "Returns category by id",
                new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "categoryId"}),
                resolve: context =>
                {
                    var categoryId = context.GetArgument<int>("categoryId");
                    var result = categoryRepository.GetCategoryById(categoryId);
                    return result;
                });
        }
    }
}
