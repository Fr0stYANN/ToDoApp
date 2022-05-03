using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.Business.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        [Required(ErrorMessage = "Task Description Is Required")]
        public string TaskName { get; set; }
        public bool IsDone { get; set; }
        public DateTime? DueDate { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? DoneDate { get; set; }
    }
}
