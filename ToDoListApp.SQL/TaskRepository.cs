using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using BusinessLogic.Models;
using BusinessLogic.Interfaces;

namespace ToDoListApp.SQL
{
    public class TaskRepository : ITaskRepository
    {
        public string ProviderName => "SQL";
        string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog = ToDoListDB; Integrated Security=True";
        public List<BusinessLogic.Models.Task> GetCompletedTasks()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = "Select * from tasks WHERE IsDone = 1 ORDER BY DoneDate DESC";
                return (db.Query<BusinessLogic.Models.Task>(sqlQuery)).ToList();
            }
        }
        public List<BusinessLogic.Models.Task> GetNotCompletedTasks()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = "SELECT * FROM Tasks WHERE IsDone = 0 Order By CASE WHEN DueDate IS NULL THEN 1 ELSE 0 END ASC, DueDate ASC";
                var result = db.Query<BusinessLogic.Models.Task>(sqlQuery);
                return result.ToList();
            }
        }
        public async Task<int> Create(BusinessLogic.Models.Task task)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = "INSERT INTO Tasks(TaskName,IsDone,DueDate,CategoryId) VALUES(@TaskName,@IsDone,@DueDate,@CategoryId)";
                return (await db.ExecuteAsync(sqlQuery, task));
            }
        }
        public int Update(int TaskId, DateTime DoneDate)
        {
            var sqlQuery = "UPDATE Tasks SET DoneDate = @DoneDate, IsDone = 1 WHERE TaskId = @Id";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                db.Execute(sqlQuery, new { DoneDate = DoneDate, Id = TaskId });
            }
            return 0;
        }
        public BusinessLogic.Models.Task GetTaskById(int id)
        {
            var sqlQuery = "SELECT * FROM Tasks where TaskId = @Id";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return (db.QueryFirstOrDefault<BusinessLogic.Models.Task>(sqlQuery, new { Id = id }));
            }
        }
        public int Delete(int id)
        {
            var sqlQuery = "Delete from Tasks where TaskId = @Id";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Execute(sqlQuery, new { Id = id });
            }
            return 0;
        }
        //public async Task<List<BusinessLogic.Models.Task>> OrderByDueDate()
        //{
        //    var sqlQuery = "SELECT * FROM Tasks Order BY DueDate DESC";
        //    using (IDbConnection db = new SqlConnection(ConnectionString))
        //    {
        //        var result = await db.QueryAsync<BusinessLogic.Models.Task>(sqlQuery);
        //        return result.ToList();
        //    }
        //}
        //public async Task<List<BusinessLogic.Models.Task>> GetCompletedByCategory(int CategoryId)
        //{
        //    var sqlQuery = "SELECT * FROM Tasks WHERE CategoryId = @CategoryId AND IsDone = 1 Order By DoneDate desc";
        //    using (IDbConnection db = new SqlConnection(ConnectionString))
        //    {
        //        var result = await db.QueryAsync<BusinessLogic.Models.Task>(sqlQuery, new { CategoryId = CategoryId });
        //        return result.ToList();
        //    }
        //}
        //public async Task<List<BusinessLogic.Models.Task>> GetNotCompletedByCategory(int CategoryId)
        //{
        //    var sqlQuery = "SELECT * FROM Tasks WHERE CategoryId = @CategoryId AND IsDone = 0 Order By CASE WHEN DueDate IS NULL THEN 1 ELSE 0 END ASC, DueDate ASC";
        //    using (IDbConnection db = new SqlConnection(ConnectionString))
        //    {
        //        var result = await db.QueryAsync<BusinessLogic.Models.Task>(sqlQuery, new { CategoryId = CategoryId });
        //        return result.ToList();
        //    }
        //}
    }
}