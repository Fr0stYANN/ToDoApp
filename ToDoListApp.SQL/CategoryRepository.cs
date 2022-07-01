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
                var sqlQuery = "Insert INTO Categories(CategoryName) OUTPUT INSERTED.CategoryId VALUES (@CategoryName)";
                var categoryId = db.ExecuteScalar<int>(sqlQuery, category);
                return categoryId;
            }
        }

        public void DeleteCategory(int id)
        {
            //using (IDbConnection db = new SqlConnection(ConnectionString))
            //{
            //    var sqlQuery = "Update Tasks set CategoryId = 0 Where CategoryId = @CategoryId";
            //    db.Execute(sqlQuery, new { CategoryId = id });
            //}
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = "Delete from Categories Where CategoryID = @id";
                db.Execute(sqlQuery, new { id = id });
            }
        }

        public Category GetCategoryById(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = "Select * from Categories where CategoryId = @CategoryId";
                return db.QueryFirstOrDefault<Category>(sqlQuery, new { CategoryId = id });
            }
        }

        public int EditCategory(int categoryId, Category category)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = "Update Categories SET CategoryName = @CategoryName Where CategoryId = @CategoryId";
                return db.Execute(sqlQuery, new { CategoryId = categoryId, CategoryName = category.CategoryName });
            }
        }
    }
}