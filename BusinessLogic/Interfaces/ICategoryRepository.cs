using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        int CreateCategory(Category category);
    }
}