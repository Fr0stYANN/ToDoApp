using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp
{
    public static class FilterByCategory
    {
        public static int? CategoryIdFiltration { get; set; } = null;
        public static void ChangeFiltrationId(int id)
        {
            CategoryIdFiltration = id;
        }
    }
}
