using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.ViewModels
{
    public class CategoriesIndexViewModel
    {
        public CreateCategoryViewModel CreateCategoryViewModel { get; set; }
        public List<Category> Categories { get; set; }
    }
}
