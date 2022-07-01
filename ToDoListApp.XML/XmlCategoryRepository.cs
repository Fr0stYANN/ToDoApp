using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;
using BusinessLogic.Interfaces;
using System.Xml.Serialization;
using System.IO;

namespace ToDoListApp.XML
{
    public class XmlCategoryRepository : ICategoryRepository
    {
        public string ProviderName => "XML";
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataContainer));
        public int CreateCategory(Category category)
        {
            int categoryId;
            DataContainer? data = new DataContainer();
            using (FileStream fs = new FileStream(@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml", FileMode.OpenOrCreate))
            {
                data = (DataContainer?)xmlSerializer.Deserialize(fs);
                if (data.Categories == null)
                {
                    category.CategoryId = 1;
                    categoryId = category.CategoryId;
                }
                else
                {
                    category.CategoryId = data.Categories.Count + 1;
                    categoryId = category.CategoryId;
                }
                data.Categories.Add(category);
            }
            using (FileStream fs = new FileStream(@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml", FileMode.Truncate))
            {
                xmlSerializer.Serialize(fs, data);
            }
            return categoryId;
        }

        public List<Category> GetCategories()
        {
            DataContainer? data;
            using (FileStream fs = new FileStream(@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml", FileMode.OpenOrCreate))
            {
                data = (DataContainer?)xmlSerializer.Deserialize(fs);
                return data.Categories;
            }
        }

        public void DeleteCategory(int id)
        {
            DataContainer? data;
            using (FileStream fs = new FileStream(@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml", FileMode.OpenOrCreate))
            {
                data = (DataContainer?)xmlSerializer.Deserialize(fs);
                int index = data.Categories.FindIndex(category => category.CategoryId == id);
                data.Categories.RemoveAt(index);
                for(int i = 0; i < data.Categories.Count; i++)
                {
                    data.Categories[i].CategoryId = i;
                }
            }
            using (FileStream fs = new FileStream(@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml", FileMode.Truncate))
            {
                xmlSerializer.Serialize(fs, data);
            }
        }

        public Category GetCategoryById(int id)
        {
            DataContainer? data;
            using (FileStream fs = new FileStream(@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml", FileMode.OpenOrCreate))
            {
                data = (DataContainer?)xmlSerializer.Deserialize(fs);
                var category = data.Categories.SingleOrDefault(c => c.CategoryId == id);
                return category;
            }
        }

        public int EditCategory(int categoryId, Category category)
        {
            DataContainer? data;
            using (FileStream fs = new FileStream(@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml", FileMode.OpenOrCreate))
            {
                data = (DataContainer?)xmlSerializer.Deserialize(fs);
                int index = data.Categories.FindIndex(category => category.CategoryId == categoryId);
                data.Categories[index] = category;
            }
            using (FileStream fs = new FileStream(@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml", FileMode.Truncate))
            {
                xmlSerializer.Serialize(fs, data);
            }
            return 0;
        }
    }
}
