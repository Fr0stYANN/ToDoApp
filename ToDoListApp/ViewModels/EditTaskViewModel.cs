using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using Task = BusinessLogic.Models.Task;
namespace ToDoListApp.ViewModels
{
    public class EditTaskViewModel
    {
        public Task Task { get; set; }
        public List<Category> Categories { get; set; }
    }
}
