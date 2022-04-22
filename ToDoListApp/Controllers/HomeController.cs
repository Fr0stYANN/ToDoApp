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
    public class HomeController : Controller
    {
        ITaskRepository taskRepo;
        ICategoryRepository categoryRepo;
        public HomeController(ITaskRepository taskRepository, ICategoryRepository categoryRepository)
        {
            taskRepo = taskRepository;
            categoryRepo = categoryRepository;
        }
        public async Task<ActionResult> Index()
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            tasksAndCategoryViewModel.Tasks = await taskRepo.GetTasks();
            tasksAndCategoryViewModel.Categories = await categoryRepo.GetCategories();
            tasksAndCategoryViewModel.Tasks.OrderByDescending(a => a.DueDate);
            return View(tasksAndCategoryViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Index(Models.Task task)
        {
            if (ModelState.IsValid)
            {
                await taskRepo.Create(task);
                return RedirectToAction("GetTasks");
            }
            return View("GetTasks");
        }
        [HttpPost]
        public async Task<ActionResult> CreateTask(Models.Task task)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            if (ModelState.IsValid)
            {
                await taskRepo.Create(task);
                tasksAndCategoryViewModel.Tasks = await taskRepo.GetTasks();
                tasksAndCategoryViewModel.Tasks.OrderByDescending(a => a.DueDate);
                return RedirectToAction(nameof(Index));
            }
            return View("Index", tasksAndCategoryViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteTask(int Id)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            Models.Task task = await taskRepo.GetTaskById(Id);
            task.IsDone = true;
            if (task.TaskId != null) {
                await taskRepo.Delete(task.TaskId, DateTime.Now);
                tasksAndCategoryViewModel.Tasks = await taskRepo.GetTasks();
                tasksAndCategoryViewModel.Tasks.OrderByDescending(a => a.DueDate);
                return RedirectToAction(nameof(Index));
            }
            return View("Index", tasksAndCategoryViewModel);
        }
        public async Task<ActionResult> CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            if (ModelState.IsValid)
            {
                await categoryRepo.CreateCategory(category);
                tasksAndCategoryViewModel.Categories = await categoryRepo.GetCategories();
                tasksAndCategoryViewModel.Tasks = await taskRepo.GetTasks();
                tasksAndCategoryViewModel.Tasks.OrderByDescending(a => a.DueDate);
                return RedirectToAction(nameof(Index));
            }
            return View("Index", tasksAndCategoryViewModel);
        }
    }
}
