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
using ToDoListApp.VieModels;

namespace ToDoListApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository taskRepo;
        private readonly ICategoryRepository categoryRepo;
        private readonly IMapper mapper;
        public TaskController(IEnumerable<ITaskRepository> taskRepositories, IEnumerable<ICategoryRepository> categoryRepositories, IMapper mapper)
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
            taskRepo.Create(Task);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public ActionResult UpdateTask(int Id)
        {
            taskRepo.Update(Id, DateTime.Now);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public ActionResult DeleteTask(int id)
        {
            if (ModelState.IsValid)
            {
                 taskRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ChangeDataProvider(string providerName)
        {
            DataProvider.ChangeProvider(providerName);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditTask(int taskId)
        {
            var task = taskRepo.GetTaskById(taskId);
            var categories = categoryRepo.GetCategories();
            return View("EditTask", new EditTaskViewModel() { 
             Task = task,
             Categories = categories
            });
        }
        [HttpPost]
        public ActionResult EditTask(Task Task)
        {
            var taskId = Task.TaskId;
            taskRepo.EditTask(taskId, Task);
            return RedirectToAction("Index");
        }
    }
}
