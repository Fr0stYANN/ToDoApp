using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
namespace ToDoListApp.ViewModels
{
    public class TasksAndCategoryViewModel
    {
        public BusinessLogic.Models.Task Task { get; set; }
        public Category Category { get; set; }
        public List<BusinessLogic.Models.Task> CompletedTasks { get; set; }
        public List<BusinessLogic.Models.Task> NotCompletedTasks { get; set; }
        public List<Category> Categories { get; set; }
    }
}
