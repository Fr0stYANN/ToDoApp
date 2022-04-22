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
        public async Task<List<ToDoListApp.Models.Task>> GetTasks()
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "Select * from tasks";
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
        public async Task<int> Delete(int TaskId,DateTime DoneDate)
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
    }
}
