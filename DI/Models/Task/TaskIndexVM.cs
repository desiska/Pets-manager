using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Models.Task
{
    public class TaskIndexVM
    {
        public string Text { get; set; }
        public ICollection<TaskVM> Items { get; set; }
        public IQueryable<Data.Entities.Task> SearchedItem { get; set; }
    }
}
