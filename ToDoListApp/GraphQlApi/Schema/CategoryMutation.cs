using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using GraphQL;
using BusinessLogic.Models;
using BusinessLogic.Interfaces;
namespace ToDoListApp.GraphQlApi.Schema
{
    public class CategoryMutation : ObjectGraphType
    {
        public CategoryMutation(ICategoryRepository categoryRepository)
        {
            Field<CategoryType>(
                "createCategory",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<CategoryInputType>> { Name = "category" }),
                resolve: context =>
                {
                    var category = context.GetArgument<Category>("category");
                    return categoryRepository.CreateCategory(category);
                }
                );
            Field<StringGraphType>(
                "deleteCategory",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "categoryId" }),
                resolve: context =>
                {
                    var categoryId = context.GetArgument<int>("categoryId");
                    return $"Category with {categoryId} has been deleted";
                }
                );
        }
    }
}
