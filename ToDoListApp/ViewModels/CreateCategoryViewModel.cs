﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required (ErrorMessage = "Category Name is Required")]
        public string CategoryName { get; set; }
    }
}
