using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
namespace BusinessLogic.Interfaces
{
    public interface ITaskRepository
    {
        List<BusinessLogic.Models.Task> GetCompletedTasks();
        List<BusinessLogic.Models.Task> GetNotCompletedTasks();
        int Create(BusinessLogic.Models.Task Task);
        int Update(int TaskId, DateTime DoneDate);
        int Delete(int id);
        BusinessLogic.Models.Task GetTaskById(int id);
        List<BusinessLogic.Models.Task> OrderByDueDate();
        List<BusinessLogic.Models.Task> GetCompletedByCategory(int CategoryId);
        List<BusinessLogic.Models.Task> GetNotCompletedByCategory(int CategoryId);
    }
}