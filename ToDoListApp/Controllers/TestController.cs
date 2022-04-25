using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Models;
using ToDoListApp.ViewModels;
namespace ToDoListApp.Controllers
{
    public class TestController : Controller
    {
        ITaskRepository taskRepo;
        ICategoryRepository categoryRepo;
        public TestController(ITaskRepository taskRepository, ICategoryRepository categoryRepository)
        {
            taskRepo = taskRepository;
            categoryRepo = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            tasksAndCategoryViewModel.Tasks = await taskRepo.OrderByDueDate();
            return View("Index",tasksAndCategoryViewModel);
        }
    }
}
