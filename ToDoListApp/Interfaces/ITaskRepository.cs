using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public interface ITaskRepository
    {
        Task<List<Models.Task>> GetCompletedTasks();
        Task<List<Models.Task>> GetNotCompletedTasks();
        Task<int> Create(Task task);
        Task<int> Update(int TaskId, DateTime DoneDate);
        Task<int> Delete(int id);
        Task<ToDoListApp.Models.Task> GetTaskById(int id);
        Task<List<Models.Task>> OrderByDueDate();
        Task<List<Models.Task>> GetCompletedByCategory(int CategoryId);
        Task<List<Models.Task>> GetNotCompletedByCategory(int CategoryId);
    }
}
