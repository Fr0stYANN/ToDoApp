using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using GraphQL;
using BusinessLogic.Models;
using BusinessLogic.Interfaces;
using ToDoListApp.GraphQL.InputTypes;

namespace ToDoListApp.GraphQL
{
    public class CategoryMutation : ObjectGraphType
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryMutation(IEnumerable<ICategoryRepository> categoryRepositories)
        {
            categoryRepository = categoryRepositories.Where(c => c.ProviderName == DataProvider.CurrentProvider).FirstOrDefault();
            Field<CategoryType>(
                "createCategory",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<CategoryInputType>> { Name = "category" }),
                resolve: context =>
                {
                    var category = context.GetArgument<Category>("category");
                    categoryRepository.CreateCategory(category);
                    return category;
                }
                );
            Field<CategoryType>(
                "editCategory",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<EditCategoryInputType>> { Name = "category" }),
                resolve: context =>
                {
                    var category = context.GetArgument<Category>("category");
                    var categoryId = category.CategoryId;
                    categoryRepository.EditCategory(categoryId, category);
                    return category;
                }
                );
            Field<StringGraphType>(
                "deleteCategory",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "categoryId" }),
                resolve: context =>
                {
                    var categoryId = context.GetArgument<int>("categoryId");
                    categoryRepository.DeleteCategory(categoryId);
                    return $"Category with {categoryId} has been deleted";
                }
                );
        }
    }
}
