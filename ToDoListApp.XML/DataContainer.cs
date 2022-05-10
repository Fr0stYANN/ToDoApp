using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;
using Task = BusinessLogic.Models.Task;
namespace ToDoListApp.XML
{
    public class DataContainer
    {
        public List<Task>? Tasks { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
