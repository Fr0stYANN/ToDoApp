using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using ToDoListApp.ViewModels;
using BusinessLogic;
using System.Web;
using BusinessLogic.Models;
using BusinessLogic.Interfaces;
using ToDoListApp.SQL;
using System.Xml;
using System.IO;
using ToDoListApp.XML;
namespace ToDoListApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository _taskRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ITaskRepository taskRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _taskRepo = taskRepository;
            _categoryRepo = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<ActionResult> Index()
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            tasksAndCategoryViewModel.CompletedTasks = /*await*/ _taskRepo.GetCompletedTasks();
            tasksAndCategoryViewModel.NotCompletedTasks =  _taskRepo.GetNotCompletedTasks();
            tasksAndCategoryViewModel.Categories =  _categoryRepo.GetCategories();
            return View(tasksAndCategoryViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTask(BusinessLogic.Models.Task task)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            if (!ModelState.IsValid)
            {
                tasksAndCategoryViewModel.CompletedTasks = /*await*/ _taskRepo.GetCompletedTasks();
                tasksAndCategoryViewModel.NotCompletedTasks = _taskRepo.GetNotCompletedTasks();
                tasksAndCategoryViewModel.Categories =  _categoryRepo.GetCategories();
                return View("CreateCategory");
            }
            /*await*/ _taskRepo.Create(task);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<ActionResult> UpdateTask(int Id)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
                _taskRepo.Update(Id, DateTime.Now);
                return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            if (!ModelState.IsValid)
            {
                tasksAndCategoryViewModel.CompletedTasks = /*await*/ _taskRepo.GetCompletedTasks();
                tasksAndCategoryViewModel.NotCompletedTasks = _taskRepo.GetNotCompletedTasks();
                tasksAndCategoryViewModel.Categories = _categoryRepo.GetCategories();
                return View("CreateCategory");
            }
            _categoryRepo.CreateCategory(category);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> DeleteTask(int Id)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            if (ModelState.IsValid)
            {
                 _taskRepo.Delete(Id);
                return RedirectToAction(nameof(Index));
            }
            return View("Index", tasksAndCategoryViewModel);
        }
        public async Task<ActionResult> SortNotCompletedByCategory(int CategoryId)
        {
            TasksAndCategoryViewModel tasksAndCategoryViewModel = new TasksAndCategoryViewModel();
            tasksAndCategoryViewModel.CompletedTasks = /*await*/ _taskRepo.GetCompletedTasks();
            if (tasksAndCategoryViewModel.CompletedTasks == null)
            {
                throw new Exception("There is no Task for such Category");
            }
            tasksAndCategoryViewModel.NotCompletedTasks = await _taskRepo.GetNotCompletedByCategory(CategoryId);
            tasksAndCategoryViewModel.Categories =  _categoryRepo.GetCategories();
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
            tasksAndCategoryViewModel.NotCompletedTasks =  _taskRepo.GetNotCompletedTasks();
            tasksAndCategoryViewModel.Categories =  _categoryRepo.GetCategories();
            return View("Index", tasksAndCategoryViewModel);
        }
    }
}
