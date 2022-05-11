
using AutoMapper;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.ViewModels;
using Task = BusinessLogic.Models.Task;
namespace ToDoListApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Task, CompletedTasksViewModel>();
            CreateMap<Task, NotCompletedTasksViewModel>();
            CreateMap<Category, CategoriesViewModel>();
            CreateMap<Category, CreateCategoryViewModel>().ReverseMap();
            CreateMap<Task, CreateTaskViewModel>().ReverseMap();
            //CreateMap<List<Task>, List<CompletedTasksViewModel>>();
            //CreateMap<List<Task>, List<NotCompletedTasksViewModel>>();
        }
    }
}
