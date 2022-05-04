using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.ViewModels;
using BusinessLogic;
using BusinessLogic.Models;
using BusinessLogic.Interfaces;
using ToDoListApp.SQL;

namespace ToDoListApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository _taskRepo;
        private readonly ICategoryRepository _categoryRepo;
        public HomeController(ITaskRepository taskRepository, ICategoryRepository categoryRepository)
        {
            _taskRepo = taskRepository;
            _categoryRepo = categoryRepository;
        }
        public async Task<ActionResult> Index()
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            var model  = await _taskRepo.GetCompletedTasks();
            var model = await _taskRepo.GetCompletedTasks();
            tasksAndCategoryViewModel.NotCompletedTasks = await _taskRepo.GetNotCompletedTasks();
            tasksAndCategoryViewModel.Categories = await _categoryRepo.GetCategories();
            return View(tasksAndCategoryViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTask(BusinessLogic.Models.Task task)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            tasksAndCategoryViewModel.CompletedTasks = await _taskRepo.GetCompletedTasks();
            tasksAndCategoryViewModel.NotCompletedTasks = await _taskRepo.GetNotCompletedTasks();
            tasksAndCategoryViewModel.Categories = await _categoryRepo.GetCategories();
            if (!ModelState.IsValid)
            {
                return View("Index", tasksAndCategoryViewModel);
            }
            await _taskRepo.Create(task);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<ActionResult> UpdateTask(int Id)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            BusinessLogic.Models.Task task = await _taskRepo.GetTaskById(Id);
            task.IsDone = true;
            if (task.TaskId != null) {
                await _taskRepo.Update(task.TaskId, DateTime.Now);
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
                await _categoryRepo.CreateCategory(category);
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
                await _taskRepo.Delete(Id);
                return RedirectToAction(nameof(Index));
            }
            return View("Index", tasksAndCategoryViewModel);
        }
        public async Task<ActionResult> SortNotCompletedByCategory(int CategoryId)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            tasksAndCategoryViewModel.CompletedTasks = await _taskRepo.GetCompletedTasks();
            if(tasksAndCategoryViewModel.CompletedTasks == null)
            {
                throw new Exception("There is no Task for such Category");
            }
            tasksAndCategoryViewModel.NotCompletedTasks = await _taskRepo.GetNotCompletedByCategory(CategoryId);
            tasksAndCategoryViewModel.Categories = await _categoryRepo.GetCategories();
            //if (ModelState.IsValid)
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            return View("Index", tasksAndCategoryViewModel);
        }
        public async Task<IActionResult> SortDoneByCategory(int CategoryId)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            tasksAndCategoryViewModel.CompletedTasks = await _taskRepo.GetCompletedByCategory(CategoryId);
            tasksAndCategoryViewModel.NotCompletedTasks = await _taskRepo.GetNotCompletedTasks();
            tasksAndCategoryViewModel.Categories = await _categoryRepo.GetCategories();
            return View("Index", tasksAndCategoryViewModel);
        }
    }
}
