using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp
{
    public static class DataProvider
    {
        public static string CurrentProvider = "SQL";
        public static void ChangeProvider(string provider)
        {
            CurrentProvider = provider;
        }
    }
}
