using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Business.Models;

namespace ToDoListApp.Models
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<int> CreateCategory(Category category);
    }
}

