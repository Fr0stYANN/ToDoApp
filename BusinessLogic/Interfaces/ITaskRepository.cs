using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
namespace BusinessLogic.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<BusinessLogic.Models.Task>> GetCompletedTasks();
        Task<List<BusinessLogic.Models.Task>> GetNotCompletedTasks();
        Task<int> Create(BusinessLogic.Models.Task Task);
        Task<int> Update(int TaskId, DateTime DoneDate);
        Task<int> Delete(int id);
        Task<BusinessLogic.Models.Task> GetTaskById(int id);
        Task<List<BusinessLogic.Models.Task>> OrderByDueDate();
        Task<List<BusinessLogic.Models.Task>> GetCompletedByCategory(int CategoryId);
        Task<List<BusinessLogic.Models.Task>> GetNotCompletedByCategory(int CategoryId);
    }
}