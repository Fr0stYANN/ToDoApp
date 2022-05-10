using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category Description is required")]
        public string CategoryName { get; set; }
    }
}
