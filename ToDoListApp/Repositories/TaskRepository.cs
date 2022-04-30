using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ToDoListApp.Models
{
    public class TaskRepository : ITaskRepository
    {
        string connectionString = null;
        public TaskRepository(string conn)
        {
            connectionString = conn;
        }
        public async Task<List<ToDoListApp.Models.Task>> GetCompletedTasks()
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "Select * from tasks WHERE IsDone = 1 ORDER BY DoneDate DESC";
                var result = await db.QueryAsync<ToDoListApp.Models.Task>(sqlQuery);
                return result.ToList();                   
            }
        }
        public async Task<List<ToDoListApp.Models.Task>> GetNotCompletedTasks()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "SELECT * FROM Tasks WHERE IsDone = 0 Order By CASE WHEN DueDate IS NULL THEN 1 ELSE 0 END ASC, DueDate ASC";
                var result = await db.QueryAsync<ToDoListApp.Models.Task>(sqlQuery);
                return result.ToList();
            }
        }
        public async Task<List<ToDoListApp.Models.Task>> GetNotCompletedTasks(string CategoryId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "SELECT * FROM Tasks WHERE IsDone = 0 Order By CASE WHEN DueDate IS NULL THEN 1 ELSE 0 END ASC, DueDate ASC";
                var result = await db.QueryAsync<ToDoListApp.Models.Task>(sqlQuery);
                return result.ToList();
            }
        }
        public async Task<int> Create(Task task)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Tasks(TaskName,IsDone,DueDate,CategoryId) VALUES(@TaskName,@IsDone,@DueDate,@CategoryId)";
                return (await db.ExecuteAsync(sqlQuery, task));
            }
        }
        public async Task<int> Update(int TaskId,DateTime DoneDate)
        {
            var sqlQuery = "UPDATE Tasks SET DoneDate = @DoneDate, IsDone = 1 WHERE TaskId = @Id";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return (await db.ExecuteAsync(sqlQuery, new {DoneDate = DoneDate, Id = TaskId}));
            }
        }
        public async Task<ToDoListApp.Models.Task> GetTaskById(int id)
        {
            var sqlQuery = "SELECT * FROM Tasks where TaskId = @Id";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return(await db.QueryFirstOrDefaultAsync<ToDoListApp.Models.Task>(sqlQuery, new { Id = id }));
            }
        }
        public async Task<int> Delete(int id)
        {
            var sqlQuery = "Delete from Tasks where TaskId = @Id";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.ExecuteAsync(sqlQuery, new { Id = id });
            }
        }
        public async Task<List<Models.Task>> OrderByDueDate()
        {
            var sqlQuery = "SELECT * FROM Tasks Order BY DueDate DESC";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
               var result =  await db.QueryAsync<Task>(sqlQuery);
                return result.ToList();
            }
        }
        public async Task<List<Models.Task>> GetByCategory(string CategoryId)
        {
            var sqlQuery = "SELECT * FROM Tasks WHERE Category = @CategoryId";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var result = await db.QueryAsync<Task>(sqlQuery, new {CategoryId = CategoryId});
                return result.ToList();
            }
        }
    }
}
