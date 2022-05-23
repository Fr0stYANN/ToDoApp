using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.VieModels;
using ToDoListApp.ViewModels;

namespace ToDoListApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ITaskRepository taskRepo;
        private readonly ICategoryRepository categoryRepo;
        private readonly IMapper mapper;
        public CategoryController(IEnumerable<ITaskRepository> taskRepositories, IEnumerable<ICategoryRepository> categoryRepositories, IMapper mapper)
        {
            taskRepo = taskRepositories.Where(t => t.ProviderName == DataProvider.CurrentProvider).FirstOrDefault();
            categoryRepo = categoryRepositories.Where(t => t.ProviderName == DataProvider.CurrentProvider).FirstOrDefault();
            this.mapper = mapper;
        }
        public ActionResult CreateCategory()
        {
            var categories = categoryRepo.GetCategories();
            return View("CreateCategory", new CategoriesIndexViewModel()
            {
                Categories = categories
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CreateCategoryViewModel createCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                var categories = categoryRepo.GetCategories();
                return View("CreateCategory", new CategoriesIndexViewModel()
                {
                    Categories = categories,
                    CreateCategoryViewModel = createCategoryViewModel
                });
            }
            Category category = mapper.Map<Category>(createCategoryViewModel);
            categoryRepo.CreateCategory(category);
            return RedirectToAction("CreateCategory");
        }
        [HttpGet]
        public ActionResult EditCategory(int categoryId)
        {
            var category = categoryRepo.GetCategoryById(categoryId);
            return View("EditCategory", new EditCategoryViewModel()
            {
                Category = category
            });
        }
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            var categoryId = category.CategoryId;
            categoryRepo.EditCategory(categoryId, category);
            return RedirectToAction("CreateCategory");
        }
        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            if (ModelState.IsValid)
            {
                categoryRepo.DeleteCategory(id);
                return RedirectToAction(nameof(CreateCategory));
            }
            return RedirectToAction(nameof(CreateCategory));
        }
    }
}
