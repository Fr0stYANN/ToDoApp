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
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Task = BusinessLogic.Models.Task;
namespace ToDoListApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository taskRepo;
        private readonly ICategoryRepository categoryRepo;
        private readonly IMapper mapper;
        public HomeController(IEnumerable<ITaskRepository> taskRepositories, IEnumerable<ICategoryRepository> categoryRepositories, IMapper mapper)
        {
            taskRepo = taskRepositories.Where(t => t.ProviderName == DataProvider.CurrentProvider).FirstOrDefault();
            categoryRepo = categoryRepositories.Where(t => t.ProviderName == DataProvider.CurrentProvider).FirstOrDefault();
            this.mapper = mapper;
        }
        public ActionResult Index()
        {
            //IndexViewModel indexViewModel = new IndexViewModel();
            var completedTasks = taskRepo.GetCompletedTasks();
            var notCompletedTasks = taskRepo.GetNotCompletedTasks();
            var categories = categoryRepo.GetCategories();
            return View("Index", new IndexViewModel()
            {
                Categories = mapper.
                Map<List<CategoriesViewModel>>(categories),
                CompletedTasksViewModels = mapper.
                Map<List<CompletedTasksViewModel>>(completedTasks),
                NotCompletedTasksViewModels = mapper.
                Map<List<NotCompletedTasksViewModel>>(notCompletedTasks)
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTask(CreateTaskViewModel createTaskViewModel)
        {
            if (!ModelState.IsValid)
            {
                var completedTasks = taskRepo.GetCompletedTasks();
                var notCompletedTasks = taskRepo.GetNotCompletedTasks();
                var categories = categoryRepo.GetCategories();
                return View("Index", new IndexViewModel()
                {
                    Categories = mapper.
                    Map<List<CategoriesViewModel>>(categories),
                    CompletedTasksViewModels = mapper.
                    Map<List<CompletedTasksViewModel>>(completedTasks),
                    NotCompletedTasksViewModels = mapper.
                    Map<List<NotCompletedTasksViewModel>>(notCompletedTasks)
                });
            }
            Task Task = mapper.Map<Task>(createTaskViewModel);
            /*await*/
            taskRepo.Create(Task);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public ActionResult UpdateTask(int Id)
        {
            taskRepo.Update(Id, DateTime.Now);
            return RedirectToAction(nameof(Index));
        }
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CreateCategoryViewModel createCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                var completedTasks = taskRepo.GetCompletedTasks();
                var notCompletedTasks = taskRepo.GetNotCompletedTasks();
                var categories = categoryRepo.GetCategories();
                return View("Index", new IndexViewModel()
                {
                    Categories = mapper.
                    Map<List<CategoriesViewModel>>(categories),
                    CompletedTasksViewModels = mapper.
                    Map<List<CompletedTasksViewModel>>(completedTasks),
                    NotCompletedTasksViewModels = mapper.
                    Map<List<NotCompletedTasksViewModel>>(notCompletedTasks)
                });
            }
            Category category = mapper.Map<Category>(createCategoryViewModel);
            categoryRepo.CreateCategory(category);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteTask(int Id)
        {
            if (ModelState.IsValid)
            {
                 taskRepo.Delete(Id);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ChangeDataProvider(string ProviderName)
        {
            DataProvider.ChangeProvider(ProviderName);
            return RedirectToAction("Index");
        }
    }
}
