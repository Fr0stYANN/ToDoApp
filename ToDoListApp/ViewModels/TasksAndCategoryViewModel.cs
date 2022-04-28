using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Models;
namespace ToDoListApp.ViewModels
{
    public class TasksAndCategoryViewModel
    {
        public ToDoListApp.Models.Task Task { get; set; }
        public Category Category { get; set; }
        public List<ToDoListApp.Models.Task> CompletedTasks { get; set; }
        public List<ToDoListApp.Models.Task> NotCompletedTasks { get; set; }
        public List<Category> Categories { get; set; }
    }
}
