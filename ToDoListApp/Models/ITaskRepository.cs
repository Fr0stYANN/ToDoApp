using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public interface ITaskRepository
    {
        Task<List<Models.Task>> GetTasks();
        Task<int> Create(Task task);
        Task<int> Delete(int TaskId, DateTime DoneDate);
        Task<ToDoListApp.Models.Task> GetTaskById(int id);
    }
}
