using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;


namespace ToDoListApp.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        string connectionString = null;
        public CategoryRepository(string conn)
        {
            connectionString = conn;
        }
        public async Task<List<Category>> GetCategories()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var result = await db.QueryAsync<Category>("Select * From Categories");
                return result.ToList();
            }
        }
        public async Task<int> CreateCategory(Category category)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "Insert INTO Categories(CategoryName) VALUES (@CategoryName)";
                 return (await db.ExecuteAsync(sqlQuery, category));
            }
        }
    }
}
