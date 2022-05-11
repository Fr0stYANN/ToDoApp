using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = BusinessLogic.Models.Task;

namespace ToDoListApp.ViewModels
{
    public class IndexViewModel
    {
        public CreateTaskViewModel? CreateTaskViewModel { get; set; }
        public List<CategoriesViewModel>? Categories { get; set; }
        public List<NotCompletedTasksViewModel>? NotCompletedTasksViewModels { get; set; }
        public List<CompletedTasksViewModel>? CompletedTasksViewModels { get; set; }
    }
}
