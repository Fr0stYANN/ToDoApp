using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using BusinessLogic.Models;
using BusinessLogic.Interfaces;
namespace ToDoListApp.SQL
{
    public class CategoryRepository : ICategoryRepository
    {
        string connectionString = null;
        public CategoryRepository(string conn)
        {
            connectionString = conn;
        }
        public List<Category> GetCategories()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var res = db.Query<Category>("Select * From Categories");
                return res.ToList();
            }
        }
        public int CreateCategory(Category category)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "Insert INTO Categories(CategoryName) VALUES (@CategoryName)";
                return db.Execute(sqlQuery, category);
            }
        }
    }
}