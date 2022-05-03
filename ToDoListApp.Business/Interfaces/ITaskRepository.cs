using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Business.Models;
namespace ToDoListApp.Models
{
    public interface ITaskRepository
    {
        Task<List<Business.Models.Task>> GetCompletedTasks();
        Task<List<Business.Models.Task>> GetNotCompletedTasks();
        Task<int> Create(Business.Models.Task Task);
        Task<int> Update(int TaskId, DateTime DoneDate);
        Task<int> Delete(int id);
        Task<Business.Models.Task> GetTaskById(int id);
        Task<List<Business.Models.Task>> OrderByDueDate();
        Task<List<Business.Models.Task>> GetCompletedByCategory(int CategoryId);
        Task<List<Business.Models.Task>> GetNotCompletedByCategory(int CategoryId);
    }
}