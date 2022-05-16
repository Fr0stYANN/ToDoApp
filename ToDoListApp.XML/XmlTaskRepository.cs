using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using BusinessLogic.Interfaces;
using System.Collections.Generic;
using Task = BusinessLogic.Models.Task;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ToDoListApp.XML
{
    public class XmlTaskRepository : ITaskRepository
    {
        public string ProviderName => "Xml";
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(DataContainer));
        public Task<int> Create(Task task)
        {
            DataContainer? data;
            using (FileStream fs = new FileStream(@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml", FileMode.OpenOrCreate))
            {
                data = (DataContainer?)xmlSerializer.Deserialize(fs);
                if (data == null || data.Tasks == null)
                    task.TaskId = 1;
                else
                    task.TaskId = data.Tasks.Count + 1;
            }
            using (FileStream fs = new FileStream(@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml", FileMode.Truncate))
            {
                data.Tasks.Add(task);
                xmlSerializer.Serialize(fs, data);
            }
            return null;
        }

        public int Delete(int id)
        {
            DataContainer? data;
            using (FileStream fs = new FileStream(@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml", FileMode.OpenOrCreate))
            {
                data = (DataContainer?)xmlSerializer.Deserialize(fs);
                int index = data.Tasks.FindIndex(task => task.TaskId == id);
                data.Tasks.RemoveAt(index);
            }
            using (FileStream fs = new FileStream(@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml", FileMode.Truncate))
            {
                xmlSerializer.Serialize(fs, data);
            }
            return 0;
        }
        public List<Task> GetCompletedTasks()
        {
            DataContainer? data;
            using (FileStream fs = new FileStream((@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml"), FileMode.OpenOrCreate))
            {
                data = (DataContainer?)xmlSerializer.Deserialize(fs);
                var result = from task in data.Tasks
                             where task.IsDone == true
                             orderby task.DoneDate ascending
                             select task;
                return result.ToList();
            }
        }
        public List<Task> GetNotCompletedTasks()
        {
            using (FileStream fs = new FileStream((@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml"), FileMode.OpenOrCreate))
            {
                DataContainer? data = (DataContainer?)xmlSerializer.Deserialize(fs);
                var result = from task in data.Tasks
                             where task.IsDone == false
                             orderby task.DueDate ascending
                             select task;
                return result.ToList();
            }
        }
        public Task GetTaskById(int id)
        {
            DataContainer? data;
            using (FileStream fs = new FileStream((@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml"), FileMode.OpenOrCreate))
            {
                data = (DataContainer?)xmlSerializer.Deserialize(fs);
                var task = data.Tasks.SingleOrDefault(task => task.TaskId == id);
                return task;
            }
        }
        public int Update(int TaskId, DateTime DoneDate)
        {
            DataContainer? data;
            using (FileStream fs = new FileStream((@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml"), FileMode.OpenOrCreate))
            {
                data = (DataContainer?)xmlSerializer.Deserialize(fs);
                int index = data.Tasks.FindIndex(task => task.TaskId == TaskId);
                data.Tasks[index].DoneDate = DoneDate;
                data.Tasks[index].IsDone = true;
            }
            using (FileStream fs = new FileStream(@"C:\Users\Phoenix\Desktop\Ism Company Course Projects\ToDoListApp\ToDoListApp.XML\Tasks.xml", FileMode.Truncate))
            {
                xmlSerializer.Serialize(fs, data);
            }
            return 0;
        }
    }
}