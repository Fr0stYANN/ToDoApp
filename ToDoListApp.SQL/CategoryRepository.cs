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
        public string ProviderName => "SQL";
        string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog = ToDoListDB; Integrated Security=True";
        public List<Category> GetCategories()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var res = db.Query<Category>("Select * From Categories");
                return res.ToList();
            }
        }
        public int CreateCategory(Category category)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = "Insert INTO Categories(CategoryName) VALUES (@CategoryName)";
                return db.Execute(sqlQuery, category);
            }
        }
    }
}