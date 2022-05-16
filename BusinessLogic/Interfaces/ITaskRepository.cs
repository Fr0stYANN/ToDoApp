using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
namespace BusinessLogic.Interfaces
{
    public interface ITaskRepository
    {
        string ProviderName { get; }
        List<BusinessLogic.Models.Task> GetCompletedTasks();
        List<BusinessLogic.Models.Task> GetNotCompletedTasks();
        Task<int> Create(BusinessLogic.Models.Task Task);
        BusinessLogic.Models.Task GetTaskById(int id);
        int Update(int TaskId, DateTime DoneDate);
        int Delete(int id);
    }
}