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
            tasksAndCategoryViewModel.Tasks = await taskRepo.OrderByDueDate();
            tasksAndCategoryViewModel.Categories = await categoryRepo.GetCategories();
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
                tasksAndCategoryViewModel.Tasks = await taskRepo.OrderByDueDate();
                tasksAndCategoryViewModel.Categories = await categoryRepo.GetCategories();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", tasksAndCategoryViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateTask(int Id)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            Models.Task task = await taskRepo.GetTaskById(Id);
            task.IsDone = true;
            if (task.TaskId != null) {
                await taskRepo.Update(task.TaskId, DateTime.Now);
                tasksAndCategoryViewModel.Tasks = await taskRepo.OrderByDueDate();
                tasksAndCategoryViewModel.Categories = await categoryRepo.GetCategories();
                tasksAndCategoryViewModel.Tasks.OrderByDescending(task => task.DueDate);
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
                tasksAndCategoryViewModel.Tasks = await taskRepo.OrderByDueDate();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", tasksAndCategoryViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteTask(int Id)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            if (ModelState.IsValid)
            {
                await taskRepo.Delete(Id);
                tasksAndCategoryViewModel.Tasks = await taskRepo.OrderByDueDate();
                tasksAndCategoryViewModel.Categories = await categoryRepo.GetCategories();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", tasksAndCategoryViewModel);
        }
        public async Task<ActionResult> SelectOnlyCategory(string CategoryId)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            if (ModelState.IsValid)
            {
                tasksAndCategoryViewModel.Tasks = await taskRepo.GetByCategory(CategoryId);
                tasksAndCategoryViewModel.Categories = await categoryRepo.GetCategories();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", tasksAndCategoryViewModel);
        }
    }
}
